namespace ArticleSubmitTool.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArticleSubmitTool.Domain.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ArticleSubmitTool.Domain.DataContext context)
        {
            context.InstantArticles.AddOrUpdate(
                new Domain.InstantArticle() { Title = "Article1", IsDeleted = false, URL = "http://www.facebook.com", Attributes = new byte[] {0}  },
                new Domain.InstantArticle() { Title = "Article2", IsDeleted = false, URL = "http://www.google.com", Attributes = new byte[] { 0 } },
                new Domain.InstantArticle() { Title = "Article3", IsDeleted = false, URL = "http://www.twitter.com", Attributes = new byte[] { 0 } }
            );

            context.InstantArticleItemTypes.AddOrUpdate(
                new Domain.InstantArticleItemType() { Name = "Image" },
                new Domain.InstantArticleItemType() { Name = "Video" },
                new Domain.InstantArticleItemType() { Name = "Text" },
                new Domain.InstantArticleItemType() { Name = "PullQuote" },
                new Domain.InstantArticleItemType() { Name = "BlockQuote" },
                new Domain.InstantArticleItemType() { Name = "Ad" },
                new Domain.InstantArticleItemType() { Name = "Map" },
                new Domain.InstantArticleItemType() { Name = "Twitter" },
                new Domain.InstantArticleItemType() { Name = "Facebook" },
                new Domain.InstantArticleItemType() { Name = "Instagram" },
                new Domain.InstantArticleItemType() { Name = "YouTube" },
                new Domain.InstantArticleItemType() { Name = "Caption" },
                new Domain.InstantArticleItemType() { Name = "SlideShow" }
            );

            context.SaveChanges();
        }
    }
}
