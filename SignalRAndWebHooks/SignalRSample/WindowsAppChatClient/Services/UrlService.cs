namespace WindowsAppChatClient.Services
{
    public class UrlService
    {
        private const string BaseUri = "http://localhost:2289/";
        public string ChatAddress => $"{BaseUri}/chat";
        public string GroupAddress => $"{BaseUri}/groupchat";
    }
}
