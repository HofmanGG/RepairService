using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloSocNetw_DAL;
using HelloSocNetw_DAL.EFRepositories;
using HelloSocNetw_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_NUnitTests.DALTests.EFRepositoriesTests
{/*
    [TestFixture]
    class EFUserInfoRepositoryTests
    {
        private DbContextOptions<SocNetwContext> options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
        [TestCase]
        public void AddUserInfo_1UserInfo_CountIsOne()  
        {
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
            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo = new UserInfo() {Id = 1, FirstName = "Stepan", LastName = "Ivanov"};

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
 */
}
