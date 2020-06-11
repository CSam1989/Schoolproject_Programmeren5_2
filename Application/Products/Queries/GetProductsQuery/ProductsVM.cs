using System.Collections.Generic;

namespace Application.Products.Queries.GetProductsQuery
{
    public class ProductsVm
    {
        public IList<ProductListDto> List { get; set; }
        //public PagSortFilterToReturnDto PagSortFilterToReturn { get; set; }
    }
}