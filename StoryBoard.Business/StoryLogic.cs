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
    public class StoryLogic : BaseLogic
    {
        public StoryLogic(ModelEntityFactory factory, StoryBoardContext context)
            : base(factory, context)
        {

        }

        public StoryModel Get(int id)
        {
            var story = _context.Stories.FirstOrDefault(s => s.StoryId == id);
            return _factory.GetModel(story);
        }

        public PagedModel<StoryModel> GetUserStories(int userId, int? page)
        {
            page = page ?? 1;

            var total = _context.Stories.Count(s => s.UserId == userId);

            var stories = _context.Stories.Where(s => s.UserId == userId)
                        .OrderByDescending(s => s.StoryId)
                        .Skip((page.Value - 1) * ListPageSize).Take(ListPageSize)
                        .Select(_factory.GetModel).ToList();

            return ToPagedModel(total, page.Value, stories);
        }

        public async Task Edit(StoryModel model)
        {
            if (model.StoryId == 0)
            {
                model.PostedOn = DateTime.Now;
            }

            var story = _factory.GetEntity(model);
            _context.Stories.Attach(story);
            _context.Entry(story).Collection(s => s.Groups).Load();

            if(model.Groups == null)
            {
                model.Groups = new List<int>();
            }
            var groupsToAdd = model.Groups.Where(id => !story.Groups.Any(g => g.GroupId == id)).ToList();
            var groupsToRemove = story.Groups.Where(group => !model.Groups.Any(id => group.GroupId == id)).ToList();

            foreach (var item in groupsToRemove)
            {
                story.Groups.Remove(item);
            }
            
            foreach (var groupId in groupsToAdd)
            {
                var group = new Group { GroupId = groupId };
                _context.Groups.Attach(group);

                story.Groups.Add(group);
            }

            _context.Stories.AddOrUpdate(story);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var story = _context.Stories.FirstOrDefault(s => s.StoryId == id);
            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();
        }
    }
}
