using System;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Identity;
using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserInfoRepository UsersInfo { get; }
        ICountryRepository Countries { get; }
        IPictureRepository Pictures { get; }

        UserManager<AppIdentityUser> UserManager { get; }
        RoleManager<AppUserRole> RoleManager { get; }

        Task SaveChangesAsync();
    }
}