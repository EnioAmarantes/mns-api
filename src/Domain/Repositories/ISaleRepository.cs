using Domain.Entities;

namespace Domain.Repositories;

public interface ISaleRepository
{
    IEnumerable<Sale> GetAll(Guid CompanyId);
    Sale GetById(Guid id, Guid CompanyId);
    Sale Create(Guid CompanyId, Sale sale);
    Sale? Update(Guid CompanyId, Guid id, Sale sale);
    void Delete(Guid id, Guid CompanyId);
}