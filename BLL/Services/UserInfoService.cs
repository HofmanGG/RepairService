using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using DAL.Entities;
using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
/*
namespace BLL.Services
{
    public class UserInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserInfoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserInfo> SingleOrDefaultUserInfoAsync(Expression<Func<UserInfo, bool>> predicate)
        {
            return await _unitOfWork.UsersInfo.SingleOrDefaultUserInfoAsync(predicate);
        }
        
        public async Task<UserInfoDTO> GetUserInfoByIdAsync(int id)
        {
            var userInfo = await _unitOfWork.UsersInfo.GetUserInfoByIdAsync(id);
            var userInfoDto = _mapper.Map<UserInfoDTO>(userInfo);
            return userInfoDto;
        }

        public async Task<IEnumerable<Group>> GetUsersInfoAsync(
            IQueryable<Group> query,
            int toSkip,
            int toTake)
        {
            return await query
                .Skip(toSkip)
                .Take(toTake)
                .ToListAsync();
        }

        public async Task<int> GetCountOfUsersInfoAsync() => await _unitOfWork.UsersInfo.GetCountOfUsersInfoAsync();

        public IQueryable<UserInfo> FindUsersInfo(Expression<Func<UserInfo, bool>> predicate) => _unitOfWork.UsersInfo.FindUsersInfo(predicate);

        public void AddUserInfo(UserInfoDTO userInfoDto)
        {
            var userInfo = _mapper.Map<UserInfo>(userInfoDto);
            _unitOfWork.UsersInfo.AddUserInfo(userInfo);
            _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> GetCountOfFriendsByUserIdAsync(int id)
        {
            return await _unitOfWork.UsersInfo.GetCountOfFriendsByUserIdAsync(id);
        }

        public async Task<int> GetCountOfSubscribersByUserIdAsync(int id)
        {
            return await _unitOfWork.UsersInfo.GetCountOfSubscribersByUserIdAsync(id);
        }

        public async Task AddSubscriberByUserIdAbdSubIdAsync(int userId, int subId)
        {
            if (await _unitOfWork.UsersInfo.UserContainsSubscriberAsync(userId, subId)) //sub is already subscribed
                return;

            if (await _unitOfWork.UsersInfo.UserContainsSubscriberAsync(subId, userId)) //user is subscribed on sub
                return;

            if (await _unitOfWork.UsersInfo.UserContainsFriendAsync(userId, subId)) //they are friends
                return;

            await _unitOfWork.UsersInfo.AddSubscriberByUserIdAndSubIdAsync(userId, subId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddFriendByUsersIdAsync(int userId, int subId)
        {
            if (await _unitOfWork.UsersInfo.UserContainsSubscriberAsync(subId, userId)) //cant apply friendship because he is sub
                return;

            if (await _unitOfWork.UsersInfo.UserContainsFriendAsync(userId, subId)) //they are already friends
                return;

            if (await _unitOfWork.UsersInfo.UserContainsSubscriberAsync(userId, subId))
            {
                _unitOfWork.UsersInfo.AddFriendByUsersIdAndSubIdAsync(userId, subId);
                _unitOfWork.UsersInfo.AddFriendByUsersIdAndSubIdAsync(subId, userId);

                _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var userInfo = _mapper.Map<UserInfo>(userInfoDto);
            _unitOfWork.UsersInfo.UpdateUserInfo(userInfo);
            await _unitOfWork.SaveChangesAsync();
        }
    }
    }
    */
