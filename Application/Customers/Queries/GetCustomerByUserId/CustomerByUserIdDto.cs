using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Customers.Queries.GetCustomerByUserId
{
    public class CustomerByUserIdDto : IMapFrom<Customer>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Street { get; set; }
        public string HouseNr { get; set; }
        public string HouseBus { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StreetBilling { get; set; }
        public string HouseNrBilling { get; set; }
        public string HouseBusBilling { get; set; }
        public string PostalCodeBilling { get; set; }
        public string CityBilling { get; set; }
        public string UserId { get; set; }
    }
}