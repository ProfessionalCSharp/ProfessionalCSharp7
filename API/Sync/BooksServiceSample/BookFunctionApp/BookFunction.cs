
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.DependencyInjection;
using BooksServiceSample.Services;

namespace BookFunctionApp
{
    public static class BookFunction
    {
        static BookFunction()
        {
            ConfigureServices();
            FeedSampleChapters();
        }

        private static void FeedSampleChapters()
        {
            var sampleChapters = ApplicationServices.GetRequiredService<SampleChapters>();
            sampleChapters.CreateSampleChapters();
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IBookChaptersService, BookChaptersService>();
            services.AddSingleton<SampleChapters>();
            ApplicationServices = services.BuildServiceProvider();
        }

        public static IServiceProvider ApplicationServices { get; private set; }

        [FunctionName("BookFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            IActionResult result = null;
            switch (req.Method)
            {
                case "GET":
                    result = DoGet(req);
                    break;
                case "POST":
                    break;
                case "PUT":
                    break;
                default:
                    break;
            }

            return result;
            //string name = req.Query["name"];

            //string requestBody = new StreamReader(req.Body).ReadToEnd();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        private static IActionResult DoGet(HttpRequest req)
        {
            var bookChapterService = ApplicationServices.GetRequiredService<IBookChaptersService>();
            var chapters = bookChapterService.GetAll();
            return new OkObjectResult(chapters);
        }
    }
}
