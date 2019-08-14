namespace HelloSocNetw_DAL.Entities
{
    public class FriendshipTable
    {
        public int UserId { get; set; }
        public virtual UserInfo User { get; set; }

        public int FriendId { get; set; }
        public virtual UserInfo Friend { get; set; }
    }
}