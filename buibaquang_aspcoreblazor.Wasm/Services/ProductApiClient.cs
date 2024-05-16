using buibaquang_aspcoreblazor.Models.Models;
using System;
using System.Net.Http.Json;

namespace buibaquang_aspcoreblazor.Wasm.Services
{
    public interface IProductApiClient
    {
        Task<List<ProductModel>> GetProducts(ProductListSearch productListSearch);
        Task<ProductModel> GetProductById(string productId);

        Task<bool> CreateProduct(ProductRequest request);
        Task<bool> UpdateProduct(Guid id,ProductRequest request);
        Task<bool> DeleteProduct(Guid id);

    }
    public class ProductApiClient : IProductApiClient
    {
        public HttpClient _httpClient;

        public ProductApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateProduct(ProductRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Products", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Products/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<ProductModel> GetProductById(string productId)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductModel>($"/api/Products/{productId}");
            return result;
        }

        public async Task<List<ProductModel>> GetProducts(ProductListSearch productListSearch)
        {
            var url = $"/api/Products?Name={productListSearch.Name}&CategoryId={productListSearch.CategoryId}&Price={productListSearch.Price}";
            var result = await _httpClient.GetFromJsonAsync<List<ProductModel>>(url);
            return result;
        }

        public async Task<bool> UpdateProduct(Guid id, ProductRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Products/{id}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
