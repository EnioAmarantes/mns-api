using Domain.Entities;

namespace Domain.Repositories;

public interface IFinanceRepository
{
    IEnumerable<Finance> GetAll(Guid CompanyId);
    Finance GetById(Guid id, Guid CompanyId);
    Finance Create(Guid CompanyId, Finance finance);
    Finance? Update(Guid CompanyId, Guid id, Finance finance);
    void Delete(Guid id, Guid CompanyId);
}