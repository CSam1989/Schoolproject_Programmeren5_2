namespace Application.Common.Models
{
    public class OrderSortingModel
    {
        public string SortByCustomer { get; set; }
        public string SortByOrderId { get; set; }
        public string SortByTotalAmount { get; set; }
        public string SortByTotalPrice { get; set; }
        public string SortByIsPayed { get; set; }
        public string SortByOrderDate { get; set; }
    }
}