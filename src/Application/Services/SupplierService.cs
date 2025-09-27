
using Application.Requests;
using Application.Responses;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public SupplierResponse Create(Guid CompanyId, SupplierRequest supplier)
    {
        var newSupplier = new Supplier(
            CompanyId,
            supplier.Name,
            supplier.CNPJ,
            supplier.Email,
            supplier.Phone,
            supplier.Address
        );

        Console.WriteLine($"Creating supplier for CompanyId: {CompanyId}, Supplier: {newSupplier.CNPJ}");

        var createdSupplier = _supplierRepository.Create(CompanyId, newSupplier);

        return new SupplierResponse
        {
            Id = createdSupplier.Id,
            Name = createdSupplier.Name,
            CNPJ = createdSupplier.CNPJ,
            Email = createdSupplier.Email,
            Phone = createdSupplier.Phone,
            Address = createdSupplier.Address
        };
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        _supplierRepository.Delete(id, CompanyId);
    }

    public IEnumerable<SupplierResponse> GetAll(Guid CompanyId)
    {
        return _supplierRepository.GetAll(CompanyId).Select(s => new SupplierResponse
        {
            Id = s.Id,
            Name = s.Name,
            CNPJ = s.CNPJ,
            Email = s.Email,
            Phone = s.Phone,
            Address = s.Address
        });
    }

    public SupplierResponse GetById(Guid CompanyId, Guid id)
    {
        var supplier = _supplierRepository.GetById(id, CompanyId);

        return new SupplierResponse
        {
            Id = supplier.Id,
            Name = supplier.Name,
            CNPJ = supplier.CNPJ,
            Email = supplier.Email,
            Phone = supplier.Phone,
            Address = supplier.Address
        };
    }

    public SupplierResponse Update(Guid CompanyId, Guid id, SupplierRequest supplier)
    {
        var supplierUpdate = new Supplier(
            CompanyId,
            supplier.Name,
            supplier.CNPJ,
            supplier.Email,
            supplier.Phone,
            supplier.Address
        );

        var updatedSupplier = _supplierRepository.Update(CompanyId, id, supplierUpdate);

        return new SupplierResponse
        {
            Id = updatedSupplier.Id,
            Name = updatedSupplier.Name,
            CNPJ = updatedSupplier.CNPJ,
            Email = updatedSupplier.Email,
            Phone = updatedSupplier.Phone,
            Address = updatedSupplier.Address
        };
    }
}