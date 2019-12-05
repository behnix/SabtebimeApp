using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities.Faq;

namespace App.Services.Interfaces
{
    public interface IFaqService
    {
        Task<Faq> GetFaqById(int? faqId);
        Task<int> AddNewFaq(Faq faq);
        void RemoveFaqAsync(int faqId);
        Task<List<Faq>> GetAllPublishedFaq();
        Task<List<Faq>> GetAllNotPublishedFaq();
        Task UpdateFaq(Faq faq);
        bool FaqExists(int faqId);
    }
}
