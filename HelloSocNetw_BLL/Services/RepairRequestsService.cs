using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_BLL.Infrastructure.Exceptions;

namespace HelloSocNetw_BLL.Services
{
    public class RepairRequestsService : IRepairRequestsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mpr;

        public RepairRequestsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mpr = mapper;
        }

        public async Task<RepairRequestDTO> GetRepReqByIdAsync(long repReqId)
        {
            var repReq = await _uow.RepairRequests.GetRepReqByIdAsync(repReqId);
            var repReqDto = _mpr.Map<RepairRequestDTO>(repReq);
            return repReqDto;
        }

        public async Task<IEnumerable<RepairRequestDTO>> GetRepReqsByUserInfoIdAsync(long userInfoId)
        {
            var repReq = await _uow.RepairRequests.GetRepReqsByUserInfoIdAsync(userInfoId);
            var repReqDto = _mpr.Map<IEnumerable<RepairRequestDTO>>(repReq);
            return repReqDto;
        }

        public async Task<IEnumerable<RepairRequestDTO>> GetRepReqsByUserInfoIdAndIdentityIdAsync(long userInfoId, Guid identityId)
        {
            var repReq = await _uow.RepairRequests
                .GetRepReqsAsync(rr => rr.UserInfoId == userInfoId && rr.UserInfo.AppIdentityUserId == identityId);
            var repReqDto = _mpr.Map<IEnumerable<RepairRequestDTO>>(repReq);
            return repReqDto;
        }

        public async Task<RepairRequestDTO> GetRepReqByIdAndUserInfoIdAsync(long repReqId, long userInfoId)
        {
            var repReq = await _uow.RepairRequests
                .GetRepReqAsync(rr => rr.Id == repReqId && rr.UserInfo.Id == userInfoId);
            var repReqDto = _mpr.Map<RepairRequestDTO>(repReq);
            return repReqDto;
        }

        public async Task<long> GetCountOfRepReqsByUserInfoIdAsync(long userInfoId)
        {
            var count = await _uow.RepairRequests.GetCountOfRepReqsByUserInfoIdAsync(userInfoId);
            return count;
        }

        public async Task AddRepReqAsync(RepairRequestDTO repReqDto)
        {
            var repReq = _mpr.Map<RepairRequest>(repReqDto);
            repReq.RequestTime = DateTime.Now;

            _uow.RepairRequests.AddRepReq(repReq);

            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Repair request deleting went wrong");
        }

        public async Task UpdateRepReqAsync(RepairRequestDTO repReqDto)
        {
            await ThrowIfSuchRepReqExistsAsync(repReqDto.Id, repReqDto.UserInfoId);

            var repReq = _mpr.Map<RepairRequest>(repReqDto);

            await _uow.RepairRequests.UpdateRepReqAsync(repReq);
             
            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Repair request updating went wrong");
        }

        private async Task ThrowIfSuchRepReqExistsAsync(long repReqId, long userInfoId)
        {   
            var repReqExists = await _uow.RepairRequests.RepReqExistsByIdAndUserInfoIdAsync(repReqId, userInfoId);
            if (!repReqExists)
                throw new NotFoundException(nameof(RepairRequest), repReqId);
        }

        public async Task DeleteRepReqByIdAndUserInfoIdAsync(long repReqId, long userInfoId)
        {
            await ThrowIfSuchRepReqExistsAsync(repReqId, userInfoId);

            await _uow.RepairRequests.DeleteRepReqByIdAsync(repReqId);

            var rowsAffected =  await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Repair request deleting went wrong");   
        }
    }
}
