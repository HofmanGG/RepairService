using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_CrossCutting.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
