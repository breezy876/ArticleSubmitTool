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
    public class BlockQuoteItem : InstantArticleItemModel
    {
        public BlockQuoteItem()
        {
            Type = InstantArticleItemType.BlockQuote;
            ItemTypeId = 5;
            Name = "BlockQuote";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }

        public BlockQuoteItem(InstantArticleItemModel model) : base(model)
        {
            
        }


        public override XElement ToXElement()
        {

            var el = new XElement("blockquote", Content.IsNullOrEmpty() ? string.Empty : Content);
            return el;
        }
    }
}
