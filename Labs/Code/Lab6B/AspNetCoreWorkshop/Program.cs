using AspNetCoreWorkshop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;
using System;
using System.IO;

namespace AspNetCoreWorkshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logfile.txt")
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var host = BuildWebHost(args);

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetService<StoreContext>();
                    if (context != null)
                    {
                        DbInitializer.Initialize(context);
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(SetupAppConfiguration)
                .UseSerilog()
                .UseUrls("http://0.0.0.0:8081")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

        private static void SetupAppConfiguration(WebHostBuilderContext context, IConfigurationBuilder configBuilder)
        {
            var env = context.HostingEnvironment;

            configBuilder.AddJsonFile("appsettings.json", false, true);
            configBuilder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                configBuilder.AddUserSecrets<Program>(true);
            }

            configBuilder.AddEnvironmentVariables();
        }
    }
}

