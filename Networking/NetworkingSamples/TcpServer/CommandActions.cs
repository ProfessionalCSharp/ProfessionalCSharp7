using System.Linq;

namespace TcpServer
{
    public class CommandActions
    {
        public string Reverse(string action) => string.Join("", action.Reverse());

        public string Echo(string action) => action;
    }
}
