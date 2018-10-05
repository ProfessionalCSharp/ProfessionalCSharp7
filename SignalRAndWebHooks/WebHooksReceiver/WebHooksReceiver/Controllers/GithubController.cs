using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WebHooksReceiver.Services;

namespace WebHooksReceiver.Controllers
{
    public class GithubController : Controller
    {
        private readonly IStorageQueueService _storageQueueService;
        public GithubController(IStorageQueueService storageQueueService)
        {
            _storageQueueService = storageQueueService;
        }

        [GitHubWebHook(EventName = "push")]
        public async Task<IActionResult> GitHubPushHandler(string id, JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string message = $" id:{id}, push-event, data: {data.ToString()}";
            await _storageQueueService.WriteToQueueStorageAsync("github", message);

            return Ok();
        }

        [GitHubWebHook()]
        public async Task<IActionResult> GitHubHandler(string id, string eventName, JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string message = $" id:{id}, event:{eventName}, data: {data.ToString()}";
            await _storageQueueService.WriteToQueueStorageAsync("github", message);

            return Ok();
        }

        [GeneralWebHook]
        public async Task<IActionResult> FallbackHandler(string receiverName, string id, string eventName, JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string message = $"receiver:{receiverName}, id:{id}, event:{eventName}, data: {data.ToString()}";
            await _storageQueueService.WriteToQueueStorageAsync("generalwebhook", message);
            return Ok();
        }
    }
}
