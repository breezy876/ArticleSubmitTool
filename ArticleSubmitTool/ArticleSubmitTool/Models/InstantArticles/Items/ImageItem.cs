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
    public class ImageItem : ItemWithCaption
    {
        public ImageItem()
        {
            Type = InstantArticleItemType.Image;
            ItemTypeId = 1;
            Name = "Image";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Image", Id = 1};
            Initialize();
        }

        public ImageItem(InstantArticleItemModel model) : base(model)
        {

        }

        public override XElement ToXElement()
        {
            var attrDic = Attributes.ToDictionary(a => a.Name, a => a);

            var attrs = InstantArticleItemModel.ToXAttribute(attrDic);

            //var attrs = new[]
            //{
            //    new XAttribute("src", attrDic["src"].Value),
            //    new XAttribute("data-mode", attrDic["data-mode"].Value),
            //    new XAttribute("data-fb-disable-360", attrDic["data-fb-disable-360"].Value),
            //};

            var el = new XElement(
                    "figure",
                    new XElement("img", attrs),
                    AddCaption(this));

                return el;
        }
    }
}
