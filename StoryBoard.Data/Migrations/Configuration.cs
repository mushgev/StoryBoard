namespace StoryBoard.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoryBoard.Data.StoryBoardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StoryBoard.Data.StoryBoardContext context)
        {
            var user = new User
            {
                Name = "user",
                Password = "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8"
            };
            context.Users.AddOrUpdate(u => u.Name, user);
            context.SaveChanges();

            for (int i = 0; i < 14; i++)
            {
                context.Groups.AddOrUpdate(g => g.Name, new Group
                {
                    Name = "Group " + i,
                    Description = "Group Description" + i,
                    OwnerId = user.UserId
                });
            }

            for (int i = 0; i < 428; i++)
            {
                context.Stories.AddOrUpdate(s => s.Title, new Story
                {
                    UserId = user.UserId,
                    Title = "Great Story " + i,
                    Description = "This is a great story " + i,
                    Content = "This is the content for the gerat story " + i,
                    PostedOn = DateTime.Now
                });
            }
        }
    }
}
