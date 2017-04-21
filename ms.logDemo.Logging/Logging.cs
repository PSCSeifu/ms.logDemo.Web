using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using ms.logDemo.Types.ProjectAppSettings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ms.logDemo.Logging
{
    public class PSCLogging
    {
        //private IOptions<ProjectAppSettings> _projectAppSettingsAccessor;
        //public PSCLogging(IOptions<ProjectAppSettings> projectAppSettingsAccessor)
        //{
        //    _projectAppSettingsAccessor = projectAppSettingsAccessor;
        //}
        /// <summary>
        /// Configure and initialise serilog logger with Seq
        /// </summary>
        public void InitLogging()
        {
            //var storage = CloudStorageAccount.Parse(_projectAppSettingsAccessor.Value.AzureTableStorageConnectionString);
            //var storage = CloudStorageAccount.Parse("");
            var storage = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=psclogdemo;AccountKey=/N2TXfgKeRK34RhfFOZknKUJe6ZdCJSBqxH9ucGRupZVSi5NEV520fMeB3nsSpke8WD1Ve6EY+gKjEdmtUk4Zg==;EndpointSuffix=core.windows.net");
            Log.Logger = new LoggerConfiguration()
                          .Enrich.WithProperty("Log Demo Web App", "LogDemoWeb")
                          //.WriteTo.AzureTableStorage(storage)
                          //.WriteTo.ApplicationInsightsEvents("7c7e2b30-936e-4959-b64c-7e8dd517405c")
                          .MinimumLevel.Verbose()
                           .WriteTo.AzureTableStorage(storage)
                          //.WriteTo.AzureLogAnalytics("0c091bfc-a5b8-4deb-ba96-f8c68953228e", "0c091bfc-a5b8-4deb-ba96-f8c68953228e")
                          .WriteTo.Seq("http://localhost:5341/")
                          .CreateLogger();
        }
    }


}
