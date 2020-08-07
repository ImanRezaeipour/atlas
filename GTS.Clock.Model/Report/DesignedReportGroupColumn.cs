using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
   public class DesignedReportGroupColumn : IEntity
    {
        public virtual Decimal ID { get; set; }
        public virtual DesignedReportColumn Column { get; set; }
     
        public virtual Report Report { get; set; }
        public virtual Person Person { get; set; }

        public virtual Int16 Order { get; set; }
        public virtual bool IsGroupingNewPage { get; set; }
    }
}
