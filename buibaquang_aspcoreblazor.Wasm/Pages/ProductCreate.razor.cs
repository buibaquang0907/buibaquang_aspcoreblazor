using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace buibaquang_aspcoreblazor.Wasm.Pages
{
    public partial class ProductCreate
    {
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        [Inject] private IProductApiClient productApiClient { get; set; }


        private List<CategoryModel> Categorys;

        private ProductRequest product = new ProductRequest();


        protected override async Task OnInitializedAsync()
        {
            Categorys = await categoryApiClient.GetCategories();
        }

        private async Task SubmitProduct(EditContext context)
        {
            var result = await productApiClient.CreateProduct(product);
            if (result)
            {
                NavigationManager.NavigateTo("/product");
            }
        }
    }
}
