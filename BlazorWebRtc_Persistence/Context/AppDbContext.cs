using BlazorWebRtc_Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<MessageRoom> MessageRooms { get; set; }



    }
}
