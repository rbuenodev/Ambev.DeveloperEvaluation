using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequest
    {
        public Guid Id { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        /// <summary>
        /// Gets the unique sale/order reference number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets the current status of the cart.
        /// </summary>
        public CartStatus Status { get; set; }

        /// <summary>
        /// Gets the branch associated with this cart.
        /// </summary>
        public Branch Branch { get; set; }
        public Guid UserId { get; set; }
    }
}
