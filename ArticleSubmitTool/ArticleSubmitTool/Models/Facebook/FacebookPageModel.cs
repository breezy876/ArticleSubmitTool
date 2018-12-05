using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain;

namespace ArticleSubmitTool.Models.InstantArticles
{
    public class FacebookPageModel
    {

        public int Id { get; set; }

        public string PageId { get; set; }

        public string Title { get; set; }

        public string URL { get; set; }

        public string AccessToken { get; set; }

        public long? UserId { get; set; }

        public string FacebookUserId { get; set; }

    }
}
