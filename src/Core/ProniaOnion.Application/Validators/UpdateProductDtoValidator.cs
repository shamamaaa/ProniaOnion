using System;
using FluentValidation;
using ProniaOnion.Application.Dtos.Products;

namespace ProniaOnion.Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("You must include name")
                .MaximumLength(100).WithMessage("You must include maximum 100 characters")
                .MinimumLength(1).WithMessage("You must include minimum q character");

            RuleFor(p => p.SKU).NotEmpty().WithMessage("You must include name")
                .MaximumLength(10).WithMessage("You must include maximum 10 characters");

            RuleFor(p => p.Price).NotEmpty().LessThanOrEqualTo(999999.99m).GreaterThanOrEqualTo(10);

            RuleFor(p => p.Description).MaximumLength(500).WithMessage("You must include maximum 500 characters"); ;

            RuleFor(p => p.CategoryId).Must(c => c > 0).WithMessage("You must include correct category id");

            RuleForEach(x => x.ColorIds).Must(c => c > 0).WithMessage("You must include correct color id");
            RuleForEach(x => x.TagIds).Must(c => c > 0).WithMessage("You must include correct tag id");
            RuleFor(p => p.ColorIds).NotEmpty().WithMessage("You must include color id");
            RuleFor(p => p.TagIds).NotEmpty().WithMessage("You must include tag id");

        }

    }
}

