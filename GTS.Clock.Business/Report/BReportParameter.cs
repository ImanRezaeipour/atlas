using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Report;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Model;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Proxy;
using Stimulsoft.Report;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.RequestFlow;
using Stimulsoft.Report.Components;
using Stimulsoft.Base.Drawing;
using System.Drawing;
using System.Data;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Criterion;
using System.IO;
using System.Web.Configuration;
using System.Web;
using GTS.Clock.Business.Temp;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Model.UIValidation;
using GTS.Clock.Model.PersonInfo;
using GTS.Clock.Business.PersonInfo;
using System.Globalization;
using GTS.Clock.Model.Rules;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model.Contracts;
//using ClosedXML.Excel;
using NHibernate;
using ClosedXML.Excel;

namespace GTS.Clock.Business.Reporting
{
    /// <summary>
    /// created at: 2011-11-22 12:50:50 PM
    ///Farhad Salavati
    /// </summary>
    public class BReportParameter : MarshalByRefObject
    {
        enum GroupDailyReportColumnEnum
        {
            Prs_ID,
            IsHourly,
            FromDate,
            ToDate,
            Prs_FirstName,
            Prs_LastName,
            Barcode
        }
        enum GroupMonthlyReportColumnEnum
        {
            Prs_ID,
            IsHourly,
            FromDate,
            ToDate,
        }
        enum GroupPersonColumnEnum
        {
            Prs_ID

        }
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BReportParameter";
        BControlParameter_YearMonth bControlParameter_YearMonth = new BControlParameter_YearMonth();
        ISearchPerson personSearch = new BPerson();
        EntityRepository<ReportParameter> ReportParamRep = new EntityRepository<ReportParameter>();
        EntityRepository<ReportParameterDesigned> ReportParamDesignedRep = new EntityRepository<ReportParameterDesigned>();
        EntityRepository<ReportUIParameter> ReportUIParameterRep = new EntityRepository<ReportUIParameter>();
        EntityRepository<DesignedReportCondition> reportDesignedReportCondition = new EntityRepository<DesignedReportCondition>(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        UIValidationExceptions exception = new UIValidationExceptions();
        private GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();
        private BTemp bTemp = new BTemp();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()], CultureInfo.InvariantCulture);


        #region Fill Page ComboBoxes

        /// <summary>
        /// لیست گروهای کاری را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<WorkGroup> GetAllWorkGroups()
        {
            return personSearch.GetAllWorkGroup();
        }

        /// <summary>
        /// لیست گروهای قوانین را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<RuleCategory> GetAllRuleGroups()
        {
            return personSearch.GetAllRuleGroup();
        }
        public IList<Contract> GetAllContracts()
        {
            return personSearch.GetAllContract();
        }
        /// <summary>
        /// لیست گروهای محدوده محاسبات را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<CalculationRangeGroup> GetAllDateRanges()
        {
            return personSearch.GetAllDateRanges();
        }

        /// <summary>
        /// لیست ایستگاههای کنترل را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ControlStation> GetAllControlStations()
        {
            return personSearch.GetAllControlStation();
        }

        /// <summary>
        /// لیست انواع استخدام را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<EmploymentType> GetAllEmploymentTypes()
        {
            //return personSearch.GetAllEmploymentTypes();
            return personSearch.GetAllEmploymentTypesWithoutAccessible();
        }
        /// <summary>
        /// لیست  کروه واسط کاربری را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<UIValidationGroup> GetAllUIValidationGroup()
        {
            return personSearch.GetAllUIValidationGroup();
        }
        /// <summary>
        /// ریشه بخش را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Department GetDepartmentRoot()
        {
            return personSearch.GetDepartmentRoot();
        }

        public IList<Department> GetAllDepartments()
        {
            //return new BDepartment().GetAll();
            return personSearch.GetAllDepartments();
        }

        /// <summary>
        /// بچههای یک گره را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<Department> GetDepartmentChilds(decimal parentId, IList<Department> allNodes)
        {
            return personSearch.GetDepartmentChild(parentId, allNodes);
        }

        /// <summary>
        /// ریشه چارت سازمانی را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public OrganizationUnit GetOrganizationUnitRoot()
        {
            return personSearch.GetOrganizationRoot();
        }

        /// <summary>
        /// بچههای یک گره را برمیگرداند
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<OrganizationUnit> GetOrganizationUnitChilds(decimal parentId)
        {
            return personSearch.GetOrganizationChild(parentId);
        }

        /// <summary>
        /// لیست پرسنل را برمیگرداند
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllPersons(int pageIndex, int PageSize)
        {
            ISearchPerson searchTool = new BPerson();
            IList<Person> list = searchTool.GetAllPerson(pageIndex, PageSize);
            return list;
        }

        /// <summary>
        /// تعداد پرسنل را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public int GetAllPaeronsCount()
        {
            ISearchPerson searchTool = new BPerson();
            int count = searchTool.GetPersonCount();
            return count;
        }


        #endregion

        #region Persons

        /// <summary>
        /// لیست نتایج جستجو را برمیگرداند
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IList<Person> GetAllPersons(Business.Proxy.PersonAdvanceSearchProxy proxy, int pageIndex, int PageSize)
        {
            ISearchPerson bperson = new BPerson();
            IList<Person> list = bperson.GetPersonInAdvanceSearch(proxy, pageIndex, PageSize);
            return list;
        }

        /// <summary>
        /// تعداد کل نتایج جستجو را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public int GetAllPaeronsCount(Business.Proxy.PersonAdvanceSearchProxy proxy)
        {
            ISearchPerson bperson = new BPerson();
            int count = bperson.GetPersonInAdvanceSearchCount(proxy);
            return count;
        }

        #endregion

