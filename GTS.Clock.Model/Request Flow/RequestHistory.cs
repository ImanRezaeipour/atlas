using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Infrastructure;
namespace GTS.Clock.Model.RequestFlow
{
  public class RequestHistory : IEntity
    {
    
      public virtual decimal ID { get; set; }
      public virtual Request Request { get; set; }
      public virtual DateTime FromDate { get; set; }
      public virtual DateTime ToDate { get; set; }
      public virtual int FromTime { get; set; }
      public virtual int ToTime { get; set; }
      public virtual int Duration { get; set; }
      public virtual string TheFromDate { get; set; }
      public virtual string TheToDate { get; set; }
      public virtual string TheFromTime { get; set; }
      public virtual string TheToTime { get; set; }
      public virtual string TheDuration { get; set; }
      public virtual RequestType RequestType { get; set; }
      public virtual string PrecardName { get; set; }
      public virtual bool ContinueOnTomorrow { get; set; }
      public virtual bool AllOnTomorrow { get; set; }
      public virtual string AttachmentFile { get; set; }
      public virtual string NewAttachmentFile { get; set; }
    }
}
