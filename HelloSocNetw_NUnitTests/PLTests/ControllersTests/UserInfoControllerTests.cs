using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using HelloSocNetw_PL.Controllers;
using HelloSocNetw_PL.Models.UserInfoModels;

namespace HelloSocNetw_NUnitTests.PLTests.ControllersTests
{
    public class UserInfoControllerTests
    {
        [Fact]
        public async Task GetUser_NotExistingId_ReturnNotFoundStatusCode()
        {
            //arrange
            var userInfoServiceMock = new Mock<IUserInfoService>();
            userInfoServiceMock.Setup(rep => rep.GetUserInfoByUserInfoIdAsync(It.IsAny<int>()))
                .ReturnsAsync((UserInfoDTO)null);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var controller = new UsersController(userInfoServiceMock.Object, null, mapperMock.Object, null);

            //act
            var response = await controller.GetUser(123123123);

            // assert
            response.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetUser_ExistingId_ReturnOkStatusCode()
        {
            //arrange
            var userInfoServiceMock = new Mock<IUserInfoService>();
            userInfoServiceMock.Setup(svc => svc.GetUserInfoByUserInfoIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserInfoDTO());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var controller = new UsersController(userInfoServiceMock.Object, null, mapperMock.Object, null);

            //act
            var response = await controller.GetUser(123123123);

            // assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}