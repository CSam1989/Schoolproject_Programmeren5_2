using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrdersByCustomerIdQuery
{
    public class ProductOrdersByCustomerIdDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}