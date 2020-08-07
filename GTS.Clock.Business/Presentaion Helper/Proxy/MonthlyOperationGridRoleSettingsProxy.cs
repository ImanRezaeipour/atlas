using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Proxy
{
   public class MonthlyOperationGridRoleSettingsProxy
    {
        public decimal ID { get; set; }
        public decimal RoleID { get; set; }
        public string GridColumn { get; set; }
        public bool ViewState { get; set; }
        public bool Exist { get; set; } 
    }
}
