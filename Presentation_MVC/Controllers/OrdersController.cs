using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Common.Models.ShoppingCart;
using Application.Customers.Queries.GetCustomerByUserId;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Commands.UpdatePaidInOrder;
using Application.Orders.Queries.GetOrderByIdQuery;
using Application.Orders.Queries.GetOrdersByCustomerIdQuery;
using Application.Orders.Queries.GetOrdersQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.Cart;

namespace Presentation_MVC.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IShoppingCart _shoppingCart;

        public OrdersController(IHttpContextAccessor accessor, IShoppingCart shoppingCart)
        {
            _accessor = accessor;
            _shoppingCart = shoppingCart;
        }

        // GET: Orders/Confirmation/5
        public async Task<ActionResult> Confirmation(int id)
        {
            return View(await Mediator.Send(new GetOrderByIdQuery {Id = id}));
        }

        // GET: Orders/PreviousOrders
        public async Task<ActionResult> PreviousOrders()
        {
            return View(await Mediator.Send(new GetOrdersByCustomerIdQuery {UserId = GetCurrentUserId()}));
        }

        // GET: Orders/Admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin(int? currentPage, string sortBy, string currentFilter,
            string searchString)
        {
            return View(await Mediator.Send(new GetOrdersQuery
            {
                CurrentPage = currentPage,
                SortBy = sortBy,
                SearchString = searchString,
                CurrentFilter = currentFilter
            }));
        }

        // GET: Orders/Create
        public async Task<ActionResult> Create()
        {
            var customerQuery = await GetCustomer();

            return View(new CreateOrderCommand
            {
                CustomerId = customerQuery.Customer.Id,
                Customer = customerQuery.Customer,
                ListCartItems = GetCart()
            });
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            if (ModelState.IsValid)
            {
                command.ListCartItems =
                    GetCart(); //Ik krijg geen complexe objecten doorgestuurd via view dus haal ik de shoppingcart hier op
                var orderId = await Mediator.Send(command);
                _shoppingCart.EmptyCart();
                return RedirectToAction(nameof(Confirmation), new {id = orderId});
            }

            command.Customer = GetCustomer().Result.Customer;
            return View(command);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditIsPayed(int id, bool isPayed, int? currentPage, string sortBy,
            string currentFilter, string searchString)
        {
            await Mediator.Send(new UpdatePaidInOrderCommand {Id = id, IsPayed = isPayed});

            return RedirectToAction(nameof(Admin),
                new
                {
                    CurrentPage = currentPage,
                    SortBy = sortBy,
                    currentFilter,
                    searchString
                });
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await Mediator.Send(new GetOrderByIdQuery {Id = id}));
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Mediator.Send(new DeleteOrderCommand {Id = id});

            return RedirectToAction(nameof(Admin));
        }


        private List<CartItemDto> GetCart()
        {
            var cart = _shoppingCart.GetCart();
            var listCartDto = new List<CartItemDto>();

            foreach (var cartItem in cart)
            {
                var cartDto = new CartItemDto
                {
                    Product = cartItem.Product,
                    Quantity = cartItem.Quantity
                };
                listCartDto.Add(cartDto);
            }

            return listCartDto;
        }

        private string GetCurrentUserId()
        {
            return _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private async Task<CustomerByUserIdVm> GetCustomer()
        {
            return await Mediator.Send(new GetCustomerByUserIdQuery {UserId = GetCurrentUserId()});
        }
    }
}