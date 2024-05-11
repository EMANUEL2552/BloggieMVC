using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.WEB.Data
{
	public class AuthDbContext: IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
		{
		
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//seed roles (user, Admin, SuperAdmin)

			var adminRoleId = "ca2f6eb0-583a-443f-ac03-5f2976a93a38";
			var superAdminRoleId = "36533c9b-8b9e-4270-a44d-ba0b19e8b7b8";
			var userRoleId = "02260038-0b94-4fd9-af86-a8f81c9510c8";

			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name = "Admin",
					NormalizedName = "Admin",
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId
				},

				new IdentityRole
				{
				   Name = "SuperAdmin",
				   NormalizedName = "SuperAdmin",
				   Id = superAdminRoleId,
				   ConcurrencyStamp = superAdminRoleId
				},

				new IdentityRole
				{
				 Name = "User",
				 NormalizedName = "User",
				 Id =userRoleId,
				 ConcurrencyStamp = userRoleId
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);

			//seed superAdminuser
			var superAdminId = "cf37fda9-10e6-4fc9-ab32-fd89c958d57c";
			var superAdminUser = new IdentityUser
			{
				UserName = "superadmin@bloggie.com",
				Email = "superadmin@bloggie.com",
				NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
				NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
				Id = superAdminId
			};

			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
				.HashPassword(superAdminUser, "Superadmin@123");

			builder.Entity<IdentityUser>().HasData(superAdminUser);

			//add all roles to SuperAdminUser
			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
				  RoleId = adminRoleId,
				  UserId = superAdminId
				},

				new IdentityUserRole<string>
				{
				  RoleId = superAdminRoleId,
				  UserId = superAdminId
				},

				new IdentityUserRole<string>
				{
				  RoleId = userRoleId ,
				  UserId = superAdminId
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);	
		}
	}
}
