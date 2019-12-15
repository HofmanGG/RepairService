using HelloSocNetw_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface IRepairRequestRepository
    {
        Task<RepairRequest> GetRepairRequestByRepairRequestIdAsync(int repairRequestId);

        Task<IEnumerable<RepairRequest>> GetRepairRequestsByUserInfoIdAsync(int userInfoId);

         Task<RepairRequest> GetRepairRequestAsync(Expression<Func<RepairRequest, bool>> filter, string includeProperties = "");

         Task<IEnumerable<RepairRequest>> GetRepairRequestsAsync(
            Expression<Func<RepairRequest, bool>> filter = null,
            Func<IQueryable<RepairRequest>, IOrderedQueryable<RepairRequest>> orderBy = null,
            string includeProperties = "");

        Task<TType> GetAsync<TType>(Expression<Func<RepairRequest, bool>> where, Expression<Func<RepairRequest, TType>> select) where TType : class;

        Task<int> GetCountOfRepairRequestsByUserInfoIdAsync(int userInfoId);

        void AddRepairRequest(RepairRequest repairRequest);

        void UpdateRepairRequestAsync(RepairRequest repairRequestToUpdate);

        Task DeleteRepairRequestByRepairRequestIdAsync(int repairRequestId);

        Task<bool> RepairRequestExistsAsync(int repairRequestId);

        Task<bool> RepairRequestExistsAsync(Expression<Func<RepairRequest, bool>> where);
    } 
}
