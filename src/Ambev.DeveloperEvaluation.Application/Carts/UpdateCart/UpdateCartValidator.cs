using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
            RuleFor(c => c.UserId).NotEmpty().NotNull();
            RuleFor(c => c.Branch).NotNull();
            RuleFor(c => c.SaleNumber).NotEmpty().NotNull();

        }
    }
}
