using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain.Interfaces;

namespace ArticleSubmitTool.Domain
{
    [Serializable()]
    public abstract class ADataEntity : IDataEntity
    {
        [Required()]
        [Key()]
        public long Id { get; set; }
    }
}
