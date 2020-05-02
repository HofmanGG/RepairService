using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Entities.IdentityEntities;
using HelloSocNetw_DAL.Infrastructure.Enums;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_DAL.Interfaces.Identity;

namespace HelloSocNetw_DAL
{
    public static class AppDbInitializer
    {
        private static IUnitOfWork _uow;
        private static IUserManager _userManager;
        private static IRoleManager _roleManager;

        public static async Task SeedAsync(
            IUnitOfWork uow,
            IUserManager userManager,
            IRoleManager roleManager)
        {
            _uow = uow;
            _userManager = userManager;
            _roleManager = roleManager;


            if (!await CountriesExist())
            {
                await CreateCountriesAsync();
            }

            if (!await RolesExist())
            {
                await CreateRolesAsync();
            }

            if (!await UserExists())
            {
                var user = await CreateUserAsync();
                await CreateRepairRequestsForUserInfo(user);
            }

            if (!await ManagerExists())
            {
                await CreateManagerAsync();
            }

            if (!await AdminExists())
            {
                await CreateAdminAsync();
            }
        }

        private static async Task<UserInfo> CreateUserAsync()
        {
                var userEmail = "user@gmail.com";
                var userPassword = "Qwe!23";
                var userIdentity = new AppIdentityUser { Email = userEmail, UserName = userEmail, EmailConfirmed = true };

                await _userManager.CreateAsync(userIdentity, userPassword);

                var userInfo = new UserInfo
                {
                    AppIdentityUser = userIdentity,
                    FirstName = "Illia",
                    LastName = "Samko",
                    DateOfBirth = new DateTime(2000, 5, 11),
                    Country = (await _uow.Countries.GetCountriesAsync()).First(),
                    Gender = DALEnums.DALGenderType.Male
                };

                _uow.UsersInfo.AddUserInfo(userInfo);
                await _uow.SaveChangesAsync();

                return userInfo;
        }

        private static async Task<UserInfo> CreateManagerAsync()
        {
            var managerEmail = "manager@gmail.com";
            var managerPassword = "Qwe!23";
            var managerIdentity = new AppIdentityUser
                {Email = managerEmail, UserName = managerEmail, EmailConfirmed = true};

            await _userManager.CreateAsync(managerIdentity, managerPassword);

            var managerInfo = new UserInfo
            {
                AppIdentityUser = managerIdentity,
                FirstName = "Illia",
                LastName = "Samko",
                DateOfBirth = new DateTime(2000, 5, 11),
                Country = (await _uow.Countries.GetCountriesAsync()).First(),
                Gender = DALEnums.DALGenderType.Male
            };

            _uow.UsersInfo.AddUserInfo(managerInfo);
            await _uow.SaveChangesAsync();

            await _userManager.AddToRoleAsync(managerIdentity, "Manager");

            return managerInfo;
        }

        private static async Task<UserInfo> CreateAdminAsync()
        {
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Qwe!23";
            var adminIdentity = new AppIdentityUser {Email = adminEmail, UserName = adminEmail, EmailConfirmed = true};
            await _userManager.CreateAsync(adminIdentity, adminPassword);

            var adminInfo = new UserInfo
            {
                AppIdentityUser = adminIdentity,
                FirstName = "Illia",
                LastName = "Samko",
                DateOfBirth = new DateTime(2000, 5, 11),
                Country = (await _uow.Countries.GetCountriesAsync()).First(),
                Gender = DALEnums.DALGenderType.Male
            };

            _uow.UsersInfo.AddUserInfo(adminInfo);
            await _uow.SaveChangesAsync();

            await _userManager.AddToRoleAsync(adminIdentity, "Manager");
            await _userManager.AddToRoleAsync(adminIdentity, "Admin");

            return adminInfo;
        }


