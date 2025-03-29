using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events.Carts
{
    public class CartCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
    {
        private readonly ILogger<CartCancelledEventHandler> _logger;
        public CartCancelledEventHandler(ILogger<CartCancelledEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(0, "*********************************************************");
            _logger.LogInformation($"Cart {notification.Cart.Id} was cancelled at {DateTime.Now}");
            _logger.LogInformation(0, "*********************************************************");
            return Task.CompletedTask;
        }
    }
}
