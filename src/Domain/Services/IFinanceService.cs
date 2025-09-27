namespace Domain.Services;

public interface IFinanceService
{
    IEnumerable<FinanceResponse> GetAll(Guid CompanyId);
    FinanceResponse GetById(Guid id, Guid CompanyId);
    FinanceResponse Create(Guid CompanyId, FinanceRequest finance);
    FinanceResponse Update(Guid CompanyId, Guid id, FinanceRequest finance);
    void Delete(Guid id, Guid CompanyId);
}