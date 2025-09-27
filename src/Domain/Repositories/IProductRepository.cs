using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAll(Guid CompanyId);
    Product GetById(Guid id, Guid CompanyId);
    Product Create(Guid CompanyId, Product product);
    Product? Update(Guid CompanyId, Guid id, Product product);
    void Delete(Guid id, Guid CompanyId);
}
