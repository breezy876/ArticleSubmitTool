using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Domain.Interfaces
{
    public interface IDeletable
    {
        DateTime? DateDeleted { get; set; }

        bool IsDeleted { get; set; }
        long? DeletedBy { get; set; }

        User DeletedByUser { get; set; }
    }
}
