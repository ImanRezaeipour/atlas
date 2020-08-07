using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.NHibernateFramework;
using NHibernate;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using System.Web.Configuration;
using GTS.Clock.Business.Temp;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Business.AppSettings;
using System.Globalization;

namespace GTS.Clock.Business.Shifts
{
    public class BYearlyHolidayWorkGroups : BaseBusiness<YearlyHolidayWorkGroups>
    {
        readonly EntityRepository<YearlyHolidayWorkGroups> _cnpRep = new EntityRepository<YearlyHolidayWorkGroups>();
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.BYearlyHolidayWorkGroups";
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        public IList<WorkGroup> GetALLWorkGroup()
        {
            try
            {
                IList<WorkGroup> WorkGroupList = new List<WorkGroup>();
                WorkGroupList = NHSession.QueryOver<WorkGroup>().List<WorkGroup>();
                return WorkGroupList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BYearlyHoLidayWorkGroups", "GetWorkGroup");
                throw ex;
            }
        }

        public IList<YearlyHolidayWorkGroups> GetHolidayWorkGroupList(decimal holidayId, int year)
        {
            try
            {
                IList<YearlyHolidayWorkGroups> HolidayWorkGroupList = NHSession.QueryOver<YearlyHolidayWorkGroups>()
                                                                               .Where(x => x.calendarType.ID == holidayId && x.Year == year)
                                                                               .List();
                return HolidayWorkGroupList;
            }
            catch (Exception ex)
            {
                LogException(ex, "ByearlyHolidayWorkGroups", "GetHolidayWorkGroupList");
                throw ex;
            }
        }

        public IList<WorkGroupProxy> GetWorkGroup(decimal holidayId, int year)
        {
            try
            {
                IList<WorkGroupProxy> Result = new List<WorkGroupProxy>();
                IList<WorkGroup> ListWorkGroup = GetALLWorkGroup();
                foreach (WorkGroup Workgroup in ListWorkGroup)
                {
                    Result.Add(new WorkGroupProxy() { WorkGroupCode = Workgroup.CustomCode, WorkGroupName = Workgroup.Name, ID = Workgroup.ID });
                }

                IList<YearlyHolidayWorkGroups> HolidayWorkGroupList = GetHolidayWorkGroupList(holidayId, year);

                foreach (WorkGroupProxy result in Result)
                {
                    foreach (YearlyHolidayWorkGroups holidayworkgrouplist in HolidayWorkGroupList)
                    {
                        if (holidayworkgrouplist.workGroup.ID == result.ID)
                        {
                            result.IsUsedByYearlyHoliday = true;
                            break;
                        }

                        else
                            result.IsUsedByYearlyHoliday = false;
                    }
                }
                return Result;
            }

            catch (Exception ex)
            {
                LogException(ex, "ByearlyHolidayWorkGroups", "GetWorkGroup");
                throw ex;
            }
        }
        public void DeleteYearlyHolidayWorkGroups(decimal selectedHolidayID, int year)
        {
            try
            {
                IList<YearlyHolidayWorkGroups> HolidayWorkGroupList = GetHolidayWorkGroupList(selectedHolidayID, year);
                foreach (YearlyHolidayWorkGroups holidayworkgroupitem in HolidayWorkGroupList)
                {
                    this.SaveChanges(holidayworkgroupitem, UIActionType.DELETE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        // این متد علاوه بر اینکه گروه های کاری اختصاص داده شده به تعطیلات را در پایگاه ذخیره میکند ,زمانی که به یک گروه کاری  در یک سال خاص در گذشته شیفت اختصاص داده شده باشد ,  انگاه اگر گروه کاری مربوطه  به تعطیلات اختصاص داده شود ,تعطیلات رااز شیفتهای اختصاص داده شده به گروه کاری مربوطه را بیرون میکشد.
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateYearlyHolidayWorkGroups(decimal selectedHolidayID, List<decimal> WorkGroupIdList, int year)
        {
            BWorkGroupCalendar WorkGroupCalendar = new BWorkGroupCalendar();
            IList<decimal> HolidayList = new List<decimal>();
            HolidayList.Add(selectedHolidayID);
            IList<CalendarCellInfo> CalendarCellInfoList = new List<CalendarCellInfo>();
           // BWorkGroupCalendar WorkGroupCalendarBusiness = new BWorkGroupCalendar();

            try
            {
                //if (WorkGroupIdList.Count > 0)
                //{
                    using (NHibernateSessionManager.Instance.BeginTransactionOn())
                    {
                        this.DeleteYearlyHolidayWorkGroups(selectedHolidayID, year);
                        foreach (decimal workgroupIdItem in WorkGroupIdList)
                        {
                            YearlyHolidayWorkGroups yearlyHolidayWorkGroups = new YearlyHolidayWorkGroups() { calendarType = new CalendarType() { ID = selectedHolidayID }, workGroup = new WorkGroup() { ID = workgroupIdItem }, Year = year };
                            this.SaveChanges(yearlyHolidayWorkGroups, UIActionType.ADD);
                            CalendarCellInfoList = WorkGroupCalendar.GetAll(workgroupIdItem, year);
                            if (CalendarCellInfoList.Count > 0)
                            {
                                int lastDayofEndMonth = 0;
                                switch (BLanguage.CurrentSystemLanguage)
                                {
                                    case LanguagesName.Parsi:
                                        PersianCalendar pCal = new PersianCalendar();
                                        lastDayofEndMonth = 29;
                                        if (pCal.IsLeapYear(year))
                                            lastDayofEndMonth = 30;
                                        break;
                                    case LanguagesName.English:
                                        lastDayofEndMonth = 31;
                                        break;
                                }
                                IList<CalendarCellInfo> CalendarCellInfoWithOutHoliday = WorkGroupCalendar.RepetitionPeriod(year, 1, 1, 12, lastDayofEndMonth, HolidayList, CalendarCellInfoList);
                                 WorkGroupCalendar.SaveChanges(CalendarCellInfoWithOutHoliday, workgroupIdItem, year);
                            }
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                //}
            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                throw ex;
            }
        }
        public IList<YearlyHolidayWorkGroups> GetYearlyHolidayList(decimal workgroupId, int year)
        {

            IList<YearlyHolidayWorkGroups> YearlyHolidayList = NHSession.QueryOver<YearlyHolidayWorkGroups>()
                                                                        .Where(x => x.workGroup.ID == workgroupId && x.Year == year)
                                                                        .List();
            return YearlyHolidayList;
        }
        

        protected override void OnSaveChangesSuccess(YearlyHolidayWorkGroups obj, UIActionType action)
        {
            base.OnSaveChangesSuccess(obj, action);
        }
        protected override void InsertValidate(YearlyHolidayWorkGroups clCar)
        {
        }

        protected override void UpdateValidate(YearlyHolidayWorkGroups clCar)
        {
        }

        protected override void DeleteValidate(YearlyHolidayWorkGroups clCar)
        {
        }
    }
}
