using ChatServer.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace ChatServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(); //.AddAzureSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
                routes.MapHub<GroupChatHub>("/groupchat");
            });
            //app.UseAzureSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/chat");
            //    routes.MapHub<GroupChatHub>("/groupchat");
            //});

            app.Run(async (context) =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>SignalR Sample</h1>");
                sb.Append("<div>Open <a href='/ChatWindow.html'>ChatWindow</a> for communication</div>");
                await context.Response.WriteAsync(sb.ToString());
                
            });
        }
    }
}
