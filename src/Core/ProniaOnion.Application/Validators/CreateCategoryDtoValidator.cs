﻿using System;
using FluentValidation;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Validators
{
    internal class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1);
        }
    }
}

