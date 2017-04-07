using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ms.logDemo.Web.Models.Contact
{
    public class ContactVM
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public int Age { get; set; }
    }

    public class ContactInfoVM
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        public int Age { get; set; }
    }

    public class ContactListVM
    {
        public int Count { get { return Items.Count; } }
        public List<ContactInfoVM> Items { get; set; } = 
            new List<ContactInfoVM>();
    }

}
