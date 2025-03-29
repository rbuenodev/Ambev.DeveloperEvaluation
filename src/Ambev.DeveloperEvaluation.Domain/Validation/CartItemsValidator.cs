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

        private bool ContainsItemsExceedingMaxQuantity(List<CartItem> items, int maxQuantityPerItem)
        {
            var itemGroups = items.GroupBy(item => item.ProductId)
                                 .Select(group => new
                                 {
                                     Id = group.Key,
                                     TotalQuantity = group.Sum(item => item.Quantity)
                                 });
            return itemGroups.Any(group => group.TotalQuantity > maxQuantityPerItem);
        }
    }
}
