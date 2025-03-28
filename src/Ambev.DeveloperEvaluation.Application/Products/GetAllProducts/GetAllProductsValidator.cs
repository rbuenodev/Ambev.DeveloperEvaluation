using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
    public class GetAllProductsValidator : AbstractValidator<GetAllProductsCommand>
    {
        public GetAllProductsValidator()
        {
        }
    }
}
