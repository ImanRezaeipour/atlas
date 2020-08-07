using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
   public class DesignedReportTrafficColumn : IEntity
    {
       public virtual Decimal ID { get; set; }

       public virtual string FnName { get; set; }
       public virtual string EnName { get; set; }
       public virtual string Name { get; set; }
       public virtual DesignedReportTrafficKeyColumn Key { get; set; }
    }
}
