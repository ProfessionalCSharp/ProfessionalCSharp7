using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Text.Encodings.Web;

namespace WebApplicationSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "CustomResources");
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IStringLocalizer<Startup> sr)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var supportedCultures = new[]
            {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-AT"),
                    new CultureInfo("de"),
                    new CultureInfo("en")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            
            app.UseRequestLocalization(options);

            app.Run(async (context) =>
            {
                IRequestCultureFeature requestCultureFeature = context.Features.Get<IRequestCultureFeature>();
                RequestCulture requestCulture = requestCultureFeature.RequestCulture;
                var today = DateTime.Today;
                await context.Response.WriteAsync("<h1>Sample Localization</h1>");
                await context.Response.WriteAsync($"<div>{requestCulture.Culture} {requestCulture.UICulture}</div>");
                await context.Response.WriteAsync($"<div>{today:D}</div>");

                await context.Response.WriteAsync($"<div>{HtmlEncoder.Default.Encode(sr["message1"])}</div>");
                await context.Response.WriteAsync($"<div>{HtmlEncoder.Default.Encode(sr.GetString("message1"))}</div>");
                await context.Response.WriteAsync($"<div>{HtmlEncoder.Default.Encode(sr.GetString("message2", requestCulture.Culture, requestCulture.UICulture))}</ div>");
            });
        }
    }
}
