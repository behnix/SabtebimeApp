using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Services.Identity.Managers;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Components
{
    public class AdminNameComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly AppUserManager _userManager;
        private readonly AppRoleManager _roleManager;

        public AdminNameComponent(ApplicationDbContext context, AppUserManager userManager, AppRoleManager roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = new List<Domain.Identity.Role>();
            foreach (var roleName in await _userManager.GetRolesAsync(user))
            {
                roles.Add(await _roleManager.FindByNameAsync(roleName));
            }

            ViewData["Roles"] = roles;
            return await Task.FromResult((IViewComponentResult) View("_adminNameComponent",
                user));
        }
    }
}
