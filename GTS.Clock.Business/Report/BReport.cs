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
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Security;
using System.Data;
using System.Data.SqlClient;
using GTS.Clock.Infrastructure.Report;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Temp;
using NHibernate;
using System.IO;
using System.Web.Configuration;
using Stimulsoft.Report;
using GTS.Clock.Model.Security;
using NHibernate.Linq;
using NHibernate.Type;
using NHibernate.Transform;
using NHibernate.Criterion;
using GTS.Clock.Business.Rules;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model;
using System.Globalization;
 
namespace GTS.Clock.Business.Reporting
{
    public class BReport : BaseBusiness<Report>
    {
        IDataAccess accessPort = new BUser();
        EntityRepository<Report> reportRep = new EntityRepository<Report>(false);
        EntityRepository<ReportFile> reportFileReposiory = new EntityRepository<ReportFile>();
        EntityRepository<DesignedReportType> reportDesignedReportType = new EntityRepository<DesignedReportType>(false);
        EntityRepository<DesignedReportColumn> repDesignedReportColumn = new EntityRepository<DesignedReportColumn>(false);
        EntityRepository<DesignedReportCondition> reportDesignedReportCondition = new EntityRepository<DesignedReportCondition>(false);
        BDesignedReportsColumn designedReportColumnBusiness = new BDesignedReportsColumn();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        EntityRepository<ReportParameter> reportParameterRep = new EntityRepository<ReportParameter>(false);
        private BTemp bTemp = new BTemp();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        string connectionString = NHibernateSessionManager.Instance.SessionFactoryPropsDic["connection.connection_string"];
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BReport";
        private int sqlConnectionTimeOut = 900;

        #region Report Tree

        /// <summary>
        /// ریشه را برمیگرداند
        /// اما نشست خالی نمیشود تا اشیا پرسیست شده باشد
        /// </summary>
        /// <returns></returns>
        public Report GetReportRoot()
        {

            try
            {
                IList<Report> list = NHSession.QueryOver<Report>()
                                              .Where(x => x.ParentId == 0 || x.ParentId == null)
                                              .List<Report>();
                if (list != null && list.Count == 1)
                {
                    return list.First();
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.ReportRootMoreThanOne, "تعداد ریشه درخت گزارشات در دیتابیس نامعتبر است", ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportRoot");
                throw ex;
            }
        }
        public override Report GetByID(decimal objID)
        {
            Report reportObj = base.GetByID(objID);
            if (reportObj.ReportParameterDesigned == null)
                reportObj.ReportParameterDesigned = new ReportParameterDesigned();
            if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
            {
                reportObj.ReportParameterDesigned.Title = reportObj.ReportParameterDesigned.FnName;
            }
            else
            {
                reportObj.ReportParameterDesigned.Title = reportObj.ReportParameterDesigned.EnName;
            }
            return reportObj;
        }
        public IList<Report> GetAllDesignedReports()
        {
            try
            {

                IList<Report> list = reportRep.GetAll();
                IList<Report> designedReportList = list.Where(r => r.IsDesignedReport == true).ToList<Report>();
                foreach (Report item in designedReportList)
                {
                    if (item.ReportParameterDesigned == null)
                        item.ReportParameterDesigned = new ReportParameterDesigned();
                    item.ParentReport = list.SingleOrDefault(r => r.ID == item.ParentId);
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        item.ReportParameterDesigned.Title = item.ReportParameterDesigned.FnName;
                        if (item.ParentReport != null)
                        {
                            if (item.ParentReport.ParentId == 0 || item.ParentReport.ParentPath == null)
                                item.ParentReport.Name = "ریشه";
                        }


                    }
                    else
                    {
                        item.ReportParameterDesigned.Title = item.ReportParameterDesigned.EnName;
                        if (item.ParentReport != null)
                        {
                            if (item.ParentReport.ParentId == 0 || item.ParentReport.ParentPath == null)
                                item.ParentReport.Name = "Root";
                        }

                    }



                }
                return designedReportList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetAllDesignedReports");
                throw ex;
            }
        }

