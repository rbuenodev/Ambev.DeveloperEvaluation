using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.RemoveCartItem
{
    public class RemoveCartItemRequestValidator : AbstractValidator<RemoveCartItemRequest>
    {
        public RemoveCartItemRequestValidator()
        {
            RuleFor(i => i.Id).NotEmpty().NotNull();
        }
    }
}
