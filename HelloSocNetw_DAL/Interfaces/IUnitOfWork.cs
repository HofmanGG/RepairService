using System.Threading.Tasks;
using HelloSocNetw_DAL.Interfaces.Repositories;

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