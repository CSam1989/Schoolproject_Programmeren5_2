using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Presentation_MVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty] public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    Customer = new Customer
                    {
                        FirstName = Input.FirstName,
                        FamilyName = Input.FamilyName,
                        Street = Input.Street,
                        HouseNr = Input.HouseNr,
                        HouseBus = Input.HouseBus,
                        PostalCode = Input.PostalCode,
                        City = Input.City,
                        StreetBilling = Input.StreetBilling,
                        HouseNrBilling = Input.HouseNrBilling,
                        HouseBusBilling = Input.HouseBusBilling,
                        PostalCodeBilling = Input.PostalCodeBilling,
                        CityBilling = Input.CityBilling
                    }
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                user.Customer.UserId = user.Id;

                await _unitOfWork.SaveChangesAsync(new CancellationToken());

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        null,
                        new {area = "Identity", userId = user.Id, code},
                        Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        return RedirectToPage("RegisterConfirmation", new {email = Input.Email});

                    await _signInManager.SignInAsync(user, false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public class InputModel
        {
            [Required] [EmailAddress] public string Email { get; set; }

            [Required] [MaxLength(20)] public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 6)]
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
}