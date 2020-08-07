using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Business;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Business.Charts;
using GTS.Clock.Infrastructure;
using NHibernate;
using GTS.Clock.Business.Temp;
using NHibernate.Criterion;


namespace GTS.Clock.Business.RequestFlow
{
    public class BManager : BaseBusiness<Manager>
    {
        IDataAccess accessPort = new BUser();
        const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BManager";
        ManagerRepository managerRep = new ManagerRepository(false);
        FlowRepository flowRep = new FlowRepository(false);
        BFlow bFlow = new BFlow();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private BTemp bTemp = new BTemp();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        #region Master Manager Form

        /// <summary>
        /// تعداد مدیر را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public override int GetRecordCount()
        {
            IList<decimal> accessableIDs = accessPort.GetAccessibleManagers();
            int count = 0;
            if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                count = managerRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Manager().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
            }
            else
            {
                Manager managerAlias = null;
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                count = NHSession.QueryOver(() => managerAlias)
                                                  .JoinAlias(() => managerAlias.TempList, () => tempAlias)
                                                  .Where(() => tempAlias.OperationGUID == operationGUID).RowCount();
                this.bTemp.DeleteTempList(operationGUID);
            }
            return count;
        }

        /// <summary>
        /// مدیرانی که در جریان کاری با دسترسی تعیین شده باشند را برمیگرداند
        /// </summary>
        /// <param name="accessGroupID"></param>
        /// <returns></returns>
        public virtual int GetRecordCountByAccessGroupFilter(decimal accessGroupID)
        {
            try
            {
                int count = 0;
                if (accessGroupID != 0)
                {
                    IList<decimal> ids = accessPort.GetAccessibleManagers();
                    count = managerRep.GetSearchCountByAccessGroupID(accessGroupID, ids.ToArray());
                }
                else
                {
                    count = base.GetRecordCount();
                }
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetRecordCountByAccessGroupFilter");
                throw ex;
            }
        }

        /// <summary>
        /// براساس کلمه کلیدی تعداد نتایج جستجورا برمیگرداند
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public virtual int GetRecordCountBySearch(string searchKey, ManagerSearchFields field)
        {
            try
            {
                int managerCount = 0;
                IList<decimal> ids = accessPort.GetAccessibleManagers();
                switch (field)
                {
                    case ManagerSearchFields.PersonCode:
                        managerCount = managerRep.GetSearchCountByPersonCode(searchKey, ids.ToArray());
                        break;
                    case ManagerSearchFields.PersonName:
                        managerCount = managerRep.GetSearchCountByPersonName(searchKey, ids.ToArray());
                        break;
                    case ManagerSearchFields.OrganizationUnitName:
                        managerCount = managerRep.GetSearchCountByOrganName(searchKey, ids.ToArray());
                        break;
                    case ManagerSearchFields.NotSpecified:
                        managerCount = managerRep.GetSearchCountByQuickSearch(searchKey, ids.ToArray());
                        break;
                }
                return managerCount;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetRecordCountBySearch");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override IList<Manager> GetAllByPage(int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleManagers();
                int count = this.GetRecordCount();
                if (pageSize > 0 && pageIndex <= (int)(count / pageSize))
                {
                    IList<Manager> result = managerRep.GetAllByPage(pageIndex, pageSize, ids.ToArray());
                    return result;
                }
                else
                {
                    throw new OutOfExpectedRangeException("0", Convert.ToString(count - 1), Convert.ToString(pageSize * (pageIndex + 1)), "BManager -> GetAllByPage ");
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetAllByPage");
                throw ex;
            }
        }

        /// <summary>
        ///نتایج فیلتر بر اساس گروه دسترسی را صفحه به صفحه برمیگرداند          
        /// </summary>
        public IList<Manager> SearchByAccessGroup(decimal accessGroupID, int pageIndex, int pageSize, out int managerCount)
        {
            try
            {
                managerCount = 0;
                IList<Manager> managers = new List<Manager>();
                IList<decimal> ids = accessPort.GetAccessibleManagers();
                managers = managerRep.GetSearchByAccessGroupID(accessGroupID, pageSize, pageIndex, ids.ToArray(), out managerCount);
                return managers;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "SearchByAccessGroup");
                throw ex;
            }
        }

