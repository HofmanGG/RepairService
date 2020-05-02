using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces.Repositories;
using HelloSocNetw_DAL.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfUserInfoRepository : IUserInfoRepository
    {
        private readonly SocNetwContext _context;
        private readonly IIncludesParser _includesParser;
        private readonly DbSet<UserInfo> _usersInfo;

        public EfUserInfoRepository(SocNetwContext dbContext, IIncludesParser includesParser)
        {
            _context = dbContext;
            _includesParser = includesParser;
            _usersInfo = _context.UsersInfo;
        }

        public async Task<UserInfo> GetUserInfoByIdAsync(long userInfoId)
        {
            return await _usersInfo
                .Include(ui => ui.Country)
                .FirstOrDefaultAsync(ui => ui.Id == userInfoId);
        }

        public async Task<UserInfo> GetUserInfoByIdentityIdAsync(Guid appIdentityUserId)
        {
            return await _usersInfo
                .Include(u => u.Country)
                .Include(u => u.AppIdentityUser.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.AppIdentityUserId == appIdentityUserId);
        }

        public async Task<IEnumerable<UserInfo>> GetUsersInfoAsync(int toSkip, int toTake)
        {
            return await _usersInfo
                .Skip(toSkip)
                .Take(toTake)
                .ToListAsync();
        }

        public async Task<UserInfo> GetUserInfoAsync(Expression<Func<UserInfo, bool>> filter, string includeProperties = "")
        {
            var query = _usersInfo.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            _includesParser.AddIncludes(query, includeProperties);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserInfo>> GetUsersInfoAsync(
            Expression<Func<UserInfo, bool>> filter = null,
            Func<IQueryable<UserInfo>, IOrderedQueryable<UserInfo>> orderBy = null,
            string includeProperties = "")
        {
            var query = _usersInfo.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            _includesParser.AddIncludes(query, includeProperties);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<UserInfo> GetUserInfoByEmailAsync(string email)
        {
            var userInfo = await _usersInfo.FirstOrDefaultAsync(u => u.AppIdentityUser.Email == email);
            return userInfo;
        }

        public async Task<long> GetCountOfUsersInfoAsync()
        {
            var count = await _usersInfo.CountAsync();
            return count;
        }

        public void AddUserInfo(UserInfo userInfoToAdd)
        {
            _usersInfo.Add(userInfoToAdd);
        }

        public async Task DeleteUserInfoById(long userInfoId)
        {
            var userInfoToDelete = await _usersInfo.FindAsync(userInfoId);
            _usersInfo.Remove(userInfoToDelete);
        }   

        public async Task UpdateUserInfoAsync(UserInfo userInfo)
        {
            var userInfoToChange = await _usersInfo.FindAsync(userInfo.Id);

            userInfoToChange.CountryId = userInfo.CountryId;
            userInfoToChange.Gender = userInfo.Gender;
            userInfoToChange.DateOfBirth = userInfo.DateOfBirth;
            userInfoToChange.FirstName = userInfo.FirstName;
            userInfoToChange.LastName = userInfo.LastName;
        }

        public async Task<bool> UserInfoExistsByIdAsync(long userInfoId)
        {
            var userInfoExists = await _usersInfo.AnyAsync(u => u.Id == userInfoId);
            return userInfoExists;   
        }

        public async Task<bool> UserInfoExistsByIdAndIdentityIdAsync(long userInfoId, Guid appIdentityUserId)
        {
            var userInfoExists = await _usersInfo.AnyAsync(u => u.Id == userInfoId && u.AppIdentityUserId == appIdentityUserId);
            return userInfoExists;
        }
    }
}
