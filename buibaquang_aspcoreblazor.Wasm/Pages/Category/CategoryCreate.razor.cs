using Blazored.Toast.Services;
using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace buibaquang_aspcoreblazor.Wasm.Pages.Category
{
    public partial class CategoryCreate
    {
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        [Inject] private IToastService toastService { get; set; }

        private CategoryRequest category = new CategoryRequest();

        private async Task SubmitCategory(EditContext context)
        {
            var result = await categoryApiClient.CreateCategory(category);
            if (result)
            {
                toastService.ShowSuccess("Category created successfully.");
                NavigationManager.NavigateTo("/category");
            }
            else
                toastService.ShowError("Failed to create category.");
        }
    }
}
