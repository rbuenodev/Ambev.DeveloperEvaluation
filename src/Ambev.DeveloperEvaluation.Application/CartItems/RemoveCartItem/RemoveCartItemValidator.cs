using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartItems.RemoveCartItem
{
    public class RemoveCartItemValidator : AbstractValidator<RemoveCartItemCommand>
    {
        public RemoveCartItemValidator()
        {
            RuleFor(i => i.Id).NotEmpty().NotNull();
        }
    }
}
