using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class CartValidator : AbstractValidator<Cart>
    {
        public CartValidator()
        {
            RuleFor(cart => cart.SaleNumber)
                .NotNull()
                .NotEmpty().WithMessage("SaleNumber must be valid.");

            RuleFor(cart => cart.UserId)
                .NotNull().WithMessage("User is required.");

            RuleFor(cart => cart.Branch)
               .NotNull().WithMessage("Branch is required.");
        }
    }
}
