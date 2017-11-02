using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ContextPoolSample
{
    class Program
    {
        static async Task Main()
        {
            var p = new Program();
            p.InitializeServices();
            p.ConfigureLogging();
            var service = p.Container.GetService<BooksService>();
            await p.Container.GetService<BooksController>().CreateDatabaseAsync();
            await p.Container.GetService<BooksController>().AddBooksAsync();
            await p.Container.GetService<BooksController>().ReadBooksAsync();
            await p.Container.GetService<BooksController>().ReadBooksAsync();
            await p.Container.GetService<BooksController>().ReadBooksAsync();
            p.Container.Dispose();
        }


        private void InitializeServices()
        {
            const string ConnectionString =
              @"server=(localdb)\MSSQLLocalDb;database=BooksWithContextPool;trusted_connection=true";
            var services = new ServiceCollection();
            services.AddTransient<BooksController>();
            services.AddTransient<BooksService>();
            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<BooksContext>(options => options.UseSqlServer(ConnectionString));
            services.AddLogging(); ;


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
