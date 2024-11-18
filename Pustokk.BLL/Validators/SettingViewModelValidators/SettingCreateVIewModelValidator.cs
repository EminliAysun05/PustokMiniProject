using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Pustokk.BLL.ViewModels.SetttingViewModels;

namespace Pustok.BLL.Validators.SettingViewModelValidators;

public class SettingCreateVIewModelValidator : AbstractValidator<SettingCreateViewModel>
{
    public SettingCreateVIewModelValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key is required.")
            .MaximumLength(100).WithMessage("Key cannot exceed 100 characters.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.")
            .MaximumLength(500).WithMessage("Value cannot exceed 500 characters.");
    }
}

