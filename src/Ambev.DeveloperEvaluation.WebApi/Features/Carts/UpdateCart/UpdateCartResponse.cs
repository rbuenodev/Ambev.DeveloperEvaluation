using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartResponse
    {
        public Guid Id { get; set; }
        public ICollection<UpdateCartItemResponse> Items { get; set; } = new List<UpdateCartItemResponse>();

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

    public class UpdateCartItemResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of cartItem
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the associated product.
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Gets or sets the quantity of this product in the cart.
        /// </summary>
        public int Quantity { get; set; } = 0;

        /// <summary>
        /// Gets or sets the unit price of the product at the time it was added to the cart.
        /// </summary>
        public decimal Price { get; set; } = 0;
        /// <summary>
        /// Gets or sets the total of discount of item
        /// </summary>
        public decimal TotalDiscounts { get; set; }
        /// <summary>
        /// Gets or sets the total product
        /// </summary>
        public decimal Total { get; set; }
    }
}
