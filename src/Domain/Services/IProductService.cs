using Application.Requests;
using Application.Responses;

namespace Domain.Services;

public interface IProductService
{
    IEnumerable<ProductResponse> GetAll(Guid CompanyId);
    ProductResponse GetById(Guid id, Guid CompanyId);
    ProductResponse Create(Guid CompanyId, ProductRequest product);
    ProductResponse Update(Guid CompanyId, Guid id, ProductRequest product);
    void Delete(Guid id, Guid CompanyId);
}