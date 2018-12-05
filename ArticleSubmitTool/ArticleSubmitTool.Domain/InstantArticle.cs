using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Domain
{
    [Serializable()]
    [Table("InstantArticle")]
    public partial class InstantArticle : ATrackableEntity
    {

        public InstantArticle()
        {
          
        }

        [Required()]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string SubTitle { get; set; }

        [StringLength(100)]
        public string Kicker { get; set; }

        [StringLength(255)]
        public string URL { get; set; }

        public byte[] Attributes { get; set; }

        [ForeignKey("Page")]
        public long? FacebookPageId { get; set; }

        public virtual FacebookPage Page { get; set; }

        public bool IsPublished { get; set; }

        public byte[] Authors { get; set; }

        public string Copyright { get; set; }
        public string Credits { get; set; }
        public string RelatedArticles { get; set; }

        public string NewsFeedTitle { get; set; }
        public string NewsFeedDescription { get; set; }
        public string NewsFeedImage { get; set; }

        public int? FacebookFeedback { get; set; }
        public bool ArticleFeedbackEnabled { get; set; }

        public DateTime? DatePublished { get; set; }

        //[ForeignKey("HeaderMedia")]
        //public long? HeaderMediaId { get; set; }
        //public virtual InstantArticleItem HeaderMedia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstantArticleItem> Items { get; set; }
    }
}
