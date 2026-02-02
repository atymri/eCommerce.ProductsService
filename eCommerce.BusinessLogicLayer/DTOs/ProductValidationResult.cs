namespace eCommerce.BusinessLogicLayer.DTOs;

public class ProductValidationResult
{
    public bool IsSuccess => !InvalidIDs.Any();
    public List<Guid> InvalidIDs { get; set; } = new List<Guid>();
}
