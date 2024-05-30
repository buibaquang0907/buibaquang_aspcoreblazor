using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;

namespace buibaquang_aspcoreblazor.Wasm.Pages.Product
{
    public partial class ProductCreate
    {
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        [Inject] private IProductApiClient productApiClient { get; set; }
        [Inject] private IToastService toastService { get; set; }

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
                toastService.ShowSuccess("Product created successfully.");
                NavigationManager.NavigateTo("/product");
            }
            else
                toastService.ShowError("Failed to create product.");
        }
    }
}
