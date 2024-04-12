using Bloggie.WEB.Data;
using Bloggie.WEB.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.WEB.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
           await bloggieDbContext.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

       
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
          return await  bloggieDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
           
        }

        public Task<IEnumerable<BlogPost>> GetPostsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id ==  blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishDate = blogPost.PublishDate;
                existingBlog.Tags = blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        
    }
}
