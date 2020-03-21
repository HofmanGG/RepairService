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
        private readonly IIncludesParserFactory _includesParserFactory;

        public UnitOfWork(
            SocNetwContext context,
            IIncludesParserFactory includesParserFactory
            )
        {
            _context = context;
            _includesParserFactory = includesParserFactory;
        }

        private IUserInfoRepository _userInfoRepository;
        public IUserInfoRepository UsersInfo
            => _userInfoRepository ?? (_userInfoRepository = new EfUserInfoRepository(_context, _includesParserFactory.GetIncludesParser<UserInfo>()));

        private IRepairRequestRepository _repairRequestRepository;
        public IRepairRequestRepository RepairRequests 
            => _repairRequestRepository ?? (_repairRequestRepository = new EfRepairRequestRepository(_context, _includesParserFactory.GetIncludesParser<RepairRequest>()));

        private ICountryRepository _countryRepository;
        public ICountryRepository Countries  
            => _countryRepository ?? (_countryRepository = new EfCountryRepository(_context));

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}