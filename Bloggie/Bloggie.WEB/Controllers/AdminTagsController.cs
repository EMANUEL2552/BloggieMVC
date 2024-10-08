﻿using Bloggie.WEB.Data;
using Bloggie.WEB.Models.Domain;
using Bloggie.WEB.Models.ViewModels;
using Bloggie.WEB.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.WEB.Controllers
{
	public class AdminTagsController : Controller
	{
		private readonly ITagRepository tagRepository;
		

        public AdminTagsController(ITagRepository tagRepository)
        {
           this.tagRepository = tagRepository;
        }

		[Authorize(Roles ="Admin")]
        [HttpGet]
		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Add")]
		public async Task <IActionResult> Add(AddTagRequest addTagRequest)
		{
			var tag = new Tag
			{
				Name = addTagRequest.Name,
				DisplayName = addTagRequest.DisplayName,
			};

			 await  tagRepository.AddAsync(tag);	

			return RedirectToAction("List");
		}

		[HttpGet]
		[ActionName("List")]
		public async Task<IActionResult> List()
		{
			// use dbContext to read the tags
			var tags =  await tagRepository.GetAllAsync();

			return View(tags);	
		}

		[HttpGet]
		public async Task <IActionResult> Edit(Guid id) 

		{
			var tag = await tagRepository.GetAsync(id);

			if (tag != null) 
			{
				var editTagRequest = new EditTagRequest
				{
					Id = tag.Id,
					Name = tag.Name,
					DisplayName = tag.DisplayName,
				};

				return View(editTagRequest);
			}

			return View(null);
		}

		[HttpPost]
		public async Task <IActionResult> Edit(EditTagRequest editTagRequest)
		{
			var tag = new Tag
			{
				Id = editTagRequest.Id,
				Name = editTagRequest.Name,
				DisplayName = editTagRequest.DisplayName,

			};

			var updatedTag = await tagRepository.UpdateAsync(tag);

			if (updatedTag != null) 
			{
			
			
			}

			else
			{

			}

			return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }

		[HttpPost]
		public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
		{
			var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

			if (deletedTag != null) 
			{
				//show success notification

				return RedirectToAction("List");
			
			}

			//show an error notification
			return RedirectToAction("Edit", new {id = editTagRequest.Id});
		
		}
	}
}
