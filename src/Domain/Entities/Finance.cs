using Domain.Enums;

namespace Domain.Entities;

public class Finance
{
    public Finance(Guid companyId, string type, string description, decimal amount, DateTime dueDate, string status)
    {
        CompanyId = companyId;
        Type = getEFinanceType(type);
        Description = description;
        Amount = amount;
        DueDate = dueDate;
        Status = getEFinanceStatus(status);
    }

    public Finance() { }

    public Guid Id { get; set; }
    public EFinanceType Type { get; set; } = EFinanceType.Pagar;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public EFinanceStatus Status { get; set; } = EFinanceStatus.Pendente;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }

    private EFinanceType getEFinanceType(string type) =>
        type.ToLower() == "pagar" ? EFinanceType.Pagar : EFinanceType.Receber;

    private EFinanceStatus getEFinanceStatus(string status) =>
        status.ToLower() == "pendente" ? EFinanceStatus.Pendente : EFinanceStatus.Pago;
}