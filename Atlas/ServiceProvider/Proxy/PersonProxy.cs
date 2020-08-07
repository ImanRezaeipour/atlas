using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.ServiceProvider.Proxy
{
    public class PersonProxy
    {
        public Decimal ID { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String BarCode { get; set; }
        public Boolean Active { get; set; }

        public Boolean IsDeleted { get; set; }
        public String CardNum { get; set; }
        public DateTime EmploymentDate { get; set; }

        public string ParentPath { get; set; }
    }
}