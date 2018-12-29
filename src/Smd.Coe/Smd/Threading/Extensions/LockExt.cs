using System;
using System.Collections.Generic;
using System.Text;

namespace Smd.Threading.Extensions
{
    /// <summary>
    /// 锁 扩展，很方便同步锁
    /// </summary>
    public static class LockExt
    {
        /// <summary>
        ///锁定对象并执行action
        /// </summary>
        /// <param name="source">被锁定对象</param>
        /// <param name="action">Action</param>
        public static void Locking(this object source, Action action)
        {
            lock (source)
            {
                action();
            }
        }

        /// <summary>
        /// 锁定对象并执行action
        /// </summary>
        /// <typeparam name="T">锁定对象的类型</typeparam>
        /// <param name="source">被锁定对象</param>
        /// <param name="action">Action</param>
        public static void Locking<T>(this T source, Action<T> action) where T : class
        {
            lock (source)
            {
                action(source);
            }
        }

        /// <summary>
        ///锁定对象并执行Func
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <typeparam name="T">锁定对象的类型</typeparam>
        /// <param name="func">Func方法</param> 
        public static TResult Locking<TResult>(this object source, Func<TResult> func)
        {
            lock (source)
            {
                return func();
            }
        }

        /// <summary>
        ///锁定对象并执行Func
        /// </summary>
        /// <typeparam name="T">锁定对象的类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">被锁定对象</param>
        /// <param name="func">Func方法</param>  
        public static TResult Locking<T, TResult>(this T source, Func<T, TResult> func) where T : class
        {
            lock (source)
            {
                return func(source);
            }
        }
    }
}
