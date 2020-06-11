using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Products.Queries.GetProductsQuery
{
    public class ProductListDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoId { get; set; }
    }
}