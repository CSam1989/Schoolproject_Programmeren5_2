using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoId { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}