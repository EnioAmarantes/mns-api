using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Sale> GetAll(Guid CompanyId)
    {
        return _context.Sales
            .Include(s => s.Items)
            .Where(s => s.CompanyId == CompanyId)
            .ToList();
    }

    public Sale GetById(Guid id, Guid CompanyId)
    {
        return _context.Sales
            .Include(s => s.Items)
            .FirstOrDefault(s => s.Id == id && s.CompanyId == CompanyId);
    }

    public Sale Create(Guid CompanyId, Sale sale)
    {
        sale.CompanyId = CompanyId;
        if (sale.Items != null)
        {
            foreach (var item in sale.Items)
            {
                item.CompanyId = CompanyId;
            }
        }
        _context.Sales.Add(sale);
        _context.SaveChanges();
        return sale;
    }

    public Sale? Update(Guid CompanyId, Guid id, Sale sale)
    {
        var existingSale = _context.Sales
            .Include(s => s.Items)
            .FirstOrDefault(s => s.Id == id && s.CompanyId == CompanyId);

        if (existingSale == null) return null;

        existingSale.Date = sale.Date;
        existingSale.Status = sale.Status;

        // Update items
        _context.SaleItems.RemoveRange(existingSale.Items);
        existingSale.Items = sale.Items;

        _context.SaveChanges();
        return existingSale;
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        var sale = _context.Sales.FirstOrDefault(s => s.Id == id && s.CompanyId == CompanyId);
        if (sale != null)
        {
            _context.Sales.Remove(sale);
            _context.SaveChanges();
        }
    }
}