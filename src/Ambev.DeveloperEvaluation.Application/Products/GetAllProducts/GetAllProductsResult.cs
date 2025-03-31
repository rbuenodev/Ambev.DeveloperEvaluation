using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsResult
    {
        /// <summary>
        /// Gets or sets the unique Id of the product.
        /// </summary>
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current price of the product.
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Gets or sets the detailed description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product category.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current status of the product.
        /// </summary>
        public ProductStatus Status { get; set; } = ProductStatus.Active;
    }
}
