using EduConnect.Core.DTOs;
using FluentValidation;

namespace EduConnect.Api.Validations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email zorunlu ").EmailAddress()
                .WithMessage("Email zorunlu");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password zorunlu");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName zorunlu");
        }
    }
}
