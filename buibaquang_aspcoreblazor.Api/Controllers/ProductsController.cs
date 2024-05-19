using buibaquang_aspcoreblazor.Api.Entities;
using buibaquang_aspcoreblazor.Api.Repositories;
using buibaquang_aspcoreblazor.Models.Models;
using buibaquang_aspcoreblazor.Models.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace buibaquang_aspcoreblazor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductListSearch productListSearch)
        {
            var pagedList = await _productRepository.GetProductList(productListSearch);
            //var productModel = pagedList.Select(x => new ProductModel
            var productModel = pagedList.Items.Select(x => new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description != null ? x.Description : "N/A",
                Image = x.Image != null ? x.Image : "N/A",
                Price = x.Price,
                CategoryId = x.CategoryId
            });
            //return Ok(productModel);
            return Ok(
                    new PageList<ProductModel>(productModel.ToList(),
                        pagedList.MetaData.TotalCount,
                        pagedList.MetaData.CurrentPage,
                        pagedList.MetaData.PageSize)
                );
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _productRepository.Create(new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                Price = request.Price,
                CategoryId = request.CategoryId
            });
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productDb = await _productRepository.GetById(id);
            if (productDb == null)
                return NotFound($"{id} is not found");

            productDb.Name = request.Name;
            productDb.Description = request.Description;
            productDb.Price = request.Price;
            productDb.Image = request.Image;
            productDb.CategoryId = request.CategoryId;

            var product = await _productRepository.Update(productDb);
            return Ok(new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description != null ? product.Description : "N/A",
                Image = product.Image != null ? product.Image : "N/A",
                Price = product.Price,
                CategoryId = product.CategoryId
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepository.GetById(id);
            if (product == null)
                return NotFound($"{id} is not found");

            var productDel = await _productRepository.Delete(product);
            return Ok(productDel != null ? "success" : "false");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound($"{id} is not found");

            return Ok(new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description != null ? product.Description : "N/A",
                Image = product.Image != null ? product.Image : "N/A",
                Price = product.Price,
                CategoryId = product.CategoryId
            });
        }
    }
}
