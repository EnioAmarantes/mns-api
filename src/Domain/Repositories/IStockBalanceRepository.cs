using Domain.Entities;

namespace Domain.Repositories;

public interface IStockBalanceRepository
{
    IEnumerable<StockBalance> GetAll(Guid companyId);
    StockBalance GetById(Guid id, Guid companyId);
    StockBalance GetByProductAndWarehouse(Guid productId, Guid warehouseId, Guid companyId);
    StockBalance Create(Guid companyId, StockBalance stockBalance);
    StockBalance Update(Guid companyId, StockBalance stockBalance);
    IEnumerable<StockBalance> GetByProductId(Guid productId, Guid companyId);
    void Delete(Guid id, Guid companyId);
}