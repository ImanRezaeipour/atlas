using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
  public class ShiftExceptionProxy
    {
      public string Date { get; set; }
      public string ShiftName { get; set; }
      public string ShiftExceptionName { get; set; }
      public bool First { get; set; }
      public bool Second { get; set; }
    }
}
