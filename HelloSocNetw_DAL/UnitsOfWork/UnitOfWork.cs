using System;
using System.Threading.Tasks;
using HelloSocNetw_DAL.EFRepositories;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Identity;
using HelloSocNetw_DAL.Infrastructure;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HelloSocNetw_DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocNetwContext _context;

        public UnitOfWork(
            SocNetwContext context
            )
        {
            _context = context;
        }

        //оставил set public для тестирования 
        private IUserInfoRepository _userInfoRepository;
        public IUserInfoRepository UsersInfo
        {
            get => _userInfoRepository ?? (_userInfoRepository = new EfUserInfoRepository(_context, new IncludesParser<UserInfo>()));
            set => UsersInfo = value;
        }

        private IRepairRequestRepository _repairRequestRepository;
        public IRepairRequestRepository RepairRequests
        {
            get => _repairRequestRepository ?? (_repairRequestRepository = new EfRepairRequestRepository(_context, new IncludesParser<RepairRequest>()));
            set => RepairRequests = value;
        }

        private ICountryRepository _countryRepository;
        public ICountryRepository Countries
        {
            get => _countryRepository ?? (_countryRepository = new EfCountryRepository(_context));
            set => Countries = value;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //не добавлял деструктор, потому что нету неуправляемых ресурсов
    }
}