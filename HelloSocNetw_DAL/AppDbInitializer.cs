using System.Collections.Generic;
using System.Linq;
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
                var countries = new HashSet<Country>
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
                };

                var userInfo1 = new UserInfo()
                {
                    FirstName = "Dmitriy",
                    LastName = "Ivanov",
                    Gender = "Male", 
                    Country = countries.First()
                };

                var user1 = new AppIdentityUser() { Email = "user1@mail.ru", UserInfo = userInfo1};

                unitOfWork.Countries.AddCountries(countries);
                unitOfWork.UsersInfo.AddUserInfo(userInfo1);
                unitOfWork.UserManager.CreateAsync(user1);
                unitOfWork.SaveChangesAsync();
            }
        }
    }
}