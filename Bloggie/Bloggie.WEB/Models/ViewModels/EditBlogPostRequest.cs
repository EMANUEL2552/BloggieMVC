﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.WEB.Models.ViewModels
{
	public class EditBlogPostRequest
	{
		public Guid Id { get; set; }

		public string Heading { get; set; }

		public string PageTitle { get; set; }

		public string Content { get; set; }

		public string ShortDescription { get; set; }

		public string FeaturedImageUrl { get; set; }

		public string UrlHandle { get; set; }

		public DateTime PublishDate { get; set; }

		public string Author { get; set; }

		public bool Visible { get; set; }

		//Display tags
		public IEnumerable<SelectListItem> Tags { get; set; }

		//colect Tag

		public string[] SelectedTags { get; set; } = Array.Empty<string>();

	}
}
