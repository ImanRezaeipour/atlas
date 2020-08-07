using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
    public class WorkGroupProxy
    {
        public decimal ID { get; set; }
        public string WorkGroupCode { get; set; }
        public string WorkGroupName { get; set; }
        public bool IsUsedByYearlyHoliday { get; set; }

    }
}
