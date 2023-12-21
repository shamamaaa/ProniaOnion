using System;
using FluentValidation;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Validators
{
    internal class UpdateTagDtoValidator : AbstractValidator<UpdateTagDto>
    {
        public UpdateTagDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1);
        }
    }
}

