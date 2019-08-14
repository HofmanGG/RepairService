using System.Collections.Generic;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
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