using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_DAL.UnitsOfWork
{
    public class IdentityUnitOfWork: IIdentityUnitOfWork
    {

        public IdentityUnitOfWork(
            UserManager<AppIdentityUser> userManager,
            RoleManager<AppRole> roleManager
            )
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        //custom identity managers
        public UserManager<AppIdentityUser> UserManager { get; }
        public RoleManager<AppRole> RoleManager { get; }
    }
}
