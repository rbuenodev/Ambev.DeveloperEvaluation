using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(c => c.Branch).IsInEnum();
            RuleFor(c => c.SaleNumber).NotEmpty().NotNull();
        }
    }
}
