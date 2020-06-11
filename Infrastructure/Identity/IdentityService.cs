using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.Authentication;
using Application.Customers.Queries.GetCustomerByUserId;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    //Identity hoort thuis in Infrastructure layer, dus ik ga niet in Application layer cqrs toepassen op authentication
    public class IdentityService : IIdentityService
    {
        public async Task<string> Login(LoginDto userCredentials)
        {
            var result =
                await _signInManager.PasswordSignInAsync(userCredentials.Username, userCredentials.Password, false,
                    false);

            if (result.Succeeded) return await LoginAndGetToken(userCredentials.Username);

            throw new UnauthorizedException("Username or password is incorrect");
        }

        public async Task<string> Register(RegisterDto registerCredentials)
        {
            if (await _userManager.FindByEmailAsync(registerCredentials.Email) != null ||
                await _userManager.FindByNameAsync(registerCredentials.Username) != null)
                throw new BadRequestException("Username/Email already exists");

            var user = new ApplicationUser
            {
                UserName = registerCredentials.Username,
                Email = registerCredentials.Email
            };

            var result = await _userManager.CreateAsync(user, registerCredentials.Password);

            if (result.Succeeded)
            {
                var Customer = new Customer
                {
                    FirstName = registerCredentials.FirstName,
                    FamilyName = registerCredentials.FamilyName,
                    Street = registerCredentials.Street,
                    HouseNr = registerCredentials.HouseNr,
                    HouseBus = registerCredentials.HouseBus,
                    PostalCode = registerCredentials.PostalCode,
                    City = registerCredentials.City,
                    StreetBilling = registerCredentials.StreetBilling,
                    HouseNrBilling = registerCredentials.HouseNrBilling,
                    HouseBusBilling = registerCredentials.HouseBusBilling,
                    PostalCodeBilling = registerCredentials.PostalCodeBilling,
                    CityBilling = registerCredentials.CityBilling,
                    UserId = user.Id
                };

                _unitOfWork.Customers.Create(Customer);

                if (await _unitOfWork.SaveChangesAsync(new CancellationToken()) > 0)
                    return await LoginAndGetToken(registerCredentials.Username);
            }

            throw new BadRequestException("Something went wrong with registering your account");
        }

        public async Task ChangePassword(ChangePasswordDto passwordCredentials)
        {
            var user = await GetCurrentUser(_currentUser.UserId);

            var changePasswordResult = await _userManager.ChangePasswordAsync(
                user,
                passwordCredentials.OldPassword,
                passwordCredentials.NewPassword);

            if (!changePasswordResult.Succeeded)
                throw new BadRequestException("Unexpected error when changing password");
        }

        public async Task<ChangeUserDetailsDto> GetCurrentUserDetails()
        {
            var user = await GetCurrentUser(_currentUser.UserId);

            var command = await _mediator.Send(new GetCustomerByUserIdQuery {UserId = _currentUser.UserId});

            // Dit is geen mooie solution, maar Ik zag geen andere oplossing..
            // Bij het Oproepen van deze methode word de CustomerByUserIdDto gebruikt
            // Maar Ik heb de gegevens van ChangeUserDetailsDto nodig
            var customer = _mapper.Map<ChangeUserDetailsDto>(command.Customer);
            customer.Username = user.UserName;

            return customer;
        }

        public async Task<string> ChangeUserDetails(ChangeUserDetailsDto userDetails)
        {
            var user = await GetCurrentUser(_currentUser.UserId);

            if (userDetails.Username != user.UserName)
            {
                var usernameResult = await _userManager.SetUserNameAsync(user, userDetails.Username);

                if (!usernameResult.Succeeded)
                    throw new BadRequestException("Unexpected error changing username");
            }

            var customer = await _unitOfWork.Customers.GetFirstAsync(c => c.UserId == user.Id);
            user.Customer = customer;

            user.Customer.FirstName = userDetails.FirstName;
            user.Customer.FamilyName = userDetails.FamilyName;
            user.Customer.Street = userDetails.Street;
            user.Customer.HouseNr = userDetails.HouseNr;
            user.Customer.HouseBus = userDetails.HouseBus;
            user.Customer.PostalCode = userDetails.PostalCode;
            user.Customer.City = userDetails.City;
            user.Customer.StreetBilling = userDetails.StreetBilling;
            user.Customer.HouseNrBilling = userDetails.HouseNrBilling;
            user.Customer.HouseBusBilling = userDetails.HouseBusBilling;
            user.Customer.PostalCodeBilling = userDetails.PostalCodeBilling;
            user.Customer.CityBilling = userDetails.CityBilling;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new BadRequestException("Unexpected error changing user details");

            return await LoginAndGetToken(userDetails.Username);
        }

        public async Task DeleteUser(DeleteUserDto deleteCredentials)
        {
            var user = await GetCurrentUser(_currentUser.UserId);

            if (!await _userManager.CheckPasswordAsync(user, deleteCredentials.Password))
                throw new UnauthorizedException("Password is incorrect");

            //eerst customer deleten, want de relatie staat op on delete restrict (voor de zekerheid)
            var customer = await _unitOfWork.Customers.GetFirstAsync(x => x.UserId == user.Id);

            //voor de zekerheid checken, maar normaal hangt er altijd een customer aan een user
            if (customer is null)
                throw new BadRequestException("Can't find customer");

            _unitOfWork.Customers.Delete(customer);

            if (await _unitOfWork.SaveChangesAsync(new CancellationToken()) > 0)
            {
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded) throw new BadRequestException("Unexpected error when deleting user");
            }
        }

        #region Ctor & DI

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICurrentUserService _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;


        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICurrentUserService currentUser,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            IHttpContextAccessor accessor
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _mediator = accessor.HttpContext.RequestServices.GetService<IMediator>();
        }

        #endregion

        #region private methods

        private async Task<string> LoginAndGetToken(string username)
        {
            var AppUser = await _userManager.Users.SingleOrDefaultAsync(a => a.UserName == username);
            var roles = await GetUserRole(AppUser.Id);
            return GenerateJwtToken(AppUser,
                roles.FirstOrDefault() ?? "User"); //Authenticated zonder role (null) = een gewone user
        }

        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(_configuration.GetSection("AppSettings:Secret").Value)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<ApplicationUser> GetCurrentUser(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == _currentUser.UserId);

            if (user is null)
                throw new NotFoundException("User", userId);

            return user;
        }

        private async Task<IList<string>> GetUserRole(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return await _userManager.GetRolesAsync(user);
        }

        #endregion
    }
}