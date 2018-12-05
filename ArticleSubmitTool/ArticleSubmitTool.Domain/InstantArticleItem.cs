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
    [Table("InstantArticleItem")]
    public partial class InstantArticleItem : ATrackableEntity
    {

        [Required()]
        [ForeignKey("InstantArticle")]
        public long InstantArticleId { get; set; }

        public virtual InstantArticle InstantArticle { get; set; }

        [Required()]
        [ForeignKey("ItemType")]
        public long ItemTypeId { get; set; }

        public virtual InstantArticleItemType ItemType { get; set; }

        public byte[] Attributes { get; set; }

        public string Content { get; set; }

        public bool IsHeader { get; set; }

        public bool IsCaption { get; set; }

        [ForeignKey("Parent")]
        public long? ParentId { get; set; }
        public virtual InstantArticleItem Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstantArticleItem> Children { get; set; }

    }
}
