using System;
using System.Runtime.Serialization;

namespace Systore.Api.Exceptions
{
    [Serializable]
    public class VerifyReleaseException : Exception
    {
        public VerifyReleaseException()
        {
        }

        public VerifyReleaseException(string message)
            : base(message)
        {
        }

        public VerifyReleaseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected VerifyReleaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}