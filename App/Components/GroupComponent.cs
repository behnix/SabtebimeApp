using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Components
{
    public class GroupComponent : ViewComponent
    {
        private readonly IGroupService _groupService;

        public GroupComponent(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_groupComponent", _groupService.GetAllGroup()));
        }
    }
}
