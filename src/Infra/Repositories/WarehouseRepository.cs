using Domain.Entities;
using Domain.Repositories;
using Infra.Data;

namespace Infra.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly AppDbContext _context;

    public WarehouseRepository(AppDbContext context)
    {
        _context = context;
    }

    public Warehouse Create(Guid CompanyId, Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        _context.SaveChanges();
        return warehouse;
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        var warehouse = _context.Warehouses.Find(id);
        if (warehouse != null && warehouse.CompanyId == CompanyId)
        {
            _context.Warehouses.Remove(warehouse);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Warehouse> GetAll(Guid CompanyId)
    {
        return _context.Warehouses.Where(w => w.CompanyId == CompanyId).ToList();
    }

    public Warehouse GetById(Guid id, Guid CompanyId)
    {
        return _context.Warehouses.FirstOrDefault(w => w.Id == id && w.CompanyId == CompanyId);
    }

    public Warehouse Update(Guid CompanyId, Guid id, Warehouse warehouse)
    {
        var existingWarehouse = _context.Warehouses.Find(id);
        if (existingWarehouse != null && existingWarehouse.CompanyId == CompanyId)
        {
            existingWarehouse.Name = warehouse.Name;
            _context.SaveChanges();
            return existingWarehouse;
        }
        return existingWarehouse;
    }
}