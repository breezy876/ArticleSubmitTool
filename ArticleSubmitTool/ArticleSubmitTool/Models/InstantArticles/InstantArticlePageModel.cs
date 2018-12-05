using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Web.Models.InstantArticles;

namespace ArticleSubmitTool.Models.InstantArticles
{
    public class InstantArticlePageModel
    {
        public Dictionary<int, string> InstantArticleItemNames { get; set; }
        public InstantArticleModel InstantArticle { get; set; }
    }
}
