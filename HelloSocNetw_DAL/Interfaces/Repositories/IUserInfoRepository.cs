using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_DAL.Interfaces.Repositories
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetUserInfoByIdAsync(long userInfoId);

        Task<UserInfo> GetUserInfoByIdentityIdAsync(Guid identityId);

        Task<IEnumerable<UserInfo>> GetUsersInfoAsync(int toSkip, int toTake);

        Task<UserInfo> GetUserInfoAsync(Expression<Func<UserInfo, bool>> filter, string includeProperties = "");

        Task<IEnumerable<UserInfo>> GetUsersInfoAsync(
            Expression<Func<UserInfo, bool>> filter = null,
            Func<IQueryable<UserInfo>, IOrderedQueryable<UserInfo>> orderBy = null,
            string includeProperties = "");

        Task<long> GetCountOfUsersInfoAsync();

        Task<UserInfo> GetUserInfoByEmailAsync(string email);

        void AddUserInfo(UserInfo userInfoToAdd);

        Task UpdateUserInfoAsync(UserInfo userInfoToUpdate);

        Task<bool> UserInfoExistsByIdAsync(long userInfoId);

        Task<bool> UserInfoExistsByIdAndIdentityIdAsync(long userInfoId, Guid identityId);
    }
}