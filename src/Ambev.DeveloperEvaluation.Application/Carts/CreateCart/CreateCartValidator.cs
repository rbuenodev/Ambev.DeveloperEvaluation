using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(p => p.UserId).NotEmpty().NotNull();
            RuleFor(p => p.Branch).NotEmpty().NotNull();
            RuleFor(p => p.SaleNumber).NotEmpty().NotNull();
        }
    }
}
