using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncStreamsWithSignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AsyncStreamsWithSignalR
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();

            services.AddSignalR()
                .AddHubOptions<ClientToServerStreamingHub>(config =>
                {
                    config.EnableDetailedErrors = true;
                })
                .AddHubOptions<ServerToClientStreamingHub>(config =>
                {
                    config.EnableDetailedErrors = true;
                })
                .AddJsonProtocol();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ServerToClientStreamingHub>("/hubs/stream");
                endpoints.MapHub<ClientToServerStreamingHub>("/hubs/uploadstream");

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
