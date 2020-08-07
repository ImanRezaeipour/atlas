using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
   public class RuleGeneratorResourceProxy
    {
       public string ResourceIF { get; set; }
       public string ResourceTHEN { get; set; }
       public string ResourceELSE { get; set; }
       public string InvalidFirstOrder { get; set; }
       public string InvalidSecondOrder { get; set; }
       public string RuleWithoutCondition { get; set; }
       public string RuleWithoutOrder { get; set; }
      
       }
}
