using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrdersQuery
{
    public class OrdersDto : IMapFrom<Order>
    {
        [DisplayName("OrderID")] public int Id { get; set; }

        [DisplayName("Is Payed")] public bool IsPayed { get; set; }

        [DisplayName("Order Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime OrderDate { get; set; }

        public CustomerOrdersDto Customer { get; set; }
        public ShoppingCartSummaryDto OrderSummary { get; set; }
    }
}