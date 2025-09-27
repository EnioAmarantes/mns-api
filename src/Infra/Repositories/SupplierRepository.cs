
using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }

    public Supplier Create(Guid CompanyId, Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
        return supplier;
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier != null && supplier.CompanyId == CompanyId)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Supplier> GetAll(Guid CompanyId)
    {
        return _context.Suppliers
            .Where(s => s.CompanyId == CompanyId)
            .AsNoTracking()
            .ToList();
    }

    public Supplier GetById(Guid id, Guid CompanyId)
    {
        return _context.Suppliers
            .AsNoTracking()
            .FirstOrDefault(s => s.Id == id && s.CompanyId == CompanyId);
    }

    public Supplier Update(Guid CompanyId, Guid id, Supplier supplier)
    {
        var existingSupplier = _context.Suppliers.Find(id);
        if (existingSupplier != null && existingSupplier.CompanyId == CompanyId)
        {
            existingSupplier.Name = supplier.Name;
            existingSupplier.CNPJ = supplier.CNPJ;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Phone = supplier.Phone;
            existingSupplier.Address = supplier.Address;
            _context.SaveChanges();
            return existingSupplier;
        }
        return null;
    }
}