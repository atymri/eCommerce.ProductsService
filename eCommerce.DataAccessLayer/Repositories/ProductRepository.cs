using eCommerce.DataAccessLayer.DatabaseContext;
using eCommerce.DataAccessLayer.Entitie;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.Repositories;

internal class ProductRepository : IProductsRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Product?> AddProduct(Product product)
    {
        product.ProductID = Guid.NewGuid();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId);
        
        if (product == null) return false;

        _context.Products.Remove(product);
        var rowsEffected = await _context.SaveChangesAsync();

        return rowsEffected > 0;
    }

    public async Task<bool> DoesProductExist(Guid productId)
        => await _context.Products.AnyAsync(p => p.ProductID == productId);

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition)
        => await _context.Products.FirstOrDefaultAsync(condition);

    public async Task<IEnumerable<Product>> GetProducts()
        => await _context.Products.ToListAsync();

    public async Task<IEnumerable<Product>?> GetProductsByCondition(Expression<Func<Product, bool>> condition)
        => await _context.Products.Where(condition).ToListAsync();

    public async Task<Product?> UpdateProduct(Product product)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

        existingProduct.ProductName = product.ProductName;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        existingProduct.Category = product.Category;

        await _context.SaveChangesAsync();
        return existingProduct;
    }
}

