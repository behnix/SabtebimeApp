using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Components
{
    public class PostTagComponent : ViewComponent
    {
        private readonly IPostTagService _postTagService;

        public PostTagComponent(IPostTagService postTagService)
        {
            _postTagService = postTagService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_postTagComponent",await _postTagService.GetAllPostTags(10)));
        }
    }
}
