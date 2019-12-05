using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities.Page;

namespace App.Services.Interfaces
{
    public interface IPageService
    {
        Task<Page> GetPageById(int? pageId);

        Task<List<Page>> GetAllPages();

        Task AddPage(Page page);

        Task UpdatePage(Page page);

        Task RemovePage(int? pageId);

        Task<bool> IsPageTitleInBrowserRepeated(string pageTitleInBrowser);

        Task<bool> PageExists(int pageId);

        Task<Page> GetPageByShortUrl(string shortUrl);

        Task<Page> GetPageByPageTitleInBrowser(string pageTitleInBrowser);

        Task IncreaseVisit(int pageId);

        Task UpdateNormal(Page page);
    }
}
