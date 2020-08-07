using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;


/// <summary>
/// این لاگر بمنظور ثبت خطاها در سرویس های بیزینس مربوط به واسط کاربر
/// استفاده میشود
/// </summary>
/// 

namespace GTS.Clock.Infrastructure.Log
{  
    public class GTSEngineDebugLogger:BaseLog
    {
        const string PersonBarcode = "PersonBarcode";
        const string ShamsiDate = "ShamsiDate";
        const string RuleIden = "RuleIden";
        const string RuleOrder = "RuleOrder";
        const string CnpIden = "CnpIden";
        const string CnpName = "cnpName";
        const string BeforeValue = "BeforeValue";
        const string AfterValue = "AfterValue";

        /// <summary>
        /// فقط برای لاگ گرفتن خطاهای بیزینس
        /// </summary>
        public GTSEngineDebugLogger()
            : base(LogSource.EngineDebugToDB)
        {
          
        }

        public void Info(string personCode, string ruleIden, string ruleOrder, string cnpIden, string shamsiDate, string before, string after, string cnpName,string ruleTitle)
        {
      

            ILog m_Log = base.GetLogFactory();
            log4net.GlobalContext.Properties[RuleIden] = ruleIden;
            log4net.GlobalContext.Properties[RuleOrder] = ruleOrder;
            log4net.GlobalContext.Properties[CnpIden] = cnpIden;
            log4net.GlobalContext.Properties[CnpName] = cnpName;
            log4net.GlobalContext.Properties[ShamsiDate] = shamsiDate;
            log4net.GlobalContext.Properties[BeforeValue] = before;
            log4net.GlobalContext.Properties[AfterValue] = after;
            log4net.GlobalContext.Properties[PersonBarcode] = personCode;
            m_Log.Info(ruleTitle);
            base.Flush();
            log4net.GlobalContext.Properties[RuleIden] = "";
            log4net.GlobalContext.Properties[ruleOrder] = "";
            log4net.GlobalContext.Properties[CnpIden] = "";
            log4net.GlobalContext.Properties[cnpName] = "";
            log4net.GlobalContext.Properties[ShamsiDate] = "";
            log4net.GlobalContext.Properties[BeforeValue] = "";
            log4net.GlobalContext.Properties[AfterValue] = "";
            log4net.GlobalContext.Properties[PersonBarcode] = "";
        }
    }
}