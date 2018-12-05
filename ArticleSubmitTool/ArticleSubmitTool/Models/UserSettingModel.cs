using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain;

namespace ArticleSubmitTool.Models
{
    public class UserSettingModel
    {
            //the facebook page to publish articles to
            public string FacebookPageId { get; set; }

            public long? UserId { get; set; }

            public UserModel User { get; set; }
    }
}
