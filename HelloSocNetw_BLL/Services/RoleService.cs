using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Interfaces;

namespace HelloSocNetw_BLL.Services
{
    public class RoleService: IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
