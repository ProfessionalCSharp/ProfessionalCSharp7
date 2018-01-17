using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHooksReceiver.Services;

namespace WebHooksReceiver.Controllers
{
    public class DropboxController : Controller
    {
        private readonly IStorageQueueService _storageQueueService;
        public DropboxController(IStorageQueueService storageQueueService)
        {
            _storageQueueService = storageQueueService;
        }

        [DropboxWebHook()]
        public async Task<IActionResult> Dropbox(string id, JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _storageQueueService.WriteToQueueStorageAsync("dropbox", data.ToString());

            return Ok();
        }
    }
}
