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
        public void AddUserInfo_AddOneUserInfoObjectIntoEmptyDB_UsersInfoCountIsOne()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "AddUserInfo")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);
                var userInfo = new UserInfo() { };

                userInfoRep.AddUserInfo(userInfo);
                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                Assert.AreEqual(1, context.UsersInfo.Count());
            }
        }

        [TestCase]
        public void AddUsersInfo_AddListOfThreeUsersInfoObjectIntoEmptyDB_UsersInfoCountIsThree()
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
        public async Task GetCountOfUsersInfoAsync_GetCountOfThreeUsersInfo_EqualsThree()
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
                
                userInfoRep.UpdateUserInfo(userInfoToChange);
                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var user = context.UsersInfo.Find(1);
                var changedName = user.LastName;
                Assert.AreEqual(changedName, "Glek");
            }
        }

        [TestCase]
        public async Task GetCountOfFriendsByUserIdAsync_GetCountFromDbWithThreeFriendTablesZ_ThreeReturned()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "GetCountOfFriendsByUserIdAsync")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo1 = new UserInfo() {UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov"};
                var userInfo2 = new UserInfo() {FirstName = "Volodimir", LastName = "Bulba"};
                var userInfo3 = new UserInfo() {FirstName = "Alex", LastName = "Brown"};
                var userInfo4 = new UserInfo() {FirstName = "Felix ", LastName = "Davis" };

                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo2 });
                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo3 });
                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo4 });

                context.UsersInfo.Add(userInfo1);
                context.UsersInfo.Add(userInfo2);
                context.UsersInfo.Add(userInfo3);
                context.UsersInfo.Add(userInfo4);

                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var friendsCount = await userInfoRep.GetCountOfFriendsByUserIdAsync(1);
                Assert.AreEqual(3, friendsCount);
            }
        }

        [TestCase]
        public async Task GetCountOfSubscribersByUserIdAsync_GetCountFromDbWithThreeSubscriberTables_ThreeReturned()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "GetCountOfSubscribersByUserIdAsync")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo1 = new UserInfo() { UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov" };
                var userInfo2 = new UserInfo() { FirstName = "Volodimir", LastName = "Bulba" };
                var userInfo3 = new UserInfo() { FirstName = "Alex", LastName = "Brown" };
                var userInfo4 = new UserInfo() { FirstName = "Felix ", LastName = "Davis" };

                context.Subscribers.Add(new SubscribersTable() { User = userInfo1, Subscriber = userInfo2 });
                context.Subscribers.Add(new SubscribersTable() { User = userInfo1, Subscriber = userInfo3 });
                context.Subscribers.Add(new SubscribersTable() { User = userInfo1, Subscriber = userInfo4 });

                context.UsersInfo.Add(userInfo1);
                context.UsersInfo.Add(userInfo2);
                context.UsersInfo.Add(userInfo3);
                context.UsersInfo.Add(userInfo4);

                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var subscribersCount = await userInfoRep.GetCountOfSubscribersByUserIdAsync(1);
                Assert.AreEqual(3, subscribersCount);
            }
        }

        [TestCase]
        public async Task AddFriendByUsersIdAndSubIdAsync_AddThreeFriendsAndGetCount_ThreeReturned()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "AddFriendByUsersIdAndSubIdAsync")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo1 = new UserInfo() { UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov" };
                var userInfo2 = new UserInfo() { FirstName = "Volodimir", LastName = "Bulba" };
                var userInfo3 = new UserInfo() { FirstName = "Alex", LastName = "Brown" };
                var userInfo4 = new UserInfo() { FirstName = "Felix ", LastName = "Davis" };

                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo2 });
                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo3 });
                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo4 });

                context.UsersInfo.Add(userInfo1);
                context.UsersInfo.Add(userInfo2);
                context.UsersInfo.Add(userInfo3);
                context.UsersInfo.Add(userInfo4);

                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var friendsCount = context.Friends.Count();
                Assert.AreEqual(3, friendsCount);
            }
        }

        [TestCase]
        public async Task AddSubscriberByUserIdAndSubIdAsync_AddThreeSubsAndGetCount_ThreeReturned()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "AddSubscriberByUserIdAndSubIdAsync")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo1 = new UserInfo() { UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov" };
                var userInfo2 = new UserInfo() { FirstName = "Volodimir", LastName = "Bulba" };
                var userInfo3 = new UserInfo() { FirstName = "Alex", LastName = "Brown" };
                var userInfo4 = new UserInfo() { FirstName = "Felix ", LastName = "Davis" };

                context.Subscribers.Add(new SubscribersTable() { User = userInfo1, Subscriber = userInfo2 });
                context.Subscribers.Add(new SubscribersTable() { User = userInfo1, Subscriber = userInfo3 });
                context.Subscribers.Add(new SubscribersTable() { User = userInfo1, Subscriber = userInfo4 });

                context.UsersInfo.Add(userInfo1);
                context.UsersInfo.Add(userInfo2);
                context.UsersInfo.Add(userInfo3);
                context.UsersInfo.Add(userInfo4);

                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var subscribersCount = context.Subscribers.Count();
                Assert.AreEqual(3, subscribersCount);
            }
        }

        [TestCase]
        public async Task DeleteSubscription_UnsubscribeUserFromUserWithOneSubAndGetCountOfSubs_ZeroReturned()
        {

            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "DeleteSubscription")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo1 = new UserInfo() { UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov" };
                var userInfo2 = new UserInfo() { UserInfoId = 2, FirstName = "Volodimir", LastName = "Bulba" };

                context.Subscribers.Add(new SubscribersTable() { UserId = userInfo1.UserInfoId, SubscriberId = userInfo2.UserInfoId });

                context.UsersInfo.Add(userInfo1);
                context.UsersInfo.Add(userInfo2);

                context.SaveChanges();

                await userInfoRep.DeleteSubscription(1, 2);

                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var subscribersCount = context.Subscribers.Count();

                Assert.AreEqual(0, subscribersCount);
            }
        }

        [TestCase]
        public async Task DeleteFriendship_DeleteFriendFromUserWithOneFriendAndGetCountOfFriends_ZeroReturned()
        {

            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "DeleteFriendship")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo1 = new UserInfo() { UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov" };
                var userInfo2 = new UserInfo() { UserInfoId = 2, FirstName = "Volodimir", LastName = "Bulba" };

                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo2 });

                context.UsersInfo.Add(userInfo1);
                context.UsersInfo.Add(userInfo2);

                context.SaveChanges();

                await userInfoRep.DeleteFriendship(1, 2);

                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var friendsCount = context.Friends.Count();

                Assert.AreEqual(0, friendsCount);
            }
        }

        [TestCase]
        public async Task GetFriendsByUserIdAsync_GetFriendsOfUserAndCheckIfUserContainsThem_TrueReturned()
        {
            var options = new DbContextOptionsBuilder<SocNetwContext>()
                .UseInMemoryDatabase(databaseName: "GetFriendsByUserIdAsync")
                .Options;

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);

                var userInfo1 = new UserInfo() { UserInfoId = 1, FirstName = "Stepan", LastName = "Ivanov" };
                var userInfo2 = new UserInfo() { UserInfoId = 2, FirstName = "Volodimir", LastName = "Bulba" };
                var userInfo3 = new UserInfo() { UserInfoId = 3, FirstName = "Alex", LastName = "Brown" };
                var userInfo4 = new UserInfo() { UserInfoId = 4, FirstName = "Felix ", LastName = "Davis" };

                context.UsersInfo.Add(userInfo1);
                context.UsersInfo.Add(userInfo2);
                context.UsersInfo.Add(userInfo3);
                context.UsersInfo.Add(userInfo4);

                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo2 });
                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo3 });
                context.Friends.Add(new FriendshipTable() { User = userInfo1, Friend = userInfo4 });

                context.SaveChanges();
            }

            using (var context = new SocNetwContext(options))
            {
                var userInfoRep = new EfUserInfoRepository(context);
                var userFriends = await userInfoRep.GetFriendsByUserIdAsync(1, 3);

                var userInfo2 = context.UsersInfo.Find(2);
                var userInfo3 = context.UsersInfo.Find(3);
                var userInfo4 = context.UsersInfo.Find(4);

                Assert.AreEqual(userInfo2, userFriends.FirstOrDefault(u => u.UserInfoId == 2));
                Assert.AreEqual(userInfo3, userFriends.FirstOrDefault(u => u.UserInfoId == 3));
                Assert.AreEqual(userInfo4, userFriends.FirstOrDefault(u => u.UserInfoId == 4));
            }
        }
    }
}
