using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.ModelsDTO;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IIdentityUserService
    {
        Task<bool> UserWithSuchEmailAlreadyExistsAsync(string email);
        Task CreateAccountAsync(UserInfoDTO userInfoDto, string email, string password);
        Task<UserInfoDTO> Authenticate(string email, string password);
        Task<string> GetJwtTokenAsync(string email);
        Task UpdateUserInfoAsync(UserInfoDTO userInfoDto);
        Task<string> GetUserEmailByIdAsync(int id);
        Task<UserInfoDTO> GetUserInfoByIdAsync(int id);
    }
}
