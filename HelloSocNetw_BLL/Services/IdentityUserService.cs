using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Interfaces;

namespace HelloSocNetw_BLL.Services
{
    public class IdentityUserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IdentityUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}