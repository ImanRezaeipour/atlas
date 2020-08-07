using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
{
   public class UIValidationRuleParam :IEntity
   {
       #region
       public virtual Decimal ID { get; set; }
   
      
       public virtual String Value { get; set; }
       public virtual UIValidationRuleGroupPrecard UIValidationPrecard { get; set; }
       public virtual UIValidationRuleTempParameter UIValidationRuleTempParam { get; set; }

       public virtual bool ContinueOnTomorrow { get; set; }
       public virtual String TheValue { get; set; }
       //public virtual UIValidationRuleGroup Grouping
       //{
       //    get;
       //    set;
       //}
       #endregion
   }
}
