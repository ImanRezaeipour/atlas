using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Utility;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Infrastructure.Report
{
    public class ReportHelper
    {
        internal class ReportHelperProxy
        {
            public string LicenseName { get; set; }
            public decimal userId { get; set; }
            public string UserName { get; set; }
            public IList<decimal> ids { get; set; }
            public string OperationGUID { get; set; }
            public string GroupingType { get; set; }
            public bool GroupByNewPage{ get; set; }
            public string headerPartFilterText { get; set; }
            public string headerEmployFilterText { get; set; }
            public IList<decimal> dutyPlaceIds { get; set; }


        }

        internal string licenseName;
        internal decimal userId;
        internal string userName;      
        internal IList<decimal> Ids;
        internal string operationGUID;
        internal string groupingType;
        internal bool groupByNewPage;
        internal string headerPartFilterText;
        internal string headerEmployFilterText;
        internal string[] ReportDllNames = new string[] { "GTS.Clock.Infrastructure.Report.dll", "GTS.Clock.Infrastructure.dll" };
        internal string[] UsingNameSapaces = new string[] { "GTS.Clock.Infrastructure.Report" };
        internal IList<decimal> DutyPlaceIds;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LicenseName"></param>
        /// <param name="UserName"></param>
        /// <param name="ids">لیستی از پارامتر ها که در دیتابیس بعنوان پارامتر نیامده است مانند شناسه پرسنلی</param>
        /// <param name="showSelectCol">گزارشاتی که نیاز به انتخاب ستون دارند</param>
        /// <returns></returns>
        public static ReportHelper Instance(string LicenseName, decimal userId, string UserName, IList<decimal> ids, string OperationGUID, string GroupingType, bool GroupByNewPage, string HeaderPartFilterText, string HeaderEmployFilterText,IList<decimal> dutyPlaceIds)
        {
            //Nested.reportHelper.Init(LicenseName, userId, UserName, ids);
            //return Nested.reportHelper;

            ReportHelper reportHelper = new ReportHelper();
            reportHelper.Init(LicenseName, userId, UserName, ids, OperationGUID);
            ReportHelperProxy reportHelperProxy = new ReportHelperProxy(){
                LicenseName = LicenseName,
                userId = userId,
                UserName = UserName,
                ids = ids,
                OperationGUID = OperationGUID,
                GroupByNewPage=GroupByNewPage,
                GroupingType=GroupingType,
                headerPartFilterText=HeaderPartFilterText,
                headerEmployFilterText=HeaderEmployFilterText,
                dutyPlaceIds  =dutyPlaceIds

            };
            SessionHelper.SaveSessionValue(SessionHelper.ReportHelperSessionName, reportHelperProxy);
            return reportHelper;
        }

        public static ReportHelper Instance()
        {
            //return Nested.reportHelper;            

            ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
            ReportHelper reportHelper = new ReportHelper()
            {
                licenseName = reportHelperProxy.LicenseName,
                userId = reportHelperProxy.userId,
                userName = reportHelperProxy.UserName,
                Ids = reportHelperProxy.ids,
                operationGUID = reportHelperProxy.OperationGUID,
                groupingType=reportHelperProxy.GroupingType,
                groupByNewPage=reportHelperProxy.GroupByNewPage,
                headerPartFilterText=reportHelperProxy.headerPartFilterText,
                headerEmployFilterText=reportHelperProxy.headerEmployFilterText,
                DutyPlaceIds = reportHelperProxy.dutyPlaceIds
            };
            return reportHelper;
        }
        public  DateTime GTSMinStandardDateTime()
        {
            
                return new DateTime(1900, 1, 1);
            
        }
        internal static class Nested
        {
            internal static readonly ReportHelper reportHelper =
                        new ReportHelper();
            static Nested()
            {
            }
        }

        /// <summary>
        /// Return new instance of ReportHelper
        /// </summary>
        public void Init(string LicenseName, decimal userId, string UserName, IList<decimal> ids, string OperationGUID)
        {
            this.licenseName = LicenseName;
            this.userName = UserName;
            this.Ids = ids;
            this.userId = userId;
            this.operationGUID = OperationGUID;
        }

        public string LicenseName
        {
            get
            {
                ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
                return reportHelperProxy.LicenseName;
            }
        }

        public string UserName
        {
            get
            {
                ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
                return reportHelperProxy.UserName;
            }
        }

        public decimal UserId
        {
            get
            {
                ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
                return reportHelperProxy.userId;
            }
        }

        public string OperationGUID
        {
            get
            {
                ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
                return reportHelperProxy.OperationGUID;
            }
        }


        public string IntTimeToRealTime(int Minute)
        {
            return Utility.Utility.IntTimeToRealTime(Minute);
        }

        public string IntTimeToRealTime(decimal Minute)
        {
            return Utility.Utility.IntTimeToRealTime(Minute);
        }

        public string FixedDailyValue(decimal DailyValue)
        {
            if (DailyValue == -1000) return "";
            if (DailyValue == 0) return "";
            return DailyValue.ToString();
        }

        public string IntTimeToTime(decimal Minute)
        {
            return Utility.Utility.IntTimeToTime(Minute); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Minute"></param>
        /// <param name="TotalWidth">طول رشته خروجی</param>
        /// <returns></returns>
        public string IntTimeToTimePadLeft(decimal Minute, int TotalWidth)
        {
            string time = Minute == 0 ? "0" : Utility.Utility.IntTimeToTime(Minute);
            for (; time.Contains(':'); )
            {
                time = time.Remove(time.IndexOf(':'), 1);
            }
            return time.PadLeft(TotalWidth, '0');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="TotalWidth">طول رشته خروجی</param>
        /// <returns></returns>
        public string PadLeftWithZero(string Source, int TotalWidth)
        {
            return Source.PadLeft(TotalWidth, '0');
        }

        public string ShamsiGetNow()
        {
            return PersianDateTime.MiladiToShamsi(DateTime.Now.ToShortDateString()) + " " + DateTime.Now.ToShortTimeString();
        }

        /// <summary>
        /// نام فارسی روز هفته تاریخ وارد شده را برمی گرداند
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public string ShamsiDayName(DateTime Date)
        {
            return PersianDateTime.GetPershianDayName(Date);
        }

        public string MiladiToShamsi(DateTime Date)
        {
            return PersianDateTime.MiladiToShamsi(Date.ToShortDateString());
        }
        public string ToMiladiDate(DateTime Date)
        {
            return Date.ToShortDateString();
        }
        public string GetPishcardName(decimal precardId) 
        {
            if (precardId < 0)
                return precardId.ToString();
            Precard p = Precard.GetPrecardRepository(false).GetById(precardId, false);
            return p.Name;
        }

        /// <summary>
        /// تاریخ میلادی معادل با تاریخ شمسی  آخرین روز، سال و ماه ارسالی را برمیگرداند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetEndOfPersianMonth(int year, int month)
        {
            return Utility.PersianDateTime.ShamsiToMiladi(string.Format("{0}/{1}/{2}", year, month, Utility.Utility.GetEndOfPersianMonth(year, month)));
        }

        /// <summary>
        /// تاریخ میلادی معادل با تاریخ شمسی آخرین روز، سال و ماه جاری را برمیگرداند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public string GetEndOfPersianMonth()
        {
            string[] ShamsiDate = Utility.PersianDateTime.MiladiToShamsi(DateTime.Now.ToString("yyyy/MM/dd")).Split('/');
            return Utility.PersianDateTime.ShamsiToMiladi(string.Format("{0}/{1}/{2}", ShamsiDate[0], ShamsiDate[1], Utility.Utility.GetEndOfPersianMonth(Convert.ToInt32(ShamsiDate[0]), Convert.ToInt32(ShamsiDate[1]))));
        }

        /// <summary>
        /// آخر سال شمسی را برمیگرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetEndOfPersianYear(DateTime date) 
        {
            PersianDateTime pd = new PersianDateTime(date);
            DateTime endOfYear = Utility.Utility.ToMildiDate(String.Format("{0}/12/{1}", pd.Year, Utility.Utility.GetEndOfPersianMonth(pd.Year, 12)));
            return endOfYear;
        }

        /// <summary>
        /// تعداد روزهای ماههای میلادی را برمیگرداند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetEndOfMiladiMonth(int year, int month)
        {
            return Utility.Utility.GetEndOfMiladiMonth(year, month);
        }


        /// <summary>
        /// یک شی از نوع "گزارش" را براساس پارامتر ورودی بر می گرداند
        /// </summary>
        /// <param name="strReport">report xml file in database</param>
        /// <returns></returns>
        public StiReport GetReport(string strReport)
        {
            StiReport Result = new StiReport();
            Result.LoadFromString(strReport);
            return Result;
        }

        /// <summary>
        /// Attach referenced assemblies to report
        /// </summary>
        /// <param name="Report"></param>
        /// <returns></returns>
        public StiReport InitAssemblyReport(StiReport Report)
        {
            foreach (string usingNamespace in UsingNameSapaces)
            {
                Report.Script = string.Format("using {0};\n{1}", usingNamespace, Report.Script);
            }

            IList<string> ReferencedAssemblies = Report.ReferencedAssemblies.ToList();
            foreach (string assemblyName in ReportDllNames)
            {
                ReferencedAssemblies.Add(assemblyName);
            }
            Report.ReferencedAssemblies = ReferencedAssemblies.ToArray();
            return Report;
        }

        /// <summary>
        /// مقدار دهی پارامتر های گزارش
        /// </summary>
        /// <param name="Report"></param>
        /// <param name="ParamValues"></param>
        /// <returns></returns>
        public StiReport InitReportParameter(StiReport Report, IDictionary<string, object> ParamValues)
        {
            foreach (string param in ParamValues.Keys)
            {
                try
                {
                    Report[param] = ParamValues[param];
                }
                catch
                {
                    ///TODO: ایجاد خطای مرتبط
                    throw new Exception();
                }
            }
            return Report;
        }

        public StiReport InitReportConnection(StiReport Report, string ConnectionString)
        {
            foreach (StiDatabase db in Report.Dictionary.Databases)
            {
                ((StiSqlDatabase)db).ConnectionString = ConnectionString;
            }
            return Report;
        }

        /// <summary>
        /// شناسه های ورودی را به صورت رشته هایی که با "،" از هم جدا شده اند برمی گرداند
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public string ModifiedIds()
        {
            ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
            this.Ids = reportHelperProxy.ids;
            if (this.Ids == null)
                throw new InvalidOperationException("مقداردهی اولیه به متغیرهای مورد استفاده در این متد انجام نشده است");
            if (this.Ids.Count == 0)
                return "";
            string Result = Convert.ToString(Ids[0]);
            for (int i = 1; i < this.Ids.Count; i++)
            {
                Result = String.Format("{0},{1}", Result, Convert.ToString(this.Ids[i]));
            }
            return Result;
        }

        public string ModifiedIds(IList<decimal> idList)
        {
            if (idList == null)
                throw new InvalidOperationException("مقداردهی اولیه به متغیرهای مورد استفاده در این متد انجام نشده است");
            if (idList.Count == 0)
                return "";
            string Result = Convert.ToString(idList[0]);
            for (int i = 1; i < idList.Count; i++)
            {
                Result = String.Format("{0},{1}", Result, Convert.ToString(idList[i]));
            }
            return Result;
        }
        public string DutyPlaceModifiedIds()
        {
            ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
            this.DutyPlaceIds = reportHelperProxy.dutyPlaceIds;
            if (this.DutyPlaceIds == null)
                throw new InvalidOperationException("مقداردهی اولیه به متغیرهای مورد استفاده در این متد انجام نشده است");
            if (this.DutyPlaceIds.Count == 0)
                return "";
            string Result = Convert.ToString(DutyPlaceIds[0]);
            for (int i = 1; i < this.DutyPlaceIds.Count; i++)
            {
                Result = String.Format("{0},{1}", Result, Convert.ToString(this.DutyPlaceIds[i]));
            }
            return Result;
        }
        public string GetHeaderPartFilter()
        {
            ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
            return reportHelperProxy.headerPartFilterText;
            
        }
        public string GetHeaderEmployFilter()
        {
            ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
            return reportHelperProxy.headerEmployFilterText;

        }
        public string GetGroupingType()
        {
            ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
            return reportHelperProxy.GroupingType;
        }


        public bool GetGroupByNewPage()
        {
            ReportHelperProxy reportHelperProxy = (ReportHelperProxy)SessionHelper.GetSessionValue(SessionHelper.ReportHelperSessionName);
            return reportHelperProxy.GroupByNewPage;
        }





    }
}
