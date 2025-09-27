using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class StockMoveRepository : IStockMoveRepository
{
    private readonly AppDbContext _context;

    public StockMoveRepository(AppDbContext context)
    {
        _context = context;
    }

    public StockMove Create(StockMove stockMove)
    {
        _context.StockMoves.Add(stockMove);
        _context.SaveChanges();
        return stockMove;
    }

    public void Delete(Guid id, Guid companyId)
    {
        var stockMove = _context.StockMoves.Find(id);
        if (stockMove != null && stockMove.CompanyId == companyId)
        {
            _context.StockMoves.Remove(stockMove);
            _context.SaveChanges();
        }
    }

    public IEnumerable<StockMove> GetAll(Guid companyId)
    {
        return _context.StockMoves
            .Where(sm => sm.CompanyId == companyId)
            .AsNoTracking()
            .ToList();
    }

    public StockMove GetById(Guid id, Guid companyId)
    {
        return _context.StockMoves
            .AsNoTracking()
            .FirstOrDefault(sm => sm.Id == id && sm.CompanyId == companyId);
    }

    public StockMove? Update(StockMove stockMove)
    {
        var existingStockMove = _context.StockMoves.Find(stockMove.Id);
        if (existingStockMove != null && existingStockMove.CompanyId == stockMove.CompanyId)
        {
            existingStockMove.ProductId = stockMove.ProductId;
            existingStockMove.WarehouseId = stockMove.WarehouseId;
            existingStockMove.Quantity = stockMove.Quantity;
            existingStockMove.Type = stockMove.Type;
            existingStockMove.Date = stockMove.Date;
            if (stockMove.SupplierId.HasValue)
            {
                existingStockMove.SupplierId = stockMove.SupplierId;
            }
            _context.SaveChanges();
            return existingStockMove;
        }
        return existingStockMove;
    }
}