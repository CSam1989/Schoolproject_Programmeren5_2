using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.Products.Queries.GetProductsByIdQuery
{
    public class ProductByIdDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoId { get; set; }
    }
}