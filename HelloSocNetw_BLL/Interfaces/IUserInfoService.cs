using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.ModelsDTO;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfoDTO> GetUserInfoByIdAsync(long id);

        Task<UserInfoDTO> GetUserInfoByIdentityIdAsync(Guid appIdentityUserId);

        Task<IEnumerable<UserInfoDTO>> GetUsersInfoAsync(int toSkip, int toTake);

        Task<long> GetUserInfoIdByEmailAsync(string email);

        Task<long> GetCountOfUsersInfoAsync();


        Task AddUserInfoAsync(UserInfoDTO userInfoDto);

        Task UpdateUserInfoAsync(UserInfoDTO newUserInfoDto);
    }
}