using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.AddCartItem
{
    public class AddCartItemRequestValidator : AbstractValidator<AddCartItemRequest>
    {
        public AddCartItemRequestValidator()
        {
            RuleFor(i => i.CartId).NotEmpty().NotNull();
            RuleFor(i => i.ProductId).NotEmpty().NotNull();
            RuleFor(i => i.Quantity).LessThanOrEqualTo(20);
            RuleFor(i => i.Price).GreaterThan(0);
        }
    }
}
