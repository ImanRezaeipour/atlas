using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Infrastructure.Log
{
    public class DataCollectorLogger : BaseLog
    {
        const string PersonBarcode = "PersonBarcode";
        const string TrafficDateTime = "TrafficDateTime";
        const string RecieveDateTime = "RecieveDateTime";
        const string DeviceID = "DeviceID";
        const string Status = "Status";
        const string Exception = "Exception";

        public DataCollectorLogger()
            : base(LogSource.DataCollectorLog)
        {
        }

        public void Error(string personBarcode, DateTime trafficDateTime, DateTime recieveDateTime, string deviceID, string status, object message, Exception exception)
        {
            ILog m_Log = base.GetLogFactory();
            log4net.GlobalContext.Properties[PersonBarcode] = personBarcode;
            log4net.GlobalContext.Properties[TrafficDateTime] = trafficDateTime;
            log4net.GlobalContext.Properties[RecieveDateTime] = recieveDateTime;
            log4net.GlobalContext.Properties[DeviceID] = deviceID.ToString();
            log4net.GlobalContext.Properties[Status] = status;
            m_Log.Error(message);
            base.Flush();
            log4net.GlobalContext.Properties[PersonBarcode] = "";
            log4net.GlobalContext.Properties[TrafficDateTime] = "";
            log4net.GlobalContext.Properties[RecieveDateTime] = "";
            log4net.GlobalContext.Properties[DeviceID] = "";
            log4net.GlobalContext.Properties[Status] = "";
        }

        public void Info(string personBarcode, DateTime trafficDateTime, DateTime recieveDateTime, string deviceID, string status, object message, Exception exception)
        {
            ILog m_Log = base.GetLogFactory();
            log4net.GlobalContext.Properties[PersonBarcode] = personBarcode;
            log4net.GlobalContext.Properties[TrafficDateTime] = trafficDateTime;
            log4net.GlobalContext.Properties[RecieveDateTime] = recieveDateTime;
            log4net.GlobalContext.Properties[DeviceID] = deviceID.ToString();
            log4net.GlobalContext.Properties[Status] = status;
            m_Log.Info(message);
            base.Flush();
            log4net.GlobalContext.Properties[PersonBarcode] = "";
            log4net.GlobalContext.Properties[TrafficDateTime] = "";
            log4net.GlobalContext.Properties[RecieveDateTime] = "";
            log4net.GlobalContext.Properties[DeviceID] = "";
            log4net.GlobalContext.Properties[Status] = "";
        }



    }
}
