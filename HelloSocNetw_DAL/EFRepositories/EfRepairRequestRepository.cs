using HelloSocNetw_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Interfaces.Repositories;
using HelloSocNetw_DAL.Interfaces.Services;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfRepairRequestRepository: IRepairRequestRepository
    {
        private readonly IIncludesParser _includesParser;
        private readonly SocNetwContext _context;
        private readonly DbSet<RepairRequest> _repairRequests;

        public EfRepairRequestRepository(SocNetwContext dbContext, IIncludesParser includesParser)
        {
            _context = dbContext;
            _includesParser = includesParser;
            _repairRequests = _context.RepairRequests;
        }

        public async Task<RepairRequest> GetRepReqByIdAsync(long repReqId)
        {
            var repReq = await _repairRequests.FindAsync(repReqId);
            return repReq;
        }

        public async Task<RepairRequest> GetRepReqAsync(Expression<Func<RepairRequest, bool>> filter, string includeProperties = "")
        {
            var query = _repairRequests.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            _includesParser.AddIncludes(query, includeProperties);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RepairRequest>> GetRepReqsAsync(
            Expression<Func<RepairRequest, bool>> filter = null,
            Func<IQueryable<RepairRequest>, IOrderedQueryable<RepairRequest>> orderBy = null,
            string includeProperties = "")
        {
            var query = _repairRequests.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            _includesParser.AddIncludes(query, includeProperties);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<TType> GetAsync<TType>(Expression<Func<RepairRequest, bool>> where, Expression<Func<RepairRequest, TType>> select) where TType : class
        {
            return await _repairRequests.Where(where).Select(select).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RepairRequest>> GetRepReqsByUserInfoIdAsync(long userInfoId)
        {
            var repReqs = await _repairRequests.Where(r => r.UserInfoId == userInfoId).ToListAsync();
            return repReqs;
        }

        public async Task<long> GetCountOfRepReqsByUserInfoIdAsync(long userInfoId)
        {
           var count = await _repairRequests.Where(r => r.UserInfoId == userInfoId).CountAsync();
           return count;
        }

        public void AddRepReq(RepairRequest repReqToAdd)
        {
            _repairRequests.Add(repReqToAdd);
        }

        public async Task UpdateRepReqAsync(RepairRequest repReq)
        {
            var repReqToChange = await _repairRequests.FindAsync(repReq.Id);

            repReqToChange.RepairStatus = repReq.RepairStatus;
            repReqToChange.ProductName = repReq.ProductName;
            repReqToChange.Comment = repReq.Comment;
        }

        public async Task DeleteRepReqByIdAsync(long repReqId)
        {
            var repReqToDelete = await _repairRequests.FindAsync(repReqId);
            _repairRequests.Remove(repReqToDelete);
        }
            
        public async Task<bool> RepReqExistsByIdAsync(long repReqId)
        {
            var repReqExists =  await _repairRequests.AnyAsync(rr => rr.Id == repReqId);
            return repReqExists;
        }

        public async Task<bool> RepReqExistsByIdAndUserInfoIdAsync(long repReqId, long userInfoId)
        {
            var repReqExists =
                await _repairRequests.AnyAsync(rr => rr.Id == repReqId && rr.UserInfoId == userInfoId);

            return repReqExists;
        }
    }
}
