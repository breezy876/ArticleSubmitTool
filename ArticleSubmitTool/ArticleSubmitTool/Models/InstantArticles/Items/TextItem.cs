using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;

namespace ArticleSubmitTool.Web.Models.InstantArticles.Items
{
    public class TextItem : InstantArticleItemModel
    {
        public TextItem()
        {
            Type = InstantArticleItemType.BodyText;
            ItemTypeId = 3;
            Name = "Text";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }

        public TextItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            var el = new XElement("p", Content.IsNullOrEmpty() ? string.Empty : Content);
            return el;
        }
    }
}
