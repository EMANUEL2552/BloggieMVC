using Bloggie.WEB.Models.Domain;
using Bloggie.WEB.Models.ViewModels;
using Bloggie.WEB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.WEB.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;   
        }

        [HttpGet]
        public async Task <IActionResult> Add()
        {
            // get tags from repository
           var tags =  await tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
           return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //map view model to domain model

            var blogpost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishDate = addBlogPostRequest.PublishDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,

            };

            // Map Tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            blogpost.Tags = selectedTags;

            await blogPostRepository.AddAsync(blogpost);

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            //call the repository
          var blogPosts = await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrieve the result from the repository
           var blogpost =  await blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagRepository.GetAllAsync();

            if (blogpost != null) 
            {

				// map the domain model into the view model
				var model = new EditBlogPostRequest
				{
					Id = blogpost.Id,
					Heading = blogpost.Heading,
					PageTitle = blogpost.PageTitle,
					Content = blogpost.Content,
					Author = blogpost.Author,
					FeaturedImageUrl = blogpost.FeaturedImageUrl,
					UrlHandle = blogpost.UrlHandle,
					ShortDescription = blogpost.ShortDescription,
					PublishDate = blogpost.PublishDate,
					Visible = blogpost.Visible,
					Tags = tagsDomainModel.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
					SelectedTags = blogpost.Tags.Select(x => x.Id.ToString()).ToArray()



				};

                return View(model);

			}

            

            //pass data to view
            return View(null);

        }
    }
}
