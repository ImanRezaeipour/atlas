using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class PersonDateRangeReportProxy
    { 
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal PersonId { get; set; }
        public string Barcode { get; set; }
        public string CardNum { get; set; }
        public int PresenceDuration { get; set; }
        public int ImpureOperation { get; set; }
        public int HourlyMission { get; set; }
        public int HourlyUnallowableAbsence { get; set; }

    }
}
