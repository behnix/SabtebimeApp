using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Components
{
    public class SliderComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;

        public SliderComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_sliderComponent", await _sliderService.GetAllActiveSliders()));
        }
    }
}
