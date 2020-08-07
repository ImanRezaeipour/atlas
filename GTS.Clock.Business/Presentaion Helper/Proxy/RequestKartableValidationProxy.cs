using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
	public class RequestKartablValidationProxy
	{
		public string PrecardName { get; set; }
		public string Date { get; set; }
		public string PersonName { get; set; }
		public Exception Exception { get; set; }
	}
}
