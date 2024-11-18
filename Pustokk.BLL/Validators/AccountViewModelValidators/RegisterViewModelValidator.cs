using FluentValidation;
using Pustokk.BLL.ViewModels.AppUserViewModels;


namespace Pustok.BLL.Validators.AccountVIewModelValidators;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{

    public RegisterViewModelValidator()
    {
        RuleFor(x => x.Username)
       .NotEmpty().WithMessage("Username is required.")
      .MaximumLength(100).WithMessage("Max length 100");

        RuleFor(x => x.Email)
       .NotEmpty().WithMessage("Username is required.")
        .EmailAddress().WithMessage("invalid email format");

        RuleFor(x => x.Password)
       .NotEmpty().WithMessage("Password is requred").
       MinimumLength(4).WithMessage("Length should be >= 4");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

    }
}
