using Blazored.Toast.Services;
using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace buibaquang_aspcoreblazor.Wasm.Pages
{
    public partial class Product
    {
        [Inject] private IProductApiClient productApiClient { get; set; }
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        [Inject] private IToastService toastService { get; set; }

        private List<ProductModel> Products;
        private List<CategoryModel> Categorys;

        private int CurrentPage = 1;
        private int TotalPages;
        private int ItemsPerPage = 5;

        private ProductListSearch ProductListSearch= new ProductListSearch();

        private List<ProductModel> PaginatedProducts => Products
        .Skip((CurrentPage - 1) * ItemsPerPage)
        .Take(ItemsPerPage)
        .ToList();

        protected override async Task OnInitializedAsync()
        {
            Categorys = await categoryApiClient.GetCategories();
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            Products = await productApiClient.GetProducts(ProductListSearch);
            CalculateTotalPages();
        }

        private void CalculateTotalPages()
        {
            TotalPages = (int)Math.Ceiling(Products.Count / (double)ItemsPerPage);
        }

        private async Task SearchForm(EditContext context)
        {
            toastService.ShowInfo("Search Completed");
            CurrentPage = 1;
            await LoadProducts();
        }

        private void OnItemsPerPageChanged(ChangeEventArgs e)
        {
            ItemsPerPage = int.Parse(e.Value.ToString());
            CurrentPage = 1;
            CalculateTotalPages();
        }

        private void ChangePage(int page)
        {
            if (page < 1 || page > TotalPages)
            {
                return;
            }

            CurrentPage = page;
        }

        private string GetCategoryName(Guid? categoryId)
        {
            var category = Categorys.FirstOrDefault(c => c.Id == categoryId);
            return category != null ? category.Name : "N/A";
        }
    }


}
