using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(cartItem => cartItem.ProductId)
                .NotNull().WithMessage("Product must be valid.");

            RuleFor(cartItem => cartItem.Price)
                .NotNull().Must(value => value > 0).WithMessage("Price must be greater than zero.");

            RuleFor(cartItem => cartItem.CartId)
                .NotNull().WithMessage("Cart must be valid.");

            RuleFor(cartItem => cartItem.Quantity)
                .NotNull().Must(value => value > 0).WithMessage("Quantity must be graater than zero.")
                .Must(value => value <= 20).WithMessage("Quantity should be lower or equal than 20.");
        }
    }
}
