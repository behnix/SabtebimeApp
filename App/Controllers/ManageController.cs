using System.ComponentModel.DataAnnotations;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Authorize(nameof(ConstantPolicies.DynamicPermission))]
    public class ManageController : BaseController
    {
        [Display(Name = "داشبورد")]
        [HttpGet("dashboard", Name = "GetDashboard")]
        public IActionResult Index()
        {
            return View();
        }
    }
}