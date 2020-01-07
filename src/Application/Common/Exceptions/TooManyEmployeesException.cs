namespace Application.Common.Exceptions
{
    public class TooManyEmployeesException : CustomValidationException
    {
        public TooManyEmployeesException(int carId)
            : base($"Too many employees for specified car ({carId})")
        {
        }
    }
}
