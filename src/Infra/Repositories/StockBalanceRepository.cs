using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class StockBalanceRepository : IStockBalanceRepository
{
    private readonly AppDbContext _context;

    public StockBalanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<StockBalance> GetAll(Guid companyId)
    {
        return _context.StockBalances
            .Include(sb => sb.Warehouse)
            .Where(sb => sb.CompanyId == companyId)
            .ToList();
    }

    public StockBalance GetById(Guid id, Guid companyId)
    {
        return _context.StockBalances
            .Include(sb => sb.Warehouse)
            .FirstOrDefault(sb => sb.Id == id && sb.CompanyId == companyId);
    }

    public StockBalance GetByProductAndWarehouse(Guid productId, Guid warehouseId, Guid companyId)
    {
        return _context.StockBalances
            .Include(sb => sb.Warehouse)
            .FirstOrDefault(sb => sb.ProductId == productId && sb.WarehouseId == warehouseId && sb.CompanyId == companyId);
    }

    public StockBalance Create(Guid companyId, StockBalance stockBalance)
    {
        stockBalance.CompanyId = companyId;
        _context.StockBalances.Add(stockBalance);
        _context.SaveChanges();
        return stockBalance;
    }

    public StockBalance Update(Guid companyId, StockBalance stockBalance)
    {
        var existingStockBalance = _context.StockBalances
            .FirstOrDefault(sb => sb.Id == stockBalance.Id && sb.CompanyId == companyId);

        if (existingStockBalance == null)
        {
            throw new KeyNotFoundException("Stock balance not found.");
        }

        existingStockBalance.ProductId = stockBalance.ProductId;
        existingStockBalance.WarehouseId = stockBalance.WarehouseId;
        existingStockBalance.Quantity = stockBalance.Quantity;

        _context.SaveChanges();
        return existingStockBalance;
    }

    public void Delete(Guid id, Guid companyId)
    {
        var stockBalance = _context.StockBalances
            .FirstOrDefault(sb => sb.Id == id && sb.CompanyId == companyId);

        if (stockBalance == null)
        {
            throw new KeyNotFoundException("Stock balance not found.");
        }

        _context.StockBalances.Remove(stockBalance);
        _context.SaveChanges();
    }

    public IEnumerable<StockBalance> GetByProductId(Guid productId, Guid companyId)
    {
        return _context.StockBalances
            .Include(sb => sb.Warehouse)
            .Where(sb => sb.ProductId == productId && sb.CompanyId == companyId)
            .ToList();
    }
}