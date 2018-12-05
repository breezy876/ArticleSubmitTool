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
    public class AdItem : InstantArticleItemModel
    {
        public AdItem()
        {
            Type = InstantArticleItemType.Ad;
            ItemTypeId = 6;
            Name = "Ad";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }


        public AdItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            var attrDic = Attributes.ToDictionary(a => a.Name, a => a);

            if (!attrDic["src"].Value.IsNullOrEmpty())
            {
                var attrs = new[]
                {
                    ToXAttribute(attrDic, "src"),
                    ToXAttribute(attrDic, "height"),
                    ToXAttribute(attrDic, "width")
                };
                var el = new XElement("figure", new XAttribute("op-ad", string.Empty), new XElement("iframe", attrs));
                return el;
            }
            else
            {
                var attrs = new[]
                {
                    ToXAttribute(attrDic, "height"),
                    ToXAttribute(attrDic, "width")
                };
                var el = new XElement("figure", new XAttribute("op-ad", string.Empty), new XElement("iframe", attrs, Content));
                return el;
            }

        }
    }
}