        /// <summary>
        ///نتایج جستجو را صفحه به صفحه برمیگرداند          
        /// </summary>
        public IList<Manager> SearchByPage(string searchKey, ManagerSearchFields field, int pageIndex, int pageSize)
        {
            try
            {
                IList<decimal> ids = accessPort.GetAccessibleManagers();
                IList<Manager> managers = new List<Manager>();
                switch (field)
                {
                    case ManagerSearchFields.PersonCode:
                        managers = managerRep.GetSearchByPersonCode(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                    case ManagerSearchFields.PersonName:
                        managers = managerRep.GetSearchByPersonName(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                    case ManagerSearchFields.OrganizationUnitName:
                        managers = managerRep.GetSearchByOrganName(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                    case ManagerSearchFields.NotSpecified:
                        managers = managerRep.GetSearchByQucikSearch(searchKey, pageSize, pageIndex, ids.ToArray());
                        break;
                }
                return managers;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "SearchByPage");
                throw ex;
            }
        }

        /// <summary>
        /// با توجه به نام کاربری اگر مدیری وجود داشته باشد آنرا برمیگرداند
        /// درصورتی که در رکورد مدیر بجای شناسه شخص از پست سازمانی استفاده شده باشد
        /// خصیصه شخص را مقداردهی میکند
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Manager GetManagerByUsername(string username)
        {
            try
            {
                Manager manager = managerRep.IsManager(username);
                if (manager.ID > 0 && manager.Person == null)
                {
                    if (manager.OrganizationUnit == null)
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ManagerOrganizationUnitProblem, "پست سازمانی یا شخص مربوط به پست سازمانی منتسب به مدیر بدرستی مقداردهی نشده است", ExceptionSrc);
                    else if (manager.OrganizationUnit.Person == null)
                        throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ManagerOrganizationUnitProblem, "پست سازمانی یا شخص مربوط به پست سازمانی منتسب به مدیر بدرستی مقداردهی نشده است", ExceptionSrc);
                }
                return manager;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetManagerByUsername");
                throw ex;
            }
        }
        
        /// <summary>
        /// جزئیات شامل سطح دسترسی و غیره را برمیگرداند
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public IList<Flow> GetManagerDetail(decimal managerId)
        {
            return bFlow.GetAllFlowByManager(managerId);
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
                IList<Manager> list = new List<Manager>();
                if (flowID > 0)
                {
                    //IList<decimal> ids = accessPort.GetAccessibleManagers();
                    list = managerRep.GetFlowManagers(flowID);
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetManagerFlow");
                throw ex;
            }
        }

        /// <summary>
        /// لیستی از شناسه مدیران میگیرد و شناسه پرسنل برمیگرداند
        /// </summary>
        /// <param name="managerIds"></param>
        /// <returns></returns>
        public IList<decimal> GetManagerPersons(IList<decimal> managerIds)
        {
            IList<decimal> result = new List<decimal>();
            foreach (decimal id in managerIds)
            {
                Manager mng = this.GetByID(id);
                if (mng != null && mng.Active)
                    result.Add(mng.ThePerson.ID);
            }
            return result;
        }

        /// <summary>
        /// اگر شخص مدیر باشد آنرا برمیگرداند
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Manager GetManager(decimal personId)
        {
            Manager mng = managerRep.GetManagerByPersonID(personId);
            if (mng == null) mng = new Manager();
            return mng;
        }

        #endregion

        #region Manager Detail Form

        public IList<UnderManagment> GetAllUnderManagments(decimal flowId)
        {
            if (flowId > 0)
            {
                return flowRep.GetAllUnderManagments(flowId);
            }
            return new List<UnderManagment>();
        }

        /// <summary>
        /// تمام سطوح دسترسی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<PrecardAccessGroup> GetAllAccessGroups()
        {
            try
            {
                EntityRepository<PrecardAccessGroup> rep = new EntityRepository<PrecardAccessGroup>(false);
                return rep.GetAll();
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetAllAccessGroups");
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
                Charts.BDepartment bdep = new GTS.Clock.Business.Charts.BDepartment();
                Department dep = bdep.GetByID(departmentID);
                return dep.PersonList.Where(p => !p.IsDeleted && p.Active).ToList();
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetDepartmentPerson");
                throw ex;
            }
        }

