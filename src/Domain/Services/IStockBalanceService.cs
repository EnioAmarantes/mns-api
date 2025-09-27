using Application.Responses;
using Domain.Entities;

namespace Domain.Services;

public interface IStockBalanceService
{
    IEnumerable<StockBalanceResponse> GetAll(Guid companyId);
    StockBalanceResponse GetById(Guid id, Guid companyId);
    IEnumerable<StockBalanceResponse> GetByProductId(Guid id, Guid companyId);
    void RecalculateBalance(StockMove stockMove, int? diferenceQuantity = null);
}