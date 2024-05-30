namespace Accounts.Common.Exceptions
{
    public class UserTypeException : Exception
    {
        public UserTypeException()
        {
        }
        public UserTypeException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }
}
