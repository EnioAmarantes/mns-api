using Domain.Entities;
using Domain.Repositories;
using Infra.Data;

namespace Infra.Repositories;

public class FinanceRepository : IFinanceRepository
{
    private readonly AppDbContext _context;

    public FinanceRepository(AppDbContext context)
    {
        _context = context;
    }
    public Finance Create(Guid CompanyId, Finance finance)
    {
        finance.CompanyId = CompanyId;
        _context.Finances.Add(finance);
        _context.SaveChanges();
        return finance;
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        var finance = _context.Finances.Find(id);
        if (finance != null && finance.CompanyId == CompanyId)
        {
            _context.Finances.Remove(finance);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Finance> GetAll(Guid CompanyId)
    {
        return _context.Finances.Where(f => f.CompanyId == CompanyId).ToList();
    }

    public Finance GetById(Guid id, Guid CompanyId)
    {
        return _context.Finances.FirstOrDefault(f => f.Id == id && f.CompanyId == CompanyId);
    }

    public Finance? Update(Guid CompanyId, Guid id, Finance finance)
    {
        var existingFinance = _context.Finances.Find(id);
        if (existingFinance != null && existingFinance.CompanyId == CompanyId)
        {
            existingFinance.Type = finance.Type;
            existingFinance.Description = finance.Description;
            existingFinance.Amount = finance.Amount;
            existingFinance.DueDate = finance.DueDate;
            existingFinance.Status = finance.Status;
            _context.SaveChanges();
            return existingFinance;
        }
        return existingFinance;
    }
}