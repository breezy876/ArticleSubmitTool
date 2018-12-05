using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleSubmitTool.Web.Models.InstantArticles;
using ArticleSubmitTool.Web.Models.InstantArticles.Items;

namespace ArticleSubmitTool.Models.InstantArticles.Items
{
    public class ItemWithCaption : InstantArticleItemModel 
    {

        public ItemWithCaption()  
        {
            Children = new List<InstantArticleItemModel>()
            {
                new CaptionItem()
            };
        }

        public ItemWithCaption(InstantArticleItemModel model) : base(model)
        {

        }

        protected XElement AddCaption(InstantArticleItemModel item)
        {
            var caption = (CaptionItem)item.Children.First(c => c.IsCaption);
            var captionAttrDic = caption.Attributes.ToDictionary(a => a.Name, a => a);

            return new XElement("figcaption",
                new XElement("h1", GetAttributeValue(captionAttrDic["title"])),
                new XElement("cite", GetAttributeValue(captionAttrDic["credits"])));

        }
    }
}
