using FluentValidation;
using Microsoft.AspNetCore.Http;
namespace Pustok.BLL.Validators;

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator()
    {
        RuleFor(x => x.Length).LessThanOrEqualTo(5 * 1024 * 1024).WithMessage("File size is larger than allowed");
        RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png")).
            WithMessage("Please choose an image File");
    }
}
    