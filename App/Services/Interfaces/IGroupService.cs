using System.Collections.Generic;
using System.Threading.Tasks;
using App.Domain.Entities.Post;

namespace App.Services.Interfaces
{
    public interface IGroupService
    {
        List<Group> GetAllGroup();

        Task CreateGroup(Group group);

        void EditGroup(Group groupId);

        bool GroupExists(int id);

        Task<Group> GetGroupByGroupId(int? id);
        Task<bool> RemoveGroup(int? groupId);

        Task SaveChangeAsync();
    }
}
