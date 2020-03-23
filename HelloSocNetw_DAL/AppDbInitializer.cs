using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static HelloSocNetw_DAL.Infrastructure.DALEnums;

namespace HelloSocNetw_DAL
{
    public static class AppDbInitializer
    {
        public async static Task SeedAsync(
            IUnitOfWork uow,
            UserManager<AppIdentityUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await uow.Countries.GetCountOfCountriesAsync() == 0)
            {
                var countries = new HashSet<Country>()
                {
                    new Country() {CountryName = "Italy"},
                    new Country() {CountryName = "Spain"},
                    new Country() {CountryName = "France"},
                    new Country() {CountryName = "Poland"},
                    new Country() {CountryName = "Russia"},
                    new Country() {CountryName = "Sweden"},
                    new Country() {CountryName = "Norway"},
                    new Country() {CountryName = "Greece"},
                    new Country() {CountryName = "Greece"},
                    new Country() {CountryName = "Latvia"},
                    new Country() {CountryName = "Canada"},
                    new Country() {CountryName = "Belgium"},
                    new Country() {CountryName = "Ukraine"},
                    new Country() {CountryName = "Belarus"},
                    new Country() {CountryName = "Finland"},
                    new Country() {CountryName = "Denmark"},
                    new Country() {CountryName = "Moldova"},
                    new Country() {CountryName = "Georgia"},
                    new Country() {CountryName = "Germany"},
                    new Country() {CountryName = "Romania"},
                    new Country() {CountryName = "Estonia"},
                    new Country() {CountryName = "Portugal"},
                    new Country() {CountryName = "Bulgaria"},
                    new Country() {CountryName = "Slovakia"},
                    new Country() {CountryName = "Lithuania"},
                    new Country() {CountryName = "Kazakhstan"},
                    new Country() {CountryName = "Azerbaijan"},
                    new Country() {CountryName = "Luxembourg"},
                    new Country() {CountryName = "Netherlands"},
                    new Country() {CountryName = "Switzerland"},
                    new Country() {CountryName = "Turkmenistan"},
                    new Country() {CountryName = "Great Britain"},
                    new Country() {CountryName = "United States of America"}
                };

                uow.Countries.AddCountries(countries);
                await uow.SaveChangesAsync();
            }

            if (!await roleManager.Roles.AnyAsync())
            {
                var roles = new HashSet<AppRole>()
                {
                    new AppRole() {Name = "User"},
                    new AppRole() {Name = "Manager"},
                    new AppRole() {Name = "Admin"}
                };

                foreach (var role in roles) {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!await userManager.Users.AnyAsync(u => u.Email == "admin@gmail.com"))
            {
                var adminEmail = "admin@gmail.com";
                var adminParol = "Qwe!23";
                var adminUser = new AppIdentityUser() { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(adminUser, adminParol);

                var adminInfo = new UserInfo()
                {
                    AppIdentityUser = adminUser,
                    FirstName = "Illia",
                    LastName = "Samko",
                    DateOfBirth = new DateTime(2000, 5, 11),
                    Country = (await uow.Countries.GetCountriesAsync()).First(),
                    Gender = DALGenderType.Male
                };

                uow.UsersInfo.AddUserInfo(adminInfo);
                await uow.SaveChangesAsync();

                await userManager.AddToRoleAsync(adminUser, "Manager");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            if (!await userManager.Users.AnyAsync(u => u.Email == "manager@gmail.com"))
            {
                var managerEmail = "manager@gmail.com";
                var amanagerParol = "Qwe!23";
                var managerUser = new AppIdentityUser() { Email = managerEmail, UserName = managerEmail, EmailConfirmed = true };
                await userManager.CreateAsync(managerUser, amanagerParol);

                var managerInfo = new UserInfo()
                {
                    AppIdentityUser = managerUser,
                    FirstName = "Illia",
                    LastName = "Samko",
                    DateOfBirth = new DateTime(2000, 5, 11),
                    Country = (await uow.Countries.GetCountriesAsync()).First(),
                    Gender = DALGenderType.Male
                };

                uow.UsersInfo.AddUserInfo(managerInfo);
                await uow.SaveChangesAsync();

                await userManager.AddToRoleAsync(managerUser, "Manager");
            }

            if (!await userManager.Users.AnyAsync(u => u.Email == "user@gmail.com"))
            {
                var userEmail = "user@gmail.com";
                var userParol = "Qwe!23";
                var userUser = new AppIdentityUser() { Email = userEmail, UserName = userEmail, EmailConfirmed = true };
                await userManager.CreateAsync(userUser, userParol);

                var userInfo = new UserInfo()
                {
                    AppIdentityUser = userUser,
                    FirstName = "Illia",
                    LastName = "Samko",
                    DateOfBirth = new DateTime(2000, 5, 11),
                    Country = (await uow.Countries.GetCountriesAsync()).First(),
                    Gender = DALGenderType.Male
                };

                uow.UsersInfo.AddUserInfo(userInfo);
                await uow.SaveChangesAsync();

                if(!await uow.RepairRequests.RepairRequestExistsAsync(userInfo.UserInfoId))
                {
                    var repairRequests = new HashSet<RepairRequest>()
                    {
                        new RepairRequest()
                        {
                            UserInfo = userInfo,
                            Email = userUser.Email,
                            Comment = "Broken Screen",
                            ProductName = "IPhone 6",
                            RequestTime = new DateTime(2017, 5, 17),
                            RepairStatus = DALRepairStatusType.Done
                        },
                        new RepairRequest()
                        {
                            UserInfo = userInfo,
                            Email = userUser.Email,
                            Comment = "Broken Matrix",
                            ProductName = "Acer Aspire E 15",
                            RequestTime = new DateTime(2019, 9, 3),
                            RepairStatus = DALRepairStatusType.Done
                        },
                        new RepairRequest()
                        {
                            UserInfo = userInfo,
                            Email = userUser.Email,
                            Comment = "Not working",
                            ProductName = "DeepCool 600W (DE600 v2)",
                            RequestTime = new DateTime(2015, 11, 28),
                            RepairStatus = DALRepairStatusType.Closed
                        },
                        new RepairRequest()
                        {
                            UserInfo = userInfo,
                            Email = userUser.Email,
                            Comment = "Broken Screen",
                            ProductName = "IPhone 6",
                            RequestTime = new DateTime(2014, 6, 9),
                            RepairStatus = DALRepairStatusType.Closed
                        }
                    };
                    foreach (var rr in repairRequests) {
                        uow.RepairRequests.AddRepairRequest(rr);
                    }
                    await uow.SaveChangesAsync();
                }
            }
        }
    }
}