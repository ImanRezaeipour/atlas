using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.General
{
   public class VersionStatus :IEntity
    {
       public virtual decimal ID { get; set; }
        public virtual string Version { get; set; }
        public virtual bool IsCompleted { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
