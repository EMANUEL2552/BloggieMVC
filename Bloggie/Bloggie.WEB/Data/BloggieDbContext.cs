using Bloggie.WEB.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.WEB.Data
{
	public class BloggieDbContext: DbContext
	{
		public BloggieDbContext(DbContextOptions options): base(options) 
		{
		
		
		}

        public DbSet<BlogPost> BlogPosts { get; set; }

		public DbSet<Tag> Tags { get; set; }

	}
}
