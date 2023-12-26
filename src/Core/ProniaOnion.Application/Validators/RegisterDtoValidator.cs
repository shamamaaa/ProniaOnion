using System;
using FluentValidation;
using ProniaOnion.Application.Dtos.Users;

namespace ProniaOnion.Application.Validators
{
	public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
		public RegisterDtoValidator()
		{
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .Matches(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")
                .WithMessage("Please input correct email address");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8).WithMessage("Please input at least 8 characters")
                .MaximumLength(150).WithMessage("Please input less than 150 characters");

            RuleFor(x => x).Must(x=>x.ConfirmPassword==x.Password).WithMessage("ConfirmPassword is not same with password");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Please input at least 3 characters")
                .MaximumLength(50).WithMessage("Please input less than 50 characters");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Please input at least 2 characters")
                .MaximumLength(50).WithMessage("Please input less than 50 characters");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MinimumLength(2).WithMessage("Please input at least 2 characters")
                .MaximumLength(50).WithMessage("Please input less than 50 characters");
        }
    }
}

