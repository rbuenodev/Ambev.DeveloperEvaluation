using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Carts.Commands
{
    [Collection("Database")]
    public class CreateCartCommandTests : DatabaseTestBase
    {
        public CreateCartCommandTests(DatabaseFixture fixture) : base(fixture)
        {
        }
        [Fact]
        public async Task ShouldCreateValidCart_WhenCommandIsValid()
        {
            var command = new CreateCartCommand { Branch = Domain.Enums.Branch.America, SaleNumber = "xx123", UserId = Guid.Parse("ee0ff89b-f80d-495c-b7af-437be2f5b306") };
            var validationResult = command.Validate();

            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);

            var response = await SendAsync(command);
            Assert.NotNull(response);
            Assert.False(string.IsNullOrEmpty(response.Id.ToString()));
        }
    }
}
