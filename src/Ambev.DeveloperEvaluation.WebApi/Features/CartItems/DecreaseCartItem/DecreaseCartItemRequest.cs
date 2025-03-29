namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.DecreaseCartItem
{
    public class DecreaseCartItemRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
