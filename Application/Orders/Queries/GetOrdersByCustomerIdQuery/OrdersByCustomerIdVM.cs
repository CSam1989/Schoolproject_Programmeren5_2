using System.Collections.Generic;

namespace Application.Orders.Queries.GetOrdersByCustomerIdQuery
{
    public class OrdersByCustomerIdVm
    {
        public IList<OrdersByCustomerIdDto> List { get; set; }
    }
}