using EduConnect.Core.DTOs;
using FluentValidation;

namespace EduConnect.Api.Validations
{
    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {
        public StudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim alanı boş olamaz.")
                .Length(2, 50).WithMessage("İsim 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim alanı boş olamaz.")
                .Length(2, 50).WithMessage("Soyisim 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi girin.");
        }
    }
}
