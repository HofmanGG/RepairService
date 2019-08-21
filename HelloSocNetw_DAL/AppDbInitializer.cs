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
            if (unitOfWork.Countries.GetCountOfCountriesAsync().Result != 0)
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
            }
        }
    }
}