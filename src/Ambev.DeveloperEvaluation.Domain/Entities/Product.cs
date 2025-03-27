using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in the e-commerce system.
/// This entity contains product information, pricing, and status management.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the title/name of the product.
    /// Must be unique within the product catalog and between 2-100 characters.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current price of the product.
    /// Must be a positive value with appropriate decimal precision for the currency.
    /// </summary>
    public decimal Price { get; set; } = 0;

    /// <summary>
    /// Gets or sets the detailed description of the product.
    /// Supports rich text formatting and product specifications.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product category.
    /// Used for product classification and filtering (e.g., "Beverages", "Snacks").
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets the UTC date and time when the product was created in the system.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UTC date and time when the product was last updated.
    /// Null if the product has never been modified.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the current status of the product.
    /// Determines product visibility and purchase availability.
    /// </summary>
    public ProductStatus Status { get; set; } = ProductStatus.Active;

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// Sets default values for a new product.
    /// </summary>
    public Product()
    {
        CreatedDate = DateTime.UtcNow;
        Status = ProductStatus.Active;
    }

    /// <summary>
    /// Performs validation of the product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title meets length requirements</list>
    /// <list type="bullet">Price is greater than zero</list>
    /// <list type="bullet">Description meets length requirements</list>
    /// <list type="bullet">Category meets length requirements</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    /// <summary>
    /// Activates the product, making it available for purchase.
    /// Sets status to Active and updates the modification timestamp.
    /// </summary>
    public void Activate()
    {
        Status = ProductStatus.Active;
        UpdatedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the product, making it unavailable for purchase.
    /// Sets status to Inactive and updates the modification timestamp.
    /// </summary>
    public void Deactivate()
    {
        Status = ProductStatus.Inactive;
        UpdatedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the product's price and modification timestamp.
    /// </summary>
    /// <param name="newPrice">The new price value (must be positive)</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when newPrice is zero or negative
    /// </exception>
    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Price must be greater or equal than zero");
        }

        Price = newPrice;
        UpdatedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the product is currently available for purchase.
    /// </summary>
    /// <returns>
    /// True if product status is Active, False otherwise
    /// </returns>
    public bool IsAvailable()
    {
        return Status == ProductStatus.Active;
    }
}