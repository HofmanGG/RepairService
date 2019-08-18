using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserInfoService: IUserInfoService
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

        public async Task<IEnumerable<UserInfoDTO>> GetUsersInfoAsync(int toSkip, int toTake)
        {
            var usersInfo = await _unitOfWork.UsersInfo.GetUsersInfoAsync(toSkip, toTake);
            var usersInfoDto = _mapper.Map<IEnumerable<UserInfoDTO>>(usersInfo);
            return usersInfoDto;
        }

        public async Task<int> GetCountOfUsersInfoAsync() => await _unitOfWork.UsersInfo.GetCountOfUsersInfoAsync();

        public IQueryable<UserInfo> FindUsersInfo(Expression<Func<UserInfo, bool>> predicate) =>
            _unitOfWork.UsersInfo.FindUsersInfo(predicate);

        public async Task AddUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var userInfo = _mapper.Map<UserInfo>(userInfoDto);
            _unitOfWork.UsersInfo.AddUserInfo(userInfo);
            await _unitOfWork.SaveChangesAsync();
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

        public async Task AddFriendByUserIdAndSubIdAsync(int userId, int subId)
        {
            if (await _unitOfWork.UsersInfo.UserContainsSubscriberAsync(subId, userId)) //cant apply friendship because he is sub
                return;

            if (await _unitOfWork.UsersInfo.UserContainsFriendAsync(userId, subId)) //they are already friends
                return;

            if (await _unitOfWork.UsersInfo.UserContainsSubscriberAsync(userId, subId))
            {
                await _unitOfWork.UsersInfo.AddFriendByUsersIdAndSubIdAsync(userId, subId);
                await _unitOfWork.UsersInfo.AddFriendByUsersIdAndSubIdAsync(subId, userId);

                await _unitOfWork.UsersInfo.DeleteSubscriptionAsync(userId, subId);

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteFriendshipByUserIdAndFriendIdAsync(int userId, int friendId)
        {
            if (await _unitOfWork.UsersInfo.UserContainsFriendAsync(userId, friendId))
            {
                await _unitOfWork.UsersInfo.DeleteFriendshipAsync(userId, friendId);
                await _unitOfWork.UsersInfo.DeleteFriendshipAsync(friendId, userId);

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteSubscriptionByUserIdAndSubIdAsync(int userId, int subId)
        {
            if (await _unitOfWork.UsersInfo.UserContainsSubscriberAsync(userId, subId))
            {
                await _unitOfWork.UsersInfo.DeleteSubscriptionAsync(userId, subId);

                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task AddPictureByUserIdAsync(int userId, PictureDTO pictureDto)
        {
            var picture = _mapper.Map<Picture>(pictureDto);
            await _unitOfWork.Pictures.AddPictureByUserIdAsync(userId, picture);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PictureDTO> GetPictureByUserIdAndPictureIdAsync(int userId, int pictureId)
        {
            var picture = await _unitOfWork.Pictures.GetPictureByUserIdAndPictureId(userId, pictureId);
            var pictureDto = _mapper.Map<PictureDTO>(picture);
            return pictureDto;
        }

        public async Task<int> GetCountOfPicturesByUserIdAsync(int userId)
        {
            return await _unitOfWork.Pictures.GetCountOfPicturesByUserIdAsync(userId);
        }

        public async Task DeletePictureByUserIdAndPictureIdAsync(int userId, int pictureId)
        {
            await _unitOfWork.Pictures.DeletePictureByUserIdAndPictureIdAsync(userId, pictureId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var userInfo = _mapper.Map<UserInfo>(userInfoDto);
            _unitOfWork.UsersInfo.UpdateUserInfo(userInfo);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserInfoByUserIdAsync(int userId)
        {
            await _unitOfWork.UsersInfo.DeleteUserInfoByUserId(userId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
    
