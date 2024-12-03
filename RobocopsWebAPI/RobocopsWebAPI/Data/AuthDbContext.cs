using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RobocopsWebAPI.Data
{
	public class AuthDbContext:IdentityDbContext<IdentityUser>
	{
		public AuthDbContext (DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var adminRoleID = "2f5a6ee7-758d-49a4-bfaa-e93ab61f78f0";
			var userRoleID = "fb7a0d52-ed67-485a-8d3a-0739deacf388";

			var roles = new List<IdentityRole>
			{ new IdentityRole
			{
				Name="admin",
				NormalizedName = "ADMIN",
				Id = adminRoleID,
				ConcurrencyStamp = adminRoleID
			},

			new IdentityRole
			{

				Name="user",
				NormalizedName="USER",
				Id=userRoleID,
				ConcurrencyStamp = userRoleID
			}

			};
			builder.Entity<IdentityRole>().HasData(roles);


            var adminID = "07a6a7ab-7c03-4a37-8866-4e89661c3a71";

			var adminUser = new IdentityUser
			{

				UserName="essardinweberzo@gmail.com",
				NormalizedUserName ="essardinweberzo@gmail.com" .ToUpper(),
				Id = adminID,
				Email ="essardinweberzo@gmail.com",
				NormalizedEmail ="essardinweberzo@gmail.com"
			};

			adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser,"Admin@123");
			builder.Entity<IdentityUser>().HasData(adminUser);

			var adminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
					RoleId= adminRoleID,
					UserId= adminID
				},

				new IdentityUserRole<string>
				{
					RoleId=userRoleID,
					UserId = adminID
				}


			};

			builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);


		}
	}
}
