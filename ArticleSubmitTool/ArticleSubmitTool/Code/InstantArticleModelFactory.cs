using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ArticleSubmitTool.Web.Models.InstantArticles;
using ArticleSubmitTool.Web.Models.InstantArticles.Items;

namespace ArticleSubmitTool.Code.Facebook.API
{
    /// <summary>
    /// builds instant article view model from domain model
    /// </summary>
    public static class InstantArticleModelFactory
    {

        private static readonly Dictionary<InstantArticleItemType, System.Func<InstantArticleItemModel, InstantArticleItemModel>> _createFromDic = new Dictionary
                <InstantArticleItemType, System.Func<InstantArticleItemModel, InstantArticleItemModel>>()
            {
                {InstantArticleItemType.Image, (x) => new ImageItem(x) },
                {InstantArticleItemType.Video,  (x) => new VideoItem(x) },

                {InstantArticleItemType.BodyText, (x) => new TextItem(x)},
                {InstantArticleItemType.PullQuote, (x) => new PullQuoteItem(x) },
                {InstantArticleItemType.BlockQuote, (x) => new BlockQuoteItem(x) },

                {InstantArticleItemType.Ad, (x) => new AdItem(x) },
                {InstantArticleItemType.Map, (x) => new MapItem(x) },

                {InstantArticleItemType.Twitter, (x) => new TwitterEmbedItem(x) },
                 {InstantArticleItemType.Facebook, (x) => new FacebookEmbedItem(x) },
                {InstantArticleItemType.Instagram, (x) => new InstagramEmbedItem(x) },
                {InstantArticleItemType.YouTube, (x) => new YouTubeEmbedItem(x)  },
                {InstantArticleItemType.Caption, (x) => new CaptionItem(x) },
                {InstantArticleItemType.SlideShow, (x) => new SlideShowItem(x) }
            };

        public static readonly Dictionary<InstantArticleItemType, InstantArticleItemModel> Items = new Dictionary
            <InstantArticleItemType, InstantArticleItemModel>()
        {
            {InstantArticleItemType.Image, new ImageItem()},
            {InstantArticleItemType.Video, new VideoItem()},

            {InstantArticleItemType.BodyText, new TextItem()},
            {InstantArticleItemType.PullQuote, new PullQuoteItem()},
            {InstantArticleItemType.BlockQuote, new BlockQuoteItem()},

            {InstantArticleItemType.Ad, new AdItem()},
            {InstantArticleItemType.Map, new MapItem()},

            {InstantArticleItemType.Twitter, new TwitterEmbedItem()},
             {InstantArticleItemType.Facebook, new FacebookEmbedItem()},
            {InstantArticleItemType.Instagram, new InstagramEmbedItem()},
            {InstantArticleItemType.YouTube, new YouTubeEmbedItem()},
            {InstantArticleItemType.Caption, new CaptionItem()},
            {InstantArticleItemType.SlideShow, new SlideShowItem()},
        };

        public static InstantArticleItemModel CreateFrom(int type, InstantArticleItemModel model)
        {


            _createFromDic.ToDictionary(kvp => kvp.Key,
                                               kvp => kvp.Value);

            return _createFromDic[(InstantArticleItemType)type](model);
        }

        public static InstantArticleItemModel Create(int type)
        {
            var itemdic = Items.ToDictionary(kvp => kvp.Key,
                                               kvp => kvp.Value);

            return itemdic[(InstantArticleItemType)type];
        }
    }
}
