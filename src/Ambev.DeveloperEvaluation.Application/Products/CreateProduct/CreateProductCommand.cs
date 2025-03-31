using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Command for creating a new product.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a product, 
    /// including product title price, description, category, status. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="CreateProductResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="CreateProductCommandValidator"/> which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class CreateProductCommand : IRequest<CreateProductResult>
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

        public ValidationResultDetail Validate()
        {
            var validator = new CreateProductValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
