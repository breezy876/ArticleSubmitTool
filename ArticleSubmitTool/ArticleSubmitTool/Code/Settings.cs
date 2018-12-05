using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Code
{
    public class Settings
    {
        public static string FacebookAppId => System.Configuration.ConfigurationManager.AppSettings["FacebookAppId"];
        public static string FacebookAppSecret => System.Configuration.ConfigurationManager.AppSettings["FacebookAppSecret"];

        public static string AdminUser => System.Configuration.ConfigurationManager.AppSettings["AdminUser"];
        public static string AdminPassword => System.Configuration.ConfigurationManager.AppSettings["AdminPassword"];
        public static string AdminEmail => System.Configuration.ConfigurationManager.AppSettings["AdminEmail"];

        public static string AdminFirstName => System.Configuration.ConfigurationManager.AppSettings["AdminFirstName"];
        public static string AdminLastName => System.Configuration.ConfigurationManager.AppSettings["AdminLastName"];

    }
}
