using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation_MVC.Cart
{
    public interface IShoppingCart
    {
        List<CartItem> GetCart();

        Task AddToCart(int id);

        void RemoveFromCart(int id);

        void EmptyCart();

        int? GetCountCartItem(int id);

        int GetCount();

        decimal GetTotalExVat();

        decimal GetTotalIncVat();

        decimal GetVat();
    }
}