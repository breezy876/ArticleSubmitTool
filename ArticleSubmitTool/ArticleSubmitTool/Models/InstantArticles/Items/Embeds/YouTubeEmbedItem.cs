using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Models.InstantArticles.Items.Embeds;

namespace ArticleSubmitTool.Web.Models.InstantArticles.Items
{
    public class YouTubeEmbedItem : EmbedItem
    {
        public YouTubeEmbedItem()
        {
            Type = InstantArticleItemType.YouTube;
            ItemTypeId = 11;
            Name = "YouTube";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }

        public YouTubeEmbedItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            return base.ToXElement();
        }
    }
}
