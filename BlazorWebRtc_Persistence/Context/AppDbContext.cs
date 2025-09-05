using BlazorWebRtc_Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<MessageRoom> MessageRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>()
                .HasOne(i => i.SenderUser)
                .WithMany()
                .HasForeignKey(i => i.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(i => i.ReceiverUser)
                .WithMany()
                .HasForeignKey(i => i.ReceiverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFriend>()
                .HasOne(i => i.Requester)
                .WithMany()
                .HasForeignKey(i => i.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFriend>()
                .HasOne(i => i.ReceiverUser)
                .WithMany()
                .HasForeignKey(i => i.ReceiverUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageRoom>()
                .HasOne(i => i.SenderUser)
                .WithMany()
                .HasForeignKey(i => i.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageRoom>()
                .HasOne(i => i.ReceiverUser)
                .WithMany()
                .HasForeignKey(i => i.ReceiverUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
