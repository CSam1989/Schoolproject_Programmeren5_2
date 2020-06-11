using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models.ShoppingCart
{
    public class CartItemProductDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}