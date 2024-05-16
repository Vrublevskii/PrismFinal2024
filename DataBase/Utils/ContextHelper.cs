using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

namespace Database.Utils
{
    public static class ContextHelper
    {
        public static void RegisterContext<T>(this IContainerRegistry containerRegistry) where T : DbContext
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json");
            var connectionString = configurationBuilder.Build().GetConnectionString("DefaultConnection");
            var builder = new DbContextOptionsBuilder<T>();
            containerRegistry.RegisterInstance(builder.UseSqlite(connectionString).Options);
            containerRegistry.Register<T>();
        }
    }
}
