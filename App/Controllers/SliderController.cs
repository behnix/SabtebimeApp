using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Convertors;
using App.Domain.Entities.Post;
using App.Domain.Entities.Slider;
using App.Services;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [Route("slider")]
    [Authorize(nameof(ConstantPolicies.DynamicPermission))]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [DisplayName("لیست اسلایدرها")]
        [HttpGet("list", Name = "GetSliders")]
        public async Task<IActionResult> Index()
        {
            var slider = await _sliderService.GetAllSliders();
            return View(slider);
        }

        [DisplayName("ایجاد اسلایدر جدید")]
        [HttpGet("new", Name = "GetCreateSlider")]
        public IActionResult Create()
        {
            return View();
        }

        [DisplayName("ایجاد اسلایدر جدید")]
        [HttpPost("new", Name = "PostCreateSlider")]
        public async Task<IActionResult> Create(Slider slider, IFormFile sliderImageUp, string startTime, string endTime)
        {
            slider.SliderImage = sliderImageUp.ToString();
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            if (slider.SliderType == 2)
            {
                var startDate = startTime.Split("/");
                var endDate = endTime.Split("/");
                var ys = DateConvertor.PersianIntToEn(startDate[0]);
                var ms = DateConvertor.PersianIntToEn(startDate[1]);
                var ds = DateConvertor.PersianIntToEn(startDate[2]);
                var ye = DateConvertor.PersianIntToEn(endDate[0]);
                var me = DateConvertor.PersianIntToEn(endDate[1]);
                var de = DateConvertor.PersianIntToEn(endDate[2]);
                var startDateTime = new DateTime(ys, ms, ds).ToMiladiDate();
                var endDateTime = new DateTime(ye, me, de).ToMiladiDate();
                slider.SliderStartTime = startDateTime;
                slider.SliderEndTime = endDateTime;
            }
            else
            {
                slider.SliderStartTime = DateTime.Now;
                slider.SliderEndTime = DateTime.Now;
            }
            await _sliderService.AddNewSlider(slider, sliderImageUp);

            return RedirectToRoute("GetSliders");
        }

        [DisplayName("ویرایش اسلایدر")]
        [HttpGet("edit", Name = "GetEditSlider")]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetSliderById(id);

            if (slider == null)
            {
                return NotFound();
            }
            ViewData["SliderImage"] = slider.SliderImage ?? "no-image.png";
            return View(slider);
        }

        [DisplayName("ویرایش اسلایدر")]
        [HttpPost("edit", Name = "PostEditSlider")]
        public async Task<IActionResult> Edit(Slider slider, IFormFile sliderImageUp, string oldImage, string startTime, string endTime)
        {

            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            if (slider.SliderType == 2)
            {
                var startDate = startTime.Split("/");
                var endDate = endTime.Split("/");
                var ys = DateConvertor.PersianIntToEn(startDate[0]);
                var ms = DateConvertor.PersianIntToEn(startDate[1]);
                var ds = DateConvertor.PersianIntToEn(startDate[2]);
                var ye = DateConvertor.PersianIntToEn(endDate[0]);
                var me = DateConvertor.PersianIntToEn(endDate[1]);
                var de = DateConvertor.PersianIntToEn(endDate[2]);
                var startDateTime = new DateTime(ys, ms, ds).ToMiladiDate();
                var endDateTime = new DateTime(ye, me, de).ToMiladiDate();
                slider.SliderStartTime = startDateTime;
                slider.SliderEndTime = endDateTime;
            }

            try
            {
                await _sliderService.UpdateSlider(slider, sliderImageUp, oldImage);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SliderExists(slider.SliderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToRoute("GetSliders");
        }

        [DisplayName("حذف اسلایدر")]
        [HttpGet("remove", Name = "GetRemoveSlider")]
        public async Task<IActionResult> Remove(int id)
        {
            var slider = await _sliderService.GetSliderById(id);

            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        [Display(Name = "حذف اسلایدر")]
        [HttpPost("remove", Name = "PostSliderPost")]
        public async Task<IActionResult> Remove(Slider slider)
        {
            var selectedSlider = await _sliderService.GetSliderById(slider.SliderId);

            if (selectedSlider != null)
            {
                await _sliderService.RemoveSliderAsync(slider.SliderId);
            }

            return RedirectToRoute("GetSliders");
        }

        private bool SliderExists(int id)
        {
            return _sliderService.SliderExists(id);
        }
    }
}
