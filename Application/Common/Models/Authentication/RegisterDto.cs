using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models.Authentication
{
    public class RegisterDto
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required] [MaxLength(20)] public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


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