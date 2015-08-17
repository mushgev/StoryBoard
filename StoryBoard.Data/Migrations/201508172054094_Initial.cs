namespace StoryBoard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Users", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        StoryId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StoryId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Group_GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Group_GroupId })
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .Index(t => t.User_UserId)
                .Index(t => t.Group_GroupId);
            
            CreateTable(
                "dbo.StoryGroups",
                c => new
                    {
                        Story_StoryId = c.Int(nullable: false),
                        Group_GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Story_StoryId, t.Group_GroupId })
                .ForeignKey("dbo.Stories", t => t.Story_StoryId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .Index(t => t.Story_StoryId)
                .Index(t => t.Group_GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stories", "UserId", "dbo.Users");
            DropForeignKey("dbo.StoryGroups", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.StoryGroups", "Story_StoryId", "dbo.Stories");
            DropForeignKey("dbo.Groups", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.UserGroups", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.UserGroups", "User_UserId", "dbo.Users");
            DropIndex("dbo.StoryGroups", new[] { "Group_GroupId" });
            DropIndex("dbo.StoryGroups", new[] { "Story_StoryId" });
            DropIndex("dbo.UserGroups", new[] { "Group_GroupId" });
            DropIndex("dbo.UserGroups", new[] { "User_UserId" });
            DropIndex("dbo.Stories", new[] { "UserId" });
            DropIndex("dbo.Groups", new[] { "OwnerId" });
            DropTable("dbo.StoryGroups");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Stories");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
        }
    }
}
