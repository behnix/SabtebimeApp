using System.Threading.Tasks;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Components
{
    public class LayoutComponent :ViewComponent
    {
        private readonly ISettingService _settingService;

        public LayoutComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_layoutComponent", await _settingService.GetSetting()));
        }
    }
}
