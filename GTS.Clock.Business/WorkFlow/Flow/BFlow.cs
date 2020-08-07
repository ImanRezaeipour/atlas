using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Temp;
using NHibernate;
using GTS.Clock.Model.Security;

namespace GTS.Clock.Business.RequestFlow
{
    public class BFlow : BaseBusiness<Flow>, IUnderManagmentTree
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BFlow";
        FlowRepository flowRep = new FlowRepository(false);
        SubstituteRepository substituteRep = new SubstituteRepository(false);
        BDepartment bDep = new BDepartment();
        BUnderManagment bUnderManagment = new BUnderManagment();
        private BTemp bTemp = new BTemp();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        UnderManagmentRepository underManagment = new UnderManagmentRepository(false);
        private bool IsSystemTechnicalAdmin
        {
            get
            {
                bool isSystemTechnicalAdmin = false;
                if (BUser.CurrentUser.Role.ID != 0 && BUser.CurrentUser.Role.CustomCode != string.Empty && (RoleCustomCodeType)Enum.Parse(typeof(RoleCustomCodeType), BUser.CurrentUser.Role.CustomCode) == RoleCustomCodeType.SystemTechnicalAdmin)
                    isSystemTechnicalAdmin = true;
                return isSystemTechnicalAdmin;
            }
        }
        #region Manager Flow

