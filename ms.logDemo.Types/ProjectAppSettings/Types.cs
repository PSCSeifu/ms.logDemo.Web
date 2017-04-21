using System;
using System.Collections.Generic;
using System.Text;

namespace ms.logDemo.Types.ProjectAppSettings
{
    public class ProjectAppSettings
    {
        public string LocalAPIServer { get; set; }
        public string AzureAPIServer { get; set; }
        public string Serilog_Seq { get; set; }
        public string AzureTableStorageConnectionString { get; set; }
    }
}
