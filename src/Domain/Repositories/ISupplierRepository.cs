using Domain.Entities;

namespace Domain.Repositories;

public interface ISupplierRepository
{
    IEnumerable<Supplier> GetAll(Guid CompanyId);
    Supplier GetById(Guid id, Guid CompanyId);
    Supplier Create(Guid CompanyId, Supplier supplier);
    Supplier? Update(Guid CompanyId, Guid id, Supplier supplier);
    void Delete(Guid id, Guid CompanyId);
}