using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using DataAccess.Repository;
using DataAccess;
using Models.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExistsAsync(host);

            host.Run();
        }

        private static async Task CreateDbIfNotExistsAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    // Call seed method
                    var context = services.GetRequiredService<KUSYSDbContext>();
                    DbInitializer.Initialize(context);
                 
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
