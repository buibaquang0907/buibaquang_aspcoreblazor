using Blazored.Toast.Services;
using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Components;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components;

namespace buibaquang_aspcoreblazor.Wasm.Pages
{
    public partial class Category
    {
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        [Inject] private IToastService ToastService { get; set; }

        private List<CategoryModel> Categorys;

        protected Confirmation DeleteConfirmation { get; set; }
        private Guid DeleteId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Categorys = await categoryApiClient.GetCategories();
        }
        private void OnDeleteCategory(Guid deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }
        private async Task OnConfirmDeleteCategory(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                var result = await categoryApiClient.DeleteCategory(DeleteId);
                if (result)
                {
                    ToastService.ShowSuccess("Category deleted successfully.");
                    Categorys = await categoryApiClient.GetCategories();
                }
                else
                    ToastService.ShowError("Failed to deleted category.");
            }
        }
    }
}
