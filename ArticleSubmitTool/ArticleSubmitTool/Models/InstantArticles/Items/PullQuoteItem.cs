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
    public class PullQuoteItem : InstantArticleItemModel
    {
        public PullQuoteItem()
        {
            Type = InstantArticleItemType.PullQuote;
            ItemTypeId = 4;
            Name = "PullQuote";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }


        public PullQuoteItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            var el = new XElement("aside", Content.IsNullOrEmpty() ? string.Empty : Content);
            return el;
        }
    }
}
