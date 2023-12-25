using System;
using FluentValidation;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Colors;

namespace ProniaOnion.Application.Validators
{
    public class UpdateColorDtoValidator : AbstractValidator<UpdateColorDto>
    {
        public UpdateColorDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1);
        }
    }
}

