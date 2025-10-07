using Domain.Entities;

namespace Domain.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll(Guid CompanyId);
    Category GetById(Guid id, Guid CompanyId);
    Category Create(Guid CompanyId, Category category);
    Category? Update(Guid CompanyId, Guid id, Category category);
    void Delete(Guid id, Guid CompanyId);
}