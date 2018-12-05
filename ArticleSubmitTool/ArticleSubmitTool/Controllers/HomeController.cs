using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArticleSubmitTool.Code.Filters;

namespace ArticleSubmitTool.Controllers
{
    public class HomeController : BaseController
    {
        [AuthorizedFilter]
        public ActionResult Index()
        {
            return View();
        }



    }
}