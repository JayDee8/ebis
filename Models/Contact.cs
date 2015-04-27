using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ebis.Models
{
    public class Contact
    {
        public Int32 ID { get; set; }
        public Int32 FirstName { get; set; }
        public Int32 LastName { get; set; }
        public Int32 Email { get; set; }
        public Int32 Phone { get; set; }
    }
}