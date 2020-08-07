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
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Assignments;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Security;
using NHibernate;
using NHibernate.Criterion;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Contracts;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// مشخصات سازمانی
    /// </summary>
    public class BChangeOrganicInfo : MarshalByRefObject
    {
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BChangeOrganicInfo";
        private EntityRepository<ApplicationLanguageSettings> objectRep = new EntityRepository<ApplicationLanguageSettings>();
        ISearchPerson searchTool = new BPerson();
        BAssignDateRange bussDateRange = new BAssignDateRange(BLanguage.CurrentSystemLanguage);
        BAssignRule busRule = new BAssignRule(BLanguage.CurrentSystemLanguage);
        BPersonContractAssignment busAssignContract = new BPersonContractAssignment(BLanguage.CurrentSystemLanguage);
        BAssignWorkGroup busWorkGroup = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage);
        PersonRepository prsRepository = new PersonRepository(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);

        #region Fill Controls

        /// <summary>
        /// ریشه اصلی چارت سازمانی را بر می گرداند
        /// </summary>
        /// <returns>ریشه چارت</returns>
        public Department GetDepartmentRoot()
        {
            return searchTool.GetDepartmentRoot();
        }

        /// <summary>
        /// زیر گره های یک گره والد از چارت سازمانی را بر می گرداند
        /// </summary>
        /// <param name="parentId">کلید گره والد</param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChild(decimal parentId)
        {
            return searchTool.GetDepartmentChild(parentId);
        }

        /// <summary>
        /// کلیه نوع استخدام ها را بر می گرداند
        /// </summary>
        /// <returns>لیست نوع استخدام</returns>
        public IList<EmploymentType> GetAllEmploymentTypes()
        {
            return searchTool.GetAllEmploymentTypes();
        }
        
        /// <summary>
        /// تمام گروه کاری ها رو بر می گرداند
        /// </summary>
        /// <returns>لیست گروه کاری ها</returns>
        public IList<WorkGroup> GetAllWorkGroup()
        {
            return searchTool.GetAllWorkGroup();
        }

        /// <summary>
        /// تمام گروه قوانین را بر می گرداند
        /// </summary>
        /// <returns>لیست گروه قوانین</returns>
        public IList<RuleCategory> GetAllRuleGroup()
        {
            return searchTool.GetAllRuleGroup();
        }
        public IList<Contract> GetAllContract()
        {
            return searchTool.GetAllContract();
        }
        /// <summary>
        /// کلیه دوره های محاسبات را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public IList<CalculationRangeGroup> GetAllDateRanges()
        {
            return searchTool.GetAllDateRanges();
        }
        #endregion

        /// <summary>
        /// تغییر مشخصات سازمانی
        /// </summary>
        /// <param name="proxy">پروکسی جستجوی پیشرفته پرسنل</param>
        /// <param name="infoProxy">پروکسی مشخصات</param>
        /// <param name="errorList">خروجی لیست خطاها</param>
        /// <returns>وضعیت انجام عملیات</returns>
        public bool ChangeInfo(PersonAdvanceSearchProxy proxy, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            int count = searchTool.GetPersonInAdvanceSearchCount(proxy);
            IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count);

            bool result = this.ChangeInfo(list, infoProxy, out errorList);

            foreach (ChangeInfoErrorProxy error in errorList)
            {
                BaseBusiness<Entity>.LogException(new Exception(error.ToString()));
            }
                
            
            if (result)
            {
                this.UpdateUnderManagement(list, infoProxy);
            }
            return result;
        }
        
        /// <summary>
        /// تغییر مشخصات سازمانی پرسنل تحت مدیریت
        /// </summary>
        /// <param name="personList">لیست پرسنل</param>
        /// <param name="infoProxy">پروکسی مشخصات سازمانی</param>
        private void UpdateUnderManagement(IList<Person> personList, OrganicInfoProxy infoProxy)
        {
            try
            {
                if (infoProxy.DepartmentID != 0)
                {
                    IQuery query = null;
                    List<decimal> personIdList = new List<decimal>();
                    string operationGUID = string.Empty;
                    string SQLCommand = string.Empty;
                    operationGUID = string.Empty;
                    foreach (Person person in personList)
                    {
                        personIdList.Add(person.ID);
                    }
                    if (personList.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                    {
                        SQLCommand = @"Update TA_UnderManagment SET underMng_DepartmentID =:DepartmentId  WHERE  underMng_PersonID in (:PersonList)";
                        query = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand);
                        query.SetParameter("DepartmentId", infoProxy.DepartmentID);
                        query.SetParameterList("PersonList", personIdList);
                        query.ExecuteUpdate();
                    }
                    else
                    {
                        TempRepository temprepository = new TempRepository(false);
                        operationGUID = temprepository.InsertTempList(personIdList);
                        //                    SQLCommand = @"Update TA_UnderManagment SET underMng_DepartmentID =:DepartmentId  WHERE  underMng_PersonId in 
                        //                    (select prs_Id from TA_Person Inner join TA_Temp on prs_Id = temp_ObjectID WHERE temp_OperationGUID =:operationGUID)";
                        SQLCommand = @"Update underMng SET underMng_DepartmentID =:DepartmentId from TA_UnderManagment underMng
                                   inner join TA_Person person on underMng.underMng_PersonId = person.prs_Id
                                   inner join TA_Temp temp on person.prs_Id = temp.temp_ObjectID and temp.temp_OperationGUID =:operationGUID";
                        query = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SQLCommand);
                        query.SetParameter("DepartmentId", infoProxy.DepartmentID);
                        query.SetParameter("operationGUID", operationGUID);
                        query.ExecuteUpdate();
                        temprepository.DeleteTempList(operationGUID);
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BChangeOrganicInfo", "UpdateUnderManagement");
                throw ex;
            }
        }

        /// <summary>
        /// تغییر مشخصات سازمانی
        /// </summary>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="infoProxy">پروکسی مشخصات سازمانی</param>
        /// <param name="errorList">خروجی لیست خطاها</param>
        /// <returns>وضعیت انجام عملیات</returns>
        public bool ChangeInfo(string searchKey, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            int count = searchTool.GetPersonInQuickSearchCount(searchKey);
            IList<Person> list = searchTool.QuickSearchByPage(0, count, searchKey);

            bool result = this.ChangeInfo(list, infoProxy, out errorList);
            if (!result)
            {
                foreach (ChangeInfoErrorProxy error in errorList)
                {
                    BaseBusiness<Entity>.LogException(new Exception(error.ToString()));
                }
                
            }
            return result;
        }

        /// <summary>
        /// تغییر مشخصات سازمانی 
        /// </summary>
        /// <param name="personId">کلید پرسنل</param>
        /// <param name="infoProxy">پروکسی مشخصات سازمانی</param>
        /// <param name="errorList">خروجی لیست خطاها</param>
        /// <returns>وضعیت انجام عملیات</returns>
        public bool ChangeInfo(decimal personId, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            //Person prs = new BPerson().GetByID(personId);
            //NHibernateSessionManager.Instance.ClearSession();
            //Person prs = new Person() { ID = personId, PersonTASpec = (new BPerson()).GetPersonTASpecByID(personId)};
            PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy();
            proxy.PersonId = personId;
            //IList<Person> list = new List<Person>();
            //list.Add(prs);
            bool result = this.ChangeInfo(proxy, infoProxy, out errorList);
            //if (!result)
            //    foreach (ChangeInfoErrorProxy error in errorList)
            //    {
            //        BaseBusiness<Entity>.LogException(new Exception(error.ToString()));
            //    }
            return result;
        }

        /// <summary>
        /// تغییر مشخصات سازمانی  
        /// </summary>
        /// <param name="personList">لیست پرسنل</param>
        /// <param name="infoProxy">پروکسی مشخصات سازمانی</param>
        /// <param name="errorList">خروجی لیست خطاها</param>
        /// <returns>وضعیت انجام عملیات</returns>
        private bool ChangeInfo(IList<Person> personList, OrganicInfoProxy infoProxy, out IList<ChangeInfoErrorProxy> errorList)
        {
            IList<PersonRuleCatAssignment> ruleAssgnList = new List<PersonRuleCatAssignment>();
            IList<PersonRangeAssignment> rangeAssgnList = new List<PersonRangeAssignment>();
            IList<AssignWorkGroup> workGroupAssgnList = new List<AssignWorkGroup>();
            IList<PersonContractAssignment> contractAssgnList = new List<PersonContractAssignment>();


            #region Validate
            DateTime workGroupFromDate, ruleGroupFromDate, ruleGroupToDate, DateRangeFromDate,contractFromDate,contractToDate;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                workGroupFromDate = Utility.ToMildiDate(infoProxy.WorkGroupFromDate);
                ruleGroupFromDate = Utility.ToMildiDate(infoProxy.RuleGroupFromDate);
                ruleGroupToDate = Utility.ToMildiDate(infoProxy.RuleGroupToDate);
                DateRangeFromDate = Utility.ToMildiDate(infoProxy.DateRangeFromDate);
                contractFromDate = Utility.ToMildiDate(infoProxy.ContractFromDate);
                contractToDate = Utility.ToMildiDate(infoProxy.ContractToDate);
            }
            else
            {
                workGroupFromDate = Utility.ToMildiDateTime(infoProxy.WorkGroupFromDate);
                ruleGroupFromDate = Utility.ToMildiDateTime(infoProxy.RuleGroupFromDate);
                ruleGroupToDate = Utility.ToMildiDateTime(infoProxy.RuleGroupToDate);
                DateRangeFromDate = Utility.ToMildiDateTime(infoProxy.DateRangeFromDate);
                contractFromDate = Utility.ToMildiDateTime(infoProxy.ContractFromDate);
                contractToDate = Utility.ToMildiDateTime(infoProxy.ContractToDate);
            }


            errorList = new List<ChangeInfoErrorProxy>();
            foreach (Person prs in personList)
            {
                string errorMessage = "";
                if (infoProxy.WorkGroupID > 0)
                {
                    workGroupAssgnList = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage).GetAllByWorkGroupId(infoProxy.WorkGroupID);
                    ValidateWorkGroupAssignment(prs, workGroupFromDate, ref errorMessage);
                }
                if (infoProxy.RuleGroupID > 0)
                {
                    ruleAssgnList = new BAssignRule(BLanguage.CurrentSystemLanguage).GetAllByRuleGroupId(infoProxy.RuleGroupID);
                    ValidateRuleGroupAssignment(prs, ruleGroupFromDate, ruleGroupToDate, ref errorMessage);
                }
                if (infoProxy.DateRangeID > 0)
                {
                    rangeAssgnList = new BAssignDateRange(BLanguage.CurrentSystemLanguage).GetAllByRangeId(infoProxy.DateRangeID);
                    ValidateDateRangeAssignment(prs, DateRangeFromDate, ref errorMessage);
                }
                if (infoProxy.ContractID > 0)
                {
                    contractAssgnList = new BPersonContractAssignment(BLanguage.CurrentSystemLanguage).GetAllByContractId(infoProxy.ContractID);
                    ValidateContractAssignment(prs, contractFromDate, contractToDate, ref errorMessage);
                }
                if (!Utility.IsEmpty(errorMessage))
                {
                    errorList.Add(new ChangeInfoErrorProxy() { ErrorMessage = errorMessage, PersonCode = prs.PersonCode, PersonName = prs.Name });
                    if (errorList.Count > 50)
                        break;
                }
                
            }
            if (errorList.Count > 0)
            {
                return false;
            }
            #endregion

            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    UnderManagmentRepository underMngRep = new UnderManagmentRepository(false);
                    List<decimal> personIdList = new List<decimal>();
                    IList<OrganizationFeatureProxy> organizationFeatureList = new List<OrganizationFeatureProxy>();                   
                    IList<FlowPersonsProxy> flowPersonList = new List<FlowPersonsProxy>();
                    List<decimal> PersonnelsDepartmentChange = new List<decimal>();
                    bool cfpUpdateRequierd = false;
                    DateTime minDate = DateTime.Now;
                    int counter = 0;
                    foreach (Person prs in personList)
                    {
                        counter++;
                        bool updatePrs = false;
                        personIdList.Add(prs.ID);
                        IList<OrganizationFeatureProxy> organizationFeatureExist = new List<OrganizationFeatureProxy>();
                        UnderManagment underMng = new UnderManagment();

                        #region Department
                        if (infoProxy.DepartmentID > 0)
                        {
                            if (prs.Department.ID != infoProxy.DepartmentID)
                                PersonnelsDepartmentChange.Add(prs.ID);
                            if (organizationFeatureList.Count != 0)
                            {
                                organizationFeatureExist = organizationFeatureList.Where(x => x.DepartmentId == prs.Department.ID).ToList();
                            }

                            if (organizationFeatureExist.Count == 0)
                            {
                                IList<decimal> FlowIds = underMngRep.GetDepartmentFlows(prs.Department.ID);                               
                                foreach (decimal flowId in FlowIds)
                                {                                 
                                    OrganizationFeatureProxy orgFeatureProxy = new OrganizationFeatureProxy();
                                    FlowPersonsProxy flowPerson = new FlowPersonsProxy();                                    
                                    orgFeatureProxy.FlowId = flowId;
                                    orgFeatureProxy.DepartmentId = prs.Department.ID;
                                    organizationFeatureList.Add(orgFeatureProxy);
                                    flowPerson.FlowId = flowId;
                                    flowPerson.PersonId = prs.ID;
                                    flowPersonList.Add(flowPerson);                                    
                                }                                
                            }
                            else
                            {
                                IList<decimal> FlowIds = organizationFeatureExist.Select(x => x.FlowId)
                                                                                 .ToList<decimal>();                                
                                foreach (decimal flowId in FlowIds)
                                {
                                    FlowPersonsProxy flowPerson = new FlowPersonsProxy();
                                    flowPerson.FlowId = flowId;
                                    flowPerson.PersonId = prs.ID;
                                    flowPersonList.Add(flowPerson);                                    
                                }                                 
                            }
                            prs.Department = new GTS.Clock.Model.Charts.Department() { ID = infoProxy.DepartmentID };
                            updatePrs = true;
                        }
                        #endregion

                        #region Employment Type
                        if (infoProxy.EmploymentTypeID > 0)
                        {
                            prs.EmploymentType = new GTS.Clock.Model.BaseInformation.EmploymentType() { ID = infoProxy.EmploymentTypeID };
                            updatePrs = true;
                        }
                        #endregion

                        if (updatePrs)
                        {
                            prsRepository.WithoutTransactUpdate(prs);
                        }

                        #region Rule Category
                        if (infoProxy.RuleGroupID > 0)
                        {
                            cfpUpdateRequierd = true;
                            PersonRuleCatAssignment ruleAssign = new PersonRuleCatAssignment();
                            BAssignRule ruleAsgnBus = new BAssignRule();
                            ruleAssgnList = ruleAsgnBus.GetAll(prs.ID);
                            IList<PersonRuleCatAssignment> confilictList =
                            ruleAssgnList.Where(x => ((Utility.ToMildiDateTime(x.FromDate) <= ruleGroupToDate && Utility.ToMildiDateTime(x.ToDate) >= ruleGroupToDate))
                                                                        ||
                                                     ((Utility.ToMildiDateTime(x.FromDate) <= ruleGroupFromDate && Utility.ToMildiDateTime(x.ToDate) >= ruleGroupFromDate))
                                                                        ||
                                                     ((Utility.ToMildiDateTime(x.FromDate) >= ruleGroupFromDate && Utility.ToMildiDateTime(x.FromDate) <= ruleGroupToDate))
                                    ).ToList();
                            if (confilictList != null && confilictList.Count > 0)
                            {
                                Range range = new Range() { From = ruleGroupFromDate, To = ruleGroupToDate, AditionalField = 0 };
                                var confilictRanges = from o in confilictList
                                                      select new Range() { From = Utility.ToMildiDateTime(o.FromDate), To = Utility.ToMildiDateTime(o.ToDate), AditionalField = o.RuleCategory.ID };
                                IList<Range> breakedList = Utility.Differance(confilictRanges.ToList(), range);

                                #region Delete
                                //foreach (PersonRuleCatAssignment asgn in ruleAssgnList)
                                //{
                                //    ruleAsgnBus.SaveChanges(asgn, UIActionType.DELETE);
                                //}
                                foreach (PersonRuleCatAssignment asgn in confilictList)
                                {
                                    ruleAsgnBus.SaveChanges(asgn, UIActionType.DELETE);
                                }
                                #endregion

                                #region add first
                                ruleAssign.FromDate = Utility.ToString(ruleGroupFromDate);
                                ruleAssign.ToDate = Utility.ToString(ruleGroupToDate);
                                ruleAssign.Person = prs;
                                ruleAssign.RuleCategory = new RuleCategory() { ID = infoProxy.RuleGroupID };
                                busRule.InsertWithoutTransaction(ruleAssign);
                                #endregion

                                #region add breaked List
                                foreach (Range r in breakedList)
                                {
                                    if (r.From == range.To)
                                        r.From = r.From.AddDays(1);
                                    if (r.To == range.From)
                                        r.To = r.To.AddDays(-1);
                                    ruleAssign = new PersonRuleCatAssignment();
                                    ruleAssign.FromDate = Utility.ToString(r.From);
                                    ruleAssign.ToDate = Utility.ToString(r.To);
                                    ruleAssign.Person = prs;
                                    ruleAssign.RuleCategory = new RuleCategory() { ID = r.AditionalField };
                                    busRule.InsertWithoutTransaction(ruleAssign);
                                }
                                #endregion
                            }
                            else
                            {

                                ruleAssign.FromDate = Utility.ToString(ruleGroupFromDate);
                                ruleAssign.ToDate = Utility.ToString(ruleGroupToDate);
                                ruleAssign.Person = prs;
                                ruleAssign.RuleCategory = new RuleCategory() { ID = infoProxy.RuleGroupID };
                                busRule.InsertWithoutTransaction(ruleAssign);
                            }
                            if (minDate > ruleGroupFromDate)
                            {
                                minDate = ruleGroupFromDate;
                            }
                        }
                        #endregion
                        #region Contract
                        if (infoProxy.ContractID > 0)
                        {
                            cfpUpdateRequierd = true;
                            PersonContractAssignment contractAssign = new PersonContractAssignment();
                            contractAssign.Person = prs;
                            contractAssign.Contract = new Contract() { ID = infoProxy.ContractID };
                            contractAssign.FromDate = contractFromDate;
                            contractAssign.ToDate = contractToDate;
                            BPersonContractAssignment contractAsgnBus = new BPersonContractAssignment();
                            contractAssgnList = contractAsgnBus.GetAll(prs.ID);
                            PersonContractAssignment PersonContractAssignmentAlias = null;
                            Person PersonAlias = null;
                            IList<PersonContractAssignment> personContractAssignmentList = NHSession.QueryOver<PersonContractAssignment>(() => PersonContractAssignmentAlias)
                                                                                        .JoinAlias(() => PersonContractAssignmentAlias.Person, () => PersonAlias)
                                                                                        .Where(() => !PersonContractAssignmentAlias.IsDeleted &&
                                                                                                      PersonAlias.ID == prs.ID
                                                                                              )
                                                                                        .List<PersonContractAssignment>();
                            if (personContractAssignmentList != null && personContractAssignmentList.Count > 0)
                            {
                                personContractAssignmentList.ToList()
                                                            .ForEach(x =>
                                                            {
                                                                if (x.ToDate == Utility.GTSMinStandardDateTime)
                                                                    x.ToDate = DateTime.MaxValue.Date;
                                                            }
                                                                    );
                            }

                            if (contractAssign.ToDate == Utility.GTSMinStandardDateTime)
                                contractAssign.ToDate = DateTime.MaxValue.Date;
                            if (contractAsgnBus.CheckPersonContractAssignmentConfilct(personContractAssignmentList.ToList(), contractAssign))
                            {
                                errorList.Add(new ChangeInfoErrorProxy() { ErrorMessage = "تاریخ انتساب قرارداد با تاریخ های قبلی همپوشانی دارد", PersonCode = prs.PersonCode, PersonName = prs.Name });
                            }
                            else
                            {
                                if (prs != null && prs.ID != 0 && contractAssign.FromDate < prs.EmploymentDate)
                                {
                                    errorList.Add(new ChangeInfoErrorProxy() { ErrorMessage = "تاریخ انتساب ابتدای قرارداد نباید از تاریخ استخدام کوچکتر باشد", PersonCode = prs.PersonCode, PersonName = prs.Name });
                                }
                                else
                                {
                                    if (contractAssign.ToDate == DateTime.MaxValue.Date)
                                        contractAssign.ToDate = Utility.GTSMinStandardDateTime;
                                    busAssignContract.InsertWithoutTransaction(contractAssign);
                                    if (minDate > contractFromDate)
                                    {
                                        minDate = contractFromDate;
                                    }
                                }
                            }
                            
                        }
                        #endregion

                        #region Date Range
                        if (infoProxy.DateRangeID > 0)
                        {
                            cfpUpdateRequierd = true;
                            PersonRangeAssignment prsRangeAssignment = new PersonRangeAssignment();
                            rangeAssgnList = new BAssignDateRange(BLanguage.CurrentSystemLanguage).GetAll(prs.ID);
                            prsRangeAssignment = rangeAssgnList.Where(x => x.FromDate == DateRangeFromDate).FirstOrDefault();
                            if (prsRangeAssignment != null)
                            {
                                prsRangeAssignment.FromDate = DateRangeFromDate;
                                prsRangeAssignment.CalcDateRangeGroup = new CalculationRangeGroup() { ID = infoProxy.DateRangeID };
                            }
                            else
                            {
                                prsRangeAssignment = new PersonRangeAssignment();
                                prsRangeAssignment.Person = prs;
                                prsRangeAssignment.FromDate = DateRangeFromDate;
                                prsRangeAssignment.CalcDateRangeGroup = new CalculationRangeGroup() { ID = infoProxy.DateRangeID };
                            }
                            bussDateRange.InsertWithoutTransaction(prsRangeAssignment);

                            if (minDate > DateRangeFromDate)
                            {
                                minDate = DateRangeFromDate;
                            }
                        }
                        #endregion

                        #region Work Group
                        if (infoProxy.WorkGroupID > 0)
                        {
                            cfpUpdateRequierd = true;
                            AssignWorkGroup prsWorkGroupAssignment = new AssignWorkGroup();
                            workGroupAssgnList = new BAssignWorkGroup(BLanguage.CurrentSystemLanguage).GetAll(prs.ID);
                            prsWorkGroupAssignment = workGroupAssgnList.Where(x => x.FromDate == workGroupFromDate).FirstOrDefault();

                            if (prsWorkGroupAssignment != null)
                            {
                                prsWorkGroupAssignment.Person = prs;
                                prsWorkGroupAssignment.FromDate = workGroupFromDate;
                                prsWorkGroupAssignment.WorkGroup = new WorkGroup() { ID = infoProxy.WorkGroupID };
                            }
                            else
                            {
                                prsWorkGroupAssignment = new AssignWorkGroup();
                                prsWorkGroupAssignment.Person = prs;
                                prsWorkGroupAssignment.FromDate = workGroupFromDate;
                                prsWorkGroupAssignment.WorkGroup = new WorkGroup() { ID = infoProxy.WorkGroupID };
                            }
                            busWorkGroup.InsertWithoutTransaction(prsWorkGroupAssignment);
                            if (minDate > workGroupFromDate)
                            {
                                minDate = workGroupFromDate;
                            }
                        }
                        #endregion
                    }
                    if (infoProxy.DepartmentID > 0)
                    {                      
                        IList<decimal> FlowIDs = organizationFeatureList.Select(x => x.FlowId).Distinct().ToList<decimal>();
                        if(FlowIDs.Count != 0)
                        {
                            IList<UnderManagment> UnderManagmentList = underMngRep.GetAllPersonelparticularFlows(FlowIDs);
                            foreach (decimal flowid in FlowIDs)
                            {
                                IList<decimal> PersonIds = flowPersonList.Where(x => x.FlowId == flowid &&
                                                                                     !UnderManagmentList.Where(y => y.Flow.ID == flowid).Select(y => y.Person.ID).ToList<decimal>().Contains(x.PersonId)
                                                                               ).Select(x => x.PersonId).ToList<decimal>();
                                if (PersonIds.Count != 0)
                                    underMngRep.DeleteUnderManagmentPersonsWithOrganicInfo(flowid, PersonIds);

                            }  
                        }                                            
                        IList<decimal> NewDepertmentFlowsList = underMngRep.GetDepartmentFlows(personList[0].Department.ID);
                        if (NewDepertmentFlowsList.Count != 0)
                            underMngRep.InsertUnderManagmentPersons(personIdList, NewDepertmentFlowsList);

                        BRequestSubstitute rSubstitute = new BRequestSubstitute();
                        if (PersonnelsDepartmentChange.Count != 0)
                        rSubstitute.UpdateRequestSubstituteOfOrganicInfo(PersonnelsDepartmentChange);
                    }
                    if (cfpUpdateRequierd)
                        this.UpdateCFP(personList, minDate);

                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "BChangeOrganicInfo", "ChangeInfo");
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }


            return true;
        }

        /// <summary>
        /// بررسی حداقل تاریخ
        /// بررسی تکراری نبودن تاریخ
        /// </summary>
        /// <param name="person">پرسنل</param>
        /// <param name="fromDate">از تاریخ</param>
        /// <param name="message"></param>
        /// <returns>وضعیت انجام عملیات</returns>
        private bool ValidateWorkGroupAssignment(Person person, DateTime fromDate, ref string message)
        {
            PersonRepository personRep = new PersonRepository(false);
            WorkGroupRepository workRep = new WorkGroupRepository(false);
            EntityRepository<AssignWorkGroup> asignRepository = new EntityRepository<AssignWorkGroup>(false);

            if (fromDate <= Utility.GTSMinStandardDateTime)
            {
                message += "مقدار تاریخ انتساب گروه کاری از حد مجاز کمتر میباشد" + " --- ";
            }
            //else if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().Person), person),
            //    new CriteriaStruct(Utility.GetPropertyName(() => new AssignWorkGroup().FromDate), fromDate, CriteriaOperation.Equal)) > 0)
            //{
            //    message += "قبلا در این تاریخ گروه کاری دیگری انتساب داده شده است" + " --- ";
            //}

            return Utility.IsEmpty(message);
        }

        /// <summary>
        /// بررسی حداقل تاریخ
        /// بررسی بزرگتری تاریخ انها از ابتدا
        /// بررسی همپوشانی تاریخ ها
        /// </summary>
        /// <param name="person">پرسنل</param>
        /// <param name="fromDate">از تاریخ</param>
        /// <param name="toDate">تا تاریخ</param>
        /// <param name="message"></param>
        /// <returns>وضعیت انجام عملیات</returns>
        private bool ValidateRuleGroupAssignment(Person person, DateTime fromDate, DateTime toDate, ref string message)
        {
            EntityRepository<PersonRuleCatAssignment> asignRepository = new EntityRepository<PersonRuleCatAssignment>(false);
            PersonRepository personRep = new PersonRepository(false);

            if (fromDate <= Utility.GTSMinStandardDateTime || toDate <= Utility.GTSMinStandardDateTime)
            {
                message += "مقدار تاریخ انتساب گروه قانون از حد مجاز کمتر میباشد" + " --- ";
            }
            else if (toDate != Utility.GTSMinStandardDateTime && fromDate >= toDate)
            {
                message += "تاریخ انتساب گروه قانون ابتدا از انتها بزرگتر است" + " --- ";
            }
            /* else
             {
                 PersonRuleCatAssignment assignRule = new PersonRuleCatAssignment();
                 IList<PersonRuleCatAssignment> list = asignRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => assignRule.PersonId), person.ID));
                 if (list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= toDate && Utility.ToMildiDateTime(x.ToDate) >= toDate).Count() > 0
                     ||
                     list.Where(x => Utility.ToMildiDateTime(x.FromDate) <= fromDate && Utility.ToMildiDateTime(x.ToDate) >= fromDate).Count() > 0
                     ||
                     list.Where(x => Utility.ToMildiDateTime(x.FromDate) >= fromDate && Utility.ToMildiDateTime(x.FromDate) <= toDate).Count() > 0
                     )
                 {
                     message += "تاریخ انتساب گروه قانون با تاریخ های قبلی همپوشانی دارد" + " --- ";
                 }
             }*/
            return Utility.IsEmpty(message);
        }

        /// <summary>
        ///بررسی حداقل تاریخ
        /// بررسی بزرگتری تاریخ انها از ابتدا
        /// </summary>
        /// <param name="person"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateContractAssignment(Person person, DateTime fromDate, DateTime toDate, ref string message)
        {
            EntityRepository<PersonContractAssignment> asignRepository = new EntityRepository<PersonContractAssignment>(false);
            PersonRepository personRep = new PersonRepository(false);

            if (fromDate <= Utility.GTSMinStandardDateTime)
            {
                message += "مقدار تاریخ انتساب گروه قانون از حد مجاز کمتر میباشد" + " --- ";
            }
            else if (toDate != Utility.GTSMinStandardDateTime && fromDate >= toDate)
            {
                message += "تاریخ انتساب گروه قانون ابتدا از انتها بزرگتر است" + " --- ";
            }
           
            
           
            return Utility.IsEmpty(message);
        }

        /// <summary>
        /// بررسی حداقل تاریخ
        /// بررسی تکراری بودن تاریخ انتساب
        /// </summary>
        /// <param name="person">پرسنل</param>
        /// <param name="fromDate">از تاریخ</param>
        /// <param name="message"></param>
        /// <returns>وضعیت انجام عملیات</returns>
        private bool ValidateDateRangeAssignment(Person person, DateTime fromDate, ref string message)
        {
            PersonRepository personRep = new PersonRepository(false);
            EntityRepository<PersonRangeAssignment> asignRepository = new EntityRepository<PersonRangeAssignment>(false);

            if (fromDate <= Utility.GTSMinStandardDateTime)
            {
                message += "مقدار تاریخ انتساب دوره محاسبات از حد مجاز کمتر میباشد" + " --- ";
            }
            //else if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), person),
            //    new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), fromDate, CriteriaOperation.Equal)) > 0)
            //{
            //    message += "قبلا در این تاریخ محدوده محاسبات دیگری انتساب داده شده است" + " --- ";
            //}
            else
            {
                DateTime startMonth, endMonth;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    PersianDateTime pd = Utility.ToPersianDateTime(fromDate);
                    int endOfMonth = Utility.GetEndOfPersianMonth(pd.Year, pd.Month);
                    startMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, 1));
                    endMonth = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", pd.Year, pd.Month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(fromDate.Year, fromDate.Month);
                    startMonth = new DateTime(fromDate.Year, fromDate.Month, 1);
                    endMonth = new DateTime(fromDate.Year, fromDate.Month, endOfMonth);
                }

                if (asignRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().Person), person),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), startMonth, CriteriaOperation.GreaterEqThan),
                                                       new CriteriaStruct(Utility.GetPropertyName(() => new PersonRangeAssignment().FromDate), endMonth, CriteriaOperation.LessEqThan)) > 0)
                {
                    message += "انتساب دوره محاسبات در یک ماه نباید دوبار صورت بگیرد" + " --- ";
                }
            }

            return Utility.IsEmpty(message);
        }

        /// <summary>
        /// نشانگر محاسبات را به روز رسانی می کند
        /// </summary>
        /// <param name="personList">لیست پرسنل</param>
        /// <param name="minChangeDate">کمینه تاریخ تغییر</param>
        private void UpdateCFP(IList<Person> personList, DateTime minChangeDate)
        {
            BusinessEntity baseBus = new BusinessEntity();
            IList<CFP> cfpList = new List<CFP>();
            Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
            IList<decimal> personIdList = personList.Select(p => p.ID).ToList();
            IList<CFP> cfpPersonList = new List<CFP>();
            if (personList.Count > 0)
                cfpPersonList = baseBus.GetCFPPersons(personList.Select(a => a.ID).ToList<decimal>());
            IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdList(personList.Select(p => p.ID).ToList());
            IList<decimal> cfpPersonIdInsertList = new List<decimal>();

            foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
            {

                if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId))
                {
                    DateTime calculationLockDate = baseBus.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId);
                    uivalidationGroupIdDic.Add(uiValidateionGrpId, calculationLockDate);
                }

            }

            baseBus.UpdateCfpByPersonList(personIdList, minChangeDate, uivalidationGroupIdDic);
            cfpPersonIdInsertList = personList.Where(p => cfpPersonList != null && !cfpPersonList.Select(c => c.PrsId).ToList().Contains(p.ID)).Select(p => p.ID).Distinct().ToList<decimal>();
            if (cfpPersonIdInsertList.Count > 0)
                baseBus.InsertCfpByPersonList(cfpPersonIdInsertList, minChangeDate, uivalidationGroupIdDic);

        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelOrganizationFeaturesChangeLoadAccess()
        {
        }


    }
}
