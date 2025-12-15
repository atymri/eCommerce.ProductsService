using eCommerce.DataAccessLayer.Entitie;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.RepositoryContracts;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>?> GetProductsByCondition(Expression<Func<Product, bool>> condition);
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition);
    Task<Product?> AddProduct(Product product);
    Task<Product?> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid productId);
    Task<bool> DoesProductExist(Guid productId);
}

