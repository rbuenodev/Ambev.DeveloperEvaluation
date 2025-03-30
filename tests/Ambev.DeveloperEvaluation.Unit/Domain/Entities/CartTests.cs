using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class CartTests
    {
        [Fact(DisplayName = "Validation should pass for valid cart data")]
        public void Validate_ShouldReturnValidationResultDetail()
        {
            // Arrange
            var cart = CartTestData.GenerateValidCart();

            // Act
            var validationResult = cart.Validate();

            // Assert
            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact(DisplayName = "Validation should fail for invalid cart data")]
        public void Validate_ShouldReturnValidationResultDetailErros()
        {
            // Arrange
            var cart = CartTestData.GenerateInvalidCart();

            // Act
            var validationResult = cart.Validate();

            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }

        [Fact(DisplayName = "Validation should fail when there is more than 20 identical products")]
        public void Validate_ShouldReturnValidationResultDetailErros_WhenThereIsMoreThan20IdenticalProducts()
        {
            // Arrange
            var cart = CartTestData.GenerateValidCart();
            var item1 = CartItemTestData.GenerateValidCartItem();
            item1.UpdateQuantity(10);
            var item2 = CartItemTestData.GenerateValidCartItem();
            item2.UpdateQuantity(11);
            item2.ProductId = item1.ProductId;

            cart.Items.Add(item1);
            cart.Items.Add(item2);

            // Act
            var validationResult = cart.Validate();

            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains(validationResult.Errors, x => x.Detail == "Cart cannot have more than 20 quantity of each product");
        }

        [Fact(DisplayName = "Should calculate total price")]
        public void GetTotalPrice_ShouldReturnCorrectTotalPrice()
        {
            // Arrange
            var cart = CartTestData.GenerateValidCart();
            cart.Items.Add(new CartItem { Price = 10, Quantity = 1 });

            // Act
            var totalPrice = cart.CalculateTotalProducts() - cart.CalculateTotalDiscounts();

            // Assert
            Assert.Equal(10, totalPrice);
        }

        [Fact(DisplayName = "Should calculate total price with discount")]
        public void GetTotalPrice_ShouldReturnCorrectTotalPriceWithDiscount()
        {
            // Arrange
            var totalProduct = 10 * 5;
            var discount = totalProduct * 0.1m;
            var totalProductWithDiscount = totalProduct - discount;
            var cart = CartTestData.GenerateValidCart();
            cart.Items.Add(new CartItem { Price = 10, Quantity = 5 });

            // Act
            var totalPrice = cart.CalculateTotalProducts() - cart.CalculateTotalDiscounts();
            // Assert
            Assert.Equal(totalProductWithDiscount, totalPrice);
        }

        [Fact(DisplayName = "Should calculete total price with discount when there is more items")]
        public void GetTotalPrice_ShouldReturnCorrectTotalPriceWithDiscountWhenThereIsMoreThanOneItems()
        {
            // Arrange
            var item1 = new CartItem { Price = 10, Quantity = 4 };
            var item2 = new CartItem { Price = 10, Quantity = 20 };
            var item3 = new CartItem { Price = 10, Quantity = 1 };
            var cart = CartTestData.GenerateValidCart();
            cart.Items.Add(item1);
            cart.Items.Add(item2);
            cart.Items.Add(item3);
            // Act
            var totalProducts = item1.CalculateTotal() + item2.CalculateTotal() + item3.CalculateTotal();
            var totalDiscounts = item1.CalculateDiscounts() + item2.CalculateDiscounts() + item3.CalculateDiscounts();
            var totalProductWithDiscount = totalProducts - totalDiscounts;
            var calculatedTotalPriceByCart = cart.CalculateTotalProducts() - cart.CalculateTotalDiscounts();
            // Assert
            Assert.Equal(totalProductWithDiscount, calculatedTotalPriceByCart);
            Assert.Equal(totalDiscounts, cart.CalculateTotalDiscounts());
            Assert.Equal(totalProducts, cart.CalculateTotalProducts());
        }

        [Fact(DisplayName = "Should change cartStatus")]
        public void ChangeStatus_ShouldChangeCartStatus()
        {
            // Arrange
            var cart = CartTestData.GenerateValidCart();
            cart.Status = CartStatus.Created;
            // Act
            cart.ChangeStatus(CartStatus.Closed);
            // Assert
            Assert.Equal(CartStatus.Closed, cart.Status);
        }
    }
}
