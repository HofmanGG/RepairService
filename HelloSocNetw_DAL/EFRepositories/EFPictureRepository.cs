using System.Linq;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfPictureRepository: IPictureRepository
    {
        private readonly SocNetwContext _context;

        public EfPictureRepository(DbContext dbContext) => _context = dbContext as SocNetwContext;

        public async Task AddPictureByUserIdAsync(int userId, Picture picture)
        {
            var user = await _context.UsersInfo.FindAsync(userId);
            user.Pictures.Add(picture);
        }

        public async Task AddProfilePictureByUserIdAsync(int userId, Picture picture)
        {
            var user = await _context.UsersInfo.FindAsync(userId);
            user.ProfilePicture = picture;
        }

        public async Task DeletePictureByUserIdAndPictureIdAsync(int userId, int pictureId)
        {
            var picture = await _context.Pictures.FirstOrDefaultAsync(p => p.UserInfoId == userId && p.PictureId == pictureId);
            _context.Pictures.Remove(picture);
        }

        public async Task<Picture> GetPictureByUserIdAndPictureId(int userId, int pictureId)
        {
            var picture = await _context.Pictures.FirstOrDefaultAsync(p => p.UserInfoId == userId && p.PictureId == pictureId);
            return picture;
        }

        public async Task<int> GetCountOfPicturesByUserIdAsync(int userId)
        {
            return await _context.Pictures.Where(p => p.UserInfoId == userId).CountAsync();
        }
    }
}
