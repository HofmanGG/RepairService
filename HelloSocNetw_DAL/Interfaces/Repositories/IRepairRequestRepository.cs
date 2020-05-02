using HelloSocNetw_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HelloSocNetw_DAL.Interfaces.Repositories
{
    public interface IRepairRequestRepository
    {
        Task<RepairRequest> GetRepReqByIdAsync(long repReqId);

        Task<IEnumerable<RepairRequest>> GetRepReqsByUserInfoIdAsync(long userInfoId);

        Task<RepairRequest> GetRepReqAsync(Expression<Func<RepairRequest, bool>> filter, string includeProperties = "");

        Task<IEnumerable<RepairRequest>> GetRepReqsAsync(
           Expression<Func<RepairRequest, bool>> filter = null,
           Func<IQueryable<RepairRequest>, IOrderedQueryable<RepairRequest>> orderBy = null,
           string includeProperties = "");

        Task<TType> GetAsync<TType>(Expression<Func<RepairRequest, bool>> where, Expression<Func<RepairRequest, TType>> select) where TType : class;

        Task<long> GetCountOfRepReqsByUserInfoIdAsync(long userInfoId);

        void AddRepReq(RepairRequest repReq);

        Task UpdateRepReqAsync(RepairRequest repReqToUpdate);

        Task DeleteRepReqByIdAsync(long repReqId);

        Task<bool> RepReqExistsByIdAsync(long repReqId);

        Task<bool> RepReqExistsByIdAndUserInfoIdAsync(long repReqId, long userInfoId);
    }
}
