using Application.Requests;
using Application.Responses;

namespace Domain.Services;

public interface IStockMoveService
{
    IEnumerable<StockMoveResponse> GetAll(Guid companyId);
    StockMoveResponse GetById(Guid id, Guid companyId);
    StockMoveResponse Create(Guid companyId, StockMoveRequest request);
    StockMoveResponse Update(Guid companyId, Guid id, StockMoveRequest request);
    void Delete(Guid companyId, Guid id);
}