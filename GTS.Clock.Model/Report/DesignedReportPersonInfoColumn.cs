using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Report
{
  public  class DesignedReportPersonInfoColumn : IEntity
    {
        public virtual Decimal ID { get; set; }
        public virtual string FnName { get; set; }
        public virtual string EnName { get; set; }
        public virtual string Name { get; set; }
        public virtual DesignedReportPersonInfoKeyColumn Key { get; set; }
        public virtual string FieldName  { get; set; }
        public virtual Boolean IsGroupColumn { get; set; }
        public virtual Boolean IsReserveField { get; set; }
    }
}
