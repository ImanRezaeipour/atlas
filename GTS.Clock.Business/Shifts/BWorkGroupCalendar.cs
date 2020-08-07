using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Model;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Security;
using System.Globalization;

namespace GTS.Clock.Business.Shifts
{
    public class BWorkGroupCalendar : BaseBusiness<WorkGroupDetail>
    {
        UIValidationGroupingRepository uivalidationGroupingRepository = new UIValidationGroupingRepository();
        const string ExceptionSrc = "GTS.Clock.Business.Shifts.BWorkGroupCalendar";
        private SysLanguageResource sysLangruage;
        private EntityRepository<WorkGroupDetail> wGDRepository
            = new EntityRepository<WorkGroupDetail>(false);
        
        public BYearlyHolidayWorkGroups YearlyHolidayWorkGroupBusiness
        {
           get
            {
                return new BYearlyHolidayWorkGroups();
            }
        }
        public BWorkGroupCalendar(SysLanguageResource sysCulture)
        {
            this.sysLangruage = sysCulture;
        }
      
        public BWorkGroupCalendar() 
        {
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLangruage = SysLanguageResource.Parsi;
            }
            else 
            {
                this.sysLangruage = SysLanguageResource.English;
            }
        }

        /// <summary>
        /// باتوجه به سال و گروه کاری مشخص شده لیستی از اطلاعات جزیات انتساب شیفت به گروه کاری را برمیگرداند
        /// </summary>
        /// <param name="workGroupId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IList<CalendarCellInfo> GetAll(decimal workGroupId, int year)
        {
            try
            {
                DateTime minDate = new DateTime();
                DateTime maxDate = new DateTime();

                if (sysLangruage == SysLanguageResource.Parsi)
                {
                    minDate = Utility.ToMildiDate(String.Format("{0}/01/01", year));
                    maxDate = Utility.ToMildiDate(String.Format("{0}/01/01", year + 1));
                }
                else if (sysLangruage == SysLanguageResource.English)
                {
                    minDate = new DateTime(year, 1, 1);
                    maxDate = new DateTime(year + 1, 1, 1);
                }

                IList<CalendarCellInfo> result = new List<CalendarCellInfo>();

                IList<WorkGroupDetail> list =
                wGDRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroupDetail().WorkGroup), new WorkGroup() { ID = workGroupId }),
                                            new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroupDetail().Date), minDate, CriteriaOperation.GreaterEqThan),
                                            new CriteriaStruct(Utility.GetPropertyName(() => new WorkGroupDetail().Date), maxDate, CriteriaOperation.LessThan));

                list = list.OrderBy(x => x.Date).ToList();
                foreach (WorkGroupDetail wgd in list)
                {
                    CalendarCellInfo cellInfo = new CalendarCellInfo();

                    if (sysLangruage == SysLanguageResource.Parsi)
                    {
                        PersianDateTime persianDateTime = new PersianDateTime(wgd.Date);
                        cellInfo.Day = persianDateTime.Day;
                        cellInfo.Month = persianDateTime.Month;
                        cellInfo.ShiftID = wgd.Shift.ID;
                    }
                    else if (sysLangruage == SysLanguageResource.English)
                    {
                        cellInfo.Day = wgd.Date.Day;
                        cellInfo.Month = wgd.Date.Month;
                    }
                    cellInfo.Title = wgd.Shift.Name;
                    cellInfo.Color = wgd.Shift.Color;

                    result.Add(cellInfo);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BWorkGroupCalendar", "GetAll");
                throw ex;
            }
        }

        /// <summary>
        /// لیست انواع تعطیلات را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<CalendarType> GetAllHolidayTypes()
        {
            return new BCalendarType().GetAll();
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int SaveChanges(IList<CalendarCellInfo> cellInfoList, decimal workGroupId, int year)
        {
            try
            {
                WorkGroupRepository workGroupRep = new WorkGroupRepository(false);
                DateTime minDate = new DateTime();
                DateTime maxDate = new DateTime();

                if (sysLangruage == SysLanguageResource.Parsi)
                {
                    minDate = Utility.ToMildiDate(String.Format("{0}/01/01", year));
                    maxDate = Utility.ToMildiDate(String.Format("{0}/01/01", year + 1));
                }
                else if (sysLangruage == SysLanguageResource.English)
                {
                    minDate = new DateTime(year, 1, 1);
                    maxDate = new DateTime(year + 1, 1, 1);
                }

                int count = cellInfoList.GroupBy(x => x.Key)
                    .Where(g => g.Count() > 1).Count();

                if (count > 0)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.WorkGroupCalendarDublicateDate, "برای روز دوبار شیفت انتساب داده شده است", ExceptionSrc);
                    throw exception;
                }

                workGroupRep.DeleteWorkGroupDetail(workGroupId, minDate, maxDate.AddDays(-1));
                IList<WorkGroupDetail> detailList = new List<WorkGroupDetail>();

                foreach (CalendarCellInfo cellInfo in cellInfoList)
                {
                    if (cellInfo.ShiftID != 0)
                    {
                        WorkGroupDetail detail = new WorkGroupDetail();
                        DateTime date = new DateTime();
                        if (sysLangruage == SysLanguageResource.Parsi)
                        {
                            date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, cellInfo.Month, cellInfo.Day));
                        }
                        else if (sysLangruage == SysLanguageResource.English)
                        {
                            date = new DateTime(year, cellInfo.Month, cellInfo.Day);
                        }
                        detail.WorkGroup = new WorkGroup() { ID = workGroupId };
                        detail.Date = date;
                        detail.Shift = new Shift() { ID = cellInfo.ShiftID };
                        base.SaveChanges(detail, UIActionType.ADD);
                    }
                }
                this.UpdateCFPByWorkGroupId(workGroupId, minDate);
                return cellInfoList.Count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BWorkGroupCalendar", "SaveChanges");
                throw ex;
            }
        }

        /// <summary>
        /// تکرار دوره
        /// </summary>
        /// <param name="year"></param>
        /// <param name="startMonth"></param>
        /// <param name="startDay"></param>
        /// <param name="endMonth"></param>
        /// <param name="endDay"></param>
        /// <param name="holidayTypes"></param>
        /// <param name="cellInfoList"></param>
        public IList<CalendarCellInfo> RepetitionPeriod(int year, int startMonth, int startDay, int endMonth, int endDay, IList<decimal> holidayTypes, IList<CalendarCellInfo> cellInfoList)
        {
            try
            {
                DateTime startDate, endDate, endOfYear;
                PersianCalendar YearKabise = new PersianCalendar();
               
                if (sysLangruage == SysLanguageResource.Parsi)
                {                    
                    startDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, startMonth, startDay));
                    endDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, endMonth, endDay));
                    endOfYear = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, 12, Utility.GetEndOfPersianMonth(year, 12)));
                 
                }
                else
                {
                    startDate = new DateTime(year, startMonth, startDay);
                    endDate = new DateTime(year, endMonth, endDay);
                    endOfYear = new DateTime(year, 12, DateTime.DaysInMonth(year, 12));
                }
                if (startDate > endDate)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.WorkGroupCalendarPriodDateIsNotValid, "تاریخ ابتدا از انتها بزرگتر است", ExceptionSrc);
                    throw exception;
                }
                IList<CalendarCellInfo> list = new List<CalendarCellInfo>();
                IList<CalendarCellInfo> cells = new List<CalendarCellInfo>();
                IList<CalendarCellInfo> result = new List<CalendarCellInfo>();
                IList<Shift> shiftList = new BShift().GetAll();
                foreach (Shift shiftItem in shiftList)
                {
                    NHibernateSessionManager.Instance.GetSession().Evict(shiftItem);
                }
                //looking for item in parameter list by date
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    int day = 0, month = 0;
                    if (sysLangruage == SysLanguageResource.Parsi)
                    {
                        day = new PersianDateTime(date).Day;
                        month = new PersianDateTime(date).Month;
                    }
                    else
                    {
                        day = date.Day;
                        month = date.Month;
                    }
                    CalendarCellInfo info = cellInfoList.Where(x => x.Day == day && x.Month == month).FirstOrDefault();
                    if (info != null)
                    {
                        list.Add(info);
                    }
                    else
                    {
                        list.Add(null);
                    }
                }
                //|| list.Where(x => x != null).Count() == 0
                if (list.Count == 0)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.WorkGroupCalendarPriodIsEmpty, "در بازه زمانی مشخص شده شیفتی انتساب داده نشده است", ExceptionSrc);
                    throw exception;
                }

                for (DateTime index = startDate; index <= endOfYear; index = index.AddDays(list.Count))
                {
                    for (int i = 0; i < list.Count && index.AddDays(i) <= endOfYear; i++)
                    {
                        if (list[i] != null)
                        {

                            CalendarCellInfo info = new CalendarCellInfo();
                            info.ShiftID = list[i].ShiftID;
                            Shift shift = shiftList.Where(x => x.ID == list[i].ShiftID).FirstOrDefault();
                            info.Color = shift != null ? shift.Color : "";
                            info.Title = list[i].Title;
                            if (sysLangruage == SysLanguageResource.Parsi)
                            {
                                info.Day = new PersianDateTime(index.AddDays(i)).Day;
                                info.Month = new PersianDateTime(index.AddDays(i)).Month;
                            }
                            else
                            {
                                info.Day = index.AddDays(i).Day;
                                info.Month = index.AddDays(i).Month;
                            }
                            cells.Add(info);
                        }
                    }
                }
                //apply holidays
                List<CalendarCellInfo> holidays = new List<CalendarCellInfo>();
                if (holidayTypes != null)
                {
                    foreach (decimal id in holidayTypes)
                    {
                        holidays.AddRange(new BCalendarType().GetCalendarList(year, id));
                    }
                }
                foreach (CalendarCellInfo cell in cells)
                {
                    if (holidays.Where(x => x.Day == cell.Day && x.Month == cell.Month).Count() == 0)
                    {
                        result.Add(cell);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BWorkGroupCalendar", "RepetitionPeriod");
                throw ex;
            }
        }

        /// <summary>
        /// همه شیفتها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Shift> GetAllShifts()
        {
            try
            {
                BShift bshift = new BShift();
                IList<Shift> shifts = bshift.GetAll();
                return shifts;
            }
            catch (Exception ex)
            {
                LogException(ex, "BWorkGroupCalendar", "GetAllShifts");
                throw ex;
            }
        }

        protected override void InsertValidate(WorkGroupDetail obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            this.CheckInterfaceRuleGroup(obj.WorkGroup.ID);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(WorkGroupDetail obj)
        {
            throw new IllegalServiceAccess("بروزرسانی برای تقویم تعریف نشده است زیرا همیشه ابتدا حذف و سپس درج صورت میگیرد", ExceptionSrc);
        }

        protected override void DeleteValidate(WorkGroupDetail obj)
        {
            throw new IllegalServiceAccess("بروزرسانی برای تقویم تعریف نشده است زیرا همیشه ابتدا حذف و سپس درج صورت میگیرد", ExceptionSrc);
        }

        public void CheckInterfaceRuleGroup(decimal workGroupId)
        {

            IList<decimal> UiVakidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdListByWorkGroup(workGroupId);
            foreach (decimal uivalidationId in UiVakidationGroupIdList)
            {
                base.UIValidator.GetCalculationLockDateByGroup(uivalidationId);
            }
            // WorkGroupRepository workGroupRep = new WorkGroupRepository(false);
            //UIValidationGroupingRepository.WorkGroup wg = workGroupRep.GetById(workGroupId, false);
            //foreach (AssignWorkGroup assign in wg.AssignWorkGroupList)
            //{
            //    if (assign.Person.PersonTASpec.UIValidationGroup != null)
            //    {
            //        base.UIValidator.GetCalculationLockDateByGroup(assign.Person.PersonTASpec.UIValidationGroup.ID);
            //    }
            //}

        }
        public void UpdateCFPByWorkGroupId(decimal workGroupId, DateTime minChangeDate)
        {
            IList<CFP> cfpList = new List<CFP>();
            Dictionary<decimal, DateTime> uivalidationGroupIdDic = new Dictionary<decimal, DateTime>();
            WorkGroupRepository workGroupRep = new WorkGroupRepository(false);
            WorkGroup wg = workGroupRep.GetById(workGroupId, false);
            IList<AssignWorkGroup> assignWorkGroupList = wg.AssignWorkGroupList;
             IList<CFP> cfpPersonList = new List<CFP>();
            if(assignWorkGroupList.Count > 0)
                cfpPersonList = base.GetCFPPersons(assignWorkGroupList.Select(a => a.Person.ID).ToList<decimal>());
            IList<decimal> UiValidationGroupIdList = uivalidationGroupingRepository.GetUivalidationIdListByWorkGroup(workGroupId);
            IList<decimal> cfpPersonIdInsertList = new List<decimal>();

            foreach (decimal uiValidateionGrpId in UiValidationGroupIdList)
            {
                
                if (!uivalidationGroupIdDic.ContainsKey(uiValidateionGrpId))
                {
                    DateTime calculationLockDate = base.UIValidator.GetCalculationLockDateByGroup(uiValidateionGrpId);
                    uivalidationGroupIdDic.Add(uiValidateionGrpId, calculationLockDate);
                }
                
            }
                                   
            base.UpdateCfpByWrokGroup(wg.ID , uivalidationGroupIdDic);
            cfpPersonIdInsertList = assignWorkGroupList.Where(p => cfpPersonList !=null && !cfpPersonList.Select(c => c.PrsId).ToList().Contains(p.Person.ID)).Select(p => p.Person.ID).Distinct().ToList<decimal>();
            if (cfpPersonIdInsertList.Count > 0)
                base.InsertCfpByWrokGroup(cfpPersonIdInsertList, wg.ID ,uivalidationGroupIdDic);
        }
        
       // زمانی که در فرم تقویم به یک گروه کاری خاص در یک سال مشخس داریم شیفت احتصاص میدهیم چک میشه که ایا  برای  این گروه کاری تعطیلاتی در نظر گرفته شده یا نه, در صورتی که تعطیلات اختصاص داده شده باشد ,تعطیلات را از شیفتهای اختصاص داده شده به گروه کاری مربوطه جدا میکند

        public bool UpdateCalendar(IList<CalendarCellInfo> calendarcellinfolist, decimal groupID, int year)
        {
            bool IsUseByHoliday = false;
            IList<YearlyHolidayWorkGroups> YearlyHolidayList = YearlyHolidayWorkGroupBusiness.GetYearlyHolidayList(groupID, year);
            if(YearlyHolidayList.Count > 0)
                IsUseByHoliday = true;
            IList<decimal> HolidayList = new List<decimal>();
            foreach (YearlyHolidayWorkGroups YearlyHolidayItem in YearlyHolidayList)
            {
                HolidayList.Add(YearlyHolidayItem.calendarType.ID);
            }
           
            int LastDayofEndMonth = 0;
            switch (BLanguage.CurrentSystemLanguage)
            {
                case LanguagesName.Parsi :
                   PersianCalendar pCal = new PersianCalendar();
                    LastDayofEndMonth = 29;
                    if (pCal.IsLeapYear(year))
                    {
                        LastDayofEndMonth = 30;
                    }
                    break;
                case LanguagesName.English :
                    LastDayofEndMonth = 31;
                    break;
            }

            IList<CalendarCellInfo> CalendarCellInfoWithOutHoliday = this.RepetitionPeriod(year, 1, 1, 12, LastDayofEndMonth, HolidayList, calendarcellinfolist);
            this.SaveChanges(CalendarCellInfoWithOutHoliday, groupID, year);
            return IsUseByHoliday;
        }
        
    }
}
