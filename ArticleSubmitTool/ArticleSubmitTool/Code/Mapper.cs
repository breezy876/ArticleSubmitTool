using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Models;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;
using ArticleSubmitTool.Shared.Helpers;
using ArticleSubmitTool.Web.Models.InstantArticles;
using InstantArticleItemType = ArticleSubmitTool.Web.Models.InstantArticles.InstantArticleItemType;

namespace ArticleSubmitTool.Code
{
    public static class Mapper
    {

        public static void Initialize()
        {

            AutoMapper.Mapper.Initialize(cfg =>
            {
                #region model to vm mappings

                cfg.CreateMap<User, UserModel>();

                cfg.CreateMap<UserSetting, UserSettingModel>();

                cfg.CreateMap<FacebookPage, FacebookPageModel>();

                cfg.CreateMap<Domain.InstantArticleItemType, InstantArticleItemTypeModel>().MaxDepth(1);

                var attrDic = Common.ItemData.ToDictionary(data => data.Key,
                    data => data.Value.Attributes.ToDictionary(a => a.Name, a => a));

                cfg.CreateMap<InstantArticleItem, InstantArticleItemModel>().MaxDepth(2)
                    .ForMember(dest => dest.Attributes,
                        opt =>
                            opt.MapFrom(src => BinarySerializer.Deserialize<Dictionary<string, string>>(src.Attributes)
                                .Select(
                                    kvp =>
                                        new InstantArticleItemAttribute(
                                            attrDic[(InstantArticleItemType) src.ItemTypeId][kvp.Key])
                                        {
                                            Value = kvp.Value
                                        })))
                    .ForMember(dest => dest.Children,
                        opt =>
                            opt.MapFrom(
                                src =>
                                    src.Children.ToList()))
                     .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent));


                cfg.CreateMap<InstantArticle, InstantArticleModel>()
.ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => BinarySerializer.Deserialize<Dictionary<string, string>>(src.Attributes)))
.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.ToList()))
.ForMember(dest => dest.Authors, opt => opt.MapFrom(src => BinarySerializer.Deserialize<List<string>>(src.Authors)));


                #endregion

                #region vm to model mappings
                cfg.CreateMap<UserModel, User>();

                cfg.CreateMap<FacebookPageModel, FacebookPage>();

                cfg.CreateMap<UserSettingModel, UserSetting>();

                cfg.CreateMap<InstantArticleItemTypeModel, Domain.InstantArticleItemType>();

                cfg.CreateMap<InstantArticleItemModel, InstantArticleItem>().MaxDepth(2)
                    .ForMember(dest => dest.Attributes,
                        opt =>
                            opt.MapFrom(
                                src =>
                                    BinarySerializer.Serialize<Dictionary<string, string>>(
                                        src.Attributes.ToDictionary(a => a.Name, a => a.Value))))
               .ForMember(dest => dest.Children,
               opt => opt.MapFrom(src => src.Children.ToList()))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent));

                cfg.CreateMap<InstantArticleModel, InstantArticle>()
                    .ForMember(dest => dest.Attributes,
                        opt =>
                            opt.MapFrom(
                                src =>
                                    BinarySerializer.Serialize<Dictionary<string, string>>(
                                        src.Attributes.ToDictionary(a => a.Name, a => a.Value))))
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                    .ForMember(dest => dest.Authors,
                        opt =>
                            opt.MapFrom(
                                src =>
                                    BinarySerializer.Serialize<List<string>>(
                                        src.Authors.ToList())));

                #endregion

            });
        }

        public static IEnumerable<M> CreateModels<VM, M>(IEnumerable<VM> vmCol)
        {
            return vmCol.Select<VM, M>(vm => CreateModel<VM, M>(vm));
        }

        public static IEnumerable<VM> CreateViewModels<M, VM>(IEnumerable<M> mCol)
        {
            return mCol.Select<M, VM>(m => CreateViewModel<M, VM>(m));
        }

        public static M CreateModel<VM, M>(VM vm)
        {

            var model = AutoMapper.Mapper.Map<VM, M>(vm);

            return model;
        }

        public static VM CreateViewModel<M, VM>(M model)
        {

            var vm = AutoMapper.Mapper.Map<M, VM>(model);

            return vm;
        }


    }
}
