using System;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Identity;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserInfoRepository UsersInfo { get; }
        ICountryRepository Countries { get; }

        AppUserManager UserManager { get; }
        AppRoleManager RoleManager { get; }

        Task SaveChangesAsync();
    }
}