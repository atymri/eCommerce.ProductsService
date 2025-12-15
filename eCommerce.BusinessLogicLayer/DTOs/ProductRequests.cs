namespace eCommerce.BusinessLogicLayer.DTOs;

public enum CategoryOptions
{
    Electronics,
    HomeAppliances,
    Furniture,
    Accessories
}

public record ProductAddRequest(
    string? productName, 
    CategoryOptions category,
    double unitPrice,
    int quantityInStock)
{
    public ProductAddRequest() : this(default, default, default, default)
    { }
}; 

public record ProductUpdateRequest(
    Guid productId,
    string? productName,
    CategoryOptions category,
    double unitPrice,
    int quantityInStock)
{
    public ProductUpdateRequest() : this(default, default, default, default, default)
    { }
};


