using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events.CartItems
{
    public class CartItemCancelledEventHandler : INotificationHandler<ItemCancelledEvent>
    {
        private readonly ILogger<CartItemCancelledEventHandler> _logger;
        public CartItemCancelledEventHandler(ILogger<CartItemCancelledEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(0, "*********************************************************");
            _logger.LogInformation($"Item {notification.CartItem.Id} was cancelled at {DateTime.Now}");
            _logger.LogInformation(0, "*********************************************************");
            return Task.CompletedTask;
        }
    }
}
