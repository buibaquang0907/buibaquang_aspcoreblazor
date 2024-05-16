using Blazored.Toast.Services;
using buibaquang_aspcoreblazor.Models.Models;
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

        private int CurrentPage = 1;
        private int TotalPages;
        private int ItemsPerPage = 5;

        private ProductListSearch ProductListSearch = new ProductListSearch();

        private List<ProductModel> PaginatedProducts => Products
            .Skip((CurrentPage - 1) * ItemsPerPage)
            .Take(ItemsPerPage)
            .ToList();

        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoryApiClient.GetCategories();
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            Products = await ProductApiClient.GetProducts(ProductListSearch);
            CalculateTotalPages();
        }

        private void CalculateTotalPages()
        {
            TotalPages = (int)Math.Ceiling(Products.Count / (double)ItemsPerPage);
        }
        private void OnItemsPerPageChanged(ChangeEventArgs e)
        {
            ItemsPerPage = int.Parse(e.Value.ToString());
            CurrentPage = 1;
            CalculateTotalPages();
        }

        private void ChangePage(int page)
        {
            if (page >= 1 && page <= TotalPages)
                CurrentPage = page;
        }

        // search product
        private async Task SearchForm(EditContext context)
        {
            ToastService.ShowInfo("Search Completed");
            CurrentPage = 1;
            await LoadProducts();
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
                    await LoadProducts();
                }
                else
                    ToastService.ShowError("Failed to deleted product.");
            }
        }
    }
}
