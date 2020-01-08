using HelloSocNetw_CrossCutting.Interfaces;
using System;

namespace HelloSocNetw_CrossCutting.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
