using System;

namespace SolicitColdCall
{

    public class ColdCallFileFormatException : Exception
    {
        public ColdCallFileFormatException(string message)
            : base(message)
        {
        }

        public ColdCallFileFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}