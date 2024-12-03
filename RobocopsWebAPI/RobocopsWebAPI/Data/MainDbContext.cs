using Microsoft.EntityFrameworkCore;
using RobocopsWebAPI.Models;

namespace RobocopsWebAPI.Data
{
	public class MainDbContext:DbContext
	{
		public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { 
		
		}
		public DbSet<UserProfile> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<FriendList> Friends { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<FriendRequest> FriendRequests { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Post>()
				.HasOne(p => p.UserProfile)
				.WithMany(u => u.Posts)
				.HasForeignKey(p => p.UserId);

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Post)
				.WithMany(p => p.Comments)
				.HasForeignKey(c => c.PostId);

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.UserProfile)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.UserId);

			modelBuilder.Entity<Like>()
				.HasOne(l => l.Post)
				.WithMany(p => p.Likes)
				.HasForeignKey(l => l.PostId);

			modelBuilder.Entity<Like>()
				.HasOne(l => l.UserProfile)
				.WithMany(u => u.Likes)
				.HasForeignKey(l => l.UserId);


			modelBuilder.Entity<FriendList>()
				.HasOne(f => f.UserProfile)
				.WithMany(u => u.FriendList)
				.HasForeignKey(f => f.UserId);

			modelBuilder.Entity<FriendRequest>()
				.HasOne(f => f.SenderUserProfile)
				.WithMany(u => u.SentFriendRequests)
				.HasForeignKey(f => f.UserId);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(f => f.ReceiverUserProfile)
                .WithMany(u => u.ReceivedFriendRequests)
                .HasForeignKey(f => f.ReceiverUserId);


        }

    }
}
