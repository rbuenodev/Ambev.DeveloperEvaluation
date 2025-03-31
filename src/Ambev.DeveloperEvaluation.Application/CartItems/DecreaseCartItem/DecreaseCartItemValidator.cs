using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartItems.DecreaseCartItem
{
    public class DecreaseCartItemValidator : AbstractValidator<DecreaseCartItemCommand>
    {
        public DecreaseCartItemValidator()
        {
            RuleFor(i => i.CartId).NotEmpty().NotNull();
            RuleFor(i => i.ProductId).NotEmpty().NotNull();
            RuleFor(i => i.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
        }
    }
}
