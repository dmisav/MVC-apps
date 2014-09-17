using System;

namespace TestProject.DataAccessLayer.Exceptions
{
    public class NonExistingUserException : Exception
    {
        public NonExistingUserException()
        {
        }

        public NonExistingUserException(string message)
            : base(message)
        {
        }

        public NonExistingUserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}