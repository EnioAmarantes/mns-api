using Application.Requests;
using Application.Responses;

namespace Domain.Services;

public interface ISaleService
{
    IEnumerable<SaleResponse> GetAll(Guid CompanyId);
    SaleResponse GetById(Guid id, Guid CompanyId);
    SaleResponse Create(Guid CompanyId, SaleRequest sale);
    SaleResponse Update(Guid CompanyId, Guid id, SaleRequest sale);
    void Delete(Guid id, Guid CompanyId);
}