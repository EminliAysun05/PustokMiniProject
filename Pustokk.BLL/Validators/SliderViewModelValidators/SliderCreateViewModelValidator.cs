using FluentValidation;
using Pustokk.BLL.ViewModels.SliderViewModels;

namespace Pustok.BLL.Validators.SliderViewModelValidators;

public class SliderCreateViewModelValidator : AbstractValidator<SliderCreateViewModel>
{
    public SliderCreateViewModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.ImageFile)
              .NotNull().WithMessage("ImageFile is required.")
              .SetValidator(new FileValidator());


        RuleFor(x => x.ButtonText)
            .NotEmpty().WithMessage("ButtonText is required.")
            .MaximumLength(50).WithMessage("ButtonText cannot exceed 50 characters.");
    }
}
