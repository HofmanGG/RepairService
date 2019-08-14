using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfImageRepository
    {
        private readonly SocNetwContext _context;

        public EfImageRepository(DbContext dbContext) => _context = dbContext as SocNetwContext;



    }
}
