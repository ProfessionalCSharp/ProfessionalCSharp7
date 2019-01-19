namespace WindowsAppChatClient.Services
{
    public class UrlService
    {
        private const string BaseUri = "http://localhost:3510";
        public string ChatAddress => $"{BaseUri}/chat";
        public string GroupAddress => $"{BaseUri}/groupchat";
    }
}
