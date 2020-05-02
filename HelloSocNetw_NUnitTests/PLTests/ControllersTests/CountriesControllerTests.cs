using AutoMapper;
using FluentAssertions;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Controllers;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_PL.Models.CountryModels;
using Xunit;

namespace HelloSocNetw_NUnitTests.PLTests.ControllersTests
{
    public class CountriesControllerTests
    {
        [Fact]
        public async Task DeleteCountry_NotExistingObject_ReturnNotFoundStatusCode()
        {
            //arrange
            var countryServiceMock = new Mock<ICountryService>();
            countryServiceMock.Setup(svc => svc.GetCountryByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((CountryDTO)null);
            countryServiceMock.Setup(svc => svc.DeleteCountryByIdAsync(It.IsAny<int>()))
                .Verifiable();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<CountryModel>(It.IsAny<CountryDTO>()))
                .Returns<CountryDTO>(u => new CountryModel());

            var controller = new CountriesController(countryServiceMock.Object, null, mapperMock.Object, null);

            //act
            var response = await controller.DeleteCountry(123123123);

            // asser
            response.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteCountry_CountryDeleted_ReturnNotContentStatusCode()
        {
            //arrange
            var countryToDelete = new CountryDTO() { Id = 1 };

            var countryServiceMock = new Mock<ICountryService>();
            countryServiceMock.Setup(svc => svc.GetCountryByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(countryToDelete);
            countryServiceMock.Setup(svc => svc.DeleteCountryByIdAsync(It.IsAny<int>()))
                .Verifiable();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<CountryModel>(It.IsAny<CountryDTO>()))
                .Returns<CountryDTO>(u => new CountryModel() { Id = u.Id});

            var controller = new CountriesController(countryServiceMock.Object, null, mapperMock.Object, null);

            //act
            var response = await controller.DeleteCountry(123123123);

            // assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteCountry_DeletingGoesWrong_ReturnBadRequestCode()
        {
            //arrange
            var countryServiceMock = new Mock<ICountryService>();
            countryServiceMock.Setup(svc => svc.GetCountryByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CountryDTO());
            countryServiceMock.Setup(svc => svc.DeleteCountryByIdAsync(It.IsAny<int>()))
                .Verifiable();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<CountryModel>(It.IsAny<CountryDTO>()))
                .Returns<CountryDTO>(u => new CountryModel());

            var controller = new CountriesController(countryServiceMock.Object, null, mapperMock.Object, null);

            //act
            var response = await controller.DeleteCountry(123123123);

            // assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteCountry_ExceptionThrown_ReturnInternalServerErrorStatusCode()
        {
            //arrange
            var countryServiceMock = new Mock<ICountryService>();
            countryServiceMock.Setup(svc => svc.GetCountryByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new CountryDTO());
            countryServiceMock.Setup(svc => svc.DeleteCountryByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<CountryModel>(It.IsAny<CountryDTO>()))
                .Returns<CountryDTO>(u => new CountryModel());

            var controller = new CountriesController(countryServiceMock.Object, null, mapperMock.Object, null);

            //act
            var response = await controller.DeleteCountry(123123123);

            // assert
            response.Should().BeOfType<StatusCodeResult>();
        }

        [Fact]
        public async Task GetCountries_5Countries_ReturnCount5()
        {
            //arrange
            var countriesToReturn = new List<CountryDTO>
            {
                new CountryDTO {CountryName = "Ukraine"},
                new CountryDTO {CountryName = "Russia"},
                new CountryDTO {CountryName = "Belarus"},
                new CountryDTO {CountryName = "Kazakhstan"},
                new CountryDTO {CountryName = "Finland"},
            };
            
            var mappedCountries = new List<CountryModel>
            {
                new CountryModel {CountryName = "Ukraine"},
                new CountryModel {CountryName = "Russia"},
                new CountryModel {CountryName = "Belarus"},
                new CountryModel {CountryName = "Kazakhstan"},
                new CountryModel {CountryName = "Finland"},
            };

            var countryServiceMock = new Mock<ICountryService>();
            countryServiceMock.Setup(svc => svc.GetCountriesAsync())
                .ReturnsAsync(countriesToReturn);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<IEnumerable<CountryModel>>(It.IsAny<IEnumerable<CountryDTO>>()))
                .Returns(mappedCountries);

            var controller = new CountriesController(countryServiceMock.Object, null, mapperMock.Object, null);

            //act
            var response = await controller.GetCountries();

            // assert
            response.Value.Should().BeAssignableTo<IEnumerable<CountryModel>>();
            response.Value.Should().HaveCount(5);
        }
    }
}
