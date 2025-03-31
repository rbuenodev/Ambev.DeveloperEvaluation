using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.ChangeStatusCart
{
    public class ChangeStatusCartValidator : AbstractValidator<ChangeStatusCartCommand>
    {
        public ChangeStatusCartValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
            RuleFor(c => c.Status).IsInEnum();
        }
    }
}
