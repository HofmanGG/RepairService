using System.Threading.Tasks;
using HelloSocNetw_DAL.EFRepositories;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_DAL.Interfaces.Repositories;
using HelloSocNetw_DAL.Interfaces.Services;

namespace HelloSocNetw_DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocNetwContext _context;
        private readonly IIncludesParser _includesParser;

        public UnitOfWork(
            SocNetwContext context,
            IIncludesParser includesParser
            )
        {
            _context = context;
            _includesParser = includesParser;
        }

        private IUserInfoRepository _userInfoRepository;
        public IUserInfoRepository UsersInfo
            => _userInfoRepository ??= new EfUserInfoRepository(_context, _includesParser);

        private IRepairRequestRepository _repairRequestRepository;
        public IRepairRequestRepository RepairRequests
            => _repairRequestRepository ??= new EfRepairRequestRepository(_context, _includesParser);

        private ICountryRepository _countryRepository;
        public ICountryRepository Countries
            => _countryRepository ??= new EfCountryRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}