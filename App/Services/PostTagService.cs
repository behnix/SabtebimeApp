using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Domain.Entities.Post;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class PostTagService : IPostTagService
    {
        private readonly ApplicationDbContext _context;

        public PostTagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PostTag> GetAllTagsByPostId(int? postId)
        {
            return _context.PostTags.Include(pt=>pt.Tag).Where(pt => pt.PostId == postId).ToList();
        }

        public async Task<List<PostTag>> GetAllPostTags(int take)
        {
            return await _context.PostTags.Include(pt => pt.Tag).Take(take).ToListAsync();
        }

        public async Task AddPostTag(PostTag postTag)
        {
            await _context.PostTags.AddAsync(postTag);
            await _context.SaveChangesAsync();
        }

        public void RemoveTagFromPostTagByPostId(int postId, int tagId)
        {
            var postTag = _context.PostTags.SingleOrDefault(pt => pt.PostId == postId && pt.TagId == tagId);
            if (postTag != null) _context.PostTags.Remove(postTag);
        }

        public void SaveChangeAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
