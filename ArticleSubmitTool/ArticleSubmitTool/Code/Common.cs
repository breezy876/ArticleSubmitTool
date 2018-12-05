using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Code.InstantArticles;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Models.InstantArticles;

namespace ArticleSubmitTool.Code
{
    public static class Common
    {
        public static Dictionary<ArticleSubmitTool.Web.Models.InstantArticles.InstantArticleItemType, InstantArticleItemData> ItemData { get; set; }
    }
}
