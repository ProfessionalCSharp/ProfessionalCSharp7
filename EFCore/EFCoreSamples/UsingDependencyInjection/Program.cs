using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace UsingDependencyInjection
{
    class Program
    {
        static async Task Main()
        {
            var p = new Program();
            p.InitializeServices();
            p.ConfigureLogging();
            var service = p.Container.GetService<BooksService>();
            await service.AddBooksAsync();
            await service.ReadBooksAsync();
            p.Container.Dispose();
        }

        private void InitializeServices()
        {
            const string ConnectionString =
              @"server=(localdb)\MSSQLLocalDb;database=Books;trusted_connection=true";
            var services = new ServiceCollection();
            services.AddTransient<BooksService>()
              .AddEntityFrameworkSqlServer()
              .AddDbContext<BooksContext>(options =>
                options.UseSqlServer(ConnectionString));
            services.AddLogging();

            Container = services.BuildServiceProvider();
        }
        public ServiceProvider Container { get; private set; }

        private void ConfigureLogging()
        {
            ILoggerFactory loggerFactory = Container.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Information);
        }
    }
}
