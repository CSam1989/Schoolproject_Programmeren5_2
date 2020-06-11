using System.ComponentModel;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Orders.Queries.GetOrdersQuery
{
    public class CustomerOrdersDto : IMapFrom<Customer>
    {
        [DisplayName("CustomerID")] public int Id { get; set; }

        public string FirstName { get; set; }
        public string FamilyName { get; set; }
    }
}