using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml.Linq;
using ArticleSubmitTool.Code.Facebook.API;
using ArticleSubmitTool.Models;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;
using Microsoft.Owin.Security;

namespace ArticleSubmitTool.Web.Models.InstantArticles
{
    public class InstantArticleModel : AModel, IMarkupItem
    {

        public string Title { get; set; }

        public string URL { get; set; }

        /// <summary>
        /// facebook page Id; if set, article is synced with user's facebook page
        /// </summary>
        public long? FacebookPageId { get; set; }

        public FacebookPageModel Page { get; set; }

        public bool IsPublished { get; set; }

        public List<InstantArticleItemModel> Items { get; set; }

        public List<InstantArticleItemAttribute> Attributes { get; set; }


        public string SubTitle { get; set; }

        public string Kicker { get; set; }


        public List<string> Authors { get; set; }

        public string Copyright { get; set; }
        public string Credits { get; set; }

        public string RelatedArticles { get; set; }

        public DateTime? DatePublished { get; set; }
        public DateTime? DateModified { get; set; }

        public string NewsFeedTitle { get; set; }
        public string NewsFeedDescription { get; set; }
        public string NewsFeedImage { get; set; }

        public bool AutoAdPlacement { get; set; }

        public string Style { get; set; }
        public string Version { get; set; }

        public InstantArticleModel()
        {
            Attributes = new List<InstantArticleItemAttribute>();
            Items = new List<InstantArticleItemModel>();
            Authors = new List<string>();
        }

        public XElement ToXElement()
        {

            var headElems = new[]
            {
                new XElement("link", new XAttribute("rel", "canonical"), new XAttribute("href", URL)),

                new XElement("meta", new XAttribute("charset", "utf-8")),

                Style.IsNullOrEmpty() ? null : new XElement("meta", new XAttribute("property", "fb:article_style"), new XAttribute("content", Style)),
                new XElement("meta", new XAttribute("property", "fb:use_automatic_ad_placement"),new XAttribute("content", AutoAdPlacement)),
                Version.IsNullOrEmpty() ? null : new XElement("meta", new XAttribute("property", "op:version"),new XAttribute("content", Version)),

                NewsFeedTitle.IsNullOrEmpty() ? null : new XElement("meta", new XAttribute("property", "og:title"),new XAttribute("content", NewsFeedTitle)),
                NewsFeedDescription.IsNullOrEmpty() ? null :  new XElement("meta", new XAttribute("property", "og:description"),new XAttribute("content", NewsFeedDescription)),
                NewsFeedImage.IsNullOrEmpty() ? null :   new XElement("meta", new XAttribute("property", "og:image"),new XAttribute("content", NewsFeedImage)),

                new XElement("title", Title),
            };

            var headerMedia = Items.FirstOrDefault(i => i.IsHeader && i.Type == InstantArticleItemType.Video || i.Type == InstantArticleItemType.Image);

            var headerMediaElem = headerMedia == null ? null : InstantArticleModelFactory.CreateFrom((int)headerMedia.ItemTypeId, headerMedia).ToXElement();


            var headerAds =
                Items.Where(i => i.IsHeader && i.Type == InstantArticleItemType.Ad)
                    .Select(i => InstantArticleModelFactory.CreateFrom((int)i.ItemTypeId, i))
                    .ToArray();

           var headerAdElem = headerAds.IsNullOrEmpty()
                ? null
                : headerAds.Length == 1
                    ? headerAds.First().ToXElement()
                    : new XElement("section", new XAttribute("class", "op-ad-template"),
                        headerAds.Select(a => a.ToXElement()));

            var headerElems = new object[]
            {
                headerAdElem,
                headerMediaElem,
                new XElement("h1", Title),
                new XElement("h2", SubTitle),
                new XElement("h3", new XAttribute("class", "op-kicker"), Kicker),
                Authors.IsNullOrEmpty() ? null : Authors.Select(a => new XElement("address", a)).ToArray(),
                !DatePublished.HasValue ? null : new XElement("time", new XAttribute("property", "op-published"),
                    new XAttribute("datettime", DatePublished)),
                  !DateModified.HasValue ? null : new XElement("time", new XAttribute("property", "op-modified"),
                    new XAttribute("datettime", DateModified)),
            };

            var bodyElems = Items.Where(i => !i.IsHeader && !i.IsCaption)
                .Select(i => InstantArticleModelFactory.CreateFrom((int)i.ItemTypeId, i))
                .Select(i => i.ToXElement()).ToArray();

            var footerElems = new object[]
            {
                Credits.IsNullOrEmpty() ? null : new XElement("aside", Credits),
                Copyright.IsNullOrEmpty() ? null :  new XElement("small", Copyright)
            };

            return new XElement("html",
                new XElement("head", headElems),
                new XElement("body",
                    new XElement("article",
                        new XElement("header", headerElems),
                        bodyElems,
                        new XElement("footer", footerElems)
                        )));
        }
    }
}
