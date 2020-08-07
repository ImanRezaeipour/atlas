using Atlas.ServiceProvider.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProfile" in both code and config file together.
[ServiceContract]
public interface IProfile
{
    [OperationContract]
    List<TreeItem> GetAllManagerDepartmentTreeByMelliCode(string code);

    [OperationContract]
    PersonProxy GetPersonByMelliCode(string code);

    [OperationContract]
    List<PersonProxy> GetPersonsByDepartmentId(int DepartmentId);
}

