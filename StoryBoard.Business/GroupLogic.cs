using StoryBoard.Data;
using StoryBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace StoryBoard.Business
{
    public class GroupLogic : BaseLogic
    {
        public GroupLogic(ModelEntityFactory factory, StoryBoardContext context)
            : base(factory, context)
        {

        }

        public GroupModel Get(int id)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            return _factory.GetModel(group);
        }

        public GroupModel Get(int id, int userId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            var model = _factory.GetModel(group);
            model.IsSubscribed = group.Users.Any(u => u.UserId == userId);
            return model;
        }

        public PagedModel<GroupModel> GetAll(int? page)
        {
            page = page ?? 1;

            var total = _context.Groups.Count();

            var groups = _context.Groups
                        .OrderBy(g => g.Name)
                        .Skip((page.Value - 1) * ListPageSize).Take(ListPageSize)
                        .Select(g => new GroupModel
                        {
                            GroupId = g.GroupId,
                            Name = g.Name,
                            Description = g.Description,
                            OwnerId = g.OwnerId,
                            SubscribersCount = g.Users.Count,
                            StoriesCount = g.Stories.Count
                        }).ToList();

            return ToPagedModel(total, page.Value, groups);
        }

        public List<GroupModel> GetUserGroups(int userId)
        {
            return _context.Groups.Where(g => g.Users.Any(u => u.UserId == userId)).Select(_factory.GetModel).ToList();
        }

        public async Task Edit(GroupModel model)
        {
            var group = _factory.GetEntity(model);
            _context.Groups.AddOrUpdate(group);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var group = _context.Groups.FirstOrDefault(s => s.GroupId == id);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task Join(int id, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            group.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Leave(int id, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == id);
            group.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
