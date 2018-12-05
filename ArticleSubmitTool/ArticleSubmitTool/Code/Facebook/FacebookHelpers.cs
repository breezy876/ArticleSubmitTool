using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Domain.Interfaces;
using ArticleSubmitTool.Models.InstantArticles;
using Facebook;

namespace ArticleSubmitTool.Code.Facebook
{
    public class FacebookHelpers
    {
        public static bool UpdateFacebookSessionInfo(string accessToken, string userId)
        {
            try
            {
                SessionManager.FacebookUserAccessToken = accessToken;
                SessionManager.FacebookUserId = userId;

                //get/save pages info to db
                var fb = new FacebookClient(accessToken);
                dynamic result = fb.Get($"{userId}/accounts");

                var pageList = new List<FacebookPageModel>();

                foreach (dynamic page in result.data)
                {
                    pageList.Add(new FacebookPageModel()
                    {
                        PageId = page.id,
                        Title = page.name,
                        AccessToken = page.access_token,
                        FacebookUserId = userId,
                        UserId = SessionManager.User.Id
                    });
                }

                var db = new DataContext();

                var fbPageRepos = new EFDataRepository<FacebookPage>(db)
                {
                    ExcludeDeleted = true,
                    IncludeRelated = true,
                    SaveChanges = true,
                    DeletePermanent = true
                };


                //remove old pages
                var oldIds = fbPageRepos.Query(p => p.UserId == SessionManager.User.Id).Select(p => p.Id).ToList();
                fbPageRepos.Remove(oldIds);

                //add new
                fbPageRepos.AddOrUpdate(
                    pageList.Select(
                        p =>
                            (FacebookPage)
                                AutoMapper.Mapper.Map(p, typeof(FacebookPageModel),
                                    typeof(FacebookPage))).Cast<IDataEntity>());


                //get/save app access token to session
                fb = new FacebookClient(accessToken);
                dynamic parameters = new ExpandoObject();
                parameters.grant_type = "fb_exchange_token";
                parameters.client_id = Settings.FacebookAppId;
                parameters.client_secret = Settings.FacebookAppSecret;
                parameters.fb_exchange_token = accessToken;
                result = fb.Get($"oauth/access_token", parameters);

                SessionManager.FacebookAppAccessToken = result.access_token;
            }

            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
