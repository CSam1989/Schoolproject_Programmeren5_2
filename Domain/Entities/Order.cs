using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsPayed { get; set; }
        public DateTime OrderDate { get; set; }
        public string StreetShipping { get; set; }
        public string HouseNrShipping { get; set; }
        public string HouseBusShipping { get; set; }
        public string PostalcodeShipping { get; set; }
        public string CityShipping { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}