        /// <summary>
        /// پارامتر های یک گزارش را برمیگرداند
        /// </summary>
        /// <param name="reportId">شناسه گزارش</param>
        /// <returns></returns>
        public IList<ReportUIParameter> GetUIReportParameters(decimal reportFileId, decimal reportId, bool isDesigned)
        {
            try
            {

                IList<ReportUIParameter> resultList = new List<ReportUIParameter>();
                IList<ReportParameter> list = new List<ReportParameter>();
                if (isDesigned == false)
                {
                    list = ReportParamRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ReportParameter().ReportFile), new ReportFile() { ID = reportFileId }));
                }
                else
                {
                    if (new BDesignedReportsColumn().GetDesignedReportsColumnsByReportID(reportId).Count(c => c.IsConcept) > 0 || new BDesignedReportsColumn().GetDesignedReportsColumnsByReportID(reportId).Count(c => c.Traffic != null) > 0)
                    {
                        list = ReportParamRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ReportParameter().Report), new Report() { ID = reportId }));
                    }


                }
                var a = from o in list
                        select o.ReportUIParameter;
                var result = from y in a
                             group y by y;
                foreach (var found in result)
                {
                    ReportUIParameter parameter = found.Key;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        parameter.ParameterTitle = parameter.fnName;
                    }
                    else
                    {
                        parameter.ParameterTitle = parameter.EnName;
                    }
                    resultList.Add(parameter);
                }

                return resultList;
            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameter>.LogException(ex, "BReportParameter", "GetReportParameter");
                throw ex;
            }
        }

        public IList<ReportParameterDesigned> GetAllReportParameterDesigned()
        {
            try
            {
                IList<ReportParameterDesigned> reportParamDesignedList = ReportParamDesignedRep.GetAll();
                foreach (ReportParameterDesigned item in reportParamDesignedList)
                {

                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        item.Title = item.FnName;
                    }
                    else
                    {
                        item.Title = item.EnName;
                    }

                }
                return reportParamDesignedList;
            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameterDesigned>.LogException(ex, "BReportParameter", "GetReportParameterDesigned");
                throw ex;
            }
        }
        public IList<ReportParameter> GetReportParameterByReportID(decimal reportId)
        {
            try
            {
                Report report = null;
                ReportParameter reportParameter = null;
                IList<ReportParameter> reportParamList = NHibernateSessionManager.Instance.GetSession().QueryOver<ReportParameter>(() => reportParameter).JoinAlias(() => reportParameter.Report, () => report).Where(() => report.ID == reportId).List<ReportParameter>();

                return reportParamList;
            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameterDesigned>.LogException(ex, "BReportParameter", "GetReportParameterByReportID");
                throw ex;
            }
        }
        public void DeleteReportParameterByReportID(decimal reportId)
        {
            try
            {
                IList<ReportParameter> reportParamList = GetReportParameterByReportID(reportId);
                foreach (ReportParameter item in reportParamList)
                {
                    ReportParamRep.Delete(item);
                }


            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameterDesigned>.LogException(ex, "BReportParameter", "DeleteReportParameterByReportID");
                throw ex;
            }
        }

        //DNN Note:-------------------------------------------
        public IList<ReportParameter> GetReportParameterByFileID(decimal FileID)
        {
            try
            {
                ReportFile reportFile = null;
                ReportParameter reportParameter = null;
                IList<ReportParameter> reportParamList = NHibernateSessionManager.Instance.GetSession().QueryOver<ReportParameter>(() => reportParameter)
                    .JoinAlias(() => reportParameter.ReportFile, () => reportFile)
                    .Where(() => reportFile.ID == FileID)
                    .List<ReportParameter>();

                return reportParamList;
            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameterDesigned>.LogException(ex, "BReportParameter", "GetReportParameterByFileID");
                throw ex;
            }
        }
         
        public void DeleteReportParameterByFileID(decimal FileID)
        {
            try
            {
                IList<ReportParameter> reportParamList = GetReportParameterByFileID(FileID);
                foreach (ReportParameter item in reportParamList)
                {
                    ReportParamRep.Delete(item);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameterDesigned>.LogException(ex, "BReportParameter", "DeleteReportParameterByFileID");
                throw ex;
            }
        }

        //End Of DNN Note:------------------------------------
        public decimal InsertReportParameter(GTS.Clock.Model.Report.ReportParameter reportParameterObj)
        {
            try
            {
                decimal id = ReportParamRep.Save(reportParameterObj).ID;
                return id;
            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameter>.LogException(ex, "BReportParameter", "InsertReportParameter");
                throw ex;
            }
        }
        /// <summary>
        /// گزارش را نمایش میدهد
        /// </summary>
        /// <param name="reportFileId"></param>
        /// <param name="proxy"></param>
        /// <param name="parmeters"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public StiReport GetReport(decimal reportFileId, PersonAdvanceSearchProxy proxy, IList<ReportUIParameter> parmeters, bool isDesigned, decimal reportId, decimal groupingTypeId, bool isPersonFilterProxyValued, bool groupingByNewPage, bool IsOnlyForCurrentUser, ReportOutPutType outPutType)
        {
            try
            {
                if (parmeters.Where(x => Utility.IsEmpty(x.ActionId)).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ReportParameterActionIdIsEmpty, "شناسه عملیات مشخص نشده است", ExceptionSrc));
                    throw exception;
                }
                ISearchPerson searchTool = new BPerson();
                IList<decimal> personIdList;
                IList<Person> persons = new List<Person>();



                if (IsOnlyForCurrentUser)
                {
                    // persons = new List<Person>();
                    //Person currentPerson = new BPerson().GetByID(BUser.CurrentUser.Person.ID);
                    persons.Add(BUser.CurrentUser.Person);
                    //NHSession.Evict(currentPerson);
                }
                else
                {
                    if (proxy.PersonIdList == null || proxy.PersonIdList.Count == 0)
                    {
                        //proxy.PersonActivateState = true;
                        persons = searchTool.GetPersonInAdvanceSearch(proxy);
                    }
                    else
                        persons = searchTool.GetPersonByPersonIdList(proxy.PersonIdList);
                }

                //کلیه پرسنل مدیر , جانشین و اپراتور
                if (persons.Count == 0 && !Utility.IsEmpty(parmeters) && !isPersonFilterProxyValued)
                {
                    persons = searchTool.QuickSearchByPage(0, searchTool.GetPersonCount(), "");
                }

                if (persons.Count == 0 && !Utility.IsEmpty(parmeters) && !isPersonFilterProxyValued)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ReportParameterPersonIsEmpty, "مجموعه انتخابی شامل هیچ پرسنلی نمیباشد", ExceptionSrc));
                    throw exception;
                }
                var ids = from o in persons
                          select o.ID;
                personIdList = ids.ToList<decimal>();

                IDictionary<string, object> ParamValues = new Dictionary<string, object>();
                foreach (ReportUIParameter param in parmeters)
                {
                    if (param.ActionId == "EmptyParam" || param.ActionId == "DutyPlace")
                    {
                        if (param.ActionId == "DutyPlace")
                        {
                            if (Utility.IsEmpty(param.Value))
                            {
                                exception.Add(new ValidationException(ExceptionResourceKeys.ReportParametersIsEmpty, "مقدار پارامترها مشخص نشده است", ExceptionSrc));
                                throw exception;
                            }
                        }
                        continue;
                    }
                    if (Utility.IsEmpty(param.Value))
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.ReportParametersIsEmpty, "مقدار پارامترها مشخص نشده است", ExceptionSrc));
                        throw exception;
                    }

                    string value = param.Value;
                    IDictionary<string, object> result;
                    if (BusinessFactory.Exists(param.ActionId))
                    {
                        result = BusinessFactory.GetBusiness<IBControlParameter>(param.ActionId).ParsParameter(value, param.ActionId);
                    }
                    else
                    {
                        result = BaseControlParameter.ParsParameter(value);
                    }
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            ParamValues.Add(item);
                        }
                    }
                }
                IList<decimal> dutyPlaceIds = new List<decimal>();
                if (parmeters.Count(c => c.ActionId == "DutyPlace") > 0)
                {
                    IDictionary<string, object> result;
                    ReportUIParameter param = parmeters.FirstOrDefault(c => c.ActionId == "DutyPlace");
                    result = BaseControlParameter.ParsParameter(param.Value);
                    if (result.Keys.Contains("dutyPlaceIds") && result["dutyPlaceIds"].ToString() != "")
                    {
                        List<string> paramStrList = result["dutyPlaceIds"].ToString().Split(',').ToList();
                        dutyPlaceIds = paramStrList.Select(p => Convert.ToDecimal(p, CultureInfo.InvariantCulture)).ToList();

                    }

                }

                string operationGUID = this.bTemp.InsertTempList(personIdList);

                string groupingType = "None";
                if (groupingTypeId != -1)
                {
                    groupingType = ((GroupingReport)groupingTypeId).ToString();
                }
                string headerPartFilterText = string.Empty;
                string headerEmployFilterText = string.Empty;
                if (!isDesigned)
                {

                    if (groupingType == "None")
                    {
                        headerPartFilterText = GetDepartmentNameListStr(proxy.DepartmentListId);
                        headerEmployFilterText = GetEmploymentNameListStr(proxy.EmploymentTypeListId);
                    }

                }

                ReportHelper reportHelper = ReportHelper.Instance("شرکت طرح و پردازش غدیر", BUser.CurrentUser.ID, BUser.CurrentUser.Person.Name, personIdList, operationGUID, groupingType, groupingByNewPage, headerPartFilterText, headerEmployFilterText, dutyPlaceIds);
                StiReport report;
                if (isDesigned == false)
                {
                    ReportFile file = this.GetReportFile(reportFileId);

                    report = reportHelper.GetReport(@file.File);
                    ((Stimulsoft.Report.Dictionary.StiSqlSource)report.Dictionary.DataSources[0]).CommandTimeout = 6000;
                }
                else
                {
                    report = InitializeDesignedReport(reportId, BUser.CurrentUser.Person.ID, ParamValues, persons, proxy, outPutType);
                }

                reportHelper.InitAssemblyReport(report);
                reportHelper.InitReportParameter(report, ParamValues);

                //string connectionString = (((NHibernate.Cfg.ConfigurationSchema.HibernateConfiguration)(System.Configuration.ConfigurationManager.GetSection("hibernate-configuration"))).SessionFactory).Properties["connection.connection_string"];
                string connectionString = NHibernateSessionManager.Instance.SessionFactoryPropsDic["connection.connection_string"];
                reportHelper.InitReportConnection(report, connectionString);

                report.ReportGuid = operationGUID;

                report.Compile();
                return report;
            }
            catch (Exception ex)
            {
                BaseBusiness<Report>.LogException(ex, "BReportParameter", "GetReport");
                throw ex;
            }
        }


        public string GetDepartmentNameListStr(List<decimal> departmentList)
        {
            try
            {
                string headerPartFilterText = string.Empty;
                int showChararcterCount = 110;
                switch (BLanguage.CurrentLocalLanguage)
                {
                    case LanguagesName.Unknown:
                        break;
                    case LanguagesName.Parsi:
                        if (departmentList != null && departmentList.Count > 0)
                            headerPartFilterText += "بخش ها : ";
                        break;
                    case LanguagesName.English:
                        if (departmentList != null && departmentList.Count > 0)
                            headerPartFilterText += "Parts :  ";
                        break;
                    default:
                        break;
                }




                if (departmentList != null && departmentList.Count > 0)
                {

                    foreach (decimal item in departmentList)
                    {
                        Department departmentObj = new BDepartment().GetByID(item);
                        if (departmentObj != null)
                        {
                            if (headerPartFilterText.Count() < showChararcterCount)
                            {
                                string departmentName = departmentObj.Name;
                                headerPartFilterText += departmentName;
                                headerPartFilterText += "،";


                            }
                        }
                    }

                }



                if (headerPartFilterText.Count() > showChararcterCount)
                    headerPartFilterText += "،...";

                return headerPartFilterText;
            }
            catch (Exception ex)
            {

                BaseBusiness<Report>.LogException(ex, "BReportParameter", "GetDepartmentNameListStr");
                throw ex;
            }
        }
        public string GetEmploymentNameListStr(List<decimal> emplomentList)
        {
            try
            {
                string headerEmployFilterText = string.Empty;
                int showChararcterCount = 110;
                switch (BLanguage.CurrentLocalLanguage)
                {
                    case LanguagesName.Unknown:
                        break;
                    case LanguagesName.Parsi:
                        if (emplomentList != null && emplomentList.Count > 0)
                            headerEmployFilterText += "نوع استخدام : ";
                        break;
                    case LanguagesName.English:
                        if (emplomentList != null && emplomentList.Count > 0)
                            headerEmployFilterText += "Employment Type : ";
                        break;
                    default:
                        break;
                }







                if (emplomentList != null && emplomentList.Count > 0)
                {
                    foreach (decimal item in emplomentList)
                    {
                        EmploymentType emploeeObj = new BEmployment().GetByID(item);
                        if (emploeeObj != null)
                        {
                            if (headerEmployFilterText.Count() < showChararcterCount)
                            {
                                string employName = emploeeObj.Name;
                                headerEmployFilterText += employName;
                                headerEmployFilterText += "،";


                            }
                        }
                    }



                }
                if (headerEmployFilterText.Count() > showChararcterCount)
                    headerEmployFilterText += "،...";

                return headerEmployFilterText;
            }
            catch (Exception ex)
            {

                BaseBusiness<Report>.LogException(ex, "BReportParameter", "GetEmploymentNameListStr");
                throw ex;
            }
        }
        public void InitializeDataTableForOutPutReport(DataTable dtReport, IList<DesignedReportColumn> designedReportColumnList, ReportOutPutType outPutType, string reportName)
        {

            DataTable dtResult = new DataTable();
            IList<DesignedReportStaticColumn> designedReportStaticColumnsList = new BDesignedReportsColumn().GetAllDesignedReportsStaticColumn();
            try
            {
                switch (outPutType)
                {
                    case ReportOutPutType.Report:
                        break;
                    case ReportOutPutType.Excel:
                        dtResult = dtReport.Clone();
                        for (int i = 0; i < dtResult.Columns.Count; i++)
                        {
                            dtResult.Columns[i].DataType = typeof(string);
                        }
                        foreach (DataRow row in dtReport.Rows)
                        {
                            dtResult.ImportRow(row);
                        }

                        if (dtResult.Columns["Prs_ID"] != null)
                            dtResult.Columns.Remove("Prs_ID");
                        int dtResultRowCount = dtResult.Rows.Count;
                        int dtResultColumnCount = dtResult.Columns.Count;
                        for (int i = 0; i < dtResultRowCount; i++)
                        {
                             
                            for (int j = 0; j < dtResultColumnCount; j++)
                            {
                                switch (BLanguage.CurrentLocalLanguage)
                                {
                                    case LanguagesName.Unknown:
                                        break;
                                    case LanguagesName.Parsi:
                                        if (dtReport.Columns[dtResult.Columns[j].ColumnName].DataType == typeof(DateTime))
                                        {
                                            dtResult.Rows[i][j] = Utility.ToPersianDate(DateTime.Parse(dtResult.Rows[i][j] == DBNull.Value ? Utility.GTSMinStandardDateTime.ToString() : dtResult.Rows[i][j].ToString(), CultureInfo.InvariantCulture));
                                        }



                                        break;
                                    case LanguagesName.English:
                                        break;
                                    default:
                                        break;
                                }

                                DesignedReportColumn desinedColumnHourlyObj = designedReportColumnList.FirstOrDefault(d => d.Concept != null && d.Concept.IsHourly && d.ColumnName == dtResult.Columns[j].ColumnName);
                                if (desinedColumnHourlyObj != null)
                                {
                                    int timeMinuteValue = Convert.ToInt32(dtResult.Rows[i][j] == DBNull.Value ? 0 : dtResult.Rows[i][j]);
                                    int dayValue = 0;
                                    int realDayValue = 0;
                                    int remainMinuteValue = timeMinuteValue;
                                    string dateAddToTime = "";
                                    int monthValue = 1;
                                    int yearValue = 1900;
                                    if (timeMinuteValue != 0)
                                    {
                                        dayValue = (timeMinuteValue / 1440);
                                        realDayValue = dayValue;
                                        if (dayValue > 1)
                                            dayValue = dayValue - 1;


                                        remainMinuteValue = timeMinuteValue % 1440;

                                        if (dayValue > 0)
                                        {

                                            int endOfMonth = DateTime.DaysInMonth(1900, monthValue);
                                            while (dayValue > endOfMonth)
                                            {
                                                dayValue = dayValue - endOfMonth;
                                                if (monthValue < 12)
                                                    monthValue++;
                                                else
                                                {
                                                    yearValue++;
                                                    monthValue = 1;
                                                }


                                                endOfMonth = DateTime.DaysInMonth(yearValue, monthValue);
                                            }
                                            if (realDayValue == 1)
                                            {
                                                int endDay = Utility.GetEndOfMiladiMonth(1899, 12);
                                                dateAddToTime = "12/" + endDay.ToString() + "/1899  ";
                                            }
                                            else
                                                dateAddToTime = monthValue.ToString() + "/" + dayValue.ToString() + "/" + yearValue.ToString() + "  ";
                                        }


                                    }

                                    dtResult.Rows[i][j] = dateAddToTime + Utility.IntTimeToTime(remainMinuteValue);


                                }
                                DesignedReportColumn desinedColumnDailyObj = designedReportColumnList.FirstOrDefault(d => d.Concept != null && !d.Concept.IsHourly && d.ColumnName == dtResult.Columns[j].ColumnName);
                                if (desinedColumnDailyObj != null && dtResult.Rows[i][j].ToString() == "-1000")
                                {
                                    dtResult.Rows[i][j] = "";
                                }
                                DesignedReportColumn desinedColumnTrafficObj = designedReportColumnList.FirstOrDefault(d => d.Traffic != null && d.ColumnName == dtResult.Columns[j].ColumnName);
                                if (desinedColumnTrafficObj != null)
                                {
                                    dtResult.Rows[i][j] = Utility.IntTimeToTime(Convert.ToInt32(dtResult.Rows[i][j] == DBNull.Value ? 0 : dtResult.Rows[i][j]));
                                }
                                DesignedReportColumn desinedColumnSexObj = designedReportColumnList.FirstOrDefault(d => d.PersonInfo != null && d.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.Sex && d.ColumnName == dtResult.Columns[j].ColumnName);
                                if (desinedColumnSexObj != null)
                                {
                                    if (dtResult.Rows[i][j] != DBNull.Value)
                                    {
                                        if (Convert.ToBoolean(dtResult.Rows[i][j]) == true)
                                        {
                                            dtResult.Rows[i][j] = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "Woman").Name;
                                        }
                                        else
                                        {
                                            dtResult.Rows[i][j] = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "Man").Name;
                                        }
                                    }
                                    else
                                    {
                                        dtResult.Rows[i][j] = string.Empty;
                                    }


                                }
                                DesignedReportColumn desinedColumnPersonActiveObj = designedReportColumnList.FirstOrDefault(d => d.PersonInfo != null && d.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.PersonActive && d.ColumnName == dtResult.Columns[j].ColumnName);
                                if (desinedColumnPersonActiveObj != null)
                                {
                                    if (dtResult.Rows[i][j] != DBNull.Value)
                                    {
                                        if (Convert.ToBoolean(dtResult.Rows[i][j] == DBNull.Value ? false : dtResult.Rows[i][j]) == true)
                                        {
                                            dtResult.Rows[i][j] = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "Active").Name;
                                        }
                                        else
                                        {
                                            dtResult.Rows[i][j] = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "DeActive").Name;
                                        }
                                    }
                                    else
                                    {
                                        dtResult.Rows[i][j] = string.Empty;
                                    }


                                }
                                DesignedReportColumn desinedColumnPersonRemainLeaveCurrentHourlyObj = designedReportColumnList.FirstOrDefault(d => d.PersonInfo != null && (d.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthHour || d.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearHour) && d.ColumnName == dtResult.Columns[j].ColumnName);
                                if (desinedColumnPersonRemainLeaveCurrentHourlyObj != null)
                                {
                                    dtResult.Rows[i][j] = Utility.IntTimeToTime(Convert.ToInt32(dtResult.Rows[i][j] == DBNull.Value ? 0 : dtResult.Rows[i][j]));

                                }

                            }

                        }
                        foreach (DesignedReportColumn item in designedReportColumnList)
                        {
                            dtResult.Columns[item.ColumnName].ColumnName = item.Name;
                        }
                        if (dtResult.Columns["Barcode"] != null)
                            dtResult.Columns["Barcode"].ColumnName = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "Barcode").Name;
                        if (dtResult.Columns["FromDate"] != null)
                            dtResult.Columns["FromDate"].ColumnName = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "FromDate").Name;
                        if (dtResult.Columns["ToDate"] != null)
                            dtResult.Columns["ToDate"].ColumnName = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "ToDate").Name;
                        if (dtResult.Columns["Date"] != null)
                            dtResult.Columns["Date"].ColumnName = designedReportStaticColumnsList.FirstOrDefault(c => c.KeyName == "Date").Name;
                        XLWorkbook workbook = new XLWorkbook();

                        var ws = workbook.Worksheets.Add(dtResult);
                         
                        //for (int i = 0; i < dtResult.Columns.Count; i++)
                        //{
                        //    ws.Cell(1, i + 1).Value = dtResult.Columns[i].ColumnName;
                        //    ws.Cell(1, i + 1).Style.Fill.SetBackgroundColor(XLColor.LightBlue);
                        //    ws.Cell(1, i + 1).Style.Font.FontName = "arial";
                        //    ws.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        //    ws.Column(i + 1).AdjustToContents();

                        //}
                         
                        int rowExcel = 2;
                        for (int i = 0; i < dtResultRowCount; i++)
                        {
                            int colExcel = 1;
                            for (int j = 0; j < dtResultColumnCount; j++)
                            {
                                if ((designedReportColumnList.Count(c => c.Name == dtResult.Columns[j].ColumnName && c.Concept != null && c.Concept.IsHourly) > 0) || (designedReportColumnList.Count(d => d.PersonInfo != null && (d.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthHour || d.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearHour) && d.Name == dtResult.Columns[j].ColumnName) > 0) || (designedReportColumnList.Count(d => d.Traffic != null && d.Name == dtResult.Columns[j].ColumnName) > 0))
                                {
                                    ws.Cell(rowExcel, colExcel).Style.DateFormat.Format = "[hh]:mm";
                                }
                                //ws.Cell(rowExcel, colExcel).Value = dtResult.Rows[i][j];
                                //ws.Cell(1, i + 1).Style.Font.FontName = "arial";
                                //ws.Cell(rowExcel, colExcel).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                //ws.Column(colExcel).AdjustToContents();
                                colExcel++;
                            }

                            rowExcel++;
                        }
                         
                        Dictionary<XLWorkbook, string> reportOutputDic = new Dictionary<XLWorkbook, string>();
                        reportOutputDic.Add(workbook, reportName);
                        SessionHelper.SaveSessionValue(SessionHelper.ReportOutPutFile, reportOutputDic);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {

                BaseBusiness<Report>.LogException(ex, "BReportParameter", "IntializeDataTableForOutPutReport");
                throw ex;
            }
        }
        private StiReport InitializeDesignedReport(decimal reportId, decimal currentPersonId, IDictionary<string, object> paramValues, IList<Person> personList, PersonAdvanceSearchProxy proxy, ReportOutPutType outPutType)
        {
            try
            {

                BReport reportBusiness = new BReport();
                BDesignedReportsColumn designedReportColumnBusiness = new BDesignedReportsColumn();
                IList<DesignedReportStaticColumn> designedReportStaticColumnList = designedReportColumnBusiness.GetAllDesignedReportsStaticColumn();
                IList<DesignedReportGroupColumn> designedReportGroupColumnList = new BDesignedReportGroupColumn().GetDesignedReportGroupColumns(reportId, currentPersonId);
                IList<DesignedReportColumn> designedReportColumnList = designedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
                IList<PersonParamField> personParamFieldList = new BPersonParamFields().GetAll().Where(p => p.Active && p.SubSystemId == SubSystemIdentifier.TimeAtendance).ToList();
                Report reportObj = reportBusiness.GetByID(reportId);
                UIValidationExceptions exception = new UIValidationExceptions();
                if (personList.Count == 0)
                {
                    exception.Add(ExceptionResourceKeys.PersonNotExist, "پرسنلی جهت نمایش گزارش وجود ندارد", ExceptionSrc);
                }
                if ((reportObj.DesignedType.CustomCode == DesignedReportTypeEnum.Daily || reportObj.DesignedType.CustomCode == DesignedReportTypeEnum.Monthly) && designedReportColumnList.Count(c => c.Concept != null || c.Traffic != null) == 0)
                {
                    exception.Add(ExceptionResourceKeys.DesignedReportTypeNotCorrectColumns, "گزارش از نوع مشروح و ماهانه می بایست ستونی از نوع مفاهیم داشته باشد.", ExceptionSrc);
                }
                if ((reportObj.DesignedType.CustomCode == DesignedReportTypeEnum.Person) && designedReportColumnList.Count(c => c.Concept != null || c.Traffic != null) > 0)
                {
                    exception.Add(ExceptionResourceKeys.DesignedReportPersonTypeNotCorrectColumns, "گزارش از نوع پرسنلی نمی تواند شامل ستون های مفاهیم باشد.", ExceptionSrc);
                }
                if ((reportObj.DesignedType.CustomCode == DesignedReportTypeEnum.Monthly || reportObj.DesignedType.CustomCode == DesignedReportTypeEnum.Person) && designedReportColumnList.Count(d => d.Traffic != null) > 0)
                {
                    exception.Add(ExceptionResourceKeys.MonthlyReportHavNotTrafficColumns, "گزارش از نوع ماهانه و یا پرسنلی نمی تواند شامل ستون تردد باشد", ExceptionSrc);
                }
                IList<string> keyColumnList = designedReportColumnList.Where(d => d.Concept != null && d.Concept.KeyColumnName != null).Select(k => k.Concept.KeyColumnName).ToList();
                if (designedReportColumnList.Count(d => d.Concept != null && keyColumnList.Count(c => c == d.Concept.KeyColumnName) > 1) > 0)
                {
                    exception.Add(ExceptionResourceKeys.KeyColumnNameConceptIsRepeated, "کلید مفاهیم ستون های انتخابی در پایگاه داده تکراری است", ExceptionSrc);
                }
                if (exception.Count > 0)
                {
                    throw exception;
                }
                double columnWidth = 0;
                double dataRowHeight = 0.55;
                Font dataBandFont = new Font("Tahoma", 8, FontStyle.Bold);
                Font headerBandFont = new Font("arial", 11, FontStyle.Bold);
                Font titleBandFont = new Font("Tahoma", 16, FontStyle.Bold);
                Font groupBandFont = new Font("Tahoma", 10, FontStyle.Bold);
                Font pageHeaderFont = new Font("Tahoma", 18, FontStyle.Bold);
                StiReport report = new StiReport();
                report.Unit = Stimulsoft.Report.Units.StiUnit.Centimeters;
                report.ReportName = reportId.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                StiPage page = report.Pages[0];
                page.PaperSize = System.Drawing.Printing.PaperKind.Custom;
                page.Orientation = StiPageOrientation.Landscape;
                page.Width = 29.7;
                page.Height = 21;
                page.Margins.Bottom = 0.5;
                page.Margins.Left = 0.5;
                page.Margins.Right = 0.5;
                page.Margins.Top = 0.5;
                page.RightToLeft = true;


                DataTable dtReport = new DataTable();
                DesignedReportTypeEnum reportType = reportObj.DesignedType.CustomCode;

                // design page header band
                StiPageHeaderBand pageHeader = new StiPageHeaderBand();
                pageHeader.Height = 2.9;
                pageHeader.Width = page.Width;
                pageHeader.Name = "PageHeader";
                page.Components.Add(pageHeader);


                StiDataBand dataBand = new StiDataBand();
                dataBand.DataSourceName = "Concepts";
                dataBand.Height = dataRowHeight;
                dataBand.Name = "DataBandMain";


                StiText officeNameText_pageHeader = new StiText(new RectangleD(0, 0, 10.6, 0.6));
                officeNameText_pageHeader.Left = (page.Width / 2) - 5.3;
                officeNameText_pageHeader.Top = 0;
                officeNameText_pageHeader.Height = 0.8;
                IList<Presentaion_Helper.Proxy.DataAccessProxy> dataAccessList = new BDataAccess().GetAllByUserId(DataAccessParts.Corporation, BUser.CurrentUser.ID);
                string headerText = string.Empty;
                if (dataAccessList != null && dataAccessList.Count > 0)
                    headerText = dataAccessList.FirstOrDefault().Name;
                officeNameText_pageHeader.Text = headerText;
                officeNameText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                officeNameText_pageHeader.VertAlignment = StiVertAlignment.Center;
                officeNameText_pageHeader.Name = "OfficeNameHeader";
                officeNameText_pageHeader.Font = pageHeaderFont;
                officeNameText_pageHeader.ShrinkFontToFit = true;
                pageHeader.Components.Add(officeNameText_pageHeader);

                StiText reportNameText_pageHeader = new StiText(new RectangleD(0, 0, 10.6, 0.6));
                reportNameText_pageHeader.Left = (page.Width / 2) - 5.3;
                reportNameText_pageHeader.Top = 0.8;
                reportNameText_pageHeader.Height = 0.8;
                reportNameText_pageHeader.Text = new BReport().GetByID(reportId).Name;
                reportNameText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportNameText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportNameText_pageHeader.Name = "reportNameText";
                reportNameText_pageHeader.Font = pageHeaderFont;
                reportNameText_pageHeader.ShrinkFontToFit = true;
                pageHeader.Components.Add(reportNameText_pageHeader);


                StiText reportCreateDateLabelText_pageHeader = new StiText(new RectangleD(0, 0, 2.6, 0.6));
                reportCreateDateLabelText_pageHeader.Left = 3.8;
                reportCreateDateLabelText_pageHeader.Top = 0;
                reportCreateDateLabelText_pageHeader.Text = designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "ReportCreateDate").Name;
                reportCreateDateLabelText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreateDateLabelText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreateDateLabelText_pageHeader.Name = "reportCreateDateLabelText";
                reportCreateDateLabelText_pageHeader.Font = dataBandFont;
                reportCreateDateLabelText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(89, 89, 89));
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportCreateDateLabelText_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportCreateDateLabelText_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }

                pageHeader.Components.Add(reportCreateDateLabelText_pageHeader);

                StiText reportCreateDateText_pageHeader = new StiText(new RectangleD(0, 0, 3.1, 0.6));
                reportCreateDateText_pageHeader.Left = 0.5;
                reportCreateDateText_pageHeader.Top = 0;
                reportCreateDateText_pageHeader.Text = ReportHelper.Instance().ShamsiGetNow();
                reportCreateDateText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreateDateText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreateDateText_pageHeader.Name = "reportCreateDateText";
                reportCreateDateText_pageHeader.Font = dataBandFont;
                reportCreateDateText_pageHeader.ShrinkFontToFit = true;
                reportCreateDateText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(183, 117, 64));
                reportCreateDateText_pageHeader.WordWrap = true;
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportCreateDateText_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportCreateDateText_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }
                pageHeader.Components.Add(reportCreateDateText_pageHeader);

                StiText reportCreatorLabelText_pageHeader = new StiText(new RectangleD(0, 0, 2, 0.6));
                reportCreatorLabelText_pageHeader.Left = 3.8;
                reportCreatorLabelText_pageHeader.Top = 0.6;
                reportCreatorLabelText_pageHeader.Text = designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "ReportCreator").Name;
                reportCreatorLabelText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreatorLabelText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreatorLabelText_pageHeader.Name = "reportCreatorLabelText";
                reportCreatorLabelText_pageHeader.Font = dataBandFont;
                reportCreatorLabelText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(89, 89, 89));
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportCreatorLabelText_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportCreatorLabelText_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }
                pageHeader.Components.Add(reportCreatorLabelText_pageHeader);


                StiText reportCreatorText_pageHeader = new StiText(new RectangleD(0, 0, 3, 0.6));
                reportCreatorText_pageHeader.Left = 0.5;
                reportCreatorText_pageHeader.Top = 0.6;
                reportCreatorText_pageHeader.Text = ReportHelper.Instance().UserName;
                reportCreatorText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreatorText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreatorText_pageHeader.Name = "reportCreatorText";
                reportCreatorText_pageHeader.Font = dataBandFont;
                reportCreatorText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(183, 117, 64));
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportCreatorText_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportCreatorText_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }
                pageHeader.Components.Add(reportCreatorText_pageHeader);
                if (designedReportGroupColumnList.Count(g => g.Column.ColumnName == "dep_name") == 0 && proxy.DepartmentListId != null && proxy.DepartmentListId.Count > 0)
                {
                    StiText reportDepartmentHeader_pageHeader = new StiText(new RectangleD(0, 0, 1.4, 0.6));
                    reportDepartmentHeader_pageHeader.Left = 0;
                    reportDepartmentHeader_pageHeader.Top = 1.6;
                    reportDepartmentHeader_pageHeader.Width = pageHeader.Width;
                    reportDepartmentHeader_pageHeader.Height = 0.6;
                    reportDepartmentHeader_pageHeader.Text = GetDepartmentNameListStr(proxy.DepartmentListId);
                    reportDepartmentHeader_pageHeader.Name = "reportDepartmentHeaderLabel";
                    reportDepartmentHeader_pageHeader.Font = dataBandFont;
                    reportDepartmentHeader_pageHeader.TextOptions.RightToLeft = true;
                    pageHeader.Components.Add(reportDepartmentHeader_pageHeader);
                }
                if (designedReportGroupColumnList.Count(g => g.Column.ColumnName == "emply_Name") == 0 && proxy.EmploymentTypeListId != null && proxy.EmploymentTypeListId.Count > 0)
                {
                    StiText reportEmployHeader_pageHeader = new StiText(new RectangleD(0, 0, 1.4, 0.6));
                    reportEmployHeader_pageHeader.Left = 0;
                    reportEmployHeader_pageHeader.Top = 2.2;
                    reportEmployHeader_pageHeader.Width = pageHeader.Width;
                    reportEmployHeader_pageHeader.Height = 0.6;
                    reportEmployHeader_pageHeader.Text = GetEmploymentNameListStr(proxy.EmploymentTypeListId);
                    reportEmployHeader_pageHeader.Name = "reportEmployHeaderLabel";
                    reportEmployHeader_pageHeader.Font = dataBandFont;
                    reportEmployHeader_pageHeader.TextOptions.RightToLeft = true;
                    pageHeader.Components.Add(reportEmployHeader_pageHeader);
                }





                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == currentPersonId).FirstOrDefault();
                DesignedReportParameterType paramType;
                if (reportObj.ReportParameterDesigned != null)
                    paramType = (DesignedReportParameterType)Enum.Parse(typeof(DesignedReportParameterType), reportObj.ReportParameterDesigned.CustomCode);
                else
                {
                    paramType = DesignedReportParameterType.DateRange;
                }
                StiGroupFooterBand groupFooterBand = new StiGroupFooterBand();
                decimal personId = BUser.CurrentUser.Person.ID;
                IList<DesignedReportGroupColumn> groupColumnList = new BDesignedReportGroupColumn().GetDesignedReportGroupColumns(reportId, personId);
                List<Color> groupBandColorList = new List<Color>();
                groupBandColorList.Add(Color.LightGreen);
                groupBandColorList.Add(Color.LightSkyBlue);
                groupBandColorList.Add(Color.LightGray);
                groupBandColorList.Add(Color.LightBlue);
                //bool isSetGroupingNewPage = false;
                foreach (DesignedReportGroupColumn item in groupColumnList)
                {
                    StiGroupHeaderBand groupHeaderUserBand = new StiGroupHeaderBand();
                    groupHeaderUserBand.Name = "groupHeader" + item.Column.PersonInfo.EnName + "Band";
                    groupHeaderUserBand.Height = 0.6;
                    groupHeaderUserBand.NewPageBefore = item.IsGroupingNewPage;
                    groupHeaderUserBand.Condition = new StiGroupConditionExpression("{Concepts." + new BReport().GetDesignedReportColumnFieldNameForDesigned(item.Column) + "}");
                    Color groupBandRandomColor = GetRandomLightColorGroup();

                    while (groupBandColorList.Contains(groupBandRandomColor))
                    {
                        groupBandRandomColor = GetRandomLightColorGroup();

                    }
                    groupBandColorList.Add(groupBandRandomColor);
                    groupHeaderUserBand.Brush = new StiSolidBrush(groupBandRandomColor);
                    groupHeaderUserBand.Border.Side = StiBorderSides.All;

                    StiText titleTextGroup = new StiText(new RectangleD(page.Width / 2, 0, page.Width / 2, 0.6));

                    switch (item.Column.PersonInfo.Key)
                    {

                        case DesignedReportPersonInfoKeyColumn.Sex:
                            titleTextGroup.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(new BReport().GetDesignedReportColumnFieldNameForDesigned(item.Column)) + "==false,\"" + item.Column.Name + " : " + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "Man").Name + "\",\"" + item.Column.Name + " : " + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "Woman").Name + "\")}";
                            break;

                        default:
                            titleTextGroup.Text = "{\"" + item.Column.Name + " : " + " \" + Concepts." + new BReport().GetDesignedReportColumnFieldNameForDesigned(item.Column) + "}";
                            break;
                    }
                    titleTextGroup.Font = groupBandFont;
                    titleTextGroup.TextOptions.RightToLeft = true;
                    titleTextGroup.Name = "nameTextGroup" + item.Column.PersonInfo.EnName;
                    titleTextGroup.HorAlignment = StiTextHorAlignment.Center;
                    titleTextGroup.VertAlignment = StiVertAlignment.Center;
                    groupHeaderUserBand.Components.Add(titleTextGroup);
                    //if (groupingNewPage && !isSetGroupingNewPage)
                    //{
                    //    groupHeaderUserBand.NewPageBefore = true;
                    //    isSetGroupingNewPage = true;
                    //}
                    page.Components.Add(groupHeaderUserBand);
                }



                StiReportSummaryBand reportSummaryBand = new StiReportSummaryBand();

                int groupDailyReportColumnLength = Enum.GetNames(typeof(GroupDailyReportColumnEnum)).Length;
                switch (reportType)
                {
                    case DesignedReportTypeEnum.Daily:
                        StiGroupHeaderBand groupHeaderBarcodeBand = new StiGroupHeaderBand();

                        groupHeaderBarcodeBand.Name = "groupHeaderBarcodeBand";
                        groupHeaderBarcodeBand.Height = 0.6;
                        groupHeaderBarcodeBand.Condition = new StiGroupConditionExpression("{Concepts.Prs_ID}");
                        StiSolidBrush groupBarcodeBrush = new StiSolidBrush(Color.LightSkyBlue);
                        groupHeaderBarcodeBand.Brush = groupBarcodeBrush;
                        groupHeaderBarcodeBand.Border.Side = StiBorderSides.All;
                        StiText barcodeTextGroup = new StiText(new RectangleD(0, 0, page.Width / 2, 0.6));
                        barcodeTextGroup.Text = "{\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "BarcodeGroup").Name + " \" + Concepts.Barcode}";
                        barcodeTextGroup.Font = groupBandFont;

                        barcodeTextGroup.Name = "barcodeTextGroupMain";
                        barcodeTextGroup.HorAlignment = StiTextHorAlignment.Center;
                        barcodeTextGroup.VertAlignment = StiVertAlignment.Center;
                        switch (BLanguage.CurrentLocalLanguage)
                        {

                            case LanguagesName.Parsi:
                                barcodeTextGroup.TextOptions.RightToLeft = true;
                                break;
                            case LanguagesName.English:
                                barcodeTextGroup.TextOptions.RightToLeft = false;
                                break;
                            default:
                                break;
                        }
                        groupHeaderBarcodeBand.Components.Add(barcodeTextGroup);

                        StiText NameTextGroup = new StiText(new RectangleD(page.Width / 2, 0, page.Width / 2, 0.6));

                        NameTextGroup.Font = groupBandFont;

                        NameTextGroup.Name = "NameTextGroupMain";
                        NameTextGroup.VertAlignment = StiVertAlignment.Center;
                        NameTextGroup.HorAlignment = StiTextHorAlignment.Center;
                        switch (BLanguage.CurrentLocalLanguage)
                        {

                            case LanguagesName.Parsi:
                                NameTextGroup.TextOptions.RightToLeft = true;
                                break;
                            case LanguagesName.English:
                                NameTextGroup.TextOptions.RightToLeft = false;
                                break;
                            default:
                                break;
                        }
                        if (designedReportColumnList.Count(c => c.ColumnName == "Prs_FirstName") > 0 && designedReportColumnList.Count(c => c.ColumnName == "Prs_LastName") == 0)
                        {
                            NameTextGroup.Text = "{\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "FirstLastName").Name + " \" + Concepts.Prs_FirstName}";
                            groupHeaderBarcodeBand.Components.Add(NameTextGroup);
                        }
                        else if (designedReportColumnList.Count(c => c.ColumnName == "Prs_FirstName") == 0 && designedReportColumnList.Count(c => c.ColumnName == "Prs_LastName") > 0)
                        {
                            NameTextGroup.Text = "{\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "FirstLastName").Name + " \" + Concepts.Prs_LastName}";
                            groupHeaderBarcodeBand.Components.Add(NameTextGroup);
                        }
                        else if (designedReportColumnList.Count(c => c.ColumnName == "Prs_FirstName") > 0 && designedReportColumnList.Count(c => c.ColumnName == "Prs_LastName") > 0)
                        {
                            NameTextGroup.Text = "{\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "FirstLastName").Name + " \" + Concepts.Prs_FirstName + \" \" + Concepts.Prs_LastName}";
                            groupHeaderBarcodeBand.Components.Add(NameTextGroup);
                        }


                        groupHeaderBarcodeBand.SortDirection = StiGroupSortDirection.Ascending;

                        //if (groupingNewPage)
                        //{
                        //    groupHeaderBarcodeBand.NewPageBefore = true;
                        //    isSetGroupingNewPage = true;
                        //}
                        page.Components.Add(groupHeaderBarcodeBand);



                        groupFooterBand.Name = "groupFooterBandMain";
                        groupFooterBand.Height = dataRowHeight;
                        groupFooterBand.Width = page.Width;
                        StiSolidBrush groupFooterBrush = new StiSolidBrush(Color.LightGray);
                        groupFooterBand.Brush = groupFooterBrush;
                        groupFooterBand.Border.Side = StiBorderSides.All;



                        switch (paramType)
                        {
                            case DesignedReportParameterType.DateRange:
                                if (designedReportColumnList.Count(c => c.Concept != null) > 0)
                                    dtReport = reportBusiness.GetMonthlyReport_DateRange_DesignedReports(reportId, currentPersonId, paramValues, personList.Select(p => p.ID).ToList<decimal>());
                                else if (designedReportColumnList.Count(c => c.Traffic != null) > 0)
                                    dtReport = reportBusiness.GetTrafficQuery_DesignedReports(reportId, currentPersonId, paramValues, personList.Select(p => p.ID).ToList<decimal>());
                                break;
                            case DesignedReportParameterType.FromToDate:
                                if (designedReportColumnList.Count(c => c.Concept != null) > 0)
                                    dtReport = reportBusiness.GetMonthlyReport_FromToDate_DesignedReports(reportId, currentPersonId, paramValues, personList.Select(p => p.ID).ToList<decimal>());
                                else if (designedReportColumnList.Count(c => c.Traffic != null) > 0)
                                    dtReport = reportBusiness.GetTrafficQuery_DesignedReports(reportId, currentPersonId, paramValues, personList.Select(p => p.ID).ToList<decimal>());
                                break;
                            default:
                                break;
                        }

                        if (designedReportColumnList.Count(d => d.ColumnName == "Prs_FirstName") == 0)
                            groupDailyReportColumnLength--;
                        if (designedReportColumnList.Count(d => d.ColumnName == "Prs_LastName") == 0)
                            groupDailyReportColumnLength--;

                        break;
                    case DesignedReportTypeEnum.Monthly:
                        reportSummaryBand.Name = "reportSummaryBandMain";
                        reportSummaryBand.Height = dataRowHeight;
                        reportSummaryBand.Width = page.Width;
                        StiSolidBrush reportSummaryBrush = new StiSolidBrush(Color.LightBlue);
                        reportSummaryBand.Brush = reportSummaryBrush;
                        reportSummaryBand.Border.Side = StiBorderSides.All;
                        switch (paramType)
                        {
                            case DesignedReportParameterType.DateRange:
                                dtReport = reportBusiness.GetMonthlySummeryReport_DateRange_DesignedReports(reportId, currentPersonId, paramValues, personList.Select(p => p.ID).ToList<decimal>());
                                break;
                            case DesignedReportParameterType.FromToDate:
                                dtReport = reportBusiness.GetMonthlySummeryReport_FromToDate_DesignedReports(reportId, currentPersonId, paramValues, personList.Select(p => p.ID).ToList<decimal>());
                                break;
                            default:
                                break;
                        }


                        break;
                    case DesignedReportTypeEnum.Person:
                        dtReport = reportBusiness.GetPersonInfoQuery_DesignedReports(reportId, personList.Select(p => p.ID).ToList<decimal>(), personId);

                        break;
                    default:
                        break;

                }
                DateTime fromDate = new DateTime();
                DateTime toDate = new DateTime();
                if (reportObj.DesignedType == new BDesignedReportsColumn().GetAllDesignedReportsTypes().FirstOrDefault(d => d.CustomCode == DesignedReportTypeEnum.Person))
                {
                    fromDate = DateTime.Now;
                    toDate = DateTime.Now;
                }
                else
                {
                    if (paramValues.Keys.Count(p => p == "fromDate") > 0 && paramValues.Keys.Count(p => p == "toDate") > 0)
                    {
                        fromDate = DateTime.Parse(paramValues["fromDate"].ToString(), CultureInfo.InvariantCulture);
                        toDate = DateTime.Parse(paramValues["toDate"].ToString(), CultureInfo.InvariantCulture);
                    }
                    else if (paramValues.Keys.Count(p => p == "@Order") > 0 && paramValues.Keys.Count(p => p == "@ToDate") > 0)
                    {
                        try
                        {
                            int i = 0;
                            DateRange dateRangePerson = null;
                            while (dateRangePerson == null)
                            {
                                Person personTempObj = null;
                                if (personList[i].AssignedScndCnpRangeList == null)
                                {
                                    personTempObj = new BPerson().GetByID(personList[i].ID);

                                }
                                else
                                    personTempObj = personList[i];
                                dateRangePerson = new BDateRange().GetDateRangePerson(personTempObj, 0, DateTime.Parse(paramValues["@ToDate"].ToString(), CultureInfo.InvariantCulture));
                                NHSession.Evict(personTempObj);
                                i++;
                            }
                            fromDate = dateRangePerson.FromDate;
                            toDate = dateRangePerson.ToDate;
                        }
                        catch (Exception)
                        {


                        }

                    }
                    else
                    {

                        if (dtReport.Rows.Count > 0)
                        {
                            fromDate = DateTime.Parse(dtReport.Rows[0]["FromDate"].ToString(), CultureInfo.InvariantCulture);
                            toDate = DateTime.Parse(dtReport.Rows[0]["ToDate"].ToString(), CultureInfo.InvariantCulture);
                        }
                    }
                }
                //if (dtReport.Rows.Count > 0 && designedReportColumnList.Count(c => c.IsConcept) > 0)
                // {
                StiText reportFromDateLabel_pageHeader = new StiText(new RectangleD(0, 0, 1.4, 0.5));
                reportFromDateLabel_pageHeader.Left = 28.2;
                reportFromDateLabel_pageHeader.Top = 0;
                reportFromDateLabel_pageHeader.Text = designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "FromDate").Name;
                reportFromDateLabel_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportFromDateLabel_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportFromDateLabel_pageHeader.Name = "reportFromDateLabel";
                reportFromDateLabel_pageHeader.Font = dataBandFont;
                reportFromDateLabel_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(89, 89, 89));
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportFromDateLabel_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportFromDateLabel_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }
                pageHeader.Components.Add(reportFromDateLabel_pageHeader);


                StiText reportFromDate_pageHeader = new StiText(new RectangleD(0, 0, 2.4, 0.5));
                reportFromDate_pageHeader.Left = 25.8;
                reportFromDate_pageHeader.Top = 0;
                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Unknown:
                        reportFromDate_pageHeader.Text = ReportHelper.Instance().MiladiToShamsi(fromDate);
                        break;
                    case LanguagesName.Parsi:
                        reportFromDate_pageHeader.Text = ReportHelper.Instance().MiladiToShamsi(fromDate);
                        break;
                    case LanguagesName.English:
                        reportFromDate_pageHeader.Text = ReportHelper.Instance().ToMiladiDate(fromDate);
                        break;
                    default:
                        break;
                }
                //if (dtReport.Rows[0]["FromDate"].ToString() != string.Empty)

                reportFromDate_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportFromDate_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportFromDate_pageHeader.Name = "reportFromDate";
                reportFromDate_pageHeader.Font = dataBandFont;
                reportFromDate_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(183, 117, 64));
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportFromDate_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportFromDate_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }
                pageHeader.Components.Add(reportFromDate_pageHeader);

                StiText reportToDateLabel_pageHeader = new StiText(new RectangleD(0, 0, 1.4, 0.5));
                reportToDateLabel_pageHeader.Left = 24.2;
                reportToDateLabel_pageHeader.Top = 0;
                reportToDateLabel_pageHeader.Text = designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "ToDate").Name;
                reportToDateLabel_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportToDateLabel_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportToDateLabel_pageHeader.Name = "reportToDateLabel";
                reportToDateLabel_pageHeader.Font = dataBandFont;
                reportToDateLabel_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(89, 89, 89));
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportToDateLabel_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportToDateLabel_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }
                pageHeader.Components.Add(reportToDateLabel_pageHeader);

                StiText reportToDate_pageHeader = new StiText(new RectangleD(0, 0, 2.4, 0.5));
                reportToDate_pageHeader.Left = 22;
                reportToDate_pageHeader.Top = 0;
                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Unknown:
                        reportToDate_pageHeader.Text = ReportHelper.Instance().MiladiToShamsi(toDate);
                        break;
                    case LanguagesName.Parsi:
                        reportToDate_pageHeader.Text = ReportHelper.Instance().MiladiToShamsi(toDate);
                        break;
                    case LanguagesName.English:
                        reportToDate_pageHeader.Text = ReportHelper.Instance().ToMiladiDate(toDate);
                        break;
                    default:
                        break;
                }
                //if (dtReport.Rows[0]["ToDate"].ToString() != string.Empty)

                reportToDate_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportToDate_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportToDate_pageHeader.Name = "reportToDate";
                reportToDate_pageHeader.Font = dataBandFont;
                reportToDate_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(183, 117, 64));
                switch (BLanguage.CurrentLocalLanguage)
                {

                    case LanguagesName.Parsi:
                        reportToDate_pageHeader.TextOptions.RightToLeft = true;
                        break;
                    case LanguagesName.English:
                        reportToDate_pageHeader.TextOptions.RightToLeft = false;
                        break;
                    default:
                        break;
                }
                pageHeader.Components.Add(reportToDate_pageHeader);
                //}

                if (designedReportColumnList.Count(c => c.ColumnType == DesignedReportColumnType.Traffic) > 0)
                {
                    IList<DesignedReportColumn> designedReportColumnTrafficsList = new List<DesignedReportColumn>();
                    dtReport = InitializeResultDesignedReportForTrafficColumns(dtReport, out designedReportColumnTrafficsList, personList, designedReportColumnList, reportId, fromDate, toDate);
                    foreach (DesignedReportColumn item in designedReportColumnTrafficsList)
                    {
                        designedReportColumnList.Add(item);
                    }
                    IList<DesignedReportColumn> designedReportColumnBasicList = designedReportColumnList.Where(d => d.Traffic != null && d.ColumnName == "BasicTraffic_Time").ToList();
                    foreach (DesignedReportColumn item in designedReportColumnBasicList)
                    {
                        designedReportColumnList.Remove(item);
                    }

                }

                InitializeDataTableForOutPutReport(dtReport, designedReportColumnList, outPutType, reportObj.Name);

                switch (reportType)
                {
                    case DesignedReportTypeEnum.Daily:
                        columnWidth = (page.Width - 1) / (dtReport.Columns.Count - groupDailyReportColumnLength - designedReportGroupColumnList.Count + 1);
                        break;
                    case DesignedReportTypeEnum.Monthly:
                        columnWidth = (page.Width - 1) / (dtReport.Columns.Count - Enum.GetNames(typeof(GroupMonthlyReportColumnEnum)).Length - designedReportGroupColumnList.Count + 1);
                        break;
                    case DesignedReportTypeEnum.Person:
                        columnWidth = (page.Width - 1) / (dtReport.Columns.Count - Enum.GetNames(typeof(GroupPersonColumnEnum)).Length - designedReportGroupColumnList.Count);
                        break;
                    default:
                        break;
                }


                double pos = 0;




                report.RegData(dtReport);
                report.Dictionary.Synchronize();

                StiHeaderBand headerBand = new StiHeaderBand();
                headerBand.Height = 0.7;
                headerBand.Name = "HeaderBand";

                page.Components.Add(headerBand);



                List<string> sortList = new List<string>();
                if (designedReportConditionObj != null && designedReportConditionObj.OrderValue != null && designedReportConditionObj.OrderValue != "")
                    sortList = ConvertSqlOrderQueryToStimulSoftSort(designedReportConditionObj.OrderValue);

                switch (reportType)
                {
                    case DesignedReportTypeEnum.Daily:
                        sortList.Add("ASC");
                        sortList.Add("Barcode");
                        sortList.Add("ASC");
                        sortList.Add("Date");

                        break;
                    case DesignedReportTypeEnum.Monthly:
                        sortList.Add("ASC");
                        sortList.Add("Barcode");
                        break;
                    case DesignedReportTypeEnum.Person:
                        sortList.Add("ASC");
                        sortList.Add("Barcode");
                        break;
                    default:
                        break;
                }

                dataBand.Sort = sortList.ToArray();
                StiCondition conditionDataBand = new StiCondition("{Line%2==0}", Color.Black, Color.PeachPuff, dataBandFont, true);
                dataBand.Conditions.Add(conditionDataBand);


                page.Components.Add(dataBand);



                int nameIndex = 1;
                //IList<PersonReserveField> personReserveFieldList = new List<PersonReserveField>();
                //if(designedReportColumnList.Count(d=>d.PersonInfo!=null && d.PersonInfo.IsReserveField)>0)
                //   personReserveFieldList = new BPersonReservedField().GetAll();
                for (int i = dtReport.Columns.Count - 1; i >= 0; i--)
                {

                    if (designedReportGroupColumnList.Count(d => d.Column.ColumnName == dtReport.Columns[i].Caption) == 0)
                    {
                        StiText hText = new StiText(new RectangleD(pos, 0, columnWidth, 0.7));


                        if (designedReportColumnList.Count(c => c.Concept != null && c.Concept.KeyColumnName == dtReport.Columns[i].ColumnName) > 0)
                        {
                            hText.Text.Value = designedReportColumnList.SingleOrDefault(c => c.Concept != null && c.Concept.KeyColumnName == dtReport.Columns[i].ColumnName).Title;
                        }
                        else if (designedReportColumnList.Count(c => c.PersonInfo != null && reportBusiness.GetDesignedReportColumnFieldNameForDesigned(c) == dtReport.Columns[i].ColumnName) > 0)
                        {
                            DesignedReportColumn designedReportColumnPersonInfoObj = designedReportColumnList.SingleOrDefault(c => c.PersonInfo != null && reportBusiness.GetDesignedReportColumnFieldNameForDesigned(c) == dtReport.Columns[i].ColumnName);

                            hText.Text.Value = designedReportColumnPersonInfoObj.Title;
                        }
                        else if (designedReportColumnList.Count(c => c.Traffic != null && c.ColumnName == dtReport.Columns[i].ColumnName) > 0)
                        {
                            hText.Text.Value = designedReportColumnList.SingleOrDefault(c => c.Traffic != null && c.ColumnName == dtReport.Columns[i].ColumnName).Title;
                        }
                        else if (designedReportStaticColumnList.Count(c => c.KeyName == dtReport.Columns[i].Caption) > 0)
                        {
                            hText.Text.Value = designedReportStaticColumnList.SingleOrDefault(c => c.KeyName == dtReport.Columns[i].ColumnName).Name;
                        }
                        else if (designedReportColumnList.Count(c => c.PersonParam != null && c.ColumnName == dtReport.Columns[i].ColumnName) > 0)
                        {
                            hText.Text.Value = personParamFieldList.SingleOrDefault(c => c.Key == dtReport.Columns[i].ColumnName).Name;
                        }

                        hText.HorAlignment = StiTextHorAlignment.Center;
                        hText.VertAlignment = StiVertAlignment.Center;
                        hText.Name = "HeaderText" + nameIndex.ToString();
                        hText.Brush = new StiSolidBrush(Color.FromArgb(191, 191, 191));
                        hText.Border.Side = StiBorderSides.All;
                        hText.Font = headerBandFont;
                        hText.WordWrap = true;
                        SizeD sizeWrap = new SizeD();
                        sizeWrap.Width = columnWidth;
                        sizeWrap.Height = 1.4;
                        hText.MaxSize = sizeWrap;
                        hText.ShrinkFontToFit = true;
                        StiText dataText = new StiText(new RectangleD(pos, 0, columnWidth, dataRowHeight));
                        DesignedReportColumn designedReportColumnObj = null;
                        if (designedReportColumnList.Count(c => c.Concept != null && c.Concept.KeyColumnName == dtReport.Columns[i].ColumnName) > 0)
                        {
                            designedReportColumnObj = designedReportColumnList.SingleOrDefault(c => c.Concept != null && c.Concept.KeyColumnName == dtReport.Columns[i].ColumnName);
                        }
                        else if (designedReportColumnList.Count(c => c.PersonInfo != null && reportBusiness.GetDesignedReportColumnFieldNameForDesigned(c) == dtReport.Columns[i].ColumnName) > 0)
                        {
                            designedReportColumnObj = designedReportColumnList.SingleOrDefault(c => c.PersonInfo != null && reportBusiness.GetDesignedReportColumnFieldNameForDesigned(c) == dtReport.Columns[i].ColumnName);
                        }
                        else if (designedReportColumnList.Count(c => c.Traffic != null && c.ColumnName == dtReport.Columns[i].ColumnName) > 0)
                        {
                            designedReportColumnObj = designedReportColumnList.SingleOrDefault(c => c.Traffic != null && c.ColumnName == dtReport.Columns[i].ColumnName);
                        }
                        if (designedReportColumnObj != null)
                        {
                            if (designedReportColumnObj.Concept != null)
                            {
                                if (designedReportColumnObj.Concept.IsHourly)
                                {

                                    dataText.Text = "{ReportHelper.Instance().IntTimeToTime(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + ")}";
                                }
                                else

                                    dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==-1000,0,Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + ")}";
                            }
                            else if (designedReportColumnObj.PersonInfo != null)
                            {
                                switch (designedReportColumnObj.PersonInfo.Key)
                                {

                                    case DesignedReportPersonInfoKeyColumn.Sex:
                                        dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==false,\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "Man").Name + "\",\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "Woman").Name + "\")}";
                                        break;
                                    case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthHour:
                                    case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearHour:
                                        dataText.Text = "{ReportHelper.Instance().IntTimeToTime(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + ")}";
                                        break;
                                    case DesignedReportPersonInfoKeyColumn.EmploymentDate:
                                        switch (BLanguage.CurrentSystemLanguage)
                                        {
                                            case LanguagesName.Unknown:
                                                dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==ReportHelper.Instance().GTSMinStandardDateTime(),\"\",ReportHelper.Instance().MiladiToShamsi(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "))}";
                                                break;
                                            case LanguagesName.Parsi:
                                                dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==ReportHelper.Instance().GTSMinStandardDateTime(),\"\",ReportHelper.Instance().MiladiToShamsi(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "))}";
                                                break;
                                            case LanguagesName.English:
                                                dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==ReportHelper.Instance().GTSMinStandardDateTime(),\"\",ReportHelper.Instance().ToMiladiDate(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "))}";
                                                break;
                                            default:
                                                break;
                                        }

                                        break;
                                    case DesignedReportPersonInfoKeyColumn.EndEmploymentDate:
                                        switch (BLanguage.CurrentSystemLanguage)
                                        {
                                            case LanguagesName.Unknown:
                                                dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==ReportHelper.Instance().GTSMinStandardDateTime(),\"\",ReportHelper.Instance().MiladiToShamsi(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "))}";
                                                break;
                                            case LanguagesName.Parsi:
                                                dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==ReportHelper.Instance().GTSMinStandardDateTime(),\"\",ReportHelper.Instance().MiladiToShamsi(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "))}";
                                                break;
                                            case LanguagesName.English:
                                                dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==ReportHelper.Instance().GTSMinStandardDateTime(),\"\",ReportHelper.Instance().ToMiladiDate(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "))}";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case DesignedReportPersonInfoKeyColumn.PersonActive:
                                        dataText.Text = "{IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==false,\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "DeActive").Name + "\",\"" + designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "Active").Name + "\")}";
                                        break;
                                    default:
                                        dataText.Text = "{Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "}";
                                        break;
                                }


                            }
                            else if (designedReportColumnObj.Traffic != null)
                            {
                                dataText.Text = "{ReportHelper.Instance().IntTimeToTime(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + ")}";
                            }

                        }

                        else
                        {
                            dataText.Text = "{Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "}";
                        }
                        dataText.Type = StiSystemTextType.DataColumn;

                        dataText.HorAlignment = StiTextHorAlignment.Center;
                        dataText.VertAlignment = StiVertAlignment.Center;
                        dataText.Name = "DataText" + nameIndex.ToString();
                        dataText.Border.Side = StiBorderSides.All;
                        dataText.Font = dataBandFont;
                        dataText.ShrinkFontToFit = true;
                        DesignedReportTypeEnum designedReportTypeEnum = reportObj.DesignedType.CustomCode;
                        bool isCreatedColumn = false;
                        hText.Interaction = new StiInteraction();
                        hText.Interaction.SortingColumn = dataBand.Name + "." + dtReport.Columns[i].ColumnName;
                        hText.Interaction.SortingEnabled = true;

                        switch (designedReportTypeEnum)
                        {
                            case DesignedReportTypeEnum.Daily:
                                if (!Enum.IsDefined(typeof(GroupDailyReportColumnEnum), dtReport.Columns[i].Caption))
                                {

                                    headerBand.Components.Add(hText);
                                    dataBand.Components.Add(dataText);

                                    isCreatedColumn = true;
                                }
                                break;
                            case DesignedReportTypeEnum.Monthly:
                                if (!Enum.IsDefined(typeof(GroupMonthlyReportColumnEnum), dtReport.Columns[i].Caption))
                                {

                                    headerBand.Components.Add(hText);
                                    dataBand.Components.Add(dataText);
                                    isCreatedColumn = true;
                                }
                                break;
                            case DesignedReportTypeEnum.Person:
                                if (!Enum.IsDefined(typeof(GroupPersonColumnEnum), dtReport.Columns[i].Caption))
                                {

                                    headerBand.Components.Add(hText);
                                    dataBand.Components.Add(dataText);
                                    isCreatedColumn = true;
                                }
                                break;
                            default:
                                break;
                        }

                        if (designedReportColumnList.Count(d => d.Concept != null && d.Concept.KeyColumnName == dtReport.Columns[i].ColumnName) > 0)
                        {
                            StiText groupFooterText = new StiText(new RectangleD(pos, 0, columnWidth, dataRowHeight));
                            if (designedReportColumnObj.Concept.IsHourly)
                                groupFooterText.Text = "{ReportHelper.Instance().IntTimeToTime(Sum(IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==-1000,0,Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + ")))}";
                            else
                                groupFooterText.Text = "{Sum(IIF(Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "==-1000,0,Concepts." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + "))}";
                            groupFooterText.HorAlignment = StiTextHorAlignment.Center;
                            groupFooterText.VertAlignment = StiVertAlignment.Center;
                            groupFooterText.Name = "GroupFooterText" + nameIndex.ToString();
                            groupFooterText.Border.Side = StiBorderSides.All;
                            groupFooterText.Height = dataRowHeight;
                            groupFooterText.Font = dataBandFont;
                            switch (reportType)
                            {
                                case DesignedReportTypeEnum.Daily:
                                    groupFooterBand.Components.Add(groupFooterText);
                                    break;
                                case DesignedReportTypeEnum.Monthly:
                                    reportSummaryBand.Components.Add(groupFooterText);
                                    break;
                                default:
                                    break;
                            }


                        }
                        if (isCreatedColumn)
                        {
                            pos = pos + columnWidth;
                            nameIndex++;
                        }
                    }
                }

                StiText hTextLine = new StiText(new RectangleD(pos, 0, 1, 0.7));
                hTextLine.Text.Value = designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "NoLine").Name;
                hTextLine.HorAlignment = StiTextHorAlignment.Center;
                hTextLine.VertAlignment = StiVertAlignment.Center;
                hTextLine.Name = "HeaderText" + nameIndex.ToString();
                hTextLine.Brush = new StiSolidBrush(Color.FromArgb(191, 191, 191));
                hTextLine.Border.Side = StiBorderSides.All;
                hTextLine.Font = headerBandFont;
                headerBand.Components.Add(hTextLine);


                StiText dataTextLine = new StiText(new RectangleD(pos, 0, 1, dataRowHeight));

                dataTextLine.Text = "{Line}";
                dataTextLine.HorAlignment = StiTextHorAlignment.Center;
                dataTextLine.VertAlignment = StiVertAlignment.Center;
                dataTextLine.Name = "DataText" + nameIndex.ToString();
                dataTextLine.Border.Side = StiBorderSides.All;
                dataTextLine.Font = dataBandFont;
                dataBand.Components.Add(dataTextLine);

                switch (reportType)
                {
                    case DesignedReportTypeEnum.Daily:
                        page.Components.Add(groupFooterBand);
                        break;
                    case DesignedReportTypeEnum.Monthly:
                        page.Components.Add(reportSummaryBand);
                        break;
                    default:
                        break;
                }
                if (dtReport.Rows.Count == 0)
                {
                    StiText emptyDataStiText = new StiText(new RectangleD(0, 0, page.Width, 5));
                    Font emptyDataFont = new Font("Tahoma", 24, FontStyle.Regular);
                    emptyDataStiText.Font = emptyDataFont;
                    emptyDataStiText.HorAlignment = StiTextHorAlignment.Center;
                    emptyDataStiText.VertAlignment = StiVertAlignment.Center;
                    emptyDataStiText.Top = pageHeader.Top + pageHeader.Height + 1;
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Unknown:
                            emptyDataStiText.Text = "رکوردی جهت نمایش وجود ندارد";
                            break;
                        case LanguagesName.Parsi:
                            emptyDataStiText.Text = "رکوردی جهت نمایش وجود ندارد";
                            break;
                        case LanguagesName.English:
                            emptyDataStiText.Text = "Record Not Exist";
                            break;
                        default:
                            break;
                    }
                    page.Components.Add(emptyDataStiText);


                }
                HttpContext.Current.Session[report.ReportName] = dtReport;
                return report;

            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameter>.LogException(ex, "BReportParameter", "InitializeDesignedReport");
                throw ex;
            }
        }
        private System.Drawing.Color GetRandomLightColorGroup()
        {
            Random randomGen = new Random();
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(91, 103)];
            Color randomColor = Color.FromKnownColor(randomColorName);
            return randomColor;

        }

        /// <summary>
        /// یک فایل گزارش را میگرداند
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        private ReportFile GetReportFile(decimal fileId)
        {
            EntityRepository<ReportFile> reportFileReposiory = new EntityRepository<ReportFile>();
            ReportFile file = reportFileReposiory.GetById(fileId, false);
            return file;
        }
        private List<string> ConvertSqlOrderQueryToStimulSoftSort(string queryOrderSql)
        {
            List<string> resultList = new List<string>();

            string[] arr = queryOrderSql.Split(',');
            for (int i = 0; i < arr.Count(); i++)
            {

                if (arr[i].Contains("(ASC)"))
                {
                    resultList.Add("ASC");
                    arr[i] = arr[i].Replace("(ASC)", "");
                    resultList.Add(arr[i].Trim());
                }
                else if (arr[i].Contains("(DESC)"))
                {
                    resultList.Add("DESC");
                    arr[i] = arr[i].Replace("(DESC)", "");
                    resultList.Add(arr[i].Trim());
                }
                else
                {
                    resultList.Add("ASC");
                    resultList.Add(arr[i].Trim());
                }

            }
            return resultList;
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckReportParametersLoadAccess()
        {
        }
        public DataTable InitializeResultDesignedReportForTrafficColumns(DataTable dataTable, out IList<DesignedReportColumn> designedReportColumnList, IList<Person> personList, IList<DesignedReportColumn> oldDesignedReportColumnList, decimal reportId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                designedReportColumnList = new List<DesignedReportColumn>();

                IList<PrecardGroups> precardGroupList = new BPrecard().GetAllPrecardGroups();
                IList<Precard> PrecardList = new BPrecard().GetAllByPrecardGroup(precardGroupList.Where(p => p.LookupKey == "traffic").FirstOrDefault().ID);

                IList<decimal> personIdList = personList.Select(x => x.ID).Distinct().ToList();
                IList<decimal> precardIdList = PrecardList.Select(p => p.ID).ToList();
                IList<BasicTraffic> basicTrafficList = new BTraffic().GetTrafficPersons(personIdList, precardIdList, fromDate, toDate);
                DataTable dtResult = dataTable.Clone();
                DesignedReportStaticColumn designedReportStaticColumnObj = new BDesignedReportsColumn().GetAllDesignedReportsStaticColumn().FirstOrDefault(c => c.KeyName == "Traffic");
                DesignedReportColumn designedReportColumnTrafficObj = oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).SingleOrDefault(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.AllTraffic);
                int maxColumnTrafficCount = 1000000;
                if (designedReportColumnTrafficObj != null && designedReportColumnTrafficObj.TrafficColumnCount != 0)
                    maxColumnTrafficCount = designedReportColumnTrafficObj.TrafficColumnCount;
                int columnTrafficCount = 0;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    int trafficRowCount = 1;
                    if (dtResult.Select("Prs_ID='" + dataTable.Rows[i]["Prs_ID"].ToString() + "' AND Date='" + dataTable.Rows[i]["Date"].ToString() + "'").Count() == 0)
                    {

                        DataRow rowObj = dtResult.NewRow();
                        dtResult.Rows.Add(rowObj);
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            dtResult.Rows[dtResult.Rows.Count - 1][dataTable.Columns[j].ColumnName] = dataTable.Rows[i][dataTable.Columns[j].ColumnName];
                        }

                        IList<BasicTraffic> basicTrafficPersonList = basicTrafficList.Where(t => t.Date == Utility.ToMildiDate(dataTable.Rows[i]["Date"].ToString()) && t.Person.ID == Convert.ToDecimal(dataTable.Rows[i]["Prs_ID"], CultureInfo.InvariantCulture)).OrderBy(o => o.Time).ToList();

                        if (oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.AllTraffic) > 0)
                        {
                            if (basicTrafficPersonList != null)
                            {

                                foreach (BasicTraffic item in basicTrafficPersonList)
                                {

                                    if (trafficRowCount > columnTrafficCount && trafficRowCount <= maxColumnTrafficCount)
                                    {
                                        dtResult.Columns.Add("BasicTraffic_Time" + (columnTrafficCount + 1).ToString(), typeof(decimal));
                                        DesignedReportColumn designedReportColumnObj = new DesignedReportColumn();
                                        designedReportColumnObj.Active = true;
                                        if (designedReportStaticColumnObj != null)
                                            designedReportColumnObj.ColumnName = "BasicTraffic_Time" + (columnTrafficCount + 1).ToString();
                                        designedReportColumnObj.ColumnType = DesignedReportColumnType.Traffic;
                                        designedReportColumnObj.Concept = null;
                                        designedReportColumnObj.PersonInfo = null;
                                        designedReportColumnObj.Traffic = new DesignedReportTrafficColumn();
                                        designedReportColumnObj.Order = -1;
                                        designedReportColumnObj.Name = designedReportStaticColumnObj.Name + " " + (columnTrafficCount + 1).ToString();
                                        designedReportColumnObj.Title = designedReportStaticColumnObj.Name + " " + (columnTrafficCount + 1).ToString();
                                        designedReportColumnList.Add(designedReportColumnObj);
                                        columnTrafficCount++;
                                    }

                                    if (trafficRowCount <= maxColumnTrafficCount)
                                    {
                                        dtResult.Rows[dtResult.Rows.Count - 1]["BasicTraffic_Time" + trafficRowCount] = item.Time;
                                    }
                                    trafficRowCount++;
                                    basicTrafficList.Remove(item);


                                }


                            }
                        }
                        else
                        {
                            if (oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic) > 0)
                            {


                                BasicTraffic basicTrafficObj = basicTrafficPersonList.Where(c => c.Person.ID == Convert.ToDecimal(dataTable.Rows[i]["Prs_ID"], CultureInfo.InvariantCulture) && c.Date == Utility.ToMildiDate(dataTable.Rows[i]["Date"].ToString())).OrderBy(o => o.Time).FirstOrDefault();
                                if (basicTrafficObj != null)
                                {
                                    if (!dtResult.Columns.Contains("BasicTraffic_Time_First"))
                                    {
                                        dtResult.Columns.Add("BasicTraffic_Time_First", typeof(decimal));
                                        DesignedReportColumn designedReportColumnObj = new DesignedReportColumn();
                                        designedReportColumnObj.Active = true;
                                        if (designedReportStaticColumnObj != null)
                                            designedReportColumnObj.ColumnName = "BasicTraffic_Time_First";
                                        designedReportColumnObj.ColumnType = DesignedReportColumnType.Traffic;
                                        designedReportColumnObj.Concept = null;
                                        designedReportColumnObj.PersonInfo = null;
                                        designedReportColumnObj.Traffic = new DesignedReportTrafficColumn();
                                        designedReportColumnObj.Order = -1;
                                        string columnTitle = new BDesignedReportsColumn().GetAllDesignedReportsTrafficColumns().SingleOrDefault(c => c.Key == DesignedReportTrafficKeyColumn.FirstTraffic).Name;
                                        designedReportColumnObj.Name = columnTitle;
                                        designedReportColumnObj.Title = columnTitle;
                                        designedReportColumnList.Add(designedReportColumnObj);
                                        columnTrafficCount++;
                                    }


                                    dtResult.Rows[dtResult.Rows.Count - 1]["BasicTraffic_Time_First"] = basicTrafficObj.Time;
                                    foreach (BasicTraffic item in basicTrafficPersonList)
                                    {
                                        basicTrafficList.Remove(item);
                                    }

                                }

                            }
                            if (oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic) > 0)
                            {
                                if (basicTrafficPersonList.Count > 1)
                                {
                                    BasicTraffic basicTrafficObj = basicTrafficPersonList.Where(c => c.Person.ID == Convert.ToDecimal(dataTable.Rows[i]["Prs_ID"], CultureInfo.InvariantCulture) && c.Date == Utility.ToMildiDate(dataTable.Rows[i]["Date"].ToString())).OrderByDescending(o => o.Time).FirstOrDefault();
                                    if (basicTrafficObj != null)
                                    {
                                        if (!dtResult.Columns.Contains("BasicTraffic_Time_Last"))
                                        {
                                            dtResult.Columns.Add("BasicTraffic_Time_Last", typeof(decimal));
                                            DesignedReportColumn designedReportColumnObj = new DesignedReportColumn();
                                            designedReportColumnObj.Active = true;
                                            if (designedReportStaticColumnObj != null)
                                                designedReportColumnObj.ColumnName = "BasicTraffic_Time_Last";
                                            designedReportColumnObj.ColumnType = DesignedReportColumnType.Traffic;
                                            designedReportColumnObj.Concept = null;
                                            designedReportColumnObj.PersonInfo = null;
                                            designedReportColumnObj.Traffic = new DesignedReportTrafficColumn();
                                            designedReportColumnObj.Order = -1;
                                            string columnTitle = new BDesignedReportsColumn().GetAllDesignedReportsTrafficColumns().SingleOrDefault(c => c.Key == DesignedReportTrafficKeyColumn.LastTraffic).Name;
                                            designedReportColumnObj.Name = columnTitle;
                                            designedReportColumnObj.Title = columnTitle;
                                            designedReportColumnList.Add(designedReportColumnObj);
                                            columnTrafficCount++;
                                        }


                                        dtResult.Rows[dtResult.Rows.Count - 1]["BasicTraffic_Time_Last"] = basicTrafficObj.Time;
                                        foreach (BasicTraffic item in basicTrafficPersonList)
                                        {
                                            basicTrafficList.Remove(item);
                                        }

                                    }
                                }
                            }

                        }

                    }

                }
                if (oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.AllTraffic) > 0)
                {
                    foreach (BasicTraffic item in basicTrafficList)
                    {
                        if (dtResult.Select("Prs_ID='" + item.Person.ID + "' AND Date='" + Utility.ToPersianDate(item.Date) + "'").Count() == 0)
                        {
                            DataRow row2Obj = dtResult.NewRow();
                            dtResult.Rows.Add(row2Obj);
                            dtResult.Rows[dtResult.Rows.Count - 1]["Date"] = Utility.ToPersianDate(item.Date);
                            dtResult.Rows[dtResult.Rows.Count - 1]["Prs_ID"] = item.Person.ID;
                            dtResult.Rows[dtResult.Rows.Count - 1]["Barcode"] = item.Person.BarCode;
                            dtResult.Rows[dtResult.Rows.Count - 1]["ToDate"] = item.Date;
                            dtResult.Rows[dtResult.Rows.Count - 1]["FromDate"] = item.Date;
                            foreach (DataColumn column in dtResult.Columns)
                            {
                                if (new BReport().GetPersonDetailInfo_DesignedReports(column.ColumnName, item.Person) != string.Empty)
                                    dtResult.Rows[dtResult.Rows.Count - 1][column.ColumnName] = new BReport().GetPersonDetailInfo_DesignedReports(column.ColumnName, item.Person);
                            }
                            IList<BasicTraffic> basicTrafficPersonList = basicTrafficList.Where(b => b.Person.ID == item.Person.ID && b.Date == item.Date).OrderBy(o => o.Time).ToList();
                            int trafficRowCount = 1;
                            foreach (BasicTraffic basicObj in basicTrafficPersonList)
                            {
                                if (trafficRowCount > columnTrafficCount && trafficRowCount <= maxColumnTrafficCount)
                                {
                                    dtResult.Columns.Add("BasicTraffic_Time" + (columnTrafficCount + 1).ToString(), typeof(decimal));
                                    DesignedReportColumn designedReportColumnObj = new DesignedReportColumn();
                                    designedReportColumnObj.Active = true;
                                    if (designedReportStaticColumnObj != null)
                                        designedReportColumnObj.ColumnName = "BasicTraffic_Time" + (columnTrafficCount + 1).ToString();
                                    designedReportColumnObj.ColumnType = DesignedReportColumnType.Traffic;
                                    designedReportColumnObj.Concept = null;
                                    designedReportColumnObj.PersonInfo = null;
                                    designedReportColumnObj.Traffic = new DesignedReportTrafficColumn();
                                    designedReportColumnObj.Order = -1;
                                    designedReportColumnObj.Name = designedReportStaticColumnObj.Name + " " + (columnTrafficCount + 1).ToString();
                                    designedReportColumnObj.Title = designedReportStaticColumnObj.Name + " " + (columnTrafficCount + 1).ToString();
                                    designedReportColumnList.Add(designedReportColumnObj);
                                    columnTrafficCount++;
                                }
                                if (trafficRowCount <= maxColumnTrafficCount)
                                {
                                    dtResult.Rows[dtResult.Rows.Count - 1]["BasicTraffic_Time" + trafficRowCount] = basicObj.Time;
                                }
                                trafficRowCount++;
                            }

                            for (int i = 0; i < dtResult.Columns.Count; i++)
                            {
                                if (oldDesignedReportColumnList.Count(d => d.PersonInfo != null && d.ColumnName == dtResult.Columns[i].ColumnName) > 0)
                                    dtResult.Rows[dtResult.Rows.Count - 1][dtResult.Columns[i]] = new BReport().GetDesignedReportPersonInfoColumnFieldValueForTrafficReports(dtResult.Columns[i].ColumnName, item);

                            }
                        }

                    }
                }
                else
                {

                    List<BasicTraffic> basicTrafficTempList = new List<BasicTraffic>();
                    foreach (BasicTraffic item in basicTrafficList)
                    {


                        if (basicTrafficTempList.Contains(item))
                            continue;
                        BasicTraffic basicFirstTrafficObj = null;
                        BasicTraffic basicLastTrafficObj = null;
                        if (oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic) > 0)
                        {
                            basicFirstTrafficObj = basicTrafficList.Where(c => c.Person.ID == item.Person.ID && c.Date == item.Date).OrderBy(o => o.Time).FirstOrDefault();
                        }
                        if (oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic) > 0)
                        {
                            if (basicTrafficList.Count(c => c.Person.ID == item.Person.ID && c.Date == item.Date) > 1)
                            {
                                basicLastTrafficObj = basicTrafficList.Where(c => c.Person.ID == item.Person.ID && c.Date == item.Date).OrderByDescending(o => o.Time).FirstOrDefault();
                            }
                        }
                        if (basicFirstTrafficObj != null)
                        {
                            if (!dtResult.Columns.Contains("BasicTraffic_Time_First"))
                            {
                                dtResult.Columns.Add("BasicTraffic_Time_First", typeof(decimal));
                                DesignedReportColumn designedReportColumnObj = new DesignedReportColumn();
                                designedReportColumnObj.Active = true;
                                if (designedReportStaticColumnObj != null)
                                    designedReportColumnObj.ColumnName = "BasicTraffic_Time_First";
                                designedReportColumnObj.ColumnType = DesignedReportColumnType.Traffic;
                                designedReportColumnObj.Concept = null;
                                designedReportColumnObj.PersonInfo = null;
                                designedReportColumnObj.Traffic = new DesignedReportTrafficColumn();
                                designedReportColumnObj.Order = -1;
                                string columnTitle = new BDesignedReportsColumn().GetAllDesignedReportsTrafficColumns().SingleOrDefault(c => c.Key == DesignedReportTrafficKeyColumn.FirstTraffic).Name;
                                designedReportColumnObj.Name = columnTitle;
                                designedReportColumnObj.Title = columnTitle;
                                designedReportColumnList.Add(designedReportColumnObj);
                                columnTrafficCount++;
                            }
                            if (dtResult.Select("Prs_ID='" + item.Person.ID + "' AND Date='" + Utility.ToPersianDate(item.Date) + "'").Count() == 0)
                            {


                                basicTrafficTempList.Add(basicFirstTrafficObj);


                                DataRow rowObj = dtResult.NewRow();
                                dtResult.Rows.Add(rowObj);
                                dtResult.Rows[dtResult.Rows.Count - 1]["Date"] = Utility.ToPersianDate(item.Date);
                                dtResult.Rows[dtResult.Rows.Count - 1]["Prs_ID"] = item.Person.ID;
                                dtResult.Rows[dtResult.Rows.Count - 1]["Barcode"] = item.Person.BarCode;
                                dtResult.Rows[dtResult.Rows.Count - 1]["ToDate"] = item.Date;
                                dtResult.Rows[dtResult.Rows.Count - 1]["FromDate"] = item.Date;
                                foreach (DataColumn column in dtResult.Columns)
                                {
                                    if (new BReport().GetPersonDetailInfo_DesignedReports(column.ColumnName, item.Person) != string.Empty)
                                        dtResult.Rows[dtResult.Rows.Count - 1][column.ColumnName] = new BReport().GetPersonDetailInfo_DesignedReports(column.ColumnName, item.Person);
                                }
                                IList<BasicTraffic> basicTrafficPersonList = basicTrafficList.Where(b => b.Person.ID == item.Person.ID && b.Date == item.Date).ToList();
                                dtResult.Rows[dtResult.Rows.Count - 1]["BasicTraffic_Time_First"] = basicFirstTrafficObj.Time;



                            }
                            else
                            {
                                int indexRow = dtResult.Rows.IndexOf(dtResult.Select("Prs_ID='" + item.Person.ID + "' AND Date='" + Utility.ToPersianDate(item.Date) + "'")[0]);
                                dtResult.Rows[indexRow]["BasicTraffic_Time_First"] = basicFirstTrafficObj.Time;
                            }
                        }
                        if (basicLastTrafficObj != null)
                        {
                            if (!dtResult.Columns.Contains("BasicTraffic_Time_Last"))
                            {
                                dtResult.Columns.Add("BasicTraffic_Time_Last", typeof(decimal));
                                DesignedReportColumn designedReportColumnObj = new DesignedReportColumn();
                                designedReportColumnObj.Active = true;
                                if (designedReportStaticColumnObj != null)
                                    designedReportColumnObj.ColumnName = "BasicTraffic_Time_Last";
                                designedReportColumnObj.ColumnType = DesignedReportColumnType.Traffic;
                                designedReportColumnObj.Concept = null;
                                designedReportColumnObj.PersonInfo = null;
                                designedReportColumnObj.Traffic = new DesignedReportTrafficColumn();
                                designedReportColumnObj.Order = -1;
                                string columnTitle = new BDesignedReportsColumn().GetAllDesignedReportsTrafficColumns().SingleOrDefault(c => c.Key == DesignedReportTrafficKeyColumn.LastTraffic).Name;
                                designedReportColumnObj.Name = columnTitle;
                                designedReportColumnObj.Title = columnTitle;
                                designedReportColumnList.Add(designedReportColumnObj);
                                columnTrafficCount++;
                            }
                            if (dtResult.Select("Prs_ID='" + item.Person.ID + "' AND Date='" + Utility.ToPersianDate(item.Date) + "'").Count() == 0)
                            {

                                basicTrafficTempList.Add(basicLastTrafficObj);


                                DataRow rowObj = dtResult.NewRow();
                                dtResult.Rows.Add(rowObj);
                                dtResult.Rows[dtResult.Rows.Count - 1]["Date"] = Utility.ToPersianDate(item.Date);
                                dtResult.Rows[dtResult.Rows.Count - 1]["Prs_ID"] = item.Person.ID;
                                dtResult.Rows[dtResult.Rows.Count - 1]["Barcode"] = item.Person.BarCode;
                                dtResult.Rows[dtResult.Rows.Count - 1]["ToDate"] = item.Date;
                                dtResult.Rows[dtResult.Rows.Count - 1]["FromDate"] = item.Date;
                                foreach (DataColumn column in dtResult.Columns)
                                {
                                    if (new BReport().GetPersonDetailInfo_DesignedReports(column.ColumnName, item.Person) != string.Empty)
                                        dtResult.Rows[dtResult.Rows.Count - 1][column.ColumnName] = new BReport().GetPersonDetailInfo_DesignedReports(column.ColumnName, item.Person);
                                }
                                IList<BasicTraffic> basicTrafficPersonList = basicTrafficList.Where(b => b.Person.ID == item.Person.ID && b.Date == item.Date).ToList();
                                dtResult.Rows[dtResult.Rows.Count - 1]["BasicTraffic_Time_Last"] = basicLastTrafficObj.Time;


                            }
                            else
                            {
                                int indexRow = dtResult.Rows.IndexOf(dtResult.Select("Prs_ID='" + item.Person.ID + "' AND Date='" + Utility.ToPersianDate(item.Date) + "'")[0]);
                                dtResult.Rows[indexRow]["BasicTraffic_Time_Last"] = basicLastTrafficObj.Time;
                            }
                        }
                    }


                }
                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == BUser.CurrentUser.Person.ID).FirstOrDefault();
                DataTable dtFinalResult = new DataTable();
                dtFinalResult = dtResult.Clone();
                if (designedReportConditionObj != null && designedReportConditionObj.TrafficConditionValue != "" && dtResult.Rows.Count > 0)
                {
                    DataRow[] dataRowCollection;
                    if (designedReportConditionObj.TrafficConditionValue.ToLower().Contains("basictraffic_time_all") && oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.AllTraffic) > 0)
                    {
                        string[] trafficConditionArray = designedReportConditionObj.TrafficConditionValue.ToLower().Split(new string[] { " and ", " or " }, StringSplitOptions.None);
                        string trafficAllColumn = trafficConditionArray.SingleOrDefault(c => c.ToLower().Contains("basictraffic_time_all"));

                        string query = string.Empty;
                        for (int i = 1; i <= columnTrafficCount; i++)
                        {
                            if (i != columnTrafficCount)
                            {
                                query += trafficAllColumn.ToLower().Replace("basictraffic_time_all", " (BasicTraffic_Time" + i.ToString()) + " or " + " BasicTraffic_Time" + i.ToString() + " is NULL) ";
                                query += " and ";
                            }
                            else
                            {
                                query += trafficAllColumn.ToLower().Replace("basictraffic_time_all", " (BasicTraffic_Time" + i.ToString()) + " or " + " BasicTraffic_Time" + i.ToString() + " is NULL) ";
                            }
                        }

                        dataRowCollection = dtResult.Select(query);
                    }
                    else if (designedReportConditionObj.TrafficConditionValue.ToLower().Contains("basictraffic_time_all") && oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic) > 0 || designedReportConditionObj.TrafficConditionValue.ToLower().Contains("basictraffic_time_all") && oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic) > 0)
                    {
                        bool conditionIsExcecute = false;
                        if (!dtResult.Columns.Contains("BasicTraffic_Time_First") && designedReportConditionObj.TrafficConditionValue.Contains("BasicTraffic_Time_First") && oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic) > 0)
                        {
                            dtResult.Columns.Add("BasicTraffic_Time_First");
                            conditionIsExcecute = true;
                        }
                        if (!dtResult.Columns.Contains("BasicTraffic_Time_Last") && designedReportConditionObj.TrafficConditionValue.Contains("BasicTraffic_Time_Last") && oldDesignedReportColumnList.Where(d => d.ColumnType == DesignedReportColumnType.Traffic).Count(c => c.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic) > 0)
                        {

                            dtResult.Columns.Add("BasicTraffic_Time_Last");
                            conditionIsExcecute = true;
                        }
                        if (conditionIsExcecute)
                            dataRowCollection = dtResult.Select(designedReportConditionObj.TrafficConditionValue);
                        else
                            dataRowCollection = dtResult.Select();
                    }
                    else
                    {
                        dataRowCollection = dtResult.Select();
                    }

                    //dtResult.Rows.Clear();
                    for (int i = 0; i < dataRowCollection.Count(); i++)
                    {
                        dtFinalResult.Rows.Add(dataRowCollection[i].ItemArray);
                    }

                }
                else
                {
                    dtFinalResult = dtResult.Copy();
                }

                dtFinalResult.Columns.Remove("BasicTraffic_Time");

                return dtFinalResult;

            }
            catch (Exception ex)
            {
                BaseBusiness<ReportParameter>.LogException(ex, "BReportParameter", "InitializeResultDesignedReportForTrafficColumns");
                throw ex;
            }
        }

        //DNN Note:------------------------------------
         
        public ReportUIParameter GetReportUIParameterByName(string name)
        {
            var param = ReportUIParameterRep.GetAll().Where(c => c.Name == name).First();
            return param;
        }

        //End of DNN Note:------------------------------------
    }
}
