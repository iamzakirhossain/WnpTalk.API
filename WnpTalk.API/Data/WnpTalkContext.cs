using Microsoft.EntityFrameworkCore;
using WnpTalk.API.Entities;

namespace WnpTalk.API.Data
{
    public class WnpTalkContext : DbContext
    {
        public WnpTalkContext(DbContextOptions<WnpTalkContext> options) : base(options)
        {

        }

        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblUserFriend> TblUserFriends { get; set; } = null!;
        public virtual DbSet<TblMessage> TblMessages { get; set; } = null!;

        public virtual DbSet<TblChatRoom> TblChatRooms { get; set; } = null!;
        public virtual DbSet<TblChatRoomMessage> TblChatRoomMessages { get; set; } = null!;

    }
}
