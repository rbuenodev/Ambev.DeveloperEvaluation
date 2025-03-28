using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent : BaseEvent
    {
        public Cart Cart { get; }
        public SaleModifiedEvent(Cart cart)
        {
            Cart = cart;
        }
    }
}
