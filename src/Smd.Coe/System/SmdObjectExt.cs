using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 所有objects的扩展
    /// </summary>
    public static class SmdObjectExt
    {
        /// <summary>
        /// 简单的类型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">源</param>
        /// <returns></returns>
        public static T As<T>(this object obj) where T : class
        {
            return (T)obj;
        }
        /// <summary>
        /// 把未知类型转为已知
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T To<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// object转换成为非null字符串 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isTrim">是否去掉首尾空格</param>
        /// <returns></returns>
        public static string ToStr(this object obj,bool isTrim=true)
        {
            if (obj == null)
                return "";

            if (!isTrim)
                return obj.ToString();

            return obj.ToString().Trim();
        }
       

        /// <summary>
        /// 项是否在指定的集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">项</param>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }



    }
}
