using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArticleSubmitTool.Controllers;

namespace ArticleSubmitTool.Code.Filters
{
public class AuthorizedFilter : System.Web.Mvc.ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        //var inExclude = filterContext.HttpContext.Request.RawUrl.Contains("/Account/Login") ||
        //        filterContext.HttpContext.Request.RawUrl.Contains("/Home/Index"); 

        var retUrl = filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath;

            if (!SessionManager.IsLoggedIn)
        {
            BaseController controller = filterContext.Controller as BaseController;

            if (controller != null)
            {
                    filterContext.Result = controller.RedirectToAction("Login", "Account", new { returnUrl = retUrl});
            }
        }

        base.OnActionExecuting(filterContext);
    }
}
}
