using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ChangeStatusCart
{
    public class ChangeStatusCartHandler : IRequestHandler<ChangeStatusCartCommand, ChangeStatusCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public ChangeStatusCartHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<ChangeStatusCartResult> Handle(ChangeStatusCartCommand command, CancellationToken cancellationToken)
        {
            var validationResult = command.Validate();
            if (!validationResult.IsValid)
            {
                throw new ValidationException((IEnumerable<FluentValidation.Results.ValidationFailure>)validationResult.Errors);
            }

            var cart = await _cartRepository.GetByIdAsync(command.Id, cancellationToken);
            if (cart == null)
                throw new KeyNotFoundException($"Cart with ID {command.Id} not found");

            if (cart.Status == command.Status)
                throw new InvalidOperationException($"Cart with ID {command.Id} already has status {command.Status}");

            cart.ChangeStatus(command.Status);
            await _cartRepository.UpdateAsync(cart, cancellationToken);
            return new ChangeStatusCartResult { Success = true };
        }
    }
}
