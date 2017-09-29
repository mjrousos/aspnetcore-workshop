using Lab6.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.IO;

namespace Lab6
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
                .UseUrls("http://localhost:8081")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

        private static void SetupAppLogging(WebHostBuilderContext context, ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));

            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();

            // We only want to emit messages from the console logger that are
            // from the Startup class.
            loggingBuilder.AddFilter<ConsoleLoggerProvider>((category, level) => {
                return category == typeof(Startup).FullName;
            });
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
