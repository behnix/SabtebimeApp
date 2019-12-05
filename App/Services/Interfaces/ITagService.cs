using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities.Post;

namespace App.Services.Interfaces
{
    public interface ITagService
    {
        List<Tag> GetAllTag();

        void AddTag(Tag tag);

        Task<bool> ExistTag(string tagTitle);

        Tag GetTagByTagTitle(string tag);

        Task<Tag> GetTagByTagId(int id);
    }
}
