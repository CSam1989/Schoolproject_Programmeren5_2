using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrdersByCustomerIdQuery
{
    public class OrderLineOrdersByCustomerIdDto : IMapFrom<OrderLine>
    {
        public int Id { get; set; }
        public ProductOrdersByCustomerIdDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal OrderLineSubTotal => Quantity * Product.Price;
    }
}