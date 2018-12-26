using System.Linq;
using JetBrains.Annotations;

namespace System.Collections.Generic
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class SmdCollectionExtensions
    {
        /// <summary>
        ///  检查集合是否为空或者数量为0
        /// </summary>
        public static bool IsNullOrEmpty<T>([CanBeNull] this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }


        /// <summary>
        /// 为集合添加项
        /// </summary>
        /// <param name="source">集合</param>
        /// <param name="item">需要新增的项</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>当集合为null或项已存在时返回false,新增成功返回true</returns>
        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
        {
            if (source == null)
                return false;

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        /// <summary>
        ///  为当前集合新增一个集合 
        /// </summary>
        /// <param name="source">集合</param>
        /// <param name="items">需要新增的项</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>结果仅返回那些已新增的项，而非添加后的整个集合</returns>
        public static IEnumerable<T> AddIfNotContains<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
        {
            if (source == null || items == null)
                return null;

            var addedItems = new List<T>();

            foreach (var item in items)
            {
                if (source.Contains(item))
                {
                    continue;
                }

                source.Add(item);
                addedItems.Add(item);
            }

            return addedItems;
        }

        /// <summary>
        /// 为集合添加一个不存在的项。项是否存在是基本给定的条件判断  <paramref name="predicate"/>.
        /// </summary>
        /// <param name="source">集合</param>
        /// <param name="predicate">定义项是否存在的条件 </param>
        /// <param name="itemFactory">新增的项目</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>true=已新增，false=没有新增</returns>
        public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, [NotNull] Func<T, bool> predicate, [NotNull] Func<T> itemFactory)
        {
            if (source == null || predicate == null || itemFactory == null)
                return false;


            if (source.Any(predicate))
            {
                return false;
            }

            source.Add(itemFactory());
            return true;
        }

        /// <summary>
        /// 从集合移除所有符合给定条件的项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">集合</param>
        /// <param name="predicate">条件</param>
        /// <returns>被移除的项集合</returns>
        public static IList<T> RemoveAll<T>([NotNull] this ICollection<T> source, Func<T, bool> predicate)
        {
            var items = source.Where(predicate).ToList();

            foreach (var item in items)
            {
                source.Remove(item);
            }

            return items;
        }
    }
}