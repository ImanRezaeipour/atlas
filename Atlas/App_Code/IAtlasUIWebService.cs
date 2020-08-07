using GTS.Clock.Model.BoxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAtlasUIWebService" in both code and config file together.
[ServiceContract]
public interface IAtlasUIWebService
{
    [OperationContract]
    string Authenticate(string username, string password);

    [OperationContract]
    IList<KartablSummary> GetKartablSummary(string username, string token);
}

[DataContract]
public enum FaultKey
{
    UsernameIsNotValued,
    PasswordIsNotValued,
    InvalidUserNameOrPassword,
    IllegalAccess
}




