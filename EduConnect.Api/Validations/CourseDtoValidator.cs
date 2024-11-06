using EduConnect.Core.DTOs;
using FluentValidation;

namespace EduConnect.Api.Validations
{
    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("Kurs adı alanı boş olamaz.")
                .Length(3, 100).WithMessage("Kurs adı 3 ile 100 karakter arasında olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş olamaz.");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Süre 0'dan büyük olmalıdır.");

            RuleFor(x => x.Instructor)
                .NotEmpty().WithMessage("Eğitmen adı alanı boş olamaz.")
                .Length(2, 50).WithMessage("Eğitmen adı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kapasite 0'dan büyük olmalıdır.");
        }
    }
}
