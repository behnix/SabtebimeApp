using System.Linq;
using System.Threading.Tasks;
using App.Core.Uploader;
using App.Data;
using App.Domain.Entities.Setting;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class SettingService : ISettingService
    {
        private readonly ApplicationDbContext _context;

        public SettingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateSetting(Setting setting)
        {
            _context.Settings.Update(setting);
            await _context.SaveChangesAsync();
        }

        public async Task<Setting> GetSetting()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }


        public async Task UpdateSettingWithImage(Setting setting, IFormFile logoImageUp, string oldImage, IFormFile faveiconImageUp, string oldfaveicon)
        {
            setting.Logo = logoImageUp == null ? oldImage : ImageTools.UploadImageNormal(oldImage, logoImageUp, "no-image.png", "wwwroot/assets/logo", false, "wwwroot/assets/logo", 240);
            setting.Faveicon = faveiconImageUp == null ? oldfaveicon : ImageTools.UploadImageNormal(oldfaveicon, faveiconImageUp, "faveicon.ico", "wwwroot", false, "wwwroot", 240);

            _context.Settings.Attach(setting).State = EntityState.Modified;
            await SaveChangeAsync();
        }

        public bool SettingExists(int settingId)
        {
            return _context.Settings.Any(p => p.SettingId == settingId);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}