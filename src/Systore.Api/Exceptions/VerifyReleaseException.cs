using System;

namespace Systore.Api.Exceptions
{
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
    }
}