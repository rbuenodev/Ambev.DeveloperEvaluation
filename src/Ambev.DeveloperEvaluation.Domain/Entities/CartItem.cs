using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item within a shopping cart, containing product information and quantity.
    /// This entity tracks individual product selections and their pricing within a cart.
    /// </summary>
    public class CartItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the associated product.
        /// This serves as a reference to the product catalog.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the full product details.
        /// Null if the product information hasn't been loaded.
        /// </summary>
        public Product? Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of this product in the cart.
        /// Must be a positive integer (minimum value of 1).
        /// </summary>
        public int Quantity { get; set; } = 0;

        /// <summary>
        /// Gets or sets the unit price of the product at the time it was added to the cart.
        /// This price is fixed when added to prevent changes during checkout.
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Gets or sets a value indicating whether this item has been removed from the cart.
        /// Soft delete flag to maintain history of removed items.
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the unique identifier of the cart this item belongs to.
        /// </summary>
        public Guid CartId { get; set; }
        /// <summary>
        /// Initializes a new instance of the CartItem class.
        /// </summary>

        public Cart Cart { get; set; }
        public CartItem()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Performs validation of the cart item using the CartItemValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        /// <remarks>
        /// <listheader>The validation includes checking:</listheader>
        /// <list type="bullet">ProductId is not empty</list>
        /// <list type="bullet">Quantity is greater than zero</list>
        /// <list type="bullet">Price is non-negative</list>
        /// <list type="bullet">CartId is not empty</list>
        /// </remarks>
        public ValidationResultDetail Validate()
        {
            var validator = new CartItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Marks the cart item as deleted without removing it from the database.
        /// Sets IsDeleted flag to true while preserving historical data.
        /// </summary>
        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

        /// <summary>
        /// Updates the quantity of this cart item.
        /// </summary>
        /// <param name="newQuantity">The new quantity (must be positive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when newQuantity is less than 1
        /// </exception>
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 1)
            {
                Quantity = 0;
                MarkAsDeleted();
                return;
            }

            if (newQuantity > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(newQuantity), "Quantity cannot be greater than 20");
            }

            Quantity = newQuantity;
        }

        /// <summary>
        /// Calculates the total price for this cart item (price × quantity).
        /// </summary>
        /// <returns>The calculated line total</returns>
        public decimal CalculateTotal()
        {
            if (IsDeleted) return 0;
            return Price * Quantity;
        }

        public decimal CalculateDiscounts()
        {
            if (IsDeleted) return 0;

            if (Quantity >= 4)
            {
                return Price * Quantity * 0.1m;
            }
            else if (Quantity >= 10)
            {
                return Price * Quantity * 0.2m;
            }

            return 0;
        }

        public bool IsDiscountable()
        {
            if (!IsDeleted)
            {
                return Quantity > 4;
            }
            return false;
        }
    }
}
