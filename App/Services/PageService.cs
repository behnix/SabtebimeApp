using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Convertors;
using App.Data;
using App.Domain.Entities.Page;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class PageService : IPageService
    {
        private readonly ApplicationDbContext _context;

        public PageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Page> GetPageById(int? pageId)
        {
            return await _context.Pages.SingleOrDefaultAsync(p => p.PageId == pageId);
        }

        public async Task<List<Page>> GetAllPages()
        {
            return await _context.Pages.ToListAsync();
        }

        public async Task AddPage(Page page)
        {
            page.PageTitleInBrowser = TextConvertor.FixingText(page.PageTitleInBrowser);
            page.PageCreateDate = DateTime.Now;
            page.PageUpdateDate = DateTime.Now;
            page.PageVisit = 0;
            await _context.Pages.AddAsync(page);
            await _context.SaveChangesAsync();
            await SetShortUrlToPost(page.PageId);
        }

        public async Task UpdatePage(Page page)
        {
            page.PageUpdateDate = DateTime.Now;
            page.PageTitleInBrowser = TextConvertor.ReplaceLetters(TextConvertor.FixingText(page.PageTitleInBrowser), ' ', '-');

            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNormal(Page page)
        {
            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePage(int? pageId)
        {
            _context.Pages.Remove( await GetPageById(pageId));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsPageTitleInBrowserRepeated(string pageTitleInBrowser)
        {
            return await _context.Pages.AnyAsync(p => p.PageTitleInBrowser == pageTitleInBrowser);
        }

        public async Task<bool> PageExists(int pageId)
        {
            return await _context.Pages.AnyAsync(p => p.PageId == pageId);
        }

        public async Task<Page> GetPageByShortUrl(string shortUrl)
        {
            return await _context.Pages.SingleOrDefaultAsync(p => p.PageShortUrl == shortUrl);
        }

        public async Task<Page> GetPageByPageTitleInBrowser(string pageTitleInBrowser)
        {
            return await _context.Pages.SingleOrDefaultAsync(p => p.PageTitleInBrowser == pageTitleInBrowser);
        }

        public async Task SetShortUrlToPost(int postId)
        {
            var postForAddShortUrl = await GetPageById(postId);
            postForAddShortUrl.PageShortUrl = Base36.Encode(postId);
            await UpdatePage(postForAddShortUrl);
            await _context.SaveChangesAsync();
        }

        public async Task IncreaseVisit(int pageId)
        {
            var page = await GetPageById(pageId);
            if (page == null) return;
            page.PageVisit += 1;
            await UpdateNormal(page);
        }
    }
}
