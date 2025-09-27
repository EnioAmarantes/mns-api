using Domain.Enums;

namespace Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public DateTime Date { get; set; }
    public IList<SaleItem> Items { get; set; } = new List<SaleItem>();
    public ESaleStatus Status { get; set; }

    public decimal Total => Items.Sum(item => item.Total);
}