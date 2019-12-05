using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Entities.Setting;
using App.Services;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Route("setting")]
    [Authorize(nameof(ConstantPolicies.DynamicPermission))]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [Display(Name = "درباره ما")]
        [HttpGet("about", Name = "GetEditAbout")]
        public async Task<IActionResult> About()
        {
            var setting = await _settingService.GetSetting();

            return View(setting);
        }

        [Display(Name = "درباره ما")]
        [HttpPost("about", Name = "PostEditAbout")]
        public async Task<IActionResult> About(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }

            var selectedSetting = await _settingService.GetSetting();
            selectedSetting.AboutUs = setting.AboutUs;
            selectedSetting.AboutUsTitle = setting.AboutUsTitle;
            selectedSetting.AboutUsDescription = setting.AboutUsDescription;
            await _settingService.UpdateSetting(selectedSetting);

            return RedirectToRoute("GetEditAbout");
        }


        [Display(Name = "تماس با ما")]
        [HttpGet("contact", Name = "GetEditContact")]
        public async Task<IActionResult> Contact()
        {
            var setting = await _settingService.GetSetting();

            return View(setting);
        }

        [Display(Name = "تماس با ما")]
        [HttpPost("contact", Name = "PostEditContact")]
        public async Task<IActionResult> Contact(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }

            var selectedSetting = await _settingService.GetSetting();
            selectedSetting.ContactTitle = setting.ContactTitle;
            selectedSetting.ContactDescription = setting.ContactDescription;
            selectedSetting.ContactWorkHour = setting.ContactWorkHour;
            selectedSetting.SiteTel = setting.SiteTel;
            selectedSetting.SiteAddress = setting.SiteAddress;
            selectedSetting.SiteEmail = setting.SiteEmail;
            selectedSetting.SiteTelegramId = setting.SiteTelegramId;
            selectedSetting.SiteInstagramId = setting.SiteInstagramId;
            selectedSetting.SiteTwitterId = setting.SiteTwitterId;
            selectedSetting.SiteFacebookId = setting.SiteFacebookId;
            await _settingService.UpdateSetting(selectedSetting);

            return RedirectToRoute("GetEditContact");
        }

        [Display(Name = "صفحه اصلی")]
        [HttpGet("index", Name = "GetEditIndex")]
        public async Task<IActionResult> Index()
        {
            var setting = await _settingService.GetSetting();

            return View(setting);
        }

        [Display(Name = "صفحه اصلی")]
        [HttpPost("index", Name = "PostEditIndex")]
        public async Task<IActionResult> Index(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View(setting);
            }

            var selectedSetting = await _settingService.GetSetting();
            selectedSetting.SiteName = setting.SiteName;
            selectedSetting.SiteNameInEnglish = setting.SiteNameInEnglish;
            selectedSetting.SiteDescription = setting.SiteDescription;
            selectedSetting.SiteNameInEnglish = setting.SiteNameInEnglish;
            selectedSetting.OfferTitle = setting.OfferTitle;
            selectedSetting.OfferText = setting.OfferText;
            selectedSetting.RecomendedTitle = setting.RecomendedTitle;
            selectedSetting.RecomendedText = setting.RecomendedText;
            selectedSetting.Slogan1 = setting.Slogan1;
            selectedSetting.Slogan2 = setting.Slogan2;
            selectedSetting.QuantityPostInIndex = setting.QuantityPostInIndex;
            selectedSetting.SiteCopyRightText = setting.SiteCopyRightText;

            await _settingService.UpdateSetting(selectedSetting);

            return RedirectToRoute("GetEditIndex");

        }
    }
}