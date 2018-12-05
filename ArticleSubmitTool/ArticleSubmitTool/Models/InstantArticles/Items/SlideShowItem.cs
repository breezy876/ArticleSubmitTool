using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Models.InstantArticles.Items;

namespace ArticleSubmitTool.Web.Models.InstantArticles.Items
{
    public class SlideShowItem : ItemWithCaption
    {
        public SlideShowItem()
        {
            Type = InstantArticleItemType.SlideShow;
            ItemTypeId = 13;
            Name = "SlideShow";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Video", Id = 2 };
            Initialize();
        }

        public SlideShowItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            throw new NotImplementedException();
        }
    }
}
