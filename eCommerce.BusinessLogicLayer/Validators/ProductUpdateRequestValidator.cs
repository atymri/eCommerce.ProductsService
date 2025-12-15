using eCommerce.BusinessLogicLayer.DTOs;
using FluentValidation;

namespace eCommerce.BusinessLogicLayer.Validators;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(req => req.productId)
            .NotEmpty().WithMessage("Product id is required");

        RuleFor(req => req.productName)
               .NotEmpty().WithMessage("Product name is required")
               .Length(3, 50).WithMessage("Product name must be between 3 and 50 characters");

        RuleFor(req => req.unitPrice)
            .InclusiveBetween(0, double.MaxValue).WithMessage($"Price should be between 0 to {double.MaxValue}");

        RuleFor(req => req.quantityInStock)
            .InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock must be between 0 to {int.MaxValue}");

        RuleFor(req => req.category)
            //.NotEmpty().WithMessage("Product category is required")
            .IsInEnum().WithMessage("Invalid category");
    }
}

