using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Uploader;
using App.Data;
using App.Domain.Entities.Slider;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class SliderService :ISliderService
    {
        private readonly ApplicationDbContext _context;

        public SliderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Slider>> GetAllSliders()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<List<Slider>> GetAllActiveSliders()
        {
            var today = DateTime.Today;
            var allSlider = await GetAllSliders();
            var sliders = new List<Slider>();
            foreach (var item in allSlider.Where(s=>s.IsActive))
            {
                var slider = new Slider();
                if (item.SliderType == 1)
                {
                    slider = item;
                }
                else if(item.SliderType == 2 && item.SliderStartTime <= today && item.SliderEndTime >= today)
                {
                    slider = item;
                }
                sliders.Add(slider);
            }
            return sliders;
        }

        public async Task<Slider> GetSliderById(int? sliderId)
        {
            return await _context.Sliders.SingleOrDefaultAsync(s => s.SliderId == sliderId);
        }

        public async Task<int> AddNewSlider(Slider slider, IFormFile sliderImageUp)
        {
            slider.SliderImage = sliderImageUp == null ? "no-image.png" : ImageTools.UploadImageNormal("no-image.png", sliderImageUp, "no-image.png", "wwwroot/assets/sliders", false, "wwwroot/assets/sliders", 240);

            _context.Sliders.Add(slider);
            await _context.SaveChangesAsync();
            return slider.SliderId;
        }


        public async Task UpdateSlider(Slider slider, IFormFile sliderImageUp, string oldImage)
        {
            slider.SliderImage = sliderImageUp == null ? oldImage : ImageTools.UploadImageNormal(oldImage, sliderImageUp, "no-image.png", "wwwroot/assets/sliders", false, "wwwroot/assets/sliders", 240);

            _context.Sliders.Update(slider);
            await SaveChangeAsync();
        }

        public bool SliderExists(int sliderId)
        {
            return _context.Sliders.Any(s => s.SliderId == sliderId);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveSliderAsync(int sliderId)
        {
            var slider = await GetSliderById(sliderId);
            _context.Sliders.Remove(slider);
            return _context.SaveChanges() != 0;
        }
    }
}
