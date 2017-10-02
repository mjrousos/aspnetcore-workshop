using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using AspNetCoreWorkshop.Data;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWorkshop.Models;
using AspNetCoreWorkshop.Services;
using AspNetCoreWorkshop.Middleware;
using Microsoft.Extensions.Logging;

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
                .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }));
            services.Configure<StoreSettingsOptions>(Configuration.GetSection("StoreSettings"));

            services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddTransient<IRequestIdFactory, RequestCounter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> startupLogger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMiddleware<RequestIdMiddleware>();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            startupLogger.LogInformation("Application startup complete!");
            startupLogger.LogCritical("This is a critical message");
            startupLogger.LogDebug("This is a debug message");
            startupLogger.LogTrace("This is a trace message");
            startupLogger.LogWarning("This is a warning message");
            startupLogger.LogError("This is a error message");
        }
    }
}
