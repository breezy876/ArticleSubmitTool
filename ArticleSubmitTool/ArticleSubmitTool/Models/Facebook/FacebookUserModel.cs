using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain;

namespace ArticleSubmitTool.Models.InstantArticles
{
    public class FacebookUserModel : AModel
    {
        public long FacebookUserId { get; set; }

        public long? UserId { get; set; }

        public UserModel User { get; set; }
    }
}
