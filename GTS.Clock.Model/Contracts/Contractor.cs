using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Contracts
{
    public class Contractor:IEntity
    {
        public virtual Decimal ID { get; set; }

        public virtual String Name { get; set; }

        public virtual String Organization { get; set; }

        public virtual String Code { get; set; }

        public virtual String EconomicCode { get; set; }

        public virtual String Tel { get; set; }

        public virtual String Fax { get; set; }

        public virtual String Email { get; set; }

        public virtual String Address { get; set; }

        public virtual String Description { get; set; }

        public virtual IList<Contract> ContractsList { get; set; }

        public virtual Boolean IsDefault { get; set; }
    }


}
