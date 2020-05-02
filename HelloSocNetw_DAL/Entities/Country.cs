using HelloSocNetw_DAL.Infrastructure.Attributes;
using HelloSocNetw_DAL.Interfaces;

namespace HelloSocNetw_DAL.Entities
{
    [Auditable]
    public class Country: IEntity
    {
        public long Id { get; set; }

        public string CountryName { get; set; }
    }
}