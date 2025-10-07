using Application.Requests;
using Application.Responses;

namespace Domain.Services;

public interface ICategoryService
{
    IEnumerable<CategoryResponse> GetAll(Guid CompanyId);
    CategoryResponse GetById(Guid CompanyId, Guid id);
    CategoryResponse Create(Guid CompanyId, CategoryRequest category);
    CategoryResponse Update(Guid CompanyId, Guid id, CategoryRequest category);
    void Delete(Guid id, Guid CompanyId);
}