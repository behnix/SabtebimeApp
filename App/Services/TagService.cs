using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Domain.Entities.Post;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Tag> GetAllTag()
        {
            return _context.Tags.Include(t=>t.PostTags).ToList();
        }

        public void AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public async Task<bool> ExistTag(string tagTitle)
        {
            return await _context.Tags.AnyAsync(t => t.TagTitle == tagTitle);
        }

        public Tag GetTagByTagTitle(string tag)
        {
            return _context.Tags.SingleOrDefault(t => t.TagTitle == tag);
        }

        public async Task<Tag> GetTagByTagId(int id)
        {
            return await _context.Tags.SingleOrDefaultAsync(t => t.TagId == id);
        }
    }
}
