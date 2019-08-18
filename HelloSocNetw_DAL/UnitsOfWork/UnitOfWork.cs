using System.Threading.Tasks;
using HelloSocNetw_DAL.EFRepositories;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Identity;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.UnitsOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly SocNetwContext _context;

        public UnitOfWork(
            SocNetwContext context,
            UserManager<AppIdentityUser> userManager,
            RoleManager<AppUserRole> roleManager
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

        private IPictureRepository _pictureRepository;
        public IPictureRepository Pictures => _pictureRepository ?? (_pictureRepository = new EfPictureRepository(_context));

        //custom identity managers
        public UserManager<AppIdentityUser> UserManager { get; }
        public RoleManager<AppUserRole> RoleManager { get; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}