        public IList<Report> GetReportGroupNodes()
        {
            try
            {

                IList<Report> list = reportRep.Find(r => r.IsReport == false).ToList<Report>();

                foreach (Report item in list)
                {


                    if (item.ParentId == 0)
                        item.Name = "ریشه";
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetAllDesignedReports");
                throw ex;
            }
        }
        public IList<DesignedReportType> GetAllDesignedReportType()
        {
            try
            {

                IList<DesignedReportType> list = reportDesignedReportType.GetAll();

                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetAllDesignedReportType");
                throw ex;
            }
        }
        public IList<Report> GetReportChilds(decimal parentId)
        {
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleReports();
                IList<Report> list = new List<Report>();
                Report reportAlias = null;
                GTS.Clock.Model.Temp.Temp tempAlias = null;

                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    list = reportRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), parentId),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new Report().ID), accessableIDs.ToArray(), CriteriaOperation.IN),
                                                                 new CriteriaStruct(Utility.GetPropertyName(() => new Report().SubSystemId), SubSystemIdentifier.TimeAtendance));

                }
                else
                {

                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    list = NHSession.QueryOver(() => reportAlias)
                                                      .JoinAlias(() => reportAlias.TempList, () => tempAlias)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID && reportAlias.ParentId == parentId && reportAlias.SubSystemId == SubSystemIdentifier.TimeAtendance)
                                                      .List<Report>();
                    this.bTemp.DeleteTempList(operationGUID);




                }

                IList<ReportParameter> parameters = reportParameterRep.GetAll();

                foreach (Report report in list)
                {
                    if (report.IsReport && !report.IsDesignedReport)
                    {
                        report.HasParameter = this.HasReportParameter(report.ReportFile.ID, parameters);
                    }
                    if (report.IsDesignedReport)
                        report.HasParameter = true;
                }

                List<Report> result = new List<Report>();
                result.AddRange(list.Where(x => x.IsReport).OrderBy(x => x.Order));
                result.AddRange(list.Where(x => !x.IsReport).OrderBy(x => x.Order));
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportChilds");
                throw ex;
            }
        }

        public IList<Report> GetReportChildsWidoutDA(decimal parentId)
        {
            try
            {
                IList<Report> list = reportRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), parentId),
                                                             new CriteriaStruct(Utility.GetPropertyName(() => new Report().SubSystemId), SubSystemIdentifier.TimeAtendance));
                IList<ReportParameter> parameters = reportParameterRep.GetAll();

                foreach (Report report in list)
                {
                    if (report.IsReport && report.ReportFile != null)
                    {
                        report.HasParameter = this.HasReportParameter(report.ReportFile.ID, parameters);
                    }
                }
                return list.OrderBy(x => x.Order).ToList(); ;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportChildsWidoutDA");
                throw ex;
            }
        }
        public IList<Report> GetReportChildsWidoutDA(string searchItem)
        {
            try
            {
                Report reportAlias = null;
                IList<Report> ReportList = new List<Report>();
                IList<Report> reportList = NHSession.QueryOver(() => reportAlias)
                                                    .Where(() => reportAlias.Name.IsInsensitiveLike(searchItem, MatchMode.Anywhere))
                                                    .List<Report>();
                foreach (Report d1 in reportList)
                {
                    foreach (Report d2 in reportList)
                    {
                        if (d2.ParentPathList.Contains(d1.ID))
                        {
                            ReportList.Add(d2);
                        }
                    }
                }
                reportList = reportList.Except(ReportList).ToList<Report>();
                return reportList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportChildsWidoutDA");
                throw ex;
            }

        }

        /// <summary>
        /// بچه های یک گره را با استفاده از آدرس پدران آن برمیگرداند
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IList<Report> GetReportChildsByParentPath(decimal parentId)
        {
            IList<Report> reportList = reportRep.GetByCriteria(
                new CriteriaStruct(
                    Utility.GetPropertyName(() => new Report().ParentPath)
                    , String.Format(",{0},", parentId)
                    , CriteriaOperation.Like),
                    new CriteriaStruct(Utility.GetPropertyName(() => new Report().SubSystemId), SubSystemIdentifier.TimeAtendance));
            IList<ReportParameter> parameters = reportParameterRep.GetAll();
            foreach (Report report in reportList)
            {
                if (report.IsReport && !report.IsDesignedReport)
                {
                    report.HasParameter = this.HasReportParameter(report.ReportFile.ID, parameters);
                }
                if (report.IsDesignedReport)
                    report.HasParameter = true;
            }
            return reportList.OrderBy(x => x.Order).ToList();
        }


        #endregion

        /// <summary>
        /// فایلهای گزارشی که هنوز منتسب به دسته گزارشی نیست را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<ReportFile> GetAllReportFiles()
        {
            try
            {
                EntityRepository<ReportFile> reportFileReposiory = new EntityRepository<ReportFile>();
                IList<ReportFile> list = reportFileReposiory.GetAll();
                if (list != null && list.Count > 0)
                {
                    list = list.Where(x => x.SubSystemId == SubSystemIdentifier.TimeAtendance).ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetAllReportFiles");
                throw ex;
            }
        }

        /// <summary>
        /// فایل گزارش را منتسب میکند
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportFile"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertReport(decimal reportGroupId, decimal reportFileId, string reportName)
        {
            try
            {
                Report report = new Report();
                report.ParentId = reportGroupId;
                report.Name = reportName;
                report.IsReport = true;
                report.ReportFile = new ReportFile() { ID = reportFileId };
                decimal id = this.SaveChanges(report, UIActionType.ADD);
                return id;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "InsertReport");
                throw ex;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertReport(Report report, UIActionType UAT)
        {
            try
            {
                return base.SaveChanges(report, UAT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "InsertReport");
                throw ex;
            }
        }

        /// <summary>
        /// نام و فایل گزارش را بروزرسانی میکند
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportFile"></param>
        /// <returns></returns>
        public decimal UpdateReport(decimal reportId, decimal reportFileId, string reportName, decimal reportParentID, bool isDesignedReport)
        {
            try
            {
                Report report = base.GetByID(reportId);
                report.Name = reportName;
                report.ParentId = reportParentID;
                report.IsReport = true;
                Report parentReport = base.GetByID(reportParentID);
                report.ParentPath = parentReport.ParentPath + parentReport.ID.ToString() + ",";

                if (!report.IsDesignedReport)
                    report.ReportFile = new ReportFile() { ID = reportFileId };
                else
                    report.ReportFile = null;
                decimal id = this.SaveChanges(report, UIActionType.EDIT);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateReport(Report report, UIActionType UAT)
        {
            try
            {

                decimal id = this.SaveChanges(report, UIActionType.EDIT);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// فایل گزارش که به یک گروه منتسب شده است را آزاد میکند
        /// </summary>
        /// <param name="reportFile"></param>
        /// <returns></returns>
        /// 

        public decimal DeleteReport(decimal reportId)
        {
            Report report = new Report() { ID = reportId };
            decimal id = base.SaveChanges(report, UIActionType.DELETE);
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        protected override void InsertValidate(Report report)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(report.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportNameRequierd, "نام گزارش باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(report.ParentId))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.InsertReportParentIDRequierd, "نام والد گزارش باید مشخص شود", ExceptionSrc));
            }
            else
            {
                int count = reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ParentId),
                                             new CriteriaStruct(Utility.GetPropertyName(() => report.IsReport), true));
                if (count != 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ReportCanNotBeParent, "گزارش نباید بعنوان والد درنظر گرفته شود", ExceptionSrc));
                }
            }

            //if (reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ParentId)) == 0)
            //{
            //    exception.Add(new ValidationException(ExceptionResourceKeys.ReportParentNotExists, "گزارش والدی با این شناسه موجود نمیباشد", ExceptionSrc));
            //}

            //else
            if (reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.Name), report.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => report.ParentId), report.ParentId)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportRepeatedName, "نام گزارش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (report.IsReport && !report.IsDesignedReport && (report.ReportFile == null || report.ReportFile.ID == 0))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportFileNotSpecified, "فایل گزارش انتخاب نشده است", ExceptionSrc));
            }
            if (report.IsDesignedReport == true && (report.ReportParameterDesigned == null || report.ReportParameterDesigned.ID == 0))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportFileNotSpecified, "پارامتر تاریخ باید انتخاب شود", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        protected override void UpdateValidate(Report report)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (!this.IsReportRoot(report.ID)
                && Utility.IsEmpty(report.ParentId))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportParentIDRequierd, "نام والد گزارش باید مشخص شود", ExceptionSrc));
            }

            if (Utility.IsEmpty(report.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportNameRequierd, "نام گزارش باید مشخص شود", ExceptionSrc));
            }

            else if (report.ParentId != 0 &&
                reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.Name), report.Name),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => report.ParentId), report.ParentId),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportRepeatedName, "نام گزارش در یک سطح نباید تکراری باشد", ExceptionSrc));
            }

            if (report.IsReport && !report.IsDesignedReport && (report.ReportFile == null || report.ReportFile.ID == 0))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportFileNotSpecified, "فایل گزارش انتخاب نشده است", ExceptionSrc));
            }
            if (report.IsDesignedReport == true && (report.ReportParameterDesigned != null && report.DesignedType.CustomCode != DesignedReportTypeEnum.Person && report.ReportParameterDesigned.CustomCode == "None"))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ReportFileNotSpecified, "پارامتر تاریخ باید انتخاب شود", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        protected override void DeleteValidate(Report report)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            bool ChildExist = this.GetChildReport(report.ID);
            int count = reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => report.ID), report.ID),
                                                                   new CriteriaStruct(Utility.GetPropertyName(() => report.ParentId), (decimal)0));

            if (count > 0)
            {
                exception.Add(ExceptionResourceKeys.ReportRootDeleteIllegal, "ریشه قابل حذف نیست", ExceptionSrc);
            }

            if (ChildExist)
            {
                exception.Add(ExceptionResourceKeys.ReportGroupDeleteIllegal, "گروه گزارش به دلیل داشتن گزارش قابل حذف نیست", ExceptionSrc);
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void OnSaveChangesSuccess(Report obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.Report, obj.ID, BUser.CurrentUser.ID, null, "");
            }
        }

        /// <summary>
        /// آیا این گزارش ریشه است؟
        /// این تابع بعلت جلوگیری از اشکال نشست را خالی میکند
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        private bool IsReportRoot(decimal reportId)
        {
            int count = reportRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Report().ParentId), (decimal)0),
                                                     new CriteriaStruct(Utility.GetPropertyName(() => new Report().ID), reportId));
            NHibernateSessionManager.Instance.ClearSession();
            return count > 0;
        }

        protected override void GetReadyBeforeSave(Report report, UIActionType action)
        {
            string separator = ",";
            report.SubSystemId = SubSystemIdentifier.TimeAtendance;
            Report parent = null; ;
            if (action == UIActionType.ADD && report.ParentId > 0)
            {
                parent = base.GetByID(report.ParentId);
                if (parent.ParentPath != null && parent.ParentPath.EndsWith(","))
                    separator = string.Empty;
                report.ParentPath = parent.ParentPath + String.Format("" + separator + "{0},", report.ParentId);
            }
            else if (action == UIActionType.EDIT)
            {
                Report oldOeport = new Report();
                Report objParent = base.GetByID(report.ParentId);
                report.ParentPath = objParent.ParentPath + report.ParentId + ",";
                if (report.ReportParameterDesigned != null)
                    oldOeport = GetByID(report.ID);
                // Report oldOeport = GetByID(report.ID);
                //report.AccessRoleList = oldOeport.AccessRoleList;
                NHSession.Evict(oldOeport);
                NHibernateSessionManager.Instance.ClearSession();
            }

        }

        /// <summary>
        /// اگر گزارش پارامتر دارد درست برمیکند
        /// </summary>
        /// <param name="reportFileId"></param>
        /// <returns></returns>
        private bool HasReportParameter(decimal reportFileId, IList<ReportParameter> parameters)
        {
            //EntityRepository<ReportParameter> rep = new EntityRepository<ReportParameter>(false);
            //int count = rep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ReportParameter().ReportFile), new ReportFile() { ID = reportFileId }));
            return parameters.Any(x => x.ReportFile != null && x.ReportFile.ID == reportFileId);
            //return count > 0;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckReportsLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertReportGroup(Report report, UIActionType UAT)
        {
            return base.SaveChanges(report, UAT);
        }

        public decimal UpdateReportGroup(Report report, UIActionType UAT)
        {
            return base.SaveChanges(report, UAT);
        }

        public decimal DeleteReportGroup(Report report, UIActionType UAT)
        {
            return base.SaveChanges(report, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUpdateAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDeleteAccess()
        {
        }

        public DataTable GetMonthlyReport_DateRange_DesignedReports(decimal reportId, decimal personId, IDictionary<string, object> paramValues, IList<decimal> personIdList)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            try
            {

                string columnTitles = string.Empty;
                string columnTrafficTitles = string.Empty;
                string columnFinalTitles = string.Empty;
                string selectItems = string.Empty;
                string pivotItems = string.Empty;
                string conditionsStr = string.Empty;
                string conditionUserStr = string.Empty;
                string conditionSelectedColumn = string.Empty;
                IList<DesignedReportColumn> designedReportColumnList = designedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
                if (designedReportColumnList.Count(c => c.IsConcept) == 0)
                {
                    exception.Add(ExceptionResourceKeys.NoColumnSelectedForReport, "ستون برای گزارش انتخاب نشده است.", ExceptionSrc);
                }
                if (exception.ExceptionList.Count > 0)
                {
                    throw exception;
                }

                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == personId).FirstOrDefault();
                for (int i = 0; i < designedReportColumnList.Count; i++)
                {
                    if (designedReportColumnList[i].Active)
                    {
                        if (designedReportColumnList[i].IsConcept)
                        {
                            if (designedReportColumnList[i].Concept.KeyColumnName != null)
                            {
                                selectItems += "ISNULL(SUM(" + designedReportColumnList[i].Concept.KeyColumnName + "), -1000) " + designedReportColumnList[i].Concept.KeyColumnName + " ";
                                columnTitles += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                columnFinalTitles += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                pivotItems += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                conditionSelectedColumn += " ( " + designedReportColumnList[i].Concept.KeyColumnName + " <>-1000 and " + designedReportColumnList[i].Concept.KeyColumnName + " <> 0 )";
                                if (i != designedReportColumnList.Count - 1)
                                {
                                    selectItems += ",";
                                    columnTitles += ",";
                                    columnFinalTitles += ",";
                                    pivotItems += ",";
                                    conditionSelectedColumn += " or ";
                                }
                            }


                        }
                        else if (designedReportColumnList[i].PersonInfo != null)
                        {

                            columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";
                            columnFinalTitles += GetDesignedReportColumnFieldNameForDesigned(designedReportColumnList[i]) + " ";
                            if (i != designedReportColumnList.Count - 1)
                            {

                                columnTitles += ",";
                                columnFinalTitles += ",";

                            }
                        }
                        else if (designedReportColumnList[i].PersonParam != null)
                        {

                            columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";
                            columnFinalTitles += GetDesignedReportColumnFieldNameForDesigned(designedReportColumnList[i]) + " "; ;
                            if (i != designedReportColumnList.Count - 1)
                            {

                                columnTitles += ",";
                                columnFinalTitles += ",";

                            }
                        }
                        else if (designedReportColumnList[i].Traffic != null)
                        {
                            if (!columnTrafficTitles.Contains(GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i])))
                            {
                                columnTrafficTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";
                                if (i != designedReportColumnList.Count - 1)
                                {

                                    columnTrafficTitles += ",";

                                }
                            }

                        }



                    }


                }
                if (columnTitles.LastOrDefault() == ',')
                    columnTitles = columnTitles.Remove(columnTitles.Count() - 1);
                if (columnFinalTitles.LastOrDefault() == ',')
                    columnFinalTitles = columnFinalTitles.Remove(columnTitles.Count() - 1);
                if (columnTrafficTitles.LastOrDefault() == ',')
                    columnTrafficTitles = columnTrafficTitles.Remove(columnTrafficTitles.Count() - 1);
                if (pivotItems.LastOrDefault() == ',')
                    pivotItems = pivotItems.Remove(pivotItems.Count() - 1);
                if (selectItems.LastOrDefault() == ',')
                    selectItems = selectItems.Remove(selectItems.Count() - 1);
                if (conditionSelectedColumn.Substring(conditionSelectedColumn.Count() - 4, 4) == " or ")
                {
                    conditionSelectedColumn = conditionSelectedColumn.Remove(conditionSelectedColumn.Count() - 4);
                }
                if (columnTitles.Count() > 0)
                    columnTitles = columnTitles.Insert(0, ",");
                if (columnFinalTitles.Count() > 0)
                    columnFinalTitles = columnFinalTitles.Insert(0, ",");
                if (columnTrafficTitles.Count() > 0)
                    columnTrafficTitles = columnTrafficTitles.Insert(0, ",");

                if (conditionSelectedColumn != string.Empty)
                {
                    conditionsStr = " Where " + conditionSelectedColumn;
                }
                if (designedReportConditionObj != null && designedReportConditionObj.ConditionValue != "")
                {
                    conditionUserStr = " where " + " (" + designedReportConditionObj.ConditionValue + ") ";
                }




                #region Query
                string additionalQuery = GetDesignedReportAdditionalQuery(designedReportColumnList);
                string additionalQueryTraffic = GetDesignedReportAdditionalQueryTraffic(designedReportColumnList);
                string operationGUID = this.bTemp.InsertTempList(personIdList);
                string dateColumnName = designedReportColumnBusiness.GetDesignedReportsStaticColumnByKeyName("Date").Name;
                string firstDayYear = Utility.GetDateOfBeginYear(DateTime.Now, LanguagesName.English).ToString("yyyy-MM-dd");

                string cmd = String.Format(@"
declare @ToDate datetime , @fromDate datetime,@Order smallint
set @ToDate='{3}'
set @Order={4}
set @fromDate ='{11}'
SELECT 
     Prs_BarCode as Barcode
    ,Prs_ID
    ,Date
    ,FromDate
	,ToDate
	{12}
    {9}
From
 (SELECT 
     dbo.GTS_ASM_MiladiToShamsi(CONVERT(nvarchar(10), [Date], 111)) as Date
	,Prs_BarCode
    ,Prs_ID
    ,ScndCnpValue_PeriodicFromDate as FromDate
	,ScndCnpValue_PeriodicToDate as ToDate
	{0}
	{9}	
	FROM (							        
SELECT ScndCnpValue_PersonId
	,ScndCnpValue_FromDate [Date]
    ,ScndCnpValue_PeriodicFromDate
	,ScndCnpValue_PeriodicToDate,
	 {1}
FROM (SELECT [No],
	ScndCnpValues.ScndCnpValue_ID,
	CnpTmp.ConceptTmp_FnName,
	CnpTmp.ConceptTmp_EngName,
	CnpTmp.ConceptTmp_KeyColumnName,
	PeriodicCnpValue.ScndCnpValue_PeriodicFromDate,
	PeriodicCnpValue.ScndCnpValue_PeriodicToDate,
	PeriodicCnpValue.ScndCnpValue_PersonId,
	ScndCnpValues.ScndCnpValue_FromDate,
	ScndCnpValues.ScndCnpValue_ToDate,
	ScndCnpValues.ScndCnpValue_Value
FROM (SELECT [No],
	PrdCnpTmpDetail_DtlCnpTmpId AS ScndCnpValue_DailyScndCnpId,
	PeriodicCnp_KeyColumnName   AS ScndCnpValue_KeyColumnName,     
	PeriodicCnp_FromDate		   AS ScndCnpValue_PeriodicFromDate,	   
	PeriodicCnp_ToDate		   AS ScndCnpValue_PeriodicToDate,
	PeriodicCnp_CnpTmpId		   AS ScndCnpValue_PeriodicScndCnpId, 
	PeriodicCnp_PersonId		   AS ScndCnpValue_PersonId
	FROM dbo.TA_PeriodicCnpTmpDetail 
	INNER JOIN(SELECT PrsRangeAsg.[No],
	CalcDateRange_ID					 AS PeriodicCnp_ID,
	PrsRangeAsg.PrsRangeAsg_PersonId	 AS PeriodicCnp_PersonId, 
	CalcDateRange_ConceptTmpId		 AS PeriodicCnp_CnpTmpId, 
	Concept.ConceptTmp_FnName 		 AS PeriodicCnp_KeyColumnName, 
	dbo.TA_ASM_CalculateFromDateRange(@ToDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
																 AS PeriodicCnp_FromDate,
							  dbo.TA_ASM_CalculateToDateRange(@ToDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
																 AS PeriodicCnp_ToDate
FROM (SELECT * 
	FROM dbo.TA_CalculationDateRange 
	WHERE CalcDateRange_Order = @Order
						    ) AS CalcDateRng
INNER JOIN (SELECT * 
	FROM dbo.TA_ConceptTemplate 
	WHERE ConceptTmp_IsPeriodic = 1
								  ) AS Concept
						ON CalcDateRange_ConceptTmpId = Concept.ConceptTmp_ID		  
INNER JOIN (SELECT *
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PrsRangeAsg_PersonId ORDER BY PrsRangeAsg_FromDate DESC) AS [No], 
    										 PrsRangeAsg_PersonId, PrsRangeAsg_CalcRangeGrpId
										  FROM TA_PersonRangeAssignment	
                                          Inner Join TA_Temp temp on PrsRangeAsg_PersonId = temp.temp_ObjectID
										  WHERE PrsRangeAsg_FromDate <= @ToDate
												  AND 
												temp.temp_OperationGUID = '@operationGUID'
										 ) AS [Range]
									WHERE [Range].[No] = 1
								   ) AS PrsRangeAsg
						ON CalcDateRange_CalcRangeGrpId = PrsRangeAsg.PrsRangeAsg_CalcRangeGrpId
						INNER JOIN TA_CalculationRangeGroup
						ON CalcDateRange_CalcRangeGrpId = CalcRangeGroup_ID
						WHERE ConceptTmp_KeyColumnName IS NOT NULL
								AND
							  Len(ConceptTmp_KeyColumnName) <> 0              
		              ) AS PeriodicCnp		
	        ON PrdCnpTmpDetail_PrdCnpTmpId = PeriodicCnp.PeriodicCnp_CnpTmpId
		   ) AS PeriodicCnpValue     
	 INNER JOIN TA_ConceptTemplate CnpTmp
	 ON CnpTmp.ConceptTmp_ID = PeriodicCnpValue.ScndCnpValue_DailyScndCnpId
	 CROSS APPLY dbo.TA_GetScndCnpValues(PeriodicCnpValue.ScndCnpValue_PersonId,
										 PeriodicCnpValue.ScndCnpValue_DailyScndCnpId,
										 PeriodicCnpValue.ScndCnpValue_PeriodicFromDate,
										 PeriodicCnpValue.ScndCnpValue_PeriodicToDate) AS  ScndCnpValues 							        
	) AS [Source]		
PIVOT
(
	SUM(ScndCnpValue_Value)
	FOR ConceptTmp_KeyColumnName
		IN ({2})
) AS Result			
GROUP BY ScndCnpValue_PersonId, ScndCnpValue_FromDate ,ScndCnpValue_PeriodicFromDate
	,ScndCnpValue_PeriodicToDate
) ScndCnpValue
INNER JOIN TA_Person Prs
ON ScndCnpValue.ScndCnpValue_PersonId = Prs.Prs_ID
 inner join TA_PersonDetail
on Prs_ID = TA_PersonDetail.PrsDtl_ID
inner join TA_PersonTASpec 
on Prs_ID = prsTA_ID 
{7} 
{10}
 {5} ) ResultTable {8} 
 ", columnTitles, selectItems, pivotItems, ((DateTime)paramValues["@ToDate"]).ToString("yyyy-MM-dd"), paramValues["@Order"], conditionsStr, dateColumnName, additionalQuery, conditionUserStr, columnTrafficTitles, additionalQueryTraffic, firstDayYear, columnFinalTitles);

                cmd = cmd.Replace("@operationGUID", operationGUID);
                #endregion

                DataTable dtResult = new DataTable("Concepts");
                SqlDataAdapter adapterObj = new SqlDataAdapter(cmd, connectionString);
                adapterObj.SelectCommand.CommandTimeout = sqlConnectionTimeOut;
                adapterObj.Fill(dtResult);

                this.bTemp.DeleteTempList(operationGUID);


                return dtResult;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetMonthlyReport_DateRange_DesignedReports");
                throw ex;
            }
        }

        public DataTable GetMonthlySummeryReport_DateRange_DesignedReports(decimal reportId, decimal personId, IDictionary<string, object> paramValues, IList<decimal> personIdList)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            try
            {
                string columnTitles = string.Empty;
                string selectItems = string.Empty;
                string pivotItems = string.Empty;
                string conditionsStr = string.Empty;

                string conditionSelectedColumn = string.Empty;
                IList<DesignedReportColumn> designedReportColumnList = designedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
                if (designedReportColumnList.Count(c => c.IsConcept) == 0)
                {
                    exception.Add(ExceptionResourceKeys.NoColumnSelectedForReport, "ستون برای گزارش انتخاب نشده است.", ExceptionSrc);
                }
                if (exception.ExceptionList.Count > 0)
                {
                    throw exception;
                }
                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == personId).FirstOrDefault();
                for (int i = 0; i < designedReportColumnList.Count; i++)
                {
                    if (designedReportColumnList[i].Active)
                    {
                        if (designedReportColumnList[i].IsConcept)
                        {
                            if (designedReportColumnList[i].Concept.KeyColumnName != null)
                            {
                                selectItems += "ISNULL(SUM(" + designedReportColumnList[i].Concept.KeyColumnName + "), -1000) " + designedReportColumnList[i].Concept.KeyColumnName + " ";

                                columnTitles += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                pivotItems += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                conditionSelectedColumn += " ( " + designedReportColumnList[i].Concept.KeyColumnName + " <>-1000 and " + designedReportColumnList[i].Concept.KeyColumnName + " <> 0 ) ";
                                if (i != designedReportColumnList.Count - 1)
                                {
                                    selectItems += ",";
                                    columnTitles += ",";
                                    pivotItems += ",";
                                    conditionSelectedColumn += " or ";
                                }
                            }
                        }
                        else
                        {
                            if (designedReportColumnList[i].PersonInfo != null || designedReportColumnList[i].PersonParam != null)
                            {
                                columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";
                                if (i != designedReportColumnList.Count - 1)
                                {

                                    columnTitles += ",";


                                }
                            }

                        }
                    }

                }
                if (columnTitles.LastOrDefault() == ',')
                    columnTitles = columnTitles.Remove(columnTitles.Count() - 1);
                if (pivotItems.LastOrDefault() == ',')
                    pivotItems = pivotItems.Remove(pivotItems.Count() - 1);
                if (selectItems.LastOrDefault() == ',')
                    selectItems = selectItems.Remove(selectItems.Count() - 1);
                if (conditionSelectedColumn.Substring(conditionSelectedColumn.Count() - 4, 4) == " or ")
                {
                    conditionSelectedColumn = conditionSelectedColumn.Remove(conditionSelectedColumn.Count() - 4);
                }
                if (columnTitles.Count() > 0)
                    columnTitles = columnTitles.Insert(0, ",");

                if (designedReportConditionObj != null && designedReportConditionObj.ConditionValue != "")
                {
                    conditionsStr = " where " + " (" + designedReportConditionObj.ConditionValue + ") and " + " (" + conditionSelectedColumn + ") ";
                }
                else
                {
                    conditionsStr = " where " + " (" + conditionSelectedColumn + ") ";
                }

                #region Query
                string additionalQuery = GetDesignedReportAdditionalQuery(designedReportColumnList);
                string operationGUID = this.bTemp.InsertTempList(personIdList);
                IList<DesignedReportStaticColumn> designedReportStaticColumnList = designedReportColumnBusiness.GetAllDesignedReportsStaticColumn();
                string calculationNotify = designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "CalculationNotify").Name;
                string firstDayYear = Utility.GetDateOfBeginYear(DateTime.Now, LanguagesName.English).ToString("yyyy-MM-dd");
                string cmd = String.Format(@"declare @ToDate datetime, @FromDate datetime,@Order int
set @ToDate='{3}'
set @Order={4}
set @FromDate ='{8}'
SELECT 
     Prs_BarCode Barcode
    ,Prs_ID
    ,FromDate
    ,ToDate
	{0}
	
	FROM  (							        
SELECT {1}
      ,ScndCnpValue_PersonId
      ,ScndCnpValue_PeriodicFromDate as FromDate
      ,ScndCnpValue_PeriodicToDate as ToDate
      ,ScndCnpValue_PeriodicFromDate as [Date]
FROM (SELECT   PeriodicCnp_KeyColumnName AS ScndCnpValue_KeyColumnName,     
	PeriodicCnp_FromDate		 AS ScndCnpValue_PeriodicFromDate,	   
	PeriodicCnp_ToDate		 AS ScndCnpValue_PeriodicToDate,
	PeriodicCnp_CnpTmpId		 AS ScndCnpValue_PeriodicScndCnpId, 
	PeriodicCnp_PersonId		 AS ScndCnpValue_PersonId,
	dbo.TA_FN_GetPeriodicScndCnpValue(PeriodicCnp_PersonId, 
	PeriodicCnp_CnpTmpId, 
	PeriodicCnp_FromDate, 
	PeriodicCnp_ToDate) 
	AS ScndCnpValue_PeriodicValue
	FROM (SELECT  CalcDateRange_ID				  AS PeriodicCnp_ID,
	PrsRangeAsg.PrsRangeAsg_PersonId  AS PeriodicCnp_PersonId, 
	CalcDateRange_ConceptTmpId		  AS PeriodicCnp_CnpTmpId, 
	ConceptTmp_KeyColumnName 		  AS PeriodicCnp_KeyColumnName, 
	dbo.TA_ASM_CalculateFromDateRange(@ToDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
													  AS PeriodicCnp_FromDate,
					dbo.TA_ASM_CalculateToDateRange(@ToDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
													  AS PeriodicCnp_ToDate
FROM (SELECT * 
	FROM dbo.TA_CalculationDateRange 
	WHERE CalcDateRange_Order = @Order
                 ) AS CalcDateRng
INNER JOIN (SELECT * 
	FROM dbo.TA_ConceptTemplate 
	WHERE ConceptTmp_IsPeriodic = 1
                            ) AS Concept
			     ON CalcDateRange_ConceptTmpId = Concept.ConceptTmp_ID		  
INNER JOIN (SELECT *
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PrsRangeAsg_PersonId ORDER BY PrsRangeAsg_FromDate DESC) AS [No], 
									  PrsRangeAsg_PersonId, PrsRangeAsg_CalcRangeGrpId AS PrsRangeAsg_CalcRangeGrpId
								   FROM TA_PersonRangeAssignment
                                   Inner Join TA_Temp temp on PrsRangeAsg_PersonId = temp.temp_ObjectID 								 
								   WHERE PrsRangeAsg_FromDate <= @ToDate
										  AND 
                                         temp.temp_OperationGUID = '@operationGUID'
								  ) AS [Range]
							 WHERE [Range].[No] = 1
                            ) AS PrsRangeAsg                            
			     ON CalcDateRange_CalcRangeGrpId = PrsRangeAsg.PrsRangeAsg_CalcRangeGrpId
			     INNER JOIN TA_CalculationRangeGroup
			     ON CalcDateRange_CalcRangeGrpId = CalcRangeGroup_ID				     		     
                 WHERE ConceptTmp_KeyColumnName IS NOT NULL
				         AND
			           Len(ConceptTmp_KeyColumnName) <> 0              
		   ) AS PeriodicCnpValue	
		   
	) AS [Source]
			
	PIVOT
	(
	 SUM(ScndCnpValue_PeriodicValue)
	 FOR ScndCnpValue_KeyColumnName
		 IN ({2})
	) AS Result			
GROUP BY ScndCnpValue_PersonId, ScndCnpValue_PeriodicFromDate ,ScndCnpValue_PeriodicToDate
) ScndCnpValue
INNER JOIN TA_Person Prs
ON ScndCnpValue.ScndCnpValue_PersonId = Prs.Prs_ID 
 inner join TA_PersonDetail
on Prs_ID = TA_PersonDetail.PrsDtl_ID
inner join TA_PersonTASpec 
on Prs_ID = prsTA_ID 
 {6} 
left outer join (select CFP_PrsId, case when  CFP_CalculationIsValid=0 and CFP_Date<@ToDate  then '{7}' else '' end  resultCalcValidation  from TA_Calculation_Flag_Persons inner join TA_Temp temp on CFP_PrsId = temp.temp_ObjectID
where temp.temp_OperationGUID = '@operationGUID') validationCalc on
ScndCnpValue.ScndCnpValue_PersonId =validationCalc.CFP_PrsId
 {5} ", columnTitles, selectItems, pivotItems, ((DateTime)paramValues["@ToDate"]).ToString("yyyy-MM-dd"), paramValues["@Order"], conditionsStr, additionalQuery, calculationNotify, firstDayYear);


                cmd = cmd.Replace("@operationGUID", operationGUID);
                #endregion

                DataTable dtResult = new DataTable("Concepts");
                SqlDataAdapter adapterObj = new SqlDataAdapter(cmd, connectionString);
                adapterObj.SelectCommand.CommandTimeout = sqlConnectionTimeOut;
                adapterObj.Fill(dtResult);

                this.bTemp.DeleteTempList(operationGUID);

                return dtResult;

            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetMonthlySummeryReport_DateRange_DesignedReports");
                throw ex;
            }
        }
        public DataTable GetMonthlySummeryReport_FromToDate_DesignedReports(decimal reportId, decimal personId, IDictionary<string, object> paramValues, IList<decimal> personIdList)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            try
            {
                string sumColumnTitles = string.Empty;
                string columnTitles = string.Empty;
                string selectItems = string.Empty;
                string pivotItems = string.Empty;
                string conditionsStr = string.Empty;
                string conditionUserStr = string.Empty;
                string groupColumns = string.Empty;
                string conditionSelectedColumn = string.Empty;

                IList<DesignedReportColumn> designedReportColumnList = designedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
                IList<DesignedReportGroupColumn> designedReportGroupColumnList = new BDesignedReportGroupColumn().GetDesignedReportGroupColumns(reportId, personId);
                if (designedReportColumnList.Count(c => c.IsConcept) == 0)
                {
                    exception.Add(ExceptionResourceKeys.NoColumnSelectedForReport, "ستون برای گزارش انتخاب نشده است.", ExceptionSrc);

                }
                if (exception.ExceptionList.Count > 0)
                {
                    throw exception;
                }
                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == personId).FirstOrDefault();
                for (int i = 0; i < designedReportColumnList.Count; i++)
                {
                    if (designedReportColumnList[i].Active)
                    {
                        if (designedReportColumnList[i].IsConcept)
                        {
                            if (designedReportColumnList[i].Concept.KeyColumnName != null)
                            {
                                selectItems += "ISNULL(SUM(" + designedReportColumnList[i].Concept.KeyColumnName + "), 0) " + designedReportColumnList[i].Concept.KeyColumnName + " ";
                                sumColumnTitles += "SUM(" + designedReportColumnList[i].Concept.KeyColumnName + ") " + designedReportColumnList[i].Concept.KeyColumnName + " ";
                                columnTitles += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                pivotItems += designedReportColumnList[i].Concept.KeyColumnName + " ";

                                conditionSelectedColumn += " ( " + designedReportColumnList[i].Concept.KeyColumnName + " <>-1000 and " + designedReportColumnList[i].Concept.KeyColumnName + " <> 0 ) ";
                                if (i != designedReportColumnList.Count - 1)
                                {
                                    sumColumnTitles += ",";
                                    selectItems += ",";
                                    columnTitles += ",";
                                    pivotItems += ",";
                                    conditionSelectedColumn += " or ";
                                }
                            }
                        }
                        else
                        {
                            if (designedReportColumnList[i].PersonInfo != null || designedReportColumnList[i].PersonParam != null)
                            {
                                columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";
                                sumColumnTitles += GetDesignedReportColumnFieldNameForDesigned(designedReportColumnList[i]) + " ";

                                groupColumns += GetDesignedReportColumnFieldNameForDesigned(designedReportColumnList[i]) + " ";

                                if (i != designedReportColumnList.Count - 1)
                                {

                                    columnTitles += ",";
                                    sumColumnTitles += ",";

                                    groupColumns += ",";


                                }
                            }
                        }
                    }

                }
                if (columnTitles.LastOrDefault() == ',')
                    columnTitles = columnTitles.Remove(columnTitles.Count() - 1);
                if (pivotItems.LastOrDefault() == ',')
                    pivotItems = pivotItems.Remove(pivotItems.Count() - 1);
                if (selectItems.LastOrDefault() == ',')
                    selectItems = selectItems.Remove(selectItems.Count() - 1);
                if (sumColumnTitles.LastOrDefault() == ',')
                    sumColumnTitles = sumColumnTitles.Remove(sumColumnTitles.Count() - 1);
                if (groupColumns.LastOrDefault() == ',')
                    groupColumns = groupColumns.Remove(groupColumns.Count() - 1);
                if (conditionSelectedColumn.Substring(conditionSelectedColumn.Count() - 4, 4) == " or ")
                {
                    conditionSelectedColumn = conditionSelectedColumn.Remove(conditionSelectedColumn.Count() - 4);
                }
                if (columnTitles.Count() > 0)
                    columnTitles = columnTitles.Insert(0, ",");
                if (sumColumnTitles.Count() > 0)
                    sumColumnTitles = sumColumnTitles.Insert(0, ",");
                if (groupColumns.Count() > 0)
                    groupColumns = groupColumns.Insert(0, ",");
                if (conditionSelectedColumn != string.Empty)
                {
                    conditionsStr = " Where " + conditionSelectedColumn;
                }
                if (designedReportConditionObj != null && designedReportConditionObj.ConditionValue != "")
                {
                    conditionUserStr = " where " + " (" + designedReportConditionObj.ConditionValue + ") ";
                }




                #region Query
                string additionalQuery = GetDesignedReportAdditionalQuery(designedReportColumnList);
                string operationGUID = this.bTemp.InsertTempList(personIdList);
                IList<DesignedReportStaticColumn> designedReportStaticColumnList = designedReportColumnBusiness.GetAllDesignedReportsStaticColumn();
                string calculationNotify = designedReportStaticColumnList.SingleOrDefault(r => r.KeyName == "CalculationNotify").Name;
                string cmd = String.Format(@"declare @fromDate datetime,@toDate datetime
set @fromDate='{3}'
set @toDate='{4}'

Select * from (
select  
     Prs_BarCode as Barcode
    ,FromDate
    ,ToDate
    ,d.ScndCnpValue_PersonId as Prs_ID
	{6}

	from(
SELECT 
     Prs_BarCode 
    ,Prs_ID
    ,ScndCnpValue_PeriodicFromDate as FromDate
    ,ScndCnpValue_PeriodicToDate as ToDate
	,ThirdExit, ThirdEntrance
	,SecondExit, SecondEntrance
	,FirstExit, FirstEntrance 
	,[Date] 
	,ScndCnpValue_PersonId
{0}
	
	FROM (							        
SELECT Entrance.FirstEntrance, [Exit].FirstExit, 
	Entrance.SecondEntrance, [Exit].SecondExit,
	Entrance.ThirdEntrance, [Exit].ThirdExit,
	Entrance.ProceedTraffic_FromDate,
	Entrance.ProceedTraffic_PersonId	   	
	FROM (SELECT ISNULL(SUM([1]), -1000) [FirstEntrance], 
			 ISNULL(SUM([2]), -1000) [SecondEntrance], 
			 ISNULL(SUM([3]), -1000) [ThirdEntrance], 
			 ProceedTraffic_FromDate, ProceedTraffic_PersonId
FROM (SELECT RANK() OVER (PARTITION BY ProceedTraffic_PersonId, ProceedTraffic_FromDate ORDER BY ProceedTrafficPair_From) Rk, *
			FROM TA_ProceedTraffic PrcTrf
			INNER JOIN TA_ProceedTrafficPair PrcTrfPair
			ON PrcTrf.ProceedTraffic_ID = PrcTrfPair.ProceedTrafficPair_ProceedTrafficId
            INNER JOIN TA_Temp temp 
            ON PrcTrf.ProceedTraffic_PersonId = temp.temp_ObjectID 
WHERE temp.temp_OperationGUID = '@operationGUID'
		   ) AS PrcTrf
	  PIVOT
		(SUM(ProceedTrafficPair_From)	  
		 FOR RK
			IN([1], [2], [3])
		) as pvt
	  GROUP BY ProceedTraffic_PersonId, ProceedTraffic_FromDate
	 ) Entrance
INNER JOIN (SELECT ISNULL(SUM([1]), -1000) [FirstExit], 
				   ISNULL(SUM([2]), -1000) [SecondExit], 
				   ISNULL(SUM([3]), -1000) [ThirdExit], 
				   ProceedTraffic_FromDate, ProceedTraffic_PersonId
FROM (SELECT RANK() OVER (PARTITION BY ProceedTraffic_PersonId, ProceedTraffic_FromDate ORDER BY ProceedTrafficPair_From) Rk, *
				  FROM TA_ProceedTraffic PrcTrf
				  INNER JOIN TA_ProceedTrafficPair PrcTrfPair
				  ON PrcTrf.ProceedTraffic_ID = PrcTrfPair.ProceedTrafficPair_ProceedTrafficId
                  INNER JOIN TA_Temp temp 
                  ON PrcTrf.ProceedTraffic_PersonId = temp.temp_ObjectID 
WHERE temp.temp_OperationGUID = '@operationGUID'
				 ) AS PrcTrf
			PIVOT
				(SUM(ProceedTrafficPair_To)	  
				 FOR RK
					IN([1], [2], [3])
				) as pvt
			GROUP BY ProceedTraffic_PersonId, ProceedTraffic_FromDate
		   ) [Exit]
ON Entrance.ProceedTraffic_FromDate = [Exit].ProceedTraffic_FromDate
	AND
   Entrance.ProceedTraffic_PersonId = [Exit].ProceedTraffic_PersonId
	) PrcTraffic
RIGHT JOIN 
(
SELECT ScndCnpValue_PersonId
	,ScndCnpValue_FromDate [Date]
	  
	  ,ScndCnpValue_PeriodicFromDate
    ,ScndCnpValue_PeriodicToDate,
{1}
FROM (SELECT [No],
	ScndCnpValues.ScndCnpValue_ID,
	CnpTmp.ConceptTmp_FnName,
	CnpTmp.ConceptTmp_EngName,
	CnpTmp.ConceptTmp_KeyColumnName,
	PeriodicCnpValue.ScndCnpValue_PeriodicFromDate,
	PeriodicCnpValue.ScndCnpValue_PeriodicToDate,
	
	PeriodicCnpValue.ScndCnpValue_PersonId,
	ScndCnpValues.ScndCnpValue_FromDate,
	ScndCnpValues.ScndCnpValue_ToDate,
	ScndCnpValues.ScndCnpValue_Value
	FROM (SELECT [No],
	PrdCnpTmpDetail_DtlCnpTmpId AS ScndCnpValue_DailyScndCnpId,
	PeriodicCnp_KeyColumnName   AS ScndCnpValue_KeyColumnName,     
	PeriodicCnp_FromDate		   AS ScndCnpValue_PeriodicFromDate,	   
	PeriodicCnp_ToDate		   AS ScndCnpValue_PeriodicToDate,
	PeriodicCnp_CnpTmpId		   AS ScndCnpValue_PeriodicScndCnpId, 
	PeriodicCnp_PersonId		   AS ScndCnpValue_PersonId
	FROM dbo.TA_PeriodicCnpTmpDetail 
INNER JOIN(SELECT PrsRangeAsg.[No],
	CalcDateRange_ID					 AS PeriodicCnp_ID,
	PrsRangeAsg.PrsRangeAsg_PersonId	 AS PeriodicCnp_PersonId, 
	CalcDateRange_ConceptTmpId		 AS PeriodicCnp_CnpTmpId, 
	Concept.ConceptTmp_FnName 		 AS PeriodicCnp_KeyColumnName, 
	--dbo.TA_ASM_CalculateFromDateRange(@toDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
	@fromDate
																 AS PeriodicCnp_FromDate,
	--dbo.TA_ASM_CalculateToDateRange(@toDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
																 @toDate
																 AS PeriodicCnp_ToDate
FROM (SELECT * 
	FROM dbo.TA_CalculationDateRange 
	WHERE CalcDateRange_Order = 1
						    ) AS CalcDateRng
INNER JOIN (SELECT * 
	FROM dbo.TA_ConceptTemplate 
	WHERE ConceptTmp_IsPeriodic = 1
								  ) AS Concept
						ON CalcDateRange_ConceptTmpId = Concept.ConceptTmp_ID		  
INNER JOIN (SELECT *
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PrsRangeAsg_PersonId ORDER BY PrsRangeAsg_FromDate DESC) AS [No], 
    										 PrsRangeAsg_PersonId, PrsRangeAsg_CalcRangeGrpId
										  FROM TA_PersonRangeAssignment
                                          Inner Join TA_Temp temp on PrsRangeAsg_PersonId = temp.temp_ObjectID 								 
										  WHERE PrsRangeAsg_FromDate <= @toDate
												  AND 
                                                temp.temp_OperationGUID = '@operationGUID'
										 ) AS [Range]
									WHERE [Range].[No] = 1
								   ) AS PrsRangeAsg
						ON CalcDateRange_CalcRangeGrpId = PrsRangeAsg.PrsRangeAsg_CalcRangeGrpId
						INNER JOIN TA_CalculationRangeGroup
						ON CalcDateRange_CalcRangeGrpId = CalcRangeGroup_ID
						WHERE ConceptTmp_KeyColumnName IS NOT NULL
								AND
							  Len(ConceptTmp_KeyColumnName) <> 0              
		              ) AS PeriodicCnp		
	        ON PrdCnpTmpDetail_PrdCnpTmpId = PeriodicCnp.PeriodicCnp_CnpTmpId
		   ) AS PeriodicCnpValue     
	 INNER JOIN TA_ConceptTemplate CnpTmp
	 ON CnpTmp.ConceptTmp_ID = PeriodicCnpValue.ScndCnpValue_DailyScndCnpId
	 CROSS APPLY dbo.TA_GetScndCnpValues(PeriodicCnpValue.ScndCnpValue_PersonId,
										 PeriodicCnpValue.ScndCnpValue_DailyScndCnpId,
										 PeriodicCnpValue.ScndCnpValue_PeriodicFromDate,
										 PeriodicCnpValue.ScndCnpValue_PeriodicToDate) AS  ScndCnpValues 							        
	) AS [Source]		
PIVOT
(
	SUM(ScndCnpValue_Value)
	FOR ConceptTmp_KeyColumnName
		IN ({2})
) AS Result			
GROUP BY ScndCnpValue_PersonId, ScndCnpValue_FromDate ,ScndCnpValue_PeriodicFromDate,ScndCnpValue_PeriodicToDate
) ScndCnpValue
ON PrcTraffic.ProceedTraffic_FromDate = ScndCnpValue.Date
	AND	
   PrcTraffic.ProceedTraffic_PersonId = ScndCnpValue.ScndCnpValue_PersonId
   INNER JOIN TA_Person Prs
ON ScndCnpValue.ScndCnpValue_PersonId = Prs.Prs_ID 
 inner join TA_PersonDetail
on Prs_ID = TA_PersonDetail.PrsDtl_ID
inner join TA_PersonTASpec 
on Prs_ID = prsTA_ID 
 {8} 
left outer join (select CFP_PrsId, case when  CFP_CalculationIsValid=0 and CFP_Date<@ToDate  then '{9}' else '' end  resultCalcValidation  from TA_Calculation_Flag_Persons inner join TA_Temp temp on CFP_PrsId = temp.temp_ObjectID
where temp.temp_OperationGUID = '@operationGUID') validationCalc on
ScndCnpValue.ScndCnpValue_PersonId =validationCalc.CFP_PrsId
) as d
{5} 
group by d.ScndCnpValue_PersonId,Prs_Barcode,FromDate,ToDate {10}
) Result  {7}
 ", columnTitles, selectItems, pivotItems, (DateTime.Parse(paramValues["fromDate"].ToString(), CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"), (DateTime.Parse(paramValues["toDate"].ToString(), CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"), conditionsStr, sumColumnTitles, conditionUserStr, additionalQuery, calculationNotify, groupColumns);


                cmd = cmd.Replace("@operationGUID", operationGUID);
                #endregion

                DataTable dtResult = new DataTable("Concepts");
                SqlDataAdapter adapterObj = new SqlDataAdapter(cmd, connectionString);
                adapterObj.SelectCommand.CommandTimeout = sqlConnectionTimeOut;
                adapterObj.Fill(dtResult);

                this.bTemp.DeleteTempList(operationGUID);

                return dtResult;

            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetMonthlySummeryReport_FromToDate_DesignedReports");
                throw ex;
            }
        }
        public DataTable GetMonthlyReport_FromToDate_DesignedReports(decimal reportId, decimal personId, IDictionary<string, object> paramValues, IList<decimal> personIdList)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            try
            {
                string columnTitles = string.Empty;
                string columnTrafficTitles = string.Empty;
                string columnFinalTitles = string.Empty;
                string selectItems = string.Empty;
                string pivotItems = string.Empty;
                string conditionsStr = string.Empty;
                string conditionUserStr = string.Empty;
                string conditionSelectedColumn = string.Empty;
                IList<DesignedReportColumn> designedReportColumnList = designedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
                if (designedReportColumnList.Count(c => c.IsConcept) == 0)
                {
                    exception.Add(ExceptionResourceKeys.NoColumnSelectedForReport, "ستون برای گزارش انتخاب نشده است.", ExceptionSrc);
                }
                if (exception.ExceptionList.Count > 0)
                {
                    throw exception;
                }
                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == personId).FirstOrDefault();
                for (int i = 0; i < designedReportColumnList.Count; i++)
                {
                    if (designedReportColumnList[i].Active == true)
                    {

                        if (designedReportColumnList[i].Concept != null)
                        {
                            if (designedReportColumnList[i].Concept.KeyColumnName != null)
                            {
                                selectItems += "ISNULL(SUM(" + designedReportColumnList[i].Concept.KeyColumnName + "), 0) " + designedReportColumnList[i].Concept.KeyColumnName + " ";

                                columnTitles += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                columnFinalTitles += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                pivotItems += designedReportColumnList[i].Concept.KeyColumnName + " ";
                                conditionSelectedColumn += " ( " + designedReportColumnList[i].Concept.KeyColumnName + " <>-1000 and " + designedReportColumnList[i].Concept.KeyColumnName + " <> 0 )";
                                if (i != designedReportColumnList.Count - 1)
                                {
                                    selectItems += ",";
                                    columnTitles += ",";
                                    columnFinalTitles += ",";
                                    pivotItems += ",";
                                    conditionSelectedColumn += " or ";
                                }
                            }
                        }
                        else if (designedReportColumnList[i].PersonInfo != null)
                        {


                            columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";
                            columnFinalTitles += GetDesignedReportColumnFieldNameForDesigned(designedReportColumnList[i]) + " ";
                            if (i != designedReportColumnList.Count - 1)
                            {

                                columnTitles += ",";
                                columnFinalTitles += ",";

                            }

                        }
                        else if (designedReportColumnList[i].PersonParam != null)
                        {


                            columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";
                            columnFinalTitles += GetDesignedReportColumnFieldNameForDesigned(designedReportColumnList[i]) + " ";
                            if (i != designedReportColumnList.Count - 1)
                            {

                                columnTitles += ",";
                                columnFinalTitles += ",";

                            }

                        }
                        else if (designedReportColumnList[i].Traffic != null)
                        {
                            if (!columnTrafficTitles.Contains(GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i])))
                            {
                                columnTrafficTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]) + " ";

                                if (i != designedReportColumnList.Count - 1)
                                {

                                    columnTrafficTitles += ",";


                                }
                            }
                        }

                    }

                }
                if (columnTitles.LastOrDefault() == ',')
                    columnTitles = columnTitles.Remove(columnTitles.Count() - 1);
                if (columnFinalTitles.LastOrDefault() == ',')
                    columnFinalTitles = columnFinalTitles.Remove(columnFinalTitles.Count() - 1);
                if (columnTrafficTitles.LastOrDefault() == ',')
                    columnTrafficTitles = columnTrafficTitles.Remove(columnTrafficTitles.Count() - 1);
                if (pivotItems.LastOrDefault() == ',')
                    pivotItems = pivotItems.Remove(pivotItems.Count() - 1);
                if (selectItems.LastOrDefault() == ',')
                    selectItems = selectItems.Remove(selectItems.Count() - 1);
                if (conditionSelectedColumn.Substring(conditionSelectedColumn.Count() - 4, 4) == " or ")
                {
                    conditionSelectedColumn = conditionSelectedColumn.Remove(conditionSelectedColumn.Count() - 4);
                }
                if (columnTitles.Count() > 0)
                    columnTitles = columnTitles.Insert(0, ",");
                if (columnFinalTitles.Count() > 0)
                    columnFinalTitles = columnFinalTitles.Insert(0, ",");
                if (columnTrafficTitles.Count() > 0)
                    columnTrafficTitles = columnTrafficTitles.Insert(0, ",");

                if (conditionSelectedColumn != string.Empty)
                {
                    conditionsStr = " Where " + conditionSelectedColumn;
                }
                if (designedReportConditionObj != null && designedReportConditionObj.ConditionValue != "")
                {
                    conditionUserStr = " where " + " (" + designedReportConditionObj.ConditionValue + ") ";
                }


                #region Query
                string additionalQuery = GetDesignedReportAdditionalQuery(designedReportColumnList);
                string additionalQueryTraffic = GetDesignedReportAdditionalQueryTraffic(designedReportColumnList);
                string operationGUID = this.bTemp.InsertTempList(personIdList);
                string dateColumnName = designedReportColumnBusiness.GetDesignedReportsStaticColumnByKeyName("Date").Name;
                string cmd = String.Format(@"
declare @fromDate datetime,@toDate datetime
set @fromDate='{3}'
set @toDate='{4}'

SELECT 
     Date
	,Prs_BarCode as Barcode
    ,Prs_ID
    ,FromDate
	,ToDate
    {11}
    {9}
From 
 (SELECT 
	
     dbo.GTS_ASM_MiladiToShamsi(CONVERT(nvarchar(10), [Date], 111)) as Date
	,Prs_BarCode
    ,Prs_ID
    ,ScndCnpValue_PeriodicFromDate as FromDate
	,ScndCnpValue_PeriodicToDate as ToDate
     {0}
     {9}
	FROM (							        
SELECT Entrance.FirstEntrance, [Exit].FirstExit, 
	Entrance.SecondEntrance, [Exit].SecondExit,
	Entrance.ThirdEntrance, [Exit].ThirdExit,
	Entrance.ProceedTraffic_FromDate,
	Entrance.ProceedTraffic_PersonId	   	
FROM (SELECT ISNULL(SUM([1]), -1000) [FirstEntrance], 
			 ISNULL(SUM([2]), -1000) [SecondEntrance], 
			 ISNULL(SUM([3]), -1000) [ThirdEntrance], 
			 ProceedTraffic_FromDate, ProceedTraffic_PersonId
FROM (SELECT RANK() OVER (PARTITION BY ProceedTraffic_PersonId, ProceedTraffic_FromDate ORDER BY ProceedTrafficPair_From) Rk, *
			FROM TA_ProceedTraffic PrcTrf
			INNER JOIN TA_ProceedTrafficPair PrcTrfPair
			ON PrcTrf.ProceedTraffic_ID = PrcTrfPair.ProceedTrafficPair_ProceedTrafficId
            INNER JOIN TA_Temp temp
            ON PrcTrf.ProceedTraffic_PersonId = temp.temp_ObjectID
			WHERE temp.temp_OperationGUID = '@operationGUID'
		   ) AS PrcTrf
	  PIVOT
		(SUM(ProceedTrafficPair_From)	  
		 FOR RK
			IN([1], [2], [3])
		) as pvt
	  GROUP BY ProceedTraffic_PersonId, ProceedTraffic_FromDate
	 ) Entrance
INNER JOIN (SELECT ISNULL(SUM([1]), -1000) [FirstExit], 
				   ISNULL(SUM([2]), -1000) [SecondExit], 
				   ISNULL(SUM([3]), -1000) [ThirdExit], 
				   ProceedTraffic_FromDate, ProceedTraffic_PersonId
FROM (SELECT RANK() OVER (PARTITION BY ProceedTraffic_PersonId, ProceedTraffic_FromDate ORDER BY ProceedTrafficPair_From) Rk, *
				  FROM TA_ProceedTraffic PrcTrf
				  INNER JOIN TA_ProceedTrafficPair PrcTrfPair
				  ON PrcTrf.ProceedTraffic_ID = PrcTrfPair.ProceedTrafficPair_ProceedTrafficId
                  INNER JOIN TA_Temp temp 
                  ON PrcTrf.ProceedTraffic_PersonId = temp.temp_ObjectID
				  WHERE temp.temp_OperationGUID = '@operationGUID'	
				 ) AS PrcTrf
			PIVOT
				(SUM(ProceedTrafficPair_To)	  
				 FOR RK
					IN([1], [2], [3])
				) as pvt
			GROUP BY ProceedTraffic_PersonId, ProceedTraffic_FromDate
		   ) [Exit]
ON Entrance.ProceedTraffic_FromDate = [Exit].ProceedTraffic_FromDate
	AND
   Entrance.ProceedTraffic_PersonId = [Exit].ProceedTraffic_PersonId
	) PrcTraffic
RIGHT JOIN 
(
SELECT ScndCnpValue_PersonId
	,ScndCnpValue_FromDate [Date]
	 ,ScndCnpValue_PeriodicFromDate
	  ,ScndCnpValue_PeriodicToDate,
      {1}
FROM (SELECT [No],
	ScndCnpValues.ScndCnpValue_ID,
	CnpTmp.ConceptTmp_FnName,
	CnpTmp.ConceptTmp_EngName,
	CnpTmp.ConceptTmp_KeyColumnName,
	PeriodicCnpValue.ScndCnpValue_PeriodicFromDate as ScndCnpValue_PeriodicFromDate,
	PeriodicCnpValue.ScndCnpValue_PeriodicToDate as ScndCnpValue_PeriodicToDate,
	PeriodicCnpValue.ScndCnpValue_PersonId,
	ScndCnpValues.ScndCnpValue_FromDate,
	ScndCnpValues.ScndCnpValue_ToDate,
	ScndCnpValues.ScndCnpValue_Value
FROM (SELECT [No],
	PrdCnpTmpDetail_DtlCnpTmpId AS ScndCnpValue_DailyScndCnpId,
	PeriodicCnp_KeyColumnName   AS ScndCnpValue_KeyColumnName,     
	PeriodicCnp_FromDate		   AS ScndCnpValue_PeriodicFromDate,	   
	PeriodicCnp_ToDate		   AS ScndCnpValue_PeriodicToDate,
	PeriodicCnp_CnpTmpId		   AS ScndCnpValue_PeriodicScndCnpId, 
	PeriodicCnp_PersonId		   AS ScndCnpValue_PersonId
	FROM dbo.TA_PeriodicCnpTmpDetail 
	INNER JOIN(SELECT PrsRangeAsg.[No],
	CalcDateRange_ID					 AS PeriodicCnp_ID,
	PrsRangeAsg.PrsRangeAsg_PersonId	 AS PeriodicCnp_PersonId, 
	CalcDateRange_ConceptTmpId		 AS PeriodicCnp_CnpTmpId, 
	Concept.ConceptTmp_FnName 		 AS PeriodicCnp_KeyColumnName, 
	--dbo.TA_ASM_CalculateFromDateRange(@toDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
	@fromDate
																 AS PeriodicCnp_FromDate,
	--dbo.TA_ASM_CalculateToDateRange(@toDate, CalcDateRange_Order, CalcDateRange_FromMonth, CalcDateRange_FromDay, CalcDateRange_ToMonth, CalcDateRange_ToDay, CalcRangeGroup_UsedCalendar)
																 @toDate
																 AS PeriodicCnp_ToDate
FROM (SELECT * 
	FROM dbo.TA_CalculationDateRange 
	WHERE CalcDateRange_Order = 1
						    ) AS CalcDateRng
INNER JOIN (SELECT * 
	FROM dbo.TA_ConceptTemplate 
	WHERE ConceptTmp_IsPeriodic = 1
								  ) AS Concept
						ON CalcDateRange_ConceptTmpId = Concept.ConceptTmp_ID		  
INNER JOIN (SELECT *
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PrsRangeAsg_PersonId ORDER BY PrsRangeAsg_FromDate DESC) AS [No], 
    										 PrsRangeAsg_PersonId, PrsRangeAsg_CalcRangeGrpId
										  FROM TA_PersonRangeAssignment
                                          INNER JOIN TA_Temp temp on PrsRangeAsg_PersonId = temp.temp_ObjectID								 
										  WHERE PrsRangeAsg_FromDate <= @toDate
												  AND 
												temp.temp_OperationGUID = '@operationGUID'
										 ) AS [Range]
									WHERE [Range].[No] = 1
								   ) AS PrsRangeAsg
						ON CalcDateRange_CalcRangeGrpId = PrsRangeAsg.PrsRangeAsg_CalcRangeGrpId
						INNER JOIN TA_CalculationRangeGroup
						ON CalcDateRange_CalcRangeGrpId = CalcRangeGroup_ID
						WHERE ConceptTmp_KeyColumnName IS NOT NULL
								AND
							  Len(ConceptTmp_KeyColumnName) <> 0              
		              ) AS PeriodicCnp		
	        ON PrdCnpTmpDetail_PrdCnpTmpId = PeriodicCnp.PeriodicCnp_CnpTmpId
		   ) AS PeriodicCnpValue     
	 INNER JOIN TA_ConceptTemplate CnpTmp
	 ON CnpTmp.ConceptTmp_ID = PeriodicCnpValue.ScndCnpValue_DailyScndCnpId
	 CROSS APPLY dbo.TA_GetScndCnpValues(PeriodicCnpValue.ScndCnpValue_PersonId,
										 PeriodicCnpValue.ScndCnpValue_DailyScndCnpId,
										 PeriodicCnpValue.ScndCnpValue_PeriodicFromDate,
										 PeriodicCnpValue.ScndCnpValue_PeriodicToDate) AS  ScndCnpValues 							        
	) AS [Source]		
PIVOT
(
	SUM(ScndCnpValue_Value)
	FOR ConceptTmp_KeyColumnName
		IN ({2})                             
) AS Result			
GROUP BY ScndCnpValue_PersonId, ScndCnpValue_FromDate ,ScndCnpValue_PeriodicFromDate,ScndCnpValue_PeriodicToDate
) ScndCnpValue
ON PrcTraffic.ProceedTraffic_FromDate = ScndCnpValue.Date
	AND	
   PrcTraffic.ProceedTraffic_PersonId = ScndCnpValue.ScndCnpValue_PersonId
   INNER JOIN TA_Person Prs
ON ScndCnpValue.ScndCnpValue_PersonId = Prs.Prs_ID 
 inner join TA_PersonDetail
on Prs_ID = TA_PersonDetail.PrsDtl_ID
inner join TA_PersonTASpec 
on Prs_ID = prsTA_ID 
 {7} 
 {10} 
{5} 
) ResultTable {8} 
 ", columnTitles, selectItems, pivotItems, (DateTime.Parse(paramValues["fromDate"].ToString())).ToString("yyyy-MM-dd"), (DateTime.Parse(paramValues["toDate"].ToString(), CultureInfo.InvariantCulture)).ToString("yyyy-MM-dd"), conditionsStr, dateColumnName, additionalQuery, conditionUserStr, columnTrafficTitles, additionalQueryTraffic, columnFinalTitles);

                cmd = cmd.Replace("@operationGUID", operationGUID);
                #endregion

                DataTable dtResult = new DataTable("Concepts");
                SqlDataAdapter adapterObj = new SqlDataAdapter(cmd, connectionString);
                adapterObj.SelectCommand.CommandTimeout = sqlConnectionTimeOut;
                adapterObj.Fill(dtResult);


                this.bTemp.DeleteTempList(operationGUID);


                return dtResult;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetMonthlyReport_FromToDate_DesignedReports");
                throw ex;
            }
        }

        public void CreateAllReportFiles()
        {
            try
            {
                string reportFilesPathKey = AppFolders.ReportFiles.ToString();
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + reportFilesPathKey))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + reportFilesPathKey);
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + reportFilesPathKey))
                {
                    string[] filePaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + reportFilesPathKey);
                    foreach (string filePath in filePaths)
                    {
                        File.Delete(filePath);
                    }

                    NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
                    IList<Report> ReportsList = NHSession.QueryOver<Report>()
                                                         .List<Report>();
                    foreach (Report reportItem in ReportsList)
                    {
                        if (reportItem.ReportFile != null)
                        {
                            try
                            {
                                ReportHelper reportHelper = ReportHelper.Instance("شرکت طرح و پردازش غدیر", BUser.CurrentUser.ID, BUser.CurrentUser.Person.Name, new List<decimal>(), string.Empty, string.Empty, false, string.Empty, string.Empty, new List<decimal>());
                                StiReport report = reportHelper.GetReport(reportItem.ReportFile.File);
                                string reportFileName = reportItem.ReportFile.Description + "@(" + Guid.NewGuid().ToString() + ")@(ID=" + reportItem.ReportFile.ID.ToString() + "#)";
                                report.Save(AppDomain.CurrentDomain.BaseDirectory + reportFilesPathKey + "\\" + reportFileName + ".mrt");
                            }
                            catch (Exception ex)
                            {
                                decimal i = reportItem.ReportFile.ID;
                            }
                        }
                    }
                }
                else
                    throw new Exception("Target Directory Is Not Created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public decimal InsertReportFile(ReportFile file)
        {
            var newItem = reportFileReposiory.Save(file);
            return newItem.ID;
        }
        public void UpdateReportFile(decimal reportFileID)
        {
            try
            {
                NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
                string reportFilesPathKey = AppFolders.ReportFiles.ToString();
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + AppFolders.ReportFiles.ToString()))
                {
                    string targetFilePath = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + AppFolders.ReportFiles.ToString()).Where(x => x.Contains("ID=" + reportFileID + "#")).FirstOrDefault();
                    if (targetFilePath != null && targetFilePath != string.Empty)
                    {
                        StiReport stiReport = new StiReport();
                        stiReport.Load(targetFilePath);
                        NHSession.CreateSQLQuery("Update TA_ReportFile set ReportFile_File = :reportFile Where ReportFile_ID = :reportFileID")
                                 .SetParameter("reportFile", stiReport.SaveToString())
                                 .SetParameter("reportFileID", reportFileID)
                                 .ExecuteUpdate();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateRoleAccess(decimal reportId, IList<Role> roleList)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (reportId > 0)
                {
                    Report report = base.GetByID(reportId);
                    if (report.AccessRoleList == null)
                    {
                        report.AccessRoleList = new List<Role>();
                    }
                    report.AccessRoleList.Clear();
                    foreach (Role role in roleList)
                    {
                        report.AccessRoleList.Add(role);
                    }
                    this.SaveChanges(report, UIActionType.EDIT);
                }
                else
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.ReportNotSpec, "گزارش مشخص نشده است", ExceptionSrc));
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        public IList<Report> GetReportGroups()
        {
            try
            {
                IList<Report> ReportList = NHSession.QueryOver<Report>()
                                                    .Where(x => x.IsReport == false)
                                                    .List();
                return ReportList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportGroups");
                throw ex;
            }
        }

        public string GetDesignedReportColumnFieldNameForQuery(DesignedReportColumn designedReportColumn)
        {
            try
            {
                string result = string.Empty;
                if (designedReportColumn.PersonInfo != null)
                {
                    switch (designedReportColumn.PersonInfo.Key)
                    {
                        case DesignedReportPersonInfoKeyColumn.CardNumber:
                            result = "Prs_CardNum";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Department:
                            result = "dep_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Employment:
                            result = "emply_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.EmploymentNumber:
                            result = "Prs_EmploymentNum";
                            break;
                        case DesignedReportPersonInfoKeyColumn.EmploymentDate:
                            result = "Prs_EmploymentDate";
                            break;
                        case DesignedReportPersonInfoKeyColumn.EndEmploymentDate:
                            result = "Prs_EndEmploymentDate";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Sex:
                            result = "Prs_Sex";
                            break;
                        case DesignedReportPersonInfoKeyColumn.FirstName:
                            result = "Prs_FirstName";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Grade:
                            result = "Grade_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.PersonActive:
                            result = "Prs_Active";
                            break;
                        case DesignedReportPersonInfoKeyColumn.NationalCode:
                            result = "PrsDtl_MeliCode";
                            break;
                        case DesignedReportPersonInfoKeyColumn.IDNumber:
                            result = "PrsDtl_ShomareShenasname";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LastName:
                            result = "Prs_LastName";
                            break;
                        case DesignedReportPersonInfoKeyColumn.WorkGroup:
                            result = "WorkGroup.WorkGroup_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Rule:
                            result = "RuleCat_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.UiValidation:
                            result = "UIValGrp_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.ControlStation:
                            result = "Station_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthDay:
                            result = "CurrentMonthDayRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthHour:
                            result = "CurrentMonthMinuteRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearDay:
                            result = "LeaveCalcResult_DayRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearHour:
                            result = "LeaveCalcResult_MinuteRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Shift:
                            result = "Shift_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R1:
                            result = "prsTA_R1";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R2:
                            result = "prsTA_R2";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R3:
                            result = "prsTA_R3";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R4:
                            result = "prsTA_R4";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R5:
                            result = "prsTA_R5";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R6:
                            result = "prsTA_R6";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R7:
                            result = "prsTA_R7";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R8:
                            result = "prsTA_R8";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R9:
                            result = "prsTA_R9";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R10:
                            result = "prsTA_R10";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R11:
                            result = "prsTA_R11";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R12:
                            result = "prsTA_R12";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R13:
                            result = "prsTA_R13";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R14:
                            result = "prsTA_R14";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R15:
                            result = "prsTA_R15";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R16:
                            result = "ReserveComboValue_ComboText16";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R17:
                            result = "ReserveComboValue_ComboText17";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R18:
                            result = "ReserveComboValue_ComboText18";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R19:
                            result = "ReserveComboValue_ComboText19";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R20:
                            result = "ReserveComboValue_ComboText20";
                            break;
                        default:
                            result = string.Empty;
                            break;
                    }
                }
                if (designedReportColumn.Traffic != null)
                {
                    switch (designedReportColumn.Traffic.Key)
                    {
                        case DesignedReportTrafficKeyColumn.AllTraffic:
                            result = "BasicTraffic_Time";
                            break;
                        case DesignedReportTrafficKeyColumn.FirstTraffic:
                            result = "BasicTraffic_Time";
                            break;
                        case DesignedReportTrafficKeyColumn.LastTraffic:
                            result = "BasicTraffic_Time";
                            break;
                        default:
                            break;
                    }
                }
                if (designedReportColumn.PersonParam != null)
                {
                    result = designedReportColumn.PersonParam.Key + ".prsParam_Value as " + designedReportColumn.PersonParam.Key;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetDesignedReportColumnFieldNameForQuery");
                throw ex;
            }

        }
        public string GetDesignedReportPersonInfoColumnFieldValueForTrafficReports(String columnName, BasicTraffic basicTraffic)
        {
            try
            {
                string result = string.Empty;

                switch (columnName)
                {
                    case "Prs_CardNum":
                        result = basicTraffic.Person.CardNum;
                        break;
                    case "dep_Name":
                        result = basicTraffic.Person.Department.Name;
                        break;
                    case "emply_Name":
                        result = basicTraffic.Person.EmploymentType.Name;
                        break;
                    case "Prs_EmploymentNum":
                        result = basicTraffic.Person.EmploymentNum;
                        break;
                    case "Prs_EmploymentDate":
                        result = Utility.GTSMinStandardDateTime == basicTraffic.Person.EmploymentDate ? "" : Utility.ToPersianDate(basicTraffic.Person.EmploymentDate);
                        break;
                    case "Prs_EndEmploymentDate":
                        result = Utility.GTSMinStandardDateTime == basicTraffic.Person.EndEmploymentDate ? "" : Utility.ToPersianDate(basicTraffic.Person.EndEmploymentDate);
                        break;
                    case "Prs_Sex":
                        result = basicTraffic.Person.Sex.ToString();
                        break;
                    case "Prs_FirstName":
                        result = basicTraffic.Person.FirstName;
                        break;
                    case "Grade_Name":
                        result = basicTraffic.Person.Grade.Name;
                        break;
                    case "Prs_Active":
                        result = basicTraffic.Person.Active.ToString();
                        break;
                    case "PrsDtl_MeliCode":
                        result = basicTraffic.Person.PersonDetail.MeliCode;
                        break;
                    case "PrsDtl_ShomareShenasname":
                        result = basicTraffic.Person.PersonDetail.ShomareShenasname;
                        break;
                    case "Prs_LastName":
                        result = basicTraffic.Person.LastName;
                        break;
                    case "WorkGroup_Name":
                        result = "";
                        break;
                    case "RuleCat_Name":
                        result = "";
                        break;
                    case "UIValGrp_Name":
                        result = basicTraffic.Person.PersonTASpec.UIValidationGroup.Name;
                        break;
                    case "Station_Name":
                        result = basicTraffic.Person.PersonTASpec.ControlStation.Name;
                        break;
                    case "CurrentMonthDayRemain":
                        result = "0";
                        break;
                    case "CurrentMonthMinuteRemain":
                        result = "0";
                        break;
                    case "LeaveCalcResult_DayRemain":
                        result = "0";
                        break;
                    case "LeaveCalcResult_MinuteRemain":
                        result = "0";
                        break;
                    default:
                        result = string.Empty;
                        break;
                }


                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetDesignedReportColumnFieldNameForDesigned");
                throw ex;
            }

        }
        public string GetDesignedReportColumnFieldNameForDesigned(DesignedReportColumn designedReportColumn)
        {
            try
            {
                string result = string.Empty;
                if (designedReportColumn.PersonInfo != null)
                {
                    switch (designedReportColumn.PersonInfo.Key)
                    {
                        case DesignedReportPersonInfoKeyColumn.CardNumber:
                            result = "Prs_CardNum";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Department:
                            result = "dep_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Employment:
                            result = "emply_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.EmploymentNumber:
                            result = "Prs_EmploymentNum";
                            break;
                        case DesignedReportPersonInfoKeyColumn.EmploymentDate:
                            result = "Prs_EmploymentDate";
                            break;
                        case DesignedReportPersonInfoKeyColumn.EndEmploymentDate:
                            result = "Prs_EndEmploymentDate";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Sex:
                            result = "Prs_Sex";
                            break;
                        case DesignedReportPersonInfoKeyColumn.FirstName:
                            result = "Prs_FirstName";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Grade:
                            result = "Grade_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.PersonActive:
                            result = "Prs_Active";
                            break;
                        case DesignedReportPersonInfoKeyColumn.NationalCode:
                            result = "PrsDtl_MeliCode";
                            break;
                        case DesignedReportPersonInfoKeyColumn.IDNumber:
                            result = "PrsDtl_ShomareShenasname";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LastName:
                            result = "Prs_LastName";
                            break;
                        case DesignedReportPersonInfoKeyColumn.WorkGroup:
                            result = "WorkGroup_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Rule:
                            result = "RuleCat_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.UiValidation:
                            result = "UIValGrp_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.ControlStation:
                            result = "Station_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthDay:
                            result = "CurrentMonthDayRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthHour:
                            result = "CurrentMonthMinuteRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearDay:
                            result = "LeaveCalcResult_DayRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearHour:
                            result = "LeaveCalcResult_MinuteRemain";
                            break;
                        case DesignedReportPersonInfoKeyColumn.Shift:
                            result = "Shift_Name";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R1:
                            result = "prsTA_R1";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R2:
                            result = "prsTA_R2";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R3:
                            result = "prsTA_R3";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R4:
                            result = "prsTA_R4";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R5:
                            result = "prsTA_R5";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R6:
                            result = "prsTA_R6";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R7:
                            result = "prsTA_R7";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R8:
                            result = "prsTA_R8";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R9:
                            result = "prsTA_R9";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R10:
                            result = "prsTA_R10";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R11:
                            result = "prsTA_R11";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R12:
                            result = "prsTA_R12";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R13:
                            result = "prsTA_R13";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R14:
                            result = "prsTA_R14";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R15:
                            result = "prsTA_R15";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R16:
                            result = "ReserveComboValue_ComboText16";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R17:
                            result = "ReserveComboValue_ComboText17";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R18:
                            result = "ReserveComboValue_ComboText18";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R19:
                            result = "ReserveComboValue_ComboText19";
                            break;
                        case DesignedReportPersonInfoKeyColumn.prsTA_R20:
                            result = "ReserveComboValue_ComboText20";
                            break;
                        default:
                            result = string.Empty;
                            break;
                    }
                }
                if (designedReportColumn.Traffic != null)
                {
                    switch (designedReportColumn.Traffic.Key)
                    {
                        case DesignedReportTrafficKeyColumn.AllTraffic:
                            result = "BasicTraffic_Time";
                            break;
                        case DesignedReportTrafficKeyColumn.FirstTraffic:
                            result = "BasicTraffic_Time";
                            break;
                        case DesignedReportTrafficKeyColumn.LastTraffic:
                            result = "BasicTraffic_Time";
                            break;
                        default:
                            break;
                    }
                }
                if (designedReportColumn.PersonParam != null)
                {
                    result = designedReportColumn.PersonParam.Key;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetDesignedReportColumnFieldNameForDesigned");
                throw ex;
            }

        }
        private string GetDesignedReportAdditionalQueryTraffic(IList<DesignedReportColumn> designedReportColumnList)
        {
            try
            {
                string query = string.Empty;
                if (query == string.Empty && designedReportColumnList.Count(c => c.Traffic != null && c.Traffic.Key == DesignedReportTrafficKeyColumn.AllTraffic) > 0)
                {
                    query += @" full outer join TA_BaseTraffic 
                             on Prs_ID = BasicTraffic_PersonID and  BasicTraffic_Date = Date and BasicTraffic_Active = 1";
                }
                if (query == string.Empty && designedReportColumnList.Count(c => c.Traffic != null && c.Traffic.Key == DesignedReportTrafficKeyColumn.FirstTraffic) > 0)
                {
                    query += @" full outer join TA_BaseTraffic 
                             on Prs_ID = BasicTraffic_PersonID and  BasicTraffic_Date = Date and BasicTraffic_Active = 1";
                }
                if (query == string.Empty && designedReportColumnList.Count(c => c.Traffic != null && c.Traffic.Key == DesignedReportTrafficKeyColumn.LastTraffic) > 0)
                {
                    query += @" full outer join TA_BaseTraffic 
                             on Prs_ID = BasicTraffic_PersonID and  BasicTraffic_Date = Date and BasicTraffic_Active = 1";
                }
                return query;
            }
            catch (Exception ex)
            {

                LogException(ex, "BReport", "GetDesignedReportAdditionalQueryTraffic");
                throw ex;
            }
        }
        private string GetDesignedReportAdditionalQuery(IList<DesignedReportColumn> designedReportColumnList)
        {
            try
            {


                string query = string.Empty;
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.WorkGroup) > 0)
                {
                    if (designedReportColumnList.Count(d => d.IsConcept) > 0)
                    {
                        query += @" left outer join TA_AssignWorkGroup AssignWorkGroup
                             on Prs_ID = AssignWorkGroup.AsgWorkGroup_PersonId
                             and [Date] <= (select isnull(min(AsgWorkGroup_FromDate),getdate()) from TA_AssignWorkGroup where AsgWorkGroup_PersonId=prs_id and AsgWorkGroup_FromDate > [Date]) and AsgWorkGroup_FromDate = (select max(AsgWorkGroup_FromDate) from TA_AssignWorkGroup where AsgWorkGroup_PersonId=prs_id and AsgWorkGroup_FromDate <= [Date])
                             left outer join TA_WorkGroup WorkGroup 
                             on AssignWorkGroup.AsgWorkGroup_WorkGroupId = WorkGroup.WorkGroup_ID ";
                    }
                    else
                    {
                        query += @" left outer join TA_AssignWorkGroup AssignWorkGroup
                            on Prs_ID = AssignWorkGroup.AsgWorkGroup_PersonId
                            and AssignWorkGroup.AsgWorkGroup_FromDate =(select max(AsgWorkGroup_FromDate) from TA_AssignWorkGroup where AsgWorkGroup_PersonId=prs_id and AsgWorkGroup_FromDate <= getdate())
                            left outer join TA_WorkGroup WorkGroup
                            on AssignWorkGroup.AsgWorkGroup_WorkGroupId = WorkGroup.WorkGroup_ID ";
                    }
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.Rule) > 0)
                {
                    if (designedReportColumnList.Count(d => d.IsConcept) > 0)
                    {
                        query += @" left outer join TA_PersonRuleCategoryAssignment
                            on Prs_ID = TA_PersonRuleCategoryAssignment.PrsRulCatAsg_PersonId 
                            and PrsRulCatAsg_FromDate <= [Date] and PrsRulCatAsg_ToDate >=[Date]
                            left outer join TA_RuleCategory
                            on TA_RuleCategory.RuleCat_ID =TA_PersonRuleCategoryAssignment.PrsRulCatAsg_RuleCategoryId ";
                    }
                    else
                    {
                        query += @" left outer join TA_PersonRuleCategoryAssignment
                            on Prs_ID = TA_PersonRuleCategoryAssignment.PrsRulCatAsg_PersonId 
                            and PrsRulCatAsg_FromDate <= getdate() and PrsRulCatAsg_ToDate >=getdate()
                            left outer join TA_RuleCategory
                            on TA_RuleCategory.RuleCat_ID =TA_PersonRuleCategoryAssignment.PrsRulCatAsg_RuleCategoryId ";
                    }
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.Grade) > 0)
                {

                    query += @" left outer join TA_Grade
                            on prs_GradeId = TA_Grade.Grade_ID ";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.UiValidation) > 0)
                {
                    query += @"left outer join TA_UIValidationGroup
                           on prsTA_UIValidationGroupID = TA_UIValidationGroup.UIValGrp_ID";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.ControlStation) > 0)
                {
                    query += @" left outer join TA_ControlStation
                          on prsTA_ControlStationId = TA_ControlStation.Station_ID";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.Department) > 0)
                {
                    query += @" left outer join  TA_Department
                           on Prs_DepartmentId = TA_Department.dep_ID ";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.Employment) > 0)
                {
                    query += @" left outer join TA_EmploymentType
                            on Prs_EmployId= TA_EmploymentType.emply_ID ";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && (c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthDay || c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentMonthHour || c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearDay || c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.LeaveRemainCurentYearHour)) > 0)
                {
                    query += @" left outer join (
  SELECT 
	 LeaveCalcResult_DayRemain
	,LeaveCalcResult_MinuteRemain
	,LeaveCalcResult_Date
	,CurrentMonthDayUsed
	,CurrentMonthMinuteUsed
	,LeaveYearRemain_DayRemainReal
	,LeaveYearRemain_MinuteRemainReal
	,LeaveYearRemain_DayRemainOK
	,LeaveYearRemain_MinuteRemainOK
	,isnull( LeaveIncDec_Day,0) as LeaveIncDec_Day
	,isnull( LeaveIncDec_Minute,0) as LeaveIncDec_Minute
	,CurrentMonthDayRemain
	,CurrentMonthMinuteRemain
	,(@toDate) as ToDateReport
    ,prs_id as personIdRemain
FROM (SELECT RANK() OVER (PARTITION BY LeaveCalcResult_PersonId ORDER BY LeaveCalcResult_Date DESC,LeaveCalcResult_ID DESC) rk, *
        FROM TA_LeaveCalcResult 
       ) as maxDateDelected 
       join ta_person 
       on LeaveCalcResult_PersonId=prs_id
       left outer join ta_LeaveYearRemain on LeaveCalcResult_PersonId=LeaveYearRemain_PersonId and LeaveYearRemain_Date>=@fromDate
left outer join (select sum(LeaveIncDec_Day) as LeaveIncDec_Day,sum(LeaveIncDec_Minute) as LeaveIncDec_Minute,LeaveIncDec_PersonId from ta_LeaveIncDec where LeaveIncDec_Applyed='True' 
	and (LeaveIncDec_Date>= @fromDate)
		    and (LeaveIncDec_Date<= @toDate) and (LeaveIncDec_PersonId in (SELECT temp_ObjectID
                                                     FROM  TA_Temp
                                                     WHERE temp_OperationGUID = '@operationGUID')) group by LeaveIncDec_PersonId) LeaveIncDec on LeaveCalcResult_PersonId=LeaveIncDec.LeaveIncDec_PersonId
      
	   left outer join ( 
SELECT distinct  (LeaveCalcResult_PersonId) as personID
	,(LeaveCalcResult_DayRemain) as CurrentMonthDayRemain
	,(LeaveCalcResult_MinuteRemain) as CurrentMonthMinuteRemain
	,(LeaveCalcResult_DayUsed) as CurrentMonthDayUsed
	,(LeaveCalcResult_MinuteUsed) as CurrentMonthMinuteUsed
      
	FROM TA_LeaveCalcResult join (select ( LeaveCalcResult_PersonId ) as prs_ID,max(LeaveCalcResult_Date) as max_date from TA_LeaveCalcResult where (LeaveCalcResult_PersonId in (SELECT temp_ObjectID
                                                     FROM  TA_Temp
                                                     WHERE temp_OperationGUID = '@operationGUID')) and (LeaveCalcResult_Date>= @fromDate)
  and (LeaveCalcResult_Date<= @toDate) group by LeaveCalcResult_PersonId) k on
  (TA_LeaveCalcResult.LeaveCalcResult_PersonId=k.prs_ID) and (TA_LeaveCalcResult.LeaveCalcResult_Date=k.max_date) 
    )
      RemainCurrentMonth on LeaveCalcResult_PersonId=RemainCurrentMonth.personID
 where rk = 1  and (LeaveCalcResult_PersonId in (SELECT temp_ObjectID
                                                     FROM  TA_Temp
                                                     WHERE temp_OperationGUID = '@operationGUID'))  ) remain  on prs_id = personIdRemain";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.Shift) > 0)
                {
                    if (designedReportColumnList.Count(d => d.IsConcept) > 0)
                    {
                        query += @" left outer join TA_AssignWorkGroup AssignWorkGroupShift
                             on Prs_ID = AssignWorkGroupShift.AsgWorkGroup_PersonId
                             and [Date] <= (select isnull(min(AsgWorkGroup_FromDate),getdate()) from TA_AssignWorkGroup where AsgWorkGroup_PersonId=prs_id and AsgWorkGroup_FromDate > [Date]) and AssignWorkGroupShift.AsgWorkGroup_FromDate = (select max(AsgWorkGroup_FromDate) from TA_AssignWorkGroup where AsgWorkGroup_PersonId=prs_id and AsgWorkGroup_FromDate <= [Date])
                             left outer join TA_WorkGroup WorkGroupShift
                             on AssignWorkGroupShift.AsgWorkGroup_WorkGroupId = WorkGroupShift.WorkGroup_ID
							 left outer join TA_WorkGroupDetail WorkGroupDetailShift
							 on WorkGroupShift.WorkGroup_ID =WorkGroupDetailShift.WorkGroupDtl_WorkGroupId 
							 and [Date] =WorkGroupDetailShift.WorkGroupDtl_Date
							 left outer join TA_Shift
							 on WorkGroupDetailShift.WorkGroupDtl_ShiftId=TA_Shift.Shift_ID ";
                    }
                    else
                    {
                        query += @" left outer join TA_AssignWorkGroup AssignWorkGroupShift
                            on Prs_ID = AssignWorkGroupShift.AsgWorkGroup_PersonId
                            and AssignWorkGroupShift.AsgWorkGroup_FromDate =(select max(AsgWorkGroup_FromDate) from TA_AssignWorkGroup where AsgWorkGroup_PersonId=prs_id and AsgWorkGroup_FromDate <= getdate())
                            left outer join TA_WorkGroup WorkGroupShift
                            on AssignWorkGroupShift.AsgWorkGroup_WorkGroupId = WorkGroupShift.WorkGroup_ID
                            left outer join TA_WorkGroupDetail WorkGroupDetailShift
							 on WorkGroupShift.WorkGroup_ID = WorkGroupDetailShift.WorkGroupDtl_WorkGroupId 
							 and WorkGroupDetailShift.WorkGroupDtl_Date = getdate()
							 left outer join TA_Shift
							 on WorkGroupDetailShift.WorkGroupDtl_ShiftId=TA_Shift.Shift_ID";
                    }
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.prsTA_R16) > 0)
                {
                    query += @" left outer join  (select ReserveComboValue_ID,ReserveComboValue_ComboText as ReserveComboValue_ComboText16 from TA_PersonReserveFieldComboValue) reserve16
					  on  prsta_r16 = reserve16.ReserveComboValue_ID ";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.prsTA_R17) > 0)
                {
                    query += @" left outer join  (select ReserveComboValue_ID,ReserveComboValue_ComboText as ReserveComboValue_ComboText17 from TA_PersonReserveFieldComboValue) reserve17
					  on  prsta_r17 = reserve17.ReserveComboValue_ID ";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.prsTA_R18) > 0)
                {
                    query += @" left outer join  (select ReserveComboValue_ID,ReserveComboValue_ComboText as ReserveComboValue_ComboText18 from TA_PersonReserveFieldComboValue) reserve18
					  on  prsta_r18 = reserve18.ReserveComboValue_ID ";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.prsTA_R19) > 0)
                {
                    query += @" left outer join  (select ReserveComboValue_ID,ReserveComboValue_ComboText as ReserveComboValue_ComboText19 from TA_PersonReserveFieldComboValue) reserve19
					  on  prsta_r19 = reserve19.ReserveComboValue_ID ";
                }
                if (designedReportColumnList.Count(c => c.PersonInfo != null && c.PersonInfo.Key == DesignedReportPersonInfoKeyColumn.prsTA_R20) > 0)
                {
                    query += @" left outer join  (select ReserveComboValue_ID,ReserveComboValue_ComboText as ReserveComboValue_ComboText20 from TA_PersonReserveFieldComboValue) reserve20
					  on  prsta_r20 = reserve20.ReserveComboValue_ID ";
                }
                IList<DesignedReportColumn> designedReportColumnPersonParamList = designedReportColumnList.Where(c => c.PersonParam != null).ToList();


                if (designedReportColumnList.Count(d => d.IsConcept) > 0)
                {
                    for (int i = 0; i < designedReportColumnPersonParamList.Count; i++)
                    {
                        string tableAliasName = designedReportColumnPersonParamList[i].PersonParam.Key;
                        query += @" left outer join  TA_PersonParamValue " + tableAliasName +
                        @" on " + tableAliasName + @".prsParam_PersonId = Prs_ID and " + tableAliasName + @".prsParam_FromDate<=[Date] and " + tableAliasName + @".prsParam_ToDate>=[Date] and " + tableAliasName + @".prsParam_fieldId=" + designedReportColumnPersonParamList[i].PersonParam.ID.ToString();
                    }
                }
                else
                {
                    for (int i = 0; i < designedReportColumnPersonParamList.Count; i++)
                    {
                        string tableAliasName = designedReportColumnPersonParamList[i].PersonParam.Key;
                        query += @" left outer join  TA_PersonParamValue " + tableAliasName +
                        @" on " + tableAliasName + @".prsParam_PersonId = Prs_ID and " + tableAliasName + @".prsParam_FromDate<=getdate() and " + tableAliasName + @".prsParam_ToDate>=getdate() and " + tableAliasName + @".prsParam_fieldId=" + designedReportColumnPersonParamList[i].PersonParam.ID.ToString();
                    }
                }



                return query;
            }
            catch (Exception ex)
            {

                LogException(ex, "BReport", "GetDesignedReportAdditionalQuery");
                throw ex;
            }
        }

        public DataTable GetPersonInfoQuery_DesignedReports(decimal reportId, IList<decimal> personIdList, decimal personId)
        {
            try
            {


                String cmd = string.Empty;
                string query = string.Empty;
                string columnTitles = string.Empty;
                IList<DesignedReportColumn> designedReportColumnList = designedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);
                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == personId).FirstOrDefault();
                for (int i = 0; i < designedReportColumnList.Count; i++)
                {
                    if (designedReportColumnList[i].PersonInfo != null)
                    {

                        columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]);
                        if (i != designedReportColumnList.Count - 1)
                        {
                            columnTitles += ",";

                        }
                    }
                    if (designedReportColumnList[i].PersonParam != null)
                    {

                        columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]);
                        if (i != designedReportColumnList.Count - 1)
                        {
                            columnTitles += ",";

                        }
                    }
                }
                string conditionUserStr = string.Empty;
                if (designedReportConditionObj != null && designedReportConditionObj.ConditionValue != "")
                {
                    conditionUserStr = " and " + " (" + designedReportConditionObj.ConditionValue + ") ";
                }
                if (columnTitles.Count() > 0)
                    columnTitles = columnTitles.Insert(0, ",");

                if (columnTitles.LastOrDefault() == ',')
                    columnTitles = columnTitles.Remove(columnTitles.Count() - 1);
                query += GetDesignedReportAdditionalQuery(designedReportColumnList);
                DateTime fromDate = Utility.GetDateOfBeginYear(DateTime.Now, BLanguage.CurrentLocalLanguage);
                DateTime toDate = DateTime.Now;
                string operationGUID = this.bTemp.InsertTempList(personIdList);

                cmd = String.Format(@" declare @fromDate datetime, @toDate datetime
                                   set @fromDate = '{2}'
                                   set @toDate = '{3}'
                       select 
                       Prs_BarCode as Barcode
                      ,Prs_ID
                      {1} 
                      from ta_person
                      inner join TA_PersonDetail
                      on Prs_ID = TA_PersonDetail.PrsDtl_ID
                      inner join TA_PersonTASpec 
                      on Prs_ID = prsTA_ID
                      Inner Join TA_Temp temp on Prs_Id = temp.temp_ObjectID 	  
                      {0} 
                      WHERE temp.temp_OperationGUID = '@operationGUID' {4} ", query, columnTitles, fromDate, toDate, conditionUserStr);
                cmd = cmd.Replace("@operationGUID", operationGUID);


                DataTable dtResult = new DataTable("Concepts");
                SqlDataAdapter adapterObj = new SqlDataAdapter(cmd, connectionString);
                adapterObj.SelectCommand.CommandTimeout = sqlConnectionTimeOut;
                adapterObj.Fill(dtResult);

                this.bTemp.DeleteTempList(operationGUID);


                return dtResult;
            }
            catch (Exception ex)
            {

                LogException(ex, "BReport", "GetPersonInfoQuery_DesignedReports");
                throw ex;
            }

        }
        public string GetPersonDetailInfo_DesignedReports(string columnName, Person personObj)
        {
            string result = string.Empty;
            try
            {

                switch (columnName)
                {
                    case "Prs_CardNum":
                        result = personObj.CardNum;
                        break;

                    case "Prs_EmploymentNum":
                        result = personObj.EmploymentNum;
                        break;
                    case "Prs_EmploymentDate":
                        switch (BLanguage.CurrentSystemLanguage)
                        {
                            case LanguagesName.Unknown:
                                break;
                            case LanguagesName.Parsi:
                                result = Utility.ToPersianDate(personObj.EmploymentDate);
                                break;
                            case LanguagesName.English:
                                result = personObj.EmploymentDate.ToShortDateString();
                                break;
                            default:
                                break;
                        }

                        break;
                    case "Prs_EndEmploymentDate":
                        switch (BLanguage.CurrentSystemLanguage)
                        {
                            case LanguagesName.Unknown:
                                break;
                            case LanguagesName.Parsi:
                                result = Utility.ToPersianDate(personObj.EndEmploymentDate);
                                break;
                            case LanguagesName.English:
                                result = personObj.EndEmploymentDate.ToShortDateString();
                                break;
                            default:
                                break;
                        }

                        break;
                    case "Prs_Sex":
                        result = personObj.Sex.ToString();
                        break;
                    case "Prs_FirstName":
                        result = personObj.FirstName;
                        break;

                    case "Prs_Active":
                        result = personObj.Active.ToString();
                        break;
                    case "PrsDtl_MeliCode":
                        result = personObj.PersonDetail.MeliCode;
                        break;
                    case "PrsDtl_ShomareShenasname":
                        result = personObj.PersonDetail.ShomareShenasname;
                        break;
                    case "Prs_LastName":
                        result = personObj.LastName;
                        break;



                    default:
                        result = string.Empty;
                        break;
                }

            }
            catch (Exception ex)
            {

                LogException(ex, "BReport", "GetPersonDetailInfo");
            }
            return result;
        }
        public DataTable GetTrafficQuery_DesignedReports(decimal reportId, decimal personId, IDictionary<string, object> paramValues, IList<decimal> personIdList)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();


                DateTime fromDate, toDate;
                if (paramValues.Keys.Count(c => c.ToLower() == "@Order".ToLower()) > 0)
                {
                    int i = 0;
                    DateRange dateRangePerson = null;
                    while (dateRangePerson == null)
                    {
                        Person personTempObj = null;
                        personTempObj = new BPerson().GetByID(personIdList[i]);
                        dateRangePerson = new BDateRange().GetDateRangePerson(personTempObj, 0, DateTime.Parse(paramValues["@ToDate"].ToString(), CultureInfo.InvariantCulture));
                        NHSession.Evict(personTempObj);
                        i++;
                    }


                    //IList<DateRangeOrderProxy> dateRangeOrderProxyList = new BPersonMonthlyWorkedTime(personId).GetDateRangeOrder(year);
                    // fromDate = DateTime.Parse(dateRangeOrderProxyList.SingleOrDefault(s => s.Order == Utility.ToInteger(paramValues["@Order"])).FromDate, CultureInfo.InvariantCulture);
                    // toDate = DateTime.Parse(dateRangeOrderProxyList.SingleOrDefault(s => s.Order == Utility.ToInteger(paramValues["@Order"])).ToDate, CultureInfo.InvariantCulture);
                    fromDate = dateRangePerson.FromDate;
                    toDate = dateRangePerson.ToDate;
                }
                else
                {
                    fromDate = DateTime.Parse(paramValues["fromDate"].ToString(), CultureInfo.InvariantCulture);
                    toDate = DateTime.Parse(paramValues["toDate"].ToString(), CultureInfo.InvariantCulture);
                }




                DesignedReportCondition designedReportConditionObj = reportDesignedReportCondition.Find(c => c.Report.ID == reportId && c.Person.ID == personId).FirstOrDefault();

                String cmd = string.Empty;
                string query = string.Empty;
                string columnTitles = string.Empty;
                IList<DesignedReportColumn> designedReportColumnList = designedReportColumnBusiness.GetDesignedReportsColumnsByReportID(reportId);

                for (int i = 0; i < designedReportColumnList.Count; i++)
                {
                    if (designedReportColumnList[i].Traffic != null || designedReportColumnList[i].PersonInfo != null || designedReportColumnList[i].PersonParam != null)
                    {
                        if (!columnTitles.Contains(GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i])))
                        {
                            columnTitles += GetDesignedReportColumnFieldNameForQuery(designedReportColumnList[i]);
                            if (i != designedReportColumnList.Count - 1)
                            {
                                columnTitles += ",";

                            }
                        }
                    }

                }
                string conditionUserStr = string.Empty;
                if (designedReportConditionObj != null && designedReportConditionObj.ConditionValue != "")
                {
                    conditionUserStr = " and " + " (" + designedReportConditionObj.ConditionValue + ") ";
                }
                if (columnTitles.Count() > 0)
                    columnTitles = columnTitles.Insert(0, ",");
                if (columnTitles.LastOrDefault() == ',')
                    columnTitles = columnTitles.Remove(columnTitles.Count() - 1);

                query += GetDesignedReportAdditionalQuery(designedReportColumnList);

                string operationGUID = this.bTemp.InsertTempList(personIdList);

                cmd = String.Format(@"declare @fromDate datetime,@toDate datetime
                                      set @fromDate='{2}'
                                      set @toDate='{3}'
                      select 
                       Prs_BarCode as Barcode
                      ,Prs_ID
                      ,dbo.GTS_ASM_MiladiToShamsi(CONVERT(nvarchar(10), BasicTraffic_Date, 111)) as Date
                      ,@fromDate as FromDate
                      ,@toDate as ToDate
                      {1} 
                      from TA_BaseTraffic
                      inner join ta_person
                      on BasicTraffic_PersonID = Prs_ID
                      inner join TA_PersonDetail
                      on Prs_ID = TA_PersonDetail.PrsDtl_ID
                      inner join TA_PersonTASpec 
                      on Prs_ID = prsTA_ID
                      Inner Join TA_Temp temp on Prs_Id = temp.temp_ObjectID 	  
                      {0} 
                      WHERE temp.temp_OperationGUID = '@operationGUID' 
                      and BasicTraffic_Date>= @fromDate and BasicTraffic_Date<=@toDate {4}  ", query, columnTitles, fromDate, toDate, conditionUserStr);
                cmd = cmd.Replace("@operationGUID", operationGUID);


                DataTable dtResult = new DataTable("Concepts");
                SqlDataAdapter adapterObj = new SqlDataAdapter(cmd, connectionString);
                adapterObj.SelectCommand.CommandTimeout = sqlConnectionTimeOut;
                adapterObj.Fill(dtResult);

                this.bTemp.DeleteTempList(operationGUID);


                return dtResult;
            }
            catch (Exception ex)
            {

                LogException(ex, "BReport", "GetPersonInfoQuery_DesignedReports");
                throw ex;
            }

        }
        public IList<Report> GetAllReports()
        {
            try
            {
                IList<decimal> accessableIDs = accessPort.GetAccessibleReports();
                Report reportAlias = null;
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                IList<Report> reportsList = null;
                if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
                {
                    reportsList = this.NHSession.QueryOver<Report>(() => reportAlias)
                                                          .Where(() => reportAlias.SubSystemId == SubSystemIdentifier.TimeAtendance)
                                                          .Where(() => reportAlias.ID.IsIn(accessableIDs.ToArray()))
                                                          .OrderBy(() => reportAlias.IsReport).Desc
                                                          .ThenBy(() => reportAlias.Order).Asc
                                                          .List<Report>();
                }
                else
                {
                    string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                    reportsList = NHSession.QueryOver(() => reportAlias)
                                                      .JoinAlias(() => reportAlias.TempList, () => tempAlias)
                                                      .Where(() => reportAlias.SubSystemId == SubSystemIdentifier.TimeAtendance)
                                                      .Where(() => tempAlias.OperationGUID == operationGUID)
                                                      .OrderBy(() => reportAlias.IsReport).Desc
                                                      .ThenBy(() => reportAlias.Order).Asc
                                                      .List<Report>();
                    this.bTemp.DeleteTempList(operationGUID);
                }

                IList<ReportParameter> parameters = reportParameterRep.GetAll();

                foreach (Report report in reportsList)
                {
                    if (report.IsReport && !report.IsDesignedReport)
                    {
                        report.HasParameter = this.HasReportParameter(report.ReportFile.ID, parameters);
                    }
                    if (report.IsDesignedReport)
                        report.HasParameter = true;
                }
                return reportsList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetAllReports");
                throw;
            }
        }
        public IList<Report> GetSearchReportOfUser(decimal userId, string SearchItem)
        {
            string SqlCommand = @"  select * from  TA_Report
							        INNER JOIN TA_DataAccessReport 
                                    on Report_ID=DataAccessReport_ReportID OR 
							        Report_ParentPath like '%,' + CONVERT(varchar(10),DataAccessReport_ReportID) + ',%'
							        WHERE DataAccessReport_UserID =:userId AND (Report_Name LIKE :SearchItem)";


            IList<Report> ReportList = NHibernateSessionManager.Instance.GetSession().CreateSQLQuery(SqlCommand)
                                                                                     .AddEntity(typeof(Report))
                                                                                     .SetParameter("SearchItem", String.Format("%{0}%", SearchItem))
                                                                                     .SetParameter("userId", userId)
                                                                                     .List<Report>();
            return ReportList;
        }
        public bool GetChildReport(decimal ReportId)
        {
            bool ChildExist = false;
            string SqlCommand = @"select * from TA_Report where Report_ParentPath like :ReportId";
            IList<Report> reportList = NHSession.CreateSQLQuery(SqlCommand)
                                     .AddEntity(typeof(Report))
                                     .SetParameter("ReportId", String.Format("%,{0},%", ReportId))
                                     .List<Report>();
            if (reportList.Count != 0)
                ChildExist = true;
            return ChildExist;
        }


        //DNN Note:

        /// <summary>
        /// [0] ReportName
        /// [1] ReportDescription
        /// [3] ReportParameter
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public string[] ReportNameResolution(string reportName)
        {
            return reportName.Split(new char[] { '[', ']' });
        }

        public Report GetReportByName(string name)
        {
            return reportRep.GetAll().Where(c => c.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// یک فایل گزارش را حذف می کند
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public void DeleteReportFile(decimal FileId)
        {
            ReportFile reportFile = reportFileReposiory.GetById(FileId, false);
            reportFileReposiory.Delete(reportFile);
        }

        /// <summary>
        /// یک فایل گزارش را میگرداند
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public ReportFile GetReportFileByFileId(decimal FileId)
        {
            ReportFile reportFile = reportFileReposiory.GetById(FileId, false);
            return reportFile;
        }

        public ReportFile GetReportFileByReportId(decimal ReportId)
        {
            try
            {
                var report = reportRep.GetById(ReportId, false);
                return report.ReportFile;

            }
            catch (Exception ex)
            {
                LogException(ex, "BReport", "GetReportFileByReportId");
                throw ex;
            }
        }

        public int GetMaxReportOrder()
        {
            int maxOrder = reportRep.GetAll().ToList().Max(c => c.Order);
            return maxOrder;
        }

    }
}