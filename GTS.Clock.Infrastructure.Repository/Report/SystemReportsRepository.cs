using GTS.Clock.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.Utility;


namespace GTS.Clock.Infrastructure.Repository
{
    public class SystemReportsRepository
    {
        private SystemReportTypesDataContext srtDataContext = null;
        public SystemReportTypesDataContext SrtDataContext
        {
            get
            {
                if (this.srtDataContext == null)
                    this.srtDataContext = new SystemReportTypesDataContext();
                return this.srtDataContext;
            }
        }

        public int GetSystemReportTypeCount(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions,LanguagesName appLanguage)
        {
            int SystemReportTypeCount = 0;
            switch (SRT)
            {
                case SystemReportType.SystemBusinessReport:
                    System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>> SystemBusinessReportExpression = (System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
                    SystemReportTypeCount = this.SrtDataContext.SystemBusinessReports.Where(SystemBusinessReportExpression).Count();
                    break;
                case SystemReportType.SystemEngineReport:
                    System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>> SystemEngineReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
                    SystemReportTypeCount = this.SrtDataContext.SystemEngineReports.Where(SystemEngineReportExpression).Count();
                    break;
                case SystemReportType.SystemWindowsServiceReport:
                    System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>> SystemWindowsServiceReportExpression = (System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
                    SystemReportTypeCount = this.SrtDataContext.SystemWindowsServiceReports.Where(SystemWindowsServiceReportExpression).Count();
                    break;
                case SystemReportType.SystemUserActionReport:
                    System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>> SystemUserActionReportExpression = (System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
                    SystemReportTypeCount = this.SrtDataContext.SystemUserActionReports.Where(SystemUserActionReportExpression).Count();
                    break;
                case SystemReportType.SystemEngineDebugReport:
                    System.Linq.Expressions.Expression<Func<SystemEngineDebugReport, bool>> SystemEngineDebugReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineDebugReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
                    SystemReportTypeCount = this.SrtDataContext.SystemEngineDebugReports.Where(SystemEngineDebugReportExpression).Count();
                    break;
                case SystemReportType.SystemDataCollectorReport:
                    System.Linq.Expressions.Expression<Func<SystemDataCollectorReport, bool>> SystemDataCollectorReportExpression = (System.Linq.Expressions.Expression<Func<SystemDataCollectorReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
                    SystemReportTypeCount = this.SrtDataContext.SystemDataCollectorReports.Where(SystemDataCollectorReportExpression).Count();
                    break;
            }
            return SystemReportTypeCount;
        }

        private object GetSystemReportTypeFilterConditions(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            object expresionObj = null;
            Dictionary<string, DateTime> SystemReportTypeConditionsDatesDic = this.GetSystemReportTypeConditionsDates(SrtFilterConditions, appLanguage);
            DateTime FromDate = SystemReportTypeConditionsDatesDic["FromDate"];
            DateTime ToDate = SystemReportTypeConditionsDatesDic["ToDate"];
            string SearchTerm = SrtFilterConditions.SearchTerm;

            switch (SRT)
            {
                case SystemReportType.SystemBusinessReport:
                    System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>> SystemBusinessReportExpresion = x => (x.Username.Contains(SearchTerm) ||
                                                                                                                               x.IPAddress.Contains(SearchTerm) ||
                                                                                                                               x.ClassName.Contains(SearchTerm) ||
                                                                                                                               x.MethodName.Contains(SearchTerm) ||
                                                                                                                               x.Message.Contains(SearchTerm) ||
                                                                                                                               x.Level.Contains(SearchTerm) ||
                                                                                                                               x.Exception.Contains(SearchTerm) ||
                                                                                                                               x.ExceptionSource.Contains(SearchTerm)) &&
                                                                                                                               x.Date.Date >= FromDate &&
                                                                                                                               x.Date.Date <= ToDate;
                    expresionObj = (object)SystemBusinessReportExpresion;
                    break;
                case SystemReportType.SystemEngineReport:
                    System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>> SystemEngineReportExpression = x => (x.PersonBarcode.Contains(SearchTerm) ||
                                                                                                                            x.Level.Contains(SearchTerm) ||
                                                                                                                            x.Message.Contains(SearchTerm) ||
                                                                                                                            x.Exception.Contains(SearchTerm)) &&
                                                                                                                            x.Date.Date >= FromDate &&
                                                                                                                            x.Date.Date <= ToDate;
                    expresionObj = (object)SystemEngineReportExpression;
                    break;
                case SystemReportType.SystemWindowsServiceReport:
                    System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>> SystemWindowsServiceReportExpression = x => (x.Level.Contains(SearchTerm) ||
                                                                                                                                            x.Message.Contains(SearchTerm) ||
                                                                                                                                            x.Exception.Contains(SearchTerm)) &&
                                                                                                                                            x.Date.Date >= FromDate &&
                                                                                                                                            x.Date.Date <= ToDate;
                    expresionObj = (object)SystemWindowsServiceReportExpression;
                    break;
                case SystemReportType.SystemUserActionReport:
                    System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>> SystemUserActionReportExpression = x => (x.Username.Contains(SearchTerm) ||
                                                                                                                                    x.IPAddress.Contains(SearchTerm) ||
                                                                                                                                    x.PageID.Contains(SearchTerm) ||
                                                                                                                                    x.ClassName.Contains(SearchTerm) ||
                                                                                                                                    x.MethodName.Contains(SearchTerm) ||
                                                                                                                                    x.Action.Contains(SearchTerm) ||
                                                                                                                                    x.ObjectInformation.Contains(SearchTerm)) &&
                                                                                                                                    x.Date.Value.Date >= FromDate &&
                                                                                                                                    x.Date.Value.Date <= ToDate;
                    expresionObj = (object)SystemUserActionReportExpression;
                    break;

                case SystemReportType.SystemEngineDebugReport:
                    System.Linq.Expressions.Expression<Func<SystemEngineDebugReport, bool>> SystemEngineDebugReportExpression = x => (x.CnpIden.Contains(SrtFilterConditions.Concept) ||
                                                                                                                                      x.cnpName.Contains(SrtFilterConditions.Concept)) &&
                                                                                                                                      x.RuleIden.Contains(SrtFilterConditions.RuleCode) &&
                                                                                                                                      x.MiladiDate.Value.Date >= FromDate &&
                                                                                                                                      x.MiladiDate.Value.Date <= ToDate;
                    expresionObj = (object)SystemEngineDebugReportExpression;
                    break;

                case SystemReportType.SystemDataCollectorReport:
                    System.Linq.Expressions.Expression<Func<SystemDataCollectorReport, bool>> SystemDataCollectorReportExpression = x => (x.PersonBarcode.Contains(SearchTerm) ||
                                                                                                                                          x.Status.Contains(SearchTerm) ||
                                                                                                                                          x.DeviceID.Contains(SearchTerm) ||
                                                                                                                                          x.Message.Contains(SearchTerm)) &&
                                                                                                                                         (x.TrafficDateTime.Value.Date >= FromDate &&
                                                                                                                                          x.TrafficDateTime.Value.Date <= ToDate) || (x.RecieveDateTime.Value.Date >= FromDate && x.RecieveDateTime.Value.Date <= FromDate);                                                                                                                                                  
                    expresionObj = (object)SystemDataCollectorReportExpression;
                    break;                                                                                                                                                                        
            }
            return expresionObj;
        }

        public IList<SystemBusinessReport> GetSystemBusinessReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions,LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>> SystemBusinessReportExpression = (System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemBusinessReport> SystemBusinessReportList = this.SrtDataContext.SystemBusinessReports.Where(SystemBusinessReportExpression)
                                                                                                            .OrderByDescending(x => x.Date)
                                                                                                            .Skip(PageIndex * PageSize)
                                                                                                            .Take(PageSize)
                                                                                                            .AsEnumerable()
                                                                                                            .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                            .Select(x => { x.UITime = x.Date.ToString("HH:mm:ss"); return x; })
                                                                                                            .ToList();
            return SystemBusinessReportList;
        }
        public IList<SystemBusinessReport> GetSystemBusinessReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
		{
			//ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>> SystemBusinessReportExpression = (System.Linq.Expressions.Expression<Func<SystemBusinessReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
			IList<SystemBusinessReport> SystemBusinessReportList = this.SrtDataContext.SystemBusinessReports.Where(SystemBusinessReportExpression)
																											.OrderByDescending(x => x.Date)
																											.AsEnumerable()
                                                                                                            .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                            .Select(x => { x.UITime = x.Date.ToString("HH:mm:ss"); return x; })
                                                                                                            .ToList();
			return SystemBusinessReportList;
		}
        public IList<SystemEngineReport> GetSystemEngineReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>> SystemEngineReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemEngineReport> SystemEngineReportList = this.SrtDataContext.SystemEngineReports.Where(SystemEngineReportExpression)
                                                                                                      .OrderByDescending(x => x.Date)
                                                                                                      .Skip(PageIndex * PageSize)
                                                                                                      .Take(PageSize)
                                                                                                      .AsEnumerable()
                                                                                                      .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                      .Select(x => { x.UITime = x.Date.ToString("HH:mm:ss"); return x; })
                                                                                                      .ToList();
            return SystemEngineReportList;
        }
        public IList<SystemEngineReport> GetSystemEngineReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
		{
			//ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>> SystemEngineReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
			IList<SystemEngineReport> SystemEngineReportList = this.SrtDataContext.SystemEngineReports.Where(SystemEngineReportExpression)
																									  .OrderByDescending(x => x.Date)
																									  .AsEnumerable()
                                                                                                      .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                      .Select(x => { x.UITime = x.Date.ToString("HH:mm:ss"); return x; })
																									  .ToList();
			return SystemEngineReportList;
		}
        public IList<SystemWindowsServiceReport> GetSystemWindowsServiceReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>> SystemWindowsServiceReportExpression = (System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemWindowsServiceReport> SystemWindowsServiceReportList = this.SrtDataContext.SystemWindowsServiceReports.Where(SystemWindowsServiceReportExpression)
                                                                                                                              .OrderByDescending(x => x.Date)
                                                                                                                              .Skip(PageIndex * PageSize)
                                                                                                                              .Take(PageSize)
                                                                                                                              .AsEnumerable()
                                                                                                                              .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                                              .Select(x => { x.UITime = x.Date.ToString("HH:mm:ss"); return x; })
                                                                                                                              .ToList();
            return SystemWindowsServiceReportList;
        }
        public IList<SystemWindowsServiceReport> GetSystemWindowsServiceReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
		{
			//ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>> SystemWindowsServiceReportExpression = (System.Linq.Expressions.Expression<Func<SystemWindowsServiceReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
			IList<SystemWindowsServiceReport> SystemWindowsServiceReportList = this.SrtDataContext.SystemWindowsServiceReports.Where(SystemWindowsServiceReportExpression)
																															  .OrderByDescending(x => x.Date)
																															  .AsEnumerable()
                                                                                                                              .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? Utility.Utility.ToPersianDate(x.Date) : x.Date.ToShortDateString(); return x; })
                                                                                                                              .Select(x => { x.UITime = x.Date.ToString("HH:mm:ss"); return x; })
																															  .ToList();
			return SystemWindowsServiceReportList;
		}
        public IList<SystemUserActionReport> GetSystemUserActionReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>> SystemUserActionReportExpression = (System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemUserActionReport> SystemUserActionReportList = this.SrtDataContext.SystemUserActionReports.Where(SystemUserActionReportExpression)
                                                                                                                  .OrderByDescending(x => x.Date)
                                                                                                                  .Skip(PageIndex * PageSize)
                                                                                                                  .Take(PageSize)
                                                                                                                  .AsEnumerable()
                                                                                                                  .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? x.Date != null ? Utility.Utility.ToPersianDate(x.Date ?? DateTime.MinValue) : string.Empty : x.Date != null ? x.Date.Value.ToShortDateString() : string.Empty; return x; })
                                                                                                                  .Select(x => { x.UITime = (x.Date ?? DateTime.MinValue).ToString("HH:mm:ss"); return x; })
                                                                                                                  .ToList();
            return SystemUserActionReportList;
        }
        public IList<SystemUserActionReport> GetSystemUserActionReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
		{
			//ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>> SystemUserActionReportExpression = (System.Linq.Expressions.Expression<Func<SystemUserActionReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
			IList<SystemUserActionReport> SystemUserActionReportList = this.SrtDataContext.SystemUserActionReports.Where(SystemUserActionReportExpression)
																												  .OrderByDescending(x => x.Date)
																												  .AsEnumerable()
                                                                                                                  .Select(x => { x.UIDate = appLanguage == LanguagesName.Parsi ? x.Date != null ? Utility.Utility.ToPersianDate(x.Date ?? DateTime.MinValue) : string.Empty : x.Date != null ? x.Date.Value.ToShortDateString() : string.Empty; return x; })
                                                                                                                  .Select(x => { x.UITime = (x.Date ?? DateTime.MinValue).ToString("HH:mm:ss"); return x; })
																												  .ToList();
			return SystemUserActionReportList;
		}

