using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart
{
    public class GetCartRequestValidator:AbstractValidator<GetCartRequest>
    {
        public GetCartRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
        }
    }
}
