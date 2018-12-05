using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleSubmitTool.Models.InstantArticles;

namespace ArticleSubmitTool.Web.Models.InstantArticles.Items
{
    public class CaptionItem : InstantArticleItemModel
    {
        public CaptionItem()
        {
            Type = InstantArticleItemType.Caption;
            ItemTypeId = 12;
            Name = "Caption";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
            IsCaption = true;
        }

        public CaptionItem(InstantArticleItemModel model) : base(model)
        {

        }


        public override XElement ToXElement()
        {
            throw new NotImplementedException();
        }
    }
}
