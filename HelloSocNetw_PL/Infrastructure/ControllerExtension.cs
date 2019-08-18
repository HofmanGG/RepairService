using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace HelloSocNetw_PL.Infrastructure
{
    public static class ControllerExtensions
    {
        public static int GetUserId(this ControllerBase controller) =>
            int.Parse(controller.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
    }
}