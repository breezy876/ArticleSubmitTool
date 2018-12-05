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
    public class TwitterEmbedItem : EmbedItem
    {
        public TwitterEmbedItem()
        {
            Type = InstantArticleItemType.Twitter;
            ItemTypeId = 8;
            Name = "Twitter";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();

        }

        public TwitterEmbedItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            return base.ToXElement();
        }
    }
}
