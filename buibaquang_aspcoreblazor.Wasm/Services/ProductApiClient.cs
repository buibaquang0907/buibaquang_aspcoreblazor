using buibaquang_aspcoreblazor.Models.Models;
using System.Net.Http.Json;

namespace buibaquang_aspcoreblazor.Wasm.Services
{
    public interface IProductApiClient
    {
        Task<List<ProductModel>> GetProducts();
    }
    public class ProductApiClient : IProductApiClient
    {
        public HttpClient _httpClient;

        public ProductApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductModel>> GetProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductModel>>("/api/Products");
            return result;
        }
    }
}
