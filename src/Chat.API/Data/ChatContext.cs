using Chat.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.Data
{
    public class ChatContext: IdentityDbContext<User>
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options){}

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomMember> RoomMembers { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

    }
}
