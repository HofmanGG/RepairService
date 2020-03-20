using Moq;
using HelloSocNetw_DAL.Interfaces;
using BLL.ModelsDTO;
using HelloSocNetw_DAL.Entities;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Services;
using Xunit;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Xunit2;
using HelloSocNetw_NUnitTests.BLLTests.ServicesTests;
using AutoFixture.Kernel;
using AutoFixture.AutoMoq;

namespace HelloSocNetw_NUnitTests.BLLTests.Services
{
    public class UserInfoServiceTests
    {
        private readonly IFixture fixture;

        public UserInfoServiceTests()
        {
            fixture = new Fixture();
        }

        [Theory, AutoMoqData]
        public async Task GetUserInfoByIdAsync_ValidIdIsGiven_ReturnObjWithTHeSameId(
            UserInfo userInfo,
            Mock<IUserInfoRepository> userInfoRepositoryMock,
            UserInfoService sut)
        {
            //arrange
            userInfoRepositoryMock.Setup(rep => rep.GetUserInfoByUserInfoIdAsync(It.IsAny<int>()))
                .ReturnsAsync(userInfo);

            //act
            var returnedObject = await sut.GetUserInfoByUserInfoIdAsync(fixture.Create<int>());

            //assert
            userInfo.UserInfoId.Should().Be(returnedObject.UserInfoId);
        }
    }
}
