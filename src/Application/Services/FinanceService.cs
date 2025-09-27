using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class FinanceService : IFinanceService
{
    private readonly IFinanceRepository _financeRepository;

    public FinanceService(IFinanceRepository financeRepository)
    {
        _financeRepository = financeRepository;
    }

    private string getString(EFinanceType type) =>
        type == EFinanceType.Pagar ? "pagar" : "receber";

    private string getString(EFinanceStatus status) =>
        status == EFinanceStatus.Pendente ? "pendente" : "pago";

    public IEnumerable<FinanceResponse> GetAll(Guid CompanyId)
    {
        var finances = _financeRepository.GetAll(CompanyId);
        return finances.Select(f => new FinanceResponse
        {
            Id = f.Id,
            Type = getString(f.Type),
            Description = f.Description,
            Amount = f.Amount,
            DueDate = f.DueDate,
            Status = getString(f.Status)
        });
    }

    public FinanceResponse GetById(Guid id, Guid CompanyId)
    {
        var finance = _financeRepository.GetById(id, CompanyId);

        return new FinanceResponse
        {
            Id = finance.Id,
            Type = getString(finance.Type),
            Description = finance.Description,
            Amount = finance.Amount,
            DueDate = finance.DueDate,
            Status = getString(finance.Status)
        };
    }

    public FinanceResponse Create(Guid CompanyId, FinanceRequest finance)
    {
        var newFinance = new Finance(
            CompanyId,
            finance.Type,
            finance.Description,
            finance.Amount,
            finance.DueDate,
            finance.Status
        );

        var createdFinance = _financeRepository.Create(CompanyId, newFinance);
        return new FinanceResponse
        {
            Id = createdFinance.Id,
            Type = getString(createdFinance.Type),
            Description = createdFinance.Description,
            Amount = createdFinance.Amount,
            DueDate = createdFinance.DueDate,
            Status = getString(createdFinance.Status)
        };
    }

    public FinanceResponse Update(Guid CompanyId, Guid id, FinanceRequest finance)
    {
        var financeUpdate = new Finance(
            CompanyId,
            finance.Type,
            finance.Description,
            finance.Amount,
            finance.DueDate,
            finance.Status
        );
        var updatedFinance = _financeRepository.Update(CompanyId, id, financeUpdate);
        return new FinanceResponse
        {
            Id = updatedFinance.Id,
            Type = getString(updatedFinance.Type),
            Description = updatedFinance.Description,
            Amount = updatedFinance.Amount,
            DueDate = updatedFinance.DueDate,
            Status = getString(updatedFinance.Status)
        };
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        _financeRepository.Delete(CompanyId, id);
    }
}