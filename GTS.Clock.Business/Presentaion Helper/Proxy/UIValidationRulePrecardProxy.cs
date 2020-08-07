using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GTS.Clock.Business.Proxy
{
  public class UIValidationRulePrecardProxy
    {
      public decimal ID { get; set; }     
      public decimal PrecardID { get; set; }
      public string PrecardName { get; set; }
      public bool Active { get; set; }
      public bool Operator { get; set; }
      public decimal ExistPrecard { get; set; }
      public string PrecardColor { get; set; }            
    }
}
