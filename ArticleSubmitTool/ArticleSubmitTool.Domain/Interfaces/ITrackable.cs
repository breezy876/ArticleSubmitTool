using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Domain.Interfaces
{
   public interface ITrackable
    {
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }

        long? CreatedBy { get; set; }
        long? ModifiedBy { get; set; }

        User CreatedByUser { get; set; }
        User ModifiedByUser { get; set; }
    }
}
