using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities.Post;

namespace App.Services.Interfaces
{
    public interface IPostTagService
    {
        List<PostTag> GetAllTagsByPostId(int? postId);

        Task<List<PostTag>> GetAllPostTags(int take);

        Task AddPostTag(PostTag postTag);

        void RemoveTagFromPostTagByPostId(int postId, int tagId);

        void SaveChangeAsync();
    }
}
