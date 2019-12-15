using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Services
{
    public class JWTService: IJWTService
    {
        private readonly IIdentityUnitOfWork _identityUow;

        public JWTService(IIdentityUnitOfWork identityUnitOfWork)
        {
            _identityUow = identityUnitOfWork;
        }

        public async Task<string> GetJwtTokenAsync(AppIdentityUser appIdentityUser)
        {
            var identity = await GetClaimsIdentityAsync(appIdentityUser);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: AuthJwtTokenOptions.Issuer,
                audience: AuthJwtTokenOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(AuthJwtTokenOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentityAsync(AppIdentityUser user)
        {
            // Here we can save some values to token.
            // For example we are storing here user id and email
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            var roles = await _identityUow.UserManager.GetRolesAsync(user);

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claimsIdentity;
        }
    }
}
