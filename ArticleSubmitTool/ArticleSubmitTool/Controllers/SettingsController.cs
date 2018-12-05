using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Management;
using System.Web.Mvc;
using ArticleSubmitTool.Code;
using ArticleSubmitTool.Code.Facebook.API;
using ArticleSubmitTool.Code.Filters;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Domain.Interfaces;
using ArticleSubmitTool.Interfaces.Data;
using ArticleSubmitTool.Models;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;
using ArticleSubmitTool.Shared.Helpers;
using ArticleSubmitTool.Web.Models.InstantArticles;
using Newtonsoft.Json;
using InstantArticleItemType = ArticleSubmitTool.Web.Models.InstantArticles.InstantArticleItemType;

namespace ArticleSubmitTool.Controllers
{
    public class SettingsController : BaseController
    {

        private IDataRepository<UserSetting> _userSettingRepos;

        public SettingsController()
        {

        }

        [AuthorizedFilter]
        public ActionResult Index()
        {

            //fetch page info

            var context = new DataContext();

            var fbPageRepos = new EFDataRepository<FacebookPage>(context)
            {
                ExcludeDeleted = true,
                IncludeRelated = true,
                SaveChanges = true,
                DeletePermanent = false
            };

            var pages = fbPageRepos.Query(u => u.UserId == SessionManager.User.Id);

            var pageModels = pages.IsNullOrEmpty() ? new List<FacebookPageModel>() : pages.Select(
                                p =>
                                    (FacebookPageModel)
                                        AutoMapper.Mapper.Map(p, typeof(FacebookPage),
                                            typeof(FacebookPageModel))).ToList();

            var model = new UserSettingsPageModel(SessionManager.UserSettings)
            {
                FacebookPageDDL = !SessionManager.IsLoggedIntoFacebook || pageModels.IsNullOrEmpty() ? 
                new Dictionary<long, string>() 
                : pageModels.ToDictionary(p => Int64.Parse(p.PageId), p => p.Title)
            };

            return View(model);
        }

        [AuthorizedFilter]
        [HttpPost]
        public ContentResult Save(string settingsJSON)
        {
            //var model = new InstantArticlesPageModel()
            //{
            //    InstantArticles = _GetViewModels()
            //};

            UserSettingModel model;

            try
            {
                model = JSONSerializer.Deserialize<UserSettingModel>(settingsJSON);

                var context = new DataContext();

                var userSettingRepos = new EFDataRepository<UserSetting>(context)
                {
                    ExcludeDeleted = true,
                    IncludeRelated = true,
                    SaveChanges = true,
                    DeletePermanent = false
                };

                var userSettings =
                    (UserSetting) AutoMapper.Mapper.Map(model, typeof (UserSettingModel), typeof (UserSetting));

                userSettingRepos.AddOrUpdate((IDataEntity) userSettings);

                SessionManager.UserSettings = model;


                var fbPageRepos = new EFDataRepository<FacebookPage>(context)
                {
                    ExcludeDeleted = true,
                    IncludeRelated = true,
                    SaveChanges = true,
                    DeletePermanent = false
                };

                var page = fbPageRepos.Query(u => u.UserId == SessionManager.User.Id).FirstOrDefault();


                SessionManager.FacebookPage =
                    (FacebookPageModel) AutoMapper.Mapper.Map(page, typeof (FacebookPage), typeof (FacebookPageModel));
            }

            catch (Exception ex)
            {
                return _ConvertToJSON(new {Success = false});
            }
            return _ConvertToJSON(new { Success = true, Settings = model });
        }


        private ContentResult _ConvertToJSON(object obj)
        {

            var json = JsonConvert.SerializeObject(obj,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Content(json, "application/json");
        }

    }
}
