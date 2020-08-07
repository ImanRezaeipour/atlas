using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Report;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.Report;
using Stimulsoft.Base;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GTS.Clock.Business.Reporting
{
    public class BSystemReports : MarshalByRefObject
    {
        const string ExceptionSrc = "GTS.Clock.Business.Reporting.BSystemReports";

        public SystemReportsRepository systemReportsRepository
        {
            get
            {
                return new SystemReportsRepository();
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSystemReportsLoadAccess()
        {
        }

        public int GetSystemReportTypeCount(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemReportTypeCount(SRT, SrtFilterConditions, BLanguage.CurrentSystemLanguage);

            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemReportTypeCount");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<GTS.Clock.Model.Report.SystemBusinessReport> GetSystemBusinessReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemBusinessReportList(SRT, PageSize, PageIndex, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemBusinessReportList");
                throw ex;
            }
        }

        public IList<GTS.Clock.Model.Report.SystemBusinessReport> GetSystemBusinessReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemBusinessReportList(SRT, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemBusinessReportList");
                throw ex;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<GTS.Clock.Model.Report.SystemEngineReport> GetSystemEngineReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemEngineReportList(SRT, PageSize, PageIndex, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemBusinessReportList");
                throw ex;
            }
        }

        public IList<GTS.Clock.Model.Report.SystemEngineReport> GetSystemEngineReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemEngineReportList(SRT, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemBusinessReportList");
                throw ex;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<GTS.Clock.Model.Report.SystemWindowsServiceReport> GetSystemWindowsServiceReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemWindowsServiceReportList(SRT, PageSize, PageIndex, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemWindowsServiceReportList");
                throw ex;
            }
        }

        public IList<GTS.Clock.Model.Report.SystemWindowsServiceReport> GetSystemWindowsServiceReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemWindowsServiceReportList(SRT, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemWindowsServiceReportList");
                throw ex;
            }
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<GTS.Clock.Model.Report.SystemUserActionReport> GetSystemUserActionReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemUserActionReportList(SRT, PageSize, PageIndex, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemUserActionReportList");
                throw ex;
            }
        }

        public IList<GTS.Clock.Model.Report.SystemUserActionReport> GetSystemUserActionReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemUserActionReportList(SRT, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemUserActionReportList");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]

        public IList<GTS.Clock.Model.Report.SystemEngineDebugReport> GetSystemEngineDebugReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemEngineDebugReportList(SRT, PageSize, PageIndex, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemEngineDebugReportList");
                throw ex;
            }
        }
        public IList<GTS.Clock.Model.Report.SystemEngineDebugReport> GetSystemEngineDebugReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemEngineDebugReportList(SRT, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemEngineDebugReportList");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public IList<GTS.Clock.Model.Report.SystemDataCollectorReport> GetSystemDataCollectorReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemDataCollectorReportList(SRT, PageSize, PageIndex, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemDataCollectorReportList");
                throw ex;
            }
        }
        public IList<GTS.Clock.Model.Report.SystemDataCollectorReport> GetSystemDataCollectorReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                return this.systemReportsRepository.GetSystemDataCollectorReportList(SRT, SrtFilterConditions, BLanguage.CurrentSystemLanguage);
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "GetSystemDataCollectorReportList");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemBusinessReport()
        {
            this.DeleteAllSystemReportType<GTS.Clock.Model.Report.SystemBusinessReport>("BSystemReports", "DeleteAllSystemBusinessReport");
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemEngineReport()
        {
            this.DeleteAllSystemReportType<GTS.Clock.Model.Report.SystemEngineReport>("BSystemReports", "DeleteAllSystemEngineReport");
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemWindowsServiceReport()
        {
            this.DeleteAllSystemReportType<GTS.Clock.Model.Report.SystemWindowsServiceReport>("BSystemReports", "DeleteAllSystemWindowsServiceReport");
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteAllSystemUserActionReport()
        {
            this.DeleteAllSystemReportType<GTS.Clock.Model.Report.SystemUserActionReport>("BSystemReports", "DeleteAllSystemUserActionReport");
        }

        public void DeleteAllEngineDebugReport()
        {
            this.DeleteAllSystemReportType<GTS.Clock.Model.Report.SystemEngineDebugReport>("BSystemReports", "DeleteAllSystemEngineDebugReport");
        }
        public void DeleteAllDataCollectorReport()
        {
            this.DeleteAllSystemReportType<GTS.Clock.Model.Report.SystemDataCollectorReport>("BSystemReports", "DeleteAllSystemEngineDebugReport");
        }

        private void DeleteAllSystemReportType<T>(string ClassName, string MethodName) where T : class
        {
            try
            {
                this.systemReportsRepository.DeleteAllSystemReportType<T>();
            }
            catch (Exception ex)
            {
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, ClassName, MethodName);
                throw ex;
            }
        }

        public StiReport GetReport(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions)
        {
            try
            {
                ReportHelper reportHelper = ReportHelper.Instance("شرکت طرح و پردازش غدیر", BUser.CurrentUser.ID, BUser.CurrentUser.Person.Name, new List<decimal>(), Guid.NewGuid().ToString(), string.Empty, false, string.Empty, string.Empty,new List<decimal>());


                StiReport stiReport = new StiReport();
                double columnWidth = 0;
                double dataBandRowHeight = 1.0;
                double dataBandHeight = 4;
                double dataHeaderRowHeight = 1;
                double dataHeaderHeight = 3;
                Font dataBandFont = new Font("Tahoma", 8, FontStyle.Bold);
                Font headerBandFont = new Font("arial", 11, FontStyle.Bold);
                Font titleBandFont = new Font("Tahoma", 16, FontStyle.Bold);
                Font groupBandFont = new Font("Tahoma", 10, FontStyle.Bold);
                Font pageHeaderFont = new Font("Tahoma", 18, FontStyle.Bold);
                stiReport.Unit = Stimulsoft.Report.Units.StiUnit.Centimeters;
                StiPage page = stiReport.Pages[0];
                page.Orientation = StiPageOrientation.Landscape;
                page.Width = 29.7;
                page.Height = 21;
                page.Margins.Bottom = 0.5;
                page.Margins.Left = 0.5;
                page.Margins.Right = 0.5;
                page.Margins.Top = 0.5;



                StiPageHeaderBand pageHeader = new StiPageHeaderBand();
                pageHeader.Height = 2;
                pageHeader.Width = page.Width;
                pageHeader.Name = "PageHeader";
                pageHeader.Border.Side = StiBorderSides.All;
                pageHeader.Border.Color = Color.Cornsilk;
                page.Components.Add(pageHeader);

                StiText officeNameText_pageHeader = new StiText(new RectangleD(0, 0, 10.6, 0.6));
                officeNameText_pageHeader.Left = (page.Width / 2) - 5.3;
                officeNameText_pageHeader.Top = 0;
                IList<Presentaion_Helper.Proxy.DataAccessProxy> dataAccessList = new BDataAccess().GetAllByUserId(DataAccessParts.Corporation, BUser.CurrentUser.ID);
                string headerText = string.Empty;
                if (dataAccessList != null && dataAccessList.Count > 0)
                    headerText = dataAccessList.FirstOrDefault().Name;
                officeNameText_pageHeader.Text = headerText;
                officeNameText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                officeNameText_pageHeader.VertAlignment = StiVertAlignment.Center;
                officeNameText_pageHeader.Name = "OfficeNameHeader";
                officeNameText_pageHeader.Font = pageHeaderFont;
                officeNameText_pageHeader.Height = 1;
                pageHeader.Components.Add(officeNameText_pageHeader);


                StiText reportNameText_pageHeader = new StiText(new RectangleD(0, 0, 10.6, 0.6));
                reportNameText_pageHeader.Left = (page.Width / 2) - 5.3;
                reportNameText_pageHeader.Top = 1;

                reportNameText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportNameText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportNameText_pageHeader.Name = "reportNameText";
                reportNameText_pageHeader.Font = pageHeaderFont;
                reportNameText_pageHeader.Height = 1;
                pageHeader.Components.Add(reportNameText_pageHeader);


                StiText reportCreateDateLabelText_pageHeader = new StiText(new RectangleD(0, 0, 2.6, 0.6));
                reportCreateDateLabelText_pageHeader.Left = 2.4;
                reportCreateDateLabelText_pageHeader.Top = 0;
                switch (BLanguage.CurrentLocalLanguage)
                {
                    case LanguagesName.Unknown:
                        reportCreateDateLabelText_pageHeader.Text = ": تاریخ تهیه گزارش";
                        break;
                    case LanguagesName.Parsi:
                        reportCreateDateLabelText_pageHeader.Text = ": تاریخ تهیه گزارش";
                        break;
                    case LanguagesName.English:
                        reportCreateDateLabelText_pageHeader.Text = "Report Date :";
                        break;
                    default:
                        break;
                }
                reportCreateDateLabelText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreateDateLabelText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreateDateLabelText_pageHeader.Name = "reportCreateDateLabelText";
                reportCreateDateLabelText_pageHeader.Font = dataBandFont;
                reportCreateDateLabelText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(89, 89, 89));
                pageHeader.Components.Add(reportCreateDateLabelText_pageHeader);

                StiText reportCreateDateText_pageHeader = new StiText(new RectangleD(0, 0, 2.4, 0.6));
                reportCreateDateText_pageHeader.Left = 0;
                reportCreateDateText_pageHeader.Top = 0;
                reportCreateDateText_pageHeader.Text = ReportHelper.Instance().ShamsiGetNow();
                reportCreateDateText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreateDateText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreateDateText_pageHeader.Name = "reportCreateDateText";
                reportCreateDateText_pageHeader.Font = dataBandFont;
                reportCreateDateText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(183, 117, 64));
                pageHeader.Components.Add(reportCreateDateText_pageHeader);

                StiText reportCreatorLabelText_pageHeader = new StiText(new RectangleD(0, 0, 2, 0.6));
                reportCreatorLabelText_pageHeader.Left = 3;
                reportCreatorLabelText_pageHeader.Top = 0.6;
                switch (BLanguage.CurrentLocalLanguage)
                {
                    case LanguagesName.Unknown:
                        reportCreatorLabelText_pageHeader.Text = ": تهیه کننده";
                        break;
                    case LanguagesName.Parsi:
                        reportCreatorLabelText_pageHeader.Text = ": تهیه کننده";
                        break;
                    case LanguagesName.English:
                        reportCreatorLabelText_pageHeader.Text = "Creator :";
                        break;
                    default:
                        break;
                }
                reportCreatorLabelText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreatorLabelText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreatorLabelText_pageHeader.Name = "reportCreatorLabelText";
                reportCreatorLabelText_pageHeader.Font = dataBandFont;
                reportCreatorLabelText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(89, 89, 89));
                pageHeader.Components.Add(reportCreatorLabelText_pageHeader);


                StiText reportCreatorText_pageHeader = new StiText(new RectangleD(0, 0, 3, 0.6));
                reportCreatorText_pageHeader.Left = 0;
                reportCreatorText_pageHeader.Top = 0.6;
                reportCreatorText_pageHeader.Text = ReportHelper.Instance().UserName;
                reportCreatorText_pageHeader.HorAlignment = StiTextHorAlignment.Center;
                reportCreatorText_pageHeader.VertAlignment = StiVertAlignment.Center;
                reportCreatorText_pageHeader.Name = "reportCreatorText";
                reportCreatorText_pageHeader.Font = dataBandFont;
                reportCreatorText_pageHeader.TextBrush = new StiSolidBrush(Color.FromArgb(183, 117, 64));
                pageHeader.Components.Add(reportCreatorText_pageHeader);

                StiHeaderBand headerBand = new StiHeaderBand();
                headerBand.Name = "HeaderBand";
                headerBand.Width = page.Width;
                headerBand.Border.Side = StiBorderSides.All;
                headerBand.Border.Color = Color.DimGray;
                page.Components.Add(headerBand);

                StiDataBand dataBand = new StiDataBand();

                dataBand.Name = "DataBand";
                dataBand.Width = page.Width;

                dataBand.Border.Side = StiBorderSides.All;
                dataBand.Border.Color = Color.Gainsboro;
                StiCondition conditionDataBand = new StiCondition("{Line%2==0}", Color.Black, Color.PeachPuff, dataBandFont, true);
                dataBand.Conditions.Add(conditionDataBand);
                page.Components.Add(dataBand);



                DataTable dtReport = new DataTable();
                string modelReportName = string.Empty;
                switch (SRT)
                {
                    case SystemReportType.SystemBusinessReport:
                        IList<GTS.Clock.Model.Report.SystemBusinessReport> SystemBusinessReportList = this.GetSystemBusinessReportList(SRT, SrtFilterConditions);
                        dtReport = Utility.ListToDataTable<GTS.Clock.Model.Report.SystemBusinessReport>(SystemBusinessReportList);
                        reportNameText_pageHeader.Text = "System Business Report";
                        modelReportName = dataBand.DataSourceName = typeof(GTS.Clock.Model.Report.SystemBusinessReport).Name;
                        dataBand.Height = dataBandHeight;
                        headerBand.Height = dataHeaderHeight;
                        break;
                    case SystemReportType.SystemEngineReport:
                        IList<GTS.Clock.Model.Report.SystemEngineReport> SystemEngineReportList = this.GetSystemEngineReportList(SRT, SrtFilterConditions);
                        dtReport = Utility.ListToDataTable<GTS.Clock.Model.Report.SystemEngineReport>(SystemEngineReportList);
                        reportNameText_pageHeader.Text = "System Engine Report";
                        modelReportName = dataBand.DataSourceName = typeof(GTS.Clock.Model.Report.SystemEngineReport).Name;
                        dataBand.Height = dataBandHeight;
                        headerBand.Height = dataHeaderHeight;
                        break;
                    case SystemReportType.SystemWindowsServiceReport:
                        IList<GTS.Clock.Model.Report.SystemWindowsServiceReport> SystemWindowsServiceReportList = this.GetSystemWindowsServiceReportList(SRT, SrtFilterConditions);
                        dtReport = Utility.ListToDataTable<GTS.Clock.Model.Report.SystemWindowsServiceReport>(SystemWindowsServiceReportList);
                        reportNameText_pageHeader.Text = "System Windows Service Report";
                        modelReportName = dataBand.DataSourceName = typeof(GTS.Clock.Model.Report.SystemWindowsServiceReport).Name;
                        dataBand.Height = dataBandHeight;
                        headerBand.Height = dataHeaderHeight;
                        break;
                    case SystemReportType.SystemUserActionReport:
                        IList<GTS.Clock.Model.Report.SystemUserActionReport> SystemUserActionReportList = this.GetSystemUserActionReportList(SRT, SrtFilterConditions);
                        dtReport = Utility.ListToDataTable<GTS.Clock.Model.Report.SystemUserActionReport>(SystemUserActionReportList);
                        reportNameText_pageHeader.Text = "System User Action Report";
                        modelReportName = dataBand.DataSourceName = typeof(GTS.Clock.Model.Report.SystemUserActionReport).Name;
                        dataBand.Height = dataBandHeight;
                        headerBand.Height = dataHeaderHeight;
                        break;
                    case SystemReportType.SystemEngineDebugReport:
                        IList<GTS.Clock.Model.Report.SystemEngineDebugReport> SystemEngineDebugReportList = this.GetSystemEngineDebugReportList(SRT, SrtFilterConditions);
                        dtReport = Utility.ListToDataTable<GTS.Clock.Model.Report.SystemEngineDebugReport>(SystemEngineDebugReportList);
                        reportNameText_pageHeader.Text = "System Engine Debug Report";
                        modelReportName = dataBand.DataSourceName = typeof(GTS.Clock.Model.Report.SystemEngineDebugReport).Name;
                        dataBand.Height = dataBandHeight;
                        headerBand.Height = dataHeaderHeight;
                        break;
                    case SystemReportType.SystemDataCollectorReport:
                        IList<GTS.Clock.Model.Report.SystemDataCollectorReport> SystemDataCollectorReportList = this.GetSystemDataCollectorReportList(SRT, SrtFilterConditions);
                        dtReport = Utility.ListToDataTable<GTS.Clock.Model.Report.SystemDataCollectorReport>(SystemDataCollectorReportList);
                        reportNameText_pageHeader.Text = "System Data Collector Report";
                        modelReportName = dataBand.DataSourceName = typeof(GTS.Clock.Model.Report.SystemDataCollectorReport).Name;
                        dataBand.Height = dataBandHeight;
                        headerBand.Height = dataHeaderHeight;
                        break;
                }


                columnWidth = (page.Width) / (dtReport.Columns.Count - 2);
                stiReport.RegData(dtReport);
                stiReport.Dictionary.Synchronize();

                double pos = 0;
                int nameIndex = 1;
                for (int i = 0; i < dtReport.Columns.Count; i++)
                {
                    StiText headerColumnText;
                    switch (dtReport.Columns[i].ColumnName.ToLower())
                    {
                        case "exception":
                            {
                                headerColumnText = new StiText(new RectangleD(columnWidth, 1, (page.Width - columnWidth), 1));

                                break;
                            }
                        case "message":
                            {
                                headerColumnText = new StiText(new RectangleD(columnWidth, 2, (page.Width - columnWidth), 1));
                                break;
                            }
                        case "id":
                            {
                                if (SRT == SystemReportType.SystemUserActionReport)
                                    headerColumnText = new StiText(new RectangleD(pos, 0, columnWidth, 1));
                                else
                                    headerColumnText = new StiText(new RectangleD(pos, 0, columnWidth, dataHeaderHeight));

                                break;
                            }
                        case "cnpname":
                            {

                                headerColumnText = new StiText(new RectangleD(columnWidth, 1, (page.Width - columnWidth), 1));
                                break;
                            }
                        default:
                            headerColumnText = new StiText(new RectangleD(pos, 0, columnWidth, 1));
                            headerColumnText.Width = columnWidth;
                            break;
                    }
                    headerColumnText.HorAlignment = StiTextHorAlignment.Center;
                    headerColumnText.VertAlignment = StiVertAlignment.Center;
                    headerColumnText.Name = "HeaderText" + nameIndex.ToString();
                    headerColumnText.Brush = new StiSolidBrush(Color.FromArgb(191, 191, 191));
                    headerColumnText.Border.Side = StiBorderSides.All;
                    headerColumnText.Font = headerBandFont;
                    headerColumnText.Text.Value = dtReport.Columns[i].Caption;

                    headerBand.Components.Add(headerColumnText);

                    StiText dataText;
                    switch (dtReport.Columns[i].ColumnName.ToLower())
                    {
                        case "exception":
                            {
                                dataText = new StiText(new RectangleD(columnWidth, dataBandRowHeight, (page.Width - columnWidth), 1.5));

                                pos = pos - columnWidth;
                                break;
                            }
                        case "message":
                            {
                                dataText = new StiText(new RectangleD(columnWidth, dataBandRowHeight + 1.5, (page.Width - columnWidth), 1.5));
                                pos = pos - columnWidth;
                                break;
                            }
                        case "id":
                            {
                                if (SRT == SystemReportType.SystemUserActionReport)
                                    dataText = new StiText(new RectangleD(pos, 0, columnWidth, 1));
                                else
                                    dataText = new StiText(new RectangleD(pos, 0, columnWidth, dataBandHeight));

                                break;
                            }
                        case "cnpname":
                            {
                                dataText = new StiText(new RectangleD(columnWidth, dataBandRowHeight, (page.Width - columnWidth), 1.5));
                                pos = pos - columnWidth;

                                break;
                            }
                        default:
                            dataText = new StiText(new RectangleD(pos, 0, columnWidth, dataBandRowHeight));

                            break;
                    }
                    dataText.Type = StiSystemTextType.DataColumn;
                    dataText.Text = "{Trim(" + modelReportName + "." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(dtReport.Columns[i].ColumnName) + ")}";
                    dataText.HorAlignment = StiTextHorAlignment.Center;
                    dataText.VertAlignment = StiVertAlignment.Center;
                    dataText.Name = "DataText" + nameIndex.ToString();
                    dataText.Border.Side = StiBorderSides.All;
                    dataText.Font = dataBandFont;
                    dataText.WordWrap = true;

                    dataBand.Components.Add(dataText);


                    pos = pos + columnWidth;
                    nameIndex++;
                }
                stiReport.Compile();
                return stiReport;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertSystemReportCurrentUserActivity(System.Web.UI.Page page, string action, string objectInfo)
        {
            try
            {
                BusinessActivityLogger businessActivityLogger = new BusinessActivityLogger();
                string username = string.Empty;
                string className = "BSystemReports";
                string methodName = "InsertSystemReportCurrentUserActivity";
                string pageId = Utility.GetCurrentPageID(page);
                string clientIPAddress = string.Empty;

                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
                {
                    username = BUser.CurrentUser.UserName;
                    if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                        clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                }

                businessActivityLogger.Info(username, className, methodName, action, pageId, clientIPAddress, objectInfo);
            }
            catch (Exception ex)
            {                
                BaseBusiness<GTS.Clock.Model.Report.SystemReportTypesDataContext>.LogException(ex, "BSystemReports", "InsertSystemReportCurrentUserActivity");
                throw ex;
            }
        }


    }
}
