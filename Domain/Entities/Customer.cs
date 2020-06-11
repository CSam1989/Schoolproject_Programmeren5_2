using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Customer
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

        [ForeignKey("ApplicationUser")] public string UserId { get; set; }

        public ICollection<Order> Orders { get; set; }

        //Is niet volgens Clean Architecture, maar hiermee kan ik gemakkelijk de personal data laten downloaden voor GDPR
        public override string ToString()
        {
            var stringbuilder = new StringBuilder();
            stringbuilder.Append($"ID: {Id}; ");
            stringbuilder.Append($"First name: {FirstName}; ");
            stringbuilder.Append($"Family name: {FamilyName}; ");
            stringbuilder.Append($"Street: {Street}; ");
            stringbuilder.Append($"Nr: {HouseNr}; ");
            stringbuilder.Append($"App.: {HouseBus}; ");
            stringbuilder.Append($"Postalcode: {PostalCode}; ");
            stringbuilder.Append($"City: {City}; ");
            stringbuilder.Append($"Street Billing: {StreetBilling}; ");
            stringbuilder.Append($"Nr Billing: {HouseNrBilling}; ");
            stringbuilder.Append($"App. Billing: {HouseBusBilling}; ");
            stringbuilder.Append($"Postalcode Billing: {PostalCodeBilling}; ");
            stringbuilder.Append($"City Billing: {CityBilling}; ");

            return stringbuilder.ToString();
        }
    }
}