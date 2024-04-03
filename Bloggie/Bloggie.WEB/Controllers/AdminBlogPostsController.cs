using Microsoft.AspNetCore.Mvc;

namespace Bloggie.WEB.Controllers
{
    public class AdminBlogPostsController : Controller
    {

        [HttpGet]
        public IActionResult Add()
        {
           return View();
        }
    }
}
