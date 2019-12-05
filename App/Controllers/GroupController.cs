using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using App.Domain.Entities.Post;
using App.Services;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("group")]
    [Authorize(nameof(ConstantPolicies.DynamicPermission))]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [Display(Name = "گروه ها")]
        [HttpGet("list", Name = "GetGroups")]
        public IActionResult Index()
        {
            var group = _groupService.GetAllGroup();

            return View(group);
        }

        [Display(Name = "افزودن گروه جدید")]
        [HttpGet("create", Name = "GetCreateGroup")]
        public IActionResult Create()
        {
            return View();
        }

        [Display(Name = "افزودن گروه جدید")]
        [HttpPost("create", Name = "PostCreateGroup")]
        public async Task<IActionResult> Create(Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            await _groupService.CreateGroup(group);

            return RedirectToRoute("GetGroups");
        }

        [Display(Name = "ویرایش گروه")]
        [HttpGet("edit", Name = "GetEditGroup")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _groupService.GetGroupByGroupId(id);

            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [Display(Name = "ویرایش گروه")]
        [HttpPost("edit", Name = "PostEditGroup")]
        public async Task<IActionResult> Edit(Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            _groupService.EditGroup(group);

            try
            {
                await _groupService.SaveChangeAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group.GroupId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToRoute("GetGroups");
        }
        
        [Display(Name = "حذف گروه")]
        [HttpGet("remove", Name = "GetRemoveGroup")]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _groupService.GetGroupByGroupId(id);

            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [Display(Name = "حذف گروه")]
        [HttpPost("remove", Name = "PostRemoveGroup")]
        public async Task<IActionResult> Remove(Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            var selectedGroup = await _groupService.GetGroupByGroupId(group.GroupId);

            if (selectedGroup != null)
            {
                await _groupService.RemoveGroup(group.GroupId);
            }

            return RedirectToRoute("GetGroups");
        }

        private bool GroupExists(int id)
        {
            return _groupService.GroupExists(id);
        }
    }
}