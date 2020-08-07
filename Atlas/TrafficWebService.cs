using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TrafficWebService" in code, svc and config file together.
[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]    
public class TrafficWebService : ITrafficWebService
{
    public BTraffic TrafficsBusiness
    {
        get
        {
            return new BTraffic();
        }
    }
    public void TransferTrafficsByConditions(decimal operatorPersonID, TrafficTransferMode TTM, decimal machineID, string fromDate, string toDate, string fromTime, string toTime, int fromRecord, int toRecord, decimal fromIdentifier, decimal toIdentifier, string transferDay, string transferTime, TrafficTransferType TTT, bool IsIntegralConditions)
    {
        this.TrafficsBusiness.TransferTraffics(operatorPersonID, TTM, machineID, fromDate, toDate, fromTime, toTime, fromRecord, toRecord, fromIdentifier, toIdentifier, transferDay, transferTime, TTT, IsIntegralConditions);
    }

    public Dictionary<string, int> GetTrafficTranferCount()
    {
        return this.TrafficsBusiness.GetTrafficTranferCount();  
    }

    public void ClearTrafficTransferCounts()
    {
        this.TrafficsBusiness.ClearTrafficTransferCounts();
    }
}
