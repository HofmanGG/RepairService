using Moq;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_DAL.Entities;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AutoFixture;
using HelloSocNetw_BLL.Services;
using HelloSocNetw_NUnitTests.Config;

namespace HelloSocNetw_NUnitTests.BLLTests.ServicesTests
{
    public class UserInfoServiceTests
    {
        private readonly IFixture _fixture;

        public UserInfoServiceTests()
        {
            _fixture = new Fixture();
        }

        [Theory, BLLAutoMoqData]
        public async Task GetUserInfoByIdAsync_ValidIdIsGiven_ReturnObjWithTHeSameId(
            UserInfo userInfo,
            Mock<IUserInfoRepository> userInfoRepositoryMock,
            UserInfoService sut)
        {
            //arrange
            userInfoRepositoryMock.Setup(rep => rep.GetUserInfoByUserInfoIdAsync(It.IsAny<long>()))
                .ReturnsAsync(userInfo);

            //act
            var returnedUserInfo = await sut.GetUserInfoByUserInfoIdAsync(_fixture.Create<long>());

            //assert
            userInfo.Id.Should().Be(returnedUserInfo.Id);
        }
    }
}