        private static async Task CreateRolesAsync()
        {
            var roles = new List<AppRole>
            {
                new AppRole {Name = "User"},
                new AppRole {Name = "Manager"},
                new AppRole {Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await _roleManager.CreateRoleAsync(role);
            }
        }

        private static async Task CreateCountriesAsync()
        {
            var countries = new List<Country>
            {
                new Country {CountryName = "Italy"},
                new Country {CountryName = "Spain"},
                new Country {CountryName = "France"},
                new Country {CountryName = "Poland"},
                new Country {CountryName = "Russia"},
                new Country {CountryName = "Sweden"},
                new Country {CountryName = "Norway"},
                new Country {CountryName = "Greece"},
                new Country {CountryName = "Latvia"},
                new Country {CountryName = "Canada"},
                new Country {CountryName = "Belgium"},
                new Country {CountryName = "Ukraine"},
                new Country {CountryName = "Belarus"},
                new Country {CountryName = "Finland"},
                new Country {CountryName = "Denmark"},
                new Country {CountryName = "Moldova"},
                new Country {CountryName = "Georgia"},
                new Country {CountryName = "Germany"},
                new Country {CountryName = "Romania"},
                new Country {CountryName = "Estonia"},
                new Country {CountryName = "Portugal"},
                new Country {CountryName = "Bulgaria"},
                new Country {CountryName = "Slovakia"},
                new Country {CountryName = "Lithuania"},
                new Country {CountryName = "Kazakhstan"},
                new Country {CountryName = "Azerbaijan"},
                new Country {CountryName = "Luxembourg"},
                new Country {CountryName = "Netherlands"},
                new Country {CountryName = "Switzerland"},
                new Country {CountryName = "Turkmenistan"},
                new Country {CountryName = "Great Britain"},
                new Country {CountryName = "United States of America"}
            };

            foreach (var country in countries)
            {
                _uow.Countries.AddCountry(country);
            }

            await _uow.SaveChangesAsync();
        }


        private static async Task<bool> UserExists()
        {
            var userExists = await _userManager.FindByEmailAsync("user@gmail.com") != null;
            return userExists;
        }

        private static async Task<bool> ManagerExists()
        {
            var managerExists = await _userManager.FindByEmailAsync("manager@gmail.com") != null;
            return managerExists;
        }

        private static async Task<bool> AdminExists()
        {
            var adminExists = await _userManager.FindByEmailAsync("admin@gmail.com") != null;
            return adminExists;
        }

        private static async Task<bool> RolesExist()
        {
            var rolesExist = (await _roleManager.GetRolesAsync()).Count() != 0;
            return rolesExist;
        }

        private static async Task<bool> CountriesExist()
        {
            var countriesExist = await _uow.Countries.GetCountOfCountriesAsync() != 0;
            return countriesExist;
        }


        private static async Task CreateRepairRequestsForUserInfo(UserInfo userInfo)
        {
            var userHasRepairRequests = await _uow.RepairRequests.RepReqExistsByIdAsync(userInfo.Id);

            if (!userHasRepairRequests)
            {
                var repairRequests = new List<RepairRequest>
                    {
                        new RepairRequest
                        {
                            UserInfo = userInfo,
                            Comment = "Broken Screen",
                            ProductName = "IPhone 6",
                            RequestTime = new DateTime(2017, 5, 17),
                            RepairStatus = DALEnums.DALRepairStatusType.Done
                        },
                        new RepairRequest
                        {
                            UserInfo = userInfo,
                            Comment = "Broken Matrix",
                            ProductName = "Acer Aspire E 15",
                            RequestTime = new DateTime(2019, 9, 3),
                            RepairStatus = DALEnums.DALRepairStatusType.Done
                        },
                        new RepairRequest
                        {
                            UserInfo = userInfo,
                            Comment = "Not working",
                            ProductName = "DeepCool 600W (DE600 v2)",
                            RequestTime = new DateTime(2015, 11, 28),
                            RepairStatus = DALEnums.DALRepairStatusType.Closed
                        },
                        new RepairRequest
                        {
                            UserInfo = userInfo,
                            Comment = "Broken Screen",
                            ProductName = "IPhone 6",
                            RequestTime = new DateTime(2014, 6, 9),
                            RepairStatus = DALEnums.DALRepairStatusType.Closed
                        }
                    };

                foreach (var rr in repairRequests)
                {
                    _uow.RepairRequests.AddRepReq(rr);
                }

                await _uow.SaveChangesAsync();
            }
        }
    }
}