        /// <summary>
        /// لیستی از مدیران یک جریان به همراه اولویت هر یک را برمیگرداند
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        public IList<ManagerProxy> GetAllManagers(decimal flowID)
        {
            try
            {
                EntityRepository<ManagerFlow> managerFlowRep = new EntityRepository<ManagerFlow>(false);
                IList<ManagerProxy> list = new List<ManagerProxy>();
                IList<ManagerFlow> mnagers = managerFlowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Flow), new Flow() { ID = flowID }),
                                                                          new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Active), true));
                foreach (ManagerFlow mngFlow in mnagers.OrderBy(x => x.Level))
                {
                    ManagerProxy proxy = new ManagerProxy();
                    if (mngFlow.Manager.ManagerType == ManagerAssignType.Person)
                    {
                        proxy.ManagerType = ManagerType.Person;
                        proxy.OwnerID = mngFlow.Manager.Person.ID;
                        proxy.Name = mngFlow.Manager.Person.Name;
                    }
                    else if (mngFlow.Manager.ManagerType == ManagerAssignType.OrganizationUnit)
                    {
                        proxy.ManagerType = ManagerType.OrganizationUnit;
                        proxy.OwnerID = mngFlow.Manager.OrganizationUnit.ID;
                        proxy.Name = mngFlow.Manager.OrganizationUnit.Name;
                        if (mngFlow.Manager.OrganizationUnit.Person != null)
                            proxy.Name += " (" + mngFlow.Manager.OrganizationUnit.Person.Name + ")";
                    }
                    else
                    {
                        proxy.ManagerType = ManagerType.None;
                    }
                    proxy.ID = mngFlow.ID;
                    proxy.Level = mngFlow.Level;
                    list.Add(proxy);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetAllManagers");
                throw ex;
            }
        }

        /// <summary>
        /// بروزرسانی مدیرهای یک جریان خاص
        /// </summary>
        /// <param name="flowId">کد جریان</param>
        /// <param name="activeFlow">وضعیت فعال بودن</param>
        /// <param name="mngrFlows">مدیران جریان کاری</param>
        //public void UpdateManagerFlows(decimal flowId, bool activeFlow, bool mainFlow, IList<ManagerProxy> mngrFlows)
        //{
        //    ManagerRepository managerRep = new ManagerRepository(false);
        //    using (NHibernateSessionManager.Instance.BeginTransactionOn())
        //    {
        //        try
        //        {
        //            if (Utility.IsEmpty(mngrFlows))
        //            {
        //                UIValidationExceptions exception = new UIValidationExceptions();
        //                exception.Add(new ValidationException(ExceptionResourceKeys.FlowMustHaveOneManagerFlow, "جریان کاری باید حداقل دارای یک مدیر در خود باشد", ExceptionSrc));
        //                throw exception;
        //            }

        //            BManager bManager = new BManager();
        //            Flow flow = base.GetByID(flowId);
        //            flow.ActiveFlow = activeFlow;
        //            flow.MainFlow = mainFlow;
        //            flow.ManagerFlowList = new List<ManagerFlow>();
        //            flowRep.DeleteManagerFlows(flowId);
        //            foreach (ManagerProxy mp in mngrFlows)
        //            {
        //                ManagerFlow managerFlow = new ManagerFlow();
        //                Manager mng;
        //                if (mp.OwnerID == 0)
        //                {
        //                    UIValidationExceptions exception = new UIValidationExceptions();
        //                    exception.Add(new ValidationException(ExceptionResourceKeys.FlowPersonOrOrganizationMustSpecified, "یا شخص یا پست سازمانی باید مقداردهی شود", ExceptionSrc));
        //                    throw exception;
        //                }
        //                if (mp.ManagerType == ManagerType.Person)
        //                {
        //                    mng = managerRep.GetManagerByPersonID(mp.OwnerID);
        //                    if (mng == null || mng.ID == 0)
        //                    {
        //                        Manager manager = new Manager();
        //                        manager.Person = new Person() { ID = mp.OwnerID };
        //                        manager.Active = true;
        //                        bManager.SaveChanges(manager, UIActionType.ADD);
        //                        mng = manager;
        //                    }
        //                    else
        //                    {
        //                        if (mng.OrganizationUnit != null)
        //                        {
        //                            mng.OrganizationUnit = null;
        //                            bManager.SaveChanges(mng, UIActionType.EDIT);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    mng = managerRep.GetManagerByOrganID(mp.OwnerID);
        //                    if (mng == null || mng.ID == 0)
        //                    {
        //                        Manager manager = new Manager();
        //                        manager.OrganizationUnit = new OrganizationUnit() { ID = mp.OwnerID };
        //                        manager.Active = true;
        //                        bManager.SaveChanges(manager, UIActionType.ADD);
        //                        mng = manager;
        //                    }
        //                    else
        //                    {
        //                        if (mng.Person != null)
        //                        {
        //                            mng.Person = null;
        //                            bManager.SaveChanges(mng, UIActionType.ADD);
        //                        }
        //                    }
        //                }
        //                managerFlow.Active = true;
        //                managerFlow.Manager = mng;
        //                managerFlow.Flow = flow;
        //                managerFlow.Level = mp.Level;
        //                flow.ManagerFlowList.Add(managerFlow);
        //            }
        //            SaveChanges(flow, UIActionType.EDIT);
        //            managerRep.SetManagerActivation();
        //            new RequestStatusRepositiory(false).DeleteSuspendRequestStates(flowId);
        //            LogUserAction(flow,"Change Manager Flow Levels");
        //            NHibernateSessionManager.Instance.CommitTransactionOn();
        //        }

        //        catch (Exception ex)
        //        {
        //            NHibernateSessionManager.Instance.RollbackTransactionOn();
        //            LogException(ex);
        //            throw ex;
        //        }
        //    }
        //}


        public void UpdateManagerFlows(decimal flowId, bool activeFlow, bool mainFlow, IList<ManagerProxy> mngrFlows)
        {
            ValidationException validationException = null;
            UIValidationExceptions exception = new UIValidationExceptions();
            ManagerRepository managerRep = new ManagerRepository(false);
            NHibernateSessionManager.Instance.ClearSession();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    if (Utility.IsEmpty(mngrFlows))
                    {
                        //UIValidationExceptions exception = new UIValidationExceptions();
                        exception.Add(new ValidationException(ExceptionResourceKeys.FlowMustHaveOneManagerFlow, "جریان کاری باید حداقل دارای یک مدیر در خود باشد", ExceptionSrc));
                        throw exception;
                    }
                    OrganizationUnit organizationUnitAlias = null;
                    Person personAllias = null;
                    IList<OrganizationUnit> organizationUnitList = NHSession.QueryOver(() => organizationUnitAlias).Left
                                                                            .JoinAlias(() => organizationUnitAlias.Person, () => personAllias)
                                                                            .Where(() => organizationUnitAlias.ID.IsIn(mngrFlows.Where(y => y.ManagerType == ManagerType.OrganizationUnit).Select(y => y.OwnerID).ToArray()) &&
                                                                                         (!personAllias.Active ||
                                                                                          personAllias.IsDeleted)
                                                                                   )
                                                                            .List<OrganizationUnit>();
                    if (organizationUnitList.Count != 0)
                    {
                        foreach (OrganizationUnit org in organizationUnitList)
                        {
                            validationException = new ValidationException(ExceptionResourceKeys.PersonnelAssignedToOrganizationPostIsNotActiveOrDeleted, "پست سازمانی مربوطه به پرسنل غیر فعال یا به پرسنل حذف شده اختصاص داده شده ; وضعیت پست سازمانی یا پرسنل را مشخص کنید :", ExceptionSrc);
                            validationException.Data.Add("Info", org.Name);
                            exception.Add(validationException);
                        }
                        if (exception.Count > 0)
                        {
                            throw exception;
                        }
                    }
                    IList<Person> personList = NHSession.QueryOver(() => personAllias)
                                                         .Where(() => personAllias.ID.IsIn(mngrFlows.Where(x => x.ManagerType == ManagerType.Person).Select(x => x.OwnerID).ToArray()) &&
                                                                      !personAllias.Active
                                                               )
                                                         .List<Person>();
                    if (personList.Count != 0)
                    {
                        foreach (Person person in personList)
                        {
                            validationException = new ValidationException(ExceptionResourceKeys.SelectedPersonnelIsDeActive, "پرسنل انتخابی زیر غیر فعال شده است :", ExceptionSrc);
                            validationException.Data.Add("Info", person.Name);
                            exception.Add(validationException);
                        }
                        if (exception.Count > 0)
                        {
                            throw exception;
                        }
                    }


                    BManager bManager = new BManager();
                    BManagerFlow bManagerFlow = new BManagerFlow();
                    BManagerFlowCondition bManagerFlowCondition = new BManagerFlowCondition();
                    Flow flow = base.GetByID(flowId);
                    flow.ActiveFlow = activeFlow;
                    flow.MainFlow = mainFlow;
                    flow.ManagerFlowList = new List<ManagerFlow>();
                    IList<ManagerFlow> existingManagerFlowList = this.flowRep.GetAllManagerFlow(flowId);
                    mngrFlows = this.ResolveManagerFlowConflicts(mngrFlows);
                    foreach (ManagerProxy mp in mngrFlows)
                    {
                        ManagerFlow managerFlow = new ManagerFlow();
                        Manager mng;
                        ManagerFlow existingManagerFlow = null;
                        if (mp.OwnerID == 0)
                        {
                            // UIValidationExceptions exception = new UIValidationExceptions();
                            exception.Add(new ValidationException(ExceptionResourceKeys.FlowPersonOrOrganizationMustSpecified, "یا شخص یا پست سازمانی باید مقداردهی شود", ExceptionSrc));
                            throw exception;
                        }
                        if (mp.ManagerType == ManagerType.Person)
                        {
                            mng = managerRep.GetManagerByPersonID(mp.OwnerID);
                            if (mng == null || mng.ID == 0)
                            {
                                Manager manager = new Manager();
                                manager.Person = new Person() { ID = mp.OwnerID };
                                manager.Active = true;
                                bManager.SaveChanges(manager, UIActionType.ADD);
                                mng = manager;
                            }
                            //else
                            //{
                            //    if (mng.OrganizationUnit != null)
                            //    {
                            //        mng.OrganizationUnit = null;
                            //        bManager.SaveChanges(mng, UIActionType.EDIT);
                            //    }
                            //}
                            existingManagerFlow = existingManagerFlowList.Where(x => x.Manager.Person != null && x.Manager.Person.ID == mp.OwnerID).FirstOrDefault();
                            if (existingManagerFlow != null)
                            {
                                existingManagerFlow.IsExist = true;
                                existingManagerFlow.Level = mp.Level;
                                bManagerFlow.SaveChanges(existingManagerFlow, UIActionType.EDIT);
                            }
                            else
                            {
                                managerFlow.Active = true;
                                managerFlow.Manager = mng;
                                managerFlow.Flow = flow;
                                managerFlow.Level = mp.Level;
                                flow.ManagerFlowList.Add(managerFlow);
                            }
                        }
                        else
                        {
                            mng = managerRep.GetManagerByOrganID(mp.OwnerID);
                            if (mng == null || mng.ID == 0)
                            {
                                Manager manager = new Manager();
                                manager.OrganizationUnit = new OrganizationUnit() { ID = mp.OwnerID };
                                manager.Active = true;
                                bManager.SaveChanges(manager, UIActionType.ADD);
                                mng = manager;
                            }
                            //else
                            //{
                            //    if (mng.Person != null)
                            //    {
                            //        mng.Person = null;
                            //        bManager.SaveChanges(mng, UIActionType.ADD);
                            //    }
                            //}
                            existingManagerFlow = existingManagerFlowList.Where(x => x.Manager.OrganizationUnit != null && x.Manager.OrganizationUnit.ID == mp.OwnerID).FirstOrDefault();
                            if (existingManagerFlow != null)
                            {
                                existingManagerFlow.IsExist = true;
                                existingManagerFlow.Level = mp.Level;
                                bManagerFlow.SaveChanges(existingManagerFlow, UIActionType.EDIT);
                            }
                            else
                            {
                                managerFlow.Active = true;
                                managerFlow.Manager = mng;
                                managerFlow.Flow = flow;
                                managerFlow.Level = mp.Level;
                                flow.ManagerFlowList.Add(managerFlow);
                            }
                        }
                    }
                    existingManagerFlowList = existingManagerFlowList.Where(x => !x.IsExist).ToList<ManagerFlow>();
                    foreach (ManagerFlow extraManagerFlow in existingManagerFlowList)
                    {
                        bManagerFlowCondition.DeleteManagerFlowConditionsByManagerFlow(extraManagerFlow.ID);
                        extraManagerFlow.Active = false;
                        bManagerFlow.SaveChanges(extraManagerFlow, UIActionType.EDIT);
                    }
                    SaveChanges(flow, UIActionType.EDIT);
                    managerRep.SetManagerActivation();
                    substituteRep.SetSubstituteActivation();
                    new RequestStatusRepositiory(false).DeleteSuspendRequestStates(flowId);
                    LogUserAction(flow, "Change Manager Flow Levels");
                    NHibernateSessionManager.Instance.CommitTransactionOn();

                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex);
                    throw ex;
                }
            }
        }

        private IList<ManagerProxy> ResolveManagerFlowConflicts(IList<ManagerProxy> managerFlowProxyList)
        {
            foreach (ManagerProxy managerFlowProxy in managerFlowProxyList)
            {
                if (managerFlowProxy.Level != -1)
                {
                    ManagerProxy conflictedManagerFlowProxy = null;
                    Person person = null;
                    OrganizationUnit organizationUnit = null;
                    IList<ManagerProxy> ImprovableManagerFlowProxyList = null;
                    switch (managerFlowProxy.ManagerType)
                    {
                        case ManagerType.Person:
                            person = this.NHSession.QueryOver<Person>()
                                                   .Where(x => x.ID == managerFlowProxy.OwnerID)
                                                   .List<Person>()
                                                   .FirstOrDefault();
                            if (person != null && person.OrganizationUnit != null)
                                conflictedManagerFlowProxy = managerFlowProxyList.Where(x => x.ManagerType == ManagerType.OrganizationUnit && x.OwnerID == person.OrganizationUnit.ID).FirstOrDefault();
                            break;
                        case ManagerType.OrganizationUnit:
                            organizationUnit = this.NHSession.QueryOver<OrganizationUnit>()
                                                             .Where(x => x.ID == managerFlowProxy.OwnerID)
                                                             .List<OrganizationUnit>()
                                                             .FirstOrDefault();
                            if (organizationUnit != null && organizationUnit.Person != null)
                                conflictedManagerFlowProxy = managerFlowProxyList.Where(x => x.ManagerType == ManagerType.Person && x.OwnerID == organizationUnit.Person.ID).FirstOrDefault();
                            break;
                    }
                    if (conflictedManagerFlowProxy != null)
                    {
                        ImprovableManagerFlowProxyList = managerFlowProxyList.Where(x => x.Level > conflictedManagerFlowProxy.Level).ToList<ManagerProxy>();
                        if (ImprovableManagerFlowProxyList != null)
                        {
                            foreach (ManagerProxy improvableManagerFlowProxy in ImprovableManagerFlowProxyList)
                            {
                                improvableManagerFlowProxy.Level = improvableManagerFlowProxy.Level - 1;
                            }
                        }
                        conflictedManagerFlowProxy.Level = -1;
                    }
                }
            }
            return managerFlowProxyList.Where(x => x.Level != -1).ToList();
        }



        #endregion

        public override IList<Flow> GetAll()
        {
            try
            {

                IList<decimal> accessableIDs = accessPort.GetAccessibleFlows();
                IList<Flow> list = new List<Flow>();
                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    list = flowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().ID), accessableIDs.ToArray(), CriteriaOperation.IN));


                }
                else
                {
                    Flow flowAlias = null;
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    list = NHSession.QueryOver(() => flowAlias)
                                                      .JoinAlias(() => flowAlias.TempList, () => tempAlias)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID)
                                                      .List<Flow>();

                    this.bTemp.DeleteTempList(operationGUID);

                }
                foreach (Flow item in list)
                {
                    if (item.FlowGroup == null)
                        item.FlowGroup = new FlowGroup();
                }
                list = list.Where(f => f.IsForService == false).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetAll");
                throw ex;
            }
        }

        public Flow GetServiceFlow()
        {
            try
            {
                Flow flowAlias = null;
                Flow flow = NHSession.QueryOver(() => flowAlias).Where(() => flowAlias.IsForService && flowAlias.IsDeleted == false && flowAlias.ActiveFlow).List().FirstOrDefault();
                return flow;
            }
            catch (Exception ex)
            {

                LogException(ex, "BFlow", "GetClockFlow");
                throw ex;
            }
        }
        /// <summary>
        /// لیست جریانهایی که یک مدیر در آنها نقش دارد را برمیگرداند
        /// </summary>
        /// <param name="managerId">شناسه مدیر</param>
        /// <returns></returns>
        public IList<Flow> GetAllFlowByManager(decimal managerId)
        {
            try
            {
                IList<Department> departmentsList = new BDepartment().GetAll();
                EntityRepository<ManagerFlow> mngFlowRep = new EntityRepository<ManagerFlow>(false);
                IList<ManagerFlow> list =
                    mngFlowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Manager), new Manager() { ID = managerId }),
                                             new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Active), true));
                IList<Flow> flows =
                    list.Where(x => !x.Flow.IsDeleted).GroupBy(x => x.Flow.ID)
                    .Select(x => x.First().Flow)
                    .ToList();
                IList<Flow> result = new List<Flow>();
                IList<decimal> ids = accessPort.GetAccessibleFlows();
                foreach (Flow flow in flows)
                {
                    if (ids.Contains(flow.ID))
                    {
                        result.Add(flow);
                    }
                }
                foreach (Flow flow in result)
                {
                    flow.DepartmentCount = bUnderManagment.GetUnderManagmentDepartmentByFlow(flow, false, departmentsList).Count;
                    flow.PersonCount = bUnderManagment.GetUnderManagmentPersonsByFlow(flow).Count;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetAllFlowByManager");
                throw ex;
            }
        }

        public IList<Flow> SearchFlow(FlowSearchFields field, string searchVal)
        {
            try
            {
                IList<Flow> list = new List<Flow>();
                IList<decimal> accessableIDs = accessPort.GetAccessibleFlows();
                switch (field)
                {
                    case FlowSearchFields.FlowName:
                    case FlowSearchFields.NotSpec:


                        if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                        {
                            list = flowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().FlowName), searchVal, CriteriaOperation.Like),
                                                   new CriteriaStruct(Utility.GetPropertyName(() => new Flow().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
                        }
                        else
                        {
                            Flow flowAlias = null;
                            GTS.Clock.Model.Temp.Temp tempAlias = null;
                            string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                            list = NHSession.QueryOver(() => flowAlias)
                                                              .JoinAlias(() => flowAlias.TempList, () => tempAlias)
                                                              .Where(() => tempAlias.OperationGUID == operationGUID && flowAlias.FlowName.IsInsensitiveLike(searchVal, MatchMode.Anywhere))
                                                              .List<Flow>();

                            this.bTemp.DeleteTempList(operationGUID);
                        }
                        break;

                    case FlowSearchFields.AccessGroupName:
                        list = flowRep.GetAllByAccessGroupName(searchVal, accessableIDs.ToArray());

                        break;
                }

                foreach (Flow item in list)
                {
                    if (item.FlowGroup == null)
                        item.FlowGroup = new FlowGroup();
                }
                list = list.Where(f => f.IsForService == false).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "SearchFlow");
                throw ex;
            }
        }

        public IList<Flow> SearchFlow(FlowSearchFields field, string searchVal, bool isMatchWholWord)
        {
            try
            {
                MatchMode matchMode = MatchMode.Anywhere;
                CriteriaOperation criteriaOperation = CriteriaOperation.Like;
                if (isMatchWholWord)
                {
                    matchMode = MatchMode.Exact;
                    criteriaOperation = CriteriaOperation.Equal;
                }
                IList<Flow> list = new List<Flow>();
                IList<decimal> accessableIDs = accessPort.GetAccessibleFlows();
                switch (field)
                {
                    case FlowSearchFields.FlowName:
                    case FlowSearchFields.NotSpec:


                        if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                        {
                            list = flowRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().FlowName), searchVal, criteriaOperation),
                                                   new CriteriaStruct(Utility.GetPropertyName(() => new Flow().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
                        }
                        else
                        {
                            Flow flowAlias = null;
                            GTS.Clock.Model.Temp.Temp tempAlias = null;
                            string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                            list = NHSession.QueryOver(() => flowAlias)
                                                              .JoinAlias(() => flowAlias.TempList, () => tempAlias)
                                                              .Where(() => tempAlias.OperationGUID == operationGUID && flowAlias.FlowName.IsInsensitiveLike(searchVal, matchMode))
                                                              .List<Flow>();

                            this.bTemp.DeleteTempList(operationGUID);
                        }
                        break;

                    case FlowSearchFields.AccessGroupName:
                        list = flowRep.GetAllByAccessGroupName(searchVal, accessableIDs.ToArray());

                        break;
                }

                foreach (Flow item in list)
                {
                    if (item.FlowGroup == null)
                        item.FlowGroup = new FlowGroup();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "SearchFlow");
                throw ex;
            }
        }

        #region Tree

        /// <summary>
        /// ریشه درخت را برمیگرداند
        /// </summary>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public Department GetDepartmentRoot()
        {
            try
            {
                return bDep.GetDepartmentsTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentRoot");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک بخش را برمیگرداند
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal nodeID, decimal flowId, IList<Department> departmentsList)
        {
            try
            {
                var flow = this.GetByID(flowId);
                return bDep.GetDepartmentChilds(flow, nodeID, departmentsList);
                /*DepartmentRepository depRep = new DepartmentRepository(false);
                Flow flow = base.GetByID(flowId);
                List<Department> underManagmentTree = new List<Department>();
                IList<Department> containsNode = bUnderManagment.GetUnderManagmentDepartmentByFlow(flow, true);
                foreach (Department dep in containsNode)
                {
                    underManagmentTree.Add(dep);
                }
                IList<Department> childs = bDep.GetDepartmentChildsWithoutDA(nodeID);
                IList<Department> result = new List<Department>();
                foreach (Department child in childs)
                {
                    if (underManagmentTree.Contains(child))
                    {
                        result.Add(child);
                    }
                }
                
                return result;*/
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentChilds");
                throw ex;
            }
        }
         
        /// <summary>
        /// پرسنل یک بخش را برمیگرداند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public IList<Person> GetDepartmentPerson(decimal departmentID)
        {
            try
            {
                Department dep = bDep.GetByID(departmentID);
                if (dep.PersonList != null)
                {
                    return dep.PersonList.Where(x => x.Active && !x.IsDeleted).ToList();
                }
                return new List<Person>();
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentPerson");
                throw ex;
            }
        }

        /// <summary>
        /// پرسنل یک بخش را برمیگرداند
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public IList<Person> GetDepartmentPerson(decimal departmentID, decimal flowId)
        {
            try
            {
                Flow flow = base.GetByID(flowId);
                IList<Department> departmentsList = new BDepartment().GetAll();
                Department dep = departmentsList.Where(x => x.ID == departmentID).FirstOrDefault();
                List<Person> list = new List<Person>();
                IList<Department> contains = bUnderManagment.GetUnderManagmentDepartmentByFlow(flow, false, departmentsList);
                IList<UnderManagment> unders = flow.UnderManagmentList.Where(x => x.Department.ID == departmentID).ToList();
                Department containsDepartment = contains.Where(x => x.ID == departmentID).FirstOrDefault();
                if (containsDepartment != null)//manager has access to Department 
                {
                    if (unders.Where(x => x.Contains && x.Person != null).Count() == 0 || unders.Where(x => x.Contains && x.Person == null).Count() > 0)
                    {
                        list.AddRange(dep.PersonList);
                    }
                    foreach (UnderManagment under in unders)
                    {
                        if (under.Person != null && under.Person.ID > 0)
                        {
                            if (under.Contains)
                            {
                                list.Add(under.Person);
                            }
                            else //this person must remove from accessable person list
                            {
                                list.Remove(under.Person);
                            }
                        }
                    }
                }
                return list.Where(x => x.Active && !x.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetDepartmentPerson");
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// جریان های کاری یک اپراتور را بر می گرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public IList<Flow> GetOperatorWorkFlows(decimal ManagerPersonId)
        {
            try
            {
                Flow flowAlias = null;
                Operator operatorAlias = null;
                IList<Flow> FlowList = NHSession.QueryOver(() => flowAlias)
                                                .JoinAlias(() => flowAlias.OperatorList, () => operatorAlias)
                                                 .Where(() => operatorAlias.Person.ID == ManagerPersonId &&
                                                              operatorAlias.Active &&
                                                              !flowAlias.IsDeleted
                                                       )
                                                .List<Flow>();
                return FlowList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetOperatorWorkFlows");
                throw ex;
            }
        }

        /// <summary>
        /// جریان های کاری یک مدیر را بر میگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public IList<Flow> GetManagerWorkFlows(decimal managerId)
        {
            try
            {
                ManagerFlow managerFlowAlias = null;
                Flow flowAlias = null;
                IList<Flow> FlowList = NHSession.QueryOver(() => flowAlias)
                                                .JoinAlias(() => flowAlias.ManagerFlowList, () => managerFlowAlias)
                                                .Where(() => managerFlowAlias.Manager.ID == managerId &&
                                                             managerFlowAlias.Active &&
                                                    //flowAlias.ActiveFlow &&
                                                             !flowAlias.IsDeleted
                                                      ).List<Flow>();
                return FlowList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetManagerWorkFlows");
                throw ex;
            }
        }

        /// <summary>
        /// جریان های کاری یک پرسنل را برمیگرداند
        /// </summary>
        /// <param name="personnId"></param>
        /// <returns></returns>
        public IList<Flow> GetPersonnlWorkFlows(decimal personnId)
        {
            try
            {
                Flow flowAlias = null;
                IList<Flow> FlowList = new List<Flow>();
                IList<decimal> FlowIdList = flowRep.GetPersonnelWorkFlows(personnId);
                if (FlowIdList.Count != 0)
                {
                    FlowList = NHSession.QueryOver(() => flowAlias)
                                               .Where(() => !flowAlias.IsDeleted &&
                                                            flowAlias.ID.IsIn(FlowIdList.ToList<decimal>())
                                                      )
                                               .List<Flow>();
                }
                return FlowList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetPersonnlWorkFlows");
                throw ex;
            }
        }
        /// <summary>
        /// مدیران یک جریان را برمیگرداند
        /// بر اساس اولویت مرتب میشوند
        /// </summary>
        /// <param name="flowID"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<Manager> GetManagerFlow(decimal flowID)
        {
            try
            {
                IList<Manager> list = new BManager().GetManagerFlow(flowID);
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BFlow", "GetManagerFlow");
                throw ex;
            }
        }

        /// <summary>
        /// نام جریان خالی نباشد
        /// نام جریان تکراری نباشد
        /// گروه پیشکارت خالی نباشد
        /// </summary>
        /// <param name="flow"></param>
        protected override void InsertValidate(Flow flow)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            IList<OrganizationUnit> organizationList = null;
            IList<OrganizationUnit> OrganizationUnitList = flow.ManagerFlowList.Where(x => x.Manager.OrganizationUnit != null).Select(x => x.Manager.OrganizationUnit).ToList<OrganizationUnit>();
            if (OrganizationUnitList.Count != 0 && OrganizationUnitList[0] != null)
            {
                organizationList = this.GetOrganizationPersonnelDeactiveOrIsDeleted(OrganizationUnitList);
                if (organizationList.Count != 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonnelAssignedToOrganizationPostIsNotActiveOrDeleted, "پست سازمانی مربوطه به پرسنل غیر فعال یابه پرسنل حذف شده اختصاص داده شده ; وضعیت پست سازمانی یا پرسنل را مشخص کنید :", ExceptionSrc));
                }
            }
            if (Utility.IsEmpty(flow.FlowName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRequierd, "نام جریان نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                //int count =
                //   flowRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().FlowName), flow.FlowName));
                int count = flowRep.Find(f => f.FlowName == flow.FlowName && f.IsDeleted != true).Count();
                if (count > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRepeated, "نام جریان نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (flow.AccessGroup == null || flow.AccessGroup.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowAccessGroupRequierd, "گروه پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            if (flow.FlowGroup == null || flow.FlowGroup.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowGroupNameRequired, "گروه جریان نباید خالی باشد", ExceptionSrc));
            }
            DADepartment daDeprtmentRootObj = this.NHSession.QueryOver<DADepartment>()
                                                         .Where(d => d.User.ID == BUser.CurrentUser.ID && d.All).SingleOrDefault();
            Department departmentRoot = new BDepartment().GetDepartmentsTree();
            if (!IsSystemTechnicalAdmin && flow.UnderManagmentList.Count(u => u.Department.ID == departmentRoot.ID) > 0 && daDeprtmentRootObj == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowDepartmentRootNotAccessSystemTechnical, "انتخاب گره سازمان مجاز نمی باشد", ExceptionSrc));
            }
            if (OrganizationUnitList.Count != 0 && OrganizationUnitList[0] != null && OrganizationUnitList.Where(x => x.ID == 1).Count() == 1)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignedOrganizationPostRootAsManagerNotValid, "تخصیص ریشه پست سازمانی به عنوان مدیر مجاز نمی باشد", ExceptionSrc));
            }
            if (flow.UnderManagmentList.Count == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowUnderManagementPersonnelRequired, "پرسنل تحت مدیریت جریان مقدار نگرفته است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        IList<OrganizationUnit> GetOrganizationPersonnelDeactiveOrIsDeleted(IList<OrganizationUnit> OrganizationUnitList)
        {
            IList<OrganizationUnit> organizationUnitList = new List<OrganizationUnit>();
            OrganizationUnit organizationunitAlias = null;
            Person personAlias = null;
            foreach (OrganizationUnit Org in OrganizationUnitList)
            {
                OrganizationUnit organizationList = NHSession.QueryOver(() => organizationunitAlias).Left
                                                           .JoinAlias(() => organizationunitAlias.Person, () => personAlias)
                                                           .Where(() => organizationunitAlias.ID == Org.ID &&
                                                                        (!personAlias.Active || personAlias.IsDeleted)
                                                                 )
                                                           .SingleOrDefault();
                if (organizationList != null)
                    organizationUnitList.Add(Org);
            }
            return organizationUnitList;
        }

        /// <summary>
        /// نام جریان خالی نباشد
        /// نام جریان تکراری نباشد
        /// گروه پیشکارت خالی نباشد
        /// </summary>
        /// <param name="flow"></param>
        protected override void UpdateValidate(Flow flow)
        {
            ValidationException validationException = null;
            UIValidationExceptions exception = new UIValidationExceptions();
            IList<OrganizationUnit> organizationList = null;
            IList<OrganizationUnit> OrganizationUnitList = flow.ManagerFlowList.Where(x => x.Manager.OrganizationUnit != null).Select(x => x.Manager.OrganizationUnit).ToList<OrganizationUnit>();
            //string OrganizationName = string.Empty;
            if (OrganizationUnitList.Count != 0 && OrganizationUnitList[0] != null)
            {
                organizationList = this.GetOrganizationPersonnelDeactiveOrIsDeleted(OrganizationUnitList);
                if (organizationList.Count != 0)
                {
                    foreach (OrganizationUnit org in organizationList)
                    {
                        //if (org.Name == null)
                        //{
                        //    OrganizationName = NHSession.QueryOver<OrganizationUnit>()
                        //                               .Where(x => x.ID == org.ID)
                        //                               .Select(x => x.Name).ToString();
                        //}
                        //else
                        //OrganizationName = org.Name;                                                                                                                                     
                        validationException = new ValidationException(ExceptionResourceKeys.PersonnelAssignedToOrganizationPostIsNotActiveOrDeleted, "پست سازمانی مربوطه به پرسنل غیر فعال یا به پرسنل حذف شده اختصاص داده شده ; وضعیت پست سازمانی یا پرسنل را مشخص کنید :", ExceptionSrc);
                        validationException.Data.Add("Info", org.Name);
                        exception.Add(validationException);
                    }
                    if (exception.Count > 0)
                    {
                        throw exception;
                    }
                }
            }
            if (Utility.IsEmpty(flow.FlowName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRequierd, "نام جریان نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                int count =
                   flowRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Flow().FlowName), flow.FlowName),
                                              new CriteriaStruct(Utility.GetPropertyName(() => new Flow().ID), flow.ID, CriteriaOperation.NotEqual),
                                              new CriteriaStruct(Utility.GetPropertyName(() => new Flow().IsDeleted), false, CriteriaOperation.Equal));
                if (count > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.FlowNameRepeated, "نام جریان نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (flow.AccessGroup == null || flow.AccessGroup.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowAccessGroupRequierd, "گروه پیشکارت نباید خالی باشد", ExceptionSrc));
            }
            if (flow.FlowGroup == null || flow.FlowGroup.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowGroupNameRequired, "گروه جریان نباید خالی باشد", ExceptionSrc));
            }
            DADepartment daDeprtmentRootObj = this.NHSession.QueryOver<DADepartment>()
                                                         .Where(d => d.User.ID == BUser.CurrentUser.ID && d.All).SingleOrDefault();
            Department departmentRoot = new BDepartment().GetDepartmentsTree();
            if (!IsSystemTechnicalAdmin && flow.UnderManagmentList.Count(u => u.Department.ID == departmentRoot.ID) > 0 && daDeprtmentRootObj == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowDepartmentRootNotAccessSystemTechnical, "انتخاب گره سازمان مجاز نمی باشد", ExceptionSrc));
            }
            if (OrganizationUnitList.Count != 0 && OrganizationUnitList[0] != null && OrganizationUnitList.Where(x => x.ID == 1).Count() == 1)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.AssignedOrganizationPostRootAsManagerNotValid, "تخصیص ریشه پست سازمانی به عنوان مدیر مجاز نمی باشد", ExceptionSrc));
            }
            if (flow.UnderManagmentList.Count == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.FlowUnderManagementPersonnelRequired, "پرسنل تحت مدیریت جریان مقدار نگرفته است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(Flow flow)
        {

        }

        protected override void GetReadyBeforeSave(Flow flow, UIActionType action)
        {
            //if (action == UIActionType.DELETE) 
            //{
            //    flow = this.GetByID(flow.ID);
            //    if (!Utility.IsEmpty(flow.OperatorList)) 
            //    {

            //        BOperator busOpr = new BOperator();
            //        foreach (Operator opr in flow.OperatorList)
            //        {
            //            busOpr.SaveChanges(opr, UIActionType.DELETE);
            //        }
            //    }
            //    NHibernateSessionManager.Instance.ClearSession();
            //}
        }

        protected override bool Delete(Flow obj)
        {
            try
            {
                NHibernateSessionManager.Instance.ClearSession();
                Flow flow = flowRep.GetById(obj.ID, false);
                flow.IsDeleted = true;
                base.Update(flow);

                RequestStatusRepositiory reqSt = new RequestStatusRepositiory(false);
                reqSt.DeleteUnConfirmedRequestStates();

                return true;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        protected override void OnSaveChangesSuccess(Flow obj, UIActionType action)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (action == UIActionType.DELETE)
            {
                ManagerRepository managerRepository = new ManagerRepository(false);
                managerRepository.SetManagerFlowActivation(obj.ID);
                managerRepository.SetManagerActivation();

                substituteRep.SetSubstituteActivation();
            }
            else if (action == UIActionType.ADD && !obj.IsForService)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.Flow, obj.ID, BUser.CurrentUser.ID, null, "");
            }

            BSubstitute bSubstitute = new BSubstitute();
            IList<decimal> flowIDsList = new List<decimal>();
            flowIDsList.Add(obj.ID);
            bSubstitute.UpdateSubstitutePrecardAccessByFlow(flowIDsList);
            switch (action)
            {
                case UIActionType.ADD:
                    try
                    {
                        underManagment.InsertUnderManagmentPersons(obj.ID);
                    }
                    catch (Exception)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.UnderManagementPersonnels, "عملیات درج جریان با موفقیت به اتمام رسید , خطا در بازسازی پرسنل تحت مدیریت : پرسنل تحت مدیریت را بازسازی کنید", ExceptionSrc));
                        throw exception;
                    }
                    break;
                case UIActionType.EDIT:
                    underManagment.UpdateUnderManagementPersons(obj.ID);
                    break;
                case UIActionType.DELETE:
                    underManagment.DeleteUnderManagmentPersons(obj.ID);
                    break;
            }
        }
        public void UpdateUnderManagmentPersons(decimal flowId)
        {
            underManagment.UpdateUnderManagementPersons(flowId);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOrganizationWorkFlowLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOrganizationFlow(Flow organizationFlow, UIActionType UAT)
        {
            return base.SaveChanges(organizationFlow, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateManagerFlows_onOrganizationFlowInsert(decimal flowID, bool isActiveFlow, bool isMainFlow, IList<ManagerProxy> ManagerProxyList)
        {
            this.UpdateManagerFlows(flowID, isActiveFlow, isMainFlow, ManagerProxyList);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateManagerFlows_onOrganizationFlowUpdate(decimal flowID, bool isActiveFlow, bool isMainFlow, IList<ManagerProxy> ManagerProxyList)
        {
            this.UpdateManagerFlows(flowID, isActiveFlow, isMainFlow, ManagerProxyList);
        }


    }
}
