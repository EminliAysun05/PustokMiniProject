using FluentValidation;
using Pustokk.BLL.ViewModels.CategoryViewModels;

namespace Pustok.BLL.Validators.CategoryViewModelValidators;

public class CategoryUpdateViewModelValidator : AbstractValidator<CategoryUpdateViewModel>
{
    public CategoryUpdateViewModelValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(50);

    }
}
