using System;
using System.Collections.Generic;
using System.Text;

namespace ms.logDemo.Types.Tax
{
    public class TaxDTO
    {
        public int Id { get; set; }
        public string Bracket { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float Percentage { get; set; }
    }

    public class TaxInfoDTO
    {
        public int Id { get; set; }
        public string Bracket { get; set; }
        public float Percentage { get; set; }
    }

    public class TaxListDTO
    {
        public int Count { get; set; }
        public List<TaxInfoDTO> Items { get; set; } = new List<TaxInfoDTO>();
    }
}
