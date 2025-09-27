using Domain.Enums;

namespace Domain.Entities;

public class StockMove
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public Guid? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
    public EStockMoveType Type { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}