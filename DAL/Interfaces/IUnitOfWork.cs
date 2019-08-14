using System;
using System.Threading.Tasks;
using DAL.Сontexts.Identity;

namespace DAL.Interfaces
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