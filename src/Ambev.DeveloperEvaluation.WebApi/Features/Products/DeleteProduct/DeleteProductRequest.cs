using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{
    /// <summary>
    /// Represents a request to create a new product in the system.
    /// </summary>
    public class DeleteProductRequest
    {
        /// <summary>
        /// Gets or sets the Id of the product.
        /// </summary>
        public Guid Id { get; set; }
    }
}
