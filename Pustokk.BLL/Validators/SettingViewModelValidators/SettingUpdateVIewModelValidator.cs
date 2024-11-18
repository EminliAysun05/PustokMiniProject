using FluentValidation;
using Pustokk.BLL.ViewModels.SetttingViewModels;

namespace Pustok.BLL.Validators.SettingViewModelValidators;

public class SettingUpdateVIewModelValidator : AbstractValidator<SettingUpdateViewModel>
{
    public SettingUpdateVIewModelValidator()
    {
        RuleFor(x => x.Key)
             .NotEmpty().WithMessage("Key is required.")
             .MaximumLength(100).WithMessage("Key cannot exceed 100 characters.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.")
            .MaximumLength(500).WithMessage("Value cannot exceed 500 characters.");
    }
}
