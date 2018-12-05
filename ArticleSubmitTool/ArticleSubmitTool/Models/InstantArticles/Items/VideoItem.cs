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
    public class VideoItem : ItemWithCaption
    {
        public VideoItem()
        {
            Type = InstantArticleItemType.Video;
            ItemTypeId = 2;
            Name = "Video";
            //ItemType = new InstantArticleItemTypeModel() { Name = "Video", Id = 2 };
            Initialize();

        }


        public VideoItem(InstantArticleItemModel model) : base(model)
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
                //    //new XAttribute("data-fb-disable-360", attrDic["data-fb-disable-360"].Value),
                //    new XAttribute("data-fb-disable-controls", attrDic["data-fb-disable-controls"].Value),
                //    new XAttribute("loop", attrDic["loop"].Value),
                //    new XAttribute("data-fb-disable-autoplay", attrDic["data-fb-disable-autoplay"].Value),
                //};

                var el = new XElement(
                    "figure",
                    new XElement("video", attrs),
                    AddCaption(this));

                return el;
        }
    }
}
