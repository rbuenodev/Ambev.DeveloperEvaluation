using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class ProductTestData
    {
        private static readonly Faker<Product> ProductFaker = new Faker<Product>()
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
            .RuleFor(p => p.Status, f => f.PickRandom<ProductStatus>())
            .RuleFor(p => p.Category, f => f.Commerce.Department());

        public static Product GenerateValidProduct()
        {
            var product = ProductFaker.Generate();
            product.Id = Guid.NewGuid();
            return product;
        }

        public static Product GenerateInvalidProduct()
        {
            return new Product
            {
                Description = GenerateInvalidDescription(),
                Price = GenerateInvalidPrice(),
                Title = GenerateInvalidTitle(),
                Status = GenerateInvalidStatus(),
            };
        }

        public static string GenerateInvalidTitle()
        {
            return string.Empty;
        }

        public static string GenerateInvalidDescription()
        {
            return string.Empty;
        }

        public static decimal GenerateInvalidPrice()
        {
            return -10;
        }

        public static ProductStatus GenerateInvalidStatus()
        {
            return (ProductStatus)10;
        }
    }


}
