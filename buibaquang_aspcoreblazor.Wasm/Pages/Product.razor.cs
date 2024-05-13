using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components;

namespace buibaquang_aspcoreblazor.Wasm.Pages
{
    public partial class Product
    {
        [Inject] private IProductApiClient productApiClient { get; set; }

        private List<ProductModel> Products;

        protected override async Task OnInitializedAsync()
        {
            Products = await productApiClient.GetProducts();
        }
    }
}
