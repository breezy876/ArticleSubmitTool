using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArticleSubmitTool.Controllers;

namespace ArticleSubmitTool.Code.Filters
{
public class AdminActionFilter : System.Web.Mvc.ActionFilterAttribute
    {
    void OnActionExecuting(ActionExecutingContext filterContext)
    {

        if (!SessionManager.IsAdmin)
        {
            BaseController controller = filterContext.Controller as BaseController;

            if (controller != null)
            {
                controller.Redirect("/Views/Shared/NoPermission.cshtml");
            }
        }

        this.OnActionExecuting(filterContext);
    }
}
}
