namespace Application.Orders.Queries.GetOrderByIdQuery
{
    public class ShoppingCartSummaryDto
    {
        public int TotalCount { get; set; }
        public decimal TotalExVat { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalInclVat { get; set; }
    }
}