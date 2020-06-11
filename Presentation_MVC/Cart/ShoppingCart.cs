using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.ShoppingCart;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Presentation_MVC.Helpers;

namespace Presentation_MVC.Cart
{
    public class ShoppingCart : IShoppingCart
    {
        private const string CartSessionKey = "cartId";

        private const decimal VatPercentage = 0.06M;
        // Shoppçing cart zet ik in het MVC project omdat ervoor het angular project een andere implementatie gebruikt wordt
        // En omdat Session niet zo goed werkt als je het buiten het MVC project gebruikt

        // Inspiratie Shoppingcart model gebruikt (met aanpassingen) van https://github.com/shakeelosmani/MvcAffableBean/tree/master/MvcAffableBean

        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCart(IHttpContextAccessor accessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accessor = accessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public List<CartItem> Cart { get; private set; }


        public List<CartItem> GetCart()
        {
            return _accessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(GetCartId()) ?? new List<CartItem>();
        }

        public async Task AddToCart(int id)
        {
            var cartItem = GetCartItem(id);

            if (cartItem is null)
            {
                var product = await _unitOfWork.Products.GetFirstAsync(p => p.Id == id);
                cartItem = new CartItem
                {
                    Product = _mapper.Map<CartItemProductDto>(product),
                    Quantity = 1
                };
                Cart.Add(cartItem);
            }
            else
            {
                Cart[Cart.IndexOf(cartItem)].Quantity++;
            }

            _accessor.HttpContext.Session.SetObjectAsJson(GetCartId(), Cart);
        }

        public void RemoveFromCart(int id)
        {
            var cartItem = GetCartItem(id);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                    Cart[Cart.IndexOf(cartItem)].Quantity--;
                else
                    Cart.Remove(cartItem);
                _accessor.HttpContext.Session.SetObjectAsJson(GetCartId(), Cart);
            }
        }

        public void EmptyCart()
        {
            _accessor.HttpContext.Session.Clear();
        }

        public int? GetCountCartItem(int id)
        {
            var cartItem = GetCartItem(id);

            return cartItem?.Quantity;
        }

        public int GetCount()
        {
            var count = 0;
            Cart = GetCart();
            Cart.ForEach(c => count += c.Quantity);
            return count;
        }

        public decimal GetTotalExVat()
        {
            return GetTotalIncVat() - GetVat();
        }

        public decimal GetVat()
        {
            return GetTotalIncVat() * VatPercentage;
        }

        public decimal GetTotalIncVat()
        {
            decimal total = 0;
            Cart = GetCart();
            Cart.ForEach(c => total += c.Product.Price * c.Quantity);
            return total;
        }


        private string GetCartId()
        {
            var cartId = _accessor.HttpContext.Session.GetObjectFromJson<string>(CartSessionKey);

            if (!(cartId is null)) return cartId;

            cartId = Guid.NewGuid().ToString();
            _accessor.HttpContext.Session.SetObjectAsJson(CartSessionKey, cartId);

            return cartId;
        }

        private CartItem GetCartItem(int productId)
        {
            Cart = GetCart();
            return Cart.FirstOrDefault(c => c.Product.Id == productId);
        }
    }
}