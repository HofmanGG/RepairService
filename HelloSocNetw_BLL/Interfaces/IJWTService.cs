using HelloSocNetw_DAL.Entities;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IJWTService
    {
        Task<string> GetJwtTokenAsync(AppIdentityUser appIdentityUser);
    }
}
