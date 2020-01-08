using HelloSocNetw_PL.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace HelloSocNetw_PL.Infrastructure.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            string userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            UserId = (userId != null) ? new Guid(userId) : Guid.Empty;
        }

        public Guid UserId { get; }
    }
}
