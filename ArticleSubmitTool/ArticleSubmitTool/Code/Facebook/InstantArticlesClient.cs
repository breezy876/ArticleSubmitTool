using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Web.Models.InstantArticles;
using Facebook;

namespace ArticleSubmitTool.Code.Facebook.API
{
    /// <summary>
    /// all web-based facebook instant articles APIs
    /// fetches article view models
    /// </summary>
    public class InstantArticlesClient
    {
        public string AccessToken { get; set; }

        /// <summary>
        /// publish article to facebook instant articles
        /// </summary>
        /// <param name="article"></param>
        public void Publish(string pageId, string articleMarkup)
        {

            var fb = new FacebookClient(AccessToken);
            dynamic parameters = new ExpandoObject();
            parameters.html_source = articleMarkup;
            parameters.access_token = SessionManager.FacebookPage.AccessToken;
            parameters.publish = true;
            dynamic result = fb.Post($"{pageId}/instant_articles", parameters);
           
        }

        public InstantArticleModel Get(string articleId)
        {
            //var fb = new FacebookClient(AccessToken);
            //dynamic result = fb.Get(articleId);

            throw new NotImplementedException();

        }

        public IEnumerable<InstantArticleModel> GetAll(string pageId)
        {
            throw new NotImplementedException();
        }
    }
}
