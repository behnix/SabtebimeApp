using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Domain.Entities.Faq;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class FaqService : IFaqService
    {
        private readonly ApplicationDbContext _context;

        public FaqService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Faq> GetFaqById(int? faqId)
        {
            return await _context.Faqs.SingleOrDefaultAsync(f => f.FaqId == faqId);
        }

        public async Task<int> AddNewFaq(Faq faq)
        {
            faq.FaqDateTime = DateTime.Now;
            _context.Faqs.Add(faq);
            await _context.SaveChangesAsync();
            return faq.FaqId;
        }

        public void RemoveFaqAsync(int faqId)
        {
            var faqSelect = _context.Faqs.SingleOrDefault(f => f.FaqId == faqId);
            if (faqSelect != null) _context.Faqs.Remove(faqSelect);
        }

        public async Task<List<Faq>> GetAllPublishedFaq()
        {
            return await _context.Faqs.Where(f => f.FaqIsPublished).OrderByDescending(p => p.FaqId).ToListAsync();
        }

        public async Task<List<Faq>> GetAllNotPublishedFaq()
        {
            return await _context.Faqs.Where(f => f.FaqIsPublished == false).OrderByDescending(p => p.FaqId).ToListAsync();
        }

        public async Task UpdateFaq(Faq faq)
        {
            faq.FaqIsPublished = true;
            _context.Faqs.Update(faq);
            await _context.SaveChangesAsync();
        }

        public bool FaqExists(int faqId)
        {
            return _context.Faqs.Any(f => f.FaqId == faqId);
        }
    }
}
