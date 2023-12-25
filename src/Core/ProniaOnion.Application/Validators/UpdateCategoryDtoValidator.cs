﻿using System;
using FluentValidation;
using ProniaOnion.Application.Dtos;

namespace ProniaOnion.Application.Validators
{
	public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
		public UpdateCategoryDtoValidator()
		{
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(1);
        }
    }
}

