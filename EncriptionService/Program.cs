using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApps.EncriptionService.Helpers;

namespace WebApps.EncriptionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateEncryptionKeyIfNotExists(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateEncryptionKeyIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configuration = services.GetRequiredService<IConfiguration>();
                var dirName = configuration.GetValue<string>("KeyStorageFolder");

                if (!Cryptography.ExistsKey(dirName))
                {
                    Cryptography.CreateKey(dirName);
                }
                
                return;
            }
        }
    }
}
