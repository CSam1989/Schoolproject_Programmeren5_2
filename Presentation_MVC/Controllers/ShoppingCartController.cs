using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.Cart;

namespace Presentation_MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var cart = _shoppingCart.GetCart();

            var vm = new ShoppingCartVm
            {
                ShoppingCart = cart
            };

            return View(vm);
        }

        public async Task<ActionResult> AddToCart(int id)
        {
            await _shoppingCart.AddToCart(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult RemoveFromCart(int id)
        {
            _shoppingCart.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ClearCart()
        {
            _shoppingCart.EmptyCart();
            return RedirectToAction(nameof(Index));
        }
    }
}