using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArticleSubmitTool.Web.Models.InstantArticles

{ 
    public enum InstantArticleItemType
    {
        Image = 1,
        Video,
        BodyText,
        PullQuote,
        BlockQuote,
        Ad,
        Map,
        Twitter,
        Facebook,
        Instagram,
        YouTube,
        Caption,
        SlideShow
    }

    public interface IMarkupItem
    {
        XElement ToXElement();
    }
}
