using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.DecreaseCartItem
{
    public class DecreaseCartItemRequestValidator : AbstractValidator<DecreaseCartItemRequest>
    {
        public DecreaseCartItemRequestValidator()
        {
            RuleFor(i => i.CartId).NotEmpty().NotNull();
            RuleFor(i => i.ProductId).NotEmpty().NotNull();
            RuleFor(i => i.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
        }
    }
}
