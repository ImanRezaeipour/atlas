using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Model.Concepts
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
	/// 		<description>5/23/2011</description>
	/// 		<description>Created</description>
	/// 	</item>

	#endregion

    public class PrecardAccessGroupDetail : IEntity
	{
		#region Properties
		public virtual Decimal ID { get; set; }

        public virtual PrecardAccessGroup PrecardAccessGroup { get; set; }

        public virtual Precard Precard { get; set; }

        public virtual IList<GTS.Clock.Model.Temp.Temp> TempList { get; set; }

		#endregion		
	}
}