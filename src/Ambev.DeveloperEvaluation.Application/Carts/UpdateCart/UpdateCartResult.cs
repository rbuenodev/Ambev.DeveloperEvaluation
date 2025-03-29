using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartResult
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
        public decimal TotalProducts { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal Total { get; set; }
    }
}
