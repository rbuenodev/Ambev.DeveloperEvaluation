namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.AddCartItem
{
    public class AddCartItemRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
