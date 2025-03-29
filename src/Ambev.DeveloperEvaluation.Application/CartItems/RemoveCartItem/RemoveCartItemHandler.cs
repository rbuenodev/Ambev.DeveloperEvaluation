using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartItems.RemoveCartItem
{
    public class RemoveCartItemHandler : IRequestHandler<RemoveCartItemCommand, RemoveCartItemResult>
    {
        private readonly ICartItemRepository _cartItemRepository;
        public RemoveCartItemHandler(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
        public async Task<RemoveCartItemResult> Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
        {
            var validationResult = command.Validate();
            if (!validationResult.IsValid)
                throw new ValidationException((IEnumerable<FluentValidation.Results.ValidationFailure>)validationResult.Errors);

            var cartItem = await _cartItemRepository.GetByIdAsync(command.Id, cancellationToken);
            cartItem.MarkAsDeleted();
            await _cartItemRepository.UpdateAsync(cartItem, cancellationToken);
            return new RemoveCartItemResult { Success = true };
        }
    }
}
