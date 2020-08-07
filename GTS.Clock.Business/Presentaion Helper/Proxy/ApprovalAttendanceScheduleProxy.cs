using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class ApprovalAttendanceScheduleProxy
    {
        public Decimal ID { get; set; }
        public string ApprovalType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int DateRangeOrder { get; set; }

        public Decimal CostCenterID { get; set; }

        public string CostCenterName { get; set; }
    }
}
