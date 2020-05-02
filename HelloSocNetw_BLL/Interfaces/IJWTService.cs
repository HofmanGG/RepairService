using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities.IdentityEntities;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IJWTService
    {
        Task<string> GetJwtTokenAsync(AppIdentityUser appIdentityUser);
    }
}
