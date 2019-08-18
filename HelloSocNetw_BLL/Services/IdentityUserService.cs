using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HelloSocNetw_BLL.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IdentityUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> UserWithSuchEmailAlreadyExistsAsync(string email)
        {
            return await _unitOfWork.UserManager.FindByEmailAsync(email) != null;
        }

        public async Task CreateAccountAsync(UserInfoDTO userInfoDto, string email, string password)
        {
            var appIdentityUser = new AppIdentityUser() {Email = email, UserName = email };

            await _unitOfWork.UserManager.CreateAsync(appIdentityUser, password);
                
            var userInfo = _mapper.Map<UserInfo>(userInfoDto); 
            userInfo.AppIdentityUser = appIdentityUser;

            _unitOfWork.UsersInfo.AddUserInfo(userInfo);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> RightDataAsync(string email, string password)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
            return user != null && await _unitOfWork.UserManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GetJwtTokenAsync(string email)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
            var identity = await GetClaimsIdentity(user);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: AuthJwtTokenOptions.Issuer,
                audience: AuthJwtTokenOptions.Audience,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(AuthJwtTokenOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(AppIdentityUser user)
        {
            // Here we can save some values to token.
            // For example we are storing here user id and email
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token");

            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claimsIdentity;
        }
    }
}