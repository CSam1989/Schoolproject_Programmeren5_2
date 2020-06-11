using System.ComponentModel;

namespace Application.Orders.Queries.GetOrdersQuery
{
    public class ShoppingCartSummaryDto
    {
        [DisplayName("Total Amount")] public int TotalCount { get; set; }

        [DisplayName("Total Price")] public decimal TotalInclVat { get; set; }
    }
}