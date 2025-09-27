using Application.Requests;
using Application.Responses;

namespace Domain.Services;

public interface ISupplierService
{
    IEnumerable<SupplierResponse> GetAll(Guid CompanyId);
    SupplierResponse GetById(Guid id, Guid CompanyId);
    SupplierResponse Create(Guid CompanyId, SupplierRequest supplier);
    SupplierResponse Update(Guid CompanyId, Guid id, SupplierRequest supplier);
    void Delete(Guid id, Guid CompanyId);
}