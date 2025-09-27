using Domain.Entities;

namespace Domain.Repositories;

public interface IStockMoveRepository
{
    IEnumerable<StockMove> GetAll(Guid companyId);
    StockMove GetById(Guid id, Guid companyId);
    StockMove Create(StockMove stockMove);
    StockMove? Update(StockMove stockMove);
    void Delete(Guid id, Guid companyId);
}