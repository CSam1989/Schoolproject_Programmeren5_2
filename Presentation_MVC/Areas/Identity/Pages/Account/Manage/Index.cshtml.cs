using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation_MVC.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public string Username { get; set; }

        [TempData] public string StatusMessage { get; set; }

        [BindProperty] public InputModel Input { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var customer = await _unitOfWork.Customers.GetFirstAsync(x => x.UserId == user.Id);
            user.Customer = customer;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Username = userName,
                FirstName = user.Customer.FirstName,
                FamilyName = user.Customer.FamilyName,
                Street = user.Customer.Street,
                HouseNr = user.Customer.HouseNr,
                HouseBus = user.Customer.HouseBus,
                PostalCode = user.Customer.PostalCode,
                City = user.Customer.City,
                StreetBilling = user.Customer.StreetBilling,
                HouseNrBilling = user.Customer.HouseNrBilling,
                HouseBusBilling = user.Customer.HouseBusBilling,
                PostalCodeBilling = user.Customer.PostalCodeBilling,
                CityBilling = user.Customer.CityBilling
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException(
                        $"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            var userName = await _userManager.GetUserNameAsync(user);
            if (Input.Username != userName)
            {
                var userNamerResult = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!userNamerResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException(
                        $"Unexpected error occurred setting userName for user with ID '{userId}'.");
                }
            }

            var customer = await _unitOfWork.Customers.GetFirstAsync(x => x.UserId == user.Id);
            user.Customer = customer;

            user.Customer.FirstName = Input.FirstName;
            user.Customer.FamilyName = Input.FamilyName;
            user.Customer.Street = Input.Street;
            user.Customer.HouseNr = Input.HouseNr;
            user.Customer.HouseBus = Input.HouseBus;
            user.Customer.PostalCode = Input.PostalCode;
            user.Customer.City = Input.City;
            user.Customer.StreetBilling = Input.StreetBilling;
            user.Customer.HouseNrBilling = Input.HouseNrBilling;
            user.Customer.HouseBusBilling = Input.HouseBusBilling;
            user.Customer.PostalCodeBilling = Input.PostalCodeBilling;
            user.Customer.CityBilling = Input.CityBilling;

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Username")]
            [MaxLength(20)]
            public string Username { get; set; }

            [Required]
            [Display(Name = "First name")]
            [MaxLength(50)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Family name")]
            [MaxLength(50)]
            public string FamilyName { get; set; }

            [Required]
            [Display(Name = "Street")]
            [MaxLength(100)]
            public string Street { get; set; }

            [Required]
            [Display(Name = "Nr")]
            [MaxLength(5)]
            public string HouseNr { get; set; }

            [Display(Name = "App.")]
            [MaxLength(4)]
            public string HouseBus { get; set; }

            [Required]
            [Display(Name = "Postalcode")]
            [MaxLength(6)]
            public string PostalCode { get; set; }

            [Required]
            [Display(Name = "City")]
            [MaxLength(100)]
            public string City { get; set; }

            [Display(Name = "Street - Billing")]
            [MaxLength(50)]
            public string StreetBilling { get; set; }

            [Display(Name = "Nr - Billing")]
            [MaxLength(5)]
            public string HouseNrBilling { get; set; }

            [Display(Name = "App. - Billing")]
            [MaxLength(4)]
            public string HouseBusBilling { get; set; }

            [Display(Name = "Postalcode - Billing")]
            [MaxLength(6)]
            public string PostalCodeBilling { get; set; }

            [Display(Name = "City - Billing")]
            [MaxLength(100)]
            public string CityBilling { get; set; }
        }
    }
}