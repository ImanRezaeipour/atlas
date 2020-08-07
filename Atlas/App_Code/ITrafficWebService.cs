using GTS.Clock.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITrafficWebService" in both code and config file together.
[ServiceContract]
public interface ITrafficWebService
{
    [OperationContract(IsOneWay = true)]
    void TransferTrafficsByConditions(decimal operatorPersonID, TrafficTransferMode TTM, decimal machineID, string fromDate, string toDate, string fromTime, string toTime, int fromRecord, int toRecord, decimal fromIdentifier, decimal toIdentifier, string transferDay, string transferTime, TrafficTransferType TTT, bool IsIntegralConditions);

    [OperationContract]
    Dictionary<string, int> GetTrafficTranferCount();

    [OperationContract]
    void ClearTrafficTransferCounts();
}
