using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using static HelloSocNetw_DAL.Infrastructure.DALEnums;

namespace BLL.Services
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

        public async Task<UserInfoDTO> GetUserInfoByUserInfoIdAsync(int id)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByUserInfoIdAsync(id);
            if(userInfo == null)
                return null;

            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);
            return userInfoDto;
        }

        public async Task<UserInfoDTO> GetUserInfoByAppIdentityIdAsync(Guid appIdentityUserId)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByAppIdentityIdAsync(appIdentityUserId);
            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);

            return userInfoDto;
        }

        public async Task<IEnumerable<UserInfoDTO>> GetUsersInfoAsync(int toSkip, int toTake)
        {
            var usersInfo = await _uow.UsersInfo.GetUsersInfoAsync(toSkip, toTake);
            var usersInfoDto = _mpr.Map<IEnumerable<UserInfoDTO>>(usersInfo);
            return usersInfoDto;
        }

        public async Task<int> GetCountOfUsersInfoAsync()
        {
            return await _uow.UsersInfo.GetCountOfUsersInfoAsync();
        }

        public async Task<int> GetUserInfoIdByEmailAsync(string email)
        {
            var userInfoWithIdAnon = await _uow.UsersInfo.GetAsync(u => u.AppIdentityUser.Email == email, x => new { x.UserInfoId });
            return userInfoWithIdAnon.UserInfoId;
        }

        public async Task AddUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var userInfo = _mpr.Map<UserInfo>(userInfoDto);
            _uow.UsersInfo.AddUserInfo(userInfo);
            await _uow.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserInfoAsync(UserInfoDTO newUserInfoDto)
        {
            var userInfoToChange = await _uow.UsersInfo.GetUserInfoByUserInfoIdAsync(newUserInfoDto.UserInfoId);

            userInfoToChange.CountryId = newUserInfoDto.CountryId;
            userInfoToChange.Gender = (DALGenderType)(int)newUserInfoDto.Gender;
            userInfoToChange.DateOfBirth = newUserInfoDto.DateOfBirth;
            userInfoToChange.FirstName = newUserInfoDto.FirstName;
            userInfoToChange.LastName = newUserInfoDto.LastName;

            _uow.UsersInfo.UpdateUserInfo(userInfoToChange);
            var rowsAffected = await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<bool> DeleteUserInfoByUserIdAsync(int userInfoId)
        {
            await _uow.UsersInfo.DeleteUserInfoByUserInfoId(userInfoId);
            var rowsAffected =  await _uow.SaveChangesAsync();
            return rowsAffected == 2;
        }

        public async Task<bool> UserInfoExistsAsync(int userInfoId)
        {
            return await _uow.UsersInfo.UserInfoExistsAsync(userInfoId);
        }
    }
}
    
