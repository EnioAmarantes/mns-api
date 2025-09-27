using Application.Requests;
using Application.Responses;
using Domain.Enums;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IStockMoveService _stockMoveService;
    private readonly IFinanceService _financeService;
    public SaleService(ISaleRepository saleRepository, IStockMoveService stockMoveService, IFinanceService financeService)
    {
        _saleRepository = saleRepository;
        _stockMoveService = stockMoveService;
        _financeService = financeService;
    }

    private string getString(ESaleStatus status)
    {
        return status == ESaleStatus.Paid ? "pago" : "pendente";
    }
    public SaleResponse Create(Guid CompanyId, SaleRequest sale)
    {
        var newSale = new Domain.Entities.Sale
        {
            CompanyId = CompanyId,
            Date = sale.Date,
            Items = sale.Items.Select(i => new Domain.Entities.SaleItem
            {
                CompanyId = CompanyId,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList(),
            Status = sale.Status.ToLower() == "pago" ? ESaleStatus.Paid : ESaleStatus.Pending
        };

        _saleRepository.Create(CompanyId, newSale);

        _financeService.Create(CompanyId, new FinanceRequest
        {
            Description = $"Venda {newSale.Id}",
            Amount = newSale.Total,
            Type = "Receber",
            Status = newSale.Status == ESaleStatus.Paid ? "pago" : "pendente"
        });

        newSale.Items.ToList().ForEach(item =>
        {
            _stockMoveService.Create(CompanyId, new StockMoveRequest
            {
                ProductId = item.ProductId,
                WarehouseId = new Guid("9af8d719-14b9-4e78-9195-5a9338b6ab85"),
                Quantity = item.Quantity,
                Date = DateTime.Now,
                Type = "saida",
            });
        });

        return new SaleResponse
        {
            Id = newSale.Id,
            Date = newSale.Date,
            Items = newSale.Items.Select(i => new SaleItemResponse
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList(),
            Status = getString(newSale.Status),
            Total = newSale.Total
        };
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        _saleRepository.Delete(id, CompanyId);
    }

    public IEnumerable<SaleResponse> GetAll(Guid CompanyId)
    {
        return _saleRepository.GetAll(CompanyId).Select(s => new SaleResponse
        {
            Id = s.Id,
            Date = s.Date,
            Items = s.Items.Select(i => new SaleItemResponse
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList(),
            Status = getString(s.Status),
            Total = s.Total
        });
    }

    public SaleResponse GetById(Guid id, Guid CompanyId)
    {
        var sale = _saleRepository.GetById(id, CompanyId); 

        return new SaleResponse
        {
            Id = sale.Id,
            Date = sale.Date,
            Items = sale.Items.Select(i => new SaleItemResponse
            {
                Id = i.Id,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList(),
            Status = getString(sale.Status),
            Total = sale.Total
        };
    }

    public SaleResponse Update(Guid CompanyId, Guid id, SaleRequest sale)
    {
        throw new NotImplementedException();
    }
}