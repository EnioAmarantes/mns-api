using Application.Responses;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class StockBalanceService : IStockBalanceService
{
    private readonly IStockBalanceRepository _stockBalanceRepository;

    public StockBalanceService(IStockBalanceRepository stockBalanceRepository)
    {
        _stockBalanceRepository = stockBalanceRepository;
    }

    public IEnumerable<StockBalanceResponse> GetAll(Guid companyId)
    {
        var stockBalances = _stockBalanceRepository.GetAll(companyId);
        return stockBalances.Select(sb => new StockBalanceResponse
        {
            Id = sb.Id,
            ProductId = sb.ProductId,
            WarehouseId = sb.WarehouseId,
            WarehouseName = sb.Warehouse != null ? sb.Warehouse.Name : string.Empty,
            Quantity = sb.Quantity,
        });
    }

    public StockBalanceResponse GetById(Guid id, Guid companyId)
    {
        var stockBalance = _stockBalanceRepository.GetById(id, companyId);
        if (stockBalance == null)
        {
            throw new KeyNotFoundException("Stock balance not found.");
        }

        return new StockBalanceResponse
        {
            Id = stockBalance.Id,
            ProductId = stockBalance.ProductId,
            WarehouseId = stockBalance.WarehouseId,
            Quantity = stockBalance.Quantity,
        };
    }

    public IEnumerable<StockBalanceResponse> GetByProductId(Guid id, Guid companyId)
    {
        var stockBalances = _stockBalanceRepository.GetByProductId(id, companyId);
        return stockBalances.Select(sb => new StockBalanceResponse
        {
            Id = sb.Id,
            ProductId = sb.ProductId,
            WarehouseId = sb.WarehouseId,
            WarehouseName = sb.Warehouse != null ? sb.Warehouse.Name : string.Empty,
            Quantity = sb.Quantity,
        });
    }

    public void RecalculateBalance(StockMove stockMove, int? diferenceQuantity = null)
    {
        var stockBalance = _stockBalanceRepository.GetByProductAndWarehouse(stockMove.ProductId, stockMove.WarehouseId, stockMove.CompanyId);

        if (stockBalance == null)
        {
            stockBalance = new StockBalance
            {
                Id = Guid.NewGuid(),
                ProductId = stockMove.ProductId,
                WarehouseId = stockMove.WarehouseId,
                Quantity = stockMove.Type == Domain.Enums.EStockMoveType.IN ? stockMove.Quantity : -stockMove.Quantity,
                CompanyId = stockMove.CompanyId
            };

            _stockBalanceRepository.Create(stockMove.CompanyId, stockBalance);
        }
        else
        {
            if (diferenceQuantity.HasValue)
            {
                stockMove.Quantity = diferenceQuantity.Value;
            }
            stockBalance.UpdateBalance(stockMove);
            _stockBalanceRepository.Update(stockMove.CompanyId, stockBalance);
        }
    }
}