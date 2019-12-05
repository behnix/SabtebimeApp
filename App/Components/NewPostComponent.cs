using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Components
{
    public class NewPostComponent : ViewComponent
    {
        private readonly IPostService _postService;

        public NewPostComponent(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_newPostComponent",await _postService.GetPublishedPosts(6)));
        }
    }
}
