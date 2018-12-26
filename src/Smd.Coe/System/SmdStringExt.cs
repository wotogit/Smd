using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// 所有string的扩展
    /// </summary>
    public static class SmdStringExt
    {
        /// <summary>
        /// string 转为bool
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defalutValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string str, bool defalutValue = false)
        {
            bool result = defalutValue;

            if (str.ToStr() == "1" || str.ToStr().ToLower() == "on" || str.ToStr().ToLower() == "yes")
                return true;

            if (str.ToStr() == "0" || str.ToStr().ToLower() == "off" || str.ToStr().ToLower() == "no")
                return false;

            bool.TryParse(str, out result);

            return result;
        }
        /// <summary>
        /// string转int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defalutValue"></param>
        /// <returns></returns>
        public static int ToInt(this string str, int defalutValue = 0)
        {
            int result = defalutValue;

            int.TryParse(str, out result);

            return result;
        }
        /// <summary>
        /// string转double
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defalutValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defalutValue = 0)
        {
            double result = defalutValue;

            double.TryParse(str, out result);

            return result;
        }
        /// <summary>
        /// string转Decimal
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defalutValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defalutValue = 0)
        {
            decimal result = defalutValue;

            decimal.TryParse(str, out result);

            return result;
        }
        /// <summary>
        /// string 转DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defalut"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime defalutValue = new DateTime())
        {
            DateTime result = defalutValue;

            DateTime.TryParse(str, out result);

            return result;
        }
        /// <summary>
        /// 字符串转Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string str) where T : struct
        {
            T result = default(T);

            Enum.TryParse<T>(str, true, out result);

            return result;
        }
        /// <summary>
        /// 若字符串结尾不是指定的字符，则添加指定的字符作为结尾 
        /// </summary>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
        {
            str = str.ToStr(isTrim: false);

            if (str.EndsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// 若字符串的开头不是指定的字符，则添加指定的字符作为开头
        /// </summary>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
        {
            str = str.ToStr(isTrim: false);

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// 字符串是否为null或者System.String.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 字符串是否为 null, empty, or 空白字符
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 从0索引开始，截取指定长度字符串
        /// </summary> 
        public static string Left(this string str, int len)
        {
            str = str.ToStr(isTrim: false);

            if (str.Length < len)
            {
                return str;
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// 确宝换行符符合当前的环境 <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string NormalizeLineEndings(this string str)
        {
            return str.ToStr(isTrim: false).Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        ///获取指定字符第n次出现时所在的位置索引，不存在则返回-1
        /// </summary>
        /// <param name="str">source string to be searched</param>
        /// <param name="c">Char to search in <see cref="str"/></param>
        /// <param name="n">Count of the occurence</param>
        public static int NthIndexOf(this string str, char c, int n)
        {
            str = str.ToStr(isTrim: false);

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 移除字符串后缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="postFixes">1个或多个后缀</param>
        /// <returns>返回替换结果</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            return str.RemovePostFix(StringComparison.Ordinal, postFixes);
        }

        /// <summary>
        /// 移除字符串后缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="comparisonType">字符串比较器</param>
        /// <param name="postFixes">1个或多个后缀</param>
        /// <returns>返回替换结果</returns>
        public static string RemovePostFix(this string str, StringComparison comparisonType, params string[] postFixes)
        {
            if (str.IsNullOrEmpty())
            {
                return null;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix, comparisonType))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        /// 删除指定前缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="preFixes">1个或多个前缀</param>
        /// <returns>修改后字符串</returns>
        public static string RemovePreFix(this string str, params string[] preFixes)
        {
            return str.RemovePreFix(StringComparison.Ordinal, preFixes);
        }

        /// <summary>
        ///删除指定前缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="comparisonType">字符串比较类型</param>
        /// <param name="preFixes">1个或多个前缀</param>
        /// <returns>修改后字符串</returns>
        public static string RemovePreFix(this string str, StringComparison comparisonType, params string[] preFixes)
        {
            if (str.IsNullOrEmpty())
            {
                return null;
            }

            if (preFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var preFix in preFixes)
            {
                if (str.StartsWith(preFix, comparisonType))
                {
                    return str.Right(str.Length - preFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        /// 替换首次出现指定字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string str, string search, string replace, StringComparison comparisonType = StringComparison.Ordinal)
        {
            str = str.ToStr(isTrim:false);

             var pos = str.IndexOf(search, comparisonType);
            if (pos < 0)
            {
                return str;
            }

            return str.Substring(0, pos) + replace + str.Substring(pos + search.Length);
        }

        /// <summary>
        ///截取字符串从尾部起指定的长度
        /// </summary> 
        public static string Right(this string str, int len)
        {
            str = str.ToStr(isTrim: false);

            if (str.Length < len)
            {
                return str;
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
        /// <returns>camelCase of the string</returns>
        public static string ToCamelCase(this string str, bool useCurrentCulture = false)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
            }

            return (useCurrentCulture ? char.ToLower(str[0]) : char.ToLowerInvariant(str[0])) + str.Substring(1);
        }

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        
        /// <summary>
        /// 截取指定长度字符串
        /// </summary> 
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Left(maxLength);
        }

       

        /// <summary>
        /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
        /// It adds a "..." postfix to end of the string if it's truncated.
        /// Returning string can not be longer than maxLength.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        ///截取字符串。先追加指定后缀再进行截取
        /// </summary> 
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }

        /// <summary>
        /// 转换成二进制字节，使用Encoding.UTF8
        /// </summary>
        public static byte[] GetBytes(this string str)
        {
            return str.GetBytes(Encoding.UTF8);
        }

        /// <summary>
        ///转换成二进制字节 
        /// </summary>
        public static byte[] GetBytes([NotNull] this string str, [NotNull] Encoding encoding)
        {
            str = str.ToStr(isTrim:false);

            return encoding.GetBytes(str);
        }
    }
}
