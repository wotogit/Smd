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
        ///  ��鼯���Ƿ�Ϊ�ջ�������Ϊ0
        /// </summary>
        public static bool IsNullOrEmpty<T>([CanBeNull] this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }


        /// <summary>
        /// Ϊ���������
        /// </summary>
        /// <param name="source">����</param>
        /// <param name="item">��Ҫ��������</param>
        /// <typeparam name="T">����</typeparam>
        /// <returns>������Ϊnull�����Ѵ���ʱ����false,�����ɹ�����true</returns>
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
        ///  Ϊ��ǰ��������һ������ 
        /// </summary>
        /// <param name="source">����</param>
        /// <param name="items">��Ҫ��������</param>
        /// <typeparam name="T">����</typeparam>
        /// <returns>�����������Щ���������������Ӻ����������</returns>
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
        /// Ϊ�������һ�������ڵ�����Ƿ�����ǻ��������������ж�  <paramref name="predicate"/>.
        /// </summary>
        /// <param name="source">����</param>
        /// <param name="predicate">�������Ƿ���ڵ����� </param>
        /// <param name="itemFactory">��������Ŀ</param>
        /// <typeparam name="T">����</typeparam>
        /// <returns>true=��������false=û������</returns>
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
        /// �Ӽ����Ƴ����з��ϸ�����������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="source">����</param>
        /// <param name="predicate">����</param>
        /// <returns>���Ƴ������</returns>
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