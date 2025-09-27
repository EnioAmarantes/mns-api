using Domain.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Product Create(Guid CompanyId, Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public void Delete(Guid id, Guid CompanyId)
    {
        var product = _context.Products.Find(id);
        if (product != null && product.CompanyId == CompanyId)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Product> GetAll(Guid CompanyId)
    {
        return _context.Products
            .Where(p => p.CompanyId == CompanyId)
            .AsNoTracking()
            .ToList();
    }

    public Product GetById(Guid id, Guid CompanyId)
    {
        return _context.Products
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id && p.CompanyId == CompanyId);
    }

    public Product? Update(Guid CompanyId, Guid id, Product product)
    {
        var existingProduct = GetById(id, CompanyId);
        if (existingProduct != null && existingProduct.CompanyId == CompanyId)
        {
            var updatedProduct = new Product
            {
                Id = existingProduct.Id,
                CompanyId = existingProduct.CompanyId,
                Name = product.Name,
                Price = product.Price,
                MinStockQuantity = product.MinStockQuantity
            };
            _context.Entry(existingProduct).State = EntityState.Detached;
            _context.Products.Update(updatedProduct);
            _context.SaveChanges();
            return updatedProduct;
        }
        return existingProduct;
    }
}