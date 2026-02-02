using eCommerce.BusinessLogicLayer.DTOs;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts;

public interface IProductsService
{
    Task<List<ProductRersponse>?> GetProducts();
    Task<List<ProductRersponse>?> GetProductsByCondition(Expression<Func<ProductRersponse, bool>> condition);
    Task<ProductRersponse?> GetProductByCondition(Expression<Func<ProductRersponse, bool>> condition);
    Task<ProductRersponse?> AddProduct(ProductAddRequest request);
    Task<ProductRersponse?> UpdateProduct(ProductUpdateRequest request);
    Task<bool> DeleteProduct(Guid productId);
    Task<ProductValidationResult> ValidateProducts(List<Guid> productIds);
}
