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
    [Table("InstantArticleItemType")]
    public partial class InstantArticleItemType: ADataEntity
    {
        [Required()]
        [StringLength(100)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstantArticleItemType()
        {
            Items = new HashSet<InstantArticleItem>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstantArticleItem> Items { get; set; }
    }
}
