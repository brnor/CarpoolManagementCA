using Application.Common.Enums;
using Application.Common.Helpers;

namespace Application.Common.Exceptions
{
    public class InvalidDateException : CustomValidationException
    {
        public InvalidDateException(InvalidDateMessageEnum message)
            : base(message.ToDescriptionString())
        {
        }
    }
}
