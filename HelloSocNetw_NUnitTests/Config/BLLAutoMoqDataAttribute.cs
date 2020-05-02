using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Moq;

namespace HelloSocNetw_NUnitTests.Config
{
    public class BLLAutoMoqDataAttribute : AutoDataAttribute
    {
        public BLLAutoMoqDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture()
                    .Customize(new AutoMoqCustomization());

                fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                fixture.Freeze<Mock<IMapper>>()
                   .Setup(m => m.Map<UserInfoDTO>(It.IsAny<UserInfo>()))
                   .Returns<UserInfo>(ui => new UserInfoDTO() { Id = ui.Id });

                fixture.Freeze<Mock<IUserInfoRepository>>();

                fixture.Freeze<Mock<IUnitOfWork>>()
                   .Setup(uof => uof.UsersInfo)
                   .Returns(fixture.Create<IUserInfoRepository>());

                return fixture;
            })
        { }
    }
}
    


    


    

