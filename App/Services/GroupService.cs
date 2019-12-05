using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Domain.Entities.Post;
using App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext _context;

        public GroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Group> GetAllGroup()
        {
            return  _context.Groups.ToList();
        }

        public async Task CreateGroup(Group group)
        {
            _context.Groups.Add(group);
            await SaveChangeAsync();
        }

        public void EditGroup(Group group)
        {
             _context.Groups.Update(group);
        }
        

        public bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }

        public async Task<Group> GetGroupByGroupId(int? id)
        {
            return await _context.Groups.SingleOrDefaultAsync(g => g.GroupId == id);
        }

        public async Task<bool> RemoveGroup(int? groupId)
        {
            var group = await GetGroupByGroupId(groupId);
            _context.Groups.Remove(group);
            return _context.SaveChanges() != 0;
        }


        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
