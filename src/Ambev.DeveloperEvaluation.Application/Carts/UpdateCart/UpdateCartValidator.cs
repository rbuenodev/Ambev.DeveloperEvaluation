using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
            RuleFor(c => c.UserId).NotEmpty().NotNull();
            RuleFor(c => c.Branch).IsInEnum();
            RuleFor(c => c.SaleNumber).NotEmpty().NotNull();
            RuleFor(c => c.Items).SetValidator(new CartItemsValidator());
        }
    }
}
