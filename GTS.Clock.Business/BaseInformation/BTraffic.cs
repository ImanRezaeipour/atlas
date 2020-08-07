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
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Temp;
using NHibernate.Transform;
using NHibernate.Criterion;
namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// تردد هاُ
    /// created at: 4/10/2012 9:57:35 AM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BTraffic : BaseBusiness<BasicTraffic>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BTraffic";
        private EntityRepository<BasicTraffic> objectRep = new EntityRepository<BasicTraffic>();
        NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        static int trafficCount = 0;
        static int trafficErrorOccuredCounter = 0;
        static int trafficCounter = 0;
        private BTemp bTemp = new BTemp();

        /// <summary>
        /// روزهای ماه را برمیگرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>لیست پروکسی روز</returns>
        public IList<DayDateProxy> GetDayList(decimal personId, int year, int month)
        {
            try
            {
                IList<DayDateProxy> result = new List<DayDateProxy>();
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                int counter = 1;
                for (DateTime day = fromDate; day <= toDate; day = day.AddDays(1))
                {
                    DayDateProxy proxy = new DayDateProxy();
                    proxy.RowID = counter;
                    counter++;
                    proxy.Date = Utility.ToString(day);
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.TheDate = Utility.ToPersianDate(day);
                        proxy.DayName = PersianDateTime.GetPershianDayName(day);
                    }
                    else
                    {
                        proxy.TheDate = Utility.ToString(day);
                        proxy.DayName = PersianDateTime.GetEnglishDayName(day);
                    }
                    result.Add(proxy);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetDayList");
                throw ex;
            }

        }

        /// <summary>
        /// ترددهای یک روز را برمیگرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="miladiDate">تاریخ</param>
        /// <returns>لیست پروکسی ترددها</returns>
        public IList<BasicTrafficProxy> GetDayTraffics(decimal personId, string miladiDate)
        {
            try
            {
                IList<BasicTrafficProxy> result = new List<BasicTrafficProxy>();
                IList<BasicTraffic> basicList = objectRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Date), Utility.ToMildiDateTime(miladiDate)),
                                          new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Active), true),
                                          new CriteriaStruct(Utility.GetPropertyName(() => new BasicTraffic().Person), new Person() { ID = personId }));
                basicList = basicList.OrderBy(x => x.Time).ToList();

                foreach (BasicTraffic t in basicList)
                {
                    BasicTrafficProxy proxy = new BasicTrafficProxy();
                    proxy.ID = t.ID;
                    proxy.ClockName = t.Clock != null ? t.Clock.Name : "";
                    proxy.TheTime = Utility.IntTimeToTime(t.Time);
                    proxy.PrecardName = t.Precard.Name;
                    proxy.OpName = t.OperatorPerson != null ? t.OperatorPerson.Name : "";
                    proxy.Description = t.Description != null ? t.Description : string.Empty;
                    result.Add(proxy);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetDayTraffics");
                throw ex;
            }
        }

        /// <summary>
        /// غیر فعال کردن تردد یک پرسنل
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="trafficId">کلید اصلی تردد</param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteTraffic(decimal personId, decimal trafficId)
        {
            try
            {
                BasicTraffic basicTraffic = base.GetByID(trafficId);
                basicTraffic.Active = false;
                basicTraffic.OperatorPerson = new Person() { ID = personId };
                objectRep.WithoutTransactUpdate(basicTraffic);
                UpdateCFP(basicTraffic, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "DeleteTraffic");
                throw ex;
            }
        }

        /// <summary>
        /// غیر فعال کردن ترددهای یک پرسنل
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="trafficIdsList">لیست کلید اصلی تردد</param>
        public void DeleteTraffics(decimal personId, IList<decimal> trafficIdsList)
        {
            try
            {
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {
                    IList<BasicTraffic> basicTrafficsList = new List<BasicTraffic>();
                    foreach (decimal trafficId in trafficIdsList)
                    {
                        BasicTraffic basicTraffic = base.GetByID(trafficId);
                        basicTraffic.Active = false;
                        basicTraffic.OperatorPerson = new Person() { ID = personId };
                        basicTrafficsList.Add(basicTraffic);
                        objectRep.WithoutTransactSave(basicTraffic);
                    }
                    if (basicTrafficsList.Count() > 0)
                        UpdateCFP(basicTrafficsList.OrderBy(x => x.Date).First(), UIActionType.EDIT);
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                }
            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BTraffic", "DeleteTraffics");
                throw ex;
            }
        }

        /// <summary>
        /// درج تردد در دیتابیس
        /// </summary>
        /// <param name="operatorPersonID">کلید اصلی اپراتور</param>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="precardId">کلید اصلی پیش کارت</param>
        /// <param name="date">تاریخ</param>
        /// <param name="time">زمان</param>
        /// <param name="description">شرح</param>
        /// <returns>کلید اصلی تردد</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertTraffic(decimal operatorPersonID, decimal personId, decimal precardId, string date, string time, string description)
        {
            try
            {
                BasicTraffic basic = new BasicTraffic();
                basic.Person = new Person() { ID = personId };
                basic.Precard = new Precard() { ID = precardId };
                basic.OperatorPerson = new Person() { ID = operatorPersonID };
                basic.Time = Utility.RealTimeToIntTime(time);
                basic.Description = description;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    basic.Date = Utility.ToMildiDate(date);
                }
                else
                {
                    basic.Date = Utility.ToMildiDateTime(date);
                }
                basic.Active = true;
                basic.Used = false;
                basic.Manual = true;
                base.SaveChanges(basic, UIActionType.ADD);
                return basic.ID;

            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "InsertTraffic");
                throw ex;
            }
        }

        /// <summary>
        /// نوع های تردد را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public IList<Precard> GetTrafficTypes()
        {
            try
            {

                EntityRepository<PrecardGroups> groupRep = new EntityRepository<PrecardGroups>(false);
                PrecardGroups group = groupRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrecardGroups().LookupKey), PrecardGroupsName.traffic.ToString())).FirstOrDefault();
                List<Precard> list = new List<Precard>();
                if (group != null)
                {
                    list = group.PrecardList.Where(x => x.IsHourly && x.Active).ToList();
                    foreach (Precard p in list)
                        p.IsTraffic = true;
                }

                list = list.Where(x => x.AccessRoleList.Where(y => y.ID == BUser.CurrentUser.Role.ID).Count() > 0).ToList();
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetTrafficTypes");
                throw ex;
            }
        }

        /// <summary>
        /// گزارش کارکرد برای یک سطر رابرمیگرداند
        /// تا یک روز بعد محاسبات انجام میگیرد
        /// </summary>
        /// <param name="personId">کلید اصلی تردد</param>
        /// <param name="miladiDate">تاریخ میلادی</param>
        /// <returns>پروکسی گزارش کارکرد روزانه</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public DailyReportProxy GetPersonDailyReport(decimal personId, string miladiDate)
        {
            try
            {
                DateTime dayDate = Utility.ToMildiDateTime(miladiDate);
                BPersonMonthlyWorkedTime monthlyReport = new BPersonMonthlyWorkedTime(personId);
                PersonalMonthlyReportRow row = monthlyReport.GetPersonDailyReport(dayDate);

                DailyReportProxy proxy = new DailyReportProxy();
                proxy.AllowableOverTime = row.AllowableOverTime;
                proxy.DailyAbsence = row.DailyAbsence;
                proxy.DailyMission = row.DailyMission;
                proxy.HourlyMeritoriouslyLeave = row.HourlyMeritoriouslyLeave;
                proxy.HourlyMission = row.HourlyMission;
                proxy.HourlyPureOperation = row.HourlyPureOperation;
                proxy.HourlySickLeave = row.HourlySickLeave;
                proxy.HourlyUnallowableAbsence = row.HourlyUnallowableAbsence;
                proxy.HourlyWithoutPayLeave = row.HourlyWithoutPayLeave;
                proxy.ShiftPairs = row.ShiftPairs;

                return proxy;
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "GetPersonDailyReport");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TTM"></param>
        /// <param name="machineID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="fromRecord"></param>
        /// <param name="toRecord"></param>
        /// <param name="fromIdentifier"></param>
        /// <param name="toIdentifier"></param>
        /// <param name="transferDay"></param>
        /// <param name="transferTime"></param>
        /// <param name="TTT"></param>
        /// <param name="IsIntegralConditions"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void TransferTrafficsByConditions(TrafficTransferMode TTM, decimal machineID, string fromDate, string toDate, string fromTime, string toTime, int fromRecord, int toRecord, decimal fromIdentifier, decimal toIdentifier, string transferDay, string transferTime, TrafficTransferType TTT, bool IsIntegralConditions)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorPersonID"></param>
        /// <param name="TTM"></param>
        /// <param name="machineID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="fromRecord"></param>
        /// <param name="toRecord"></param>
        /// <param name="fromIdentifier"></param>
        /// <param name="toIdentifier"></param>
        /// <param name="transferDay"></param>
        /// <param name="transferTime"></param>
        /// <param name="TTT"></param>
        /// <param name="IsIntegralConditions"></param>
        public void TransferTraffics(decimal operatorPersonID, TrafficTransferMode TTM, decimal machineID, string fromDate, string toDate, string fromTime, string toTime, int fromRecord, int toRecord, decimal fromIdentifier, decimal toIdentifier, string transferDay, string transferTime, TrafficTransferType TTT, bool IsIntegralConditions)
        {
            try
            {
                GTS.Clock.Infrastructure.Repository.BasicTrafficRepository basicTrafficRepository = new Infrastructure.Repository.BasicTrafficRepository();
                BLanguage LanguageBusiness = new BLanguage();
                DateTime FromDate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
                switch (LanguageBusiness.GetCurrentSysLanguage())
                {
                    case "fa-IR":
                        FromDate = Utility.ToMildiDate(fromDate);
                        ToDate = Utility.ToMildiDate(toDate);
                        break;
                    case "en-US":
                        FromDate = Utility.ToMildiDateTime(fromDate);
                        ToDate = Utility.ToMildiDateTime(toDate);
                        break;
                }
                if (DateTime.Compare(FromDate, ToDate) > 0)
                    FromDate = ToDate;
                int FromTime = Utility.RealTimeToIntTime(fromTime);
                int ToTime = Utility.RealTimeToIntTime(toTime);
                int TransferTime = Utility.RealTimeToIntTime(transferTime);
                int TransferDay = 0;
                if (transferDay != null && transferDay != string.Empty)
                    TransferDay = int.Parse(transferDay);
                int TargetTime = 0;

                switch (TTM)
                {
                    case TrafficTransferMode.RecordBase:
                        int BasicTrafficsRowCount = basicTrafficRepository.GetBasicTrfficsRowCount();
                        toRecord = toRecord > 0 ? toRecord <= BasicTrafficsRowCount ? toRecord : BasicTrafficsRowCount : 1;
                        fromRecord = fromRecord > 0 ? fromRecord <= toRecord ? fromRecord : 1 : 1;
                        break;
                    case TrafficTransferMode.IdentifierBase:
                        decimal BasicTrafficsLastRowIdentifier = basicTrafficRepository.GetBaseTrafficsLastRowIdentifier();
                        toIdentifier = toIdentifier > 0 ? toIdentifier <= BasicTrafficsLastRowIdentifier ? toIdentifier : BasicTrafficsLastRowIdentifier : 0;
                        fromIdentifier = fromIdentifier >= 0 ? fromIdentifier <= toIdentifier ? fromIdentifier : 0 : 0;
                        break;
                }

                IList<BasicTraffic> BasicTrafficsList = basicTrafficRepository.GetAllBaiscTrafficsByConditions(TTM, machineID, FromDate, ToDate, FromTime, ToTime, fromRecord, toRecord, fromIdentifier, toIdentifier, IsIntegralConditions);
                trafficCount = BasicTrafficsList.Count;

                foreach (BasicTraffic BasicTrafficsListItem in BasicTrafficsList)
                {
                    string errorDate = string.Empty;
                    string errorDescription = string.Empty;
                    switch (LanguageBusiness.GetCurrentLanguage())
                    {
                        case "fa-IR":
                            errorDescription = "اشکال در انتقال تردد " + BasicTrafficsListItem.Person.Name + " به شماره پرسنلی " + BasicTrafficsListItem.Person.BarCode + " در تاریخ " + BasicTrafficsListItem.Date + " و ساعت " + Utility.IntTimeToRealTime(BasicTrafficsListItem.Time) + "";
                            break;
                        case "en-US":
                            errorDescription = "Problem In Traffic Transfer " + BasicTrafficsListItem.Person.Name + " with Personnel Code " + BasicTrafficsListItem.Person.BarCode + " in Date " + BasicTrafficsListItem.Date + " and Time " + Utility.IntTimeToRealTime(BasicTrafficsListItem.Time) + "";
                            break;
                    }
                    switch (LanguageBusiness.GetCurrentSysLanguage())
                    {
                        case "fa-IR":
                            errorDate = Utility.ToPersianDate(BasicTrafficsListItem.Date);
                        break;
                        case "en-US":
                            errorDate = Utility.ToString(BasicTrafficsListItem.Date);
                           break;
                    }

                    switch (TTT)
                    {
                        case TrafficTransferType.Backward:
                            BasicTrafficsList = BasicTrafficsList.OrderBy(x => x.DateTime).ToList<BasicTraffic>();
                            TargetTime = BasicTrafficsListItem.Time - TransferTime;
                            using (this.NHSession.BeginTransaction())
                            {
                                try
                                {
                                    if (TargetTime >= 0)
                                    {
                                        BasicTrafficsListItem.Time = TargetTime;
                                        BasicTrafficsListItem.Date = BasicTrafficsListItem.Date.AddDays(-TransferDay);
                                        BasicTrafficsListItem.Transferred = true;
                                        objectRep.WithoutTransactUpdate(BasicTrafficsListItem);
                                        UpdateCFP(BasicTrafficsListItem, UIActionType.EDIT);
                                    }
                                    else
                                    {
                                        this.DeleteTraffic(operatorPersonID, BasicTrafficsListItem.ID);
                                        this.InsertTraffic(operatorPersonID, BasicTrafficsListItem.Person.ID, BasicTrafficsListItem.Precard.ID, BasicTrafficsListItem.Date.AddDays(-(TransferDay + 1)), BasicTrafficsListItem.Time + (24 * 60 - TransferTime), "TrraficTimeTransfer");
                                    }
                                    NHibernateSessionManager.Instance.CommitTransactionOn();
                                }
                                catch (Exception ex)
                                {
                                    LogException(new Exception(errorDescription + " " + ex.Message), "BTraffic", "TransferDayTrafficsByConditions");
                                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                                    SessionHelper.SaveSessionValue(SessionHelper.TrafficsTransferErrorOccuredCountSessionName, trafficErrorOccuredCounter++);
                                }
                            } 
                            break;
                        case TrafficTransferType.Forward:
                            BasicTrafficsList = BasicTrafficsList.OrderByDescending(x => x.DateTime).ToList<BasicTraffic>();
                            TargetTime = BasicTrafficsListItem.Time + TransferTime;
                            using (this.NHSession.BeginTransaction())
                            {
                                try
                                {
                                    if (TargetTime < 24 * 60)
                                    {
                                        BasicTrafficsListItem.Time = TargetTime;
                                        BasicTrafficsListItem.Date = BasicTrafficsListItem.Date.AddDays(TransferDay);
                                        BasicTrafficsListItem.Transferred = true;
                                        objectRep.WithoutTransactUpdate(BasicTrafficsListItem);
                                        UpdateCFP(BasicTrafficsListItem, UIActionType.EDIT);
                                        
                                    }
                                    else
                                    {
                                        this.DeleteTraffic(operatorPersonID, BasicTrafficsListItem.ID);
                                        this.InsertTraffic(operatorPersonID, BasicTrafficsListItem.Person.ID, BasicTrafficsListItem.Precard.ID, BasicTrafficsListItem.Date.AddDays(TransferDay + 1), (BasicTrafficsListItem.Time + TransferTime) - 24 * 60, "TrraficTimeTransfer");
                                    }
                                    NHibernateSessionManager.Instance.CommitTransactionOn();
                                }
                                catch (Exception ex)
                                {
                                    LogException(new Exception(errorDescription + " " + ex.Message), "BTraffic", "TransferDayTrafficsByConditions");
                                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                                    SessionHelper.SaveSessionValue(SessionHelper.TrafficsTransferErrorOccuredCountSessionName, trafficErrorOccuredCounter++);
                                }
                            } 
                            break;
                    }
                    SessionHelper.SaveSessionValue(SessionHelper.TrafficsTransferCompletedCountSessionName, trafficCounter++);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "TransferDayTrafficsByConditions");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetTrafficTranferCount()
        {
            Dictionary<string, int> TranferTrafficCountDic = new Dictionary<string, int>();
            TranferTrafficCountDic[SessionHelper.TrafficsCountSessionName] = trafficCount;
            TranferTrafficCountDic[SessionHelper.TrafficsTransferCompletedCountSessionName] = trafficCounter;
            TranferTrafficCountDic[SessionHelper.TrafficsTransferErrorOccuredCountSessionName] = trafficErrorOccuredCounter;
            return TranferTrafficCountDic;
        }

        /// <summary>
        /// تعداد ترددهای انتقالی را صفر می کند
        /// </summary>
        public void ClearTrafficTransferCounts()
        {
            trafficCount = 0;
            trafficCounter = 0;
            trafficErrorOccuredCounter = 0;
        }

        /// <summary>
        /// تردد را در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="operatorPersonID">کلید اصلی اپراتور</param>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="precardId">کلید اصلی پیش کارت</param>
        /// <param name="date">تاریخ</param>
        /// <param name="time">ساعت</param>
        /// <param name="description">شرح</param>
        /// <returns>کلید اصلی تردد</returns>
        private decimal InsertTraffic(decimal operatorPersonID, decimal personId, decimal precardId, DateTime date, int time, string description)
        {
            try
            {
                BasicTraffic basicTraffic = new BasicTraffic();
                basicTraffic.Person = new Person() { ID = personId };
                basicTraffic.Precard = new Precard() { ID = precardId };
                basicTraffic.OperatorPerson = new Person() { ID = operatorPersonID };
                if (time == 0)
                    time++;
                basicTraffic.Time = time;
                basicTraffic.Description = description;
                basicTraffic.Date = date;
                basicTraffic.Active = true;
                basicTraffic.Used = false;
                basicTraffic.Manual = true;
                basicTraffic.Transferred = true;
                objectRep.WithoutTransactSave(basicTraffic);
                UpdateCFP(basicTraffic, UIActionType.EDIT);
                return basicTraffic.ID;

            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "InsertTraffic");
                return -1;
            }
        }
        
        /// <summary>
        /// ذخیره تردد در دیتابیس به همراه به روز رسانی نشانگر محاسبات طی یک تراکنش واحد
        /// </summary>
        /// <param name="basicTraffic"></param>
        /// <returns></returns>
        public decimal InsertTrafficTransactionUpdateCFP(BasicTraffic basicTraffic)
        {
            decimal basicTrafficID = 0;
            try
            {
                using (NHibernateSessionManager.Instance.BeginTransactionOn())
                {

                    objectRep.WithoutTransactSave(basicTraffic);

                    if (basicTraffic.ID > 0)
                    {
                        BasicTraffic basicTrafficObjForUpdateCfp = (BasicTraffic)basicTraffic.Clone();
                        DateTime cfpDate = basicTrafficObjForUpdateCfp.Date.AddDays(-1);
                        basicTrafficObjForUpdateCfp.Date = cfpDate;
                        UpdateCFP(basicTrafficObjForUpdateCfp, UIActionType.EDIT);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();

                }
                basicTrafficID = basicTraffic.ID;
            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                LogException(ex, "BTraffic", "InsertTrafficTransactionUpdateCFP");
                //basicTrafficID = -1;
                throw ex;
            }

            return basicTrafficID;
        }
        
        #region BaseBusiness Implementation

        /// <summary>
        /// اعتبارسنجی عملیات درج تردد
        /// </summary>
        /// <param name="obj">تردد</param>
        protected override void InsertValidate(BasicTraffic basicTraffic)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (basicTraffic.Person == null || basicTraffic.Person.ID == 0)
            {
                exception.Add(ExceptionResourceKeys.TrafficPersonRequierd, "برای تردد پرسنل مشخص نشده است", ExceptionSrc);
            }

            if (basicTraffic.Date < Utility.GTSMinStandardDateTime)
            {
                exception.Add(ExceptionResourceKeys.TrafficDateRequierd, "تاریخ تردد وارد نشده است", ExceptionSrc);
            }
            if (basicTraffic.Time <= 0)
            {
                exception.Add(ExceptionResourceKeys.TrafficTimeRequierd, "زمان تردد وارد نشده است", ExceptionSrc);
            }
            if (basicTraffic.Precard == null || basicTraffic.Precard.ID == 0)
            {
                exception.Add(ExceptionResourceKeys.TrafficPrecardRequierd, "نوع تردد وارد نشده است", ExceptionSrc);
            }
            if (exception.Count == 0)
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Active), true),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Person), new Person() { ID = basicTraffic.Person.ID }),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Time), basicTraffic.Time),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Date), basicTraffic.Date),
                                            new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Precard), new Precard() { ID = basicTraffic.PrecardID })) > 0)
                {
                    exception.Add(ExceptionResourceKeys.TrafficIsRepeated, "تردد تکراری است", ExceptionSrc);
                }
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        
        /// <summary>
        /// بررسی موجود بودن یک تردد
        /// </summary>
        /// <param name="basicTraffic">تردد</param>
        /// <returns>وضعیت موجود بودن</returns>
        public bool TrafficIsExist(BasicTraffic basicTraffic)
        {
            try
            {
                if (objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Active), true),
                                           new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Person), new Person() { ID = basicTraffic.Person.ID }),
                                           new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Time), basicTraffic.Time),
                                           new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Date), basicTraffic.Date),
                                           new CriteriaStruct(Utility.GetPropertyName(() => basicTraffic.Precard), new Precard() { ID = basicTraffic.PrecardID })) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "TrafficIsExist");
                throw ex;
            }
        }
        
        /// <summary>
        /// تردد های یک پرسنل در یک روز را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="precardId">کلید اصلی پیش کارت</param>
        /// <param name="date">تاریخ</param>
        /// <param name="time">ساعت</param>
        /// <returns>لیست ترددها</returns>
        public IList<BasicTraffic> GetTraffic(decimal personId, decimal precardId, DateTime date, int time)
        {
            try
            {
                BasicTraffic basicTrafficAlias = null;
                IList<BasicTraffic> basicList = NHibernateSessionManager.Instance.GetSession().
                                      QueryOver<BasicTraffic>(() => basicTrafficAlias)
                                      .Where(() => basicTrafficAlias.Person.ID == personId)
                                      .And(() => basicTrafficAlias.Precard.ID == precardId)
                                      .And(() => basicTrafficAlias.Time == time)
                                      .And(() => basicTrafficAlias.Date == date).List<BasicTraffic>();
                return basicList;


            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "TrafficIsExist");
                throw ex;
            }
        }
       
        /// <summary>
        /// تردد های لیست پرسنل در یک بازه زمانی مشخص را بر می گرداند 
        /// </summary>
        /// <param name="personListId">لیست کلید اصلی پرسنلی</param>
        /// <param name="precardListId">لیست کلید اصلی پیش کارت</param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <returns>لیست ترددها</returns>
        public IList<BasicTraffic> GetTrafficPersons(IList<decimal> personListId, IList<decimal> precardListId, DateTime fromDate,DateTime toDate)
        {
            try
            {
                
                BasicTraffic basicTrafficAlias = null;
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                Person personAlias = null;
                string operationGUID = this.bTemp.InsertTempList(personListId);
                IList<BasicTraffic> basicList = NHibernateSessionManager.Instance.GetSession().
                                      QueryOver<BasicTraffic>(() => basicTrafficAlias)
                                      .JoinAlias(() => basicTrafficAlias.Person,() => personAlias)
                                      .JoinAlias(() => personAlias.TempList, () => tempAlias)
                                      .Where(() => tempAlias.OperationGUID == operationGUID)
                                      .And(() => basicTrafficAlias.Precard.ID.IsIn(precardListId.ToArray()))
                                      .And(() => basicTrafficAlias.Date >= fromDate)
                                      .And(() => basicTrafficAlias.Date <= toDate)
                                      .And(() => basicTrafficAlias.Active).List<BasicTraffic>();

                this.bTemp.DeleteTempList(operationGUID);
                return basicList;


            }
            catch (Exception ex)
            {
                LogException(ex, "BTraffic", "TrafficIsExist");
                throw ex;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش تردد
        /// </summary>
        /// <param name="obj">تردد</param>
        protected override void UpdateValidate(BasicTraffic basicTraffic)
        {
            //throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نمیباشد", ExceptionSrc);
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف تردد
        /// </summary>
        /// <param name="obj">تردد</param>
        protected override void DeleteValidate(BasicTraffic obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            throw new NotImplementedException();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی قوانین واسط کاربری
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        protected override void UIValidate(BasicTraffic obj, UIActionType action)
        {
            UIValidator.DoValidate(obj);
        }

        /// <summary>
        /// نشانگر محاسبات را بروزرسانی می کند
        /// </summary>
        /// <param name="obj">تردد</param>
        /// <param name="action">نوع عملیات</param>
        protected override void UpdateCFP(BasicTraffic obj, UIActionType action)
        {
            base.UpdateCFP(obj.Person.ID, obj.Date);
        }
        #endregion

        /// <summary>
        /// کلیه تردد های پردازش شده یک پرسنل را در یک محدوده زمانی بر می گرداند
        /// </summary>
        /// <param name="prsId"></param>
        /// <param name="fromDate">تاریخ شروع</param>
        /// <param name="toDate">تاریخ پایان</param>
        /// <returns>لیست پروکسی ترددهای پردازش شده</returns>
        public IList<ProceedTrafficProxy> GetAllTrafic(decimal prsId, DateTime fromDate, DateTime toDate)
        {
            IList<ProceedTrafficProxy> list3;
            try
            {
                EntityRepository<ProceedTraffic> repository = new EntityRepository<ProceedTraffic>();
                List<ProceedTrafficProxy> list = new List<ProceedTrafficProxy>();
                CriteriaStruct[] structArray = new CriteriaStruct[3];
                Person person = new Person();
                person.ID = prsId;

                IList<ProceedTraffic> byCriteria = repository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().Person), new Person() { ID = prsId }),
                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().FromDate), fromDate, CriteriaOperation.GreaterEqThan),
                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new ProceedTraffic().FromDate), toDate, CriteriaOperation.LessEqThan));
                foreach (ProceedTraffic traffic in byCriteria)
                {
                    for (int i = 0; i < traffic.Pairs.Count; i++)
                    {
                        ProceedTrafficProxy item = new ProceedTrafficProxy();
                        item.From = Utility.IntTimeToRealTime(traffic.Pairs[i].From);
                        item.To = Utility.IntTimeToRealTime(traffic.Pairs[i].To);
                        item.Pishcard = traffic.Pairs[i].Precard.Name;
                        item.Date = Utility.ToString(traffic.FromDate);
                        list.Add(item);
                    }
                }
                list3 = list;
            }
            catch (Exception exception)
            {
                BaseBusiness<BasicTraffic>.LogException(exception);
                throw exception;
            }
            return list3;
        }

        /// <summary>
        /// بررسی دسترسی به کنترل تردد ها
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckTrafficsControlLoadAccess()
        {
        }

        /// <summary>
        /// بررسی دسترسی به تردد های آنلاین
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckOnlineTrafficsLoadAccess()
        {
        }
         
    }
}
