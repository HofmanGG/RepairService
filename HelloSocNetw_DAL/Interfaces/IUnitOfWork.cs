using System;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Identity;
using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserInfoRepository UsersInfo { get; }
        IRepairRequestRepository RepairRequests { get; }
        ICountryRepository Countries { get; }

        Task<int> SaveChangesAsync();
    }
}