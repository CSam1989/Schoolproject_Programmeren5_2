namespace Application.Common.Models.ShoppingCart
{
    public class CartItemDto
    {
        public CartItemProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}