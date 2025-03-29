using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
            RuleFor(c => c.UserId).NotEmpty().NotNull();
            RuleFor(c => c.Branch).IsInEnum();
            RuleFor(c => c.SaleNumber).NotEmpty().NotNull();

        }
    }
}
