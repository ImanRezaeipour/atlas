using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.Model.Temp
{

    public class Temp : IEntity
	{
		#region Properties
		/// <summary>
		/// Gets or sets the ID value.
		/// </summary>
		public virtual Decimal ID { get; set; }

		/// <summary>
        /// Gets or sets the ObjectID value.
		/// </summary>
        public virtual decimal ObjectID { get; set; }

		/// <summary>
        /// Gets or sets the OperationGUID value.
		/// </summary>
        public virtual String OperationGUID { get; set; }

        /// <summary>
        /// Gets or sets the CreationDate value.
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as Temp;

            if (t == null)
                return false;

            if (this.ObjectID == t.ObjectID && this.OperationGUID == t.OperationGUID)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return (this.ObjectID + "|" + this.OperationGUID).GetHashCode();
        }

		#endregion		
	}
}