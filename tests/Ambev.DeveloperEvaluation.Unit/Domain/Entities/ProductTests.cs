using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class ProductTests
    {
        [Fact(DisplayName = "Product should change to Active when activated")]
        public void Activate_ShouldSetStatusToActive()
        {
            // Arrange
            var product = new Product { Status = ProductStatus.Inactive };

            // Act
            product.Activate();

            // Assert
            Assert.Equal(ProductStatus.Active, product.Status);
            Assert.NotNull(product.UpdatedDate);
        }

        [Fact(DisplayName = "Product status should change to Suspended when suspended")]
        public void Deactivate_ShouldSetStatusToInactive()
        {
            // Arrange
            var product = new Product { Status = ProductStatus.Active };

            // Act
            product.Deactivate();

            // Assert
            Assert.Equal(ProductStatus.Inactive, product.Status);
        }

        [Fact(DisplayName = "Product price should be changed successfully")]
        public void ChangePrice_ShouldUpdatePrice()
        {
            // Arrange
            var product = new Product { Price = 10m };

            // Act
            product.ChangePrice(20m);

            // Assert
            Assert.Equal(20m, product.Price);
            Assert.NotNull(product.UpdatedDate);
        }

        [Fact(DisplayName = "Validation should fail for invalid price")]
        public void ChangePrice_ShouldThrowArgumentOutOfRangeException_WhenPriceIsZeroOrNegative()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => product.ChangePrice(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => product.ChangePrice(-1));
        }

        [Fact(DisplayName = "Product availability should be true")]
        public void IsAvailable_ShouldReturnTrue_WhenStatusIsActive()
        {
            // Arrange
            var product = new Product { Status = ProductStatus.Active };

            // Act
            var result = product.IsAvailable();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Product availability should be false")]
        public void IsAvailable_ShouldReturnFalse_WhenStatusIsInactive()
        {
            // Arrange
            var product = new Product { Status = ProductStatus.Inactive };

            // Act
            var result = product.IsAvailable();

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Validation should pass for valid product data")]
        public void Validate_ShouldReturnValidationResultDetail()
        {
            // Arrange
            var product = ProductTestData.GenerateValidProduct();

            // Act
            var result = product.Validate();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validation should fail for invalid product data")]
        public void Validate_ShouldReturnValidationResultDetailWithErrors()
        {
            // Arrange
            var product = ProductTestData.GenerateInvalidProduct();
            // Act
            var result = product.Validate();
            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}