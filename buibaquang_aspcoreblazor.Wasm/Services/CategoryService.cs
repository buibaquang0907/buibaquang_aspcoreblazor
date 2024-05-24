using buibaquang_aspcoreblazor.Models.Models;
using System.Net.Http.Json;

namespace buibaquang_aspcoreblazor.Wasm.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryModel>> GetCategories();
        Task<CategoryModel> GetCategoryById(string categoryId);
        Task<bool> CreateCategory(CategoryRequest request);
        Task<bool> UpdateCategory(Guid id, CategoryRequest request);
        Task<bool> DeleteCategory(Guid id);
    }
    public class CategoryService : ICategoryApiClient
    {
        public HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CategoryModel>> GetCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryModel>>("/api/Categories");
            return result;
        }
        public async Task<CategoryModel> GetCategoryById(string categoryId)
        {
            var result = await _httpClient.GetFromJsonAsync<CategoryModel>($"/api/Categories/{categoryId}");
            return result;
        }

        public async Task<bool> CreateCategory(CategoryRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Categories", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCategory(Guid id, CategoryRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Categories/{id}", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Categories/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
