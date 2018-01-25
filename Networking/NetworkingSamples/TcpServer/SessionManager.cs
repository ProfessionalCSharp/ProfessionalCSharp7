using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static TcpServer.CustomProtocol;

namespace TcpServer
{
    public struct Session
    {
        public string SessionId { get; set; }
        public DateTime LastAccessTime { get; set; }
    }

    public class SessionManager
    {
        private readonly ConcurrentDictionary<string, Session> _sessions = new ConcurrentDictionary<string, Session>();
        private readonly ConcurrentDictionary<string, Dictionary<string, string>> _sessionData = new ConcurrentDictionary<string, Dictionary<string, string>>();

        public string CreateSession()
        {
            string sessionId = Guid.NewGuid().ToString();
            if (_sessions.TryAdd(sessionId, new Session { SessionId = sessionId, LastAccessTime = DateTime.UtcNow }))
            {
                return sessionId;
            }
            else
            {
                return string.Empty;
            }
        }
        public void CleanupAllSessions()
        {
            foreach (var session in _sessions)
            {
                if (session.Value.LastAccessTime + SessionTimeout >= DateTime.UtcNow)
                {
                    CleanupSession(session.Key);
                }
            }
        }

        public void CleanupSession(string sessionId)
        {
            if (_sessionData.TryRemove(sessionId, out Dictionary<string, string> removed))
            {
                Console.WriteLine($"removed {sessionId} from session data");
            }
            if (_sessions.TryRemove(sessionId, out Session header))
            {
                Console.WriteLine($"removed {sessionId} from sessions");
            }
        }

        public void SetSessionData(string sessionId, string key, string value)
        {
            if (!_sessionData.TryGetValue(sessionId, out Dictionary<string, string> data))
            {
                data = new Dictionary<string, string>();
                data.Add(key, value);
                _sessionData.TryAdd(sessionId, data);
            }
            else
            {
                string val;
                if (data.TryGetValue(key, out val))
                {
                    data.Remove(key);
                }
                data.Add(key, value);
            }
        }

        public string GetSessionData(string sessionId, string key)
        {
            if (_sessionData.TryGetValue(sessionId, out Dictionary<string, string> data))
            {
                string value;
                if (data.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            return STATUSNOTFOUND;
        }

        public string ParseSessionData(string sessionId, string requestAction)
        {
            string[] sessionData = requestAction.Split('=');
            if (sessionData.Length != 2) return STATUSUNKNOWN;
            string key = sessionData[0];
            string value = sessionData[1];
            SetSessionData(sessionId, key, value);
            return $"{key}={value}";
        }

        public bool TouchSession(string sessionId)
        {
            if (!_sessions.TryGetValue(sessionId, out Session oldHeader))
            {
                return false;
            }

            Session updatedHeader = oldHeader;
            updatedHeader.LastAccessTime = DateTime.UtcNow;
            _sessions.TryUpdate(sessionId, updatedHeader, oldHeader);
            return true;
        }
    }
}
