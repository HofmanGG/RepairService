using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PL.Controllers;
using System.Threading.Tasks;
using System;
using Xunit;
using FluentAssertions;

namespace HelloSocNetw_NUnitTests.PLTests.ControllersTests
{   
    public class UserInfoControllerTests
    {
        [Fact]
        public async Task GetUser_NotExistingId_ReturnNotFoundStatusCode()
        {
            //arrrange
            var userInfoServiceMock = new Mock<IUserInfoService>();
            userInfoServiceMock.Setup(rep => rep.GetUserInfoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((UserInfoDTO)null);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var controller = new UsersController(userInfoServiceMock.Object, mapperMock.Object);

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
            userInfoServiceMock.Setup(svc => svc.GetUserInfoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserInfoDTO());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var controller = new UsersController(userInfoServiceMock.Object, mapperMock.Object);

            //act
            var response = await controller.GetUser(123123123);

            // assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task DeleteUser_NotExistingId_ReturnNotFoundStatusCode()
        {
            //arrange
            var userInfoServiceMock = new Mock<IUserInfoService>();
            userInfoServiceMock.Setup(svc => svc.GetUserInfoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((UserInfoDTO)null);
            userInfoServiceMock.Setup(svc => svc.DeleteUserInfoByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var controller = new UsersController(userInfoServiceMock.Object, mapperMock.Object);

            //act
            var responseResult = await controller.DeleteUser(123123123);

            // assert
            responseResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteUser_DeletingGoesWrong_ReturnBadRequestCode()
        {
            //arrange
            var userInfoServiceMock = new Mock<IUserInfoService>();
            userInfoServiceMock.Setup(svc => svc.GetUserInfoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserInfoDTO());
            userInfoServiceMock.Setup(svc => svc.DeleteUserInfoByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var controller = new UsersController(userInfoServiceMock.Object, mapperMock.Object);

            //act
            var responseResult = await controller.DeleteUser(123123123);

            // assert
            responseResult.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteUser_ExceptionThrown_ReturnInternalServerErrorStatusCode()
        {
            //arrange
            var userInfoServiceMock = new Mock<IUserInfoService>();
            userInfoServiceMock.Setup(svc => svc.GetUserInfoByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserInfoDTO());
            userInfoServiceMock.Setup(svc => svc.DeleteUserInfoByUserIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var controller = new UsersController(userInfoServiceMock.Object, mapperMock.Object);

            //act
            var responseResult = await controller.DeleteUser(123123123);

            // assert
            responseResult.Should().BeOfType<StatusCodeResult>();
        }
    }
}