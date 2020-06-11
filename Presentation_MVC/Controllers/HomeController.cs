using System.Diagnostics;
using System.Threading.Tasks;
using Application.Products.Queries.GetProductsQuery;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.Cart;
using Presentation_MVC.Models;

namespace Presentation_MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IShoppingCart _shoppingCart;

        public HomeController(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<ActionResult> Index(int? currentPage, string currentFilter, string searchString)
        {
            return View(await Mediator.Send(new GetProductsQuery
            {
                CurrentPage = currentPage,
                SearchString = searchString,
                CurrentFilter = currentFilter
            }));
        }

        public async Task<ActionResult> AddToCart(int id, int? currentPage, string currentFilter, string searchString)
        {
            await _shoppingCart.AddToCart(id);
            return RedirectToAction("Index",
                new
                {
                    CurrentPage = currentPage,
                    currentFilter,
                    searchString
                });
        }

        public ActionResult RemoveFromCart(int id, int? currentPage, string currentFilter, string searchString)
        {
            _shoppingCart.RemoveFromCart(id);
            return RedirectToAction("Index",
                new
                {
                    CurrentPage = currentPage,
                    currentFilter,
                    searchString
                });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}