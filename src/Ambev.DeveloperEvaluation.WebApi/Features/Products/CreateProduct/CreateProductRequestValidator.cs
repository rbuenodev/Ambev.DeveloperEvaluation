using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    /// <summary>
    /// Validator for CreateProductRequest that defines validation rules for user creation command.
    /// </summary>
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        /// <summary>
        /// Initializes a new instance of the CreateProductValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Price: Must be greater than 0
        /// - Title: Required, must be between 5 and 50 characters
        /// - Description: Maximum length of 255 characters
        /// - Category: Required, must be between 5 and 50 characters
        /// - Status: Must be a valid enum value
        /// </remarks>

        public CreateProductRequestValidator()
        {
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Title).NotEmpty().Length(5, 50);
            RuleFor(p => p.Description).MaximumLength(255);
            RuleFor(p => p.Category).NotEmpty().Length(5, 50);
            RuleFor(p => p.Status).IsInEnum();
        }
    }
}
