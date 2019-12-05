using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities.Slider;
using Microsoft.AspNetCore.Http;

namespace App.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllSliders();

        Task<List<Slider>> GetAllActiveSliders();

        Task<Slider> GetSliderById(int? sliderId);

        Task<int> AddNewSlider(Slider slider, IFormFile sliderImageUp);

        Task<bool> RemoveSliderAsync(int sliderId);
        
        Task UpdateSlider(Slider slider, IFormFile sliderImageUp, string oldImage);

        bool SliderExists(int sliderId);
        
        Task SaveChangeAsync();
    }
}
