namespace Application.Common.Exceptions
{
    public class NoDriverException : CustomValidationException
    {
        public NoDriverException()
            : base("At least one employee must be a driver.")
        {
        }
    }
}
