using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using BLL.ModelsDTO;
using FluentAssertions;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_NUnitTests.Config;
using HelloSocNetw_PL.Infrastructure.Interfaces;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PL.Controllers;
using Xunit;

namespace HelloSocNetw_NUnitTests.PLTests.ControllersTests
{
    public class AccountsControllerTests
    {
        private readonly IFixture fixture;

        public AccountsControllerTests()
        {
            fixture = new Fixture();
        }

        [Theory, PLAutoMoqData]
        public async Task SignIn_UserDoesNotExist_ReturnUnauthorizedStatusCode(
            LoginModel loginModel,
            [Frozen]Mock<IIdentityUserService> identityUserServiceMock,
            [Greedy]AccountsController sut)
        {
            //arrrange
            identityUserServiceMock.Setup(svc => svc.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((AppIdentityUserDTO)null);

            //act
            var response = await sut.SignIn(loginModel);

            // assert
            response.Result.Should().BeOfType<UnauthorizedResult>();
        }

        [Theory, PLAutoMoqData]
        public async Task SignIn_UserSuccessfullySignedIn_UserInfoModelReturned(
            LoginModel loginModel,
            AppIdentityUserDTO appIdentityUserDto,
            UserInfoDTO userInfoDto,
            [Frozen]Mock<IIdentityUserService> identityUserServiceMock,
            [Greedy]AccountsController sut)
        {
            //arrrange
            identityUserServiceMock.Setup(svc => svc.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(appIdentityUserDto);

            identityUserServiceMock.Setup(svc => svc.IsEmailConfirmedAsync(It.IsAny<AppIdentityUserDTO>()))
                .ReturnsAsync(true);

            identityUserServiceMock.Setup(svc => svc.IsLockedOutByEmailAsync(It.IsAny<AppIdentityUserDTO>()))
                .ReturnsAsync(false);

            identityUserServiceMock.Setup(svc => svc.GetUserInfoWithTokenAsync(It.IsAny<AppIdentityUserDTO>()))
                .ReturnsAsync(userInfoDto);

            //act
            var response = await sut.SignIn(loginModel);

            // assert
            response.Value.Should().NotBeNull();
        }

        [Theory, PLAutoMoqData]
        public async Task SignUp_UserSuccessfullyRegistered_ReturnNoContextStatusCode(
            RegisterModel registerModel,
            IUrlHelper urlHelper,
            HttpContext httpContext,
            [Frozen]Mock<IIdentityUserService> identityUserServiceMock,
            [Greedy] AccountsController sut)
        {
            //arrange
            identityUserServiceMock
                .Setup(svc => svc.CreateAccountAsync(It.IsAny<UserInfoDTO>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            sut.Url = urlHelper;
            sut.ControllerContext.HttpContext = httpContext;

            //act       
            var response = await sut.SignUp(registerModel);

            // assert
            identityUserServiceMock.Verify(ius =>
                ius.CreateAccountAsync(It.IsAny<UserInfoDTO>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), 
                Times.Once);

            response.Should().BeOfType<NoContentResult>();
        }
    }
}
