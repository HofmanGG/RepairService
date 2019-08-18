using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;

namespace HelloSocNetw_DAL
{
    public static class AppDbInitializer
    {
        public static void Seed(IUnitOfWork unitOfWork)
        {
            if (!unitOfWork.UserManager.Users.Any())
            {
                var countries = new HashSet<Country>()
                {
                    new Country() {CountryName = "Ukraine"},
                    new Country() {CountryName = "Russia"},
                    new Country() {CountryName = "Belarus"},
                    new Country() {CountryName = "Kazakhstan"},
                    new Country() {CountryName = "Turkmenistan"},
                    new Country() {CountryName = "Azerbaijan"},
                    new Country() {CountryName = "Moldova"},
                    new Country() {CountryName = "Georgia"},
                    new Country() {CountryName = "Poland"}
                };;

                unitOfWork.Countries.AddCountries(countries);
                unitOfWork.SaveChangesAsync();

                var user1 = new AppIdentityUser() {Email = "addDbInitiali45645645zer@mail.ru", UserName = "Hofman"};

                unitOfWork.UserManager.CreateAsync(user1, "qwSD12490()");

                var role = new AppUserRole() { Name = "Admin" };
                unitOfWork.RoleManager.CreateAsync(role);

                unitOfWork.UserManager.AddToRoleAsync(user1, role.Name);

                var userInfo = new UserInfo()
                {
                    FirstName = "Illia",
                    LastName = "Samko",
                    Country = countries.First(),
                    AppIdentityUser = user1,
                    DateOfBirth =  new DateTime(2000, 5, 11),
                    Gender = "Male"
                };

                unitOfWork.UsersInfo.AddUserInfo(userInfo);
                unitOfWork.SaveChangesAsync();

                user1.UserInfo = userInfo;
                unitOfWork.UserManager.UpdateAsync(user1);
            }
        }
    }
}