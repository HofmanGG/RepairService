using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IPictureRepository
    {
        Task AddPictureByUserIdAsync(int userId, Picture picture);

        Task AddProfilePictureByUserIdAsync(int userId, Picture picture);

        Task DeletePictureByUserIdAndPictureIdAsync(int userId, int pictureId);

        Task<Picture> GetPictureByUserIdAndPictureId(int userId, int pictureId);

        Task<int> GetCountOfPicturesByUserIdAsync(int userId);
    }
}