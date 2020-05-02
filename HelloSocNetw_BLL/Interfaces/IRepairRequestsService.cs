using HelloSocNetw_BLL.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IRepairRequestsService
    {
        Task<RepairRequestDTO> GetRepReqByIdAsync(long repReqId);

        Task<IEnumerable<RepairRequestDTO>> GetRepReqsByUserInfoIdAsync(long userInfoId);

        Task<IEnumerable<RepairRequestDTO>> GetRepReqsByUserInfoIdAndIdentityIdAsync(long userInfoId, Guid identityId);

        Task<RepairRequestDTO> GetRepReqByIdAndUserInfoIdAsync(long repReqId, long userInfoId);

        Task<long> GetCountOfRepReqsByUserInfoIdAsync(long userInfoId);

        Task AddRepReqAsync(RepairRequestDTO repReqDto);

        Task UpdateRepReqAsync(RepairRequestDTO repReqDto);

        Task DeleteRepReqByIdAndUserInfoIdAsync(long repReqId, long userInfoId);
    }
}
    