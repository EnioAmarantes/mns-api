using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public Category Create(Guid CompanyId, Category category)
    {
        var entity = category with { Id = Guid.NewGuid(), CompanyId = CompanyId };
        _context.Categories.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        var entity = GetById(id, CompanyId);
        if (entity != null)
        {
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Category> GetAll(Guid CompanyId)
    {
        return _context.Categories
            .Where(c => c.CompanyId == CompanyId)
            .AsNoTracking()
            .ToList();
    }

    public Category GetById(Guid id, Guid CompanyId)
    {
        Console.WriteLine($"Fetching Category with Id: {id} for CompanyId: {CompanyId}");
        return _context.Categories
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == id && c.CompanyId == CompanyId);
    }

    public Category? Update(Guid CompanyId, Guid id, Category category)
    {
        var entity = GetById(id, CompanyId);
        if (entity != null)
        {
            var updateCategory = new Category
            {
                Id = entity.Id,
                CompanyId = entity.CompanyId,
                Name = category.Name
            };
            _context.Entry(entity).State = EntityState.Detached;
            _context.Categories.Update(updateCategory);
            _context.SaveChanges();
            return updateCategory;
        }
        return entity;
    }
}