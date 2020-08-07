using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UIValidation
{
     public class UIValidationGroup: IEntity
     {
         #region
         public virtual Decimal ID { get; set; }
         public virtual String Name{ get; set; }
         public virtual String CustomCode { get; set; }
         public virtual int SubSystemID { get; set; }
         public virtual IList<PersonTASpec> PersonTAList { get; set; }
         public virtual IList<UIValidationRuleGroup> RuleGroupList { get; set; }
    
         #endregion
     }
}
