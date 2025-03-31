using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events.Carts
{
    public class CartModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
    {
        private readonly ILogger<CartModifiedEventHandler> _logger;

        public CartModifiedEventHandler(ILogger<CartModifiedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(0, "*********************************************************");
            _logger.LogInformation($"Cart {notification.Cart.Id} was modified at {DateTime.Now}");
            _logger.LogInformation(0, "*********************************************************");
            return Task.CompletedTask;
        }
    }
}
