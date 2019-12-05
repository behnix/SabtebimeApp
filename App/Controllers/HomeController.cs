using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using App.Core.Securities;
using App.Domain.Entities.Contact;
using App.Domain.Entities.Post;
using App.DTOs.ContactUs;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services.Identity;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace App.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISettingService _settingService;
        private readonly IPostService _postService;
        private readonly ITagService _tagService;
        private readonly IContactService _contactService;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public HomeController(ISettingService settingService, IPostService postService, ITagService tagService, IContactService contactService, IEmailSender emailSender, ISmsSender smsSender)
        {
            _settingService = settingService;
            _postService = postService;
            _tagService = tagService;
            _contactService = contactService;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        [HttpGet("", Name = "GetIndex")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("about", Name = "GetAbout")]
        public async Task<IActionResult> About()
        {
            var setting = await _settingService.GetSetting();

            return View(setting);
        }
        [HttpGet("contact", Name = "GetContact")]
        public async Task<IActionResult> Contact()
        {
            var chaptcha = DetectBot.Chaptcha();
            ViewData["first"] = chaptcha[0];
            ViewData["second"] = chaptcha[1];
            ViewData["operators"] = chaptcha[2];
            TempData["result"] = chaptcha[3];
            var contactUs = new ContactUs
            {
                Setting = await _settingService.GetSetting()
            };
            return View(contactUs);
        }

        [HttpPost("contact", Name = "PostContact")]
        public async Task<IActionResult> Contact(Contact contact, int res)
        {
            var contactUs = new ContactUs
            {
                Setting = await _settingService.GetSetting()
            };
            var resBack = TempData["result"];
            if (!ModelState.IsValid)
            {
                var chaptcha = DetectBot.Chaptcha();
                ViewData["first"] = chaptcha[0];
                ViewData["second"] = chaptcha[1];
                ViewData["operators"] = chaptcha[2];
                TempData["result"] = chaptcha[3];
                if (res == 0)
                {
                    ModelState.AddModelError(string.Empty, "پاسخ امنیتی را وارد نکرده اید.");
                }
                return View(contactUs);
            }

            if (res == Convert.ToInt32(resBack))
            {
                contact.PostedOn = DateTime.Now;
                contact.IsRead = false;
                var result = await _contactService.AddNewContact(contact);
                if (result != 0)
                {
                    var settings = _settingService.GetSetting();
                    var adminSmsSender = settings.Result.IsSmsActiveToAdmin;
                    var adminCellPhone = settings.Result.SiteSmsNumber;
                    ViewData["Message"] = "پیام شما با موفقیت ارسال گردید و در اسرع وقت به آن پاسخ داده خواهد شد.";
                    if (adminSmsSender)
                    {
                        await _smsSender.SendSmsAsync(adminCellPhone, "پیامی در بخش تماس با ما ارسال شد");
                    }
                }
                var chaptcha = DetectBot.Chaptcha();
                ViewData["first"] = chaptcha[0];
                ViewData["second"] = chaptcha[1];
                ViewData["operators"] = chaptcha[2];
                ViewData["results"] = chaptcha[3];
                TempData["result"] = chaptcha[3];
            }
            else
            {
                var chaptcha = DetectBot.Chaptcha();
                ViewData["first"] = chaptcha[0];
                ViewData["second"] = chaptcha[1];
                ViewData["operators"] = chaptcha[2];
                TempData["result"] = chaptcha[3];
                ModelState.AddModelError(string.Empty, "پاسخ امنیتی صحیح نمی باشد.");
            }

            return View(contactUs);
        }

        [HttpGet("post/{titleInBrowser}")]
        public async Task<IActionResult> Post(string titleInBrowser)
        {
            if (titleInBrowser == null)
            {
                return NotFound();
            }

            ViewData["SiteAddress"] = _settingService.GetSetting().Result.SiteNameInEnglish;
            ViewData["SiteName"] = _settingService.GetSetting().Result.SiteName;

            var post = await _postService.GetPostByTitleInBrowser(titleInBrowser);
            var tags = new List<Tag>();
            foreach (var tag in post.PostTags)
            {
                tags.Add(await _tagService.GetTagByTagId(tag.TagId));
            }

            ViewData["Tags"] = tags;
            return View(post);
        }

        [HttpGet("a/{shortUrl}")]
        public async Task<IActionResult> PostShortUrl(string shortUrl)
        {
            if (shortUrl == null)
            {
                return NotFound();
            }

            ViewData["SiteAddress"] = _settingService.GetSetting().Result.SiteNameInEnglish;
            ViewData["SiteName"] = _settingService.GetSetting().Result.SiteName;

            var post = await _postService.GetPostByShortUrl(shortUrl);
            var tags = new List<Tag>();
            foreach (var tag in post.PostTags)
            {
                tags.Add(await _tagService.GetTagByTagId(tag.TagId));
            }

            ViewData["Tags"] = tags;
            return View(post);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}