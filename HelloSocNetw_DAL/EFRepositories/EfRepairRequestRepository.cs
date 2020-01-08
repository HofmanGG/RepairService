using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure;
using HelloSocNetw_DAL.Infrastructure.Exceptions;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfRepairRequestRepository: IRepairRequestRepository
    {
        private readonly SocNetwContext _context;
        private readonly IIncludesParser<RepairRequest> _includesParser;

        public EfRepairRequestRepository(DbContext dbContext, IIncludesParser<RepairRequest> includesParser)
        {
            _context = dbContext as SocNetwContext;
            _includesParser = includesParser;
        }

        public async Task<RepairRequest> GetRepairRequestByRepairRequestIdAsync(int repairRequestId)
        {
            var repairRequest = await _context.RepairRequests.FindAsync(repairRequestId);
            return repairRequest;
        }

        public async Task<RepairRequest> GetRepairRequestAsync(Expression<Func<RepairRequest, bool>> filter, string includeProperties = "")
        {
            var query = _context.RepairRequests.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            _includesParser.AddIncludes(query, includeProperties);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RepairRequest>> GetRepairRequestsAsync(
            Expression<Func<RepairRequest, bool>> filter = null,
            Func<IQueryable<RepairRequest>, IOrderedQueryable<RepairRequest>> orderBy = null,
            string includeProperties = "")
        {
            var query = _context.RepairRequests.AsQueryable();

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
            return await _context.RepairRequests.Where(where).Select(select).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RepairRequest>> GetRepairRequestsByUserInfoIdAsync(int userInfoId)
        {
            var repairRequests = await _context.RepairRequests.Where(r => r.UserInfoId == userInfoId).ToListAsync();
            return repairRequests;
        }

        public async Task<int> GetCountOfRepairRequestsByUserInfoIdAsync(int userInfoId)
        {
           var count = await _context.RepairRequests.Where(r => r.UserInfoId == userInfoId).CountAsync();
           return count;
        }

        public void AddRepairRequest(RepairRequest repairRequestToAdd)
        {
            _context.RepairRequests.Add(repairRequestToAdd);
        }

        public void UpdateRepairRequestAsync(RepairRequest repairRequestToChange)
        {
            _context.RepairRequests.Update(repairRequestToChange);
        }

        public async Task DeleteRepairRequestByRepairRequestIdAsync(int repairRequestId)
        {
            var repairRequestToDelete = await _context.RepairRequests.FindAsync(repairRequestId);
            if (repairRequestToDelete == null)
                throw new NotFoundException(nameof(RepairRequest), repairRequestId);

            _context.RepairRequests.Remove(repairRequestToDelete);
        }
            
        public async Task<bool> RepairRequestExistsAsync(int repairRequestId)
        {
            return await _context.RepairRequests.AnyAsync(r => r.RepairRequestId == repairRequestId);
        }

        public async Task<bool> RepairRequestExistsAsync(Expression<Func<RepairRequest, bool>> where)
        {
            return await _context.RepairRequests.AnyAsync(where);
        }
    }
}
