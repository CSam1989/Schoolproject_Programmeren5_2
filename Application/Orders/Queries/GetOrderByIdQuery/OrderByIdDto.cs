using System;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrderByIdQuery
{
    public class OrderByIdDto : IMapFrom<Order>
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string StreetShipping { get; set; }
        public string HouseNrShipping { get; set; }
        public string HouseBusShipping { get; set; }
        public string PostalcodeShipping { get; set; }
        public string CityShipping { get; set; }
        public CustomerOrderByIdDto Customer { get; set; }
    }
}