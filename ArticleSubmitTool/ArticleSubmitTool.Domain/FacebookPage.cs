using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Domain
{
    [Serializable()]
    [Table("FacebookPage")]
    public partial class FacebookPage : ADataEntity
    {
      
        [Required()]
        public string PageId { get; set; }

        public string URL { get; set; }

        [Required()]
        public string Title { get; set; }

        [Required()]
        public string AccessToken { get; set; }

        [Required()]
        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual User User { get; set; }

        [Required()]
        public string FacebookUserId { get; set; }

    }
}
