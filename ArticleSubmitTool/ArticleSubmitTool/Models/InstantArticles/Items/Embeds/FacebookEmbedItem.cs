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
    public class FacebookEmbedItem : EmbedItem
    {
        public FacebookEmbedItem()
        {
            Type = InstantArticleItemType.Facebook;
            ItemTypeId = 9;
            Name = "Facebook";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();

        }


        public FacebookEmbedItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            return base.ToXElement();
        }
    }
}
