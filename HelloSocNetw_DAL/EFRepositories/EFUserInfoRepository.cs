using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfUserInfoRepository : IUserInfoRepository
    {
        private readonly SocNetwContext _context;

        public EfUserInfoRepository(DbContext dbContext) => _context = dbContext as SocNetwContext;

        public async Task<UserInfo> SingleOrDefaultUserInfoAsync(Expression<Func<UserInfo, bool>> predicate)
        {
            return await _context.UsersInfo.SingleOrDefaultAsync(predicate);
        }

        public async Task<UserInfo> GetUserInfoByIdAsync(int id)
        {
            return await _context.UsersInfo.FindAsync(id);
        }

        public async Task<IEnumerable<UserInfo>> GetUsersInfoAsync(int toSkip, int toTake)
        {
            return await _context.UsersInfo
                .Skip(toSkip)
                .Take(toTake)
                .ToListAsync();
        }

        public async Task<bool> UserContainsSubscriberAsync(int userId, int subId)
        {
            return await _context.Subscribers.AnyAsync(t => t.UserId == userId && t.SubscriberId == subId);
        }

        public async Task<bool> UserContainsFriendAsync(int userId, int subId)
        {
            return await _context.Subscribers.AnyAsync(t => t.UserId == userId && t.SubscriberId == subId);
        }

        public async Task<int> GetCountOfUsersInfoAsync() => await _context.UsersInfo.CountAsync();

        public async Task<int> GetCountOfFriendsByUserIdAsync(int id)
        {
            return await _context.Friends.Where(u => u.UserId == id).CountAsync();
        }

        public async Task<int> GetCountOfSubscribersByUserIdAsync(int id)
        {
            return await _context.Subscribers.Where(u => u.UserId == id).CountAsync();
        }

        public IQueryable<UserInfo> FindUsersInfo(Expression<Func<UserInfo, bool>> predicate) => _context.UsersInfo.Where(predicate);

        public void AddUserInfo(UserInfo userInfo) => _context.UsersInfo.Add(userInfo);

        public void AddUsersInfo(IEnumerable<UserInfo> usersInfo) => _context.UsersInfo.AddRange(usersInfo);

        public async Task AddSubscriberByUserIdAndSubIdAsync(int userId, int subId)
        {
            var user = await _context.UsersInfo.FindAsync(userId);
            var subscription = new SubscribersTable()
            {
                UserId = userId,
                SubscriberId = subId
            };
            user.Subscribers.Add(subscription);
        }

        public async Task AddFriendByUsersIdAndSubIdAsync(int userId, int subId)
        {
            var user = await _context.UsersInfo.FindAsync(userId);

            var friendshipForUser = new FriendshipTable()
            {
                UserId = userId,
                FriendId = subId
            };
            user.Friends.Add(friendshipForUser);
        }

        public async Task DeleteSubscriptionAsync(int userId, int subId)
        {
            var subscription = await _context.Subscribers.FirstOrDefaultAsync(t => t.UserId == userId && t.SubscriberId == subId);
            _context.Subscribers.Remove(subscription);
        }

        public async Task DeleteFriendshipAsync(int userId, int friendId)
        {
            var subscription = await _context.Friends.FirstOrDefaultAsync(t => t.UserId == userId && t.FriendId == friendId);
            _context.Friends.Remove(subscription);
        }

        public async Task DeleteUserInfoByUserId(int userId)
        {
            var user = await _context.UsersInfo.FindAsync(userId);
            _context.UsersInfo.Remove(user);
        }

        public async Task<IEnumerable<UserInfo>> GetFriendsByUserIdAsync(int id, int toTake)
        {
            var friends = await _context.Friends
                .Where(u => u.UserId == id)
                .Select(u => u.Friend)
                .ToListAsync();

            return friends;
        }

        public async Task<IEnumerable<UserInfo>> GetSubscribersByUserIdAsync(int id, int toTake)
        {
            var subs = await _context.Subscribers
                .Where(u => u.UserId == id)
                .Select(u => u.Subscriber)
                .ToListAsync();

            return subs;
        }

        public void UpdateUserInfo(UserInfo userInfo)
        {
            _context.UsersInfo.Update(userInfo);
        }
    }
}
