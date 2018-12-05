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
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;
using ArticleSubmitTool.Shared.Helpers;
using ArticleSubmitTool.Web.Models.InstantArticles;
using Newtonsoft.Json;
using InstantArticleItemType = ArticleSubmitTool.Web.Models.InstantArticles.InstantArticleItemType;

namespace ArticleSubmitTool.Controllers
{
    public class ArticleController : BaseController
    {

        private IDataRepository<InstantArticle> _articleRepos;
        private IDataRepository<InstantArticleItem> _articleItemRepos;

        public ArticleController()
        {
            var context = new DataContext();

            _articleRepos = new EFDataRepository<InstantArticle>(context)
            {
                ExcludeDeleted = true,
                IncludeRelated = true,
                SaveChanges = true,
                DeletePermanent = false
            };

            _articleItemRepos = new EFDataRepository<InstantArticleItem>(context)
            {
                ExcludeDeleted = true,
                IncludeRelated = true,
                SaveChanges = true,
                DeletePermanent = false
            };
        }

        [AuthorizedFilter]
        public ActionResult Index()
        {
            var model = new InstantArticlesPageModel()
            {
                InstantArticles = _GetViewModels()
            };

            return View(model);
        }

        [AuthorizedFilter]
        public ActionResult Preview(long Id)
        {

            var articleModel = _GetArticle(Id);

            if (articleModel == null)
            {
                return Redirect("/Shared/Error.cshtml");
            }

            var articleVm = _CreateArticleViewModel(articleModel);

            if (articleVm == null)
            {
                return Redirect("/Shared/Error.cshtml");
            }

            var articleMarkup = articleVm.ToXElement().ToString();

            ViewData["articleMarkup"] = articleMarkup;

            return View();

        }


        [AuthorizedFilter]
        public ActionResult Detail(int? id)
        {
            InstantArticleModel articleVm;

            if (id.HasValue)
            {
                ViewData["IsEdit"] = true;

                var articleModel = _GetArticle(id.Value);
                    
                if (articleModel == null)
                {
                    return Redirect("/Shared/Error.cshtml");
                }

                articleVm = _CreateArticleViewModel(articleModel);

            }
            else
            {
                ViewData["IsEdit"] = false;
                articleVm = new InstantArticleModel();
            }

            return View(articleVm);
        }




        public ContentResult Delete(long articleId)
        {
            var articleModels = new List< InstantArticleModel>();
            try
            {
                _articleRepos.Remove(articleId);
                articleModels = _GetViewModels();
            }
            catch
            {
                return _ConvertToJSON(new { Success = false});
            }
            return _ConvertToJSON(new { Success = true, Items = articleModels });
        }

        [HttpPost()]
        public ContentResult DeleteItem(InstantArticleItemModel item)
        {
            var itemModels = new List<InstantArticleItemModel>();
            try
            {
                //remove children first
                if (!item.Children.IsNullOrEmpty())
                {
                    _articleItemRepos.Remove(item.Children.Select(c => c.Id).ToList());
                }
                _articleItemRepos.Remove(item.Id);
                itemModels = _GetItems(item.InstantArticleId);
            }
            catch
            {
                return _ConvertToJSON(new { Success = false });
            }


            return _ConvertToJSON(new {Success = true, Items = itemModels});
        }


        [HttpPost()]
        public ContentResult Save(string articleJSON)
        {
            InstantArticleModel articleVm;

            try
            {
                var article = JSONSerializer.Deserialize<InstantArticleModel>(articleJSON);

                var articleModel = (InstantArticle)AutoMapper.Mapper.Map(article, typeof (InstantArticleModel), typeof (InstantArticle));

                var articleModelItems = articleModel.Items.ToList().DeepClone();


                //add/update article
                articleModel.Items = null;
                _articleRepos.AddOrUpdate((IDataEntity)articleModel);

                articleModelItems.ForEach(i => i.InstantArticleId = articleModel.Id);

                //update article items
                if (!articleModelItems.IsNullOrEmpty())
                {
              
                    var articleModelItemNoChildren = articleModelItems.DeepClone();

                    foreach (var item in articleModelItemNoChildren)
                    {
                        item.Parent = null;
                        item.Children = null;
                    }

                    _articleItemRepos.AddOrUpdate(articleModelItemNoChildren.Cast<IDataEntity>());
                    var savedItems = articleModelItemNoChildren.ToList();

                    var index = 0;
                    //add/update child items 
                    foreach (var item in articleModelItems)
                    {
                        var savedItem = savedItems[index];
                        if (!item.Children.IsNullOrEmpty())
                        {

                            foreach (var child in item.Children)
                            {
                                child.ParentId = savedItem.Id;
                                child.Parent = null;
                                child.InstantArticleId = savedItem.InstantArticleId;
                            }

                            _articleItemRepos.AddOrUpdate(item.Children.Cast<IDataEntity>());
                        }
                        index++;
                    }

                }

         

                articleVm = (InstantArticleModel)AutoMapper.Mapper.Map(articleModel, typeof(InstantArticle), typeof(InstantArticleModel));

                if (!articleModelItems.IsNullOrEmpty())
                {
                    articleVm.Items = articleModelItems
                        .Select(i => (InstantArticleItemModel)AutoMapper.Mapper.Map(i, typeof (InstantArticleItem),typeof (InstantArticleItemModel)))
                        .Select(i => _CreateItemViewModel(i)).ToList();

                }
            }
            catch (Exception ex)
            {
                return _ConvertToJSON(new { Success = false });
            }
            return _ConvertToJSON(new { Success = true, InstantArticle = articleVm });
        }

