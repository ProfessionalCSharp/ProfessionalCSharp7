namespace WindowsAppChatClient.Services
{
    public class UrlService
    {
        private const string BaseUri = "http://localhost:2952/";
        public string ChatAddress => $"{BaseUri}/chat";
        public string GroupAddress => $"{BaseUri}/groupchat";
    }
}
