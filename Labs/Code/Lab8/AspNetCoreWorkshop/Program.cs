using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreWorkshop.Data;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AspNetCoreWorkshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<OrdersContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(SetupAppConfiguration)
                .ConfigureLogging(SetupAppLogging)
                .UseUrls("http://0.0.0.0:8081")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

        private static void SetupAppLogging(WebHostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        }

        private static void SetupAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configBuilder)
        {
            var env = context.HostingEnvironment;

            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configBuilder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                configBuilder.AddUserSecrets<Program>(true);
            }

            configBuilder.AddEnvironmentVariables();
        }
    }
}
