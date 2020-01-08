using AutoMapper;
using BLL.ModelsDTO;
using FluentAssertions;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure.Interfaces;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PL.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HelloSocNetw_NUnitTests.PLTests.ControllersTests
{
    public class AccountsControllerTests
    {
        private ILogger<AccountsController> loggerObject = new Mock<ILogger<AccountsController>>().Object;
        private ICurrentUserService currentUserServiceObject = new Mock<ICurrentUserService>().Object;

        [Fact]
        public async Task SignIn_UserDoesNotExist_ReturnUnauthorizedStatusCode()
        {
            //arrrange
            var loginModel = new LoginModel() { Email = "validmail@gmail.com", Password = "VeEySecurePassWorD231#" };

            var identityUserService = new Mock<IIdentityUserService>();
            identityUserService.Setup(svc => svc.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((AppIdentityUserDTO)null);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var emailSenderMock = new Mock<IEmailSender>();

            var controller = new AccountsController(identityUserService.Object, emailSenderMock.Object, loggerObject, mapperMock.Object, currentUserServiceObject);

            //act
            var response = await controller.SignIn(loginModel);

            // assert
            response.Result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task SignIn_SuccessfulSignIn_ReturnOKStatusCode()
        {
            //arrrange
            var loginModel = new LoginModel() { Email = "validmail@gmail.com", Password = "VeEySecurePassWorD231#" };

            var identityUserService = new Mock<IIdentityUserService>();
            identityUserService.Setup(svc => svc.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new AppIdentityUserDTO());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var emailSenderMock = new Mock<IEmailSender>();

            var controller = new AccountsController(identityUserService.Object, emailSenderMock.Object, loggerObject, mapperMock.Object, currentUserServiceObject);

            //act
            var response = await controller.SignIn(loginModel);

            // assert
            response.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task SignIn_ExceptionThrown_ReturnInternalServerErrorStatusCode()
        {
            //arrrange
            var loginModel = new LoginModel() { Email = "validmail@gmail.com", Password = "VeEySecurePassWorD231#" };

            var identityUserService = new Mock<IIdentityUserService>();
            identityUserService.Setup(svc => svc.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new NullReferenceException());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                .Returns<UserInfoDTO>(u => new UserInfoModel());

            var emailSenderMock = new Mock<IEmailSender>();

            var controller = new AccountsController(identityUserService.Object, emailSenderMock.Object, loggerObject, mapperMock.Object, currentUserServiceObject);

            //act
            var response = await controller.SignIn(loginModel);

            // assert
            response.Result.Should().BeOfType<StatusCodeResult>();
        }

        [Fact]
        public async Task SignUp_UserWithSuckEmailExists_ReturnConflictStatusCode()
        {
            //arrrange
            var registerModel = new RegisterModel() { Email = "validmail@gmail.com", Password = "VeEySecurePassWorD231#" };

            var identityUserService = new Mock<IIdentityUserService>();
            identityUserService.Setup(svc => svc.UserWithSuchEmailAlreadyExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoDTO>(It.IsAny<RegisterModel>()))
                .Returns<RegisterModel>(u => new UserInfoDTO());

            var emailSenderMock = new Mock<IEmailSender>();

            var controller = new AccountsController(identityUserService.Object, emailSenderMock.Object, loggerObject, mapperMock.Object, currentUserServiceObject);

            //act
            var response = await controller.SignUp(registerModel);

            // assert
            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task SignUp_UserRegistered_ReturnOkStatusCode()
        {
            //arrrange
            var registerModel = new RegisterModel() { Email = "validmail@gmail.com", Password = "VeEySecurePassWorD231#" };

            var identityUserService = new Mock<IIdentityUserService>();
            identityUserService.Setup(svc => svc.UserWithSuchEmailAlreadyExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoDTO>(It.IsAny<RegisterModel>()))
                .Returns<RegisterModel>(u => new UserInfoDTO());

            var emailSenderMock = new Mock<IEmailSender>();

            var controller = new AccountsController(identityUserService.Object, emailSenderMock.Object, loggerObject, mapperMock.Object, null);

            //act
            var response = await controller.SignUp(registerModel);

            // assert
            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task SignUp_UserRegistered_ReturnInternalServerErrorStatusCode()
        {
            //arrrange
            var registerModel = new RegisterModel() { Email = "validmail@gmail.com", Password = "VeEySecurePassWorD231#" };

            var identityUserService = new Mock<IIdentityUserService>();
            identityUserService.Setup(svc => svc.UserWithSuchEmailAlreadyExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoDTO>(It.IsAny<RegisterModel>()))
                .Throws(new Exception());

            var emailSenderMock = new Mock<IEmailSender>();

            var controller = new AccountsController(identityUserService.Object, emailSenderMock.Object, loggerObject, mapperMock.Object, currentUserServiceObject);

            //act
            var response = await controller.SignUp(registerModel);

            // assert
            response.Should().BeOfType<StatusCodeResult>();
        }
    }
}
