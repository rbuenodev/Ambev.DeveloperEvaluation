using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartRequestValidator()
        {
            RuleFor(c => c.Id).NotEmpty().NotNull();
            RuleFor(c => c.Branch).IsInEnum();
            RuleFor(c => c.SaleNumber).NotEmpty().NotNull();
            RuleFor(c => c.Items).SetValidator(new UpdateCartItemsRequestValidator());
        }
    }

    public class UpdateCartItemsRequestValidator : AbstractValidator<ICollection<UpdateCartItemRequest>>
    {
        public UpdateCartItemsRequestValidator()
        {
            RuleFor(items => items).Must(items => !ContainsItemsExceedingMaxQuantity(items.ToList(), 20))
                .WithMessage("Cart cannot have more than 20 quantity of each product");
        }

        private bool ContainsItemsExceedingMaxQuantity(List<UpdateCartItemRequest> items, int maxQuantityPerItem)
        {
            return items.GroupBy(item => item.ProductId)
                  .Any(group => group.Sum(item => item.Quantity) > maxQuantityPerItem);
        }
    }
}
