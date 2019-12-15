using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfoDTO> GetUserInfoByUserInfoIdAsync(int id);

        Task<UserInfoDTO> GetUserInfoByAppIdentityIdAsync(Guid appIdentityUserId);

        Task<IEnumerable<UserInfoDTO>> GetUsersInfoAsync(int toSkip, int toTake);

        Task<int> GetUserInfoIdByEmailAsync(string email);

        Task<int> GetCountOfUsersInfoAsync();

        Task AddUserInfoAsync(UserInfoDTO userInfoDto);

        Task<bool> UpdateUserInfoAsync(UserInfoDTO newUserInfoDto);

        Task<bool> DeleteUserInfoByUserIdAsync(int userInfoId);

        Task<bool> UserInfoExistsAsync(int userInfoId);
    }
}