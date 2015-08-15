namespace StoryBoard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Story_StoryId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Stories", t => t.Story_StoryId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Story_StoryId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        StoryId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.StoryId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stories", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Groups", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Groups", "Story_StoryId", "dbo.Stories");
            DropIndex("dbo.Stories", new[] { "User_UserId" });
            DropIndex("dbo.Groups", new[] { "User_UserId" });
            DropIndex("dbo.Groups", new[] { "Story_StoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Stories");
            DropTable("dbo.Groups");
        }
    }
}
