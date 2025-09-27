using Application.Requests;
using Application.Responses;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IStockBalanceService _stockBalanceService;

    public ProductService(IProductRepository productRepository, IStockBalanceService stockBalanceService)
    {
        _productRepository = productRepository;
        _stockBalanceService = stockBalanceService;
    }

    public ProductResponse Create(Guid CompanyId, ProductRequest product)
    {
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Price = product.Price,
            CompanyId = CompanyId,
            MinStockQuantity = product.MinStockQuantity
        };

        var createdProduct = _productRepository.Create(CompanyId, newProduct);
        var stockBalance = _stockBalanceService.GetByProductId(createdProduct.Id, CompanyId);

        return new ProductResponse
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price,
            MinStockQuantity = createdProduct.MinStockQuantity,
            StockBalances = stockBalance.ToList()
        };
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        _productRepository.Delete(id, CompanyId);
    }

    public IEnumerable<ProductResponse> GetAll(Guid CompanyId)
    {
        var stockBalances = _stockBalanceService.GetAll(CompanyId);
        return _productRepository.GetAll(CompanyId).Select(p => new ProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            MinStockQuantity = p.MinStockQuantity,
            StockBalances = stockBalances.Where(sb => sb.ProductId == p.Id).ToList()
        });
    }

    public ProductResponse GetById(Guid id, Guid CompanyId)
    {
        var product = _productRepository.GetById(id, CompanyId);
        var stockBalance = _stockBalanceService.GetByProductId(id, CompanyId);

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            MinStockQuantity = product.MinStockQuantity,
            StockBalances = stockBalance.ToList()
        };
    }

    public ProductResponse Update(Guid CompanyId, Guid id, ProductRequest product)
    {
        var productUpdate = new Product
        {
            Id = id,
            CompanyId = CompanyId,
            Name = product.Name,
            Price = product.Price,
            MinStockQuantity = product.MinStockQuantity
        };
        var updatedProduct = _productRepository.Update(CompanyId, id, productUpdate);
        var stockBalance = _stockBalanceService.GetByProductId(id, CompanyId);

        if (updatedProduct == null)
        {
            throw new Exception("Product not found or could not be updated.");
        }

        return new ProductResponse
        {
            Id = updatedProduct.Id,
            Name = updatedProduct.Name,
            Price = updatedProduct.Price,
            MinStockQuantity = updatedProduct.MinStockQuantity,
            StockBalances = stockBalance.ToList()
        };
    }
}
