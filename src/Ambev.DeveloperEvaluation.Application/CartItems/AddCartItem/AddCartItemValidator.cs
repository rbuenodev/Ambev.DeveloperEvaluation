using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartItems.AddCartItem
{
    public class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
    {
        public AddCartItemValidator()
        {
            RuleFor(i => i.CartId).NotEmpty().NotNull();
            RuleFor(i => i.ProductId).NotEmpty().NotNull();
            RuleFor(i => i.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(i => i.Price).GreaterThan(0);
        }
    }
}
