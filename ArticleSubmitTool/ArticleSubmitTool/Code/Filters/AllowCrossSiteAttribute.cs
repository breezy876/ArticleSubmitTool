﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArticleSubmitTool.Code.Filters
{
    public class AllowCrossSiteAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }
}
