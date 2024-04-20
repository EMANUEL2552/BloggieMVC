using Bloggie.WEB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.WEB.Controllers
{
	public class BlogsController : Controller
	{
		private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
		public async Task<IActionResult> Index(string urlHandle)
		{
			var blogPost = await blogPostRepository.GetByUrlHandle(urlHandle);

			return View(blogPost);
		}
	}
}
