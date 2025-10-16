using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    // Inject IUnitOfWork here if you have one
    // private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id.ToString());
        if (category is null) return null;
        return new CategoryDto { Id = category.Id, Name = category.Name };
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var categoryEntity = new Category { Name = categoryDto.Name };
        await _categoryRepository.AddAsync(categoryEntity);
        // _unitOfWork.SaveChangesAsync();
        return new CategoryDto { Id = categoryEntity.Id, Name = categoryEntity.Name };
    }

    public async Task UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id.ToString());
        if (categoryEntity is null)
            throw new KeyNotFoundException("Category not found.");

        categoryEntity.Name = categoryDto.Name;
        // The DbContext tracks the change. _unitOfWork.SaveChangesAsync(); will persist it.
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id.ToString());
        if (categoryEntity is null)
            throw new KeyNotFoundException("Category not found.");

        _categoryRepository.Remove(categoryEntity);
        // _unitOfWork.SaveChangesAsync();
    }
}
