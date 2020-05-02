using System;
using System.Threading.Tasks;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.EntitiesDTO;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IIdentityUserService
    {

        Task CreateAccountAsync(UserInfoDTO userInfoDto, string email, string userName, string password);

        Task<AppIdentityUserDTO> Authenticate(string email, string password);

        Task<AppIdentityUserDTO> FindByIdAsync(Guid identityId);

        Task<string> GenerateEmailConfirmationTokenAsyncByEmail(string email);

        Task<UserInfoDTO> GetUserInfoWithTokenAsync(AppIdentityUserDTO identityUserDto);

        Task<UserInfoDTO> GetUserInfoByIdentityIdAsync(Guid identityId);

        Task<bool> ConfirmEmailAsync(string email, string code);

        Task<bool> UserWithSuchEmailAlreadyExistsAsync(string email);

        Task DeleteAccountByIdentityIdAsync(Guid identityId);

        Task DeleteAccountAsync(AppIdentityUserDTO identityUserDto);

        Task<bool> IsEmailConfirmedAsync(AppIdentityUserDTO identityUserDto);

        Task<bool> IsLockedOutByEmailAsync(AppIdentityUserDTO identityUserDto);
    }
}
