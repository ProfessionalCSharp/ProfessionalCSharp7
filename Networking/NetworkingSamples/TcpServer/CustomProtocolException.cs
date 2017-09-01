using System;

namespace TcpServer
{
    public class CustomProtocolException : Exception
    {
        public CustomProtocolException() { }
        public CustomProtocolException(string message) : base(message) { }
        public CustomProtocolException(string message, Exception inner) : base(message, inner) { }
    }
}
