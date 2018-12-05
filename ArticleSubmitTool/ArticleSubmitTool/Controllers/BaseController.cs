using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArticleSubmitTool.Controllers
{
    public class BaseController : Controller
    {
        public new RedirectToRouteResult RedirectToAction(string actionName, string controllerName, object routeVals)
        {
            return base.RedirectToAction(actionName, controllerName, routeVals);
        }

        public new RedirectResult Redirect(string url)
        {
           return base.Redirect(url);
        }
    }
}
