using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.Exceptions;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;

namespace HelloSocNetw_BLL.Services
{
    public class UserInfoService: IUserInfoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mpr;

        public UserInfoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mpr = mapper;
        }

        public async Task<UserInfoDTO> GetUserInfoByIdAsync(long userInfoId)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByIdAsync(userInfoId);
            if (userInfo == null)
                throw new NotFoundException(nameof(UserInfo), userInfoId);

            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);
            return userInfoDto;
        }

        public async Task<UserInfoDTO> GetUserInfoByIdentityIdAsync(Guid appIdentityUserId)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByIdentityIdAsync(appIdentityUserId);
            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);
            return userInfoDto;
        }

        public async Task<IEnumerable<UserInfoDTO>> GetUsersInfoAsync(int toSkip, int toTake)
        {
            var usersInfo = await _uow.UsersInfo.GetUsersInfoAsync(toSkip, toTake);
            var usersInfoDto = _mpr.Map<IEnumerable<UserInfoDTO>>(usersInfo);
            return usersInfoDto;
        }

        public async Task<long> GetCountOfUsersInfoAsync()
        { 
            var count = await _uow.UsersInfo.GetCountOfUsersInfoAsync();
            return count;
        }

        public async Task<long> GetUserInfoIdByEmailAsync(string email)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByEmailAsync(email);
            if (userInfo == null)
            {
                throw new NotFoundException(nameof(UserInfo), email);
            }

            return userInfo.Id;
        }

        public async Task AddUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var userInfo = _mpr.Map<UserInfo>(userInfoDto);

            _uow.UsersInfo.AddUserInfo(userInfo);

            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("User info creating went wrong");
        }

        public async Task UpdateUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var doesUserInfoToChangeExist = await _uow.UsersInfo
                .UserInfoExistsByIdAndIdentityIdAsync(userInfoDto.Id, userInfoDto.AppIdentityUserId);

            if (!doesUserInfoToChangeExist)
                throw new NotFoundException(nameof(UserInfo), userInfoDto.Id);

            var userInfo = _mpr.Map<UserInfo>(userInfoDto);

            await _uow.UsersInfo.UpdateUserInfoAsync(userInfo);

            var rowsAffected = await _uow.SaveChangesAsync();
            if(rowsAffected != 1)
                throw new DBOperationException("User info updating went wrong");
        }
    }
}
    