        [AuthorizedFilter]
        public ActionResult Publish(long articleId)
        {
            //fetch instant article from db 
            //create article model
            //convert to markup and post
            var articleModel = _GetArticle(articleId);

            if (articleModel == null)
            {
                return Redirect("/Shared/Error.cshtml");
            }

            var articleVm = _CreateArticleViewModel(articleModel);

            if (articleVm == null)
            {
                return Redirect("/Shared/Error.cshtml");
            }
            var articleMarkup = articleVm.ToXElement().ToString();

            return View(articleMarkup);

        }

        [HttpPost()]
        public ContentResult CreateItem(int type, long articleId)
        {
            var item = InstantArticleModelFactory.Create(type);
            item.InstantArticleId = articleId;

            if (!item.Children.IsNullOrEmpty())
            {
                foreach (var child in item.Children)
                {
                    child.InstantArticleId = articleId;
                }
            }

            return _ConvertToJSON(item);
        }

        #region private methods
        private InstantArticle _GetArticle(long articleId)
        {
            var article = _articleRepos.Get(articleId);

            if (article == null)
            {
                return null;
            }

            var items = _articleItemRepos.Query(i => !i.IsDeleted && i.InstantArticleId == articleId);

            if (article.Items.IsNullOrEmpty())
            {
                article.Items = null;
            }
            else
            {
                article.Items = items.ToList();
            }

            return article;
        }

        private InstantArticleModel _CreateArticleViewModel(InstantArticle model)
        {
            var articleVm = Mapper.CreateViewModel<InstantArticle, InstantArticleModel>(model);

            if (!articleVm.Items.IsNullOrEmpty())
            {
                articleVm.Items = articleVm.Items.Select(i => _CreateItemViewModel(i)).ToList();
            }

            return articleVm;
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

        private InstantArticleItemModel _CreateItemChildViewModel(InstantArticleItemModel model)
        {
            var type = (InstantArticleItemType)model.ItemTypeId;
            var vm = InstantArticleModelFactory.CreateFrom((int)model.ItemTypeId, model);


            vm.Name = InstantArticleModelFactory.Items[type].Name;
            vm.Type = (InstantArticleItemType) model.ItemTypeId;
            vm.AttributeValues = Common.ItemData.ContainsKey(type) ? Common.ItemData[type].AttributeValues : null;

            return vm;

        }

        private InstantArticleItemModel _CreateItemViewModel(InstantArticleItemModel model)
        {
            var type = (InstantArticleItemType) model.ItemTypeId;
            return new InstantArticleItemModel(model)
            {
                Name = InstantArticleModelFactory.Items[type].Name,
                Type = (InstantArticleItemType)model.ItemTypeId,
                AttributeValues = Common.ItemData.ContainsKey(type) ? Common.ItemData[type].AttributeValues : null,
                Children = model.Children.IsNullOrEmpty() ? null : model.Children.Select(c => _CreateItemChildViewModel(c)).ToList()
            };
        }

        private List<InstantArticleModel> _GetViewModels()
        {
            var articles = _articleRepos.GetAll().ToList();
            var articleModels = articles.Select(a => Mapper.CreateViewModel<InstantArticle, InstantArticleModel>(a)).ToList();
            return articleModels;
        }

        private List<InstantArticleItemModel> _GetItems(long articleId)
        {
            var articleItems = _articleItemRepos.Query(i => i.InstantArticleId == articleId).ToList();
            var articleItemModels = articleItems.Select(i => Mapper.CreateViewModel<InstantArticleItem, InstantArticleItemModel>(i)).ToList();

            articleItemModels = articleItemModels.Select(i => _CreateItemViewModel(i)).ToList();

            return articleItemModels;
        }
        #endregion

    }
}
