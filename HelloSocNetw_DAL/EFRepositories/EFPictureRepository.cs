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

        public async Task AddProfilePictureByUserIdAsync(int userId, Picture picture)
        {
            var user = await _context.UsersInfo.FindAsync(userId);
            user.ProfilePicture = picture;
        }
    }
}
