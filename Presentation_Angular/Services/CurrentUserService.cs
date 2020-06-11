using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Presentation_Angular.Services
{
    //Thanks to Jason the dev - github Repo NorthwindTraders
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = UserId != null;
            IsAdmin = httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
        }

        public string UserId { get; }
        public bool IsAuthenticated { get; }
        public bool IsAdmin { get; }
    }
}