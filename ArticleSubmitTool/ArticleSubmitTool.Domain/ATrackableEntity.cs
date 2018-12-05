using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain.Interfaces;

namespace ArticleSubmitTool.Domain
{
    [Serializable()]
    public abstract class ATrackableEntity : ADataEntity, ITrackable, IDeletable
    {
        [ForeignKey("CreatedByUser")]
        public long? CreatedBy { get; set; }

        public virtual User CreatedByUser { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime? DateDeleted { get; set; }


        [ForeignKey("DeletedByUser")]
        public long? DeletedBy { get; set; }

        public virtual User DeletedByUser { get; set; }


        [Required()]
        public bool IsDeleted { get; set; }


        [ForeignKey("ModifiedByUser")]
        public long? ModifiedBy { get; set; }

        public virtual User ModifiedByUser { get; set; }


    }
}
