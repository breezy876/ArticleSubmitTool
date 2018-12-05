using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Domain
{
    public class UserSetting: ADataEntity
    {
        //the facebook page to publish articles to
        public string FacebookPageId { get; set; }

        [Required()]
        [ForeignKey("User")]
        public long? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
