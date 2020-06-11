using Application.Common.Models.ShoppingCart;

namespace Presentation_MVC.Cart
{
    public class CartItem
    {
        public CartItemProductDto Product { get; set; }
        public int Quantity { get; set; }

        public decimal Price => Product.Price * Quantity;
    }
}