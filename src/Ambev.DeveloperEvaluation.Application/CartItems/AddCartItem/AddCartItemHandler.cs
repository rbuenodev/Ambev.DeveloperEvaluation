using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.CartItems.AddCartItem
{
    public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, AddCartItemResult>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        public AddCartItemHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }
        public async Task<AddCartItemResult> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
        {
            var validationResult = command.Validate();
            if (!validationResult.IsValid)
                throw new ValidationException((IEnumerable<FluentValidation.Results.ValidationFailure>)validationResult.Errors);

            var cartItems = await _cartItemRepository.GetFiltered(o => o.CartId == command.CartId && o.ProductId == command.ProductId, cancellationToken);
            if (cartItems != null && cartItems.Any())
            {
                var cartItemsValidator = new CartItemsValidator(command.Quantity);
                var validationItemResult = cartItemsValidator.Validate((ICollection<CartItem>)cartItems);
                if (!validationItemResult.IsValid)
                    throw new ValidationException((IEnumerable<FluentValidation.Results.ValidationFailure>)validationItemResult.Errors);

                var cartItem = cartItems.FirstOrDefault();
                cartItem!.UpdateQuantity(cartItem.Quantity + command.Quantity);
                await _cartItemRepository.UpdateAsync(cartItem, cancellationToken);
            }
            else
            {
                var cartItem = _mapper.Map<CartItem>(command);
                await _cartItemRepository.CreateAsync(cartItem, cancellationToken);
            }
            return new AddCartItemResult { Success = true };
        }
    }
}
