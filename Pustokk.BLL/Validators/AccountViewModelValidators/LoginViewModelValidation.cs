

using FluentValidation;
using Pustokk.BLL.ViewModels.AppUserViewModels;

namespace Pustok.BLL.Validators.AccountViewModelValidators;

public class LoginViewModelValidation : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidation()
    {
        RuleFor(x => x.EmailOrUserName)
            .NotEmpty().WithMessage("Username is required.")
        .EmailAddress().WithMessage("invalid email format");

        RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is requred");

    }
}
