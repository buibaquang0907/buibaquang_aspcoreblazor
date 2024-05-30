using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Blazored.Toast.Services;

namespace buibaquang_aspcoreblazor.Wasm.Pages.Product
{
    public partial class ProductUpdate
    {
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        [Inject] private IProductApiClient productApiClient { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private List<CategoryModel> Categorys;

        private ProductRequest product;

        [Parameter]
        public string ProductId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Categorys = await categoryApiClient.GetCategories();
            var productUpdate = await productApiClient.GetProductById(ProductId);
            product = new ProductRequest();
            product.Name = productUpdate.Name;
            product.Description = productUpdate.Description;
            product.Price = productUpdate.Price;
            product.Image = productUpdate.Image;
            product.CategoryId = productUpdate.CategoryId;
        }

        private async Task SubmitProduct(EditContext context)
        {
            var result = await productApiClient.UpdateProduct(Guid.Parse(ProductId), product);
            if (result)
            {
                ToastService.ShowSuccess("Product updated successfully.");
                NavigationManager.NavigateTo("/product");
            }
            else
                ToastService.ShowError("Failed to updated product.");
        }
    }
}
