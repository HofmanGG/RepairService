using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IUserInfoService
    {
        Task<UserInfo> SingleOrDefaultUserInfoAsync(Expression<Func<UserInfo, bool>> predicate);

        Task<UserInfoDTO> GetUserInfoByIdAsync(int id);

        Task<IEnumerable<UserInfoDTO>> GetUsersInfoAsync(int toSkip, int toTake);

        Task<int> GetCountOfUsersInfoAsync();

        IQueryable<UserInfo> FindUsersInfo(Expression<Func<UserInfo, bool>> predicate);

        Task AddUserInfoAsync(UserInfoDTO userInfoDto);

        Task<int> GetCountOfFriendsByUserIdAsync(int id);

        Task<int> GetCountOfSubscribersByUserIdAsync(int id);

        Task AddSubscriberByUserIdAbdSubIdAsync(int userId, int subId);

        Task AddFriendByUsersIdAsync(int userId, int subId);

        Task AddPictureByUserIdAsync(int userId, PictureDTO pictureDto);

        Task<PictureDTO> GetPictureByUserIdAndPictureIdAsync(int userId, int pictureId);

        Task<int> GetCountOfPicturesByUserIdAsync(int userId);

        Task DeletePictureByUserIdAndPictureIdAsync(int userId, int pictureId);

        Task DeleteUserInfoByUserIdAsync(int userId);

        Task UpdateUserInfoAsync(UserInfoDTO userInfoDto);

        Task DeleteFriendshipByUserIdAndFriendIdAsync(int userId, int friendId);

        Task DeleteSubscriptionByUserIdAndSubIdAsync(int userId, int subId);
    }
}