using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Models.SeedWork;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http.Json;

namespace buibaquang_aspcoreblazor.Wasm.Services
{
    public interface IProductApiClient
    {
        //Task<List<ProductModel>> GetProducts(ProductListSearch productListSearch);
        Task<PageList<ProductModel>> GetProducts(ProductListSearch productListSearch);
        Task<ProductModel> GetProductById(string productId);
        Task<bool> CreateProduct(ProductRequest request);
        Task<bool> UpdateProduct(Guid id, ProductRequest request);
        Task<bool> DeleteProduct(Guid id);
    }
    public class ProductApiClient : IProductApiClient
    {
        public HttpClient _httpClient;

        public ProductApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<List<ProductModel>> GetProducts(ProductListSearch productListSearch)
        public async Task<PageList<ProductModel>> GetProducts(ProductListSearch productListSearch)
        {
            //var url = $"/api/Products?Name={productListSearch.Name}" +
            //    $"&CategoryId={productListSearch.CategoryId}" +
            //    $"&Price={productListSearch.Price}";

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(productListSearch.Name))
                queryStringParam.Add("name", productListSearch.Name);
            if (productListSearch.Price.HasValue)
                queryStringParam.Add("price", productListSearch.Price.ToString());
            if (productListSearch.CategoryId.HasValue)
                queryStringParam.Add("categoryId", productListSearch.CategoryId.ToString());

            string url = QueryHelpers.AddQueryString("/api/products", queryStringParam);

            //var result = await _httpClient.GetFromJsonAsync<List<ProductModel>>(url);
            var result = await _httpClient.GetFromJsonAsync<PageList<ProductModel>>(url);
            return result;
        }

        public async Task<ProductModel> GetProductById(string productId)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductModel>($"/api/Products/{productId}");
            return result;
        }

        public async Task<bool> CreateProduct(ProductRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Products", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProduct(Guid id, ProductRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Products/{id}", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Products/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
