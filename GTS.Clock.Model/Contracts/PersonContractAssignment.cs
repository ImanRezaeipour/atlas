using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Contracts
{
    public class PersonContractAssignment : IEntity
    {
        public virtual Decimal ID { get; set; }

        public virtual DateTime FromDate { get; set; }

        public virtual DateTime ToDate { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Person Person { get; set; }

        public virtual string UIFromDate { get; set; }

        public virtual string UIToDate { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}
