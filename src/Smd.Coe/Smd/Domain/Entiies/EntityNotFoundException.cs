using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Smd.Domain.Entiies
{
    /// <summary>
    /// 无法找到实体的异常
    /// </summary>
    [Serializable]
    public class EntityNotFoundException:SmdException
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// 实体Id值
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// 创建 <see cref="EntityNotFoundException"/> 实例
        /// </summary>
        public EntityNotFoundException()
        {

        }

        /// <summary>
        /// 创建实例
        /// </summary>
        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// 创建实例
        /// </summary>
        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }

        /// <summary>
        ///创建实例
        /// </summary>
        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="message">错误信息</Eparam>
        public EntityNotFoundException(string message)
            : base(message)
        {

        }

        /// <summary>
        ///创建实例
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内部错误</param>
        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
