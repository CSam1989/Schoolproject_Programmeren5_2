using System.Collections.Generic;
using Application.Common.Models;

namespace Application.Orders.Queries.GetOrdersQuery
{
    public class OrdersVm
    {
        public IList<OrdersDto> List { get; set; }
        public PagSortFilterToReturnDto PagSortFilterToReturn { get; set; }
    }
}