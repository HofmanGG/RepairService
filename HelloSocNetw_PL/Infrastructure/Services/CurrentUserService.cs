using HelloSocNetw_PL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace HelloSocNetw_PL.Infrastructure.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var stringUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            UserId = stringUserId != null ? new Guid(stringUserId) : Guid.Empty;
        }

        public Guid UserId { get; }
    }
}
    