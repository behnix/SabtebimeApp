using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Convertors;
using App.Domain.Entities.Page;
using App.Services;
using App.Services.Identity.Managers;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{

    [Route("page")]
    [Authorize(nameof(ConstantPolicies.DynamicPermission))]
    public class PageController : BaseController
    {
        private readonly IPageService _pageService;
        private readonly IGroupService _groupService;

        public PageController(IPageService pageService, IGroupService groupService)
        {
            _pageService = pageService;
            _groupService = groupService;
        }


        [Display(Name = "صفحه ها")]
        [HttpGet("list", Name = "GetPages")]
        public async Task<IActionResult> Index()
        {
            var pages = await _pageService.GetAllPages();

            return View(pages);
        }

        [Display(Name = "افزودن صفحه جدید")]
        [HttpGet("create", Name = "GetCreatePage")]
        public IActionResult Create()
        {
            var selectItem = new Dictionary<int, string>();
            foreach (var grp in _groupService.GetAllGroup())
                selectItem.Add(grp.GroupId, grp.GroupTitle);

            ViewData["Groups"] = selectItem;
            return View();
        }

        [Display(Name = "افزودن صفحه جدید")]
        [HttpPost("create", Name = "PostCreatePage")]
        public async Task<IActionResult> Create(Page page)
        {
            if (!ModelState.IsValid)
                return View(page);

            if (await _pageService.IsPageTitleInBrowserRepeated(page.PageTitleInBrowser))
            {
                ModelState.AddModelError("page.PageTitleInBrowser", "قبلا صفحه ای با این عنوان برای مرورگر ثبت شده است!");
                return View(page);
            }

            await _pageService.AddPage(page);

            return RedirectToRoute("GetPages");
        }

        [Display(Name = "ویرایش صفحه")]
        [HttpGet("edit", Name = "GetEditPage")]
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _pageService.GetPageById(id);

            if (page == null)
            {
                return NotFound();
            }

            var selectItem = new Dictionary<int, string>();
            foreach (var grp in _groupService.GetAllGroup())
                selectItem.Add(grp.GroupId, grp.GroupTitle);

            ViewData["Groups"] = selectItem;
            page.PageTitleInBrowser = TextConvertor.ReplaceLetters(page.PageTitleInBrowser, '-', ' ');

            return View(page);
        }


        [Display(Name = "ویرایش صفحه")]
        [HttpPost("edit", Name = "PostEditPage")]
        public async Task<IActionResult> Edit(Page page)
        {
            if (!ModelState.IsValid)
            {
                return View(page);
            }


            try
            {
                await _pageService.UpdatePage(page);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PageExists(page.PageId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToRoute("GetPages");
        }

        [Display(Name = "حذف صفحه")]
        [HttpGet("remove", Name = "GetRemovePage")]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _pageService.GetPageById(id);

            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [Display(Name = "حذف صفحه")]
        [HttpPost("remove", Name = "PostRemovePage")]
        public async Task<IActionResult> Remove(Page page)
        {
            var selectedPage = await _pageService.GetPageById(page.PageId);

            if (selectedPage != null)
            {
                await _pageService.RemovePage(page.PageId);
            }

            return RedirectToRoute("GetPages");
        }
        private async Task<bool> PageExists(int id)
        {
            return await _pageService.PageExists(id);
        }
    }
}