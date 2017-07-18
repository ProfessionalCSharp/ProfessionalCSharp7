using System;

namespace RethrowExceptions
{
    public class MyCustomException : Exception
    {
        public MyCustomException(string message)
            : base(message)
        {
        }
        public int ErrorCode { get; set; }
    }

    public class AnotherCustomException : Exception
    {
        public AnotherCustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}