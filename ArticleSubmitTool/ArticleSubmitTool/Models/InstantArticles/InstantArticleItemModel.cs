using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml.Linq;
using ArticleSubmitTool.Code;
using ArticleSubmitTool.Domain;
using ArticleSubmitTool.Models;
using ArticleSubmitTool.Models.InstantArticles;
using ArticleSubmitTool.Shared;
using ArticleSubmitTool.Web.Models.InstantArticles.Items;
using AutoMapper.Mappers;

namespace ArticleSubmitTool.Web.Models.InstantArticles
{
    public class InstantArticleItemModel : AModel, IMarkupItem
    {
        public long InstantArticleId { get; set; }

        public long ItemTypeId { get; set; }

        public List<InstantArticleItemAttribute> Attributes { get; set; }
        public Dictionary<string, List<NameTitle>> AttributeValues { get; set; }

        public InstantArticleItemType Type { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public long? ParentId { get; set; }

        public InstantArticleItemModel Parent { get; set; }

        public List<InstantArticleItemModel> Children { get; set; }

        public bool IsHeader { get; set; }

        public bool IsCaption { get; set; }

        public InstantArticleItemModel(InstantArticleItemModel model)
        {
            InstantArticleId = model.InstantArticleId;
            ItemTypeId = model.ItemTypeId;
            Attributes = model.Attributes;
            AttributeValues = model.AttributeValues;
            Type = model.Type;
            Name = model.Name;
            Id = model.Id;
            Content = model.Content;
            IsHeader = model.IsHeader;
            IsCaption = model.IsCaption;
            ParentId = model.ParentId;
            Parent = model.Parent;
            Children = model.Children;
        }

        public InstantArticleItemModel()
        {
            Attributes = new List<InstantArticleItemAttribute>();
            AttributeValues = new Dictionary<string, List<NameTitle>>();
        }

        protected void Initialize()
        {
            if (Common.ItemData.ContainsKey(Type))
            {
                Attributes = Common.ItemData[Type].Attributes;
                AttributeValues = Common.ItemData[Type].AttributeValues;
            }
            
        }

        public virtual XElement ToXElement()
        {
            return null;
        }

        public static XAttribute ToXAttribute(Dictionary<string, InstantArticleItemAttribute> attrDic, string attrName)
        {
            return attrDic[attrName].Value.IsNullOrEmpty() ?
                      new XAttribute(attrName, string.Empty) :
                      new XAttribute(attrName, attrDic[attrName].Value);

        }

        public static string GetAttributeValue(InstantArticleItemAttribute attr)
        {
            return attr.Value.IsNullOrEmpty() ? string.Empty : attr.Value;
        }

        public static XAttribute[] ToXAttribute(Dictionary<string, InstantArticleItemAttribute> attrDic)
        {
            var attrs = attrDic.Select(
                a => ToXAttribute(attrDic, a.Key)).ToArray();

            return attrs;
        }

    }
}
