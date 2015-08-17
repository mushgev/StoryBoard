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
        public StoryLogic(ModelEntityFactory factory)
            : base(factory)
        {

        }

        public StoryModel Get(int id)
        {
            using (var db = new StoryBoardContext())
            {
                var story = db.Stories.FirstOrDefault(s => s.StoryId == id);
                return _factory.GetModel(story);
            }
        }

        public PagedModel<StoryModel> GetUserStories(int userId, int? page)
        {
            page = page ?? 1;

            using (var db = new StoryBoardContext())
            {
                var total = db.Stories.Count(s => s.UserId == userId);

                var stories = db.Stories.Where(s => s.UserId == userId)
                            .OrderByDescending(s => s.StoryId)
                            .Skip((page.Value - 1) * ListPageSize).Take(ListPageSize)
                            .Select(_factory.GetModel).ToList();

                return ToPagedModel(total, page.Value, stories);
            }
        }

        public async Task Edit(StoryModel model)
        {
            if(model.StoryId == 0)
            {
                model.PostedOn = DateTime.Now;
            }

            using (var db = new StoryBoardContext())
            {
                var story = _factory.GetEntity(model);
                db.Stories.AddOrUpdate(story);
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var db = new StoryBoardContext())
            {
                var story = db.Stories.FirstOrDefault(s => s.StoryId == id);
                db.Stories.Remove(story);
                await db.SaveChangesAsync();
            }
        }
    }
}
