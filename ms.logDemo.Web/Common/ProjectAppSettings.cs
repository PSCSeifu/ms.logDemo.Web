using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ms.logDemo.Web.Common
{
    public class ProjectAppSettings
    {
        public string LocalAPIServer { get; set; }
        public string AzureAPIServer { get; set; }
        public string Serilog_Seq { get; set; }
    }
}
