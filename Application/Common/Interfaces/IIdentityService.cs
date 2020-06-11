using System.Threading.Tasks;
using Application.Common.Models.Authentication;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> Login(LoginDto userCredentials);
        Task<string> Register(RegisterDto registerCredentials);
        Task ChangePassword(ChangePasswordDto passwordCredentials);
        Task<ChangeUserDetailsDto> GetCurrentUserDetails();
        Task<string> ChangeUserDetails(ChangeUserDetailsDto userDetails);
        Task DeleteUser(DeleteUserDto deleteCredentials);
    }
}