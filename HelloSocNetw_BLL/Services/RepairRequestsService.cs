using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HelloSocNetw_DAL.Infrastructure.DALEnums;

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

        public async Task<RepairRequestDTO> GetRepairRequestByRepairRequestIdAsync(int repairRequestId)
        {
            var repairRequest = await _uow.RepairRequests.GetRepairRequestByRepairRequestIdAsync(repairRequestId);
            var repairRequestDto = _mpr.Map<RepairRequestDTO>(repairRequest);
            return repairRequestDto;
        }

        public async Task<IEnumerable<RepairRequestDTO>> GetRepairRequestsByUserInfoIdAsync(int userInfoId)
        {
            var repairRequests = await _uow.RepairRequests.GetRepairRequestsByUserInfoIdAsync(userInfoId);
            var repairRequestsDto = _mpr.Map<IEnumerable<RepairRequestDTO>>(repairRequests);
            return repairRequestsDto;
        }

        public async Task<IEnumerable<RepairRequestDTO>> GetRepairRequestsByUserInfoIdAndAppIdentityUserIdAsync(int userInfoId, Guid appIdentityUserId)
        {
            var repairRequest = await _uow.RepairRequests
                .GetRepairRequestsAsync(rr => rr.UserInfoId == userInfoId && rr.UserInfo.AppIdentityUserId == appIdentityUserId);
            var repairRequestDto = _mpr.Map<IEnumerable<RepairRequestDTO>>(repairRequest);
            return repairRequestDto;
        }

        public async Task<RepairRequestDTO> GetRepairRequestByRepairRequestIdAndUserInfoIdAsync(int repairRequestId, int userInfoId)
        {
            var repairRequest = await _uow.RepairRequests
                .GetRepairRequestAsync(rr => rr.RepairRequestId == repairRequestId && rr.UserInfo.UserInfoId == userInfoId);
            var repairRequestDto = _mpr.Map<RepairRequestDTO>(repairRequest);
            return repairRequestDto;
        }

        public async Task<int> GetCountOfRepairRequestsByUserInfoIdAsync(int userInfoId)
        {
            var count = await _uow.RepairRequests.GetCountOfRepairRequestsByUserInfoIdAsync(userInfoId);
            return count;
        }

        public async Task<bool> AddRepairRequestAsync(RepairRequestDTO repairRequestDto)
        {
            var repairRequest = _mpr.Map<RepairRequest>(repairRequestDto);
            repairRequest.RequestTime = DateTime.Now;

            var emailAnon = await _uow.UsersInfo.GetAsync(u => u.UserInfoId == repairRequestDto.UserInfoId, u => new { u.AppIdentityUser.Email });
            repairRequest.Email = emailAnon.Email;

            _uow.RepairRequests.AddRepairRequest(repairRequest);
            var rowsAffected = await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<bool> UpdateRepairRequestAsync(RepairRequestDTO newRepairRequestInfoDto)
        {
            var repairRequestToChange = await _uow.RepairRequests.GetRepairRequestByRepairRequestIdAsync(newRepairRequestInfoDto.RepairRequestId);

            repairRequestToChange.RepairStatus = (DALRepairStatusType)(int)newRepairRequestInfoDto.RepairStatus;
            repairRequestToChange.ProductName = newRepairRequestInfoDto.ProductName;
            repairRequestToChange.Comment = newRepairRequestInfoDto.Comment;

            _uow.RepairRequests.UpdateRepairRequestAsync(repairRequestToChange);
            var rowsAffected = await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<bool> DeleteRepairRequestByRepairRequestIdAsync(int repairRequestId)
        {
            await _uow.RepairRequests.DeleteRepairRequestByRepairRequestIdAsync(repairRequestId);
            var rowsAffected =  await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<bool> RepairRequestExistsAsync(int repairRequestId)
        {
            return await _uow.RepairRequests.RepairRequestExistsAsync(repairRequestId);
        }

        public async Task<bool> RepairRequestWithSuchRepairRequestIdAndUserInfoIdExistsAsync(int repairRequestId, int userInfoId)
        {
            return await _uow.RepairRequests
                .RepairRequestExistsAsync(rr => rr.RepairRequestId == repairRequestId && rr.UserInfoId == userInfoId);
        }
    }
}
