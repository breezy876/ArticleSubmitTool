using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Models;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;

namespace ArticleSubmitTool.Code
{
    public class SessionManager
    {
        public static UserModel User
        {
            get { return (UserModel)HttpContext.Current.Session["User"]; }
            set { HttpContext.Current.Session["User"] = value;  }
        }

        public static UserSettingModel UserSettings
        {
            get { return (UserSettingModel)HttpContext.Current.Session["UserSettings"]; }
            set { HttpContext.Current.Session["UserSettings"] = value; }
        }

        //public static string UserGuid
        //{
        //    get { return (string)HttpContext.Current.Session["UserGuid"]; }
        //    set { HttpContext.Current.Session["UserGuid"] = value; }
        //}

        /// <summary>
        /// The Facebook User ID
        /// </summary>
        public static string FacebookUserId { get; set; }


        /// <summary>
        /// The Facebook User ID
        /// </summary>
        public static string FacebookUserAccessToken { get; set; }

        /// <summary>
        /// The Facebook app access token
        /// </summary>
        public static string FacebookAppAccessToken { get; set; }


        public static string DisplayName => User == null ? string.Empty : $"{User.FirstName} {User.LastName}";

        public static bool IsLoggedIn => User != null && HttpContext.Current.User.Identity.IsAuthenticated;

        public static bool IsLoggedIntoFacebook => !FacebookAppAccessToken.IsNullOrEmpty();

        public static bool IsAdmin => User != null && Roles.IsUserInRole(User.UserName, "Admin");

        /// <summary>
        /// selected facebook page for publishing
        /// </summary>
        public static FacebookPageModel FacebookPage { get; set; }

        public static Dictionary<string, FacebookPageModel>  FacebookPages { get; set; }
    }
}
