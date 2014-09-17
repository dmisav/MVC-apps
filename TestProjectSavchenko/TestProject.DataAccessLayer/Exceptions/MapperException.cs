using System;

namespace TestProject.DataAccessLayer.Exceptions
{
    public class MapperException : Exception
    {
        public MapperException()
        {
        }

        public MapperException(string message): base(message)
        {
        }

        public MapperException(string message, Exception inner): base(message, inner)
        {
        }
    }
}