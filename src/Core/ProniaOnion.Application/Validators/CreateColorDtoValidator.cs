using System;
using FluentValidation;
using ProniaOnion.Application.Dtos.Colors;

namespace ProniaOnion.Application.Validators
{
    public class CreateColorDtoValidator : AbstractValidator<CreateColorDto>
    {
        public CreateColorDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1);
        }
    }
}

