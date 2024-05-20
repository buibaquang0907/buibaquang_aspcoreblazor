using Blazored.Toast.Services;
using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Models.SeedWork;
using buibaquang_aspcoreblazor.Wasm.Components;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;

namespace buibaquang_aspcoreblazor.Wasm.Pages
{
    public partial class Product
    {
        [Inject] private IProductApiClient ProductApiClient { get; set; }
        [Inject] private ICategoryApiClient CategoryApiClient { get; set; }
        [Inject] private IToastService ToastService { get; set; }

        protected Confirmation DeleteConfirmation { get; set; }
        private Guid DeleteId { get; set; }

        private List<ProductModel> Products;
        private List<CategoryModel> Categories;

        [CascadingParameter]
        public Error? Error { get; set; }

        public MetaData MetaData { get; set; } = new MetaData();


        private ProductListSearch ProductListSearch = new ProductListSearch();


        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoryApiClient.GetCategories();
            await GetProducts();
        }

        private async Task GetProducts()
        {
            try
            {
                var pagingResponse = await ProductApiClient.GetProducts(ProductListSearch);
                Products = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }

        private async Task SelectedPage(int page)
        {
            ProductListSearch.PageNumber = page;
            await GetProducts();
        }

        // search product
        private async Task SearchForm(EditContext context)
        {
            ToastService.ShowInfo("Search Completed");
            await GetProducts();
        }

        private string GetCategoryName(Guid? categoryId)
        {
            var category = Categories.Find(c => c.Id == categoryId);
            return category?.Name ?? "N/A";
        }

        // delete product to confirmation
        private void OnDeleteProduct(Guid deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }
        private async Task OnConfirmDeleteProduct(bool deleteConfirmed)
        {
            if(deleteConfirmed)
            {
                var result = await ProductApiClient.DeleteProduct(DeleteId);
                if (result)
                {
                    ToastService.ShowSuccess("Product deleted successfully.");
                    await GetProducts();
                }
                else
                    ToastService.ShowError("Failed to deleted product.");
            }
        }
    }
}
