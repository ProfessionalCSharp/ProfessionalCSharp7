using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TcpServer.CustomProtocol;

namespace TcpServer
{
    class Program
    {
        private const int PortNumber = 8800;
        private readonly SessionManager _sessionManager = new SessionManager();
        private readonly CommandActions _commandActions = new CommandActions();

        private enum ParseResponse
        {
            OK,
            CLOSE,
            ERROR,
            TIMEOUT
        }

        static void Main()
        {
            var p = new Program();
            p.Run();
        }

        public void Run()
        {
            using (var timer = new Timer(TimerSessionCleanup, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1)))
            {
                RunServerAsync().Wait();
            }
        }

        public async Task RunServerAsync()
        {
            try
            {
                var listener = new TcpListener(IPAddress.Any, PortNumber);
                Console.WriteLine($"listener started at port {PortNumber}");
                listener.Start();

                while (true)
                {
                    Console.WriteLine("waiting for client...");
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Task t = RunClientRequestAsync(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception of type {ex.GetType().Name}, Message: {ex.Message}");
            }
        }

        private Task RunClientRequestAsync(TcpClient client)
        {
            return Task.Run(async () =>
            {
                try
                {
                    using (client)
                    {
                        Console.WriteLine("client connected");

                        using (NetworkStream stream = client.GetStream())
                        {
                            bool completed = false;

                            do
                            {
                                byte[] readBuffer = new byte[1024];
                                int read = await stream.ReadAsync(readBuffer, 0, readBuffer.Length);
                                string request = Encoding.ASCII.GetString(readBuffer, 0, read);
                                Console.WriteLine($"received {request}");

                                byte[] writeBuffer = null;
                                string response = string.Empty;

                                ParseResponse resp = ParseRequest(request, out string sessionId, out string result);
                                switch (resp)
                                {
                                    case ParseResponse.OK:
                                        string content = $"{STATUSOK}::{SESSIONID}::{sessionId}";
                                        if (!string.IsNullOrEmpty(result)) content += $"{SEPARATOR}{result}";
                                        response = $"{STATUSOK}{SEPARATOR}{SESSIONID}{SEPARATOR}{sessionId}{SEPARATOR}{content}";
                                        break;
                                    case ParseResponse.CLOSE:
                                        response = $"{STATUSCLOSED}";
                                        completed = true;
                                        break;
                                    case ParseResponse.TIMEOUT:
                                        response = $"{STATUSTIMEOUT}";
                                        break;
                                    case ParseResponse.ERROR:
                                        response = $"{STATUSINVALID}";
                                        break;
                                    default:
                                        break;
                                }
                                writeBuffer = Encoding.ASCII.GetBytes(response);
                                await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                                await stream.FlushAsync();
                                Console.WriteLine($"returned {Encoding.ASCII.GetString(writeBuffer, 0, writeBuffer.Length)}");
                            } while (!completed);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in client request handling of type {ex.GetType().Name}, Message: {ex.Message}");
                }
                Console.WriteLine("client disconnected");
            });
        }

        private ParseResponse ParseRequest(string request, out string sessionId, out string response)
        {
            sessionId = string.Empty;
            response = string.Empty;
            string[] requestColl = request.Split(new string[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

            if (requestColl[0] == COMMANDHELO)  // first request
            {
                sessionId = _sessionManager.CreateSession();
               
            }
            else if (requestColl[0] == SESSIONID)  // any other valid request
            {
                sessionId = requestColl[1];

                if (!_sessionManager.TouchSession(sessionId))
                {
                    return ParseResponse.TIMEOUT;
                }

                if (requestColl[2] == COMMANDBYE)
                {
                    return ParseResponse.CLOSE;
                }
                if (requestColl.Length >= 4)
                {
                    response = ProcessRequest(requestColl);
                }
            }
            else
            {
                return ParseResponse.ERROR;
            }
            return ParseResponse.OK;
        }

        // process all requests with the exception of the inital HELO request
        private string ProcessRequest(string[] requestColl)
        {
            if (requestColl.Length < 4) throw new ArgumentException("invalid length requestColl");

            string sessionId = requestColl[1];
            string response = string.Empty;
            string requestCommand = requestColl[2];
            string requestAction = requestColl[3];


            switch (requestCommand)
            {
                case COMMANDECHO:
                    response = _commandActions.Echo(requestAction);
                    break;
                case COMMANDREV:
                    response = _commandActions.Reverse(requestAction);
                    break;
                case COMMANDSET:
                    response = _sessionManager.ParseSessionData(sessionId, requestAction);
                    break;
                case COMMANDGET:
                    response = $"{_sessionManager.GetSessionData(sessionId, requestAction)}";
                    break;
                default:
                    response = STATUSUNKNOWN;
                    break;
            }
            return response;
        }

        private void TimerSessionCleanup(object o) =>
            _sessionManager.CleanupAllSessions();
    }
}