        /// <summary>
        /// ریشه درخت را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Department GetDepartmentRoot()
        {
            try
            {
                Charts.BDepartment bDep = new GTS.Clock.Business.Charts.BDepartment();
                return bDep.GetDepartmentsTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetDepartmentRoot");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک بخش را برمیگرداند
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal nodeID)
        {
            try
            {
                Charts.BDepartment bDep = new GTS.Clock.Business.Charts.BDepartment();
                return bDep.GetDepartmentChilds(nodeID);
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetDepartmentChilds");
                throw ex;
            }
        }

        /// <summary>
        /// درج جریان کاری
        /// </summary>
        /// <param name="personId">شناسه مدیر اولیه</param> 
        /// <param name="accessGroup">شناسه گروه دسترسی</param>        
        /// <param name="flowName">نام جریان کاری</param>
        /// <param name="underMnagList">افراد و بخشهای تحت مدیریت</param>
        /// <returns></returns>
        public decimal InsertFlowByPerson(decimal personId, decimal accessGroup, string flowName, IList<UnderManagment> underMnagList, decimal groupID)
        {
            Flow flow = new Flow();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {                    
                    UIValidationExceptions exception = new UIValidationExceptions();
                    Person personAllias = null;
                    IList<Person> personList = NHSession.QueryOver(() => personAllias)
                                                         .Where(() => personAllias.ID == personId &&
                                                                      !personAllias.Active
                                                               )
                                                         .List<Person>();                                    
                        foreach (Person person in personList)
                        {                                                      
                            exception.Add(new ValidationException(ExceptionResourceKeys.SelectedPersonnelIsDeActive, "پرسنل انتخاب شده غیر فعال شده است ", ExceptionSrc));
                            throw exception;
                        }                                                              
                    flow.FlowName = flowName;
                    flow.AccessGroup = new PrecardAccessGroup() { ID = accessGroup };
                    flow.FlowGroup = new FlowGroup() { ID = groupID };
                    flow.ActiveFlow = true;
                    flow.MainFlow = true;
                    flow.ManagerFlowList = new List<ManagerFlow>();
                    flow.UnderManagmentList = new List<UnderManagment>();

                    Manager mng = managerRep.GetManagerByPersonID(personId , new object[] {false});
                    if (mng == null || mng.ID == 0)
                    {
                        Manager manager = new Manager();
                        manager.Person = new Person() { ID = personId };
                        manager.Active = true;
                        this.SaveChanges(manager, UIActionType.ADD);
                        mng = manager;
                    }

                    ManagerFlow managerFlow = new ManagerFlow();
                    managerFlow.Active = true;
                    managerFlow.Flow = flow;
                    managerFlow.Manager = mng;
                    managerFlow.Level = 1;
                    flow.ManagerFlowList.Add(managerFlow);

                    foreach (UnderManagment un in underMnagList)
                    {
                        un.Flow = flow;
                        flow.UnderManagmentList.Add(un);
                    }


                    BFlow bflow = new BFlow();
                    bflow.SaveChanges(flow, UIActionType.ADD);
                    return flow.ID;

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return flow.ID;
                }

                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BManager", "InsertFlowByPerson");

                    throw ex;
                }
            }
        }

        /// <summary>
        /// درج جریان کاری
        /// </summary>
        /// <param name="personId">شناسه پست سلزمانی مدیر اولیه</param>
        /// <param name="accessGroup">شناسه گروه دسترسی</param>
        /// <param name="flowName">نام جریان کاری</param>        
        /// <param name="underMnagList">افراد و بخشهای تحت مدیریت</param>
        /// <returns></returns>
        public decimal InsertFlowByOrganization(decimal organizationId, decimal accessGroup, string flowName, IList<UnderManagment> underMnagList, decimal groupID, bool isForTraffic = false)
        {
            Flow flow = new Flow();
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    OrganizationUnit organizationUnitAlias = null;
                    Person personAllias = null;
                    IList<OrganizationUnit> organizationUnitList = NHSession.QueryOver(() => organizationUnitAlias).Left
                                                                           .JoinAlias(() => organizationUnitAlias.Person, () => personAllias)
                                                                           .Where(() => organizationUnitAlias.ID == organizationId &&
                                                                                        (!personAllias.Active ||
                                                                                         personAllias.IsDeleted)
                                                                                  )
                                                                           .List<OrganizationUnit>();                   
                        foreach (OrganizationUnit org in organizationUnitList)
                        {                            
                            exception.Add(new ValidationException(ExceptionResourceKeys.PersonnelAssignedToOrganizationPostIsNotActiveOrDeleted, "پست سازمانی مربوطه به پرسنل غیر فعال یابه پرسنل حذف شده اختصاص داده شده ; وضعیت پست سازمانی یا پرسنل را مشخص کنید :", ExceptionSrc));
                            throw exception;
                        }                                           
                    flow.FlowName = flowName;
                    flow.FlowGroup = new FlowGroup() { ID = groupID };
                    flow.AccessGroup = new PrecardAccessGroup() { ID = accessGroup };
                    flow.ActiveFlow = true;
                    flow.MainFlow = true;
                    flow.ManagerFlowList = new List<ManagerFlow>();
                    flow.UnderManagmentList = new List<UnderManagment>();
                    flow.IsForService = isForTraffic;
                    Manager mng = managerRep.GetManagerByOrganID(organizationId);
                    if (mng == null || mng.ID == 0)
                    {
                        Manager manager = new Manager();
                        manager.OrganizationUnit = new OrganizationUnit() { ID = organizationId };
                        manager.Active = true;
                        this.SaveChanges(manager, UIActionType.ADD);
                        mng = manager;
                    }

                    ManagerFlow managerFlow = new ManagerFlow();
                    managerFlow.Flow = flow;
                    managerFlow.Manager = mng;
                    managerFlow.Level = 1;
                    managerFlow.Active = true;
                    flow.ManagerFlowList.Add(managerFlow);

                    foreach (UnderManagment un in underMnagList)
                    {
                        un.Flow = flow;
                        flow.UnderManagmentList.Add(un);
                    }


                    BFlow bflow = new BFlow();
                    bflow.SaveChanges(flow, UIActionType.ADD);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return flow.ID;
                }

                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BManager", "InsertFlowByOrganization");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// بروزرسانی یک جریان
        /// </summary>
        /// <param name="flowID">شناسه جریان</param>
        /// <param name="accessGroupId">گروه دسترسی</param>
        /// <param name="flowName">نام جریان</param>
        /// <param name="underMnagList">لیست افراد تحت مدیریت</param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public decimal UpdateFlow(decimal flowID, decimal accessGroupId, string flowName, IList<UnderManagment> underMnagList, decimal groupID)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                BFlow bflow = new BFlow();
                Flow flow = bflow.GetByID(flowID);
                flow.ID = flowID;
                flow.FlowName = flowName;
                flow.AccessGroup = new PrecardAccessGroup() { ID = accessGroupId };
                flow.FlowGroup = new FlowGroup() { ID = groupID };
                flow.UnderManagmentList = new List<UnderManagment>();
                foreach (UnderManagment un in underMnagList)
                {
                    un.Flow = flow;
                    flow.UnderManagmentList.Add(un);
                }
                UnderManagmentRepository underRep = new UnderManagmentRepository(false);
                if (underMnagList == null || underMnagList.Count == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.FlowUnderManagementPersonnelRequired, "پرسنل تحت مدیریت جریان مقدار نگرفته است", ExceptionSrc));  
                }
                if (exception.Count > 0)
                {
                    throw exception;
                }
                underRep.DeleteUnderManagments(flow.ID);
                bflow.SaveChanges(flow, UIActionType.EDIT);
                return flow.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "UpdateFlow");
                throw ex;
            }
        }

        /// <summary>
        /// یک جریان کاری را حذف میکند
        /// </summary>
        /// <param name="flowID"></param>
        public void DeleteFlow(decimal flowID)
        {
            Flow flow = bFlow.GetByID(flowID);
            flow.IsDeleted = true;
            bFlow.SaveChanges(flow, UIActionType.EDIT);
        }

        /// <summary>
        /// یک مدیر را حذف میکند
        /// </summary>
        /// <param name="managerID"></param>
        public void DeleteManager(decimal managerID)
        {
            Manager manager = new Manager() { ID = managerID };
            this.SaveChanges(manager, UIActionType.DELETE);
        }
        #endregion

        #region Manager Flow From

        #region Manager Search
        public int QuickSearchPersonCount(string searchKey)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(searchKey);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchPersonCount");
                throw ex;
            }
        }

        /// <summary>
        /// در بین پرسنلی جستجوی سریع انجام میدهد
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<Person> QuickSearchPersonByPage(string searchKey, int pageIndex, int pageSize)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearchByPage(pageIndex, pageSize, searchKey);
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchPersonByPage");
                throw ex;
            }
        }

        public OrganizationUnit GetOrganizationUnitTree()
        {
            try
            {
                Charts.BOrganizationUnit borgan = new GTS.Clock.Business.Charts.BOrganizationUnit();
                return borgan.GetOrganizationUnitTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetOrganizationUnitTree");
                throw ex;
            }
        }

        public IList<OrganizationUnit> GetOrganizationUnitChilds(decimal parentId)
        {
            try
            {
                Charts.BOrganizationUnit borgan = new GTS.Clock.Business.Charts.BOrganizationUnit();
                return borgan.GetChilds(parentId);
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetOrganizationUnitChilds");
                throw ex;
            }
        }

        /// <summary>
        /// پستهای سازمانی را با نام جستجو میکند
        /// ریشه نباید جزو مجموعه جواب باشد
        /// </summary>
        /// <param name="organName"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> QuickSearchByOrganizationUnitName(string organName)
        {
            try
            {
                IList<OrganizationUnit> list = new BOrganizationUnit().SearchByUnitName(organName);
                foreach (OrganizationUnit organ in list)
                {
                    if (organ.Person == null)
                        organ.Person = new Person();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchByOrganizationUnitName");
                throw ex;
            }
        }

        #endregion



        #endregion

        /// <summary>
        /// - شخص یا پست باید انتحاب شود
        /// </summary>
        /// <param name="manager"></param>
        protected override void InsertValidate(Manager manager)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if ((manager.Person == null || manager.Person.ID == 0)
                &&
                (manager.OrganizationUnit == null || manager.OrganizationUnit.ID == 0))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ManagerOwnerNotSpecified, "برای مدیر شخص یا پست سازمانی مشخص نشده است", ExceptionSrc));
            }
           
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(Manager obj)
        {

        }

        /// <summary>
        /// - نباید مدیر در جریان کار استفاده شده باشد
        /// </summary>
        /// <param name="manager"></param>
        protected override void DeleteValidate(Manager manager)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            EntityRepository<ManagerFlow> rep = new EntityRepository<ManagerFlow>(false);
            int count = rep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ManagerFlow().Manager), manager));
            if (count > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ManagerUsedByFlow, "مدیر در سلسله مراتب جریان کار استفاده شده است و امکان حذف آن نیست", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void Insert(Manager manager)
        {
            managerRep.WithoutTransactSave(manager);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertFlowByManagerCreator(ManagerCreator MC, decimal targetManagerCreatorID, decimal accessGroupID, string flowName, IList<UnderManagment> UnderManagmentList, decimal groupID)
        {
            decimal flowID = 0;
            switch (MC)
            {
                case ManagerCreator.Personnel:
                    flowID = this.InsertFlowByPerson(targetManagerCreatorID, accessGroupID, flowName, UnderManagmentList, groupID);
                    break;
                case ManagerCreator.OrganizationPost:
                    flowID = this.InsertFlowByOrganization(targetManagerCreatorID, accessGroupID, flowName, UnderManagmentList, groupID);
                    break;
                case ManagerCreator.None:
                    break;
            }
            return flowID;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckManagesLoadAccess()
        {
        }

        //public void UpdateManager_OnAfterPersonUpdate(Person person)
        //{
        //    try
        //    {
        //        using (NHibernateSessionManager.Instance.BeginTransactionOn())
        //        {
        //            this.UpdateManagerBasedonOrganization(person);
        //            this.UpdateManagerBasedonPerson(person);
        //            NHibernateSessionManager.Instance.CommitTransactionOn();
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        NHibernateSessionManager.Instance.RollbackTransactionOn();
        //        LogException(ex, "BManager", "UpdateManager_OnAfterPersonUpdate");
        //        throw ex;
        //    }
        //}
        /// <summary>
        ///      
        /// در این تابع پست  , پرسنلی که ویرایش کردیم را در جدول منیجر جستجو میکنیم , اگر پرسنل این پست ویزایش شده باشد ,پرسنل جدید رابه جدول منیجر
        ///  اختصاص میدهیم
        /// درواقع پست مورد نظر را  در جدول منیجر جستجو میکنیم , اگر پرسنل این پست ویرایش شده باشد,پرسنل ویرایش شده را در جدول منیجر جایگزین میکنیم.
        /// </summary>
        /// <param name="person"></param>
        private void UpdateManagerBasedonOrganization(Person person)
        {
            try
            {
                IList<Manager> managerlist = new List<Manager>();

                if (person.OrganizationUnit != null)
                {
                    managerlist = NHSession.QueryOver<Manager>()
                                           .Where(current => current.Active && current.OrganizationUnit != null && current.Person != null && current.OrganizationUnit.ID == person.OrganizationUnit.ID && current.Person.ID != person.ID)
                                           .List<Manager>();
                    if (managerlist.Count > 0)
                    {
                        foreach (Manager manager in managerlist)
                        {
                            manager.Person = person;
                            managerRep.WithoutTransactSave(manager);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "UpdateManagerBasedonOrganization");
                throw ex;
            }
        }
        /// <summary>
        /// در جدول منیجر پرسنل ویرایش شده را جستجو میکنیم , اگر پست سازمانی آن ویرایش شده ,پست سازمانی جدید را جایگزین میکنیم.
        /// </summary>
        /// <param name="person"></param>
        private void UpdateManagerBasedonPerson(Person person)
        {
            IList<Manager> managerlist = null;
            try
            {
                managerlist = new List<Manager>();
                if (person.OrganizationUnit != null)
                {
                    managerlist = NHSession.QueryOver<Manager>()
                                           .Where(current => current.Person.ID == person.ID && current.Active && current.OrganizationUnit.ID != person.OrganizationUnit.ID)
                                           .List<Manager>();
                    if (managerlist.Count > 0)
                    {
                        foreach (Manager manager in managerlist)
                        {
                            manager.OrganizationUnit = person.OrganizationUnit;
                            managerRep.WithoutTransactSave(manager);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "UpdateManagerBasedonPerson");
                throw ex;
            }
        }

        public bool CheckPersonUsedInFlowAsManager(Person person)
        {
            bool isManager = false;
            IList<Flow> flowList = new List<Flow>();
            Person personAlias = null;
            OrganizationUnit organizationUnitAlias = null;
            Manager managerAlias = null;
            Flow flowAlias = null;
            ManagerFlow managerFlowAlias = null;
            try
            {
                flowList = this.NHSession.QueryOver<Flow>(() => flowAlias)
                                         .JoinAlias(() => flowAlias.ManagerFlowList, () => managerFlowAlias)
                                         .JoinAlias(() => managerFlowAlias.Manager, () => managerAlias)
                                         .JoinAlias(() => managerAlias.Person, () => personAlias)
                                         .Where(() => !flowAlias.IsDeleted &&
                                                       managerFlowAlias.Active &&
                                                       managerAlias.Active &&
                                                       personAlias.ID == person.ID
                                               )
                                         .List<Flow>();
                if (flowList.Count > 0)
                    isManager = true;
                else
                {
                    if (person.OldOrganizationUnitId != 0)
                    {
                        flowList = this.NHSession.QueryOver<Flow>(() => flowAlias)
                                                    .JoinAlias(() => flowAlias.ManagerFlowList, () => managerFlowAlias)
                                                    .JoinAlias(() => managerFlowAlias.Manager, () => managerAlias)
                                                    .JoinAlias(() => managerAlias.OrganizationUnit, () => organizationUnitAlias)
                                                    .Where(() => !flowAlias.IsDeleted &&
                                                                  managerFlowAlias.Active &&
                                                                  managerAlias.Active &&
                                                                  organizationUnitAlias.ID == person.OldOrganizationUnitId
                                                          )
                                                    .List<Flow>();
                        if (flowList.Count > 0)
                            isManager = true;
                    }
                }
                return isManager;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "CheckPersonUsedInFlowAsManager");
                throw ex;
            }
        }

        public bool CheckOrganizationUnitUsedInFlowAsManager(OrganizationUnit organizationUnit, bool IsCheckChild)
        {
            bool isManager = false;
            IList<Flow> flowList = new List<Flow>();
            OrganizationUnit organizationUnitAlias = null;
            Manager managerAlias = null;
            Flow flowAlias = null;
            ManagerFlow managerFlowAlias = null;
            GTS.Clock.Model.Temp.Temp tempAlias = null;
            string operationGUID = string.Empty;
            try
            {
               if (organizationUnit != null)
               {                                                                                   
                   flowList = this.NHSession.QueryOver<Flow>(() => flowAlias)
                                            .JoinAlias(() => flowAlias.ManagerFlowList, () => managerFlowAlias)
                                            .JoinAlias(() => managerFlowAlias.Manager, () => managerAlias)
                                            .JoinAlias(() => managerAlias.OrganizationUnit, () => organizationUnitAlias)
                                            .Where(() => !flowAlias.IsDeleted &&
                                                          managerFlowAlias.Active &&
                                                          managerAlias.Active &&
                                                          organizationUnitAlias.ID == organizationUnit.ID
                                                  )
                                            .List<Flow>();
                   if (flowList.Count > 0)
                       isManager = true;
                   else
                   {
                       if (IsCheckChild)
                       {
                           IList<decimal> childOrganizationUnitIdsList = this.NHSession.QueryOver<OrganizationUnit>()
                                                                               .Where(x => x.ParentPath.IsInsensitiveLike("," + organizationUnit.ID + ",", MatchMode.Anywhere))
                                                                               .Select(x => x.ID)
                                                                               .List<decimal>();
                           operationGUID = this.bTemp.InsertTempList(childOrganizationUnitIdsList);
                           flowList = this.NHSession.QueryOver<Flow>(() => flowAlias)
                                                    .JoinAlias(() => flowAlias.ManagerFlowList, () => managerFlowAlias)
                                                    .JoinAlias(() => managerFlowAlias.Manager, () => managerAlias)
                                                    .JoinAlias(() => managerAlias.OrganizationUnit, () => organizationUnitAlias)
                                                    .JoinAlias(() => organizationUnitAlias.TempList, () => tempAlias)
                                                    .Where(() => !flowAlias.IsDeleted &&
                                                                  managerFlowAlias.Active &&
                                                                  managerAlias.Active &&
                                                                  tempAlias.OperationGUID == operationGUID
                                                          )
                                                    .List<Flow>();
                           this.bTemp.DeleteTempList(operationGUID);
                           if (flowList.Count > 0)
                               isManager = true;                           
                       }
                   }
                }
                return isManager;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "CheckOrganizationUnitUsedInFlowAsManager");
                throw ex;
            }
        }

        public IList<Manager> GetAllManagers(string searchValue, bool matchWholeWord)
        {
            try
            {
                MatchMode matchMode = MatchMode.Anywhere;
                if (matchWholeWord)
                    matchMode = MatchMode.Exact;
                IList<decimal> accessableIDs = accessPort.GetAccessibleManagers();
                IList<Manager> ManagerList = new List<Manager>();
                Manager managerAlias = null;
                Manager manageralias = null;
                Person personAlias = null;
                Person orgPersonAlias = null;
                OrganizationUnit organizationAlias = null;
                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    ManagerList = NHSession.QueryOver(() => managerAlias).Left
                                       .JoinAlias(() => managerAlias.Person, () => personAlias).Left
                                       .JoinAlias(() => managerAlias.OrganizationUnit, () => organizationAlias).Left
                                       .JoinAlias(() => organizationAlias.Person, () => orgPersonAlias)
                                       .Where(() => managerAlias.Active &&
                                                   (personAlias.FirstName.IsInsensitiveLike(searchValue, matchMode) ||
                                                    personAlias.LastName.IsInsensitiveLike(searchValue, matchMode) ||
                                                    personAlias.BarCode.IsInsensitiveLike(searchValue, matchMode) ||                                                   
                                                    organizationAlias.Name.IsInsensitiveLike(searchValue, matchMode) ||
                                                    organizationAlias.CustomCode.IsInsensitiveLike(searchValue, matchMode) ||
                                                    orgPersonAlias.BarCode.IsInsensitiveLike(searchValue, matchMode) ||
                                                    orgPersonAlias.FirstName.IsInsensitiveLike(searchValue , matchMode) ||
                                                    orgPersonAlias.LastName.IsInsensitiveLike(searchValue, matchMode)                                                    
                                                   )
                                              )
                                       .List<Manager>();
                    if (searchValue != string.Empty)
                    {
                        IList<decimal> organizationIds = NHSession.QueryOver<OrganizationUnit>()
                                                                  .Select(x => x.ID)
                                                                  .Where(x => x.Name.IsInsensitiveLike(searchValue, matchMode) ||
                                                                              x.CustomCode.IsInsensitiveLike(searchValue, matchMode)
                                                                        )
                                                                  .List<decimal>();
                        foreach (decimal orgId in organizationIds)
                        {
                            IList<Manager> managerList = NHSession.QueryOver(() => manageralias)
                                                               .JoinAlias(() => manageralias.OrganizationUnit, () => organizationAlias)
                                                               .Where(() => manageralias.Active &&
                                                                            (organizationAlias.ParentPath.IsInsensitiveLike(string.Format(",{0},", orgId), MatchMode.Anywhere) ||
                                                                             organizationAlias.ID == orgId
                                                                            )
                                                                     )
                                                               .List<Manager>();

                            managerList = managerList.Where(x => !ManagerList.Select(y => y.ID).ToList().Contains(x.ID)).ToList();
                            foreach (Manager managerItem in managerList)
                            {
                                ManagerList.Add(managerItem);
                            }
                        }
                    }

                }
                else
                {
                    GTS.Clock.Model.Temp.Temp tempAlias = null;
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    ManagerList = NHSession.QueryOver(() => managerAlias).Left
                                       .JoinAlias(() => managerAlias.Person, () => personAlias).Left
                                       .JoinAlias(() => managerAlias.OrganizationUnit, () => organizationAlias).Left
                                       .JoinAlias(() => organizationAlias.Person, () => orgPersonAlias)
                                       .JoinAlias(() => managerAlias.TempList, () => tempAlias)
                                       .Where(() => tempAlias.OperationGUID == operationGUID &&
                                                    managerAlias.Active &&
                                                   (personAlias.FirstName.IsInsensitiveLike(searchValue, matchMode) ||
                                                    personAlias.LastName.IsInsensitiveLike(searchValue, matchMode) ||
                                                    personAlias.BarCode.IsInsensitiveLike(searchValue, matchMode) ||
                                                    organizationAlias.Name.IsInsensitiveLike(searchValue, matchMode) ||
                                                    organizationAlias.CustomCode.IsInsensitiveLike(searchValue, matchMode) ||
                                                    orgPersonAlias.BarCode.IsInsensitiveLike(searchValue, matchMode) ||
                                                    orgPersonAlias.FirstName.IsInsensitiveLike(searchValue, matchMode) ||
                                                    orgPersonAlias.LastName.IsInsensitiveLike(searchValue, matchMode)        
                                                   )
                                              )
                                       .List<Manager>();
                    this.bTemp.DeleteTempList(operationGUID);
                    if (searchValue != string.Empty)
                    {
                        IList<decimal> organizationIds = NHSession.QueryOver<OrganizationUnit>()
                                                                   .Select(x => x.ID)
                                                                  .Where(x => x.Name.IsInsensitiveLike(searchValue, matchMode) ||
                                                                              x.CustomCode.IsInsensitiveLike(searchValue, matchMode)
                                                                         )
                                                                  .List<decimal>();
                        foreach (decimal orgId in organizationIds)
                        {
                            IList<Manager> managerList = NHSession.QueryOver(() => manageralias)
                                                               .JoinAlias(() => manageralias.OrganizationUnit, () => organizationAlias)
                                                               .Where(() => manageralias.Active &&
                                                                           (organizationAlias.ParentPath.IsInsensitiveLike(string.Format(",{0},", orgId), MatchMode.Anywhere) ||
                                                                           organizationAlias.ID == orgId)
                                                                     )
                                                               .List<Manager>();
                            managerList = managerList.Where(x => !ManagerList.Select(y => y.ID).ToList().Contains(x.ID)).ToList();
                            foreach (Manager managerItem in managerList)
                            {
                                ManagerList.Add(managerItem);
                            }
                        }
                    }
                }
                return ManagerList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetAllManagers");
                throw ex;
            }
        }
        public IList<Manager> GetSubstituteManagers(decimal managerId)
        {
            try
            {
                Manager managerAlias = null;
                Person personAlias = null;
                OrganizationUnit OrganizationAlias = null;
                IList<Manager> ManagerList = NHSession.QueryOver(() => managerAlias).Left
                                                       .JoinAlias(() => managerAlias.Person, () => personAlias).Left
                                                       .JoinAlias(() => managerAlias.OrganizationUnit, () => OrganizationAlias)
                                                        .Where(() => managerAlias.ID == managerId &&
                                                                      managerAlias.Active
                                                               ).List<Manager>();
                return ManagerList;
            }
            catch (Exception ex)
            {
                LogException(ex, "", "");
                throw ex;
            }
        }

    }
}
