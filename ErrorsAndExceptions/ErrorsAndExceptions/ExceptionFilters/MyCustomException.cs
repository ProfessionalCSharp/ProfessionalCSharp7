using System;

namespace ExceptionFilters
{
    public class MyCustomException : Exception
    {
        public MyCustomException(string message)
            : base(message)
        {
        }
        public int ErrorCode { get; set; }
    }
}