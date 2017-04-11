using System;
using System.Collections.Generic;
using System.Text;

namespace ms.LogDemo.Data.Common
{
    public  class BaseRepository
    {
        public string URL { get; set; } 
        public string  Path { get; set; }
        public string  Response { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
