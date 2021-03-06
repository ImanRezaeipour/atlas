using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Model.RequestFlow
{
    #region Comments
    /// <h3>Changes</h3>
    /// 	<listheader>
    /// 		<th>Author</th>
    /// 		<th>Date</th>
    /// 		<th>Details</th>
    /// 	</listheader>
    /// 	<item>
    /// 		<term>Farhad Salavati</term>
    /// 		<description>2011-12-27</description>
    /// 		<description>Created</description>
    /// 	</item>

    #endregion

    public class Operator : IEntity
    {
        public Operator()
        {
            this.Description = "";
        }
     
        #region Properties

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the Active value.
        /// </summary>
        public virtual Boolean Active { get; set; }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public virtual String Description { get; set; }

        /// <summary>
        /// Gets or sets the PersonId value.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// جریان کاری
        /// </summary>
        public virtual Flow Flow { get; set; }

		public virtual IList<GTS.Clock.Model.Temp.Temp> TempList { get; set; } 


        #endregion
        public class OperatorComparer : IEqualityComparer<Operator>
        {
            public bool Equals(Operator x, Operator y)
            {
                return x.Person.ID == y.Person.ID;
            }
            public int GetHashCode(Operator obj)
            {
                return obj.Person.ID.GetHashCode();
            }
        }
    }
}