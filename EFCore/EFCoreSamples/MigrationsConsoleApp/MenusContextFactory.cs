using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MigrationsLib;

namespace MigrationsConsoleApp
{
    public class MenusContextFactory : IDesignTimeDbContextFactory<MenusContext>
    {
        private const string ConnectionString = @"server=(localdb)\mssqllocaldb;database=ProCSharpMigrations;trusted_connection=true";

        public MenusContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MenusContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new MenusContext(optionsBuilder.Options);
        }
    }
}
