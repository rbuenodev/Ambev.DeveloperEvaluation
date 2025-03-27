using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(cart => cart.SaleNumber)
                .NotEmpty().WithMessage("SaleNumber is required.");

            RuleFor(cart => cart.UserId)
                .NotNull().WithMessage("User is required.");
        }
    }
}
