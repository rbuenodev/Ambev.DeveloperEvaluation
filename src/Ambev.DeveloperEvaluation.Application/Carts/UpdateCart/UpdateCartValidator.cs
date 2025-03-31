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
            RuleFor(c => c.Items).SetValidator(new UpdateCartItemsValidator());
        }
    }

    public class UpdateCartItemsValidator : AbstractValidator<ICollection<UpdateCartItemCommand>>
    {
        public UpdateCartItemsValidator()
        {
            RuleFor(items => items).Must(items => !ContainsItemsExceedingMaxQuantity(items.ToList(), 20))
                .WithMessage("Cart cannot have more than 20 quantity of each product");
        }

        private bool ContainsItemsExceedingMaxQuantity(List<UpdateCartItemCommand> items, int maxQuantityPerItem)
        {
            return items.GroupBy(item => item.ProductId)
                  .Any(group => group.Sum(item => item.Quantity) > maxQuantityPerItem);
        }
    }
}
