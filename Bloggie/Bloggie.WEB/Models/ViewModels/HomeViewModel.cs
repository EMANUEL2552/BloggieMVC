using Bloggie.WEB.Models.Domain;

namespace Bloggie.WEB.Models.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<BlogPost> BlogPosts { get; set; }

		public IEnumerable<Tag> Tags { get; set; }

	}
}
