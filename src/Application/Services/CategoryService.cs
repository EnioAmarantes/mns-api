using Application.Requests;
using Application.Responses;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public CategoryResponse Create(Guid CompanyId, CategoryRequest category)
    {
        var newCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = category.Name,
            CompanyId = CompanyId
        };

        var createdCategory = _categoryRepository.Create(CompanyId, newCategory);
        return new CategoryResponse
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name
        };
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        _categoryRepository.Delete(id, CompanyId);
    }

    public IEnumerable<CategoryResponse> GetAll(Guid CompanyId)
    {
        return _categoryRepository.GetAll(CompanyId).Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public CategoryResponse GetById(Guid CompanyId, Guid id)
    {
        var category = _categoryRepository.GetById(id, CompanyId);
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public CategoryResponse Update(Guid CompanyId, Guid id, CategoryRequest category)
    {
        var categoryUpdate = new Category
        {
            Id = id,
            Name = category.Name,
            CompanyId = CompanyId
        };
        var updatedCategory = _categoryRepository.Update(CompanyId, id, categoryUpdate);

        if (updatedCategory == null)
        {
            throw new KeyNotFoundException("Category not found");
        }

        return new CategoryResponse
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name
        };
    }
}