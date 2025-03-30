using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CartItemTests
    {

        [Fact(DisplayName = "CartItem should be created successfully")]
        public void CreateCartItem_ShouldCreateCartItem()
        {
            // Arrange
            var cartItem = CartItemTestData.GenerateValidCartItem();
            // Act & Assert
            Assert.NotNull(cartItem);
            Assert.NotEqual(Guid.Empty, cartItem.Id);
            Assert.NotNull(cartItem.Product);
            Assert.NotEqual(Guid.Empty, cartItem.ProductId);
        }

        [Fact(DisplayName = "Validation should fail for invalid quantity")]
        public void CreateCartItem_ShouldFailValidation_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            var cartItem = CartItemTestData.GenerateValidCartItem();
            cartItem.Quantity = -20;
            // Act
            var result = cartItem.Validate();
            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact(DisplayName = "Validation should fail for invalid quantity above 20")]
        public void CreateCartItem_ShouldFailValidation_WhenQuantityIsBiggerThan20()
        {
            // Arrange
            var cartItem = CartItemTestData.GenerateValidCartItem();
            cartItem.Quantity = 21;
            // Act
            var result = cartItem.Validate();
            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, x => x.Detail == "Quantity should be lower or equal than 20.");
        }

        [Fact(DisplayName = "Validation should fail for invalid price")]
        public void CreateCartItem_ShouldFailValidation_WhenPriceIsNegative()
        {
            // Arrange
            var cartItem = CartItemTestData.GenerateValidCartItem();
            cartItem.Price = -10;
            // Act
            var result = cartItem.Validate();
            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact(DisplayName = "Should throw ArgumentOutOfRangeException when change quantity bigger than 20")]
        public void CreateCartItem_ShouldThrowArgumentOutOfRangeException_WhenChangeQuantityBiggerThan20()
        {
            // Arrange
            var cartItem = CartItemTestData.GenerateValidCartItem();
            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => cartItem.UpdateQuantity(26));
        }

        [Fact(DisplayName = "Should calculate discount for quantity > 4 and < 10 correctly")]
        public void CalculateDiscount_ShouldReturnCorrectDiscount()
        {
            // Arrange
            var cartItem = CartItemTestData.GenerateValidCartItem();
            cartItem.Price = 10;
            cartItem.Quantity = 5;
            var discountValue = cartItem.Price * cartItem.Quantity * 0.1m;

            // Act
            // Assert
            Assert.Equal(discountValue, cartItem.CalculateDiscounts());
        }

        [Fact(DisplayName = "Should calculate discount for quantity >= 10 items correctly")]
        public void CalculateDiscount_ShouldReturnCorrectDiscount_WhenQuantityIsBiggerThan10()
        {
            // Arrange
            var cartItem = CartItemTestData.GenerateValidCartItem();
            cartItem.Price = 10;
            cartItem.Quantity = 15;
            var discountValue = cartItem.Price * cartItem.Quantity * 0.2m;
            // Act
            var calculatedDiscount = cartItem.CalculateDiscounts();
            // Assert
            Assert.Equal(discountValue, calculatedDiscount);
        }
    }
}
