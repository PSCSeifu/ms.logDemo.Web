using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ms.logDemo.Web.Common;
using Serilog;
using Microsoft.WindowsAzure.Storage;

namespace ms.logDemo.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            InitLogging();
        }

        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Configures Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            AddApplicationInsights(services);
            AddMvc(services);
            AddBusiness(services);
            AddHttpClient(services);
            AddConfigOptions(services);
        }
                
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            UseLogging(loggerFactory);

            app.UseApplicationInsightsRequestTelemetry();

            UseExceptionHandling(app, env);

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();      

            UseMvc(app);

        }

        #region " Add Services "

        private void AddApplicationInsights(IServiceCollection services)
            => services.AddApplicationInsightsTelemetry(Configuration);      

        private void AddMvc(IServiceCollection services) 
            =>  services.AddMvc();

        private void AddBusiness(IServiceCollection services)
            => DependencyInjection.Configure(services);

        private void AddHttpClient(IServiceCollection services)
            => services.AddSingleton<IHttpClientsFactory, HttpClientsFactory>();

        private void AddConfigOptions(IServiceCollection services)
            => services.Configure<ProjectAppSettings>(options => Configuration.GetSection("ServicePoints").Bind(options));

        #endregion

        #region " Use Middleware"

        private void UseExceptionHandling(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage().UseBrowserLink();
            else
                app.UseExceptionHandler("/Home/Error/");
        }

        private void UseMvc(IApplicationBuilder app) 
            => app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        private void UseLogging(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole()
                .AddSerilog()
                .AddDebug();
        }


        #endregion

        #region " Private "

        private static void InitLogging()
        {
            var log = new logDemo.Logging.PSCLogging();
            log.InitLogging();

           
            //Log.Logger = new LoggerConfiguration()
            //                .Enrich.WithProperty("Log Demo Web App", "LogDemoWeb")
            //                .MinimumLevel.Verbose()
            //                .WriteTo.AzureLogAnalytics("0c091bfc-a5b8-4deb-ba96-f8c68953228e", "0c091bfc-a5b8-4deb-ba96-f8c68953228e")
            //                .WriteTo.Seq("http://localhost:5341/")
            //                .CreateLogger();
        }
        #endregion
    }
}
