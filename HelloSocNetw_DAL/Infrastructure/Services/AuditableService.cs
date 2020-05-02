using System;
using System.Collections.Generic;
using System.Text;
using HelloSocNetw_DAL.Infrastructure.Attributes;

namespace HelloSocNetw_DAL.Infrastructure.Services
{
    public static class AuditableService
    {
        public static bool IsTypeAuditable(Type typeOfEntity)
        {
            var typeHasAuditableAttribute = typeOfEntity.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0;
            return typeHasAuditableAttribute;
        }
    }
}
