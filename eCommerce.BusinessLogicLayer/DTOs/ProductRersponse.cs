namespace eCommerce.BusinessLogicLayer.DTOs;

public record ProductRersponse(
    Guid productID,
    string? productName,
    string category,
    double unitPrice,
    int quantityInStock)
{
    public ProductRersponse() : this(default, default, default, default, default)
    { }
};

