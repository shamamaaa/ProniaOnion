using System;
using FluentValidation;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Validators
{
    internal class CreateTagDtoValidator : AbstractValidator<CreateTagDto>
    {
        public CreateTagDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1);
        }
    }
}

