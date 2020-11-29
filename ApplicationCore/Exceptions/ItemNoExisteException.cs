using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class ItemNoExisteException : Exception
    {
        public ItemNoExisteException()
        {
        }

        public ItemNoExisteException(string message) : base(message)
        {
        }

        public ItemNoExisteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemNoExisteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
