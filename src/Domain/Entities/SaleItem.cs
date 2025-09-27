namespace Domain.Entities;

public class SaleItem
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public decimal Total => Quantity * Price;
}