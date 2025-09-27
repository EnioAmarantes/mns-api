using Application.Requests;
using Application.Responses;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class WarehouseService : IWarehouseService
{

    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public WarehouseResponse Create(Guid CompanyId, WarehouseRequest warehouse)
    {
        var newWarehouse = new Domain.Entities.Warehouse
        {
            Id = Guid.NewGuid(),
            Name = warehouse.Name,
            CompanyId = CompanyId
        };
        _warehouseRepository.Create(CompanyId, newWarehouse);
        return new WarehouseResponse
        {
            Id = newWarehouse.Id,
            Name = newWarehouse.Name
        };
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        _warehouseRepository.Delete(id, CompanyId);
    }

    public IEnumerable<WarehouseResponse> GetAll(Guid CompanyId)
    {
        return _warehouseRepository.GetAll(CompanyId)
            .Select(w => new WarehouseResponse
            {
                Id = w.Id,
                Name = w.Name
            });
    }

    public WarehouseResponse GetById(Guid id, Guid CompanyId)
    {
        var warehouse = _warehouseRepository.GetById(id, CompanyId);
        return new WarehouseResponse
        {
            Id = warehouse.Id,
            Name = warehouse.Name
        };
    }

    public WarehouseResponse Update(Guid CompanyId, Guid id, WarehouseRequest warehouse)
    {
        var existingWarehouse = _warehouseRepository.GetById(id, CompanyId);

        existingWarehouse.Name = warehouse.Name;
        _warehouseRepository.Update(CompanyId, id, existingWarehouse);
        return new WarehouseResponse
        {
            Id = existingWarehouse.Id,
            Name = existingWarehouse.Name
        };
    }
}