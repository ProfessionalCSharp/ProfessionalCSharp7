using System;

namespace SolicitColdCall
{

    public class SalesSpyFoundException : Exception
    {
        public SalesSpyFoundException(string spyName)
          : base($"Sales spy found, with name {spyName}")
        {
        }

        public SalesSpyFoundException(string spyName, Exception innerException)
          : base($"Sales spy found with name {spyName}", innerException)
        {
        }
    }
}