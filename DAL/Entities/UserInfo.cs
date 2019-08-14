using System;
using System.Collections.Generic;
using DAL.Entities;

namespace Entities
{
    public class UserInfo
    {
        public UserInfo()
        {
            Pictures = new HashSet<Picture>();
            Dialogs = new HashSet<Dialog>();
            Friends = new HashSet<FriendshipTable>();
            Subscribers = new HashSet<SubscribersTable>();
        }
        public int UserInfoId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual Picture ProfilePicture { get; set; } 
        public virtual ICollection<Picture> Pictures { get; private set; }

        public virtual ICollection<Dialog> Dialogs { get; private set; }

        public virtual ICollection<FriendshipTable> Friends { get; private set; }
        public virtual ICollection<SubscribersTable> Subscribers { get; private set; }

        public int AppIdentityUserId { get; set; }
        public virtual AppIdentityUser AppIdentityUser { get; set; }
    }
}