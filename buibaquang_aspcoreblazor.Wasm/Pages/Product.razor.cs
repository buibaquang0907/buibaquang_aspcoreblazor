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

        private List<ProductModel> Products;
        private List<CategoryModel> Categorys;

        private ProductListSearch ProductListSearch= new ProductListSearch();

        protected override async Task OnInitializedAsync()
        {
            Products = await productApiClient.GetProducts(ProductListSearch);
            Categorys = await categoryApiClient.GetCategories();
        }
        
        private async Task SearchForm(EditContext context)
        {
            Products = await productApiClient.GetProducts(ProductListSearch);
        }
    }


}
