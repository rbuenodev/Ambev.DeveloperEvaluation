using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{
    /// <summary>
    /// Validator for DeleteProductRequest that defines validation rules for user creation command.
    /// </summary>
    public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
    {
        /// <summary>
        /// Initializes a new instance of the DeleteProductRequestValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Id: Must be not null or empty
        /// </remarks>

        public DeleteProductRequestValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty();
        }
    }
}
