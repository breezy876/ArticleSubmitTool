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
    public class InstagramEmbedItem : EmbedItem
    {
        public InstagramEmbedItem()
        {
            Type = InstantArticleItemType.Instagram;
            ItemTypeId = 10;
            Name = "Instagram";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }

        public InstagramEmbedItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            return base.ToXElement();
        }
    }
}
