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
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using NHibernate;
using GTS.Clock.Model.Concepts;


namespace GTS.Clock.Business.Shifts
{
    /// <summary>
    /// created at: 2011-12-01 11:05:15 AM
    /// write your name here
    /// </summary>
    public class BCalendarType : BaseBusiness<CalendarType>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Shifts.BCalendarType";
        private EntityRepository<CalendarType> calendarTypeRep = new EntityRepository<CalendarType>();
        private EntityRepository<HolidaysTemplate> holidaysRep = new EntityRepository<HolidaysTemplate>();
        private CalendarRepository calendarRepository = new CalendarRepository();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();

        public IList<decimal> InTestCasePersonContext { get; set; }

        /// <summary>
        /// لیست تقویم را برمیگرداند
        /// اگر نوع تقویم رسمی و آیتم های آن در دیتابیس خالی باشد آنگاه تعطیلات تقویم پیشفرض را از دیتابیس بارگزاری میکند
        /// </summary>
        /// <param name="year"></param>
        /// <param name="calendarTypeID"></param>
        /// <param name="CalendarTypeCustomId">اگر رسمی باشد تعطلات سال برگردانده میشود</param>
        /// <returns></returns>
        public IList<CalendarCellInfo> GetCalendarList(int year, decimal calendarTypeID)
        {
            DateTime yearStart, yearEnd;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                string date = String.Format("{0}/01/01", year);
                yearStart = Utility.ToMildiDate(date);
                date = String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12));
                yearEnd = Utility.ToMildiDate(date);
            }
            else
            {
                yearStart = new DateTime(year, 1, 1);
                yearEnd = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
            }

            IList<Calendar> calendarList = calendarRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Calendar().CalendarType), new CalendarType() { ID = calendarTypeID }),
                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new HolidaysTemplate().Date), yearStart, CriteriaOperation.GreaterEqThan),
                                                                            new CriteriaStruct(Utility.GetPropertyName(() => new HolidaysTemplate().Date), yearEnd, CriteriaOperation.LessEqThan));

            IList<CalendarCellInfo> resultList = new List<CalendarCellInfo>();
            CalendarType calendarType = base.GetByID(calendarTypeID);

            if (calendarList.Count == 0 && calendarType.HolidayTemplateList != null)
            {
                ///load default Holidays
                IList<HolidaysTemplate> holidays = calendarType.HolidayTemplateList;

                holidays = holidays.Where(x => x.Date >= yearStart && x.Date <= yearEnd)
                    .ToList<HolidaysTemplate>();

                foreach (HolidaysTemplate holiday in holidays)
                {
                    Calendar cal = new Calendar();
                    cal.CalendarType = new CalendarType() { ID = calendarTypeID };
                    cal.Date = holiday.Date;
                    CalendarCellInfo cell = new CalendarCellInfo(cal);
                    calendarList.Add(cal);
                }
            }
            var result = from calendar in calendarList
                         select new CalendarCellInfo(calendar);

            resultList = result.ToList<CalendarCellInfo>();
            return resultList;
        }

        public IList<Calendar> GetCalendarListByDateRange(DateTime startDate, DateTime endDate, string CustomCode)
        {
            return calendarRepository.GetCalendarListByDateRange(startDate, endDate, CustomCode);
        }


        /// <summary>
        /// درج تقویم
        /// آیتمهای قبلی حذف میشوند
        /// </summary>
        /// <param name="calendarTypeID"></param>
        /// <param name="calendars"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCalendars(decimal calendarTypeID, int year, IList<CalendarCellInfo> calendars)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    this.CheckUserInterfaceRuleGroup();
                    DateTime yearStart, yearEnd;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        string date = String.Format("{0}/01/01", year);
                        yearStart = Utility.ToMildiDate(date);
                        date = String.Format("{0}/12/{1}", year, Utility.GetEndOfPersianMonth(year, 12));
                        yearEnd = Utility.ToMildiDate(date);
                    }
                    else
                    {
                        yearStart = new DateTime(year, 1, 1);
                        yearEnd = new DateTime(year, 12, Utility.GetEndOfMiladiMonth(year, 12));
                    }
                    IList<DateTime> oldCalDateList = calendarRepository.GetAllCalendarDateByTypeId(calendarTypeID);
                    calendarRepository.DeleteCalendarsByType(calendarTypeID, yearStart, yearEnd);
                    foreach (CalendarCellInfo cell in calendars)
                    {
                        Calendar cal = cell.Export(year);
                        cal.CalendarType = new CalendarType() { ID = calendarTypeID };
                        calendarRepository.Save(cal);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    this.UpdateCFPByCalendar(calendarTypeID, year, oldCalDateList, calendars);
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BCalendarType", "InsertCalendars");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calenderType"></param>
        protected override void InsertValidate(CalendarType calenderType)
        {

            UIValidationExceptions exception = new UIValidationExceptions();
            if (Utility.IsEmpty(calenderType.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (calendarTypeRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => calenderType.Name), calenderType.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (Utility.IsEmpty(calenderType.CustomCode))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarCustomCodeRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (calendarTypeRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => calenderType.CustomCode), calenderType.CustomCode)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarCustomCodeRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        private void CheckUserInterfaceRuleGroup()
        {
            PersonRepository prsRep = new PersonRepository(false);
            IList<decimal> prsIds = prsRep.GetAllActivePersonIds();
            IList<decimal> uivalidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdList(prsIds);
            foreach (decimal uivalidationGroupId in uivalidationGroupIdList)
            {
                base.UIValidator.GetCalculationLockDateByGroup(uivalidationGroupId);
            }

            //if (InTestCasePersonContext != null && InTestCasePersonContext.Count > 0)
            //{
            //    prsIds = InTestCasePersonContext;
            //}
            //foreach (decimal prsId in prsIds)
            //{
            //    base.UIValidator.GetCalculationLockDate(prsId);
            //}

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="calenderType"></param>
        protected override void UpdateValidate(CalendarType calenderType)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            //this.CheckUserInterfaceRuleGroup();

            if (Utility.IsEmpty(calenderType.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarNameRequierd, "ویرایش - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (calendarTypeRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => calenderType.Name), calenderType.Name),
                                                        new CriteriaStruct(Utility.GetPropertyName(() => calenderType.ID), calenderType.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarNameRepeated, "ویرایش - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (Utility.IsEmpty(calenderType.CustomCode))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarCustomCodeRequierd, "ویرایش - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (calendarTypeRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => calenderType.CustomCode), calenderType.CustomCode),
                                                        new CriteriaStruct(Utility.GetPropertyName(() => calenderType.ID), calenderType.ID, CriteriaOperation.NotEqual)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarCustomCodeRepeated, "ویرایش - نام نباید تکراری باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calenderType"></param>
        protected override void DeleteValidate(CalendarType calenderType)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            EntityRepository<HolidaysTemplate> holidayRep = new EntityRepository<HolidaysTemplate>();

            int count = holidayRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new HolidaysTemplate().CalendarTypeId), calenderType.ID));
            if (count > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarTypeUsedInHolidayTemplates, "حذف - این نوع تعطیلات بدلیل دارا بودن تمپلیت قابل حذف نیست", ExceptionSrc));
            }

            CalendarType calendarObj = GetByID(calenderType.ID);
            if (calendarObj.YearlyHolidayWorkGroupsList.Count > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CalendarTypeAssignWorkGroup, "تقویم تعطیلات سالیانه به گروه های کاری انتساب داده شده است", ExceptionSrc));
            }
            NHSession.Evict(calendarObj);
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// برای تمامی پرسنل
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        protected void UpdateCFPByCalendar1(decimal calendarTypeId, int year, IList<DateTime> oldCalDateList, IList<CalendarCellInfo> calendars)
        {
            DateTime firstDate = this.GetFirstDateDiffrence(year, oldCalDateList, calendars, calendarTypeId).Date;
            PersonRepository prsRep = new PersonRepository(false);
            IList<decimal> prsIds = prsRep.GetAllActivePersonIds();

            if (InTestCasePersonContext != null && InTestCasePersonContext.Count > 0)
            {
                prsIds = InTestCasePersonContext;
            }

            DateTime newCfpDate = firstDate;
            foreach (decimal prsId in prsIds)
            {
                CFP cfp = base.GetCFP(prsId);

                if (cfp.ID == 0 || cfp.Date > newCfpDate)
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDate(prsId);

                    //بسته بودن محاسبات 
                    if (calculationLockDate > Utility.GTSMinStandardDateTime && calculationLockDate > newCfpDate)
                    {
                        newCfpDate = calculationLockDate.AddDays(1);
                    }

                    base.UpdateCFP(prsId, newCfpDate);
                }
            }
        }

        protected void UpdateCFPByCalendar(decimal calendarTypeId, int year, IList<DateTime> oldCalDateList, IList<CalendarCellInfo> calendars)
        {
            DateTime firstDate = this.GetFirstDateDiffrence(year, oldCalDateList, calendars, calendarTypeId).Date;
            PersonRepository prsRep = new PersonRepository(false);
            IList<decimal> prsIds = prsRep.GetAllActivePersonIds();
            if (InTestCasePersonContext != null && InTestCasePersonContext.Count > 0)
            {
                prsIds = InTestCasePersonContext;
            }
            //IList<CFP> cfpPersonList = base.GetCFPPersons(prsIds);
            IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdList(prsIds);
            Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
            foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
            {

                if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId))
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId);
                    uivalidationGroupIdDic.Add(uiValidateionGrpId, calculationLockDate);
                }

            }
            CFPRepository cfpRepository = new CFPRepository();
            cfpRepository.InsertAndUpdateCfpByPersons(uivalidationGroupIdDic, firstDate);

        }

        /// <summary>
        /// اولین تاریخ تغییرات
        /// </summary>
        /// <param name="year"></param>
        /// <param name="calendars"></param>
        /// <param name="calendarTypeId"></param>
        /// <returns></returns>
        private DateTime GetFirstDateDiffrence(int year, IList<DateTime> oldCalDateList, IList<CalendarCellInfo> calendars, decimal calendarTypeId)
        {
            oldCalDateList = oldCalDateList.OrderBy(x => x).ToList<DateTime>();
            var calendarList = from i in calendars
                               select i.Export(year).Date;
            IList<DateTime> newCalDateList = calendarList.OrderBy(x => x).ToList<DateTime>();

            if (oldCalDateList.Count > 0 && newCalDateList.Count > 0)
            {
                foreach (DateTime calDate in oldCalDateList)
                {
                    foreach (DateTime newCalDate in newCalDateList)
                    {
                        if (calDate != newCalDate)
                        {
                            return calDate < newCalDate ? calDate : newCalDate;
                        }
                    }
                }
            }
            else if (newCalDateList.Count > 0)
            {
                return newCalDateList.First();
            }
            else if (oldCalDateList.Count > 0)
            {
                return oldCalDateList.First();
            }
            return DateTime.Now;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckYearlyHolidaysLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertYearlyHolidaysGroup(CalendarType yearlyHolidaysGroup, UIActionType UAT)
        {
            return base.SaveChanges(yearlyHolidaysGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateYearlyHolidaysGroup(CalendarType yearlyHolidaysGroup, UIActionType UAT)
        {
            return base.SaveChanges(yearlyHolidaysGroup, UAT);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteYearlyHolidaysGroup(CalendarType yearlyHolidaysGroup, UIActionType UAT)
        {
            return base.SaveChanges(yearlyHolidaysGroup, UAT);
        }

        public IList<Calendar> GetMonthlyHoliday(DateTime startmonth, DateTime endmonth)
        {
            IList<Calendar> calendarList = null;
            calendarList = NHSession.QueryOver<Calendar>()
                                    .Where(x => x.Date >= startmonth && x.Date <= endmonth && x.CalendarType.ID == 1)
                                    .List<Calendar>();
            return calendarList;
        }


    }
}
