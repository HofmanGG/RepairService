using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_NUnitTests.BLLTests.ServicesTests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture()
                    .Customize(new AutoMoqCustomization());

                fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                fixture.Freeze<Mock<IMapper>>()
                   .Setup(m => m.Map<UserInfoDTO>(It.IsAny<UserInfo>()))
                   .Returns<UserInfo>(ui => new UserInfoDTO() { UserInfoId = ui.UserInfoId });

                fixture.Freeze<Mock<IUserInfoRepository>>();

                fixture.Freeze<Mock<IUnitOfWork>>()
                   .Setup(uof => uof.UsersInfo)
                   .Returns(fixture.Create<IUserInfoRepository>());

                return fixture;
            })
        { }
    }
}
    


    


    

