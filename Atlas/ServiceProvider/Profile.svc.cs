using Atlas.ServiceProvider.Proxy;
using GTS.Clock.Business;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Profile" in code, svc and config file together.
// NOTE: In order to launch WCF Test Client for testing this service, please select Profile.svc or Profile.svc.cs at the Solution Explorer and start debugging.
public class Profile : IProfile
{
    #region Properties

    public ExceptionHandler exceptionHandler
    {
        get
        {
            return new ExceptionHandler();
        }
    }

    #endregion

    public List<TreeItem> GetAllManagerDepartmentTreeByMelliCode(string code)
    {
        try
        {
            BDepartment DepartmentBusiness = new BDepartment();
            BPerson PersonBusinesss = new BPerson();
            BManager ManagerBusiness = new BManager();

            var person = PersonBusinesss.GetByBarcode(code);
            if (person == null)
                return new List<TreeItem>();

            decimal managerPersonId = person.ID;

            var manager = ManagerBusiness.GetManager(managerPersonId);
            IList<Department> departments = DepartmentBusiness.GetAllManagerDepartmentTree_JustOrgan(manager.ID);

            var result = departments.Select(c => new TreeItem() { id = (Int32)c.ID, parent = c.ParentID.ToString(), text = c.Name }).ToList();
            result[0].parent = "#";
            return result;
        }
        catch (UIValidationExceptions ex)
        {
            this.exceptionHandler.ApiHandleException("ProfileServiceProvider", ex);
            throw ex;
        }
        catch (Exception ex)
        {
            this.exceptionHandler.ApiHandleException("ProfileServiceProvider", ex);
            throw ex;
        }
    }


    public PersonProxy GetPersonByMelliCode(string code)
    {
        try
        {
            BPerson PersonBusinesss = new BPerson();

            var person = PersonBusinesss.GetByBarcode(code);
            if (person == null)
                return new PersonProxy();

            PersonProxy proxy = new PersonProxy();
            proxy.ID = person.ID;
            proxy.FirstName = person.FirstName;
            proxy.LastName = person.LastName;
            proxy.BarCode = person.BarCode;
            proxy.Active = person.Active;
            proxy.IsDeleted = person.IsDeleted;
            proxy.CardNum = person.CardNum;
            proxy.EmploymentDate = person.EmploymentDate;
            proxy.ParentPath = person.Department.ParentPath;

            return proxy;
        }
        catch (UIValidationExceptions ex)
        {
            this.exceptionHandler.ApiHandleException("ProfileServiceProvider", ex);
            throw ex;
        }
        catch (Exception ex)
        {
            this.exceptionHandler.ApiHandleException("ProfileServiceProvider", ex);
            throw ex;
        }
    }


    public List<PersonProxy> GetPersonsByDepartmentId(int DepartmentId)
    {
        try
        {
            BPerson PersonBusinesss = new BPerson();
            List<PersonProxy> proxys = new List<PersonProxy>();
            var persons = PersonBusinesss.GetPersonsByDirecetDepartmentId(DepartmentId);
            foreach (var person in persons)
            {
                PersonProxy proxy = new PersonProxy();
                proxy.ID = person.ID;
                proxy.FirstName = person.FirstName;
                proxy.LastName = person.LastName;
                proxy.BarCode = person.BarCode;
                proxy.Active = person.Active;
                proxy.IsDeleted = person.IsDeleted;
                proxy.CardNum = person.CardNum;
                proxy.EmploymentDate = person.EmploymentDate;
                proxy.ParentPath = person.Department.ParentPath;
                proxys.Add(proxy);
            }

            return proxys;
        }
        catch (UIValidationExceptions ex)
        {
            this.exceptionHandler.ApiHandleException("ProfileServiceProvider", ex);
            throw ex;
        }
        catch (Exception ex)
        {
            this.exceptionHandler.ApiHandleException("ProfileServiceProvider", ex);
            throw ex;
        }
    }

}

