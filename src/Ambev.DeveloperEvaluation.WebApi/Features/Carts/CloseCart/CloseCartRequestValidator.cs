using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseCart
{
    public class CloseCartRequestValidator : AbstractValidator<CloseCartRequest>
    {
        public CloseCartRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
        }
    }
}
