using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        readonly ICategoryRepository  _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(ICategoryService categoryService, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id.ToString());
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            await _categoryRepository.AddAsync(category);
           await _unitOfWork.SaveAllChangesAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id) return BadRequest();
             _categoryRepository.Update(category);
             await _unitOfWork.SaveAllChangesAsync();
            
            return NoContent();
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id.ToString());
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Remove(category);
            await _unitOfWork.SaveAllChangesAsync();
            return NoContent();
        }
    }
}
