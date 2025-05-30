﻿using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    /// <summary>
    /// Represents a request to create a new product in the system.
    /// </summary>
    public class CreateProductRequest
    {
        /// <summary>
        /// Gets or sets the title/name of the product.
        /// </summary>
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
