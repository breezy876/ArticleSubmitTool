using System.Web;
using System.Web.Optimization;

namespace ArticleSubmitTool
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/Angular/angular.js",
                "~/Scripts/Angular/angular-sanitize.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                      "~/Scripts/materialize/materialize.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include("~/Scripts/tinymce/tinymce.js"));

            bundles.Add(new ScriptBundle("~/bundles/linq").Include("~/Scripts/linq.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/materialize/css/materialize.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));
        }
    }
}
