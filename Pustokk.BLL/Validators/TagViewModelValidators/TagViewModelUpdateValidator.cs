using FluentValidation;
using Pustokk.BLL.ViewModels.TagViewModels;

namespace Pustok.BLL.Validators.TagViewModelValidators;

public class TagViewModelUpdateValidator : AbstractValidator<TagUpdateViewModel>
{
    public TagViewModelUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tag name is required.") // Boş olmamalıdır
            .MaximumLength(50).WithMessage("Tag name cannot exceed 50 characters."); // Maksimum uzunluq
    }
}