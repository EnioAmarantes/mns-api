using Application.Requests;
using Application.Responses;

namespace Domain.Services;

public interface IWarehouseService
{
    IEnumerable<WarehouseResponse> GetAll(Guid CompanyId);
    WarehouseResponse GetById(Guid id, Guid CompanyId);
    WarehouseResponse Create(Guid CompanyId, WarehouseRequest warehouse);
    WarehouseResponse Update(Guid CompanyId, Guid id, WarehouseRequest warehouse);
    void Delete(Guid id, Guid CompanyId);
}