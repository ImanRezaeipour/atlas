using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Report;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.Rules;
using GTS.Clock.Business.Temp;
using NHibernate;
using NHibernate.Criterion;
using System.Globalization;
using GTS.Clock.Business.BaseInformation;

namespace GTS.Clock.Business.Security
{
    /// <summary>
    /// created at: 2012-02-16 10:00:48 AM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BDataAccess : MarshalByRefObject
    {
        #region variables
        private const string ExceptionSrc = "GTS.Clock.Business.Security.BDataAccess";
        DepartmentRepository depRep = new DepartmentRepository(false);
        OrganizationUnitRepository organRep = new OrganizationUnitRepository(false);
        ShiftRepository shiftRep = new ShiftRepository(false);
        WorkGroupRepository wrkGrpRep = new WorkGroupRepository(false);
        RoleRepository roleRepository = new RoleRepository(false);
        PrecardRepository precardRep = new PrecardRepository(false);
        EntityRepository<ControlStation> ctlStRep = new EntityRepository<ControlStation>();
        EntityRepository<Doctor> docRep = new EntityRepository<Doctor>();
        ManagerRepository managerRep = new ManagerRepository(false);
        RuleCategoryRepository ruleCatRep = new RuleCategoryRepository();
        FlowRepository flowRep = new FlowRepository(false);
        EntityRepository<Report> reportRep = new EntityRepository<Report>();
        EntityRepository<CostCenter> costCenterRep = new EntityRepository<CostCenter>();
        UserRepository userRepository = new UserRepository(false);
        EntityRepository<Corporation> organizationRepository = new EntityRepository<Corporation>();
        EntityRepository<EmploymentType> EmploymentTypeRepository = new EntityRepository<EmploymentType>();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        BTemp bTemp = new BTemp();
        BOrganizationUnit bOrgan = new BOrganizationUnit();
        BDepartment bDep = new BDepartment();
        BReport bReport = new BReport();
        #endregion

        private bool IsSystemTechnicalAdmin
        {
            get
            {
                bool isSystemTechnicalAdmin = false;
                if (BUser.CurrentUser.Role.CustomCode != string.Empty && (RoleCustomCodeType)Enum.Parse(typeof(RoleCustomCodeType), BUser.CurrentUser.Role.CustomCode) == RoleCustomCodeType.SystemTechnicalAdmin)
                    isSystemTechnicalAdmin = true;
                return isSystemTechnicalAdmin;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAll(DataAccessParts part)
        {
            IList<DataAccessProxy> result = new List<DataAccessProxy>();
            switch (part)
            {
                case DataAccessParts.Shift:
                    result = this.GetAllShifts();
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.GetAllWorkGroups();
                    break;
                case DataAccessParts.Precard:
                    result = this.GetAllPrecards();
                    break;
                case DataAccessParts.ControlStation:
                    result = this.GetAllControlStations();
                    break;
                case DataAccessParts.Doctor:
                    result = this.GetAllDoctors();
                    break;
                case DataAccessParts.Flow:
                    result = this.GetAllFlow();
                    break;
                case DataAccessParts.Corporation:
                    result = this.GetAllCorporations();
                    break;
                case DataAccessParts.EmploymentType:
                    result = this.GetAllEmploymentTypes();
                    break;
                case DataAccessParts.CostCenter:
                    result = this.GetAllCostCenters();
                    break;
            }
            return result;
        }
public IList<DataAccessProxy> GetAll(DataAccessParts part, string SearchTerm)
        {
            IList<DataAccessProxy> result = new List<DataAccessProxy>();
            switch (part)
            {
                case DataAccessParts.Shift:
                    result = this.GetAllShifts(SearchTerm);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.GetAllWorkGroups(SearchTerm);
                    break;
                case DataAccessParts.Precard:
                    result = this.GetAllPrecards(SearchTerm);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.GetAllControlStations(SearchTerm);
                    break;
                case DataAccessParts.Doctor:
                    result = this.GetAllDoctors(SearchTerm);
                    break;
                case DataAccessParts.Flow:
                    result = this.GetAllFlow(SearchTerm);
                    break;
                case DataAccessParts.Corporation:
                    result = this.GetAllCorporations(SearchTerm);
                    break;
                case DataAccessParts.EmploymentType:
                    result = this.GetAllEmploymentTypes(SearchTerm);
                    break;
               case DataAccessParts.CostCenter:
                    result = this.GetAllCostCenters();
                    break;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAllByUserId(DataAccessParts part, decimal userId)
        {
            IList<DataAccessProxy> result = new List<DataAccessProxy>();
            switch (part)
            {
                case DataAccessParts.Shift:
                    result = this.GetAllShiftsOfUser(userId);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.GetAllWorkGroupsOfUser(userId);
                    break;
                case DataAccessParts.Precard:
                    result = this.GetAllPrecardOfUser(userId);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.GetAllControlStationsOfUser(userId);
                    break;
                case DataAccessParts.Doctor:
                    result = this.GetAllDoctorsOfUser(userId);
                    break;
                case DataAccessParts.Flow:
                    result = this.GetAllFlowOfUser(userId);
                    break;
                case DataAccessParts.Corporation:
                    result = this.GetAllCorporationsOfUser(userId);
                    break;
                case DataAccessParts.EmploymentType:
                    result = this.GetAllEmploymentTypesOfUser(userId);
                    break;
                case DataAccessParts.CostCenter:
                    result = this.GetAllCostCentersOfUser(userId);
                    break;
            }
            return result;
        }

        public IList<DataAccessProxy> GetAllByUserId(DataAccessParts part, decimal userId, string SearchTerm)
        {
            IList<DataAccessProxy> result = new List<DataAccessProxy>();
            switch (part)
            {
                case DataAccessParts.Shift:
                    result = this.GetAllShiftsOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.GetAllWorkGroupsOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.Precard:
                    result = this.GetAllPrecardOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.GetAllControlStationsOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.Doctor:
                    result = this.GetAllDoctorsOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.Flow:
                    result = this.GetAllFlowOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.Corporation:
                    result = this.GetAllCorporationsOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.EmploymentType:
                    result = this.GetAllEmploymentTypesOfUser(userId, SearchTerm);
                    break;
                case DataAccessParts.CostCenter:
                    result = this.GetAllCostCentersOfUser(userId);
                    break;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="dataAccessId"></param>
        /// <returns></returns>
        public bool DeleteAccess(DataAccessParts part, decimal dataAccessId, decimal userId)
        {
            bool result = false;
            switch (part)
            {
                case DataAccessParts.Department:
                    result = this.DeleteDepartment(dataAccessId, userId);
                    break;
                case DataAccessParts.OrganizationUnit:
                    result = this.DeleteOrganization(dataAccessId, userId);
                    break;
                case DataAccessParts.Report:
                    result = this.DeleteReport(dataAccessId, userId);
                    break;
                case DataAccessParts.Shift:
                    result = this.DeleteShift(dataAccessId);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.DeleteWorkGroup(dataAccessId);
                    break;
                case DataAccessParts.Precard:
                    result = this.DeletePrecard(dataAccessId);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.DeleteControlStation(dataAccessId);
                    break;
                case DataAccessParts.Doctor:
                    result = this.DeleteDoctor(dataAccessId);
                    break;
                case DataAccessParts.Manager:
                    result = this.DeleteManager(dataAccessId, userId);
                    break;
                case DataAccessParts.RuleGroup:
                    result = this.DeleteRuleGroup(dataAccessId, userId);
                    break;
                case DataAccessParts.Flow:
                    result = this.DeleteFlow(dataAccessId);
                    break;
                case DataAccessParts.Corporation:
                    result = this.DeleteCorporation(dataAccessId);
                    break;
                case DataAccessParts.EmploymentType:
                    result = this.DeleteEmploymentType(dataAccessId);
                    break;
                case DataAccessParts.Role:
                    result = this.DeleteRole(dataAccessId, userId);
                    break;
                case DataAccessParts.CostCenter:
                    result = this.DeleteCostCenter(dataAccessId);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InsertDataAccess(DataAccessLevelOperationType Dalot, DataAccessParts part, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            bool result = false;
            switch (part)
            {
                case DataAccessParts.Department:
                    result = this.InsertDepartment(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.OrganizationUnit:
                    result = this.InsertOrganization(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Report:
                    result = this.InsertReport(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Shift:
                    result = this.InsertShift(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.WorkGroup:
                    result = this.InsertWorkGroup(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Precard:
                    result = this.InsertPrecard(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.ControlStation:
                    result = this.InsertControlStaion(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Doctor:
                    result = this.InsertDoctor(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Manager:
                    result = this.InsertManager(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.RuleGroup:
                    result = this.InsertRuleGroup(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Flow:
                    result = this.InsertFlow(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Corporation:
                    result = this.InsertCorporation(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.EmploymentType:
                    result = this.InsertEmploymentType(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.Role:
                    result = this.InsertRole(Dalot, partId, userId, searchKey, searchTerm);
                    break;
                case DataAccessParts.CostCenter:
                    result = this.InsertCostCenter(Dalot, partId, userId, searchKey, searchTerm);
                    break;
            }
            return result;
        }


        #region Department

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetDepartmentRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                IList<Department> list = depRep.GetDepartmentTree();
                Department result = new Department();
                if (list.Count > 0)
                {
                    result = list.First();
                }

                return new DataAccessProxy() { ID = 0, Name = result.Name };

            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllDepartmentAccess(userId))
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        /// <summary>
        /// زیر بخشهای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetDepartmentChilds(decimal parentId)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                if (parentId == 0)
                {
                    parentId = new BDepartment().GetDepartmentsTree().ID;
                }
                IList<Department> list = depRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Department().Parent), new Department() { ID = parentId }));
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetDepartmentsOfUser(BUser.CurrentUser.ID, parentId);
        }
        public IList<DataAccessProxy> GetDepartmentChilds(decimal parentId, string SearchItem)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Department> depList = bDep.GetDepartmentChildsWithoutDA(SearchItem);
                var result = from o in depList
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 ParentIds = o.ParentPathList
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetDepartmentsOfUser(BUser.CurrentUser.ID, parentId, SearchItem);
        }

        /// <summary>
        /// زیربخش های قابل دسترس برای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetDepartmentsOfUser(decimal userId, decimal parentId)
        {
            try
            {
                BDepartment bDep = new BDepartment();
                IList<Department> result = new List<Department>();

                if (parentId == 0)
                {
                    EntityRepository<DADepartment> rep = new EntityRepository<DADepartment>();
                    if (userRepository.HasAllDepartmentAccess(userId))
                    {
                        Department root = bDep.GetDepartmentsTree();
                        result = bDep.GetDepartmentChildsWithoutDA(root.ID);
                    }
                    else
                    {
                        IList<DADepartment> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId));
                        var ids = from o in daList
                                  select o.Department;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DADepartment da1 in daList)
                        {
                            foreach (DADepartment da2 in daList)
                            {
                                if (da2.Department.ParentPath.Contains(String.Format(",{0},", da1.DepID.ToString())))
                                {
                                    result.Remove(da2.Department);
                                }
                            }
                        }

                        foreach (Department dep in result)
                        {
                            dep.Visible = true;
                        }
                    }
                }
                else
                {
                    result = bDep.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetDepartmentsOfUser");
                throw ex;
            }
        }
        public IList<DataAccessProxy> GetDepartmentsOfUser(decimal userId, decimal parentId, string SearchItem)
        {
            try
            {
                DADepartment daDepartmentAlias = null;
                IList<Department> DepartmentList = new List<Department>();
                IList<Department> DepList = new List<Department>();
                if (parentId == 0)
                {
                    EntityRepository<DADepartment> rep = new EntityRepository<DADepartment>();
                    if (userRepository.HasAllDepartmentAccess(userId))
                    {
                        Department root = bDep.GetDepartmentsTree();
                        DepartmentList = bDep.GetDepartmentChildsWithoutDA(SearchItem);
                    }
                    else
                    {
                        DepartmentList = depRep.GetSearchDepartmentsOfUser(userId, SearchItem);
                        foreach (Department d1 in DepartmentList)
                        {
                            foreach (Department d2 in DepartmentList)
                            {
                                if (d2.ParentPathList.Contains(d1.ID))
                                {
                                    DepList.Add(d2);
                                }
                            }
                        }
                        DepartmentList = DepartmentList.Except(DepList).ToList<Department>();
                        IList<decimal> DepartmentDirectIds = NHSession.QueryOver(() => daDepartmentAlias)
                                                                      .Where(() => daDepartmentAlias.UserID == userId)
                                                                      .Select(x => x.DepID)
                                                                      .List<decimal>();
                        foreach (Department dep in DepartmentList)
                        {
                            if (DepartmentDirectIds.Contains(dep.ID))
                                dep.Visible = true;
                        }
                    }
                }
                else
                {
                    DepartmentList = bDep.GetByID(parentId).ChildList;
                }
                var lastResult = from o in DepartmentList
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetDepartmentsOfUser");
                throw ex;
            }
        }

        public IList<DataAccessProxy> GetRoleOfUser(decimal userId, decimal parentId)
        {
            try
            {
                BRole bRole = new BRole();
                IList<Role> result = new List<Role>();

                if (parentId == 0)
                {
                    EntityRepository<DARole> rep = new EntityRepository<DARole>();
                    if (userRepository.HasAllRoleAccess(userId) > 0)
                    {
                        Role root = bRole.GetRoleTree();
                        result = bRole.GetRoleChildsWithoutDA(root.ID);
                    }
                    else
                    {
                        IList<DARole> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId));
                        var ids = from o in daList
                                  select o.Role;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DARole da1 in daList)
                        {
                            foreach (DARole da2 in daList)
                            {
                                if (da2.Role.ParentPath.Contains(String.Format(",{0},", da1.RoleID.ToString())))
                                {
                                    result.Remove(da2.Role);
                                }
                            }
                        }

                        foreach (Role dep in result)
                        {
                            dep.Visible = true;
                        }
                    }
                }
                else
                {
                    result = bRole.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetRolesOfUser");
                throw ex;
            }
        }
        public IList<DataAccessProxy> GetRoleOfUser(decimal userId, decimal parentId, string SearchItem)
        {
            try
            {
                BRole bRole = new BRole();
                IList<Role> result = new List<Role>();

                if (parentId == 0)
                {
                    EntityRepository<DARole> rep = new EntityRepository<DARole>();
                    if (userRepository.HasAllRoleAccess(userId) > 0)
                    {
                        //Role root = bRole.GetRoleTree();
                        result = bRole.GetSearchedRole(SearchItem);
                    }
                    else
                    {
                        //IList<DARole> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId));
                        //var ids = from o in daList
                        //          select o.Role;
                        //result = ids.ToList();

                        ///حذف بچه از بین والدها
                        //foreach (DARole da1 in daList)
                        //{
                        //    foreach (DARole da2 in daList)
                        //    {
                        //        if (da2.Role.ParentPath.Contains(String.Format(",{0},", da1.RoleID.ToString())))
                        //        {
                        //            result.Remove(da2.Role);
                        //        }
                        //    }
                        //}
                        result = roleRepository.GetSearchedRoleOfUser(userId, SearchItem);
                        foreach (Role dep in result)
                        {
                            dep.Visible = true;
                        }
                    }
                }
                else
                {
                    result = bRole.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetRolesOfUser");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertDepartment(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DADepartment daDep = new DADepartment();
                    EntityRepository<DADepartment> daRep = new EntityRepository<DADepartment>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DADepartment> daDepartmentList = new List<DADepartment>();
                    IList<DADepartment> userDADepartmentList = new List<DADepartment>();
                    IList<DADepartment> userRecycleDADepartmentList = new List<DADepartment>();
                    IList<decimal> dADepartmentIdList = new List<decimal>();
                    string[] daDepartmentParentPathList = null;
                    IList<decimal> daDepartmentParentPathIdList = null;
                    Department department = null;
                    if (partId == 0)//درج همه
                    {
                        IList<DADepartment> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);

                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DADepartment dADepartmentAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DADepartment>(() => dADepartmentAlias)
                                        .JoinAlias(() => dADepartmentAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DADepartment>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daDepartmentList = this.NHSession.QueryOver<DADepartment>()
                                                             .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                             .List<DADepartment>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DADepartment da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daDepartmentList.Count == 1 && daDepartmentList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daDepartmentList.Any(x => x.Department.ID == da.Department.ID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daDep = daRep.WithoutTransactSave(new DADepartment() { UserID = userID, All = true, DepID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daDepartmentList.Count == 1 && daDepartmentList[0].All)
                                    daDep = daRep.WithoutTransactSave(new DADepartment() { UserID = userID, All = true, DepID = null });
                                else
                                {
                                    userDADepartmentList = this.NHSession.QueryOver<DADepartment>()
                                                                         .Where(x => x.User.ID == userId && !x.All)
                                                                         .List<DADepartment>();
                                    dADepartmentIdList = userDADepartmentList.Select(x => x.Department.ID).ToList<decimal>();
                                    foreach (DADepartment daDepartmentItem in daDepartmentList)
                                    {
                                        userRecycleDADepartmentList = userDADepartmentList.Where(x => x.Department.ParentPath.Contains("," + daDepartmentItem.Department.ID.ToString() + ","))
                                                                                          .ToList<DADepartment>();
                                        foreach (DADepartment userRecycleDADepartmentItem in userRecycleDADepartmentList)
                                        {
                                            daRep.WithoutTransactDelete(userRecycleDADepartmentItem);
                                        }
                                        daDepartmentParentPathList = daDepartmentItem.Department.ParentPath.Split(new char[] { ',' });
                                        daDepartmentParentPathIdList = new List<decimal>();
                                        foreach (string daDepartmentParentPathItem in daDepartmentParentPathList)
                                        {
                                            if (daDepartmentParentPathItem != null && daDepartmentParentPathItem != string.Empty)
                                                daDepartmentParentPathIdList.Add(decimal.Parse(daDepartmentParentPathItem, CultureInfo.InvariantCulture));
                                        }
                                        if (!dADepartmentIdList.Any(x => daDepartmentParentPathIdList.Contains(x)))
                                            daDep = daRep.WithoutTransactSave(new DADepartment() { UserID = userID, All = false, DepID = daDepartmentItem.DepID });
                                    }
                                }
                            }
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DADepartment> daSinglePartList = null;
                        IList<DADepartment> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().DepID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().All), true));

                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                            {
                                department = new BDepartment().GetByID(partId);
                                this.NHSession.Evict(department);
                                userDADepartmentList = this.NHSession.QueryOver<DADepartment>()
                                                                     .Where(x => x.User.ID == userId && !x.All)
                                                                     .List<DADepartment>();
                                dADepartmentIdList = userDADepartmentList.Select(x => x.Department.ID).ToList<decimal>();
                                userRecycleDADepartmentList = userDADepartmentList.Where(x => x.Department.ParentPath.Contains("," + partId.ToString() + ","))
                                                                                  .ToList<DADepartment>();
                                foreach (DADepartment userRecycleDADepartmentItem in userRecycleDADepartmentList)
                                {
                                    daRep.WithoutTransactDelete(userRecycleDADepartmentItem);
                                }
                                daDepartmentParentPathList = department.ParentPath.Split(new char[] { ',' });
                                daDepartmentParentPathIdList = new List<decimal>();
                                foreach (string daDepartmentParentPathItem in daDepartmentParentPathList)
                                {
                                    if (daDepartmentParentPathItem != null && daDepartmentParentPathItem != string.Empty)
                                        daDepartmentParentPathIdList.Add(decimal.Parse(daDepartmentParentPathItem, CultureInfo.InvariantCulture));
                                }
                                if (!dADepartmentIdList.Any(x => daDepartmentParentPathIdList.Contains(x)))
                                    daDep = daRep.WithoutTransactSave(new DADepartment() { DepID = partId, UserID = userID, All = false });
                            }
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertDepartment");
                    throw ex;
                }
            }
        }

        private bool InsertRole(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DARole daDep = new DARole();
                    EntityRepository<DARole> daRep = new EntityRepository<DARole>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DARole> daRoleList = new List<DARole>();
                    IList<DARole> userDARoleList = new List<DARole>();
                    IList<DARole> userRecycleDARoleList = new List<DARole>();
                    IList<decimal> daRoleIdList = new List<decimal>();
                    string[] daRoleParentPathList = null;
                    IList<decimal> daRoleParentPathIdList = null;
                    Role role = null;
                    if (partId == 0)//درج همه
                    {
                        IList<DARole> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARole().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);

                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARole().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DARole dARoleAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DARole>(() => dARoleAlias)
                                                          .JoinAlias(() => dARoleAlias.User, () => userAlias)
                                                          .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                                          .Where(() => tempAlias.OperationGUID == operationGUID)
                                                          .List<DARole>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daRoleList = this.NHSession.QueryOver<DARole>()
                                                       .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                       .List<DARole>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DARole da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daRoleList.Count == 1 && daRoleList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daRoleList.Any(x => x.Role.ID == da.Role.ID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daDep = daRep.WithoutTransactSave(new DARole() { UserID = userID, All = true, RoleID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daRoleList.Count == 1 && daRoleList[0].All)
                                    daDep = daRep.WithoutTransactSave(new DARole() { UserID = userID, All = true, RoleID = null });
                                else
                                {
                                    userDARoleList = this.NHSession.QueryOver<DARole>()
                                                                   .Where(x => x.User.ID == userId && !x.All)
                                                                   .List<DARole>();
                                    daRoleIdList = userDARoleList.Select(x => x.Role.ID).ToList<decimal>();
                                    foreach (DARole daRoleItem in daRoleList)
                                    {
                                        userRecycleDARoleList = userDARoleList.Where(x => x.Role.ParentPath.Contains("," + daRoleItem.Role.ID.ToString() + ","))
                                                                              .ToList<DARole>();
                                        foreach (DARole userRecycleDARoleItem in userRecycleDARoleList)
                                        {
                                            daRep.WithoutTransactDelete(userRecycleDARoleItem);
                                        }
                                        daRoleParentPathList = daRoleItem.Role.ParentPath.Split(new char[] { ',' });
                                        daRoleParentPathIdList = new List<decimal>();
                                        foreach (string daRoleParentPathItem in daRoleParentPathList)
                                        {
                                            if (daRoleParentPathItem != null && daRoleParentPathItem != string.Empty)
                                                daRoleParentPathIdList.Add(decimal.Parse(daRoleParentPathItem, CultureInfo.InvariantCulture));
                                        }
                                        if (!daRoleIdList.Any(x => daRoleParentPathIdList.Contains(x)))
                                            daDep = daRep.WithoutTransactSave(new DARole() { UserID = userID, All = false, RoleID = daRoleItem.RoleID });
                                    }
                                }
                            }
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DARole> daSinglePartList = null;
                        IList<DARole> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARole().UserID), userID),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new DARole().RoleID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARole().UserID), userID),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new DARole().All), true));

                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                            {
                                role = new BRole().GetByID(partId);
                                this.NHSession.Evict(role);
                                userDARoleList = this.NHSession.QueryOver<DARole>()
                                                               .Where(x => x.User.ID == userId && !x.All)
                                                               .List<DARole>();
                                daRoleIdList = userDARoleList.Select(x => x.Role.ID).ToList<decimal>();
                                userRecycleDARoleList = userDARoleList.Where(x => x.Role.ParentPath.Contains("," + partId.ToString() + ","))
                                                                      .ToList<DARole>();
                                foreach (DARole userRecycleDARoleItem in userRecycleDARoleList)
                                {
                                    daRep.WithoutTransactDelete(userRecycleDARoleItem);
                                }
                                daRoleParentPathList = role.ParentPath.Split(new char[] { ',' });
                                daRoleParentPathIdList = new List<decimal>();
                                foreach (string daRoleParentPathItem in daRoleParentPathList)
                                {
                                    if (daRoleParentPathItem != null && daRoleParentPathItem != string.Empty)
                                        daRoleParentPathIdList.Add(decimal.Parse(daRoleParentPathItem, CultureInfo.InvariantCulture));
                                }
                                if (!daRoleIdList.Any(x => daRoleParentPathIdList.Contains(x)))
                                    daDep = daRep.WithoutTransactSave(new DARole() { RoleID = partId, UserID = userID, All = false });
                            }
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertRole");
                    throw ex;
                }
            }
        }


         
        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteDepartment(decimal departmentId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DADepartment> daRep = new EntityRepository<DADepartment>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DADepartment> daPrtList = null;
                    Department department = null;
                    Department departmentAlias = null;
                    User userAlias = null;
                    DADepartment userDADepartmentAlias = null;
                    QueryOver<DADepartment, DADepartment> dataAccessSubQuery = null;
                    IList<decimal> parentDepartmentIdsList = new List<decimal>();
                    UIValidationExceptions exceptionsList = new UIValidationExceptions();
                    if (departmentId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().All), true));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DADepartment>(() => userDADepartmentAlias)
                                                          .JoinAlias(() => userDADepartmentAlias.User, () => userAlias)
                                                          .Where(() => userDADepartmentAlias.All)
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DADepartment>(() => userDADepartmentAlias)
                                                      .JoinAlias(() => userDADepartmentAlias.User, () => userAlias)
                                                      .Where(() => userDADepartmentAlias.All &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DADepartment>();
                        }
                        if (daPrtList.Count > 0 && daPrtList.First().All)
                        {
                            foreach (DADepartment da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.DepartmentAccessDenied, "دسترسی غیر مجاز به بخش", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    else
                    {
                        department = new BDepartment().GetByID(departmentId);
                        if (department != null)
                        {
                            string[] parentPathArray = department.ParentPath.Split(new char[] { ',' });
                            foreach (string parentPath in parentPathArray)
                            {
                                if (parentPath != null && parentPath != string.Empty)
                                    parentDepartmentIdsList.Add(decimal.Parse(parentPath, CultureInfo.InvariantCulture));
                            }
                        }
                        NHSession.Evict(department);

                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DADepartment().DepID), departmentId));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DADepartment>(() => userDADepartmentAlias)
                                                          .JoinAlias(() => userDADepartmentAlias.User, () => userAlias).Left
                                                          .JoinAlias(() => userDADepartmentAlias.Department, () => departmentAlias)
                                                          .Where(() => departmentAlias.ID == departmentId || userDADepartmentAlias.All || departmentAlias.ID.IsIn(parentDepartmentIdsList.ToArray()))
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DADepartment>(() => userDADepartmentAlias)
                                                      .JoinAlias(() => userDADepartmentAlias.User, () => userAlias)
                                                      .JoinAlias(() => userDADepartmentAlias.Department, () => departmentAlias)
                                                      .Where(() => departmentAlias.ID == departmentId &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DADepartment>();
                        }
                        if (daPrtList.Count > 0)
                        {
                            foreach (DADepartment da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.DepartmentAccessDenied, "دسترسی غیر مجاز به بخش", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteDepartment");
                    throw ex;
                }
            }
        }

        private bool DeleteRole(decimal roleId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DARole> daRep = new EntityRepository<DARole>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DARole> daPrtList = null;
                    Role role = null;
                    Role roleAlias = null;
                    User userAlias = null;
                    DARole userDARoleAlias = null;
                    QueryOver<DARole, DARole> dataAccessSubQuery = null;
                    IList<decimal> parentRoleIdsList = new List<decimal>();
                    UIValidationExceptions exceptionsList = new UIValidationExceptions();
                    if (roleId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARole().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DARole().All), true));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DARole>(() => userDARoleAlias)
                                                          .JoinAlias(() => userDARoleAlias.User, () => userAlias)
                                                          .Where(() => userDARoleAlias.All)
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DARole>(() => userDARoleAlias)
                                                      .JoinAlias(() => userDARoleAlias.User, () => userAlias)
                                                      .Where(() => userDARoleAlias.All &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DARole>();
                        }
                        if (daPrtList.Count > 0 && daPrtList.First().All)
                        {
                            foreach (DARole da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.RoleAccessDenied, "دسترسی غیر مجاز به نقش", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    else
                    {
                        role = new BRole().GetByID(roleId);
                        if (role != null)
                        {
                            string[] parentPathArray = role.ParentPath.Split(new char[] { ',' });
                            foreach (string parentPath in parentPathArray)
                            {
                                if (parentPath != null && parentPath != string.Empty)
                                    parentRoleIdsList.Add(decimal.Parse(parentPath, CultureInfo.InvariantCulture));
                            }
                        }
                        NHSession.Evict(role);

                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARole().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DARole().RoleID), roleId));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DARole>(() => userDARoleAlias)
                                                          .JoinAlias(() => userDARoleAlias.User, () => userAlias).Left
                                                          .JoinAlias(() => userDARoleAlias.Role, () => roleAlias)
                                                          .Where(() => roleAlias.ID == roleId || userDARoleAlias.All || roleAlias.ID.IsIn(parentRoleIdsList.ToArray()))
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DARole>(() => userDARoleAlias)
                                                      .JoinAlias(() => userDARoleAlias.User, () => userAlias)
                                                      .JoinAlias(() => userDARoleAlias.Role, () => roleAlias)
                                                      .Where(() => roleAlias.ID == roleId &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DARole>();
                        }
                        if (daPrtList.Count > 0)
                        {
                            foreach (DARole da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.RoleAccessDenied, "دسترسی غیر مجاز به نقش", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteRole");
                    throw ex;
                }
            }
        }


        #endregion

        #region OrganizationUnit

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetOrganizationRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                IList<OrganizationUnit> list = organRep.GetOrganizationUnitTree();
                OrganizationUnit result = new OrganizationUnit();
                if (list.Count > 0)
                {
                    result = list.First();
                }
                result.ChildList = null;
                return new DataAccessProxy() { ID = 0, Name = result.Name };
            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllOrganAccess(userId))
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        /// <summary>
        /// زیر بخشهای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetOrganizationChilds(decimal parentId)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                if (parentId == 0)
                {
                    parentId = new BOrganizationUnit().GetOrganizationUnitTree().ID;
                }
                IList<OrganizationUnit> list = organRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new OrganizationUnit().Parent), new OrganizationUnit() { ID = parentId }));
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetOrganizationOfUser(BUser.CurrentUser.ID, parentId);
        }
        public IList<DataAccessProxy> GetOrganizationChilds(decimal parentId, string searchItem)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                IList<OrganizationUnit> OrgList = new List<OrganizationUnit>();
                IList<OrganizationUnit> orglist = bOrgan.GetSearchedOrganizationWithoutDA(searchItem);
                var result = from o in orglist
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 ParentIds = o.ParentPathList
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetOrganizationOfUser(BUser.CurrentUser.ID, parentId, searchItem);
        }
        /// <summary>
        /// زیربخش های قابل دسترس برای یک بخش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetOrganizationOfUser(decimal userId, decimal parentId)
        {
            try
            {
                BOrganizationUnit borgan = new BOrganizationUnit();
                IList<OrganizationUnit> result = new List<OrganizationUnit>();

                if (parentId == 0)
                {
                    EntityRepository<DAOrganizationUnit> rep = new EntityRepository<DAOrganizationUnit>();
                    if (userRepository.HasAllOrganAccess(userId))
                    {
                        OrganizationUnit root = borgan.GetOrganizationUnitTree();
                        result = borgan.GetOrganizationChildsWithoutDA(root.ID);
                    }
                    else
                    {
                        IList<DAOrganizationUnit> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId));
                        var ids = from o in daList
                                  select o.Organization;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DAOrganizationUnit da1 in daList)
                        {
                            foreach (DAOrganizationUnit da2 in daList)
                            {
                                if (da2.Organization.ParentPath.Contains(String.Format(",{0},", da1.OrgUnitID.ToString())))
                                {
                                    result.Remove(da2.Organization);
                                }
                            }
                        }

                        foreach (OrganizationUnit organ in result)
                        {
                            organ.Visible = true;
                        }
                    }
                }
                else
                {
                    result = borgan.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(new Exception(userId.ToString() + ":user -- parent:" + parentId.ToString()), "BDataAccess", "GetOrganizationOfUser");
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetOrganizationOfUser");
                throw ex;
            }
        }
        public IList<DataAccessProxy> GetOrganizationOfUser(decimal userId, decimal parentId, string searchItem)
        {
            try
            {
                IList<OrganizationUnit> orglist = new List<OrganizationUnit>();
                IList<OrganizationUnit> OrgList = new List<OrganizationUnit>();
                DAOrganizationUnit daOrgAlias = null;

                if (parentId == 0)
                {
                    EntityRepository<DAOrganizationUnit> rep = new EntityRepository<DAOrganizationUnit>();
                    if (userRepository.HasAllOrganAccess(userId))
                    {
                        orglist = bOrgan.GetSearchedOrganizationWithoutDA(searchItem);
                    }
                    else
                    {
                        orglist = organRep.GetSearchOrganizationOfUser(userId, searchItem);

                        foreach (OrganizationUnit o1 in orglist)
                        {
                            foreach (OrganizationUnit o2 in orglist)
                            {
                                if (o2.ParentPathList.Contains(o1.ID))
                                {
                                    OrgList.Add(o2);
                                }
                            }
                        }
                        orglist = orglist.Except(OrgList).ToList<OrganizationUnit>();
                        IList<decimal> OrganDirectIds = NHSession.QueryOver(() => daOrgAlias)
                                                                      .Where(() => daOrgAlias.UserID == userId)
                                                                      .Select(x => x.OrgUnitID)
                                                                      .List<decimal>();
                        foreach (OrganizationUnit org in orglist)
                        {
                            if (OrganDirectIds.Contains(org.ID))
                                org.Visible = true;
                        }
                    }
                }
                else
                {
                    orglist = bOrgan.GetByID(parentId).ChildList;
                }
                var lastResult = from o in orglist
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                //BaseBusiness<Entity>.LogException(new Exception(userId.ToString() + ":user -- parent:" + parentId.ToString()), "BDataAccess", "GetOrganizationOfUser");
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetOrganizationOfUser");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertOrganization(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DAOrganizationUnit daorgan = new DAOrganizationUnit();
                    EntityRepository<DAOrganizationUnit> daRep = new EntityRepository<DAOrganizationUnit>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAOrganizationUnit> daOrganizationUnitList = new List<DAOrganizationUnit>();
                    IList<DAOrganizationUnit> userDAOrganizationUnitList = new List<DAOrganizationUnit>();
                    IList<DAOrganizationUnit> userRecycleDAOrganizationUnitList = new List<DAOrganizationUnit>();
                    IList<decimal> dAOrganizationUnitIdList = new List<decimal>();
                    string[] daOrganizationUnitParentPathList = null;
                    IList<decimal> daOrganizationUnitParentPathIdList = null;
                    OrganizationUnit organizationUnit = null;
                    if (partId == 0)//درج همه
                    {
                        IList<DAOrganizationUnit> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAOrganizationUnit dAOrganizationUnitAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAOrganizationUnit>(() => dAOrganizationUnitAlias)
                                        .JoinAlias(() => dAOrganizationUnitAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAOrganizationUnit>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daOrganizationUnitList = this.NHSession.QueryOver<DAOrganizationUnit>()
                                                                   .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                                   .List<DAOrganizationUnit>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAOrganizationUnit da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daOrganizationUnitList.Count == 1 && daOrganizationUnitList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daOrganizationUnitList.Any(x => x.Organization.ID == da.Organization.ID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daorgan = daRep.WithoutTransactSave(new DAOrganizationUnit() { UserID = userID, All = true, OrgUnitID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daOrganizationUnitList.Count == 1 && daOrganizationUnitList[0].All)
                                    daorgan = daRep.WithoutTransactSave(new DAOrganizationUnit() { UserID = userID, All = true, OrgUnitID = null });
                                else
                                {
                                    userDAOrganizationUnitList = this.NHSession.QueryOver<DAOrganizationUnit>()
                                                                               .Where(x => x.User.ID == userId && !x.All)
                                                                               .List<DAOrganizationUnit>();
                                    dAOrganizationUnitIdList = userDAOrganizationUnitList.Select(x => x.Organization.ID).ToList<decimal>();
                                    foreach (DAOrganizationUnit daOrganizationUnitItem in daOrganizationUnitList)
                                    {
                                        userRecycleDAOrganizationUnitList = userDAOrganizationUnitList.Where(x => x.Organization.ParentPath.Contains("," + daOrganizationUnitItem.Organization.ID.ToString() + ","))
                                                                                                      .ToList<DAOrganizationUnit>();
                                        foreach (DAOrganizationUnit userRecycleDAOrganizationUnitItem in userRecycleDAOrganizationUnitList)
                                        {
                                            daRep.WithoutTransactDelete(userRecycleDAOrganizationUnitItem);
                                        }
                                        daOrganizationUnitParentPathList = daOrganizationUnitItem.Organization.ParentPath.Split(new char[] { ',' });
                                        daOrganizationUnitParentPathIdList = new List<decimal>();
                                        foreach (string daOrganizationUnitParentPathItem in daOrganizationUnitParentPathList)
                                        {
                                            if (daOrganizationUnitParentPathItem != null && daOrganizationUnitParentPathItem != string.Empty)
                                                daOrganizationUnitParentPathIdList.Add(decimal.Parse(daOrganizationUnitParentPathItem, CultureInfo.InvariantCulture));
                                        }
                                        if (!dAOrganizationUnitIdList.Any(x => daOrganizationUnitParentPathIdList.Contains(x)))
                                            daorgan = daRep.WithoutTransactSave(new DAOrganizationUnit() { UserID = userID, All = false, OrgUnitID = daOrganizationUnitItem.OrgUnitID });
                                    }
                                }
                            }
                        }

                        succes = true;
                    }
                    else
                    {
                        IList<DAOrganizationUnit> daSinglePartList = null;
                        IList<DAOrganizationUnit> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userID),
                                                                                                                    new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().OrgUnitID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userID),
                                                                                                                  new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                            {
                                organizationUnit = new BOrganizationUnit().GetByID(partId);
                                this.NHSession.Evict(organizationUnit);
                                userDAOrganizationUnitList = this.NHSession.QueryOver<DAOrganizationUnit>()
                                                                           .Where(x => x.User.ID == userId && !x.All)
                                                                           .List<DAOrganizationUnit>();
                                dAOrganizationUnitIdList = userDAOrganizationUnitList.Select(x => x.Organization.ID).ToList<decimal>();
                                userRecycleDAOrganizationUnitList = userDAOrganizationUnitList.Where(x => x.Organization.ParentPath.Contains("," + partId.ToString() + ","))
                                                                                              .ToList<DAOrganizationUnit>();
                                foreach (DAOrganizationUnit userRecycleDAOrganizationUnitItem in userRecycleDAOrganizationUnitList)
                                {
                                    daRep.WithoutTransactDelete(userRecycleDAOrganizationUnitItem);
                                }
                                daOrganizationUnitParentPathList = organizationUnit.ParentPath.Split(new char[] { ',' });
                                daOrganizationUnitParentPathIdList = new List<decimal>();
                                foreach (string daOrganizationUnitParentPathItem in daOrganizationUnitParentPathList)
                                {
                                    if (daOrganizationUnitParentPathItem != null && daOrganizationUnitParentPathItem != string.Empty)
                                        daOrganizationUnitParentPathIdList.Add(decimal.Parse(daOrganizationUnitParentPathItem, CultureInfo.InvariantCulture));
                                }
                                if (!dAOrganizationUnitIdList.Any(x => daOrganizationUnitParentPathIdList.Contains(x)))
                                    daorgan = daRep.WithoutTransactSave(new DAOrganizationUnit() { OrgUnitID = partId, UserID = userID, All = false });
                            }
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertOrganization");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteOrganization(decimal organizationUnitID, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DAOrganizationUnit> daRep = new EntityRepository<DAOrganizationUnit>(false);
                    IList<DAOrganizationUnit> daPrtList = null;
                    OrganizationUnit organizationUnit = null;
                    OrganizationUnit organizationUnitAlias = null;
                    User userAlias = null;
                    DAOrganizationUnit userDAOrganizationUnitAlias = null;
                    QueryOver<DAOrganizationUnit, DAOrganizationUnit> dataAccessSubQuery = null;
                    IList<decimal> parentOrganizationUnitIdsList = new List<decimal>();
                    UIValidationExceptions exceptionsList = new UIValidationExceptions();

                    if (organizationUnitID == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().All), true));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DAOrganizationUnit>(() => userDAOrganizationUnitAlias)
                                                          .JoinAlias(() => userDAOrganizationUnitAlias.User, () => userAlias)
                                                          .Where(() => userDAOrganizationUnitAlias.All)
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DAOrganizationUnit>(() => userDAOrganizationUnitAlias)
                                                      .JoinAlias(() => userDAOrganizationUnitAlias.User, () => userAlias)
                                                      .Where(() => userDAOrganizationUnitAlias.All &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DAOrganizationUnit>();
                        }
                        if (daPrtList.Count > 0 && daPrtList.First().All)
                        {
                            daRep.WithoutTransactDelete(daPrtList.First());
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitAccessDenied, "دسترسی غیر مجاز به پست سازمانی", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    else
                    {
                        organizationUnit = new BOrganizationUnit().GetByID(organizationUnitID);
                        if (organizationUnit != null)
                        {
                            string[] parentPathArray = organizationUnit.ParentPath.Split(new char[] { ',' });
                            foreach (string parentPath in parentPathArray)
                            {
                                if (parentPath != null && parentPath != string.Empty)
                                    parentOrganizationUnitIdsList.Add(decimal.Parse(parentPath, CultureInfo.InvariantCulture));
                            }
                        }
                        NHSession.Evict(organizationUnit);

                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DAOrganizationUnit().OrgUnitID), organizationUnitID));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DAOrganizationUnit>(() => userDAOrganizationUnitAlias)
                                                          .JoinAlias(() => userDAOrganizationUnitAlias.User, () => userAlias).Left
                                                          .JoinAlias(() => userDAOrganizationUnitAlias.Organization, () => organizationUnitAlias)
                                                          .Where(() => organizationUnitAlias.ID == organizationUnitID || userDAOrganizationUnitAlias.All || organizationUnitAlias.ID.IsIn(parentOrganizationUnitIdsList.ToArray()))
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DAOrganizationUnit>(() => userDAOrganizationUnitAlias)
                                                      .JoinAlias(() => userDAOrganizationUnitAlias.User, () => userAlias)
                                                      .JoinAlias(() => userDAOrganizationUnitAlias.Organization, () => organizationUnitAlias)
                                                      .Where(() => organizationUnitAlias.ID == organizationUnitID &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DAOrganizationUnit>();
                        }
                        if (daPrtList.Count > 0)
                        {
                            foreach (DAOrganizationUnit da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.OrganizationUnitAccessDenied, "دسترسی غیر مجاز به پست سازمانی", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteOrganization");
                    throw ex;
                }
            }
        }


        #endregion

        #region Shift

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllShifts()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAShift> daShiftList = new List<DAShift>();
            IList<Shift> shiftList = new List<Shift>();
            QueryOver<DAShift, DAShift> dataAccessSubQuery = null;
            Shift shiftAlias = null;
            DAShift daShiftAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Shift> list = shiftRep.GetAll();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.CustomCode,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAShift>(() => daShiftAlias)
                                              .Where(() => daShiftAlias.ShiftID == shiftAlias.ID || daShiftAlias.All)
                                              .And(() => daShiftAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                shiftList = this.NHSession.QueryOver<Shift>(() => shiftAlias)
                                          .WithSubquery
                                          .WhereExists(dataAccessSubQuery)
                                          .List<Shift>();
                daPrtList = shiftList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }

        private IList<DataAccessProxy> GetAllShifts(string SearchTerm)
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAShift> daShiftList = new List<DAShift>();
            IList<Shift> shiftList = new List<Shift>();
            QueryOver<DAShift, DAShift> dataAccessSubQuery = null;
            Shift shiftAlias = null;
            DAShift daShiftAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Shift> list = shiftRep.GetAllShift(SearchTerm);
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.CustomCode,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAShift>(() => daShiftAlias)
                                              .Where(() => daShiftAlias.ShiftID == shiftAlias.ID || daShiftAlias.All)
                                              .And(() => daShiftAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                shiftList = this.NHSession.QueryOver<Shift>(() => shiftAlias)
                                          .Where(() => shiftAlias.Name.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere) ||
                                                       shiftAlias.CustomCode.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere)
                                                )
                                          .WithSubquery
                                          .WhereExists(dataAccessSubQuery)
                                          .List<Shift>();
                daPrtList = shiftList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllShiftsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllShiftAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Shift> list = userRepository.GetUserShiftList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }

        private IList<DataAccessProxy> GetAllShiftsOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllShiftAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Shift> list = userRepository.GetUserShiftList(userId, SearchTerm);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }
        /// <summary>
        /// دادن سطح دسترسی یک شیفت به یک کاربر
        /// اگر شناسه شیفت برابر صفر بود معانیش این است که دسترسی [همه] داده شدود
        /// </summary>
        /// <param name="shiftId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertShift(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAShift daShift = new DAShift();
                    EntityRepository<DAShift> shiftDARep = new EntityRepository<DAShift>();
                    EntityRepository<DAShift> daRep = new EntityRepository<DAShift>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAShift> daShiftList = new List<DAShift>();
                    if (partId == 0)
                    {
                        IList<DAShift> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAShift dAShiftGroup = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAShift>(() => dAShiftGroup)
                                        .JoinAlias(() => dAShiftGroup.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAShift>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daShiftList = this.NHSession.QueryOver<DAShift>()
                                                        .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                        .List<DAShift>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAShift da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daShiftList.Count == 1 && daShiftList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daShiftList.Any(x => x.ShiftID == da.ShiftID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daShift = daRep.WithoutTransactSave(new DAShift() { UserID = userID, All = true, ShiftID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daShiftList.Count == 1 && daShiftList[0].All)
                                    daShift = daRep.WithoutTransactSave(new DAShift() { UserID = userID, All = true, ShiftID = null });
                                else
                                {
                                    foreach (DAShift daShiftItem in daShiftList)
                                    {
                                        daShift = daRep.WithoutTransactSave(new DAShift() { UserID = userID, All = false, ShiftID = daShiftItem.ShiftID });
                                    }
                                }
                            }
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAShift> daSinglePartList = null;
                        IList<DAShift> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().ShiftID), partId));
                            daAllPartsList = shiftDARep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAShift().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daShift = daRep.WithoutTransactSave(new DAShift() { ShiftID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertShift");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataAccessId"></param>
        /// <returns></returns>
        private bool DeleteShift(decimal dataAccessId)
        {
            bool success = false;
            DAShift daShift = null;
            EntityRepository<DAShift> daRep = new EntityRepository<DAShift>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DAShift() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DAShift> daPrtList = this.NHSession.QueryOver<DAShift>()
                                                                 .Where(x => x.ID == dataAccessId)
                                                                 .List<DAShift>();
                        if (daPrtList.Count > 0)
                        {
                            daShift = daPrtList.First();
                            IList<DAShift> daCurrentUserPrtList = this.NHSession.QueryOver<DAShift>()
                                                                                .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                            (x.ShiftID == daShift.ShiftID || x.All)
                                                                                      )
                                                                                .List<DAShift>();
                            this.NHSession.Evict(daShift);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DAShift() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.ShiftAccessDenied, "دسترسی غیر مجاز به شیفت", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteShift");
                    throw ex;
                }
            }
        }

        
        #endregion

        #region WorkGroup

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllWorkGroups()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAWorkGroup> daWorkGroupList = new List<DAWorkGroup>();
            IList<WorkGroup> workGroupList = new List<WorkGroup>();
            QueryOver<DAWorkGroup, DAWorkGroup> dataAccessSubQuery = null;
            WorkGroup workGroupAlias = null;
            DAWorkGroup daWorkGroupAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<WorkGroup> list = wrkGrpRep.GetAll();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.CustomCode,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAWorkGroup>(() => daWorkGroupAlias)
                                             .Where(() => daWorkGroupAlias.WorkGrpID == workGroupAlias.ID || daWorkGroupAlias.All)
                                             .And(() => daWorkGroupAlias.UserID == BUser.CurrentUser.ID)
                                             .Select(x => x.ID);
                workGroupList = this.NHSession.QueryOver<WorkGroup>(() => workGroupAlias)
                                              .WithSubquery
                                              .WhereExists(dataAccessSubQuery)
                                              .List<WorkGroup>();
                daPrtList = workGroupList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        private IList<DataAccessProxy> GetAllWorkGroups(string SearchTerm)
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAWorkGroup> daWorkGroupList = new List<DAWorkGroup>();
            IList<WorkGroup> workGroupList = new List<WorkGroup>();
            QueryOver<DAWorkGroup, DAWorkGroup> dataAccessSubQuery = null;
            WorkGroup workGroupAlias = null;
            DAWorkGroup daWorkGroupAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<WorkGroup> list = wrkGrpRep.GetAllWorkGroup(SearchTerm);
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.CustomCode,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAWorkGroup>(() => daWorkGroupAlias)
                                             .Where(() => daWorkGroupAlias.WorkGrpID == workGroupAlias.ID || daWorkGroupAlias.All)
                                             .And(() => daWorkGroupAlias.UserID == BUser.CurrentUser.ID)
                                             .Select(x => x.ID);
                workGroupList = this.NHSession.QueryOver<WorkGroup>(() => workGroupAlias)
                                               .Where(() => workGroupAlias.Name.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere) ||
                                                            workGroupAlias.CustomCode.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere)
                                                     )
                                              .WithSubquery
                                              .WhereExists(dataAccessSubQuery)
                                              .List<WorkGroup>();
                daPrtList = workGroupList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllWorkGroupsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllWorkGroupAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<WorkGroup> list = userRepository.GetUserWorkGroupList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllWorkGroupsOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllWorkGroupAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<WorkGroup> list = userRepository.GetUserWorkGroupList(userId, SearchTerm);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }

        private bool InsertWorkGroup(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAWorkGroup daWorkGroup = new DAWorkGroup();
                    DAWorkGroup daObject = new DAWorkGroup();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DAWorkGroup> daRep = new EntityRepository<DAWorkGroup>(false);
                    IList<DAWorkGroup> daWorkGroupList = new List<DAWorkGroup>();
                    if (partId == 0)
                    {
                        IList<DAWorkGroup> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAWorkGroup DAWorkGroupGroup = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAWorkGroup>(() => DAWorkGroupGroup)
                                        .JoinAlias(() => DAWorkGroupGroup.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAWorkGroup>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daWorkGroupList = this.NHSession.QueryOver<DAWorkGroup>()
                                                            .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                            .List<DAWorkGroup>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAWorkGroup da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daWorkGroupList.Count == 1 && daWorkGroupList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daWorkGroupList.Any(x => x.WorkGrpID == da.WorkGrpID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daObject = daRep.WithoutTransactSave(new DAWorkGroup() { UserID = userID, All = true, WorkGrpID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daWorkGroupList.Count == 1 && daWorkGroupList[0].All)
                                    daWorkGroup = daRep.WithoutTransactSave(new DAWorkGroup() { UserID = userID, All = true, WorkGrpID = null });
                                else
                                {
                                    foreach (DAWorkGroup daWorkGroupItem in daWorkGroupList)
                                    {
                                        daWorkGroup = daRep.WithoutTransactSave(new DAWorkGroup() { UserID = userID, All = false, WorkGrpID = daWorkGroupItem.WorkGrpID });
                                    }
                                }
                            }
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAWorkGroup> daSinglePartList = null;
                        IList<DAWorkGroup> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().UserID), userID),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().WorkGrpID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().UserID), userID),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new DAWorkGroup().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daObject = daRep.WithoutTransactSave(new DAWorkGroup() { WorkGrpID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertWorkGroup");
                    throw ex;
                }
            }
        }

        private bool DeleteWorkGroup(decimal dataAccessId)
        {
            bool success = false;
            DAWorkGroup daWorkGroup = null;
            EntityRepository<DAWorkGroup> daRep = new EntityRepository<DAWorkGroup>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DAWorkGroup() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DAWorkGroup> daPrtList = this.NHSession.QueryOver<DAWorkGroup>()
                                                                     .Where(x => x.ID == dataAccessId)
                                                                     .List<DAWorkGroup>();
                        if (daPrtList.Count > 0)
                        {
                            daWorkGroup = daPrtList.First();
                            IList<DAWorkGroup> daCurrentUserPrtList = this.NHSession.QueryOver<DAWorkGroup>()
                                                                                    .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                               (x.WorkGrpID == daWorkGroup.WorkGrpID || x.All)
                                                                                      )
                                                                                    .List<DAWorkGroup>();
                            this.NHSession.Evict(daWorkGroup);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DAWorkGroup() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.WorkGroupAccessDenied, "دسترسی غیر مجاز به گروه کاری", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteWorkGroup");
                    throw ex;
                }
            }
        }
        #endregion

        #region Precard

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllPrecards()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAPrecard> daPrecardList = new List<DAPrecard>();
            IList<Precard> precardList = new List<Precard>();
            QueryOver<DAPrecard, DAPrecard> dataAccessSubQuery = null;
            Precard precardAlias = null;
            DAPrecard daPrecardAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Precard> list = precardRep.GetAll();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.Code,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAPrecard>(() => daPrecardAlias)
                                              .Where(() => daPrecardAlias.PreCardID == precardAlias.ID || daPrecardAlias.All)
                                              .And(() => daPrecardAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                precardList = this.NHSession.QueryOver<Precard>(() => precardAlias)
                                            .WithSubquery
                                            .WhereExists(dataAccessSubQuery)
                                            .List<Precard>();
                daPrtList = precardList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.Code,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        private IList<DataAccessProxy> GetAllPrecards(string SearchTerm)
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAPrecard> daPrecardList = new List<DAPrecard>();
            IList<Precard> precardList = new List<Precard>();
            QueryOver<DAPrecard, DAPrecard> dataAccessSubQuery = null;
            Precard precardAlias = null;
            DAPrecard daPrecardAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Precard> list = precardRep.GetAllPrecard(SearchTerm);
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.Code,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAPrecard>(() => daPrecardAlias)
                                              .Where(() => daPrecardAlias.PreCardID == precardAlias.ID || daPrecardAlias.All)
                                              .And(() => daPrecardAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                precardList = this.NHSession.QueryOver<Precard>(() => precardAlias)
                                            .Where(() => precardAlias.Name.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere) || precardAlias.Code.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere))
                                            .WithSubquery
                                            .WhereExists(dataAccessSubQuery)
                                            .List<Precard>();
                daPrtList = precardList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.Code,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllPrecardOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllPrecardAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Precard> list = userRepository.GetUserPrecardList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.Code };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllPrecardOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllPrecardAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Precard> list = userRepository.GetUserPrecardList(userId, SearchTerm);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.Code };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }

        private bool InsertPrecard(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAPrecard daPrecard = new DAPrecard();
                    EntityRepository<DAPrecard> daRep = new EntityRepository<DAPrecard>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAPrecard> daPrecardList = new List<DAPrecard>();
                    if (partId == 0)
                    {
                        IList<DAPrecard> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAPrecard dAPrecardAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAPrecard>(() => dAPrecardAlias)
                                        .JoinAlias(() => dAPrecardAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAPrecard>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daPrecardList = this.NHSession.QueryOver<DAPrecard>()
                                                          .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                          .List<DAPrecard>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAPrecard da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daPrecardList.Count == 1 && daPrecardList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daPrecardList.Any(x => x.PreCardID == da.PreCardID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daPrecard = daRep.WithoutTransactSave(new DAPrecard() { UserID = userID, All = true, PreCardID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daPrecardList.Count == 1 && daPrecardList[0].All)
                                    daPrecard = daRep.WithoutTransactSave(new DAPrecard() { UserID = userID, All = true, PreCardID = null });
                                else
                                {
                                    foreach (DAPrecard daPrecardItem in daPrecardList)
                                    {
                                        daPrecard = daRep.WithoutTransactSave(new DAPrecard() { UserID = userID, All = false, PreCardID = daPrecardItem.PreCardID });
                                    }
                                }
                            }
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAPrecard> daSinglePartList = null;
                        IList<DAPrecard> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().PreCardID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAPrecard().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daPrecard = daRep.WithoutTransactSave(new DAPrecard() { PreCardID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertPrecard");
                    throw ex;
                }
            }
        }

        private bool DeletePrecard(decimal dataAccessId)
        {
            bool success = false;
            DAPrecard daPrecard = null;
            EntityRepository<DAPrecard> daRep = new EntityRepository<DAPrecard>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DAPrecard() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DAPrecard> daPrtList = this.NHSession.QueryOver<DAPrecard>()
                                                                   .Where(x => x.ID == dataAccessId)
                                                                   .List<DAPrecard>();
                        if (daPrtList.Count > 0)
                        {
                            daPrecard = daPrtList.First();
                            IList<DAPrecard> daCurrentUserPrtList = this.NHSession.QueryOver<DAPrecard>()
                                                                                .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                            (x.PreCardID == daPrecard.PreCardID || x.All)
                                                                                      )
                                                                                .List<DAPrecard>();
                            this.NHSession.Evict(daPrecard);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DAPrecard() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.PrecardAccessDenied, "دسترسی غیر مجاز به پیشکارت", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeletePrecard");
                    throw ex;
                }
            }
        }

        #endregion

        #region Control Station
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllControlStations()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DACtrlStation> daControlStationList = new List<DACtrlStation>();
            IList<ControlStation> controlStationList = new List<ControlStation>();
            QueryOver<DACtrlStation, DACtrlStation> dataAccessSubQuery = null;
            ControlStation controlStationAlias = null;
            DACtrlStation daControlStationAlias = null;

            if (this.IsSystemTechnicalAdmin)
            {
                IList<ControlStation> list = ctlStRep.GetAll();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.CustomCode,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DACtrlStation>(() => daControlStationAlias)
                                              .Where(() => daControlStationAlias.CtrlStationID == controlStationAlias.ID || daControlStationAlias.All)
                                              .And(() => daControlStationAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                controlStationList = this.NHSession.QueryOver<ControlStation>(() => controlStationAlias)
                                                   .WithSubquery
                                                   .WhereExists(dataAccessSubQuery)
                                                   .List<ControlStation>();
                daPrtList = controlStationList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        private IList<DataAccessProxy> GetAllControlStations(string SearchTerm)
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DACtrlStation> daControlStationList = new List<DACtrlStation>();
            IList<ControlStation> controlStationList = new List<ControlStation>();
            QueryOver<DACtrlStation, DACtrlStation> dataAccessSubQuery = null;
            ControlStation controlStationAlias = null;
            DACtrlStation daControlStationAlias = null;
            BControlStation bControlStation = new BControlStation();

            if (this.IsSystemTechnicalAdmin)
            {
                IList<ControlStation> list = bControlStation.GetAll(SearchTerm);
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = o.CustomCode,
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DACtrlStation>(() => daControlStationAlias)
                                              .Where(() => daControlStationAlias.CtrlStationID == controlStationAlias.ID || daControlStationAlias.All)
                                              .And(() => daControlStationAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                controlStationList = this.NHSession.QueryOver<ControlStation>(() => controlStationAlias)
                                                    .Where(() => controlStationAlias.Name.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere) ||
                                                                 controlStationAlias.CustomCode.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere)
                                                          )
                                                   .WithSubquery
                                                   .WhereExists(dataAccessSubQuery)
                                                   .List<ControlStation>();
                daPrtList = controlStationList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllControlStationsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllControlStationAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<ControlStation> list = userRepository.GetUserControlStationList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllControlStationsOfUser");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllControlStationsOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllControlStationAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<ControlStation> list = userRepository.GetUserControlStationList(userId, SearchTerm);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllControlStationsOfUser");
                throw ex;
            }
        }

        private bool InsertControlStaion(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DACtrlStation daControlStation = new DACtrlStation();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DACtrlStation> daRep = new EntityRepository<DACtrlStation>(false);
                    IList<DACtrlStation> daControlStationList = new List<DACtrlStation>();
                    if (partId == 0)
                    {
                        IList<DACtrlStation> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DACtrlStation dACtrlStationAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DACtrlStation>(() => dACtrlStationAlias)
                                        .JoinAlias(() => dACtrlStationAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DACtrlStation>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daControlStationList = this.NHSession.QueryOver<DACtrlStation>()
                                                                 .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                                 .List<DACtrlStation>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DACtrlStation da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daControlStationList.Count == 1 && daControlStationList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daControlStationList.Any(x => x.CtrlStationID == da.CtrlStationID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daControlStation = daRep.WithoutTransactSave(new DACtrlStation() { UserID = userID, All = true, CtrlStationID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daControlStationList.Count == 1 && daControlStationList[0].All)
                                    daControlStation = daRep.WithoutTransactSave(new DACtrlStation() { UserID = userID, All = true, CtrlStationID = null });
                                else
                                {
                                    foreach (DACtrlStation daControlStationItem in daControlStationList)
                                    {
                                        daControlStation = daRep.WithoutTransactSave(new DACtrlStation() { UserID = userID, All = false, CtrlStationID = daControlStationItem.CtrlStationID });
                                    }
                                }
                            }
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DACtrlStation> daSinglePartList = null;
                        IList<DACtrlStation> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().CtrlStationID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DACtrlStation().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daControlStation = daRep.WithoutTransactSave(new DACtrlStation() { CtrlStationID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertControlStation");
                    throw ex;
                }
            }
        }

        private bool DeleteControlStation(decimal dataAccessId)
        {
            bool success = false;
            DACtrlStation daControlStation = null;
            EntityRepository<DACtrlStation> daRep = new EntityRepository<DACtrlStation>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DACtrlStation() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DACtrlStation> daPrtList = this.NHSession.QueryOver<DACtrlStation>()
                                                                       .Where(x => x.ID == dataAccessId)
                                                                       .List<DACtrlStation>();
                        if (daPrtList.Count > 0)
                        {
                            daControlStation = daPrtList.First();
                            IList<DACtrlStation> daCurrentUserPrtList = this.NHSession.QueryOver<DACtrlStation>()
                                                                                      .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                                  (x.CtrlStationID == daControlStation.CtrlStationID || x.All)
                                                                                            )
                                                                                      .List<DACtrlStation>();
                            this.NHSession.Evict(daControlStation);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DACtrlStation() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.ControlStationAccessDenied, "دسترسی غیر مجاز به ایستگاه کنترل", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteControlStation");
                    throw ex;
                }
            }
        }
        #endregion

        #region Doctors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllDoctors()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DADoctor> daDoctorList = new List<DADoctor>();
            IList<Doctor> doctorList = new List<Doctor>();
            QueryOver<DADoctor, DADoctor> dataAccessSubQuery = null;
            Doctor doctorAlias = null;
            DADoctor daDoctorAlias = null;

            if (this.IsSystemTechnicalAdmin)
            {
                IList<Doctor> list = docRep.GetAll();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = "",
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DADoctor>(() => daDoctorAlias)
                                              .Where(() => daDoctorAlias.DoctorID == doctorAlias.ID || daDoctorAlias.All)
                                              .And(() => daDoctorAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                doctorList = this.NHSession.QueryOver<Doctor>(() => doctorAlias)
                                             .WithSubquery
                                             .WhereExists(dataAccessSubQuery)
                                             .List<Doctor>();
                daPrtList = doctorList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = "",
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        private IList<DataAccessProxy> GetAllDoctors(string SearchTerm)
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DADoctor> daDoctorList = new List<DADoctor>();
            IList<Doctor> doctorList = new List<Doctor>();
            QueryOver<DADoctor, DADoctor> dataAccessSubQuery = null;
            Doctor doctorAlias = null;
            DADoctor daDoctorAlias = null;
            BDoctor bDoctor = new BDoctor();

            if (this.IsSystemTechnicalAdmin)
            {
                IList<Doctor> list = bDoctor.GetAll(SearchTerm);
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 CustomCode = "",
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DADoctor>(() => daDoctorAlias)
                                              .Where(() => daDoctorAlias.DoctorID == doctorAlias.ID || daDoctorAlias.All)
                                              .And(() => daDoctorAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                doctorList = this.NHSession.QueryOver<Doctor>(() => doctorAlias)
                                            .Where(() => doctorAlias.FirstName.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere) ||
                                                         doctorAlias.LastName.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere)
                                                  )
                                             .WithSubquery
                                             .WhereExists(dataAccessSubQuery)
                                             .List<Doctor>();
                daPrtList = doctorList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = "",
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllDoctorsOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllDoctorAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Doctor> list = userRepository.GetUserDoctorList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllDoctorsOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllDoctorAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Doctor> list = userRepository.GetUserDoctorList(userId, SearchTerm);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllShiftOfUser");
                throw ex;
            }
        }
        private bool InsertDoctor(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DADoctor daDoctor = new DADoctor();
                    EntityRepository<DADoctor> daRep = new EntityRepository<DADoctor>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DADoctor> daDoctorList = new List<DADoctor>();
                    if (partId == 0)
                    {
                        IList<DADoctor> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DADoctor dADoctorAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DADoctor>(() => dADoctorAlias)
                                        .JoinAlias(() => dADoctorAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DADoctor>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daDoctorList = this.NHSession.QueryOver<DADoctor>()
                                                         .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                         .List<DADoctor>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DADoctor da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daDoctorList.Count == 1 && daDoctorList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daDoctorList.Any(x => x.DoctorID == da.DoctorID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daDoctor = daRep.WithoutTransactSave(new DADoctor() { UserID = userID, All = true, DoctorID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daDoctorList.Count == 1 && daDoctorList[0].All)
                                    daDoctor = daRep.WithoutTransactSave(new DADoctor() { UserID = userID, All = true, DoctorID = null });
                                else
                                {
                                    foreach (DADoctor daDoctorItem in daDoctorList)
                                    {
                                        daDoctor = daRep.WithoutTransactSave(new DADoctor() { UserID = userID, All = false, DoctorID = daDoctorItem.DoctorID });
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        IList<DADoctor> daSinglePartList = null;
                        IList<DADoctor> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().DoctorID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DADoctor().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daDoctor = daRep.WithoutTransactSave(new DADoctor() { DoctorID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertDoctor");
                    throw ex;
                }
            }
        }

        private bool DeleteDoctor(decimal dataAccessId)
        {
            bool success = false;
            DADoctor daDoctor = null;
            EntityRepository<DADoctor> daRep = new EntityRepository<DADoctor>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DADoctor() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DADoctor> daPrtList = this.NHSession.QueryOver<DADoctor>()
                                                                  .Where(x => x.ID == dataAccessId)
                                                                  .List<DADoctor>();
                        if (daPrtList.Count > 0)
                        {
                            daDoctor = daPrtList.First();
                            IList<DADoctor> daCurrentUserPrtList = this.NHSession.QueryOver<DADoctor>()
                                                                                 .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                            (x.DoctorID == daDoctor.DoctorID || x.All)
                                                                                      )
                                                                                 .List<DADoctor>();
                            this.NHSession.Evict(daDoctor);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DADoctor() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.DoctorAccessDenied, "دسترسی غیر مجاز به پزشک", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteDoctor");
                    throw ex;
                }
            }
        }

        #endregion

        #region Manager

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAllManagers(string searchKey, int pageIndex, int pageSize)
        {
            try
            {
                IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
                IList<DAManager> daManagerList = new List<DAManager>();
                IList<Manager> managerList = new List<Manager>();

                IList<Manager> list = managerRep.GetQuickSearch(searchKey, pageSize, pageIndex, this.IsSystemTechnicalAdmin, BUser.CurrentUser.ID);
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.ThePerson.Name,
                                 CustomCode = "",
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
                return daPrtList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllManagers");
                throw ex;
            }
        }

        public int GetManagerCount(string searchKey, DataAccessLevelsType Dalt)
        {
            try
            {
                int count = managerRep.GetQuickSearchCount(searchKey, Dalt, this.IsSystemTechnicalAdmin, BUser.CurrentUser.ID);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetManagerCount");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند         
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetAllManagerOfUser(decimal userId, string searchKey, int pageIndex, int pageSize)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllManagerAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = 0, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Manager> list = userRepository.GetUserManagerList(searchKey, userId, pageIndex, pageSize);
                    var l = from o in list
                            //.Where(x => x.ThePerson.Name.ToLower().Contains(searchKey) ||
                            //        (x.ThePerson != null && x.ThePerson.PersonCode.Contains(searchKey)) ||
                            //        (x.TheOrganizationUnit != null && x.TheOrganizationUnit.Name.ToLower().Contains(searchKey))
                            //        )
                            //.Skip(pageIndex * pageSize).Take(pageSize)
                            select new DataAccessProxy() { ID = o.ID, Name = o.ThePerson.Name, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllManagerOfUser");
                throw ex;
            }
        }

        public int GetManagerOfUserCount(decimal userId, string searchKey)
        {
            int result = 0;
            decimal allId = userRepository.HasAllManagerAccess(userId);

            if (allId > 0)
            {
                result = 1;
            }
            else
            {
                IList<Manager> list = userRepository.GetUserManagerList(searchKey, userId, 0, this.GetManagerCount("", DataAccessLevelsType.Target));

                result = list.Count();
            }

            return result;
        }

        private bool InsertManager(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAManager daManager = new DAManager();
                    EntityRepository<DAManager> daRep = new EntityRepository<DAManager>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAManager> daManagerList = new List<DAManager>();
                    if (partId == 0)
                    {
                        IList<DAManager> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAManager dAManagerAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAManager>(() => dAManagerAlias)
                                        .JoinAlias(() => dAManagerAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAManager>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daManagerList = this.NHSession.QueryOver<DAManager>()
                                                          .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                          .List<DAManager>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAManager da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daManagerList.Count == 1 && daManagerList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daManagerList.Any(x => x.ManagerID == da.ManagerID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daManager = daRep.WithoutTransactSave(new DAManager() { UserID = userID, All = true, ManagerID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daManagerList.Count == 1 && daManagerList[0].All)
                                    daManager = daRep.WithoutTransactSave(new DAManager() { UserID = userID, All = true, ManagerID = null });
                                else
                                {
                                    foreach (DAManager daManagerItem in daManagerList)
                                    {
                                        daManager = daRep.WithoutTransactSave(new DAManager() { UserID = userID, All = false, ManagerID = daManagerItem.ManagerID });
                                    }
                                }
                            }
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAManager> daSinglePartList = null;
                        IList<DAManager> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userID),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().ManagerID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userID),
                                                                           new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daManager = daRep.WithoutTransactSave(new DAManager() { ManagerID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertManager");
                    throw ex;
                }
            }
        }

        private bool DeleteManager(decimal mangerId, decimal userId)
        {
            bool success = false;
            DAManager daManager = null;
            IList<DAManager> daList = new List<DAManager>();
            EntityRepository<DAManager> daRep = new EntityRepository<DAManager>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        if (mangerId == 0)
                        {
                            daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userId),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().All), true));
                        }
                        else
                        {
                            daList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().UserID), userId),
                                                                             new CriteriaStruct(Utility.GetPropertyName(() => new DAManager().ManagerID), mangerId));
                        }
                        if (daList.Count > 0)
                        {
                            foreach (DAManager da in daList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }

                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DAManager> daPrtList = this.NHSession.QueryOver<DAManager>()
                                                                   .Where(x => x.UserID == userId && x.ManagerID == mangerId)
                                                                   .List<DAManager>();
                        if (daPrtList.Count > 0)
                        {
                            daManager = daPrtList.First();
                            IList<DAManager> daCurrentUserPrtList = this.NHSession.QueryOver<DAManager>()
                                                                                  .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                             (x.ManagerID == daManager.ManagerID || x.All)
                                                                                        )
                                                                                  .List<DAManager>();
                            this.NHSession.Evict(daManager);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DAManager() { ID = daManager.ID });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.ManagerAccessDenied, "دسترسی غیر مجاز به مدیر", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteManager");
                    throw ex;
                }
            }
        }

        #endregion

        #region Rule Category

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetRuleRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                RuleCategory cat = new BRuleCategory().GetRoot();
                RuleCategory result = new RuleCategory();
                result = cat;
                return new DataAccessProxy() { ID = 0, Name = result.Name };
            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllRuleGroupAccess(userId) > 0)
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        /// <summary>
        /// زیر گزارشات یک دسته گزارش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetRuleChilds(decimal parentId)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                BRuleCategory bRuleCat = new BRuleCategory();
                if (parentId == 0)
                {
                    parentId = bRuleCat.GetRoot().ID;
                }
                IList<RuleCategory> list = bRuleCat.GetByID(parentId).ChildList.Distinct(new RuleCategoryComparer()).ToList();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetRuleOfUser(BUser.CurrentUser.ID, parentId);
        }
        public IList<DataAccessProxy> GetRuleChilds(decimal parentId, string searchItem)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                BRuleCategory bRuleCat = new BRuleCategory();
                IList<RuleCategory> RuleCategoryList = bRuleCat.GetRuleCategory(searchItem);
                var result = from o in RuleCategoryList
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetRuleOfUser(BUser.CurrentUser.ID, parentId, searchItem);
        }

        public IList<DataAccessProxy> GetRoleChilds(decimal parentId)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                if (parentId == 0)
                {
                    parentId = new BRole().GetRoleTree().ID;
                }
                IList<Role> list = roleRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Role().ParentId), parentId));
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetRoleOfUser(BUser.CurrentUser.ID, parentId);
        }
        public IList<DataAccessProxy> GetRoleChilds(decimal parentId, string searchItem)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                BRole bRole = new BRole();
                IList<Role> RoleList = bRole.GetSearchedRole(searchItem);
                var result = from o in RoleList
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetRoleOfUser(BUser.CurrentUser.ID, parentId, searchItem);
        }

        /// <summary>
        /// زیرگزارشهای قابل دسترس برای یک گروه گزارش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetRuleOfUser(decimal userId, decimal parentId)
        {
            try
            {
                EntityRepository<DARuleGroup> rep = new EntityRepository<DARuleGroup>(false);
                BRuleCategory bRuleCat = new BRuleCategory();
                IList<RuleCategory> result = new List<RuleCategory>();

                if (parentId == 0)
                {
                    if (userRepository.HasAllRuleGroupAccess(userId) > 0)
                    {
                        result = this.NHSession.QueryOver<RuleCategory>()
                                               .Where(x => !x.IsRoot)
                                               .List<RuleCategory>();
                    }
                    else
                    {
                        IList<DARuleGroup> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId));
                        var ids = from o in daList
                                  select o.RuleCategory;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DARuleGroup da1 in daList)
                        {
                            foreach (DARuleGroup da2 in daList)
                            {
                                if (da1.RuleCategory.ChildList.Contains(da2.RuleCategory))
                                {
                                    result.Remove(da2.RuleCategory);
                                }
                            }
                        }

                        foreach (RuleCategory organ in result)
                        {
                            organ.Visible = true;
                        }
                    }
                }
                else
                {
                    result = bRuleCat.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible,
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetRuleOfUser");
                throw ex;
            }
        }
        public IList<DataAccessProxy> GetRuleOfUser(decimal userId, decimal parentId, string searchItem)
        {
            try
            {
                EntityRepository<DARuleGroup> rep = new EntityRepository<DARuleGroup>(false);
                BRuleCategory bRuleCat = new BRuleCategory();
                IList<RuleCategory> RuleCategoryList = new List<RuleCategory>();
                if (parentId == 0)
                {
                    if (userRepository.HasAllRuleGroupAccess(userId) > 0)
                    {
                        RuleCategoryList = bRuleCat.GetRuleCategory(searchItem);
                    }
                    else
                    {
                        RuleCategoryList = ruleCatRep.GetRuleOfUser(userId, searchItem);

                        foreach (RuleCategory organ in RuleCategoryList)
                        {
                            organ.Visible = true;
                        }
                    }
                }
                else
                {
                    RuleCategoryList = bRuleCat.GetByID(parentId).ChildList;
                }
                var lastResult = from o in RuleCategoryList
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible,
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetRuleOfUser");
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertRuleGroup(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DARuleGroup daRuleGroup = new DARuleGroup();
                    EntityRepository<DARuleGroup> daRep = new EntityRepository<DARuleGroup>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DARuleGroup> daRuleGroupList = new List<DARuleGroup>();
                    IList<DARuleGroup> userDARuleGroupList = new List<DARuleGroup>();
                    IList<DARuleGroup> userRecycleDARuleGroupList = new List<DARuleGroup>();
                    IList<decimal> dARuleGroupIdList = new List<decimal>();
                    RuleCategory ruleGroup = null;
                    if (partId == 0)//درج همه
                    {
                        IList<DARuleGroup> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DARuleGroup dARuleGroup = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DARuleGroup>(() => dARuleGroup)
                                        .JoinAlias(() => dARuleGroup.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DARuleGroup>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daRuleGroupList = this.NHSession.QueryOver<DARuleGroup>()
                                                             .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                             .List<DARuleGroup>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DARuleGroup da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daRuleGroupList.Count == 1 && daRuleGroupList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daRuleGroupList.Any(x => x.RuleCategory.ID == da.RuleCategory.ID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daRuleGroup = daRep.WithoutTransactSave(new DARuleGroup() { UserID = userID, All = true, RuleGrpID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daRuleGroupList.Count == 1 && daRuleGroupList[0].All)
                                    daRuleGroup = daRep.WithoutTransactSave(new DARuleGroup() { UserID = userID, All = true, RuleGrpID = null });
                                else
                                {
                                    userDARuleGroupList = this.NHSession.QueryOver<DARuleGroup>()
                                                                        .Where(x => x.User.ID == userId && !x.All)
                                                                        .List<DARuleGroup>();
                                    dARuleGroupIdList = userDARuleGroupList.Select(x => x.RuleCategory.ID).ToList<decimal>();
                                    foreach (DARuleGroup daRuleGroupItem in daRuleGroupList)
                                    {
                                        if (daRuleGroupList.Where(x => x.RuleCategory.IsRoot).Any())
                                            userRecycleDARuleGroupList = userDARuleGroupList.Where(x => !x.RuleCategory.IsRoot).ToList<DARuleGroup>();
                                        foreach (DARuleGroup userRecycleDARuleGroupItem in userRecycleDARuleGroupList)
                                        {
                                            daRep.WithoutTransactDelete(userRecycleDARuleGroupItem);
                                        }
                                        if (!dARuleGroupIdList.Any(x => x == daRuleGroupItem.RuleCategory.ID) && !userDARuleGroupList.Where(x => x.RuleCategory.IsRoot).Any())
                                            daRuleGroup = daRep.WithoutTransactSave(new DARuleGroup() { UserID = userID, All = false, RuleGrpID = daRuleGroupItem.RuleGrpID });
                                    }
                                }
                            }
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DARuleGroup> daSinglePartList = null;
                        IList<DARuleGroup> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().RuleGrpID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                            {
                                ruleGroup = new BRuleCategory().GetByID(partId);
                                this.NHSession.Evict(ruleGroup);
                                userDARuleGroupList = this.NHSession.QueryOver<DARuleGroup>()
                                                                    .Where(x => x.User.ID == userId && !x.All)
                                                                    .List<DARuleGroup>();
                                dARuleGroupIdList = userDARuleGroupList.Select(x => x.RuleCategory.ID).ToList<decimal>();
                                if (daRuleGroupList.Where(x => x.RuleCategory.IsRoot).Any())
                                    userRecycleDARuleGroupList = userDARuleGroupList.Where(x => !x.RuleCategory.IsRoot).ToList<DARuleGroup>();
                                foreach (DARuleGroup userRecycleDARuleGroupItem in userRecycleDARuleGroupList)
                                {
                                    daRep.WithoutTransactDelete(userRecycleDARuleGroupItem);
                                }
                                if (!dARuleGroupIdList.Any(x => x == partId) && !userDARuleGroupList.Where(x => x.RuleCategory.IsRoot).Any())
                                    daRuleGroup = daRep.WithoutTransactSave(new DARuleGroup() { UserID = userID, All = false, RuleGrpID = partId });
                            }
                        }
                    }
                    succes = true;
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertRuleGroup");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteRuleGroup(decimal ruleGroupId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DARuleGroup> daRep = new EntityRepository<DARuleGroup>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DARuleGroup> daPrtList = null;
                    RuleCategory ruleGroupAlias = null;
                    User userAlias = null;
                    DARuleGroup userDARuleGroupAlias = null;
                    QueryOver<DARuleGroup, DARuleGroup> dataAccessSubQuery = null;
                    UIValidationExceptions exceptionsList = new UIValidationExceptions();
                    if (ruleGroupId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().All), true));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DARuleGroup>(() => userDARuleGroupAlias)
                                                          .JoinAlias(() => userDARuleGroupAlias.User, () => userAlias)
                                                          .Where(() => userDARuleGroupAlias.All)
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DARuleGroup>(() => userDARuleGroupAlias)
                                                      .JoinAlias(() => userDARuleGroupAlias.User, () => userAlias)
                                                      .Where(() => userDARuleGroupAlias.All &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DARuleGroup>();
                        }
                        if (daPrtList.Count > 0 && daPrtList.First().All)
                        {
                            daRep.WithoutTransactDelete(daPrtList.First());
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.RuleGroupAccessDenied, "دسترسی غیر مجاز به گروه قانون", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    else
                    {
                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DARuleGroup().RuleGrpID), ruleGroupId));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DARuleGroup>(() => userDARuleGroupAlias)
                                                          .JoinAlias(() => userDARuleGroupAlias.User, () => userAlias).Left
                                                          .JoinAlias(() => userDARuleGroupAlias.RuleCategory, () => ruleGroupAlias)
                                                          .Where(() => ruleGroupAlias.ID == ruleGroupId || userDARuleGroupAlias.All)
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DARuleGroup>(() => userDARuleGroupAlias)
                                                      .JoinAlias(() => userDARuleGroupAlias.User, () => userAlias)
                                                      .JoinAlias(() => userDARuleGroupAlias.RuleCategory, () => ruleGroupAlias)
                                                      .Where(() => ruleGroupAlias.ID == ruleGroupId &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DARuleGroup>();
                        }
                        if (daPrtList.Count > 0)
                        {
                            foreach (DARuleGroup da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.RuleGroupAccessDenied, "دسترسی غیر مجاز به گروه قانون", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteRuleGroup");
                    throw ex;
                }
            }
        }

        #endregion

        #region Flow

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllFlow()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAFlow> daFlowList = new List<DAFlow>();
            IList<Flow> flowList = new List<Flow>();
            QueryOver<DAFlow, DAFlow> dataAccessSubQuery = null;
            Flow flowAlias = null;
            DAFlow daFlowAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Flow> list = flowRep.GetAll();
                list = list.Where(x => x.IsDeleted == false).ToList();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.FlowName,
                                 CustomCode = "",
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAFlow>(() => daFlowAlias)
                                              .Where(() => daFlowAlias.FlowID == flowAlias.ID || daFlowAlias.All)
                                              .And(() => daFlowAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                flowList = this.NHSession.QueryOver<Flow>(() => flowAlias)
                                         .WithSubquery
                                         .WhereExists(dataAccessSubQuery)
                                         .List<Flow>();
                daPrtList = flowList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.FlowName,
                    CustomCode = "",
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }

        private IList<DataAccessProxy> GetAllFlow(string SearchTerm)
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAFlow> daFlowList = new List<DAFlow>();
            IList<Flow> flowList = new List<Flow>();
            QueryOver<DAFlow, DAFlow> dataAccessSubQuery = null;
            Flow flowAlias = null;
            DAFlow daFlowAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Flow> list = flowRep.GetSearchFlow(SearchTerm);
                //  list = list.Where(x => x.IsDeleted == false).ToList();
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.FlowName,
                                 CustomCode = "",
                                 DeleteEnable = false,
                                 All = false
                             };
                daPrtList = result.ToList<DataAccessProxy>();
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAFlow>(() => daFlowAlias)
                                              .Where(() => (daFlowAlias.FlowID == flowAlias.ID || daFlowAlias.All))
                                              .And(() => daFlowAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                flowList = this.NHSession.QueryOver<Flow>(() => flowAlias)
                                         .Where(() => flowAlias.FlowName.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere))
                                         .WithSubquery
                                         .WhereExists(dataAccessSubQuery)
                                         .List<Flow>();
                daPrtList = flowList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.FlowName,
                    CustomCode = "",
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        /// <summary>
        /// لیستی از شیفتها که کاربر به آنها دسترسی دارد را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<DataAccessProxy> GetAllFlowOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllFlowAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Flow> list = userRepository.GetUserFlowList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.FlowName, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllFlowOfUser");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllFlowOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllFlowAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<Flow> list = userRepository.GetUserFlowList(userId, SearchTerm);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.FlowName, DeleteEnable = true, CustomCode = "" };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllFlowOfUser");
                throw ex;
            }
        }
        private bool InsertFlow(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DAFlow daFlow = new DAFlow();
                    EntityRepository<DAFlow> daRep = new EntityRepository<DAFlow>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAFlow> daFlowList = new List<DAFlow>();
                    if (partId == 0)
                    {
                        IList<DAFlow> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAFlow dAFlowAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAFlow>(() => dAFlowAlias)
                                        .JoinAlias(() => dAFlowAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAFlow>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daFlowList = this.NHSession.QueryOver<DAFlow>()
                                                       .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                       .List<DAFlow>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAFlow da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daFlowList.Count == 1 && daFlowList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daFlowList.Any(x => x.FlowID == da.FlowID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daFlow = daRep.WithoutTransactSave(new DAFlow() { UserID = userID, All = true, FlowID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daFlowList.Count == 1 && daFlowList[0].All)
                                    daFlow = daRep.WithoutTransactSave(new DAFlow() { UserID = userID, All = true, FlowID = null });
                                else
                                {
                                    foreach (DAFlow daFlowItem in daFlowList)
                                    {
                                        daFlow = daRep.WithoutTransactSave(new DAFlow() { UserID = userID, All = false, FlowID = daFlowItem.FlowID });
                                    }
                                }
                            }
                        }
                        success = true;
                    }
                    else
                    {
                        IList<DAFlow> daSinglePartList = null;
                        IList<DAFlow> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), userID),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().FlowID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().UserID), userID),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new DAFlow().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daFlow = daRep.WithoutTransactSave(new DAFlow() { FlowID = partId, UserID = userID, All = false });
                        }
                        success = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertFlow");
                    throw ex;
                }
            }
        }

        private bool DeleteFlow(decimal dataAccessId)
        {
            bool success = false;
            DAFlow daFlow = null;
            EntityRepository<DAFlow> daRep = new EntityRepository<DAFlow>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DAFlow() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DAFlow> daPrtList = this.NHSession.QueryOver<DAFlow>()
                                                                .Where(x => x.ID == dataAccessId)
                                                                .List<DAFlow>();
                        if (daPrtList.Count > 0)
                        {
                            daFlow = daPrtList.First();
                            IList<DAFlow> daCurrentUserPrtList = this.NHSession.QueryOver<DAFlow>()
                                                                               .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                          (x.FlowID == daFlow.FlowID || x.All)
                                                                                      )
                                                                               .List<DAFlow>();
                            this.NHSession.Evict(daFlow);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DAFlow() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.FlowAccessDenied, "دسترسی غیر مجاز به جریان کاری", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteFlow");
                    throw ex;
                }
            }
        }

        #endregion

        #region Report

        /// <summary>
        /// ریشه را برای هردو درخت برمیگرداند
        /// اگر شخص دسترسی به همه داشته باشد ریشه باید قابل حذف باشد
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataAccessProxy GetReportRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                Report list = new BReport().GetReportRoot();
                Report result = new Report();
                result = list;
                return new DataAccessProxy() { ID = 0, Name = result.Name };
            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllReportAccess(userId) > 0)
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }

        public DataAccessProxy GetRoleRoot(DataAccessLevelsType type, decimal userId)
        {
            if (type == DataAccessLevelsType.Source)
            {
                Role list = new BRole().GetRoleTree();
                Role result = new Role();
                result = list;
                return new DataAccessProxy() { ID = 0, Name = result.Name };
            }
            else
            {
                DataAccessProxy proxy = new DataAccessProxy();

                if (userRepository.HasAllRoleAccess(userId) > 0)
                {
                    proxy.DeleteEnable = true;
                }
                return proxy;
            }
        }


        /// <summary>
        /// زیر گزارشات یک دسته گزارش را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetReportChilds(decimal parentId)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                if (parentId == 0)
                {
                    parentId = new BReport().GetReportRoot().ID;
                }
                IList<Report> list = reportRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), parentId),
                                                             new CriteriaStruct(Utility.GetPropertyName(() => new Report().SubSystemId), SubSystemIdentifier.TimeAtendance)
                                                            );
                var result = from o in list
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 IsReport = o.IsReport
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetReportOfUser(BUser.CurrentUser.ID, parentId);
        }
        public IList<DataAccessProxy> GetReportChilds(decimal parentId, string searchItem)
        {
            if (this.IsSystemTechnicalAdmin)
            {
                IList<Report> ReportList = new List<Report>();
                IList<Report> reportList = bReport.GetReportChildsWidoutDA(searchItem);
                var result = from o in reportList
                             select new DataAccessProxy()
                             {
                                 ID = o.ID,
                                 Name = o.Name,
                                 IsReport = o.IsReport,
                                 ParentIds = o.ParentPathList
                             };
                return result.ToList<DataAccessProxy>();
            }
            else
                return this.GetReportOfUser(BUser.CurrentUser.ID, parentId, searchItem);
        }
        /// <summary>
        /// زیرگزارشهای قابل دسترس برای یک گروه گزارش را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<DataAccessProxy> GetReportOfUser(decimal userId, decimal parentId)
        {
            try
            {
                BReport breport = new BReport();
                IList<Report> result = new List<Report>();

                if (parentId == 0)
                {
                    EntityRepository<DAReport> rep = new EntityRepository<DAReport>();
                    if (userRepository.HasAllReportAccess(userId) > 0)
                    {
                        Report root = breport.GetReportRoot();
                        result = breport.GetReportChildsWidoutDA(root.ID);
                    }
                    else
                    {
                        IList<DAReport> daList = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId));
                        var ids = from o in daList
                                  select o.Report;
                        result = ids.ToList();

                        ///حذف بچه از بین والدها
                        foreach (DAReport da1 in daList)
                        {
                            foreach (DAReport da2 in daList)
                            {
                                if (da1.Report.ChildList.Contains(da2.Report))
                                {
                                    result.Remove(da2.Report);
                                }
                            }
                        }

                        foreach (Report organ in result)
                        {
                            organ.Visible = true;
                        }
                    }
                }
                else
                {
                    result = breport.GetByID(parentId).ChildList;
                }
                var lastResult = from o in result
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible,
                                     IsReport = o.IsReport
                                 };
                return lastResult.ToList<DataAccessProxy>();
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetReportOfUser");
                throw ex;
            }
        }
        public IList<DataAccessProxy> GetReportOfUser(decimal userId, decimal parentId, string searchItem)
        {
            try
            {
                DAReport daReportAlias = null;
                IList<Report> ReportList = new List<Report>();
                IList<Report> RepList = new List<Report>();
                if (parentId == 0)
                {
                    if (userRepository.HasAllReportAccess(userId) > 0)
                    {
                        Report root = bReport.GetReportRoot();
                        ReportList = bReport.GetReportChildsWidoutDA(searchItem);
                    }
                    else
                    {
                        ReportList = bReport.GetSearchReportOfUser(userId, searchItem);
                        foreach (Report r1 in ReportList)
                        {
                            foreach (Report r2 in ReportList)
                            {
                                if (r2.ParentPathList.Contains(r1.ID))
                                {
                                    RepList.Add(r2);
                                }
                            }
                        }
                        ReportList = ReportList.Except(RepList).ToList<Report>();
                        IList<decimal> DepartmentDirectIds = NHSession.QueryOver(() => daReportAlias)
                                                                      .Where(() => daReportAlias.UserID == userId)
                                                                      .Select(x => x.ReportID)
                                                                      .List<decimal>();
                        foreach (Report Rep in ReportList)
                        {
                            if (DepartmentDirectIds.Contains(Rep.ID))
                                Rep.Visible = true;
                        }

                    }
                }
                else
                {
                    ReportList = bReport.GetByID(parentId).ChildList;
                }
                var lastResult = from o in ReportList
                                 select new DataAccessProxy()
                                 {
                                     ID = o.ID,
                                     Name = o.Name,
                                     DeleteEnable = o.Visible,
                                     IsReport = o.IsReport
                                 };
                return lastResult.ToList<DataAccessProxy>();

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetReportOfUser");
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool InsertReport(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DAReport daReport = new DAReport();
                    EntityRepository<DAReport> daRep = new EntityRepository<DAReport>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAReport> daReportList = new List<DAReport>();
                    IList<DAReport> userDAReportList = new List<DAReport>();
                    IList<DAReport> userRecycleDAReportList = new List<DAReport>();
                    IList<decimal> dAReportIdList = new List<decimal>();
                    string[] daReportParentPathList = null;
                    IList<decimal> daReportParentPathIdList = null;
                    Report report = null;
                    if (partId == 0)//درج همه
                    {
                        IList<DAReport> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAReport dAReportAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAReport>(() => dAReportAlias)
                                        .JoinAlias(() => dAReportAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAReport>();
                                    bTemp.DeleteTempList(operationGUID);
                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daReportList = this.NHSession.QueryOver<DAReport>()
                                                         .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                         .List<DAReport>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAReport da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daReportList.Count == 1 && daReportList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daReportList.Any(x => x.Report.ID == da.Report.ID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daReport = daRep.WithoutTransactSave(new DAReport() { UserID = userID, All = true, ReportID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daReportList.Count == 1 && daReportList[0].All)
                                    daReport = daRep.WithoutTransactSave(new DAReport() { UserID = userID, All = true, ReportID = null });
                                else
                                {
                                    userDAReportList = this.NHSession.QueryOver<DAReport>()
                                                                     .Where(x => x.User.ID == userId && !x.All)
                                                                     .List<DAReport>();
                                    dAReportIdList = userDAReportList.Select(x => x.Report.ID).ToList<decimal>();
                                    foreach (DAReport daReportItem in daReportList)
                                    {
                                        userRecycleDAReportList = userDAReportList.Where(x => x.Report.ParentPath.Contains("," + daReportItem.Report.ID.ToString() + ","))
                                                                                  .ToList<DAReport>();
                                        foreach (DAReport userRecycleDAReportItem in userRecycleDAReportList)
                                        {
                                            daRep.WithoutTransactDelete(userRecycleDAReportItem);
                                        }
                                        daReportParentPathList = daReportItem.Report.ParentPath.Split(new char[] { ',' });
                                        daReportParentPathIdList = new List<decimal>();
                                        foreach (string daReportParentPathItem in daReportParentPathList)
                                        {
                                            if (daReportParentPathItem != null && daReportParentPathItem != string.Empty)
                                                daReportParentPathIdList.Add(decimal.Parse(daReportParentPathItem, CultureInfo.InvariantCulture));
                                        }
                                        if (!dAReportIdList.Any(x => daReportParentPathIdList.Contains(x)))
                                            daReport = daRep.WithoutTransactSave(new DAReport() { UserID = userID, All = false, ReportID = daReportItem.ReportID });
                                    }
                                }
                            }
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DAReport> daSinglePartList = null;
                        IList<DAReport> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().ReportID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userID),
                                                                                     new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                            {
                                report = new BReport().GetByID(partId);
                                this.NHSession.Evict(report);
                                userDAReportList = this.NHSession.QueryOver<DAReport>()
                                                                 .Where(x => x.User.ID == userId && !x.All)
                                                                 .List<DAReport>();
                                dAReportIdList = userDAReportList.Select(x => x.Report.ID).ToList<decimal>();
                                userRecycleDAReportList = userDAReportList.Where(x => x.Report.ParentPath.Contains("," + partId.ToString() + ","))
                                                                          .ToList<DAReport>();
                                foreach (DAReport userRecycleDAReportItem in userRecycleDAReportList)
                                {
                                    daRep.WithoutTransactDelete(userRecycleDAReportItem);
                                }
                                daReportParentPathList = report.ParentPath.Split(new char[] { ',' });
                                daReportParentPathIdList = new List<decimal>();
                                foreach (string daReportParentPathItem in daReportParentPathList)
                                {
                                    if (daReportParentPathItem != null && daReportParentPathItem != string.Empty)
                                        daReportParentPathIdList.Add(decimal.Parse(daReportParentPathItem, CultureInfo.InvariantCulture));
                                }
                                if (!dAReportIdList.Any(x => daReportParentPathIdList.Contains(x)))
                                    daReport = daRep.WithoutTransactSave(new DAReport() { ReportID = partId, UserID = userID, All = false });
                            }
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertReport");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// حذف یک گره 
        /// اگر دسترسی همه بخواهد حذف شود باید شناسه صفر باشد
        /// در هنگام واکشی در پرکسی شناسه بخش قرارداده میشود لذا در هنگام حذف نیز باید بر اساس شناسه بخش کار کنیم
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool DeleteReport(decimal reportId, decimal userId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    EntityRepository<DAReport> daRep = new EntityRepository<DAReport>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAReport> daPrtList = null;
                    Report report = null;
                    Report reportAlias = null;
                    User userAlias = null;
                    DAReport userDAReportAlias = null;
                    QueryOver<DAReport, DAReport> dataAccessSubQuery = null;
                    IList<decimal> parentReportIdsList = new List<decimal>();
                    UIValidationExceptions exceptionsList = new UIValidationExceptions();
                    if (reportId == 0)//ریشه مجازی برای کسانی که دسترسی یه همه دارند
                    {
                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().All), true));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DAReport>(() => userDAReportAlias)
                                                          .JoinAlias(() => userDAReportAlias.User, () => userAlias)
                                                          .Where(() => userDAReportAlias.All)
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DAReport>(() => userDAReportAlias)
                                                      .JoinAlias(() => userDAReportAlias.User, () => userAlias)
                                                      .Where(() => userDAReportAlias.All &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DAReport>();
                        }
                        if (daPrtList.Count > 0 && daPrtList.First().All)
                        {
                            daRep.WithoutTransactDelete(daPrtList.First());
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.ReportAccessDenied, "دسترسی غیر مجاز به گزارش", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    else
                    {
                        report = new BReport().GetByID(reportId);
                        if (report != null)
                        {
                            string[] parentPathArray = report.ParentPath.Split(new char[] { ',' });
                            foreach (string parentPath in parentPathArray)
                            {
                                if (parentPath != null && parentPath != string.Empty)
                                    parentReportIdsList.Add(decimal.Parse(parentPath, CultureInfo.InvariantCulture));
                            }
                        }
                        NHSession.Evict(report);

                        if (this.IsSystemTechnicalAdmin)
                        {
                            daPrtList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().UserID), userId),
                                                            new CriteriaStruct(Utility.GetPropertyName(() => new DAReport().ReportID), reportId));
                        }
                        else
                        {
                            dataAccessSubQuery = QueryOver.Of<DAReport>(() => userDAReportAlias)
                                                          .JoinAlias(() => userDAReportAlias.User, () => userAlias).Left
                                                          .JoinAlias(() => userDAReportAlias.Report, () => reportAlias)
                                                          .Where(() => reportAlias.ID == reportId || userDAReportAlias.All || reportAlias.ID.IsIn(parentReportIdsList.ToArray()))
                                                          .And(() => userAlias.ID == BUser.CurrentUser.ID)
                                                          .Select(x => x.ID);
                            daPrtList = this.NHSession.QueryOver<DAReport>(() => userDAReportAlias)
                                                      .JoinAlias(() => userDAReportAlias.User, () => userAlias)
                                                      .JoinAlias(() => userDAReportAlias.Report, () => reportAlias)
                                                      .Where(() => reportAlias.ID == reportId &&
                                                                   userAlias.ID == userId
                                                            )
                                                      .WithSubquery
                                                      .WhereExists(dataAccessSubQuery)
                                                      .List<DAReport>();
                        }
                        if (daPrtList.Count > 0)
                        {
                            foreach (DAReport da in daPrtList)
                            {
                                daRep.WithoutTransactDelete(da);
                            }
                            success = true;
                        }
                        else
                        {
                            success = false;
                            exceptionsList.Add(new ValidationException(ExceptionResourceKeys.ReportAccessDenied, "دسترسی غیر مجاز به گزارش", ExceptionSrc));
                            throw exceptionsList;
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteReport");
                    throw ex;
                }
            }
        }


        #endregion

        #region Corporations
        private IList<DataAccessProxy> GetAllCorporations()
        {
            try
            {
                IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
                IList<DACorporation> daPrecardList = new List<DACorporation>();
                IList<Corporation> corporationList = new List<Corporation>();
                QueryOver<DACorporation, DACorporation> dataAccessSubQuery = null;
                Corporation corporationAlias = null;
                DACorporation daCorporationAlias = null;
                if (this.IsSystemTechnicalAdmin)
                {
                    IList<Corporation> CorporationList = organizationRepository.GetAll();
                    IList<DataAccessProxy> DataAccessProxyList = CorporationList.Select(corporation => new DataAccessProxy() { ID = corporation.ID, Name = corporation.Name, CustomCode = corporation.Code, DeleteEnable = false, All = false }).ToList<DataAccessProxy>();
                    daPrtList = DataAccessProxyList;
                }
                else
                {
                    dataAccessSubQuery = QueryOver.Of<DACorporation>(() => daCorporationAlias)
                                                  .Where(() => daCorporationAlias.CorporationID == corporationAlias.ID)
                                                  .And(() => daCorporationAlias.UserID == BUser.CurrentUser.ID)
                                                  .Select(x => x.ID);
                    corporationList = this.NHSession.QueryOver<Corporation>(() => corporationAlias)
                                                    .WithSubquery
                                                    .WhereExists(dataAccessSubQuery)
                                                    .List<Corporation>();
                    daPrtList = corporationList.Select(x => new DataAccessProxy
                    {
                        ID = x.ID,
                        Name = x.Name,
                        CustomCode = x.Code,
                        DeleteEnable = true,
                        All = false
                    })
                   .ToList<DataAccessProxy>();
                }
                return daPrtList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllCorporations");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllCorporations(string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
                IList<DACorporation> daPrecardList = new List<DACorporation>();
                IList<Corporation> corporationList = new List<Corporation>();
                QueryOver<DACorporation, DACorporation> dataAccessSubQuery = null;
                BCorporation bCorporation = new BCorporation();
                Corporation corporationAlias = null;
                DACorporation daCorporationAlias = null;
                if (this.IsSystemTechnicalAdmin)
                {
                    IList<Corporation> CorporationList = bCorporation.GetAll(SearchTerm);
                    IList<DataAccessProxy> DataAccessProxyList = CorporationList.Select(corporation => new DataAccessProxy() { ID = corporation.ID, Name = corporation.Name, CustomCode = corporation.Code, DeleteEnable = false, All = false }).ToList<DataAccessProxy>();
                    daPrtList = DataAccessProxyList;
                }
                else
                {
                    dataAccessSubQuery = QueryOver.Of<DACorporation>(() => daCorporationAlias)
                                                  .Where(() => daCorporationAlias.CorporationID == corporationAlias.ID)
                                                  .And(() => daCorporationAlias.UserID == BUser.CurrentUser.ID)
                                                  .Select(x => x.ID);
                    corporationList = this.NHSession.QueryOver<Corporation>(() => corporationAlias)
                                                    .Where(() => corporationAlias.Name.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere) ||
                                                                corporationAlias.Code.IsInsensitiveLike(SearchTerm, MatchMode.Anywhere)
                                                          )
                                                    .WithSubquery
                                                    .WhereExists(dataAccessSubQuery)
                                                    .List<Corporation>();
                    daPrtList = corporationList.Select(x => new DataAccessProxy
                    {
                        ID = x.ID,
                        Name = x.Name,
                        CustomCode = x.Code,
                        DeleteEnable = true,
                        All = false
                    })
                   .ToList<DataAccessProxy>();
                }
                return daPrtList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllCorporations");
                throw ex;
            }
        }

        private IList<DataAccessProxy> GetAllEmploymentTypes()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAEmploymentType> daEmplymentType = new List<DAEmploymentType>();
            IList<EmploymentType> employmentTypeList = new List<EmploymentType>();
            QueryOver<DAEmploymentType, DAEmploymentType> dataAccessSubQuery = null;
            EmploymentType employmentTypeAlias = null;
            DAEmploymentType daEmploymentTypeAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<EmploymentType> list = EmploymentTypeRepository.GetAll();
                IList<DataAccessProxy> DataAccessProxyList = list.Select(employmentType => new DataAccessProxy() { ID = employmentType.ID, Name = employmentType.Name, CustomCode = employmentType.CustomCode, DeleteEnable = false, All = false }).ToList<DataAccessProxy>();
                daPrtList = DataAccessProxyList;
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAEmploymentType>(() => daEmploymentTypeAlias)
                                              .Where(() => daEmploymentTypeAlias.EmploymentTypeID == employmentTypeAlias.ID || daEmploymentTypeAlias.All)
                                              .And(() => daEmploymentTypeAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                employmentTypeList = this.NHSession.QueryOver<EmploymentType>(() => employmentTypeAlias)
                                                   .WithSubquery
                                                   .WhereExists(dataAccessSubQuery)
                                                   .List<EmploymentType>();
                daPrtList = employmentTypeList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }

        private IList<DataAccessProxy> GetAllEmploymentTypes(string SearchTem)
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DAEmploymentType> daEmplymentType = new List<DAEmploymentType>();
            IList<EmploymentType> employmentTypeList = new List<EmploymentType>();
            QueryOver<DAEmploymentType, DAEmploymentType> dataAccessSubQuery = null;
            EmploymentType employmentTypeAlias = null;
            DAEmploymentType daEmploymentTypeAlias = null;
            BEmployment bEmployment = new BEmployment();
            if (this.IsSystemTechnicalAdmin)
            {
                IList<EmploymentType> list = bEmployment.GetEmploymentTypeList(SearchTem);
                IList<DataAccessProxy> DataAccessProxyList = list.Select(employmentType => new DataAccessProxy() { ID = employmentType.ID, Name = employmentType.Name, CustomCode = employmentType.CustomCode, DeleteEnable = false, All = false }).ToList<DataAccessProxy>();
                daPrtList = DataAccessProxyList;
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DAEmploymentType>(() => daEmploymentTypeAlias)
                                              .Where(() => daEmploymentTypeAlias.EmploymentTypeID == employmentTypeAlias.ID || daEmploymentTypeAlias.All)
                                              .And(() => daEmploymentTypeAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                employmentTypeList = this.NHSession.QueryOver<EmploymentType>(() => employmentTypeAlias)
                                                    .Where(() => employmentTypeAlias.Name.IsInsensitiveLike(SearchTem, MatchMode.Anywhere) ||
                                                                 employmentTypeAlias.CustomCode.IsInsensitiveLike(SearchTem, MatchMode.Anywhere)
                                                          )
                                                   .WithSubquery
                                                   .WhereExists(dataAccessSubQuery)
                                                   .List<EmploymentType>();
                daPrtList = employmentTypeList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.CustomCode,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }
        private IList<DataAccessProxy> GetAllCorporationsOfUser(decimal userId)
        {
            try
            {
                IList<Corporation> CorporationsOfUserList = userRepository.GetUserCorporationList(userId);
                IList<DataAccessProxy> DataAccessProxyList = CorporationsOfUserList.Select(corporation => new DataAccessProxy() { ID = corporation.ID, Name = corporation.Name, DeleteEnable = true, CustomCode = corporation.Code }).ToList<DataAccessProxy>();
                return DataAccessProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllCorporationsOfUser");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllCorporationsOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<Corporation> CorporationsOfUserList = userRepository.GetUserCorporationList(userId, SearchTerm);
                IList<DataAccessProxy> DataAccessProxyList = CorporationsOfUserList.Select(corporation => new DataAccessProxy() { ID = corporation.ID, Name = corporation.Name, DeleteEnable = true, CustomCode = corporation.Code }).ToList<DataAccessProxy>();
                return DataAccessProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllCorporationsOfUser");
                throw ex;
            }
        }
        private IList<DataAccessProxy> GetAllEmploymentTypesOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllEmploymentTypesAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<EmploymentType> list = userRepository.GetUserEmployTypeList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllEmploymentTypesOfUser");
                throw ex;
            }
        }

        private bool InsertEmploymentType(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DAEmploymentType daEmploymentType = new DAEmploymentType();
                    DAEmploymentType daDep = new DAEmploymentType();
                    EntityRepository<DAEmploymentType> daRep = new EntityRepository<DAEmploymentType>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DAEmploymentType> daEmploymentTypeList = new List<DAEmploymentType>();
                    if (partId == 0)//درج همه
                    {
                        IList<DAEmploymentType> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAEmploymentType().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAEmploymentType().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DAEmploymentType dAEmploymentTypeAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DAEmploymentType>(() => dAEmploymentTypeAlias)
                                        .JoinAlias(() => dAEmploymentTypeAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DAEmploymentType>();
                                    bTemp.DeleteTempList(operationGUID);

                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daEmploymentTypeList = this.NHSession.QueryOver<DAEmploymentType>()
                                                                 .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                                 .List<DAEmploymentType>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DAEmploymentType da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daEmploymentTypeList.Count == 1 && daEmploymentTypeList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daEmploymentTypeList.Any(x => x.EmploymentTypeID == da.EmploymentTypeID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daDep = daRep.WithoutTransactSave(new DAEmploymentType() { UserID = userID, All = true, EmploymentTypeID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daEmploymentTypeList.Count == 1 && daEmploymentTypeList[0].All)
                                    daEmploymentType = daRep.WithoutTransactSave(new DAEmploymentType() { UserID = userID, All = true, EmploymentTypeID = null });
                                else
                                {
                                    foreach (DAEmploymentType daEmploymentTypeItem in daEmploymentTypeList)
                                    {
                                        daEmploymentType = daRep.WithoutTransactSave(new DAEmploymentType() { UserID = userID, All = false, EmploymentTypeID = daEmploymentTypeItem.EmploymentTypeID });
                                    }
                                }
                            }
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DAEmploymentType> daSinglePartList = null;
                        IList<DAEmploymentType> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAEmploymentType().UserID), userID),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new DAEmploymentType().EmploymentTypeID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DAEmploymentType().UserID), userID),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new DAEmploymentType().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daDep = daRep.WithoutTransactSave(new DAEmploymentType() { EmploymentTypeID = partId, UserID = userID, All = false });
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertEmploymentType");
                    throw ex;
                }
            }
        }

        private bool DeleteEmploymentType(decimal dataAccessId)
        {
            bool success = false;
            DAEmploymentType daEmploymentType = null;
            EntityRepository<DAEmploymentType> daRep = new EntityRepository<DAEmploymentType>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DAEmploymentType() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DAEmploymentType> daPrtList = this.NHSession.QueryOver<DAEmploymentType>()
                                                                          .Where(x => x.ID == dataAccessId)
                                                                          .List<DAEmploymentType>();
                        if (daPrtList.Count > 0)
                        {
                            daEmploymentType = daPrtList.First();
                            IList<DAEmploymentType> daCurrentUserPrtList = this.NHSession.QueryOver<DAEmploymentType>()
                                                                                         .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                                    (x.EmploymentTypeID == daEmploymentType.EmploymentTypeID || x.All)
                                                                                      )
                                                                                .List<DAEmploymentType>();
                            this.NHSession.Evict(daEmploymentType);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DAEmploymentType() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.EmploymentTypeAccessDenied, "دسترسی غیر مجاز به نوع استخدام", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteEmploymentType");
                    throw ex;
                }
            }
        }

        #endregion

        #region CostCenter

        private IList<DataAccessProxy> GetAllCostCenters()
        {
            IList<DataAccessProxy> daPrtList = new List<DataAccessProxy>();
            IList<DACostCenter> daCostCenter = new List<DACostCenter>();
            IList<CostCenter> costCenterList = new List<CostCenter>();
            QueryOver<DACostCenter, DACostCenter> dataAccessSubQuery = null;
            CostCenter costCenterAlias = null;
            DACostCenter daCostCenterAlias = null;
            if (this.IsSystemTechnicalAdmin)
            {
                IList<CostCenter> list = costCenterRep.GetAll();
                IList<DataAccessProxy> DataAccessProxyList = list.Select(CostCenter => new DataAccessProxy() { ID = CostCenter.ID, Name = CostCenter.Name, CustomCode = CostCenter.Code, DeleteEnable = false, All = false }).ToList<DataAccessProxy>();
                daPrtList = DataAccessProxyList;
            }
            else
            {
                dataAccessSubQuery = QueryOver.Of<DACostCenter>(() => daCostCenterAlias)
                                              .Where(() => daCostCenterAlias.CostCenterID == costCenterAlias.ID || daCostCenterAlias.All)
                                              .And(() => daCostCenterAlias.UserID == BUser.CurrentUser.ID)
                                              .Select(x => x.ID);
                costCenterList = this.NHSession.QueryOver<CostCenter>(() => costCenterAlias)
                                                   .WithSubquery
                                                   .WhereExists(dataAccessSubQuery)
                                                   .List<CostCenter>();
                daPrtList = costCenterList.Select(x => new DataAccessProxy
                {
                    ID = x.ID,
                    Name = x.Name,
                    CustomCode = x.Code,
                    DeleteEnable = true,
                    All = false
                })
               .ToList<DataAccessProxy>();
            }
            return daPrtList;
        }

        private IList<DataAccessProxy> GetAllEmploymentTypesOfUser(decimal userId, string SearchTerm)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllEmploymentTypesAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<EmploymentType> list = userRepository.GetUserEmployTypeList(userId, SearchTerm);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.CustomCode };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllEmploymentTypesOfUser");
                throw ex;
            }
        }

        private IList<DataAccessProxy> GetAllCostCentersOfUser(decimal userId)
        {
            try
            {
                IList<DataAccessProxy> result = new List<DataAccessProxy>();
                decimal allId = userRepository.HasAllCostCentersAccess(userId);

                if (allId > 0)
                {
                    result.Add(new DataAccessProxy() { ID = allId, All = true, DeleteEnable = true });
                }
                else
                {
                    IList<CostCenter> list = userRepository.GetUserCostCenterList(userId);
                    var l = from o in list
                            select new DataAccessProxy() { ID = o.ID, Name = o.Name, DeleteEnable = true, CustomCode = o.Code };
                    result = l.ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BDataAccess", "GetAllCostCentersOfUser");
                throw ex;
            }
        }

        private bool InsertCostCenter(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool succes = false;
                    DACostCenter daCostCenter = new DACostCenter();
                    DACostCenter daDep = new DACostCenter();
                    EntityRepository<DACostCenter> daRep = new EntityRepository<DACostCenter>(false);
                    IList<decimal> TempUserIDList = new List<decimal>();
                    IList<DACostCenter> daCostCenterList = new List<DACostCenter>();
                    if (partId == 0)//درج همه
                    {
                        IList<DACostCenter> daPartList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACostCenter().UserID), userId));
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                IList<decimal> accessableIDs = TempUserIDList;
                                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                                {
                                    daPartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACostCenter().UserID), TempUserIDList.ToArray(), CriteriaOperation.IN));
                                }
                                else
                                {
                                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                                    DACostCenter dACostCenterAlias = null;
                                    User userAlias = null;
                                    string operationGUID = bTemp.InsertTempList(accessableIDs);
                                    daPartList = NHSession.QueryOver<DACostCenter>(() => dACostCenterAlias)
                                        .JoinAlias(() => dACostCenterAlias.User, () => userAlias)
                                        .JoinAlias(() => userAlias.TempList, () => tempAlias)
                                        .Where(() => tempAlias.OperationGUID == operationGUID)
                                        .List<DACostCenter>();
                                    bTemp.DeleteTempList(operationGUID);

                                }
                                break;
                        }
                        if (!this.IsSystemTechnicalAdmin)
                        {
                            daCostCenterList = this.NHSession.QueryOver<DACostCenter>()
                                                                 .Where(x => x.UserID == BUser.CurrentUser.ID)
                                                                 .List<DACostCenter>();
                        }
                        if (daPartList.Count > 0)
                        {
                            foreach (DACostCenter da in daPartList)
                            {
                                if (this.IsSystemTechnicalAdmin || (daCostCenterList.Count == 1 && daCostCenterList[0].All))
                                    daRep.WithoutTransactDelete(da);
                                else
                                {
                                    if (daCostCenterList.Any(x => x.CostCenterID == da.CostCenterID))
                                        daRep.WithoutTransactDelete(da);
                                }
                            }
                        }
                        if (this.IsSystemTechnicalAdmin)
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                daDep = daRep.WithoutTransactSave(new DACostCenter() { UserID = userID, All = true, CostCenterID = null });
                            }
                        }
                        else
                        {
                            foreach (decimal userID in TempUserIDList)
                            {
                                if (daCostCenterList.Count == 1 && daCostCenterList[0].All)
                                    daCostCenter = daRep.WithoutTransactSave(new DACostCenter() { UserID = userID, All = true, CostCenterID = null });
                                else
                                {
                                    foreach (DACostCenter daCostCenterItem in daCostCenterList)
                                    {
                                        daCostCenter = daRep.WithoutTransactSave(new DACostCenter() { UserID = userID, All = false, CostCenterID = daCostCenterItem.CostCenterID });
                                    }
                                }
                            }
                        }
                        succes = true;
                    }
                    else
                    {
                        IList<DACostCenter> daSinglePartList = null;
                        IList<DACostCenter> daAllPartsList = null;
                        switch (Dalot)
                        {
                            case DataAccessLevelOperationType.Single:
                                TempUserIDList.Add(userId);
                                break;
                            case DataAccessLevelOperationType.Group:
                                TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                                break;
                        }
                        foreach (decimal userID in TempUserIDList)
                        {
                            daSinglePartList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACostCenter().UserID), userID),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => new DACostCenter().CostCenterID), partId));
                            daAllPartsList = daRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACostCenter().UserID), userID),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new DACostCenter().All), true));
                            if (daSinglePartList.Count == 0 && daAllPartsList.Count == 0)
                                daDep = daRep.WithoutTransactSave(new DACostCenter() { CostCenterID = partId, UserID = userID, All = false });
                        }
                        succes = true;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return succes;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertCostCenter");
                    throw ex;
                }
            }
        }

        private bool DeleteCostCenter(decimal dataAccessId)
        {
            bool success = false;
            DACostCenter daCostCenter = null;
            EntityRepository<DACostCenter> daRep = new EntityRepository<DACostCenter>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DACostCenter() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        success = true;
                    }
                    else
                    {
                        IList<DACostCenter> daPrtList = this.NHSession.QueryOver<DACostCenter>()
                                                                          .Where(x => x.ID == dataAccessId)
                                                                          .List<DACostCenter>();
                        if (daPrtList.Count > 0)
                        {
                            daCostCenter = daPrtList.First();
                            IList<DACostCenter> daCurrentUserPrtList = this.NHSession.QueryOver<DACostCenter>()
                                                                                         .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                                    (x.CostCenterID == daCostCenter.CostCenterID || x.All)
                                                                                      )
                                                                                .List<DACostCenter>();
                            this.NHSession.Evict(daCostCenter);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DACostCenter() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.CostCenterAccessDenied, "دسترسی غیر مجاز به مرکز هزینه", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteEmploymentType");
                    throw ex;
                }
            }
        }

        #endregion


        /// <summary>
        /// یک کاربر به همراه دسترسی اطلاعاتی را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserSecurityInfo(decimal userId)
        {
            try
            {
                User user = new BUser().GetByID(userId);
                return user;
            }
            catch (Exception ex)
            {
                BaseBusiness<User>.LogException(ex, "BDataAccess", "GetUserSecurityInfo");
                throw ex;
            }
        }


        private bool InsertCorporation(DataAccessLevelOperationType Dalot, decimal partId, decimal userId, UserSearchKeys? searchKey, string searchTerm)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    bool success = false;
                    DACorporation daCorporationObj = new DACorporation();
                    IList<decimal> TempUserIDList = new List<decimal>();
                    EntityRepository<DACorporation> DACorporationRep = new EntityRepository<DACorporation>(false);
                    switch (Dalot)
                    {
                        case DataAccessLevelOperationType.Single:
                            TempUserIDList.Add(userId);
                            break;
                        case DataAccessLevelOperationType.Group:
                            TempUserIDList = this.userRepository.GetAllUserIDList(BUser.CurrentUser.ID, searchKey, searchTerm, false);
                            break;
                    }
                    foreach (decimal userID in TempUserIDList)
                    {
                        IList<DACorporation> DACorporationList = DACorporationRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DACorporation().UserID), userID));
                        if (DACorporationList.Count > 0)
                        {
                            foreach (DACorporation daCorporation in DACorporationList)
                            {
                                DACorporationRep.Delete(daCorporation);
                            }
                        }
                        DACorporationRep.Save(new DACorporation() { UserID = userID, CorporationID = partId });
                    }
                    success = true;
                    return success;
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "InsertCorporation");
                    throw ex;
                }
            }
        }

        private bool DeleteCorporation(decimal dataAccessId)
        {
            bool success = false;
            DACorporation daCorporation = null;
            EntityRepository<DACorporation> daRep = new EntityRepository<DACorporation>(false);
            UIValidationExceptions exceptionsList = new UIValidationExceptions();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (this.IsSystemTechnicalAdmin)
                    {
                        daRep.WithoutTransactDelete(new DACorporation() { ID = dataAccessId });
                        NHibernateSessionManager.Instance.CommitTransactionOn();
                        return true;
                    }
                    else
                    {
                        IList<DACorporation> daPrtList = this.NHSession.QueryOver<DACorporation>()
                                                                       .Where(x => x.ID == dataAccessId)
                                                                       .List<DACorporation>();
                        if (daPrtList.Count > 0)
                        {
                            daCorporation = daPrtList.First();
                            IList<DAPrecard> daCurrentUserPrtList = this.NHSession.QueryOver<DACorporation>()
                                                                                  .Where(x => x.UserID == BUser.CurrentUser.ID &&
                                                                                              x.CorporationID == daCorporation.CorporationID
                                                                                        )
                                                                                  .List<DAPrecard>();
                            this.NHSession.Evict(daCorporation);
                            if (daCurrentUserPrtList.Count > 0)
                            {
                                daRep.WithoutTransactDelete(new DACorporation() { ID = dataAccessId });
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                                success = true;
                            }
                            else
                            {
                                success = false;
                                exceptionsList.Add(new ValidationException(ExceptionResourceKeys.CorporationAccessDenied, "دسترسی غیر مجاز به شرکت", ExceptionSrc));
                                throw exceptionsList;
                            }
                        }
                    }
                    return success;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BDataAccess", "DeleteCorporation");
                    throw ex;
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckMasterDataAccessLevelsLoadAccess()
        {
        }


    }
}
