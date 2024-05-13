using buibaquang_aspcoreblazor.Api.Entities;
using buibaquang_aspcoreblazor.Api.Repositories;
using buibaquang_aspcoreblazor.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace buibaquang_aspcoreblazor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetCategoryList();
            var productModel = categories.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image != null ? x.Image : "N/A",
            });
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepository.Create(new Category()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Image = request.Image,
            });

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, CategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDb = await _categoryRepository.GetById(id);
            if (categoryDb == null)
                return NotFound($"{id} is not found");

            categoryDb.Name = request.Name;
            categoryDb.Image = request.Image;

            var category = await _categoryRepository.Update(categoryDb);
            return Ok(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image != null ? category.Image : "N/A"
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepository.GetById(id);
            if (category == null)
                return NotFound($"{id} is not found");

            var categoryDel = await _categoryRepository.Delete(category);
            return Ok(categoryDel != null ? "success" : "false");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var category = await _categoryRepository.GetById(id);

            if (category == null)
                return NotFound($"{id} is not found");

            return Ok(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image != null ? category.Image : "N/A"
            });
        }
    }
}
