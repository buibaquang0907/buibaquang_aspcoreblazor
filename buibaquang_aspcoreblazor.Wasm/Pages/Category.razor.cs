using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components;

namespace buibaquang_aspcoreblazor.Wasm.Pages
{
    public partial class Category
    {
        [Inject] private ICategoryApiClient categoryApiClient { get; set; }
        private List<CategoryModel> Categorys;
        protected override async Task OnInitializedAsync()
        {
            Categorys = await categoryApiClient.GetCategories();
        }
    }
}
