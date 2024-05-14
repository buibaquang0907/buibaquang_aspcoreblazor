using buibaquang_aspcoreblazor.Models.Models;
using System.Net.Http.Json;

namespace buibaquang_aspcoreblazor.Wasm.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryModel>> GetCategories();
    }
    public class CategoryApiClient : ICategoryApiClient
    {
        public HttpClient _httpClient;

        public CategoryApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CategoryModel>> GetCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CategoryModel>>("/api/Categories");
            return result;
        }
    }
}
