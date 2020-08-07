using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.UI
{    
   public class KartableGridClientSettings:IEntity
   {
       #region Properties
       public virtual Decimal ID { get; set; }
       public virtual AppSetting.UserSettings UserSetting { get; set; }
       public virtual GridSettingCaller Type { get; set; }
       public virtual Boolean FlowStatus { get; set; }
       public virtual Boolean FlowLevels { get; set; }
       public virtual Boolean RequestType { get; set; }
       public virtual Boolean RequestSource { get; set; }
       public virtual Boolean Select { get; set; }
       public virtual Boolean Row { get; set; }
       public virtual Boolean BarCode { get; set; }
       public virtual Boolean Applicant { get; set; }
       public virtual Boolean RequestTitle { get; set; }
       public virtual Boolean TheFromDate { get; set; }
       public virtual Boolean TheToDate { get; set; }
       public virtual Boolean TheFromTime { get; set; }
       public virtual Boolean TheToTime { get; set; }
       public virtual Boolean TheDuration { get; set; }
       public virtual Boolean RegistrationDate { get; set; }
       public virtual Boolean OperatorUser { get; set; }
       public virtual Boolean DepartmentName { get; set; }
       public virtual Boolean Description { get; set; }
       public virtual Boolean AttachmentFile { get; set; }
       public virtual Boolean RequestHistory { get; set; }

       #endregion
   }
}
