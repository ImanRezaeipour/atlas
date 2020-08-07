using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Contracts
{
    public class Contract:IEntity
    {
        public virtual Decimal ID { get; set; }

        public virtual String Title { get; set; }

        public virtual String Code { get; set; }

        public virtual String Description { get; set; }

        public virtual Contractor Contractor { get; set; }
        public virtual Boolean IsDefault { get; set; }
    }
}
