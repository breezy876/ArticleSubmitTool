using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleSubmitTool.Shared;
using ArticleSubmitTool.Web.Models.InstantArticles;
using ArticleSubmitTool.Web.Models.InstantArticles.Items;

namespace ArticleSubmitTool.Models.InstantArticles.Items.Embeds
{
    public class EmbedItem : ItemWithCaption
    {

        public EmbedItem()
        {
            
        }

        public EmbedItem(InstantArticleItemModel model) : base(model)
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

                var el = new XElement(
                    "figure",
                    new XElement("iframe", attrs),
                    AddCaption(this));

                return el;
            }
            else
            {
                    var attrs = new[]
                    {
                        ToXAttribute(attrDic, "height"),
                        ToXAttribute(attrDic, "width")
                    };

                    var el = new XElement(
                        "figure",
                        new XAttribute("op-interactive", string.Empty),
                        new XElement("iframe", attrs, Content),
                        AddCaption(this));

                    return el;
            }
            return null;
        }

    }
}
