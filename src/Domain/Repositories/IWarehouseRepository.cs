using Domain.Entities;

namespace Domain.Repositories;

public interface IWarehouseRepository
{
    IEnumerable<Warehouse> GetAll(Guid CompanyId);
    Warehouse GetById(Guid id, Guid CompanyId);
    Warehouse Create(Guid CompanyId, Warehouse warehouse);
    Warehouse? Update(Guid CompanyId, Guid id, Warehouse warehouse);
    void Delete(Guid id, Guid CompanyId);
}
