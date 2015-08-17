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
        public GroupLogic(ModelEntityFactory factory)
            : base(factory)
        {

        }

        public GroupModel Get(int id)
        {
            using (var db = new StoryBoardContext())
            {
                var group = db.Groups.FirstOrDefault(g => g.GroupId == id);
                return _factory.GetModel(group);
            }
        }

        public PagedModel<GroupModel> GetAll(int? page)
        {
            page = page ?? 1;

            using (var db = new StoryBoardContext())
            {
                var total = db.Groups.Count();

                var groups = db.Groups
                            .OrderBy(g => g.Name)
                            .Skip((page.Value - 1) * ListPageSize).Take(ListPageSize)
                            .Select(_factory.GetModel).ToList();

                return ToPagedModel(total, page.Value, groups);
            }
        }

        public List<GroupModel> GetUserGroups(int userId)
        {
            using (var db = new StoryBoardContext())
            {
                return db.Groups.Where(g => g.OwnerId == userId).Select(_factory.GetModel).ToList();
            }
        }

        public async Task Edit(GroupModel model)
        {
            using (var db = new StoryBoardContext())
            {
                var group = _factory.GetEntity(model);
                db.Groups.AddOrUpdate(group);
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var db = new StoryBoardContext())
            {
                var group = db.Groups.FirstOrDefault(s => s.GroupId == id);
                db.Groups.Remove(group);
                await db.SaveChangesAsync();
            }
        }
    }
}
