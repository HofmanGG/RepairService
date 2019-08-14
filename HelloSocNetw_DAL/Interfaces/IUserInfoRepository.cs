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
        Task<UserInfo> SingleOrDefaultUserInfoAsync(Expression<Func<UserInfo, bool>> predicate);
        Task<UserInfo> GetUserInfoByIdAsync(int id);

        Task<IEnumerable<Group>> GetUsersInfoAsync(
            IQueryable<Group> query,
            int toSkip,
            int toTake);

        Task<int> GetCountOfUsersInfoAsync();

        Task<int> GetCountOfFriendsByUserIdAsync(int id);

        Task<int> GetCountOfSubscribersByUserIdAsync(int id);

        Task<bool> UserContainsSubscriberAsync(int userId, int subId);

        Task<bool> UserContainsFriendAsync(int userId, int friendId);

        IQueryable<UserInfo> FindUsersInfo(Expression<Func<UserInfo, bool>> predicate);

        void AddUserInfo(UserInfo userInfo);

        Task AddSubscriberByUserIdAndSubIdAsync(int userId, int subId);

        Task<IEnumerable<UserInfo>> GetSubscribersByUserIdAsync(int id, int toTake);

        Task AddFriendByUsersIdAndSubIdAsync(int firstUserId, int secondUserId);

        Task<IEnumerable<UserInfo>> GetFriendsByUserIdAsync(int id, int toTake);

        void UpdateUserInfo(UserInfo userInfo);
    }
}