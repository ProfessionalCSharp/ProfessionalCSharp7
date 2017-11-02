using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MigrationsLib;
using System;

namespace MigrationsConsoleApp
{
    class Program
    {
        private const string ConnectionString = @"server=(localdb)\mssqllocaldb;database=ProCSharpMigrations;trusted_connection=true";

        static void Main(string[] args)
        {
            RegisterServices();
            var context = Container.GetService<MenusContext>();
            context.Database.Migrate();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<MenusContext>(options =>
                options.UseSqlServer(ConnectionString));
            Container = services.BuildServiceProvider();
        }
        public static IServiceProvider Container { get; private set; }
    }
}
