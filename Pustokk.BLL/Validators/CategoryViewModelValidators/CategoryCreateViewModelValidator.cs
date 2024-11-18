using FluentValidation;
using Pustokk.BLL.ViewModels.CategoryViewModels;


namespace Pustok.BLL.Validators.CategoryViewModelValidators;

public class CategoryCreateViewModelValidator : AbstractValidator<CategoryCreateViewModel>
{
    public CategoryCreateViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(50).WithMessage("Category name cannot exceed 100 characters.");
       RuleFor(x => x.ParentCategoryId)
            .GreaterThanOrEqualTo(0).When(x => x.ParentCategoryId.HasValue)
            .WithMessage("ParentCategoryId must be greater than or equal to 0.");

        //RuleForEach(x => x.SubCategories)
        //    .SetValidator(new CategoryViewModelValidator());
    }
}
