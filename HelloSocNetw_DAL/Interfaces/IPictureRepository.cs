using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IPictureRepository
    {
        Task AddProfilePictureByUserIdAsync(int userId, Picture picture);
    }
}