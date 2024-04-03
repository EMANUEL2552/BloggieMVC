using Microsoft.AspNetCore.Mvc;

namespace Bloggie.WEB.Controllers
{
	public class HomeController1 : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
