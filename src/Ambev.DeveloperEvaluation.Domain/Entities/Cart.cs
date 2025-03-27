using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a shopping cart in the e-commerce system.
    /// This entity manages product items, cart status, and order processing information.
    /// </summary>
    public class Cart : BaseEntity
    {
        /// <summary>
        /// Gets the collection of products in the cart.
        /// Each item contains product, price and quantity.
        /// </summary>
        public IEnumerable<CartItem> Products { get; set; } = Enumerable.Empty<CartItem>();

        /// <summary>
        /// Gets the unique sale/order reference number.
        /// This is generated when the cart becomes an order.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets the current status of the cart.
        /// Indicates where the cart is in the order lifecycle.
        /// </summary>
        public CartStatus Status { get; set; } = CartStatus.Created;

        /// <summary>
        /// Gets the user associated with this cart.
        /// Null for guest checkouts or unauthenticated sessions.
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// Gets the userId associated with this cart.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Initializes a new instance of the Cart class.
        /// Sets default values for a new cart.
        /// </summary>
        public Cart()
        {
            CreatedAt = DateTime.UtcNow;
            Status = CartStatus.Created;
        }

        /// <summary>
        /// Performs validation of the cart entity using the CartValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        /// <remarks>
        /// <listheader>The validation includes checking:</listheader>
        /// <list type="bullet">Sale number valid</list>
        /// <list type="bullet">User association required</list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CartValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancel the cart.
        /// Changes the cart's status to Cancelled.
        /// Typically used for inactive carts after a timeout period.
        /// </summary>
        public void Cancel()
        {
            Status = CartStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Calculates the total monetary value of the cart.
        /// Sums all items' prices multiplied by their quantities.
        /// </summary>
        /// <returns>The total cart value</returns>
        public decimal CalculateTotalProducts()
        {
            return Products.Sum(item => item.CalculateTotal());
        }

        /// <summary>
        /// Calculates the total monetary value of the cart discounts.
        /// Sums all items' discounts.
        /// </summary>
        /// <returns>The total cart value</returns>
        public decimal CalculateTotalDiscounts()
        {
            return Products.Sum(product => product.IsDiscountable() ? product.CalculateDiscounts() : 0);
        }
    }
}
