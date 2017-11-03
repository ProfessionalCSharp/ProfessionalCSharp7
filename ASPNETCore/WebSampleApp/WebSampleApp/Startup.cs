using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebSampleApp.Services;

namespace WebSampleApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISampleService, DefaultSampleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.Map("/RequestAndResponse", app1 =>
            {
                app1.Run(async context =>
                {
                    string result = string.Empty;

                    switch (context.Request.Path.Value.ToLower())
                    {
                        case "/header":
                            result = RequestAndResponseSamples.GetHeaderInformation(context.Request);
                            break;
                        case "/add":
                            result = RequestAndResponseSamples.QueryString(context.Request);
                            break;
                        case "/content":
                            result = RequestAndResponseSamples.Content(context.Request);
                            break;
                        case "/encoded":
                            result = RequestAndResponseSamples.ContentEncoded(context.Request);
                            break;
                        case "/form":
                            result = RequestAndResponseSamples.GetForm(context.Request);
                            break;
                        case "/writecookie":
                            result = RequestAndResponseSamples.WriteCookie(context.Response);
                            break;
                        case "/readcookie":
                            result = RequestAndResponseSamples.ReadCookie(context.Request);
                            break;
                        case "/json":
                            result = RequestAndResponseSamples.GetJson(context.Response);
                            break;
                        default:
                            result = RequestAndResponseSamples.GetRequestInformation(context.Request);
                            break;
                    }

                    await context.Response.WriteAsync(result);
                });
            });

            app.Run(async (context) =>
            {
                // await context.Response.WriteAsync("Hello World!");

                string[] lines = new[]
                {
                    "<ul>",
                    @"<li><a href=""/hello.html"">Static Files</a> - requires UseStaticFiles</li>",
                    @"<li><a href=""/RequestAndResponse"">Request and Response</a></li>",
                    "<li>Request and Response 2",
                    "<ul>",
                    @"<li><a href=""/RequestAndResponse/header"">Header</a></li>",
                    @"<li><a href=""/RequestAndResponse/add?x=3&y=4"">Add</a></li>",
                    @"<li><a href=""/RequestAndResponse/content?data=sample"">Content</a></li>",
                    @"<li><a href=""/RequestAndResponse/content?data=<h1>sample</h1>"">HTML Content</a></li>",
                    @"<li><a href=""/RequestAndResponse/content?data=<script>alert('hacker');</script>"">Bad Content</a></li>",
                    @"<li><a href=""/RequestAndResponse/encoded?data=<h1>sample</h1>"">Encoded content</a></li>",
                    @"<li><a href=""/RequestAndResponse/encoded?data=<script>alert('hacker');</script>"">Encoded bad Content</a></li>",
                    @"<li><a href=""/RequestAndResponse/form"">Form</a></li>",
                    @"<li><a href=""/RequestAndResponse/writecookie"">Write cookie</a></li>",
                    @"<li><a href=""/RequestAndResponse/readcookie"">Read cookie</a></li>",
                    @"<li><a href=""/RequestAndResponse/json"">JSON</a></li>",
                    "</ul>",
                    "</li>",
                    @"<li><a href=""/home2"">Home Controller with dependency injection</a></li>",
                    @"<li><a href=""/session"">Session</a></li>",
                    "<ul>",
                    "<li>Configuration",
                    @"<li><a href=""/configuration/appsettings"">Appsettings</a></li>",
                    @"<li><a href=""/configuration/database"">Database</a></li>",
                    "</ul>",
                    "</li>",
                    "</ul>"
                };

                var sb = new StringBuilder();
                foreach (var line in lines)
                {
                    sb.Append(line);
                }
                string html = sb.ToString().HtmlDocument("Web Sample App");

                await context.Response.WriteAsync(html);
            });
        }
    }
}
