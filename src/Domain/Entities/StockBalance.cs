using Domain.Enums;

namespace Domain.Entities;

public class StockBalance
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

    public void UpdateBalance(StockMove stockMove)
    {
        Quantity += stockMove.Type == EStockMoveType.IN ? stockMove.Quantity : -stockMove.Quantity;
    }
}