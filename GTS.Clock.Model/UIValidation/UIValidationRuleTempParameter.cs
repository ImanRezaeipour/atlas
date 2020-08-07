using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
{
   public class UIValidationRuleTempParameter :IEntity
   {
       #region
       public virtual Decimal ID { get; set; }

       public virtual RuleParamType Type { get; set; }
       public virtual String Name { get; set; }
       public virtual String KeyName { get; set; }
       public virtual UIValidationRule UIValidationRule { get; set; }
       #endregion
   }
}
