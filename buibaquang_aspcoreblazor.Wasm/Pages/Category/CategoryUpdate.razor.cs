using Blazored.Toast.Services;
using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace buibaquang_aspcoreblazor.Wasm.Pages.Category
{
    public partial class CategoryUpdate
    {
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private CategoryRequest category;

        [Parameter]
        public string CategoryId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var productUpdate = await categoryApiClient.GetCategoryById(CategoryId);
            category = new CategoryRequest();
            category.Name = productUpdate.Name;
            category.Image = productUpdate.Image;
        }
        private async Task SubmitCategory(EditContext context)
        {
            var result = await categoryApiClient.UpdateCategory(Guid.Parse(CategoryId), category);
            if (result)
            {
                ToastService.ShowSuccess("Category updated successfully.");
                NavigationManager.NavigateTo("/category");
            }
            else
                ToastService.ShowError("Failed to updated category.");
        }
    }
}
