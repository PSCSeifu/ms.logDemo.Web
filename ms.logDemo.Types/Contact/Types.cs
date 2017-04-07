using System;
using System.Collections.Generic;
using System.Text;

namespace ms.logDemo.Types.Contact
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public int Age { get; set; }
    }

    public class ContactInfoDTO
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        public int Age { get; set; }
    }

    public class ContactListDTO
    {
        public int Count { get { return Items.Count; } }
        public List<ContactInfoDTO> Items { get; set; } =
            new List<ContactInfoDTO>();
    }

}
