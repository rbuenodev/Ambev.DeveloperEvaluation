using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class CartItemTestData
    {
        public static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
            .RuleFor(c => c.Quantity, f => f.Random.Number(1, 20))
            .RuleFor(c => c.Price, f => f.Random.Decimal(1, 1000));

        public static CartItem GenerateValidCartItem()
        {
            var cartItem = CartItemFaker.Generate();
            cartItem.Id = Guid.NewGuid();
            cartItem.Product = ProductTestData.GenerateValidProduct();
            cartItem.ProductId = cartItem.Product.Id;
            return cartItem;
        }

        public static CartItem GenerateInvalidCartItem()
        {
            return new CartItem
            {
                Quantity = 26,
                Price = -10,
                Product = ProductTestData.GenerateInvalidProduct(),
                ProductId = Guid.NewGuid()
            };
        }
    }
}
