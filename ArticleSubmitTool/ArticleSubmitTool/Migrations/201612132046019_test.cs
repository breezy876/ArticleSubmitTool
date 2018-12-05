namespace ArticleSubmitTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacebookPage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PageId = c.String(nullable: false),
                        URL = c.String(),
                        Title = c.String(nullable: false),
                        AccessToken = c.String(nullable: false),
                        UserId = c.Long(nullable: false),
                        FacebookUserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstantArticleItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InstantArticleId = c.Long(nullable: false),
                        ItemTypeId = c.Long(nullable: false),
                        Attributes = c.Binary(),
                        Content = c.String(),
                        IsHeader = c.Boolean(nullable: false),
                        IsCaption = c.Boolean(nullable: false),
                        ParentId = c.Long(),
                        CreatedBy = c.Long(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        DateDeleted = c.DateTime(),
                        DeletedBy = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedBy)
                .ForeignKey("dbo.User", t => t.DeletedBy)
                .ForeignKey("dbo.InstantArticle", t => t.InstantArticleId, cascadeDelete: true)
                .ForeignKey("dbo.InstantArticleItemType", t => t.ItemTypeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.ModifiedBy)
                .ForeignKey("dbo.InstantArticleItem", t => t.ParentId)
                .Index(t => t.InstantArticleId)
                .Index(t => t.ItemTypeId)
                .Index(t => t.ParentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.DeletedBy)
                .Index(t => t.ModifiedBy);
            
            CreateTable(
                "dbo.InstantArticle",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        SubTitle = c.String(maxLength: 100),
                        Kicker = c.String(maxLength: 100),
                        URL = c.String(maxLength: 255),
                        Attributes = c.Binary(),
                        FacebookPageId = c.Long(),
                        IsPublished = c.Boolean(nullable: false),
                        Authors = c.Binary(),
                        Copyright = c.String(),
                        Credits = c.String(),
                        RelatedArticles = c.String(),
                        NewsFeedTitle = c.String(),
                        NewsFeedDescription = c.String(),
                        NewsFeedImage = c.String(),
                        FacebookFeedback = c.Int(),
                        ArticleFeedbackEnabled = c.Boolean(nullable: false),
                        DatePublished = c.DateTime(),
                        CreatedBy = c.Long(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                        DateDeleted = c.DateTime(),
                        DeletedBy = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedBy)
                .ForeignKey("dbo.User", t => t.DeletedBy)
                .ForeignKey("dbo.User", t => t.ModifiedBy)
                .ForeignKey("dbo.FacebookPage", t => t.FacebookPageId)
                .Index(t => t.FacebookPageId)
                .Index(t => t.CreatedBy)
                .Index(t => t.DeletedBy)
                .Index(t => t.ModifiedBy);
            
            CreateTable(
                "dbo.InstantArticleItemType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FacebookPageId = c.String(),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "UserId", "dbo.User");
            DropForeignKey("dbo.InstantArticleItem", "ParentId", "dbo.InstantArticleItem");
            DropForeignKey("dbo.InstantArticleItem", "ModifiedBy", "dbo.User");
            DropForeignKey("dbo.InstantArticleItem", "ItemTypeId", "dbo.InstantArticleItemType");
            DropForeignKey("dbo.InstantArticleItem", "InstantArticleId", "dbo.InstantArticle");
            DropForeignKey("dbo.InstantArticle", "FacebookPageId", "dbo.FacebookPage");
            DropForeignKey("dbo.InstantArticle", "ModifiedBy", "dbo.User");
            DropForeignKey("dbo.InstantArticle", "DeletedBy", "dbo.User");
            DropForeignKey("dbo.InstantArticle", "CreatedBy", "dbo.User");
            DropForeignKey("dbo.InstantArticleItem", "DeletedBy", "dbo.User");
            DropForeignKey("dbo.InstantArticleItem", "CreatedBy", "dbo.User");
            DropForeignKey("dbo.FacebookPage", "UserId", "dbo.User");
            DropIndex("dbo.UserSettings", new[] { "UserId" });
            DropIndex("dbo.InstantArticle", new[] { "ModifiedBy" });
            DropIndex("dbo.InstantArticle", new[] { "DeletedBy" });
            DropIndex("dbo.InstantArticle", new[] { "CreatedBy" });
            DropIndex("dbo.InstantArticle", new[] { "FacebookPageId" });
            DropIndex("dbo.InstantArticleItem", new[] { "ModifiedBy" });
            DropIndex("dbo.InstantArticleItem", new[] { "DeletedBy" });
            DropIndex("dbo.InstantArticleItem", new[] { "CreatedBy" });
            DropIndex("dbo.InstantArticleItem", new[] { "ParentId" });
            DropIndex("dbo.InstantArticleItem", new[] { "ItemTypeId" });
            DropIndex("dbo.InstantArticleItem", new[] { "InstantArticleId" });
            DropIndex("dbo.FacebookPage", new[] { "UserId" });
            DropTable("dbo.UserSettings");
            DropTable("dbo.InstantArticleItemType");
            DropTable("dbo.InstantArticle");
            DropTable("dbo.InstantArticleItem");
            DropTable("dbo.User");
            DropTable("dbo.FacebookPage");
        }
    }
}
