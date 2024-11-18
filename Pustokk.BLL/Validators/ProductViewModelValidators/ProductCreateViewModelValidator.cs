using FluentValidation;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustok.BLL.Validators;

namespace Pustok.BLL.Validators.ProductViewModelValidators;

public class ProductCreateViewModelValidator : AbstractValidator<ProductCreateViewModel>
{
    public ProductCreateViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Cannot be empty").MaximumLength(255).WithMessage("Lenght should be less than 255");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Cannot be empty").MaximumLength(1024).WithMessage("Lenght should be less than 1024");
        RuleFor(x => x.Brand).NotEmpty().WithMessage("Cannot be empty").MaximumLength(100).WithMessage("Lenght should be less than 100");
        RuleFor(x => x.ProductCode).NotEmpty().WithMessage("Cannot be empty").MaximumLength(100).WithMessage("Lenght should be less than 100");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
        RuleFor(x => x.DisCountPrice).GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative")
            .LessThanOrEqualTo(100).WithMessage("Discount percentage cannot be creater than 100");
        RuleFor(x => x.RewardPoints).GreaterThanOrEqualTo(0).WithMessage("Cannot be negative");
        // RuleFor(x => x.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative");
        RuleForEach(x => x.ImageFiles)
            .SetValidator(new FileValidator());
        RuleFor(x => x.ProductCode)
             .NotEmpty().WithMessage("Product Code is required.")
             .MaximumLength(50).WithMessage("Product Code cannot exceed 50 characters.");
        RuleFor(x => x.Brand)
           .NotEmpty().WithMessage("Brand is required.")
           .MaximumLength(100).WithMessage("Brand cannot exceed 100 characters.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Cannot be empty").MaximumLength(255).WithMessage("Lenght should be less than 255");
    }
}
