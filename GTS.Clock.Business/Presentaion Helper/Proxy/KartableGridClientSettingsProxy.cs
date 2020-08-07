using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
   public class KartableGridClientSettingsProxy
    {
       public decimal ID { get; set; }
       public decimal CurrentUserSettingID { get; set; }
       public string GridColumn {get; set;}
       public bool ViewState { get; set; }
        public bool Exist { get; set; }
    }
}
