using System;
using FluentValidation;
using ProniaOnion.Application.Dtos.Products;

namespace ProniaOnion.Application.Validators
{
    internal class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("You must include name")
                .MaximumLength(100).WithMessage("You must include maximum 100 characters")
                .MinimumLength(1).WithMessage("You must include minimum q character");

            RuleFor(p => p.SKU).NotEmpty().WithMessage("You must include name")
                .MaximumLength(10).WithMessage("You must include maximum 10 characters");

            RuleFor(p => p.Price).NotEmpty().LessThanOrEqualTo(999999.99m).GreaterThanOrEqualTo(10);

            RuleFor(p => p.Description).MaximumLength(500).WithMessage("You must include maximum 500 characters");;

        }



    }
}

