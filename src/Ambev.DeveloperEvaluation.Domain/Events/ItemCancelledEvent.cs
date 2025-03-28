using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent : BaseEvent
    {
        public CartItem CartItem { get; }
        public ItemCancelledEvent(CartItem cartItem)
        {
            CartItem = cartItem;
        }
    }
}
