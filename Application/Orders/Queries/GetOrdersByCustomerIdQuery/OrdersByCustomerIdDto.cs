using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using Application.Orders.Queries.GetOrdersQuery;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrdersByCustomerIdQuery
{
    public class OrdersByCustomerIdDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public bool IsPayed { get; set; }
        public DateTime OrderDate { get; set; }
        public string StreetShipping { get; set; }
        public string HouseNrShipping { get; set; }
        public string HouseBusShipping { get; set; }
        public string PostalcodeShipping { get; set; }
        public string CityShipping { get; set; }
        public CustomerOrdersByCustomerIdDto Customer { get; set; }
        public ICollection<OrderLineOrdersByCustomerIdDto> OrderLines { get; set; }
        public ShoppingCartSummaryOrderByCustomerIdDto OrderSummary { get; set; }
    }
}