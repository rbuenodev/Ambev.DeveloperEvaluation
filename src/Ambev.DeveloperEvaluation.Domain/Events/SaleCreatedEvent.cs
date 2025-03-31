using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : BaseEvent
    {
        public Cart Cart { get; }
        public SaleCreatedEvent(Cart cart)
        {
            Cart = cart;
        }
    }
}
