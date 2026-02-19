namespace eCommerce.BusinessLogicLayer.DTOs;

public class ProductValidationResult
{
    public bool IsSuccess => !InvalidIDs.Any();
    public List<Guid> InvalidIDs { get; set; } = new List<Guid>();
    public List<ProductSummery> Products { get; set; } = new List<ProductSummery>();
}

public record ProductSummery(Guid ProductId, string ProductName, string Category, decimal UnitPrice);