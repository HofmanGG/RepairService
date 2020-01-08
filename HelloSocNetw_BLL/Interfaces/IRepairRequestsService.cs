using HelloSocNetw_BLL.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IRepairRequestsService
    {
        Task<RepairRequestDTO> GetRepairRequestByRepairRequestIdAsync(int id);

        Task<IEnumerable<RepairRequestDTO>> GetRepairRequestsByUserInfoIdAsync(int id);

        Task<IEnumerable<RepairRequestDTO>> GetRepairRequestsByUserInfoIdAndAppIdentityUserIdAsync(int userInfoId, Guid appIdentityUserId);

        Task<RepairRequestDTO> GetRepairRequestByRepairRequestIdAndUserInfoIdAsync(int repairRequestId, int userInfoId);

        Task<int> GetCountOfRepairRequestsByUserInfoIdAsync(int id);

        Task AddRepairRequestAsync(RepairRequestDTO repairRequest);

        Task UpdateRepairRequestAsync(RepairRequestDTO newRepairRequestInfoDto);

        Task DeleteRepairRequestByRepairRequestIdAsync(int repairRequestId);

        Task<bool> RepairRequestExistsAsync(int id);

        Task<bool> RepairRequestWithSuchRepairRequestIdAndUserInfoIdExistsAsync(int repairRequestId, int userInfoId);
    }
}
