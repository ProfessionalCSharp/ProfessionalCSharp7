using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSampleApp.Services;

namespace WebSampleApp.Controllers
{
    public class HomeController
    {
        private readonly ISampleService _sampleService;
        public HomeController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public async Task<int> Index(HttpContext context)
        {
            var sb = new StringBuilder();
            sb.Append("<ul>");
            sb.Append(string.Join("",
              _sampleService.GetSampleStrings().Select(
                s => $"<li>{s}</li>").ToArray()));
            sb.Append("</ul>");
            await context.Response.WriteAsync(sb.ToString());
            return 200;
        }

    }
}
