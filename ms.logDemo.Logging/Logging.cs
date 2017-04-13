using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ms.logDemo.Logging
{
    public class PSCLogging
    {
        /// <summary>
        /// Configure and initialise serilog logger with Seq
        /// </summary>
        public void InitLogging()
            => Log.Logger = new LoggerConfiguration()
                            .Enrich.WithProperty("Log Demo Web App", "LogDemoWeb")
                            .MinimumLevel.Verbose()
                            .WriteTo.AzureLogAnalytics("0c091bfc-a5b8-4deb-ba96-f8c68953228e", "0c091bfc-a5b8-4deb-ba96-f8c68953228e")
                            .WriteTo.Seq("http://localhost:5341/")
                            .CreateLogger();
        
    }


}
