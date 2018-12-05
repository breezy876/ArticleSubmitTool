using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ArticleSubmitTool.Code;
using ArticleSubmitTool.Code.InstantArticles;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared.Helpers;
using ArticleSubmitTool.Web.Models.InstantArticles;

namespace ArticleSubmitTool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var fileJson = Shared.Helpers.IO.FileHelpers.ReadToEnd(
                string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory,"InstantArticleItems.json"));

            Common.ItemData =
                JSONSerializer
                    .Deserialize
                    <
                        Dictionary
                            <ArticleSubmitTool.Web.Models.InstantArticles.InstantArticleItemType, InstantArticleItemData
                                >>(fileJson);

   
            Mapper.Initialize();
        }
    }
}
