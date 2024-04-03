using Microsoft.AspNetCore.Mvc;

namespace Bloggie.WEB.Controllers
{
	public class AdminTagsContoller : Controller
	{
		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
	}
}
