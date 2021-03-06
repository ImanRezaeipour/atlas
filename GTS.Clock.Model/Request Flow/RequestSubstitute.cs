using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.RequestFlow
{
    public class RequestSubstitute : IEntity
	{
        public RequestSubstitute()
        {
        }

		#region Properties

		public virtual Decimal ID { get; set; }

		public virtual Request Request { get; set; }

        public virtual Person SubstitutePerson { get; set; }

        public virtual Nullable<bool> Confirmed { get; set; }

        public virtual DateTime OperationDate { get; set; }

        public virtual string Description { get; set; }

        #endregion
    }
}