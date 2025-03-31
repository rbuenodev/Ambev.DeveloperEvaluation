using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events.Carts
{
    public class CartCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<CartModifiedEventHandler> _logger;
        public CartCreatedEventHandler(ILogger<CartModifiedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(0, "*********************************************************");
            _logger.LogInformation($"Cart {notification.Cart.Id} was created at {DateTime.Now}");
            _logger.LogInformation(0, "*********************************************************");
            return Task.CompletedTask;
        }
    }
}
