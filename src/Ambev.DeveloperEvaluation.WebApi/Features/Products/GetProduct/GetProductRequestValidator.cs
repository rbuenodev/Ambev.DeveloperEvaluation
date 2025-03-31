﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductRequestValidator: AbstractValidator<GetProductRequest>
    {
        public GetProductRequestValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty();
        }
    }
}
