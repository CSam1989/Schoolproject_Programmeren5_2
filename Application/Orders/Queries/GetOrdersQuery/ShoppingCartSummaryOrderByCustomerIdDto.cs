namespace Application.Orders.Queries.GetOrdersQuery
{
    public class ShoppingCartSummaryOrderByCustomerIdDto
    {
        public decimal TotalExVat { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalInclVat { get; set; }
    }
}