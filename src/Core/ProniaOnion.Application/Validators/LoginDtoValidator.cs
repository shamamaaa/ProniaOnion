using System;
using FluentValidation;
using ProniaOnion.Application.Dtos.Users;

namespace ProniaOnion.Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Email address or username are required.")
                .MaximumLength(256).WithMessage("You must include less than 256 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8).WithMessage("Please input at least 8 characters")
                .MaximumLength(150).WithMessage("Please input less than 150 characters");

        }
    }

}

