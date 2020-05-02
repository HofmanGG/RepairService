using System;
using HelloSocNetw_DAL.Interfaces.Services;

namespace HelloSocNetw_DAL.Infrastructure.Services
{
    public class DateService: IDateService
    {
        public DateTime DateNow => DateTime.Now;
    }
}
