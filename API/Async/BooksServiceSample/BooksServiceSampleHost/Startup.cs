using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksServiceSample.Models;
using BooksServiceSample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace BooksServiceSampleHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddXmlSerializerFormatters();            
//          services.AddScoped<IBookChaptersService, BookChaptersService>();
            services.AddScoped<IBookChaptersService, DBBookChaptersService>();
            services.AddScoped<SampleChapters>();

            services.AddDbContext<BooksContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BooksConnection")));

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments("../docs/BooksServiceSample.xml");
                options.SwaggerDoc("v2", new Info
                {
                    Title = "Books Service API",
                    Version = "v2",
                    Description = "Sample service for Professional C# 7",
                    Contact = new Contact { Name = "Christian Nagel", Url = "https://csharp.christiannagel.com" },
                    License = new License { Name = "MIT License" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, SampleChapters sampleChapters, BooksContext booksContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Book Chapter Services"));

            bool created = booksContext.Database.EnsureCreated();
            if (created)
            {
                await sampleChapters.CreateSampleChaptersAsync();
            }
        }
    }
}
