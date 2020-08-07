using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
{
   public class UIValidationRuleGroup :IEntity
   {
       #region
       public virtual Decimal ID { get; set; }
       public virtual Boolean Active { get; set; }
       public virtual Boolean Warning { get; set; }
       public virtual string Tag { get; set; }
       public virtual IList<UIValidationRuleGroupPrecard> PrecardList { get; set; }
       public virtual UIValidationGroup ValidationGroup { get; set; }

       public virtual UIValidationRule ValidationRule { get; set; }
     //  public virtual IList<UIValidationRuleGroupPrecard> PrecardList { get; set; }
       #endregion
   }
}
