using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Models.InstantArticles.Items;
using ArticleSubmitTool.Shared;

namespace ArticleSubmitTool.Web.Models.InstantArticles.Items
{
    public class MapItem : ItemWithCaption
    {
        public MapItem()
        {
            Type = InstantArticleItemType.Map;
            ItemTypeId = 7;
            Name = "Map";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }

        public MapItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            var attrDic = Attributes.ToDictionary(a => a.Name, a => a);

            var el = new XElement("figure",
                new XAttribute("op-map", null),
                new XElement("script", new XAttribute("type", "application/json"), GetAttributeValue(attrDic["geoJSON"])),
                AddCaption(this));

            return el;
        }
    }
}
