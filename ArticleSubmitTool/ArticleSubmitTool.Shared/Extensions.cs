using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ArticleSubmitTool.Shared
{
    public static class Extensions
    {
        public static List<T> DeepClone<T>(this IList<T> list)
        {
            var newList = list.Select(i => (T)i.DeepClone()).ToList();
            return newList;
        }

        public static object DeepClone(this object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }

        public static IEnumerable<V> WhereKeys<K, V>(this Dictionary<K, V> dic, IEnumerable<K> keys)
        {
            return dic.Where(kvp => keys.Contains(kvp.Key)).Select(kvp => kvp.Value);
        }

        public static Dictionary<K, V> Append<K, V>(this Dictionary<K, V> dic, IEnumerable<KeyValuePair<K, V>> items)
        {
            dic = dic.Concat(items).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return dic;
        }

        public static IList Append(this IList src, IEnumerable items)
        {
            foreach (var item in items)
            {
                src.Add(item);
            }
            return src;
        }

        public static List<T> Append<T>(this List<T> src, IEnumerable<T> items)
        {
            src.AddRange(items);
            return src;
        }

        public static IEnumerable<T> Exclude<T>(this List<T> items, T exItem)
        {
            items.Remove(exItem);
            return items.AsEnumerable();
        }

        public static IEnumerable<T> Exclude<T>(this List<T> items, IEnumerable<T> exItems)
        {
            foreach (var exItem in exItems)
            {
                items.Remove(exItem);
            }
            return items.AsEnumerable();
        }


        public static string ToCSV(this IEnumerable<string> items)
        {
            if (items.IsNullOrEmpty())
                return null;
            return items.Count() > 1 ? items.Aggregate((x, y) => String.Format("{0},{1}", x, y)) : items.First();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> src)
        {
            return src == null || !src.Any();
        }

        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        #region misc
        public static Dictionary<string, object> GetProperties(this object obj)
        {
            Type type = obj.GetType();
            var propDic = type.GetProperties().ToDictionary(p => p.Name, p => p.GetValue(obj));
            return propDic;
        }
        #endregion
    }
}
