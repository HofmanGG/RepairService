using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PL.Controllers;

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
                    .Returns<UserInfoDTO>(uid => new UserInfoModel() { UserInfoId = uid.UserInfoId });

                /*fixture.Customize<AccountsController>(customization =>
                     customization
                         .With(ac => ac.Url, new Mock<IUrlHelper>() { DefaultValue = DefaultValue.Mock}.Object));*/

                return fixture;
            })
        { }
    }
}
