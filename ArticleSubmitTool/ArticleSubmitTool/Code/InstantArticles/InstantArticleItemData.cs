using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Models.InstantArticles;

namespace ArticleSubmitTool.Code.InstantArticles
{
    public class InstantArticleItemData
    {
        public List<InstantArticleItemAttribute> Attributes { get; set; }
        public Dictionary<string, List<NameTitle>> AttributeValues { get; set; }
    }
}
