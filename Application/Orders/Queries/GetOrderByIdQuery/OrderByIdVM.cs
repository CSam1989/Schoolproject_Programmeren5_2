namespace Application.Orders.Queries.GetOrderByIdQuery
{
    public class OrderByIdVm
    {
        public OrderByIdDto Order { get; set; }
        public ShoppingCartSummaryDto OrderSummary { get; set; }
    }
}