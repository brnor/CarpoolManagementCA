using System;

namespace Application.Common.Exceptions
{
    public abstract class CustomValidationException : Exception
    {
        public CustomValidationException(string message) : base(message)
        {
        }

        public CustomValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
