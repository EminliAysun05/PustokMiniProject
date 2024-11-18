using FluentValidation;
using Pustokk.BLL.ViewModels.ServiceViewModels;

namespace Pustok.BLL.Validators.ServiceViewModelValidations;

public class ServiceUpdateViewModelValidation : AbstractValidator<ServiceUpdateViewModel>
{
    public ServiceUpdateViewModelValidation()
    {
        

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.NewIconFile)
            .SetValidator(new FileValidator());
    }
}
