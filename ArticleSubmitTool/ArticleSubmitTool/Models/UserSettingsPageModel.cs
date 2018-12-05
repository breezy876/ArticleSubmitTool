using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ArticleSubmitTool.Models
{
    public class UserSettingsPageModel : UserSettingModel
    {
        public UserSettingsPageModel()
        {
            
        }

        public UserSettingsPageModel(UserSettingModel model)
        {
            FacebookPageId = model.FacebookPageId;
            UserId = model.UserId;
        }

        public Dictionary<long, string>  FacebookPageDDL { get; set; }
    }
}
