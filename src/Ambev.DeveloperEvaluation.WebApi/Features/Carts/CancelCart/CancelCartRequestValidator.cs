using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelCart
{
    public class CancelCartRequestValidator : AbstractValidator<CancelCartRequest>
    {
        public CancelCartRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
        }
    }
}
