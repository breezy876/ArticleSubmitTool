using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Models.InstantArticles
{
    public class NameTitle
    {
        public NameTitle()
        {
            
        }

        public NameTitle(NameTitle obj)
        {
            Name = obj.Name;
            Title = obj.Title;
        }

        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class InstantArticleItemAttribute : NameTitle
    {
        public InstantArticleItemAttribute()
        {
        }

        public InstantArticleItemAttribute(InstantArticleItemAttribute attr) : base (attr)
        {
            Type = attr.Type;
        }

        public string Value { get; set; }
        public int Type { get; set; }
    }
}
