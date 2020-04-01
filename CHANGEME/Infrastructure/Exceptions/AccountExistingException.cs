using System;

namespace Infrastructure.Exceptions
{
    public class AccountExistingException : Exception
    {
        public AccountExistingException(string message) : base(message)
        {
        }
    }
}
