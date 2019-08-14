using DAL.EFRepositories;
using DAL.Interfaces;
using DAL.Сontexts;
using System.Threading.Tasks;
using DAL.Сontexts.Identity;

namespace DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocNetwContext _context;

        public UnitOfWork(
            SocNetwContext context,
            AppUserManager userManager,
            AppRoleManager roleManager
            )
        {
            _context = context;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private IUserInfoRepository _userInfoRepository;
        public IUserInfoRepository UsersInfo => _userInfoRepository ?? (_userInfoRepository = new EfUserInfoRepository(_context));

        private ICountryRepository _countryRepository;
        public ICountryRepository Countries => _countryRepository ?? (_countryRepository = new EfCountryRepository(_context));

        //custom identity managers
        public AppUserManager UserManager { get; }
        public AppRoleManager RoleManager { get; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}