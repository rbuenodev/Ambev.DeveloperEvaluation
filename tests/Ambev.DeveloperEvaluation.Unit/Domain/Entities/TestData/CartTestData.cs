using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class CartTestData
    {

        private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
            .RuleFor(c => c.CreatedAt, f => f.Date.Past())
            .RuleFor(c => c.Status, f => f.PickRandom<CartStatus>())
            .RuleFor(c => c.Branch, f => f.PickRandom<Branch>())
            .RuleFor(c => c.SaleNumber, f => f.Commerce.Ean8());

        public static Cart GenerateValidCart()
        {
            var cart = CartFaker.Generate();
            cart.Id = Guid.NewGuid();
            cart.User = UserTestData.GenerateValidUser();
            cart.UserId = cart.User.Id;

            return cart;
        }

        public static Cart GenerateInvalidCart()
        {
            return new Cart
            {
                SaleNumber = string.Empty,
                Status = (CartStatus)10,
                User = null,
                UserId = Guid.NewGuid()
            };
        }
    }
}
