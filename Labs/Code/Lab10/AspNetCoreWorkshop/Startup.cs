using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using AspNetCoreWorkshop.Data;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWorkshop.Models;
using AspNetCoreWorkshop.Services;
using AspNetCoreWorkshop.Middleware;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.IO;

namespace AspNetCoreWorkshop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddDataAnnotationsLocalization() // Localize data annotation error messages
                .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }));
            services.Configure<StoreSettingsOptions>(Configuration.GetSection("StoreSettings"));

            //services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "StoreDatabase.db");
            services.AddDbContext<StoreContext>(options => options.UseSqlite($"Data Source={dbPath};"));

            services.AddSingleton<IRequestIdFactory, RequestCounter>();

            services.Configure<RequestCultureOptions>(options =>
            {
                options.DefaultCulture = new CultureInfo(Configuration["DefaultCulture"] ?? "en-GB");
            });

            // Add localization services (StringLocalizer, HtmlLocalizer, etc. with 
            // resources from the given path)
            services.AddLocalization(options => options.ResourcesPath = "Resources");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> startupLogger)
        {
            app.UseStatusCodePages("text/html", "<p>You got a <strong>{0}</strong></p>");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(subApp =>
                {
                    subApp.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("<strong> Application error. Please contact support. </strong>");
                        await context.Response.WriteAsync(new string(' ', 512));  // Padding for IE
                    });
                });
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr-FR")
            };

            // app.UseRequestCulture();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();

            app.UseMiddleware<RequestIdMiddleware>();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            startupLogger.LogInformation("Application startup complete!");

            //startupLogger.LogCritical("This is a critical message");
            //startupLogger.LogDebug("This is a debug message");
            //startupLogger.LogTrace("This is a trace message");
            //startupLogger.LogWarning("This is a warning message");
            //startupLogger.LogError("This is an error message");
        }
    }
}
