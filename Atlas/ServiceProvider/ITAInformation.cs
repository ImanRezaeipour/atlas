using Atlas.ServiceProvider.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

 
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITAInformation" in both code and config file together.
    [ServiceContract]
    public interface ITAInformation
    {
        /// <summary>
        ///  اطلاعات حضور غیاب شخص در بازه زمانی را برمیگرداند
        /// </summary>
        [OperationContract]
        TAProxy GetTAInfo(string personCode, DateTime fromDate, DateTime toDate);
    }
 
