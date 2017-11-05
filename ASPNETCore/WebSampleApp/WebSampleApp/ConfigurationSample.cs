using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebSampleApp
{
    public class SubSection1
    {
        public string Setting4 { get; set; }
    }

    public class AppSettings
    {
        public string Setting2 { get; set; }
        public string Setting3 { get; set; }
        public SubSection1 SubSection1 { get; set; }
    }

    public class ConfigurationSample
    {
        private readonly IConfiguration _configuration;
        public ConfigurationSample(IConfiguration configuration) =>
            _configuration = configuration;

        public async Task ShowApplicationSettingsAsync(HttpContext context)
        {
            string settings = _configuration.GetSection("SampleSettings")["Setting1"];
            await context.Response.WriteAsync(settings.Div());
        }

        public async Task ShowApplicationSettingsUsingColonsAsync(HttpContext context)
        {
            string settings = _configuration["SampleSettings:Setting1"];
            await context.Response.WriteAsync(settings.Div());
        }

        public async Task ShowApplicationSettingsStronglyTyped(HttpContext context)
        {
            AppSettings settings = _configuration.GetSection("AppSettings").Get<AppSettings>();
            await context.Response.WriteAsync($"setting 2: {settings.Setting2}, setting3: {settings.Setting3}, setting4: {settings.SubSection1.Setting4}".Div());
        }

        public async Task ShowConnectionStringSettingAsync(HttpContext context)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            await context.Response.WriteAsync(connectionString.Div());
        }
    }
}