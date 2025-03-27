using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Price).NotNull().Must(value => value > 0).WithMessage("Price must be greater than zero.");
            RuleFor(product => product.Title).NotEmpty()
                .MinimumLength(5).WithMessage("Title must be at least 5 characters long.")
                .MinimumLength(50).WithMessage("Title cannot have more than 50 characters.");
            RuleFor(product => product.Category).NotEmpty()
                .MinimumLength(5).WithMessage("Category must be at least 5 characters long.")
                .MinimumLength(50).WithMessage("Category cannot have more than 50 characters."); ;
        }
    }
}
