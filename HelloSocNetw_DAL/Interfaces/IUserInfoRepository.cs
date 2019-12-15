using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetUserInfoByUserInfoIdAsync(int userInfoId);

        Task<UserInfo> GetUserInfoByAppIdentityIdAsync(Guid appIdentityUserId);

        Task<IEnumerable<UserInfo>> GetUsersInfoAsync(int toSkip, int toTake);

        Task<UserInfo> GetUserInfoAsync(Expression<Func<UserInfo, bool>> filter, string includeProperties = "");

        Task<IEnumerable<UserInfo>> GetUsersInfoAsync(
            Expression<Func<UserInfo, bool>> filter = null,
            Func<IQueryable<UserInfo>, IOrderedQueryable<UserInfo>> orderBy = null,
            string includeProperties = "");

        Task<TType> GetAsync<TType>(Expression<Func<UserInfo, bool>> filter, Expression<Func<UserInfo, TType>> select) where TType : class;

        Task<int> GetCountOfUsersInfoAsync();

        Task<int> GetUserInfoIdByEmailAsync(string email);

        void AddUserInfo(UserInfo userInfoToAdd);

        void AddUsersInfo(IEnumerable<UserInfo> usersInfoToAdd);

        void UpdateUserInfo(UserInfo userInfoToUpdate);

        Task DeleteUserInfoByUserInfoId(int userInfoId);

        void DeleteUserInfo(UserInfo userInfoToDelete);

        Task<bool> UserInfoExistsAsync(int userInfoId);
    }
}