using System;
using System.Runtime.Serialization;

namespace Smd
{
    /// <summary>
    /// 自定义错误基类
    /// </summary>
    [Serializable]
    public class SmdException:Exception
    {
        /// <summary>
        /// 创建 <see cref="SmdException"/> 实例.
        /// </summary>
        public SmdException()
        {

        }

        /// <summary>
        ///   创建 <see cref="SmdException"/> 实例.
        /// </summary>
        public SmdException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        ///  创建 <see cref="SmdException"/> 实例.
        /// </summary>
        /// <param name="message">错误信息</param>
        public SmdException(string message)
            : base(message)
        {

        }

        /// <summary>
        ///  创建 <see cref="AbpException"/> 实例.
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内部异常</param>
        public SmdException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
