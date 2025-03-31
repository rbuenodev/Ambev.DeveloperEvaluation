using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartItems.DecreaseCartItem
{
    public class DecreaseCartItemHandler : IRequestHandler<DecreaseCartItemCommand, DecreaseCartItemResult>
    {
        private readonly ICartItemRepository _cartItemRepository;
        public DecreaseCartItemHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<DecreaseCartItemResult> Handle(DecreaseCartItemCommand command, CancellationToken cancellationToken)
        {
            var validationResult = command.Validate();
            if (!validationResult.IsValid)
                throw new ValidationException((IEnumerable<FluentValidation.Results.ValidationFailure>)validationResult.Errors);

            var cartItems = await _cartItemRepository.GetFiltered(o => o.CartId == command.CartId && o.ProductId == command.ProductId, cancellationToken);
            if (cartItems != null && cartItems.Any())
            {
                var cartItem = cartItems.FirstOrDefault();
                cartItem!.UpdateQuantity(cartItem.Quantity - command.Quantity);
                await _cartItemRepository.UpdateAsync(cartItem, cancellationToken);

                return new DecreaseCartItemResult { Success = true };
            }

            return new DecreaseCartItemResult { Success = false };
        }
    }
}
