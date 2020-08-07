using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
   public class CollectServiceRequest :IEntity
    {
        public virtual Decimal ID { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual DateTime RegisterDate { get; set; }
        public virtual int FromTime { get; set; }
        public virtual int ToTime { get; set; }
        public virtual Boolean IsApplied { get; set; }
        public virtual CollectServiceOperationType OperationType { get; set; }

        public virtual BasicTraffic FromTraffic { get; set; }
        public virtual BasicTraffic ToTraffic { get; set; }
        public virtual Person Person { get; set; }
        public virtual Precard Precard { get; set; }
      


    }
}
