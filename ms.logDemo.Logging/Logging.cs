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
                            .WriteTo.Seq("http://localhost:5341/")
                            .CreateLogger();
        
    }


}
