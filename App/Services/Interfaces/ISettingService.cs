using System.Threading.Tasks;
using App.Domain.Entities.Setting;
using Microsoft.AspNetCore.Http;

namespace App.Services.Interfaces
{
    public interface ISettingService
    {
        Task UpdateSetting(Setting setting);

        Task<Setting> GetSetting();

        Task UpdateSettingWithImage(Setting setting, IFormFile logoImageUp, string oldImage, IFormFile faveiconImageUp,
            string oldfaveicon);

        bool SettingExists(int settingId);
    }
}