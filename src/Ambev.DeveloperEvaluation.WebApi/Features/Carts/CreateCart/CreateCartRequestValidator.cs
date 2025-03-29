using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(c=> c.UserId).NotEmpty().NotNull();
            RuleFor(c=> c.Branch).NotEmpty().NotNull();
            RuleFor(c=> c.SaleNumber).NotEmpty().NotNull();
        }
    }
}
