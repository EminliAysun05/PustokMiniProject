using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pustokk.BLL.ViewModels.ServiceViewModels;

namespace Pustok.BLL.Validators.ServiceViewModelValidations;

public class ServiceCreateViewModelValidation : AbstractValidator<ServiceCreateViewModel>
{
    public ServiceCreateViewModelValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.IconFile)
            .SetValidator(new FileValidator());
    }
}