        public IList<SystemEngineDebugReport> GetSystemEngineDebugReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemEngineDebugReport, bool>> SystemEngineDebugReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineDebugReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemEngineDebugReport> SystemEngineDebugReportList = this.SrtDataContext.SystemEngineDebugReports.Where(SystemEngineDebugReportExpression)
                                                                                                                     .Skip(PageIndex * PageSize)
                                                                                                                     .Take(PageSize)
                                                                                                                     .ToList();
            return SystemEngineDebugReportList;
        }

        public IList<SystemEngineDebugReport> GetSystemEngineDebugReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemEngineDebugReport, bool>> SystemEngineDebugReportExpression = (System.Linq.Expressions.Expression<Func<SystemEngineDebugReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemEngineDebugReport> SystemEngineDebugReportList = this.SrtDataContext.SystemEngineDebugReports.Where(SystemEngineDebugReportExpression)
                                                                                                                     .ToList();
            return SystemEngineDebugReportList;
        }

        public IList<SystemDataCollectorReport> GetSystemDataCollectorReportList(SystemReportType SRT, int PageSize, int PageIndex, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemDataCollectorReport, bool>> SystemDataCollectorReportExpression = (System.Linq.Expressions.Expression<Func<SystemDataCollectorReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemDataCollectorReport> SystemDataCollectorReportList = this.SrtDataContext.SystemDataCollectorReports.Where(SystemDataCollectorReportExpression)
                                                                                                                  .OrderByDescending(x => x.TrafficDateTime)
                                                                                                                  .Skip(PageIndex * PageSize)
                                                                                                                  .Take(PageSize)
                                                                                                                  .AsEnumerable()
                                                                                                                  .Select(x => { x.TrafficDate = appLanguage == LanguagesName.Parsi ? x.TrafficDateTime != null ? Utility.Utility.ToPersianDate(x.TrafficDateTime ?? DateTime.MinValue) : string.Empty : x.TrafficDateTime != null ? x.TrafficDateTime.Value.ToShortDateString() : string.Empty; return x; })
                                                                                                                  .Select(y => { y.RecieveDate = appLanguage == LanguagesName.Parsi ? y.RecieveDateTime != null ? Utility.Utility.ToPersianDate(y.RecieveDateTime ?? DateTime.MinValue) : string.Empty : y.RecieveDateTime != null ? y.TrafficDateTime.Value.ToShortDateString() : string.Empty; return y; })
                                                                                                                  .Select(xt => { xt.TrafficTime = xt.TrafficDateTime != null ? xt.TrafficDateTime.Value.TimeOfDay.ToString() : string.Empty; return xt; })
                                                                                                                  .Select(yt => { yt.RecieveTime = yt.RecieveDateTime != null ? yt.RecieveDateTime.Value.TimeOfDay.ToString() : string.Empty; return yt; })
                                                                                                                  .ToList();
            return SystemDataCollectorReportList;
        }

        public IList<SystemDataCollectorReport> GetSystemDataCollectorReportList(SystemReportType SRT, SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            //ApplicationLanguageSettings AppLanguageSettings = this.GetCurrentApplicationLanguageSettings();
            System.Linq.Expressions.Expression<Func<SystemDataCollectorReport, bool>> SystemDataCollectorReportExpression = (System.Linq.Expressions.Expression<Func<SystemDataCollectorReport, bool>>)this.GetSystemReportTypeFilterConditions(SRT, SrtFilterConditions, appLanguage);
            IList<SystemDataCollectorReport> SystemDataCollectorReportList = this.SrtDataContext.SystemDataCollectorReports.Where(SystemDataCollectorReportExpression)
                                                                                                                  .OrderByDescending(x => x.TrafficDateTime)
                                                                                                                  .AsEnumerable()
                                                                                                                  .Select(x => { x.TrafficDate = appLanguage == LanguagesName.Parsi ? x.TrafficDateTime != null ? Utility.Utility.ToPersianDate(x.TrafficDateTime ?? DateTime.MinValue) : string.Empty : x.TrafficDateTime != null ? x.TrafficDateTime.Value.ToShortDateString() : string.Empty; return x; })
                                                                                                                  .Select(y => { y.RecieveDate = appLanguage == LanguagesName.Parsi ? y.RecieveDateTime != null ? Utility.Utility.ToPersianDate(y.RecieveDateTime ?? DateTime.MinValue) : string.Empty : y.RecieveDateTime != null ? y.TrafficDateTime.Value.ToShortDateString() : string.Empty; return y; })
                                                                                                                  .Select(xt => { xt.TrafficTime = xt.TrafficDateTime != null ? xt.TrafficDateTime.Value.ToShortTimeString() : string.Empty; return xt; })
                                                                                                                  .Select(yt => { yt.RecieveTime = yt.RecieveDateTime != null ? yt.RecieveDateTime.Value.ToShortTimeString() : string.Empty; return yt; })
                                                                                                                  .ToList();
            return SystemDataCollectorReportList;
        }
                                                                                                                
        public void DeleteAllSystemReportType<T>() where T : class
        {
            this.SrtDataContext.ExecuteCommand("TRUNCATE TABLE " + this.SrtDataContext.Mapping.GetTable(typeof(T)).TableName);
            //this.SrtDataContext.GetTable<T>().DeleteAllOnSubmit<T>(this.SrtDataContext.GetTable<T>());
            //this.SrtDataContext.SubmitChanges();
        }

        private Dictionary<string, DateTime> GetSystemReportTypeConditionsDates(SystemReportTypeFilterConditions SrtFilterConditions, LanguagesName appLanguage)
        {
            Dictionary<string, DateTime> SystemReportTypeConditionsDatesDic = new Dictionary<string, DateTime>();
            //ApplicationLanguageSettings appLangSet = this.GetCurrentApplicationLanguageSettings();
            switch (appLanguage)
            {
                case LanguagesName.Parsi:
                    SystemReportTypeConditionsDatesDic.Add("FromDate", SrtFilterConditions.FromDate != string.Empty ? Utility.Utility.ToMildiDate(SrtFilterConditions.FromDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                    SystemReportTypeConditionsDatesDic.Add("ToDate", SrtFilterConditions.ToDate != string.Empty ? Utility.Utility.ToMildiDate(SrtFilterConditions.ToDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                    break;
                case LanguagesName.English:
                    SystemReportTypeConditionsDatesDic.Add("FromDate", SrtFilterConditions.FromDate != string.Empty ? Utility.Utility.ToMildiDateTime(SrtFilterConditions.FromDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                    SystemReportTypeConditionsDatesDic.Add("ToDate", SrtFilterConditions.ToDate != string.Empty ? Utility.Utility.ToMildiDateTime(SrtFilterConditions.ToDate) : (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                    break;
            }
            return SystemReportTypeConditionsDatesDic;
        }

        private ApplicationLanguageSettings GetCurrentApplicationLanguageSettings()
        {
            EntityRepository<ApplicationLanguageSettings> appRep = new EntityRepository<ApplicationLanguageSettings>(false);
            ApplicationLanguageSettings appLangSet = appRep.GetByCriteria(new CriteriaStruct(GTS.Clock.Infrastructure.Utility.Utility.GetPropertyName(() => new ApplicationLanguageSettings().IsActive), true)).FirstOrDefault();
            return appLangSet;
        }
    }


}
