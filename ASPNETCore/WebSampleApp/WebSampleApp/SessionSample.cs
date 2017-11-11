using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace WebSampleApp
{
    public static class SessionSample
    {
        private const string SessionVisits = nameof(SessionVisits);
        private const string SessionTimeCreated = nameof(SessionTimeCreated);
        public static async Task SessionAsync(HttpContext context)
        {
            int visits = context.Session.GetInt32(SessionVisits) ?? 0;
            string timeCreated = context.Session.GetString(SessionTimeCreated) ?? string.Empty;
            if (string.IsNullOrEmpty(timeCreated))
            {
                timeCreated = DateTime.Now.ToString("t", CultureInfo.InvariantCulture);
                context.Session.SetString(SessionTimeCreated, timeCreated);
            }
            DateTime timeCreated2 = DateTime.Parse(timeCreated);
            context.Session.SetInt32(SessionVisits, ++visits);
            await context.Response.WriteAsync(
              $"Number of visits within this session: {visits} " +
              $"that was created at {timeCreated2:T}; " +
              $"current time: {DateTime.Now:T}");
        }
    }
}
