using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class ApprovalAttendanceScheduleExceptionProxy
    {
        public Decimal ID { get; set; }
        public string ApprovalType { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal PersonID { get; set; }
        public string PersonFullName { get; set; }

        public string PersonCode { get; set; }

        public decimal ApprovalAttendanceScheduleID { get; set; }
    }
}
