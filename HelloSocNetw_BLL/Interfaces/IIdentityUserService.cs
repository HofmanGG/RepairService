using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.EntitiesDTO;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IIdentityUserService
    {

        Task CreateAccountAsync(UserInfoDTO userInfoDto, string email, string userName, string password);

        Task<AppIdentityUserDTO> Authenticate(string email, string password);

        Task UpdateUserInfoAsync(UserInfoDTO userInfoDto);

        Task<AppIdentityUserDTO> FindByIdAsync(Guid appIdentityUserId);

        Task<string> GenerateEmailConfirmationTokenAsyncByEmail(string email);

        Task<UserInfoDTO> GetUserInfoWithTokenAsync(AppIdentityUserDTO appIdentityUserDto);

        Task<UserInfoDTO> GetUserInfoByAppIdentityIdAsync(Guid appIdentityUserId);

        Task<bool> ConfirmEmailAsync(string email, string code);

        Task<bool> UserWithSuchEmailAlreadyExistsAsync(string email);

        Task DeleteAccountByAppIdentityUserIdAsync(Guid appIdentityUserId);

        Task DeleteAccountAsync(AppIdentityUserDTO appIdentityUserDto);

        Task<bool> IsEmailConfirmedAsync(AppIdentityUserDTO appIdentityUserDto);

        Task<bool> IsLockedOutByEmailAsync(AppIdentityUserDTO appIdentityUserDto);
    }
}
