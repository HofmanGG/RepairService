using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloSocNetw_DAL;
using HelloSocNetw_DAL.EFRepositories;
using HelloSocNetw_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HelloSocNetw_NUnitTests.DALTests.EFRepositoriesTests
{
    [TestFixture]
    class EFUserInfoRepositoryTests
    {
        [TestCase]
        public void AddUserInfo_1UserInfo_CountIsOne()  
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "AddUserInfo")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);
                var userInfo = new UserInfo();

                userInfoRep.AddUserInfo(userInfo);
                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                Assert.AreEqual(1, context.UsersInfo.Count());
            }
        }

        [TestCase]
        public void AddUsersInfo_ListOf3UserInfo_CountIsThree()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "AddUsersInfo")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var users = new HashSet<UserInfo>()
                {
                    new UserInfo() {},
                    new UserInfo() {},
                    new UserInfo() {}
                };

                context.UsersInfo.AddRange(users);
                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                Assert.AreEqual(3, context.UsersInfo.Count());
            }
        }

        [TestCase]
        public async Task GetCountOfUsersInfoAsync_3UserInfoExist_Return3()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "GetCountOfUsersInfo")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var users = new HashSet<UserInfo>()
                {
                    new UserInfo() {},
                    new UserInfo() {},
                    new UserInfo() {}
                };

                context.UsersInfo.AddRange(users);
                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);
                var expectedCount = await userInfoRep.GetCountOfUsersInfoAsync();
                Assert.AreEqual(3, expectedCount);
            }
        }

        [TestCase]
        public async Task UpdateUserInfo_ChangeLastName_Changed()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "UpdateUserInfo")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo = new UserInfo() {UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov"};

                context.UsersInfo.Add(userInfo);

                context.SaveChanges();

                var userInfoToChange = context.UsersInfo.Find(1);
                userInfoToChange.LastName = "Glek";
                
                await userInfoRep.UpdateUserInfoAsync(userInfoToChange);
                await context.SaveChangesAsync();
            }

            using (var context = new SocNetwContext(options))
            {
                var user = context.UsersInfo.Find(1);
                var changedName = user.LastName;
                Assert.AreEqual(changedName, "Glek");
            }
        }
    }
}
