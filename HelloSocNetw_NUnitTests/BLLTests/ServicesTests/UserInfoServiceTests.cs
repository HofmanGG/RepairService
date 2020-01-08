using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using HelloSocNetw_DAL.Interfaces;
using BLL.ModelsDTO;
using HelloSocNetw_DAL.Entities;
using System.Threading.Tasks;
using HelloSocNetw_DAL.UnitsOfWork;
using Microsoft.AspNetCore.Identity;
using HelloSocNetw_DAL;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BLL.Services;
using Xunit;
using FluentAssertions;

namespace HelloSocNetw_NUnitTests.BLLTests.Services
{
    public class UserInfoServiceTests
    {
        private readonly IMapper mapperObject;

        public UserInfoServiceTests()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<UserInfoDTO>(It.IsAny<UserInfo>()))
                .Returns(new UserInfoDTO());

            mapperMock.Setup(m => m.Map<UserInfo>(It.IsAny<UserInfoDTO>()))
                .Returns(new UserInfo());
        }

        [Fact]
        public async Task GetUserInfoByIdAsync_IdIs1_ReturnObjWithTHeSameId()
        {
            //arrange
            var trueObject = new UserInfo() { UserInfoId = 1 } ;

            var userInfoRepository = new Mock<IUserInfoRepository>();
            userInfoRepository.Setup(rep => rep.GetUserInfoByUserInfoIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserInfo() { UserInfoId =  1});

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uof => uof.UsersInfo)
                .Returns(userInfoRepository.Object);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserInfoDTO>(It.IsAny<UserInfo>()))
                .Returns<UserInfo>(u => new UserInfoDTO() { UserInfoId = u.UserInfoId });

            var userInfoService = new UserInfoService(unitOfWorkMock.Object, mapperMock.Object);

            //act
            var returnedObject = await userInfoService.GetUserInfoByUserInfoIdAsync(1);

            //assert
            trueObject.UserInfoId.Should().Be(returnedObject.UserInfoId);
        }
    }
}
