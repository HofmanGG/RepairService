using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure;
using HelloSocNetw_DAL.Infrastructure.Exceptions;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfUserInfoRepository : IUserInfoRepository
    {
        private readonly SocNetwContext _context;
        private readonly IIncludesParser<UserInfo> _includesParser;

        public EfUserInfoRepository(DbContext dbContext, IIncludesParser<UserInfo> includesParser)
        {
            _context = dbContext as SocNetwContext;
            _includesParser = includesParser;
        }

        public async Task<UserInfo> GetUserInfoByUserInfoIdAsync(int userInfoId)
        {
            return await _context.UsersInfo.FindAsync(userInfoId);
        }

        public async Task<UserInfo> GetUserInfoByAppIdentityIdAsync(Guid appIdentityUserId)
        {
            return await _context.UsersInfo
                .Include(u => u.Country)
                .FirstOrDefaultAsync(u => u.AppIdentityUserId == appIdentityUserId);
        }

        public async Task<IEnumerable<UserInfo>> GetUsersInfoAsync(int toSkip, int toTake)
        {
            return await _context.UsersInfo
                .Skip(toSkip)
                .Take(toTake)
                .ToListAsync();
        }

        public async Task<UserInfo> GetUserInfoAsync(Expression<Func<UserInfo, bool>> filter, string includeProperties = "")
        {
            var query = _context.UsersInfo.AsQueryable();

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
            var query = _context.UsersInfo.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            _includesParser.AddIncludes(query, includeProperties);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<TType> GetAsync<TType>(Expression<Func<UserInfo, bool>> filter, Expression<Func<UserInfo, TType>> select) where TType : class
        {
            var foundObject =  await _context.UsersInfo.Where(filter).Select(select).FirstOrDefaultAsync();
            if (foundObject == null)
                throw new ObjectNotFoundException("Object with such filter is not found");
            else
                return foundObject;
        }

        public async Task<int> GetUserInfoIdByEmailAsync(string email)
        {
            return await _context.UsersInfo
                .Where(u => u.AppIdentityUser.Email == email)
                .Select(u => u.UserInfoId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountOfUsersInfoAsync()
        {
            return await _context.UsersInfo.CountAsync();
        }

        public void AddUserInfo(UserInfo userInfoToAdd)
        {
            _context.UsersInfo.Add(userInfoToAdd);
        }

        public void AddUsersInfo(IEnumerable<UserInfo> usersInfoToAdd)
        {
            _context.UsersInfo.AddRange(usersInfoToAdd);
        }

        public async Task DeleteUserInfoByUserInfoId(int userInfoId)
        {
            var userInfoToDelete = await _context.UsersInfo.FindAsync(userInfoId);
            _context.UsersInfo.Remove(userInfoToDelete);
        }

        public void DeleteUserInfo(UserInfo userInfoToDelete)
        {
            if (_context.Entry(userInfoToDelete).State == EntityState.Detached)
            {
                _context.UsersInfo.Attach(userInfoToDelete);
            }
            _context.UsersInfo.Remove(userInfoToDelete);
        }

        public void UpdateUserInfo(UserInfo userInfoToChange)
        {
            _context.UsersInfo.Update(userInfoToChange);
        }

        private void DetachEntity(UserInfo userInfoToDetach)
        {
            var local = _context.Set<UserInfo>()
               .Local
               .FirstOrDefault(u => u.UserInfoId == userInfoToDetach.UserInfoId);

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
        }

        public async Task<bool> UserInfoExistsAsync(int userInfoId)
        {
            return await _context.UsersInfo.AnyAsync(u => u.UserInfoId == userInfoId);
        }
    }
}
