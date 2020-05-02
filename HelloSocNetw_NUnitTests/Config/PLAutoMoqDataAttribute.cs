using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.UserInfoModels;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HelloSocNetw_NUnitTests.Config
{
    public class PLAutoMoqDataAttribute : AutoDataAttribute
    {
        public PLAutoMoqDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture()
                    .Customize(new AutoMoqCustomization());

                fixture.Freeze<Mock<IMapper>>();
                var mapperMock = fixture.Create<Mock<IMapper>>();

                mapperMock.Setup(m => m.Map<UserInfoDTO>(It.IsAny<RegisterModel>()))
                    .Returns<RegisterModel>(rm => new UserInfoDTO());

                mapperMock.Setup(m => m.Map<UserInfoModel>(It.IsAny<UserInfoDTO>()))
                    .Returns<UserInfoDTO>(uid => new UserInfoModel() { Id = uid.Id });

                fixture.Freeze<Mock<HttpContext>>();
                var httpContextMock = fixture.Create<Mock<HttpContext>>();

                httpContextMock.Setup(hc => hc.Request)
                    .Returns(fixture.Create<HttpRequest>());

                httpContextMock.DefaultValue = DefaultValue.Empty;

                return fixture;
            })
        { }
    }
}
