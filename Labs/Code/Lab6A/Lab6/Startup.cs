﻿using Lab6.Data;
using Lab6.Middleware;
using Lab6.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Globalization;

namespace Lab6
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
            // Add localization services (StringLocalizer, HtmlLocalizer, etc. with
            // resources from the given path)
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddDataAnnotationsLocalization() // Localize data annotation error messages
                .AddXmlDataContractSerializerFormatters();

            services.AddDbContext<OrdersContext>(contextOptions =>
                contextOptions.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }));

            services.Configure<StoreSettingsOptions>(Configuration.GetSection("StoreSettings"));
            services.Configure<RequestCultureOptions>(options =>
            {
                options.DefaultCulture = new CultureInfo(Configuration["DefaultCulture"] ?? "en-GB");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> startupLogger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseRequestCulture();

            var supportedCultures = new[]
            {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr-FR")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));

            app.UseMvc();

            startupLogger.LogInformation("Application startup complete!");

            startupLogger.LogCritical("This is a critical message");
            startupLogger.LogDebug("This is a debug message");
            startupLogger.LogTrace("This is a trace message");
            startupLogger.LogWarning("This is a warning message");
            startupLogger.LogError("This is an error message");
        }
    }
}
