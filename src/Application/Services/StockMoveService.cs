using Application.Requests;
using Application.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class StockMoveService : IStockMoveService
{
    private readonly IStockBalanceService _stockBalanceService;
    private readonly IStockMoveRepository _stockMoveRepository;

    public StockMoveService(IStockBalanceService stockBalanceService, IStockMoveRepository stockMoveRepository)
    {
        _stockBalanceService = stockBalanceService;
        _stockMoveRepository = stockMoveRepository;
    }

    public StockMoveResponse Create(Guid companyId, StockMoveRequest request)
    {
        var stockMove = new StockMove
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            ProductId = request.ProductId,
            WarehouseId = request.WarehouseId,
            SupplierId = request.SupplierId,
            Date = request.Date,
            Quantity = request.Quantity,
            Type = getStockMoveType(request.Type)
        };

        _stockMoveRepository.Create(stockMove);
        _stockBalanceService.RecalculateBalance(stockMove);

        return new StockMoveResponse
        {
            Id = stockMove.Id,
            ProductId = stockMove.ProductId,
            WarehouseId = stockMove.WarehouseId,
            Quantity = stockMove.Quantity,
            Date = stockMove.Date,
            SupplierId = stockMove.SupplierId,
            Type = getString(stockMove.Type)
        };
    }

    private string getString(EStockMoveType type)
    {
        return type switch
        {
            EStockMoveType.IN => "entrada",
            EStockMoveType.OUT => "saida",
            _ => throw new ArgumentException("Invalid stock move type.")
        };
    }

    private EStockMoveType getStockMoveType(string type)
    {
        return type switch
        {
            "entrada" => EStockMoveType.IN,
            "saida" => EStockMoveType.OUT,
            _ => throw new ArgumentException("Invalid stock move type.")
        };
    }

    public void Delete(Guid companyId, Guid id)
    {
        _stockMoveRepository.Delete(id, companyId);
    }

    public IEnumerable<StockMoveResponse> GetAll(Guid companyId)
    {
        var stockMoves = _stockMoveRepository.GetAll(companyId);
        return stockMoves.Select(sm => new StockMoveResponse
        {
            Id = sm.Id,
            ProductId = sm.ProductId,
            WarehouseId = sm.WarehouseId,
            Quantity = sm.Quantity,
            Date = sm.Date,
            SupplierId = sm.SupplierId,
            Type = getString(sm.Type)
        });
    }

    public StockMoveResponse GetById(Guid id, Guid companyId)
    {
        var stockMove = _stockMoveRepository.GetById(id, companyId);
        if (stockMove == null)
        {
            throw new KeyNotFoundException("Stock move not found.");
        }

        return new StockMoveResponse
        {
            Id = stockMove.Id,
            ProductId = stockMove.ProductId,
            WarehouseId = stockMove.WarehouseId,
            Quantity = stockMove.Quantity,
            Date = stockMove.Date,
            SupplierId = stockMove.SupplierId,
            Type = getString(stockMove.Type)
        };
    }

    public StockMoveResponse Update(Guid companyId, Guid id, StockMoveRequest request)
    {
        var stockMove = _stockMoveRepository.GetById(id, companyId);
        if (stockMove == null)
        {
            throw new KeyNotFoundException("Stock move not found.");
        }

        var diferenceQuantity = request.Quantity - stockMove.Quantity;

        stockMove.ProductId = request.ProductId;
        stockMove.WarehouseId = request.WarehouseId;
        stockMove.Quantity = request.Quantity;
        stockMove.Type = getStockMoveType(request.Type);
        stockMove.Date = request.Date;
        if (request.SupplierId.HasValue)
        {
            stockMove.SupplierId = request.SupplierId.Value;
        }

        _stockMoveRepository.Update(stockMove);
        _stockBalanceService.RecalculateBalance(stockMove, diferenceQuantity);

        return new StockMoveResponse
        {
            Id = stockMove.Id,
            ProductId = stockMove.ProductId,
            WarehouseId = stockMove.WarehouseId,
            Quantity = stockMove.Quantity,
            Type = getString(stockMove.Type)
        };
    }
}