using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Smd.Domain.Uow
{
    [Serializable]
    public class SmdDbConcurrencyException :SmdException
    {
        /// <summary>
        /// Creates a new <see cref="SmdDbConcurrencyException"/> object.
        /// </summary>
        public SmdDbConcurrencyException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="SmdException"/> object.
        /// </summary>
        public SmdDbConcurrencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SmdDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public SmdDbConcurrencyException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="SmdDbConcurrencyException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public SmdDbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
