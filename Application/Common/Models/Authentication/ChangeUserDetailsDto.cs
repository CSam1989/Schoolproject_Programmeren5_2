using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Application.Customers.Queries.GetCustomerByUserId;

namespace Application.Common.Models.Authentication
{
    public class ChangeUserDetailsDto : IMapFrom<CustomerByUserIdDto>
    {
        [Required] [MaxLength(20)] public string Username { get; set; }

        [Required] [MaxLength(50)] public string FirstName { get; set; }

        [Required] [MaxLength(50)] public string FamilyName { get; set; }

        [Required] [MaxLength(100)] public string Street { get; set; }

        [Required] [MaxLength(5)] public string HouseNr { get; set; }

        [MaxLength(4)] public string HouseBus { get; set; }

        [Required] [MaxLength(6)] public string PostalCode { get; set; }

        [Required] [MaxLength(100)] public string City { get; set; }

        [MaxLength(50)] public string StreetBilling { get; set; }

        [MaxLength(5)] public string HouseNrBilling { get; set; }

        [MaxLength(4)] public string HouseBusBilling { get; set; }

        [MaxLength(6)] public string PostalCodeBilling { get; set; }

        [MaxLength(100)] public string CityBilling { get; set; }
    }
}