using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class CartItemsValidator : AbstractValidator<ICollection<CartItem>>
    {
        public CartItemsValidator()
        {
            RuleFor(items => items).Must(items => !ContainsItemsExceedingMaxQuantity(items.ToList(), 20))
                .WithMessage("Cart cannot have more than 20 quantity of each product");
        }

        public CartItemsValidator(int addingValue)
        {
            RuleFor(items => items).Must(items => !ContainsItemsExceedingMaxQuantity(items.ToList(), 20 - addingValue))
                .WithMessage("Cart cannot have more than 20 quantity of each product");
        }

        private bool ContainsItemsExceedingMaxQuantity(List<CartItem> items, int maxQuantityPerItem)
        {
            return items.GroupBy(item => item.ProductId)
                      .Any(group => group.Sum(item => item.Quantity) > maxQuantityPerItem);
        }
    }
}
