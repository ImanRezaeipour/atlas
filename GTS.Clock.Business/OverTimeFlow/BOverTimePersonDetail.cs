using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.ArchiveCalculations;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Temp;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.General;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.OverTimeFlow;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using GTS.Clock.Model.Concepts;

namespace GTS.Clock.Business.OverTimeFlow
{
    /// <summary>
    /// کلاس بیزینس مربوط به سرانه اضافه کار تشویقی, شب کاری تشویقی, تعطیل کار تشویقی
    /// </summary>
    public class BOverTimePersonDetail : BaseBusiness<OverTimePersonDetail>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private const string ExceptionSrc = "GTS.Clock.Business.OverTimeFlow.BOverTimePersonDetail";
        TempRepository tempRepository = new TempRepository(false);
        IDataAccess accessPort = new BUser();
        private BTemp bTemp = new BTemp();
        private BDepartment bDepartment = new BDepartment();
        private BPerson bPerson = new BPerson();
        private BWorkedTime bWorkedTime = new BWorkedTime();
        private BOverTimeDetail bOverTimeDetail = new BOverTimeDetail();
        private BApprovalAttendanceSchedule ApprovalAttendanceScheduleBusiness = new BApprovalAttendanceSchedule();
        private EntityRepository<OverTime> overTimeRepository = new EntityRepository<OverTime>();
        BOverTimePersonDetailHistory overTimePersonDetailHistoryBusiness = new BOverTimePersonDetailHistory();
        private BPersonApprovalAttendance personApprovalAttendanceBusiness = new BPersonApprovalAttendance();
        private BCalendarType bCalendar = new BCalendarType();
        ManagerRepository managerRepository = new ManagerRepository(false);
        private BArchiveCalculator archiveCalculatorBusiness = new BArchiveCalculator();
        private BApprovalAttendanceScheduleException ApprovalAttendanceScheduleExceptionBusiness = new BApprovalAttendanceScheduleException();

        private EntityRepository<OverTimePersonDetail> overTimePersonDetailRepository = new EntityRepository<OverTimePersonDetail>();
        private EntityRepository<SendPersonFunctionLog> sendPersonFunctionLogRepository = new EntityRepository<SendPersonFunctionLog>();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);

        LanguagesName sysLanguageResource;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public BOverTimePersonDetail()
        {
            


            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }
        }

        /// <summary>
        /// سرانه اضافه کار تشویقی ثبت شده در دیتابیس را برای یک شخص خاص در تاریخ مشخص برمی گرداند
        /// </summary>
        /// <param name="date">تاریخ دوره</param>
        /// <param name="personId">کد پرسنلی</param>
        /// <returns>سرانه اضافه کار تشویقی ثبت شده</returns>
        public OverTimePersonDetail GetByPersonId(DateTime date, decimal personId)
        {
            OverTimePersonDetail overTimePersonDetailAlias = null;
            OverTime overTimeAlias = null;

            IQueryOver<OverTimePersonDetail, OverTimePersonDetail> searchQuery;
            searchQuery = NHSession.QueryOver(() => overTimePersonDetailAlias)
                            .JoinAlias(() => overTimePersonDetailAlias.OverTime, () => overTimeAlias)
                            .Where(() =>
                                        overTimeAlias.Date == date
                                        && overTimePersonDetailAlias.Person.ID == personId
                            );

            OverTimePersonDetail item = searchQuery.SingleOrDefault<OverTimePersonDetail>();
            return item;
        }

        /// <summary>
        /// سرانه اضافه کار تشویقی ثبت شده در دیتابیس را برای یک شخص خاص در تاریخ مشخص برمی گرداند
        /// </summary>
        /// <param name="overTime">آبجکت دوره</param>
        /// <param name="personId">کد پرسنلی</param>
        /// <returns>سرانه اضافه کار تشویقی ثبت شده</returns>
        public OverTimePersonDetail GetByPersonId(OverTime overTime, decimal personId)
        {
            OverTimePersonDetail overTimePersonDetailAlias = null;

            IQueryOver<OverTimePersonDetail, OverTimePersonDetail> searchQuery;
            searchQuery = NHSession.QueryOver(() => overTimePersonDetailAlias)
                            .Where(() =>
                                        overTimePersonDetailAlias.OverTime.ID == overTime.ID
                                        && overTimePersonDetailAlias.Person.ID == personId
                            );

            OverTimePersonDetail item = searchQuery.SingleOrDefault<OverTimePersonDetail>();
            return item;
        }

        /// <summary>
        /// لیست سرانه پرسنل را برا اساس آبجکت ماه/سال اضافه کار تشویقی بر می گرداند
        /// </summary>
        /// <param name="overTimeID">کلید اصلی ماه/سال اضافه کار تشویقی</param>
        /// <returns>لیست سرانه پرسنل</returns>
        public IList<OverTimePersonDetail> GetListByOverTimeID(decimal overTimeID)
        {
            OverTimePersonDetail overTimePersonDetailAlias = null;

            IQueryOver<OverTimePersonDetail, OverTimePersonDetail> searchQuery;
            searchQuery = NHSession.QueryOver(() => overTimePersonDetailAlias)
                            .Where(() => overTimePersonDetailAlias.OverTime.ID == overTimeID);

            IList<OverTimePersonDetail> items = searchQuery.List<OverTimePersonDetail>();
            return items;
        }

        /// <summary>
        /// لیست سرانه های پرسنل را بر می گرداند
        /// </summary>
        /// <param name="overTime">دوره سرانه</param>
        /// <param name="personIds">لیست پرسنل</param>
        /// <returns>لیست سرانه</returns>
        public IList<OverTimePersonDetail> GetByPersonList(OverTime overTime, IList<decimal> personIds)
        {
            OverTimePersonDetail overTimePersonDetailAlias = null;
            IList<OverTimePersonDetail> list;

            if (personIds.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                list = NHSession.QueryOver<OverTimePersonDetail>()
                                .Where(c =>
                                            c.OverTime.ID == overTime.ID
                                            && c.Person.ID.IsIn(personIds.ToList())
                                ).List<OverTimePersonDetail>();
            }
            else
            {
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                string operationGUID = this.bTemp.InsertTempList(personIds);
                list = NHSession.QueryOver(() => overTimePersonDetailAlias)
                           .JoinAlias(() => overTimePersonDetailAlias.TempList, () => tempAlias)
                           .Where(() =>
                                        overTimePersonDetailAlias.OverTime.ID == overTime.ID
                                    && tempAlias.OperationGUID == operationGUID
                           ).List<OverTimePersonDetail>();

                this.bTemp.DeleteTempList(operationGUID);
            }
            return list;
        }

        /// <summary>
        /// لیست سرانه اضافه کار تشویقی پرسنل موجود در زیرشاخه های یک گره از چارت سازمانی را در تاریخ مشخص بر می گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="departmentId">گره چارت سازمانی</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد صفحه</param>
        /// <param name="totalCount">تعداد کل رکوردهای موجود</param>
        /// <returns>لیست سرانه اضافه کار تشویقی</returns>
        public IList<OverTimePersonProxy> GetAllByDepartmentId(int year, int month, int departmentId, decimal managerPersonId, string searchKey, int pageIndex, int pageSize, out int totalCount)
        {
            //تبدیل ماه و سال به اولین روز تاریخی
            DateTime date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            //END------------------------------------------------------------------------------------
            //لیست کلیه پرسنل موجود در زیرشاخه های یک گره از چارت سازمانی را واکشی می کند

            IList<UnderManagementPerson> underManagementPersons;
            //یک به معنای ریشه است و به جای آن جستجوی با عبارت خالی انجام می دهیم
            if (Utility.IsEmpty(searchKey) && departmentId != 0)
            {
                totalCount = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.NONE, managerPersonId, departmentId, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), GridOrderFields.NONE, GridOrderFieldType.asc, 0, 9999999).ToList().Count();
                // totalCount = bWorkedTime.GetJustUnderManagmentMainFlowByDepartmentCount(month, departmentId, managerPersonId);
                underManagementPersons = bWorkedTime.GetJustUnderManagmentMainFlowByDepartment(month, departmentId, managerPersonId, pageIndex, pageSize, GridOrderFields.gridFields_Family, GridOrderFieldType.asc).ToList();
            }
            else
            {
                totalCount = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.Complex, managerPersonId, 0, searchKey, searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), GridOrderFields.NONE, GridOrderFieldType.asc, 0, 9999999).ToList().Count();
                //totalCount = bWorkedTime.GetJustUnderManagmentMainFlowByDepartmentCount(month, 0, managerPersonId);
                underManagementPersons = bWorkedTime.GetJustUnderManagmentMainflowBySearch(month, searchKey, managerPersonId, GridSearchFields.Complex, pageIndex, pageSize, GridOrderFields.gridFields_Family, GridOrderFieldType.asc).ToList();
            }
            //  IList<UnderManagementPerson> underManagementPersons = new List<UnderManagementPerson>();
            //END-------------

            //آبجکت دوره را برای استفاده در شروط درخواست های بعدی واکشی می کنیم
            OverTime overTime = bOverTimeDetail.GetPeriodByDate(date);
            //END-------------

            //-----------------------
            //لیست سرانه ثبت شده در دیتابیس را بازای همه پرسنل در رم واکشی کن
            IList<OverTimePersonDetail> overTimePersonDetails = this.GetListByOverTimeID(overTime.ID);
            //-----------------------
            //TODO: filter by PersonId list
            //لیست اطلاعات اضافی کلیه پرسنل را در رم واکشی کن
            IList<Model.PersonTASpec> personTASpecList = bPerson.GetTASpecList();
            //-----------------------

            IList<OverTimePersonProxy> proxyList = new List<OverTimePersonProxy>();
            foreach (var item in underManagementPersons)
            {
                OverTimePersonProxy proxy = new OverTimePersonProxy();
                //اطلاعات پرسنل را جهت نوع پرسنل که اضافه کار تشویقی, تعطیل کاری تشویقی, شب کاری تشویقی دارد یا خیر را واکشی میکند
                //Model.PersonTASpec spec = bPerson.GetPersonTASpecByID(item.PersonId);
                //Select From RAM
                Model.PersonTASpec spec = personTASpecList.Where(c => c.ID == item.PersonId).FirstOrDefault();
                //END-------------- 

                //سرانه ثبت شده در دیتابیس را بازای شخص واکشی کن
                //OverTimePersonDetail overTimePersonDetail = this.GetByPersonId(overTime, item.PersonId);
                //Select From RAM
                OverTimePersonDetail overTimePersonDetail = overTimePersonDetails.Where(c => c.Person.ID == item.PersonId).FirstOrDefault();

                //سرانه ثبت شده مربوط به معاونت این شخص را از دیتابیس واکشی کن
                //Select From DATABASE
                OverTimeDetail overTimeDetail = bOverTimeDetail.GetDetailByPersonForValidation(item.PersonId, date);

                //چک می کند اگر بازای این شخص سرانه ای در دیتابیس ثبت شده است یا خیر
                if (overTimePersonDetail != null)
                {
                    //اگر بازای این شخص سرانه ای ثبت در دیتابیس شده بود اطلاعات ثبت شده را واکشی کند
                    proxy.MaxOverTime = overTimePersonDetail.MaxOverTime;
                    proxy.MaxHoliday = overTimePersonDetail.MaxHoliday;
                    proxy.MaxNightly = overTimePersonDetail.MaxNightly;
                    proxy.ID = overTimePersonDetail.ID;
                }
                else
                {
                    //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست مقدار پیش فرض صفر برای آن در دیتابیس ذخیره کند
                    //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست بر اساس مقدار پیش فرض برای اضافه کار آن در دیتابیس ذخیره کند
                    //بررسی شود آبجکت سرانه اصلی وجود داشته باشد
                    if (overTimeDetail != null)
                    {
                        var defaultMaxOverTimeObj = spec.GetParamValue(item.PersonId, "DefaultOverTime", DateTime.Now);
                        decimal defaultMaxOverTime = defaultMaxOverTimeObj != null ? Utility.ToDecimal(defaultMaxOverTimeObj.Value) : 0;

                        var defaultMaxHolidayObj = spec.GetParamValue(item.PersonId, "DefaultHoliday", DateTime.Now);
                        decimal defaultMaxHoliday = defaultMaxHolidayObj != null ? Utility.ToDecimal(defaultMaxHolidayObj.Value) : 0;

                        var defaultMaxNightlyObj = spec.GetParamValue(item.PersonId, "DefaultNightly", DateTime.Now);
                        decimal defaultMaxNightly = defaultMaxNightlyObj != null ? Utility.ToDecimal(defaultMaxNightlyObj.Value) : 0;

                        var obj = new OverTimePersonDetail();
                        obj.OverTime = overTime;
                        obj.Person = new Model.Person() { ID = item.PersonId };
                        obj.MaxOverTime = spec.OverTimeWork == true ? defaultMaxOverTime : 0;
                        obj.MaxHoliday = spec.HolidayWork == true ? defaultMaxHoliday : 0;
                        obj.MaxNightly = spec.NightWork == true ? defaultMaxNightly : 0;
                        obj.ModifiedBy = BUser.CurrentUser.Person;
                        obj.ModifiedDate = DateTime.Now;

                        decimal newID = this.InsertOverTimePersonDetail(obj);
                        proxy.ID = newID;
                        proxy.MaxOverTime = obj.MaxOverTime;
                        proxy.MaxHoliday = obj.MaxHoliday;
                        proxy.MaxNightly = obj.MaxNightly;
                    }
                }

                //اطلاعات پرسنلی
                proxy.PersonId = item.PersonId;
                proxy.PersonFullName = item.PersonName + " " + item.Family;
                proxy.PersonCode = item.BarCode;

                //اطلاعات مجوز استفاده از سرانه
                proxy.HasOverTimeWork = spec.OverTimeWork;
                proxy.HasHolidayWork = spec.HolidayWork;
                proxy.HasNightWork = spec.NightWork;

                if (overTimeDetail != null)
                {
                    //اطلاعات سرانه معاونت مربوطه
                    proxy.RealMaxOverTime = overTimeDetail.MaxOverTime;
                    proxy.RealMaxHoliday = overTimeDetail.MaxHoliday;
                    proxy.RealMaxNightly = overTimeDetail.MaxNightly;
                }

                //اطلاعات کارکرد
                proxy.DailyMeritoriouslyLeave = item.DailyMeritoriouslyLeave;
                proxy.DailySickLeave = item.DailySickLeave;
                proxy.DailyWithoutPayLeave = item.DailyWithoutPayLeave;
                proxy.DailyAbsence = item.DailyAbsence;
                proxy.DailyPureOperation = item.DailyPureOperation;
                proxy.AllowableOverTime = item.AllowableOverTime;
                proxy.UnallowableOverTime = item.UnallowableOverTime;

                proxyList.Add(proxy);

            }
            return proxyList;
        }

        /// <summary>
        /// مجموع سرانه پرسنل را بر می گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="departmentId">کلید اصلی چارت سازمانی</param>
        /// <returns>پروکسی مجموع سرانه پرسنل</returns>
        public OverTimeTotalPersonProxy GetTotalByDepartmentId(int year, int month, int departmentId, decimal managerPersonId, string searchKey)
        {
            //تبدیل ماه و سال به اولین روز تاریخی
            DateTime date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            //END------------------------------------------------------------------------------------
            //لیست کلیه پرسنل موجود در زیرشاخه های یک گره از چارت سازمانی را واکشی می کند

            IList<UnderManagementPerson> underManagementPersons;
            if (Utility.IsEmpty(searchKey) && departmentId != 0)
            {
                underManagementPersons = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.NONE, managerPersonId, departmentId, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), GridOrderFields.NONE, GridOrderFieldType.asc, 0, 9999999);
            }
            else
            {
                underManagementPersons = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.Complex, managerPersonId, 0, searchKey, searchKey, month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), GridOrderFields.NONE, GridOrderFieldType.asc, 0, 9999999);
            }

            //END-------------

            //TODO: filter by PersonId list
            //لیست اطلاعات اضافی کلیه پرسنل را در رم واکشی کن
            IList<Model.PersonTASpec> personTASpecList = bPerson.GetTASpecList();
            //-----------------------

            //آبجکت دوره را برای استفاده در شروط درخواست های بعدی واکشی می کنیم
            OverTime overTime = bOverTimeDetail.GetPeriodByDate(date);
            //END-------------

            IList<decimal> personIds = underManagementPersons.Select(c => c.PersonId).ToList();

            IList<OverTimePersonDetail> overTimePersonDetails = this.GetByPersonList(overTime, personIds);

            IList<OverTimePersonProxy> proxyList = new List<OverTimePersonProxy>();

            var calendarList = bCalendar.GetCalendarListByDateRange(date, date.AddMonths(1), "6");
            int MonthHolidayCount = calendarList != null ? calendarList.Count() : 0;
            foreach (var item in underManagementPersons)
            {
                OverTimePersonProxy proxy = new OverTimePersonProxy();

                //اطلاعات پرسنل را جهت نوع پرسنل که اضافه کار تشویقی, تعطیل کاری تشویقی, شب کاری تشویقی دارد یا خیر را واکشی میکند
                //Model.PersonTASpec spec = bPerson.GetPersonTASpecByID(item.PersonId);
                //Select From RAM
                Model.PersonTASpec spec = personTASpecList.Where(c => c.ID == item.PersonId).FirstOrDefault();
                //END-------------- 

                //سرانه ثبت شده مربوط به معاونت این شخص را از دیتابیس واکشی کن
                //Select From Database
                OverTimeDetail overTimeDetail = bOverTimeDetail.GetDetailByPersonForValidation(item.PersonId, date);
                if (overTimeDetail != null)
                {
                    //اطلاعات سرانه معاونت مربوطه
                    proxy.RealMaxOverTime = overTimeDetail.MaxOverTime;
                    proxy.RealMaxHoliday = spec.HolidayWork == true ? overTimeDetail.MaxHoliday * MonthHolidayCount / 100 : 0;
                    proxy.RealMaxNightly = spec.NightWork == true ? overTimeDetail.MaxNightly : 0;
                }
                //------------------------------------------------------------------------------------------------------------
                //سرانه ثبت شده در دیتابیس را بازای شخص واکشی کن
                //Select From RAM
                OverTimePersonDetail overTimePersonDetail = overTimePersonDetails.Where(c => c.Person.ID == item.PersonId).FirstOrDefault();
                if (overTimePersonDetail != null)
                {
                    //اگر بازای این شخص سرانه ای ثبت در دیتابیس شده بود اطلاعات ثبت شده را واکشی کند
                    proxy.MaxOverTime = overTimePersonDetail.MaxOverTime;
                    proxy.MaxHoliday = overTimePersonDetail.MaxHoliday;
                    proxy.MaxNightly = overTimePersonDetail.MaxNightly;
                    proxy.ID = overTimePersonDetail.ID;
                }
                else
                {
                    if (overTimeDetail != null)
                    {
                        //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست برود پیش فرض را از سرانه معاونت بگیرد و در دیتابیس ذخیره کند
                        //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست و بر اساس مقدار پیش فرض  برای اضافه کار آن در دیتابیس ذخیره کند

                        var defaultMaxOverTimeObj = spec.GetParamValue(item.PersonId, "DefaultOverTime", DateTime.Now);
                        decimal defaultMaxOverTime = defaultMaxOverTimeObj != null ? Utility.ToDecimal(defaultMaxOverTimeObj.Value) : 0;

                        var defaultMaxHolidayObj = spec.GetParamValue(item.PersonId, "DefaultHoliday", DateTime.Now);
                        decimal defaultMaxHoliday = defaultMaxHolidayObj != null ? Utility.ToDecimal(defaultMaxHolidayObj.Value) : 0;

                        var defaultMaxNightlyObj = spec.GetParamValue(item.PersonId, "DefaultNightly", DateTime.Now);
                        decimal defaultMaxNightly = defaultMaxNightlyObj != null ? Utility.ToDecimal(defaultMaxNightlyObj.Value) : 0;

                        var obj = new OverTimePersonDetail();
                        obj.OverTime = overTime;
                        obj.Person = new Model.Person() { ID = item.PersonId };
                        obj.MaxOverTime = spec.OverTimeWork == true ? defaultMaxOverTime : 0;
                        obj.MaxHoliday = spec.HolidayWork == true ? defaultMaxHoliday : 0;
                        obj.MaxNightly = spec.NightWork == true ? defaultMaxNightly : 0;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = BUser.CurrentUser.Person;

                        decimal newID = this.InsertOverTimePersonDetail(obj);

                        proxy.ID = newID;
                        proxy.MaxOverTime = obj.MaxOverTime;
                        proxy.MaxHoliday = obj.MaxHoliday;
                        proxy.MaxNightly = obj.MaxNightly;
                    }
                }

                proxyList.Add(proxy);
            }

            OverTimeTotalPersonProxy result = new OverTimeTotalPersonProxy();

            result.Total = proxyList.Count();
            result.MaxOverTime = proxyList.Select(c => c.MaxOverTime).Sum();
            result.MaxHoliday = proxyList.Select(c => c.MaxHoliday).Sum();
            result.MaxNightly = proxyList.Select(c => c.MaxNightly).Sum();
            result.RealMaxOverTime = proxyList.Select(c => c.RealMaxOverTime).Sum();
            result.RealMaxHoliday = proxyList.Select(c => c.RealMaxHoliday).Sum();
            result.RealMaxNightly = proxyList.Select(c => c.RealMaxNightly).Sum();

            return result;
        }

        /// <summary>
        /// لیست سرانه پرسنل را بر می گرداند
        /// </summary>
        /// <returns>لیست سرانه پرسنل </returns>
        public IList<OverTimePersonDetail> GetAllWithoutAthorize()
        {
            IList<OverTimePersonDetail> list = base.GetAll();
            return list;
        }

        /// <summary>
        /// لیست اضافه کار تشویقی پرسنل را بر اساس دسترسی کاربر به پرسنل به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="departmentId">کلید اصلی چارت سازمانی</param>
        /// <param name="searchKeyPerson">پرسنل</param>
        /// <param name="searchKeyCardNum">شماره کارت</param>
        /// <param name="searchKeyNationalCode">کد ملی</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد هر صفحه</param>
        /// <param name="totalCount">تعداد کل رکورد موجود</param>
        /// <returns>لیست اضافه کار تشویقی پرسنل</returns>
        public IList<OverTimePersonProxy> GetAllByAthorize(int year, int month, int departmentId, int costCenterId, int employmentTypeId, string searchKeyPerson,string searchKeyCardNum,string searchKeyNationalCode,  int pageIndex, int pageSize, out int totalCount)
        {
            //تبدیل ماه و سال به اولین روز تاریخی
            DateTime date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            //END------------------------------------------------------------------------------------
            //لیست کلیه پرسنل بر اساس دسترسی فرد و کلید چارت سازمانی را بر می گرداند
            PersonAdvanceSearchProxy personAdvanceSearchProxy = new PersonAdvanceSearchProxy();
            personAdvanceSearchProxy.PersonActivateState = true;
            personAdvanceSearchProxy.PersonIsDeleted = false;
            personAdvanceSearchProxy.SearchInCategory = GTS.Clock.Infrastructure.PersonCategory.Public;
            personAdvanceSearchProxy.OrganizationUnitId = 0;
            personAdvanceSearchProxy.UiValidationGroupID = 0;
            ISearchPerson searchPerson = new BPerson();
            if (departmentId != 1)
            {
                personAdvanceSearchProxy.IncludeSubDepartments = true;
                personAdvanceSearchProxy.DepartmentListId = new List<decimal>() { departmentId };
            }
            if (costCenterId != 0)
            {
                personAdvanceSearchProxy.CostCenterId = costCenterId;
            }
            if (employmentTypeId != 0)
            {
                List<decimal> EmploymentTypeListIds = new List<decimal>();
                EmploymentTypeListIds.Add(employmentTypeId);
                personAdvanceSearchProxy.EmploymentTypeListId = EmploymentTypeListIds;
            }

            if (!Utility.IsEmpty(searchKeyPerson))
                personAdvanceSearchProxy.LastName = searchKeyPerson;
            if (!Utility.IsEmpty(searchKeyCardNum))
                personAdvanceSearchProxy.CartNumber = searchKeyCardNum;
            if (!Utility.IsEmpty(searchKeyNationalCode))
                personAdvanceSearchProxy.PersonCode = searchKeyNationalCode;
            //ADD TASPEC 
            IList<Person> personList = searchPerson.GetPersonInAdvanceSearch(personAdvanceSearchProxy, 0, 99999999);
            totalCount = personList.Where(c => c.PersonTASpec.OverTimeWork == true).Count();

            //END------------------------------------------------------------------------------------
            //آبجکت دوره را برای استفاده در شروط درخواست های بعدی واکشی می کنیم
            OverTime overTime = bOverTimeDetail.GetPeriodByDate(date);
            //END------------------------------------------------------------------------------------

            IList<decimal> personIds = personList.Select(c => c.ID).ToList();
            //لیست سرانه ثبت شده در دیتابیس را بازای پرسنل در رم واکشی کن
            IList<OverTimePersonDetail> overTimePersonDetails = this.GetByPersonList(overTime, personIds);
            //END------------------------------------------------------------------------------------
            //لیست اطلاعات اضافی کلیه پرسنل را در رم واکشی کن
            IList<Model.PersonTASpec> personTASpecList = bPerson.GetTASpecByPersonIdList(personIds);
            //END------------------------------------------------------------------------------------

            IList<OverTimePersonProxy> proxyList = new List<OverTimePersonProxy>();
            foreach (var item in personList.Where(c => c.PersonTASpec.OverTimeWork == true))
            {
                OverTimePersonProxy proxy = new OverTimePersonProxy();
                //اطلاعات پرسنل را جهت نوع پرسنل که اضافه کار تشویقی, تعطیل کاری تشویقی, شب کاری تشویقی دارد یا خیر را واکشی میکند
                //Model.PersonTASpec spec = bPerson.GetPersonTASpecByID(item.PersonId);
                //Select From RAM
                Model.PersonTASpec spec = personTASpecList.Where(c => c.ID == item.ID).FirstOrDefault();
                //END-------------- 
                if (spec.OverTimeWork == true)
                {
                    //سرانه ثبت شده در دیتابیس را بازای شخص واکشی کن
                    //OverTimePersonDetail overTimePersonDetail = this.GetByPersonId(overTime, item.PersonId);
                    //Select From RAM
                    OverTimePersonDetail overTimePersonDetail = overTimePersonDetails.Where(c => c.Person.ID == item.ID).FirstOrDefault();

                    //سرانه ثبت شده مربوط به معاونت این شخص را از دیتابیس واکشی کن
                    //Select From DATABASE
                    OverTimeDetail overTimeDetail = bOverTimeDetail.GetDetailByPersonForValidation(item.ID, date);

                    //چک می کند اگر بازای این شخص سرانه ای در دیتابیس ثبت شده است یا خیر
                    if (overTimePersonDetail != null)
                    {
                        //اگر بازای این شخص سرانه ای ثبت در دیتابیس شده بود اطلاعات ثبت شده را واکشی کند
                        proxy.MaxOverTime = overTimePersonDetail.MaxOverTime;
                        proxy.MaxHoliday = overTimePersonDetail.MaxHoliday;
                        proxy.MaxNightly = overTimePersonDetail.MaxNightly;
                        proxy.ID = overTimePersonDetail.ID;
                    }
                    else
                    {
                        //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست مقدار پیش فرض صفر برای آن در دیتابیس ذخیره کند
                        //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست بر اساس مقدار پیش فرض برای اضافه کار آن در دیتابیس ذخیره کند
                        //بررسی شود آبجکت سرانه اصلی وجود داشته باشد
                        if (overTimeDetail != null)
                        {
                            var defaultMaxOverTimeObj = spec.GetParamValue(item.ID, "DefaultOverTime", DateTime.Now);
                            decimal defaultMaxOverTime = defaultMaxOverTimeObj != null ? Utility.ToDecimal(defaultMaxOverTimeObj.Value) : 0;

                            var defaultMaxHolidayObj = spec.GetParamValue(item.ID, "DefaultHoliday", DateTime.Now);
                            decimal defaultMaxHoliday = defaultMaxHolidayObj != null ? Utility.ToDecimal(defaultMaxHolidayObj.Value) : 0;

                            var defaultMaxNightlyObj = spec.GetParamValue(item.ID, "DefaultNightly", DateTime.Now);
                            decimal defaultMaxNightly = defaultMaxNightlyObj != null ? Utility.ToDecimal(defaultMaxNightlyObj.Value) : 0;

                            var obj = new OverTimePersonDetail();
                            obj.OverTime = overTime;
                            obj.Person = new Model.Person() { ID = item.ID };
                            obj.MaxOverTime = spec.OverTimeWork == true ? defaultMaxOverTime : 0;
                            obj.MaxHoliday = spec.HolidayWork == true ? defaultMaxHoliday : 0;
                            obj.MaxNightly = spec.NightWork == true ? defaultMaxNightly : 0;

                            decimal newID = this.InsertOverTimePersonDetail(obj);
                            proxy.ID = newID;
                            proxy.MaxOverTime = obj.MaxOverTime;
                            proxy.MaxHoliday = obj.MaxHoliday;
                            proxy.MaxNightly = obj.MaxNightly;
                        }
                    }

                    //اطلاعات پرسنلی
                    proxy.PersonId = item.ID;
                    proxy.PersonFullName = item.Name;
                    proxy.PersonCode = item.BarCode;

                    //اطلاعات مجوز استفاده از سرانه
                    proxy.HasOverTimeWork = spec.OverTimeWork;
                    proxy.HasHolidayWork = spec.HolidayWork;
                    proxy.HasNightWork = spec.NightWork;

                    if (overTimeDetail != null)
                    {
                        //اطلاعات سرانه معاونت مربوطه
                        proxy.RealMaxOverTime = overTimeDetail.MaxOverTime;
                        proxy.RealMaxHoliday = overTimeDetail.MaxHoliday;
                        proxy.RealMaxNightly = overTimeDetail.MaxNightly;
                    }

                    //اطلاعات کارکرد--------------------------------------------------------------------------------------------------------------
                    IList<InfoPeriodicScndCnpValue> infoPeriodicScndCnpValueList = NHibernateSessionManager.Instance.GetSession().GetNamedQuery("GetPeriodicScndCnpValueList")
                                                                                                                                                        .SetParameter("date", DateTime.Now.Date)
                                                                                                                                                        .SetParameter("dateRangeOrderIndex", month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource))
                                                                                                                                                        .SetParameter("dateRangeOrder", month)
                                                                                                                                                        .SetParameter("prsId", item.ID)
                                                                                                                                                        .SetResultTransformer(Transformers.AliasToBean(typeof(InfoPeriodicScndCnpValue)))
                                                                                                                                                        .List<InfoPeriodicScndCnpValue>();
                    string defaultValue = "0";
                    InfoPeriodicScndCnpValue ipscv_DailyMeritoriouslyLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyMeritoriouslyLeave").FirstOrDefault();
                    proxy.DailyMeritoriouslyLeave = ipscv_DailyMeritoriouslyLeave != null ? ipscv_DailyMeritoriouslyLeave == null ? "0" : ipscv_DailyMeritoriouslyLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;

                    InfoPeriodicScndCnpValue ipscv_DailySickLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailySickLeave").FirstOrDefault();
                    proxy.DailySickLeave = ipscv_DailySickLeave != null ? ipscv_DailySickLeave == null ? "0" : ipscv_DailySickLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;

                    InfoPeriodicScndCnpValue ipscv_DailyWithoutPayLeave = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyWithoutPayLeave").FirstOrDefault();
                    proxy.DailyWithoutPayLeave = ipscv_DailyWithoutPayLeave != null ? ipscv_DailyWithoutPayLeave == null ? "0" : ipscv_DailyWithoutPayLeave.ScndCnpValue_PeriodicValue.ToString() : defaultValue;

                    InfoPeriodicScndCnpValue ipscv_DailyAbsence = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyAbsence").FirstOrDefault();
                    proxy.DailyAbsence = ipscv_DailyAbsence != null ? ipscv_DailyAbsence == null ? "0" : ipscv_DailyAbsence.ScndCnpValue_PeriodicValue.ToString() : defaultValue;

                    InfoPeriodicScndCnpValue ipscv_DailyPureOperation = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_DailyPureOperation").FirstOrDefault();
                    proxy.DailyPureOperation = ipscv_DailyPureOperation != null ? ipscv_DailyPureOperation == null ? "0" : ipscv_DailyPureOperation.ScndCnpValue_PeriodicValue.ToString() : defaultValue;

                    InfoPeriodicScndCnpValue ipscv_AllowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_AllowableOverTime").FirstOrDefault();
                    proxy.AllowableOverTime = ipscv_AllowableOverTime != null ? Utility.IntTimeToTime(ipscv_AllowableOverTime == null ? 0 : ipscv_AllowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                    InfoPeriodicScndCnpValue ipscv_UnallowableOverTime = infoPeriodicScndCnpValueList.Where(x => x.ScndCnpValue_KeyColumnName == "gridFields_UnallowableOverTime").FirstOrDefault();
                    proxy.UnallowableOverTime = ipscv_UnallowableOverTime != null ? Utility.IntTimeToTime(ipscv_UnallowableOverTime == null ? 0 : ipscv_UnallowableOverTime.ScndCnpValue_PeriodicValue) : defaultValue;

                    proxyList.Add(proxy);
                }
            }
            return proxyList;
        }

        /// <summary>
        /// لیست کارکرد نهایی پرسنل را بر اساس دسترسی کاربر به پرسنل به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="departmentId">کلید اصلی چارت سازمانی</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد هر صفحه</param>
        /// <param name="totalCount">تعداد کل رکورد موجود</param>
        /// <returns>لیست کارکرد پرسنل</returns>
        public List<FunctionProxy> GetFunctionsByAthorize(int year, int month, int departmentId, int costCenterId, int employmentTypeId, string searchKey, int pageIndex, int pageSize, out int totalCount)
        {

            PersonAdvanceSearchProxy personAdvanceSearchProxy = new PersonAdvanceSearchProxy();
            personAdvanceSearchProxy.PersonActivateState = true;
            personAdvanceSearchProxy.PersonIsDeleted = false;
            personAdvanceSearchProxy.SearchInCategory = GTS.Clock.Infrastructure.PersonCategory.Public;
            personAdvanceSearchProxy.OrganizationUnitId = 0;
            personAdvanceSearchProxy.UiValidationGroupID = 0;
            ISearchPerson searchPerson = new BPerson();
            //if (departmentId != 1)
            //{
            personAdvanceSearchProxy.IncludeSubDepartments = true;
            personAdvanceSearchProxy.DepartmentListId = new List<decimal>() { departmentId };
            //}
            if (costCenterId != 0)
            {
                personAdvanceSearchProxy.CostCenterId = costCenterId;
            }
            if (employmentTypeId != 0)
            {
                List<decimal> EmploymentTypeListIds = new List<decimal>();
                EmploymentTypeListIds.Add(employmentTypeId);
                personAdvanceSearchProxy.EmploymentTypeListId = EmploymentTypeListIds;
            }
            if (!Utility.IsEmpty(searchKey))
                personAdvanceSearchProxy.LastName = searchKey;


            IList<Person> personList = searchPerson.GetPersonInAdvanceSearch(personAdvanceSearchProxy, 0, 99999999);
            totalCount = personList.Count();
            IList<decimal> personIds = personList.Select(c => c.ID).ToList();

            //دریافت اطلاعات کارکد پرسنل از دیتابیس
            List<FunctionProxy> functionList = GetPersonFunctions(personIds, year, month).ToList();

            return functionList;
        }

        /// <summary>
        /// مجموع اضافه کار تشویقی پرسنل را بر اساس دسترسی کاربر به پرسنل به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="departmentId">کلید اصلی چارت سازمانی</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد هر صفحه</param>
        /// <param name="totalCount">تعداد کل رکورد موجود</param>
        /// <returns>لیست اضافه کار تشویقی پرسنل</returns>
        public OverTimeTotalPersonProxy GetTotalByAthorize(int year, int month, int departmentId, int costCenterId, string searchKey)
        {
            //تبدیل ماه و سال به اولین روز تاریخی
            DateTime date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            //END------------------------------------------------------------------------------------
            //لیست کلیه پرسنل موجود در زیرشاخه های یک گره از چارت سازمانی را واکشی می کند
            PersonAdvanceSearchProxy personAdvanceSearchProxy = new PersonAdvanceSearchProxy();
            personAdvanceSearchProxy.PersonActivateState = true;
            personAdvanceSearchProxy.PersonIsDeleted = false;
            ISearchPerson searchPerson = new BPerson();
            if (departmentId != 1)
            {
                personAdvanceSearchProxy.IncludeSubDepartments = true;
                personAdvanceSearchProxy.DepartmentListId = new List<decimal>() { departmentId };
            }
            if (costCenterId != 0)
            {
                personAdvanceSearchProxy.CostCenterId = costCenterId;
            }
            if (!Utility.IsEmpty(searchKey))
                personAdvanceSearchProxy.LastName = searchKey;

            IList<Person> personList = searchPerson.GetPersonInAdvanceSearch(personAdvanceSearchProxy, 0, 9999999);
            //END------------------------------------------------------------------------------------
            IList<decimal> personIds = personList.Select(c => c.ID).ToList();
            //TODO: filter by PersonId list
            //لیست اطلاعات اضافی کلیه پرسنل را در رم واکشی کن
            IList<Model.PersonTASpec> personTASpecList = bPerson.GetTASpecByPersonIdList(personIds);
            //END------------------------------------------------------------------------------------

            //آبجکت دوره را برای استفاده در شروط درخواست های بعدی واکشی می کنیم
            OverTime overTime = bOverTimeDetail.GetPeriodByDate(date);
            //END------------------------------------------------------------------------------------

            IList<OverTimePersonDetail> overTimePersonDetails = this.GetByPersonList(overTime, personIds);

            IList<OverTimePersonProxy> proxyList = new List<OverTimePersonProxy>();

            var calendarList = bCalendar.GetCalendarListByDateRange(date, date.AddMonths(1), "6");
            int MonthHolidayCount = calendarList != null ? calendarList.Count() : 0;
            foreach (var item in personList)
            {
                OverTimePersonProxy proxy = new OverTimePersonProxy();

                //اطلاعات پرسنل را جهت نوع پرسنل که اضافه کار تشویقی, تعطیل کاری تشویقی, شب کاری تشویقی دارد یا خیر را واکشی میکند
                Model.PersonTASpec spec = personTASpecList.Where(c => c.ID == item.ID).FirstOrDefault();
                //END------------------------------------------------------------------------------------

                if (spec.OverTimeWork == true)
                {
                    //سرانه ثبت شده مربوط به معاونت این شخص را از دیتابیس واکشی کن
                    //Select From Database
                    OverTimeDetail overTimeDetail = bOverTimeDetail.GetDetailByPersonForValidation(item.ID, date);
                    if (overTimeDetail != null)
                    {
                        //اطلاعات سرانه معاونت مربوطه
                        proxy.RealMaxOverTime = overTimeDetail.MaxOverTime;
                        proxy.RealMaxHoliday = spec.HolidayWork == true ? overTimeDetail.MaxHoliday * MonthHolidayCount / 100 : 0;
                        proxy.RealMaxNightly = spec.NightWork == true ? overTimeDetail.MaxNightly : 0;
                    }
                    //------------------------------------------------------------------------------------------------------------
                    //سرانه ثبت شده در دیتابیس را بازای شخص واکشی کن
                    //Select From RAM
                    OverTimePersonDetail overTimePersonDetail = overTimePersonDetails.Where(c => c.Person.ID == item.ID).FirstOrDefault();
                    if (overTimePersonDetail != null)
                    {
                        //اگر بازای این شخص سرانه ای ثبت در دیتابیس شده بود اطلاعات ثبت شده را واکشی کند
                        proxy.MaxOverTime = overTimePersonDetail.MaxOverTime;
                        proxy.MaxHoliday = overTimePersonDetail.MaxHoliday;
                        proxy.MaxNightly = overTimePersonDetail.MaxNightly;
                        proxy.ID = overTimePersonDetail.ID;
                    }
                    else
                    {
                        if (overTimeDetail != null)
                        {
                            //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست برود پیش فرض را از سرانه معاونت بگیرد و در دیتابیس ذخیره کند
                            //اگر بازای این شخص سرانه ای ثبت نشده بود می بایست بر اساس مقدار پیش فرض  برای اضافه کار آن در دیتابیس ذخیره کند

                            var defaultMaxOverTimeObj = spec.GetParamValue(item.ID, "DefaultOverTime", DateTime.Now);
                            decimal defaultMaxOverTime = defaultMaxOverTimeObj != null ? Utility.ToDecimal(defaultMaxOverTimeObj.Value) : 0;

                            var defaultMaxHolidayObj = spec.GetParamValue(item.ID, "DefaultHoliday", DateTime.Now);
                            decimal defaultMaxHoliday = defaultMaxHolidayObj != null ? Utility.ToDecimal(defaultMaxHolidayObj.Value) : 0;

                            var defaultMaxNightlyObj = spec.GetParamValue(item.ID, "DefaultNightly", DateTime.Now);
                            decimal defaultMaxNightly = defaultMaxNightlyObj != null ? Utility.ToDecimal(defaultMaxNightlyObj.Value) : 0;

                            var obj = new OverTimePersonDetail();
                            obj.OverTime = overTime;
                            obj.Person = new Model.Person() { ID = item.ID };
                            obj.MaxOverTime = spec.OverTimeWork == true ? defaultMaxOverTime : 0;
                            obj.MaxHoliday = spec.HolidayWork == true ? defaultMaxHoliday : 0;
                            obj.MaxNightly = spec.NightWork == true ? defaultMaxNightly : 0;

                            decimal newID = this.InsertOverTimePersonDetail(obj);
                            proxy.ID = newID;
                            proxy.MaxOverTime = obj.MaxOverTime;
                            proxy.MaxHoliday = obj.MaxHoliday;
                            proxy.MaxNightly = obj.MaxNightly;
                        }
                    }

                    proxyList.Add(proxy);
                }
            }

            OverTimeTotalPersonProxy result = new OverTimeTotalPersonProxy();

            result.Total = proxyList.Count();
            result.MaxOverTime = proxyList.Select(c => c.MaxOverTime).Sum();
            result.MaxHoliday = proxyList.Select(c => c.MaxHoliday).Sum();
            result.MaxNightly = proxyList.Select(c => c.MaxNightly).Sum();
            result.RealMaxOverTime = proxyList.Select(c => c.RealMaxOverTime).Sum();
            result.RealMaxHoliday = proxyList.Select(c => c.RealMaxHoliday).Sum();
            result.RealMaxNightly = proxyList.Select(c => c.RealMaxNightly).Sum();

            return result;
        }

        /// <summary>
        /// مجموع اضافه کار تشویقی پرسنل را بر اساس دسترسی کاربر به پرسنل به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="departmentId">کلید اصلی چارت سازمانی</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد هر صفحه</param>
        /// <param name="totalCount">تعداد کل رکورد موجود</param>
        /// <returns>لیست اضافه کار تشویقی پرسنل</returns>
        public OverTimeTotalPersonProxy GetOrganizationTotal(int year, int month, int departmentId)
        {
            //تبدیل ماه و سال به اولین روز تاریخی
            DateTime date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            //END------------------------------------------------------------------------------------
            OverTimeTotalPersonProxy proxy = new OverTimeTotalPersonProxy();
            decimal OrgMaxOverTime = 0, OrgMaxNightly = 0, OrgMaxHoliday = 0;
            decimal OrgAssignedOverTime = 0, OrgAssignedHoliday = 0, OrgAssignedNightly = 0;

            OverTime overTime = bOverTimeDetail.GetActivePeriodByDate(date);
            if (overTime == null) return null;

            //تعداد روزهای تعطیل ماه مربوطه را واکشی کن
            var calendarList = bCalendar.GetCalendarListByDateRange(overTime.Date, overTime.Date.AddMonths(1), "6");
            int MonthHolidayCount = calendarList != null ? calendarList.Count() : 0;

            //لیست سرانه همه معاونت های سازمان
            IList<OverTimeDetail> overtimeDetails = bOverTimeDetail.GetOrganizationDetailByDepartmentId(departmentId, date);

            foreach (var item in overtimeDetails)
            {
                //1-جمع سرانه پیش فرض کلیه پرسنل مدیریت را واکشی کن--------------------------------------------------------------------------
                OverTimeDetail overtimeDetail = bOverTimeDetail.GetByID(item.ID);

                //لیست پرسنل آن معاونت را بازیابی کن
                IList<Person> persons = bPerson.GetPersonsByDepartmentId(overtimeDetail.Department.ID);
                IList<decimal> PersonIds = persons.Select(c => c.ID).ToList();
                IList<PersonTASpec> PersonSpecs = bPerson.GetTASpecByPersonIdList(PersonIds);

                //تعداد کل پرسنل را در مقدار پیش فرض ضرب کن

                int OverTimeCount = PersonSpecs.Where(c => c.OverTimeWork == true).ToList().Count();
                int NightlyCount = PersonSpecs.Where(c => c.NightWork == true).ToList().Count();
                int HolidayCount = PersonSpecs.Where(c => c.HolidayWork == true).ToList().Count();

                proxy.OverTimeCount += OverTimeCount;
                proxy.NightlyCount += NightlyCount;
                proxy.HolidayCount += HolidayCount;

                proxy.RealMaxOverTime += 120 * OverTimeCount;
                proxy.RealMaxNightly += 25 * NightlyCount * (month <= 6 ? 31 : 30 - MonthHolidayCount) / 100;
                proxy.RealMaxHoliday += 50 * HolidayCount * MonthHolidayCount / 100;

                OrgMaxOverTime += 120 * OverTimeCount;
                OrgMaxNightly += 25 * NightlyCount * (month <= 6 ? 31 : 30 - MonthHolidayCount) / 100;
                OrgMaxHoliday += 50 * HolidayCount * MonthHolidayCount / 100;

                //2-جمع سرانه اختصاص داده شده پرسنل سازمان را واکشی کن--------------------------------------------------------------------------
                //لیست سرانه ذخیره شده پرسنل معاونت را واکشی کن
                IList<OverTimePersonDetail> overTimePersonDetails = this.GetByPersonList(overTime, PersonIds);

                proxy.MaxOverTime += overTimePersonDetails.Select(c => c.MaxOverTime).Sum();
                proxy.MaxHoliday += overTimePersonDetails.Select(c => c.MaxHoliday).Sum();
                proxy.MaxNightly += overTimePersonDetails.Select(c => c.MaxNightly).Sum();

                OrgAssignedOverTime += overTimePersonDetails.Select(c => c.MaxOverTime).Sum();
                OrgAssignedHoliday += overTimePersonDetails.Select(c => c.MaxHoliday).Sum();
                OrgAssignedNightly += overTimePersonDetails.Select(c => c.MaxNightly).Sum();
            }

            return proxy;

        }

        /// <summary>
        /// ارسال کارکرد به وب سرویس
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="departmentId">کلید بخش</param>
        /// <param name="searchKey">عبارت جستجو</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool SendPersonsFunctionList(int year, int month, int departmentId, int costCenterId, int employmentTypeId, string searchKey)
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            bool result = false;
            GTS.Clock.Business.TMIFunctionServiceRefrence.InsertListResponse response;

            //لیست کلیه پرسنل بر اساس دسترسی فرد و کلید چارت سازمانی را بر می گرداند
            PersonAdvanceSearchProxy personAdvanceSearchProxy = new PersonAdvanceSearchProxy();
            personAdvanceSearchProxy.PersonActivateState = true;
            personAdvanceSearchProxy.PersonIsDeleted = false;
            personAdvanceSearchProxy.SearchInCategory = GTS.Clock.Infrastructure.PersonCategory.Public;
            personAdvanceSearchProxy.OrganizationUnitId = 0;
            personAdvanceSearchProxy.UiValidationGroupID = 0;
            ISearchPerson searchPerson = new BPerson();
            if (departmentId != 1)
            {
                personAdvanceSearchProxy.IncludeSubDepartments = true;
                personAdvanceSearchProxy.DepartmentListId = new List<decimal>() { departmentId };
            }
            if (costCenterId != 0)
            {
                personAdvanceSearchProxy.CostCenterId = costCenterId;
            }
            if (employmentTypeId != 0)
            {
                List<decimal> EmploymentTypeListIds = new List<decimal>();
                EmploymentTypeListIds.Add(employmentTypeId);
                personAdvanceSearchProxy.EmploymentTypeListId = EmploymentTypeListIds;
            }
            if (!Utility.IsEmpty(searchKey))
                personAdvanceSearchProxy.LastName = searchKey;

            IList<Person> personList = searchPerson.GetPersonInAdvanceSearch(personAdvanceSearchProxy, 0, 99999999);

            if (personList == null || personList.Count == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonCodeRequierd, "پرسنلی جهت ارسال کارکرد یافت نشد", ExceptionSrc));
                throw exception;
            }

            List<decimal> personIds = personList.Select(c => c.ID).ToList();
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //دریافت اطلاعات کارکد پرسنل از دیتابیس
            List<FunctionProxy> functionList = GetPersonFunctions(personIds, year, month).ToList();
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //تبدیل اطلاعات به مدل وب سرویس
            var list = this.ConvertFunctionToPoCo(functionList);

            //------------------------------------------------------------------------------------------------------------------------------------------------
            //فراخوانی وب سرویس و ارسال کارکرد
            try
            {
                GTS.Clock.Business.TMIFunctionServiceRefrence.FunctionClient functionClient = new GTS.Clock.Business.TMIFunctionServiceRefrence.FunctionClient();
                functionClient.Open();

                var requestList = new GTS.Clock.Business.TMIFunctionServiceRefrence.InsertListRequest(list);
                response = functionClient.InsertList(requestList);
                if (response.InsertListResult.ToString() == "Ok")
                {
                    result = true;
                }
                else
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonCodeRequierd, response.InsertListResult.ToString(), ExceptionSrc));
                }
                functionClient.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("خطا در ارتباط با وب سرویس");
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            //ثبت لاگ مربوط به فراخوانی وب سرویس
            string clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress != null ? System.Web.HttpContext.Current.Request.UserHostAddress : string.Empty;
            var modifiedDate = DateTime.Now;
            foreach (var person in personList)
            {
                var item = list.Where(c => c.NationalNo.Contains(person.BarCode)).FirstOrDefault();
                var json = new JavaScriptSerializer().Serialize(item);
                sendPersonFunctionLogRepository.Save(new SendPersonFunctionLog()
                {
                    IPAddress = clientIPAddress,
                    ModifiedBy = BUser.CurrentUser.Person,
                    ModifiedDate = modifiedDate,
                    Year = year,
                    Month = month,
                    Person = person,
                    Result = response.InsertListResult.ToString(),
                    JsonObj = json.ToString()
                });
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------  
            if (exception.Count > 0)
            {
                throw exception;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------
            return result;
            //------------------------------------------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// کارکرد پرسنل را جهت ارسال به وب سرویس واکشی می کند
        /// </summary>
        /// <param name="personIds">لیست پرسنل انتخاب شده</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>لیست پروکسی کارکرد</returns>
        private IList<FunctionProxy> GetPersonFunctions(IList<decimal> personIds, int year, int month)
        {
            IList<FunctionProxy> list = null;

            string SQLCommand = string.Empty;
            string personOperationGUID = this.tempRepository.InsertTempList(personIds);

            //TODO://تبدیل تعداد روزهای ماه شهریور به 30 روز به طور موقت جهت ارسال کارکرد
            //convert(int,(case when [CnpValue_RangeOrder] between 1 and 6 then 31 when [CnpValue_RangeOrder]=12 then 29 else 30 End)) AS [FunctionDay] ,
            SQLCommand = @"select    
                        prs.Prs_FirstName + ' ' + prs.Prs_LastName AS [PersonnelFullName],
                        HRM.NationalNo AS HRM_NationalNo,
                        prs.Prs_ID AS [PersonnelId],
                        REPLACE(STR(prs.Prs_Barcode, 10), SPACE(1), '0') AS [PersonnelCode],
                        TA_CostCenter.Cost_Code as [CurrentServiceLocationId],
                        convert(int,isnull([gridFields_ReserveField7]+[Otp_MaxHoliday],0))  AS [PersonnelWorkingHolidaysDay],
                        convert(decimal(18,2),(isnull(round((([gridFields_ReserveField7]+[Otp_MaxHoliday])*7.33),0.2),0))) AS [PersonnelWorkingHolidays],	
                        convert(int,round(((isnull(Otp_MaxNightly,0)+(isnull([gridFields_ReserveField6],0)/60)) /7.33),0.2)) AS [PersonnelNightWorkDay],
                        convert(decimal(18,2),round((isnull(Otp_MaxNightly,0)+(isnull([gridFields_ReserveField6],0)/60)),0.2)) AS [PersonnelNightWorkHours],
	                    Convert(int,Result.[gridFields_DailyMeritoriouslyLeave]) AS [DeservedFunctionOutHoliday],
                        0 AS [DeservedFunctionInHoliday],
                        0 AS [PaylessDay],
                        convert(varchar(2),CnpValue_RangeOrder) AS [Month],
                        convert(varchar(4),CnpValue_Year) AS [Year],   
                        convert(decimal(18,0),[gridFields_MonthlyHolidayFoodDue]) AS [HolidayFunctionDay],
                        convert(decimal(18,0),[gridFields_MonthlyFoodDue]) AS [RealFunctionDay],
                        isnull([Otp_MaxOverTime],0) AS [PersonnelOverTimeHours],
                        CAST(ROUND([gridFields_HourlyMission]/60,0,2) AS decimal(18,0)) AS [PersonnelMissionHours],
                        convert(decimal(18,0),[gridFields_DailyAbsence]) AS [PersonnelNoEnter],
                        Convert(int,case when emply_CustomCode=168 then Round([gridFields_PresenceDuration] / 60 /6.4,0,1) else  [gridFields_DailyPureOperation] End) AS [PersonnelFunctionDay],
                        convert(decimal(18,0),(case when [gridFields_DailySickLeave] between 0 and 3 then 0 else [gridFields_HourlyUnallowableAbsence] End)) AS [PersonnelIllnessDay],
                        CAST(ROUND((CAST((case when [gridFields_HourlyUnallowableAbsence] between 0 and 120 then 0 else [gridFields_HourlyUnallowableAbsence] End) AS REAL)/60),2,2) AS DECIMAL(18,2)) AS PersonnelAbsence,
                        convert(int,(case when emply_CustomCode=168 then [gridFields_PresenceDuration] else 0 end)) AS [PersonnelHourPresent],
                        convert(int,(case when [CnpValue_RangeOrder] between 1 and 5 then 31 when [CnpValue_RangeOrder]=12 then 29 else 30 End)) AS [FunctionDay] ,
                        TA_OrganizationUnit.organ_Name AS [PostType] ,
                        TA_Department.dep_ID AS [DepartmentID]                                                                              
	
	                    from (	SELECT ACV.CnpValue_PersonId,ACV.CnpValue_FromDate,ACV.CnpValue_ToDate,CT.ConceptTmp_KeyColumnName,ACV.CnpValue_ChangedValue,ACV.CnpValue_Year,ACV.CnpValue_RangeOrder
			                    From TA_ArchiveConceptValue AS ACV
			                    INNER JOIN TA_ConceptTemplate CT ON ACV.CnpValue_ConceptTmpId=CT.ConceptTmp_ID
			                    WHERE ACV.CnpValue_Year= :hijriYear AND ACV.CnpValue_RangeOrder= :rangeOrder
	                      )	AS A
                        PIVOT
                        (SUM(CnpValue_ChangedValue)
                         FOR ConceptTmp_KeyColumnName IN ( [gridFields_NecessaryOperation]
								                          ,[gridFields_HourlyPureOperation]
								                          ,[gridFields_DailyPureOperation]
								                          ,[gridFields_ImpureOperation]
								                          ,[gridFields_AllowableOverTime]
								                          ,[gridFields_UnallowableOverTime]
								                          ,[gridFields_HourlyAllowableAbsence]
								                          ,[gridFields_HourlyUnallowableAbsence]
								                          ,[gridFields_DailyAbsence]
								                          ,[gridFields_HourlyMission]
								                          ,[gridFields_DailyMission]
								                          ,[gridFields_HostelryMission]
								                          ,[gridFields_HourlySickLeave]
								                          ,[gridFields_DailySickLeave]
								                          ,[gridFields_HourlyMeritoriouslyLeave]
								                          ,[gridFields_DailyMeritoriouslyLeave]
								                          ,[gridFields_HourlyWithoutPayLeave]
								                          ,[gridFields_PresenceDuration]
								                          ,[gridFields_DailyWithoutPayLeave]
								                          ,[gridFields_HourlyWithPayLeave]
								                          ,[gridFields_DailyWithPayLeave]
								                          ,[gridFields_MonthlyFoodDue]
								                          ,[gridFields_MonthlyHolidayFoodDue]
								                          ,[gridFields_ReserveField6]
								                          ,[gridFields_ReserveField7])
                        ) AS Result			
                        INNER JOIN TA_Person AS	Prs	ON Result.CnpValue_PersonId = Prs.Prs_ID and prs.Prs_Active=1 and prs.prs_IsDeleted=0
                        LEFT JOIN TA_PersonTASpec AS PrsSpec ON PrsSpec.prsTA_ID=Prs.Prs_ID
                        INNER JOIN TA_EmploymentType ON Prs_EmployId=emply_ID
                        LEFT  JOIN (SELECT * FROM TA_OverTimePersonDetail	
					                         JOIN TA_OverTime ON	TA_OverTime.OverTime_ID=TA_OverTimePersonDetail.Otp_OverTimeID 
                                                                AND Convert(int,right(left(dbo.GTS_ASM_MiladiToShamsi( Convert(varchar(10),TA_OverTime.OverTime_DateTime,101)),7),2)) = :rangeOrder
                                                                AND Convert(int,right(left(dbo.GTS_ASM_MiladiToShamsi( Convert(varchar(10),TA_OverTime.OverTime_DateTime,101)),4),4)) = :hijriYear  
                                    ) AS overwork ON prs.Prs_ID=Otp_PrsID
                        INNER JOIN  TA_Department           ON dep_id=prs.Prs_DepartmentId
                        LEFT JOIN   TA_OrganizationUnit     ON TA_OrganizationUnit.organ_PersonID=prs.Prs_ID
                        LEFT JOIN   TA_CostCenter           ON TA_CostCenter.Cost_ID=prs.Prs_CostCenterID
                        INNER JOIN  TA_Temp AS personTemp   ON prs.Prs_ID = personTemp.temp_ObjectID and  personTemp.temp_OperationGUID = :personOperationGUID 
                        LEFT JOIN   [HumanResourceManagement].[dbo].[vwEmp_Employee]  AS HRM     ON HRM.NationalNo = REPLACE(STR(prs.Prs_Barcode, 10), SPACE(1), '0') 
                        where PrsSpec.prsTA_HasPeyment=1 ";


            IQuery Query = this.NHSession.CreateSQLQuery(SQLCommand)
              .SetResultTransformer(Transformers.AliasToBean(typeof(FunctionProxy)))
              .SetParameter("personOperationGUID", personOperationGUID)
              .SetParameter("rangeOrder", month)
              .SetParameter("hijriYear", year);

            list = Query.List<FunctionProxy>();

            this.tempRepository.DeleteTempList(personOperationGUID);

            return list;
        }

        private List<GTS.Clock.Business.TMIFunctionServiceRefrence.PoCoTempPersonnelFunction> ConvertFunctionToPoCo(List<FunctionProxy> functionList)
        {

            var PoCoList = new List<GTS.Clock.Business.TMIFunctionServiceRefrence.PoCoTempPersonnelFunction>();

            foreach (var item in functionList)
            {
                var organization = bDepartment.GetOraganizationByPersonId(item.PersonnelId);

                var PoCoItem = new GTS.Clock.Business.TMIFunctionServiceRefrence.PoCoTempPersonnelFunction();

                PoCoItem.NationalNo = item.PersonnelCode;//کد ملی

                //به دلیل آنکه در شهرداری حقوق هر ماه با کارکرد ماه قبل محاسبه می شود لذا ماه را بعلاوه یک می کنیم
                if (item.Month == "12")
                {
                    PoCoItem.Year = (Int32.Parse(item.Year) + 1).ToString();
                    PoCoItem.Month = "01";
                }
                else
                {
                    PoCoItem.Year = item.Year;// ماه کارکرد شمسی
                    PoCoItem.Month = (Int32.Parse(item.Month) + 1).ToString();
                    PoCoItem.Month = (Int32.Parse(item.Month) + 1) >= 10 ? (Int32.Parse(item.Month) + 1).ToString() : "0" + (Int32.Parse(item.Month) + 1).ToString();
                }
                //----------------------------------------------------------------------------------------------------------
                PoCoItem.PersonnelFunctionDay = item.PersonnelFunctionDay;//روزهای کارکرد
                PoCoItem.FunctionDay = item.FunctionDay;//روزهای کاری ماه
                PoCoItem.DeservedFunctionInHoliday = item.DeservedFunctionInHoliday;//مرخصی های بین تعطیلی
                PoCoItem.DeservedFunctionOutHoliday = item.DeservedFunctionOutHoliday;//مرخصی های خارج از تعطیلی
                PoCoItem.RealFunctionDay = item.RealFunctionDay;//نهار
                PoCoItem.HolidayFunctionDay = item.HolidayFunctionDay;//نهار تعطیل
                PoCoItem.PersonnelHourPresent = item.PersonnelHourPresent;//ساعت حضور برای استخدام های ساعتی
                PoCoItem.PersonnelNoEnter = item.PersonnelNoEnter;//روزهای غیبت
                PoCoItem.PersonnelAbsence = item.PersonnelAbsence;//غیبت ساعتی
                PoCoItem.PersonnelIllnessDay = item.PersonnelIllnessDay;//استعلاجی
                PoCoItem.PersonnelMissionHours = item.PersonnelMissionHours;//ماموریت ساعتی
                PoCoItem.PersonnelOverTimeHours = item.PersonnelOverTimeHours;//ساعت اضافه کاری
                PoCoItem.PersonnelNightWorkDay = item.PersonnelNightWorkDay;//روز شب کاری
                PoCoItem.PersonnelNightWorkHours = item.PersonnelNightWorkHours;//ساعت شب کاری
                PoCoItem.PersonnelWorkingHolidays = item.PersonnelWorkingHolidays;//ساعات تعطیل کاری
                PoCoItem.PersonnelWorkingHolidaysDay = item.PersonnelWorkingHolidaysDay;//روز تعطیل کاری
                PoCoItem.PaylessDay = item.PaylessDay;//روز بدون حقوق
                PoCoItem.CurrentServiceLocationId = Convert.ToInt64(item.CurrentServiceLocationId);//کد مرکز هزینه
                PoCoItem.OrganId = Convert.ToInt64(organization.CustomCode);//کد سازمان

                //عادی=صفر ، رییس اداره=یک، معاون =دو ،سه =
                if (string.IsNullOrEmpty(item.PostType))
                    PoCoItem.PostType = 0;
                else if (item.PostType.Contains("رییس اداره"))
                    PoCoItem.PostType = 1;
                else if (item.PostType.Contains("معاونت"))
                    PoCoItem.PostType = 2;
                else
                    PoCoItem.PostType = 0;

                PoCoList.Add(PoCoItem);
            }
            return PoCoList;
        }

        #region CRUD

        /// <summary>
        /// عملیات ذخیره سرانه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل سرانه</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertOverTimePersonDetail(OverTimePersonDetail obj)
        {
            obj.ModifiedDate = DateTime.Now;
            obj.ModifiedBy = BUser.CurrentUser.Person;
            //TODO//آیا هنگام درج اتوماتیک بحث زمان تایید باید بررسی شودCustomeValidate(approvalType);
            return base.SaveChanges(obj, UIActionType.ADD);
        }

        /// <summary>
        /// عملیات ویرایش سرانه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل سرانه</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateOverTimePersonDetail(decimal ObjectId, OverTimeProxy objViewModel, ApprovalScheduleType approvalType, decimal managerPersonId)
        {
            OverTimePersonDetail obj = this.GetByID(ObjectId);

            objViewModel.OldValueMaxOverTime = obj.MaxOverTime;
            objViewModel.OldValueMaxHoliday = obj.MaxHoliday;
            objViewModel.OldValueMaxNightly = obj.MaxNightly;

            if (obj.Person.PersonTASpec.OverTimeWork)
                obj.MaxOverTime = objViewModel.MaxOverTime;

            if (obj.Person.PersonTASpec.NightWork)
                obj.MaxNightly = objViewModel.MaxNightly;

            if (obj.Person.PersonTASpec.HolidayWork)
                obj.MaxHoliday = objViewModel.MaxHoliday;
            //--------------------------------------------------------------------------------------------------------
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = BUser.CurrentUser.Person;

                    //Cehck Validation 
                    CustomeValidate(obj, approvalType, managerPersonId);

                    var result = base.SaveChanges(obj, UIActionType.EDIT);

                    //Insert Logs after SaveChanges
                    string clientIPAddress = "";
                    if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                    {
                        clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }

                    //----------------------------------------------------------------------------------------------------------------------------------------------------------
                    if (objViewModel.OldValueMaxOverTime != obj.MaxOverTime)
                    {
                        OverTimePersonDetailHistory historyMaxOverTime = new OverTimePersonDetailHistory() { ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, NewValue = obj.MaxOverTime.ToString(), OldValue = objViewModel.OldValueMaxOverTime.ToString().Replace("/", "."), Person = obj.Person, RefrenceId = obj.ID, Title = "اضافه کاری تشویقی", Period = obj.OverTime.Date, IPAddress = clientIPAddress };
                        overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(historyMaxOverTime);
                    }
                    if (objViewModel.OldValueMaxHoliday != obj.MaxHoliday)
                    {
                        OverTimePersonDetailHistory historyMaxHoliday = new OverTimePersonDetailHistory() { ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, NewValue = obj.MaxHoliday.ToString(), OldValue = objViewModel.OldValueMaxHoliday.ToString().Replace("/", "."), Person = obj.Person, RefrenceId = obj.ID, Title = "تعطیل کاری تشویقی", Period = obj.OverTime.Date, IPAddress = clientIPAddress };
                        overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(historyMaxHoliday);
                    }
                    if (objViewModel.OldValueMaxNightly != obj.MaxNightly)
                    {
                        OverTimePersonDetailHistory historyMaxNightly = new OverTimePersonDetailHistory() { ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, NewValue = obj.MaxNightly.ToString(), OldValue = objViewModel.OldValueMaxNightly.ToString().Replace("/", "."), Person = obj.Person, RefrenceId = obj.ID, Title = "شب کاری تشویقی", Period = obj.OverTime.Date, IPAddress = clientIPAddress };
                        overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(historyMaxNightly);
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------------------------------
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return result;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateOverTimePersonDetailByAdministrative(decimal ObjectId, OverTimePersonDetailProxy objViewModel, ApprovalScheduleType approvalType, decimal PersonId)
        {
            OverTimePersonDetail obj = this.GetByID(ObjectId);

            //Check Archive Validation
            CustomeArchiveValidate(objViewModel, obj.OverTime.Date);
            //--------------------------------------------------------------------------------------------------------

            objViewModel.OldValueMaxOverTime = obj.MaxOverTime;
            objViewModel.OldValueMaxHoliday = obj.MaxHoliday;
            objViewModel.OldValueMaxNightly = obj.MaxNightly;

            if (obj.Person.PersonTASpec.OverTimeWork)
                obj.MaxOverTime = objViewModel.MaxOverTime;

            if (obj.Person.PersonTASpec.NightWork)
                obj.MaxNightly = objViewModel.MaxNightWork;

            if (obj.Person.PersonTASpec.HolidayWork)
                obj.MaxHoliday = objViewModel.MaxHolidayWork;
            //--------------------------------------------------------------------------------------------------------
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    //آبجکت اضافه اضافه کار تشویقی را ویرایش شود--------------------------------------------------------------------------------------------------------
                    obj.ModifiedDate = DateTime.Now;
                    obj.ModifiedBy = BUser.CurrentUser.Person;

                    //Check Validation
                    CustomeValidateAdministrative(obj, approvalType, PersonId);

                    var result = base.SaveChanges(obj, UIActionType.EDIT);

                    //Insert Logs after SaveChanges
                    string clientIPAddress = "";
                    if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                    {
                        clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }

                    //----------------------------------------------------------------------------------------------------------------------------------------------------------
                    //آبجت ویرایش انجام محاسبات ویرایش شود----------------------------------------------------------------------------------------------------------------------------------------------------------
                    if (objViewModel.IsArchiveEnable)
                    {
                        PersianDateTime pDate = new PersianDateTime(obj.OverTime.Date);
                        ArchiveCalcValuesProxy archiveCalcValuesProxy = new ArchiveCalcValuesProxy();
                        archiveCalcValuesProxy.PersonId = PersonId;
                        archiveCalcValuesProxy.P1 = objViewModel.P1;
                        archiveCalcValuesProxy.P2 = objViewModel.P2;
                        archiveCalcValuesProxy.P3 = objViewModel.P3;
                        archiveCalcValuesProxy.P4 = objViewModel.P4;
                        archiveCalcValuesProxy.P5 = objViewModel.P5;
                        archiveCalcValuesProxy.P6 = objViewModel.P6;
                        archiveCalcValuesProxy.P7 = objViewModel.P7;
                        archiveCalcValuesProxy.P8 = objViewModel.P8;
                        archiveCalcValuesProxy.P9 = objViewModel.P9;
                        archiveCalcValuesProxy.P10 = objViewModel.P10;
                        archiveCalcValuesProxy.P11 = objViewModel.P11;
                        archiveCalcValuesProxy.P12 = objViewModel.P12;
                        archiveCalcValuesProxy = archiveCalculatorBusiness.SetArchiveValues(pDate.Year, pDate.Month, obj.Person.ID, archiveCalcValuesProxy);

                        if (objViewModel.P1 != objViewModel.P1Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "ناهار", NewValue = objViewModel.P1.ToString(), OldValue = objViewModel.P1Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P2 != objViewModel.P2Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "تعطیل ناهار", NewValue = objViewModel.P2.ToString(), OldValue = objViewModel.P2Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P3 != objViewModel.P3Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "اضافه کاری", NewValue = objViewModel.P3.ToString(), OldValue = objViewModel.P3Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P4 != objViewModel.P4Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "تعطیل کاری", NewValue = objViewModel.P4.ToString(), OldValue = objViewModel.P4Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P5 != objViewModel.P5Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "شب کاری", NewValue = objViewModel.P5.ToString(), OldValue = objViewModel.P5Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P6 != objViewModel.P6Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "مرخصی بی حقوق", NewValue = objViewModel.P6.ToString(), OldValue = objViewModel.P6Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P7 != objViewModel.P7Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "مرخصی استحقاقی", NewValue = objViewModel.P7.ToString(), OldValue = objViewModel.P7Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P8 != objViewModel.P8Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "مرخصی استعلاجی", NewValue = objViewModel.P8.ToString(), OldValue = objViewModel.P8Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P9 != objViewModel.P9Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "غیبت", NewValue = objViewModel.P9.ToString(), OldValue = objViewModel.P9Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P10 != objViewModel.P10Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "کسر کار", NewValue = objViewModel.P10.ToString(), OldValue = objViewModel.P10Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P11 != objViewModel.P11Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "کارکرد", NewValue = objViewModel.P11.ToString(), OldValue = objViewModel.P11Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });

                        if (objViewModel.P12 != objViewModel.P12Old)
                            overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(new OverTimePersonDetailHistory() { Title = "کارکرد ساعتی", NewValue = objViewModel.P12.ToString(), OldValue = objViewModel.P12Old.ToString(), ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, Person = obj.Person, Period = obj.OverTime.Date, IPAddress = clientIPAddress });
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------------------------------
                    //آبجت مربوط به لاگ تغییرات اضافه کار تشویقی ذخیره گردد----------------------------------------------------------------------------------------------------------------------------------------------------------
                    if (objViewModel.OldValueMaxOverTime != obj.MaxOverTime)
                    {
                        OverTimePersonDetailHistory historyMaxOverTime = new OverTimePersonDetailHistory() { ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, NewValue = obj.MaxOverTime.ToString(), OldValue = objViewModel.OldValueMaxOverTime.ToString().Replace("/", "."), Person = obj.Person, RefrenceId = obj.ID, Title = "اضافه کاری تشویقی", Period = obj.OverTime.Date, IPAddress = clientIPAddress };
                        overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(historyMaxOverTime);
                    }
                    if (objViewModel.OldValueMaxHoliday != obj.MaxHoliday)
                    {
                        OverTimePersonDetailHistory historyMaxHoliday = new OverTimePersonDetailHistory() { ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, NewValue = obj.MaxHoliday.ToString(), OldValue = objViewModel.OldValueMaxHoliday.ToString().Replace("/", "."), Person = obj.Person, RefrenceId = obj.ID, Title = "تعطیل کاری تشویقی", Period = obj.OverTime.Date, IPAddress = clientIPAddress };
                        overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(historyMaxHoliday);
                    }
                    if (objViewModel.OldValueMaxNightly != obj.MaxNightly)
                    {
                        OverTimePersonDetailHistory historyMaxNightly = new OverTimePersonDetailHistory() { ModifiedBy = BUser.CurrentUser.Person, ModifiedDate = DateTime.Now, NewValue = obj.MaxNightly.ToString(), OldValue = objViewModel.OldValueMaxNightly.ToString().Replace("/", "."), Person = obj.Person, RefrenceId = obj.ID, Title = "شب کاری تشویقی", Period = obj.OverTime.Date, IPAddress = clientIPAddress };
                        overTimePersonDetailHistoryBusiness.InsertOverTimePersonDetailHistory(historyMaxNightly);
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------------------------------
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return result;
                }
                catch (Exception ex)
                {
                    LogException(ex);
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// عملیات حذف سرانه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل سرانه</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOverTimePersonDetail(OverTimePersonDetail obj)
        {
            return base.SaveChanges(obj, UIActionType.DELETE);
        }

        #endregion

        #region Validation

        /// <summary>
        ///چک می شود که مهلت زمان تایید به پایان رسیده است یا خیر
        /// </summary>
        /// <param name="approvalType"></param>
        /// <returns></returns>
        public bool CheckIsExpireTime(ApprovalScheduleType approvalType, Person person)
        {
            var currentDate = DateTime.Now;
            UIValidationExceptions exception = new UIValidationExceptions();
            //---------------------------------------------------------------------
            //ابتدا چک کن مرکز هزینه برای پرسنل تعریف شده است یا خیر
            if (person.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterRequired, "مرکز هزینه به پرسنل اختصاص داده نشده است", ExceptionSrc));
                throw exception;
            }
            //---------------------------------------------------------------------
            //آبجت زمانبندی را برای پرسنل بر اساس مرکزهزینه آن واکشی کن
            ApprovalAttendanceSchedule approvalSchedule = ApprovalAttendanceScheduleBusiness.GetByApprovalScheduleTypeAndCostCenter(approvalType, person.ID);
            if (approvalSchedule == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleCostCenterRequired, "مرکز هزینه در بودجه بندی مشخص نشده است", ExceptionSrc));
                throw exception;
            }
            //---------------------------------------------------------------------
            //چک کن این پرسنل در لیست استثناء زمانبندی واکشی شده است یا خیر ؟
            var exceptionList = ApprovalAttendanceScheduleExceptionBusiness.GetListProxyByApprovalAttendanceScheduleID(approvalSchedule.ID, person.ID);
            if (exceptionList != null || exceptionList.Count > 0)
            {
                if (exceptionList.Where(c => c.DateFrom.Date <= currentDate && c.DateTo.Date >= currentDate).Any())
                {
                    return false;
                }
            }
            //---------------------------------------------------------------------
            if (approvalSchedule.DateFrom.Date <= currentDate.Date && approvalSchedule.DateTo.Date >= currentDate.Date)
                return false;
            else
                return true;
        }

        public bool CheckPersonApproved(DateTime date, decimal PersonId)
        {
            return !personApprovalAttendanceBusiness.CheckIsDuplicate(date, PersonId);
        }

        protected void CustomeValidate(OverTimePersonDetail obj, ApprovalScheduleType approvalType, decimal managerPersonId)
        {
            var managerPerson = bPerson.GetByID(managerPersonId);

            UIValidationExceptions exception = new UIValidationExceptions();
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            if (managerPerson.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterRequired, string.Format("مرکز هزینه {0} اختصاص داده نشده است", managerPerson.Name), ExceptionSrc));
                throw exception;
                //در صورتی که مهلت به اتمام رسید دلیلی ندارد ادامه بررسی های انجام شود
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            //0-چک کن مهلت تایید کارکرد رد شده است یا خیر


            if (CheckIsExpireTime(approvalType, managerPerson))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalExpire, "مهلت تغییر اضافه کار تشویقی به اتمام رسیده است", ExceptionSrc));
                throw exception;
                //در صورتی که مهلت به اتمام رسید دلیلی ندارد ادامه بررسی های انجام شود
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            //0-چک کن پرسنل کارکرد خود را تایید کرده است یا خیر
            if (CheckPersonApproved(obj.OverTime.Date, obj.Person.ID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalExpire, string.Format("امکان ثبت به دلیل عدم تایید کارکرد {0} وجود ندارد", obj.Person.Name), ExceptionSrc));
                throw exception;
                //در صورتی که مهلت به اتمام رسید دلیلی ندارد ادامه بررسی های انجام شود
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------------

            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            int month = obj.OverTime.Date.Month;
            //تعداد روزهای تعطیل ماه مربوطه را واکشی کن
            var calendarList = bCalendar.GetCalendarListByDateRange(obj.OverTime.Date, obj.OverTime.Date.AddMonths(1), "6");
            int MonthHolidayCount = calendarList != null ? calendarList.Count() : 0;
            //--------------------------------------------------------------------------------------------------------------------------------------------------------
            //1-جمع سرانه پیش فرض کلیه پرسنل تحت مدیریت را واکشی کن
            //سرانه معاونت ای را که شخص در مسیر زیر شاخه آن مدیریت است را بر می گرداند
            OverTimeDetail overtimeDetail = bOverTimeDetail.GetDetailByPersonForValidation(obj.Person.ID, obj.OverTime.Date);
            //GET OvertimeDetail from database by overtimeDetail.Id for navigation Property Department
            overtimeDetail = bOverTimeDetail.GetByID(overtimeDetail.ID);
            decimal departmentId = overtimeDetail.Department.ID;
            //لیست پرسنل تحت مدیریت
            //IList<UnderManagementPerson> underManagementPersons = bWorkedTime.GetJustUnderManagmentMainFlowByDepartment(month, departmentId, managerPersonId, 0, 9999999, GridOrderFields.NONE, GridOrderFieldType.asc);
            IList<UnderManagementPerson> underManagementPersons = managerRepository.GetUnderManagmentByDepartment_JustMainManagers(GridSearchFields.Complex, managerPersonId, 0, "", "", month, month > 0 ? 0 : Utility.ToDateRangeIndex(DateTime.Now, sysLanguageResource), DateTime.Now.ToString("yyyy/MM/dd"), GridOrderFields.NONE, GridOrderFieldType.asc, 0, 9999999);
            //تعداد کل پرسنل تحت مدیریت را بر می گرداند
            int totalCount = underManagementPersons.ToList().Count();
            IList<decimal> PersonIds = underManagementPersons.Select(c => c.PersonId).ToList();
            IList<PersonTASpec> PersonSpecs = bPerson.GetTASpecByPersonIdList(PersonIds);
            //تعداد کل پرسنل را در مقدار پیش فرض ضرب کن
            decimal MaxOverTime = overtimeDetail.MaxOverTime * totalCount;
            decimal MaxOverTime2 = overtimeDetail.MaxOverTime * PersonSpecs.Where(c => c.OverTimeWork == true).Count();
            decimal MaxNightly = overtimeDetail.MaxNightly * PersonSpecs.Where(c => c.NightWork == true).Count();
            decimal MaxHoliday = overtimeDetail.MaxHoliday * PersonSpecs.Where(c => c.HolidayWork == true).Count() * MonthHolidayCount / 100;
            //-----------------------------------------------------------------------------------------------------------------------------------------
            //2-جمع سرانه اختصاص داده شده پرسنل تحت مدیریت را واکشی کن
            //لیست کلید پرسنل را بگیر
            IList<decimal> personIds = underManagementPersons.Select(c => c.PersonId).ToList();
            //لیست سرانه ذخیره شده پرسنل آن معاونت را واکشی کن
            IList<OverTimePersonDetail> overTimePersonDetails = this.GetByPersonList(obj.OverTime, personIds);
            decimal AssignedOverTime = overTimePersonDetails.Select(c => c.MaxOverTime).Sum();
            decimal AssignedHoliday = overTimePersonDetails.Select(c => c.MaxHoliday).Sum();
            decimal AssignedNightly = overTimePersonDetails.Select(c => c.MaxNightly).Sum();
            //-----------------------------------------------------------------------------------------------------------------------------------------
            //3-سرانه اختصاص داده شده به خود شخص را نیز واکشی کن
            IList<decimal> personId = new List<decimal>();
            personId.Add(obj.Person.ID);
            OverTimePersonDetail overTimePersonDetail = this.GetByPersonList(obj.OverTime, personId).FirstOrDefault();
            decimal personOverTime = overTimePersonDetail.MaxOverTime;
            decimal personHoliday = overTimePersonDetail.MaxHoliday;
            decimal personNightly = overTimePersonDetail.MaxNightly;
            //-----------------------------------------------------------------------------------------------------------------------------------------
            if (obj.MaxOverTime != 0 && ((personOverTime - obj.MaxOverTime + AssignedOverTime) > MaxOverTime))
            {
                //Add Exception
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxOverTimeOverFlow, "مجموع اضافه کاری پیشنهادی مازاد سرانه معاونت می باشد", ExceptionSrc));
            }
            if (obj.MaxHoliday != 0 && ((personHoliday - obj.MaxHoliday + AssignedHoliday) > MaxHoliday))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxHolidayOverFlow, "مجموع تعطیلی کاری پیشنهادی مازاد سرانه معاونت می باشد", ExceptionSrc));
            }
            if (obj.MaxNightly != 0 && ((personNightly - obj.MaxNightly + AssignedNightly) > MaxOverTime))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxNightlyOverFlow, "مجموع شب کاری پیشنهادی مازاد سرانه معاونت می باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected void CustomeValidateAdministrative(OverTimePersonDetail obj, ApprovalScheduleType approvalType, decimal managerPersonId)
        {
            var managerPerson = bPerson.GetByID(managerPersonId);


            UIValidationExceptions exception = new UIValidationExceptions();
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            if (managerPerson.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterRequired, string.Format("مرکز هزینه {0} اختصاص داده نشده است", managerPerson.Name), ExceptionSrc));
                throw exception;
                //در صورتی که مهلت به اتمام رسید دلیلی ندارد ادامه بررسی های انجام شود
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            //0-چک کن مهلت تایید کارکرد رد شده است یا خیر

            if (CheckIsExpireTime(approvalType, managerPerson))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalExpire, "مهلت تغییر اضافه کار تشویقی به اتمام رسیده است", ExceptionSrc));
                throw exception;
                //در صورتی که مهلت به اتمام رسید دلیلی ندارد ادامه بررسی های انجام شود
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            //تعداد روزهای تعطیل ماه مربوطه را واکشی کن
            var calendarList = bCalendar.GetCalendarListByDateRange(obj.OverTime.Date, obj.OverTime.Date.AddMonths(1), "6");
            int MonthHolidayCount = calendarList != null ? calendarList.Count() : 0;
            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            int month = obj.OverTime.Date.Month;

            decimal OrgMaxOverTime = 0, OrgMaxNightly = 0, OrgMaxHoliday = 0;
            decimal OrgAssignedOverTime = 0, OrgAssignedHoliday = 0, OrgAssignedNightly = 0;

            //1-جمع سرانه پیش فرض سازمان را واکشی کن
            //سرانه همه معاونت ها را که با شخص در یک سازمان هستند بر می گرداند
            IList<OverTimeDetail> overtimeDetails = bOverTimeDetail.GetOrganizationDetailByPerson(obj.Person.ID, obj.OverTime.Date);

            foreach (var item in overtimeDetails)
            {
                //1-جمع سرانه پیش فرض کلیه پرسنل مدیریت را واکشی کن--------------------------------------------------------------------------
                OverTimeDetail overtimeDetail = bOverTimeDetail.GetByID(item.ID);
                decimal departmentId = overtimeDetail.Department.ID;

                //لیست پرسنل آن معاونت را بازیابی کن
                IList<Person> persons = bPerson.GetPersonsByDepartmentId(departmentId);
                IList<decimal> PersonIds = persons.Select(c => c.ID).ToList();
                IList<PersonTASpec> PersonSpecs = bPerson.GetTASpecByPersonIdList(PersonIds);

                //تعداد کل پرسنل را در مقدار پیش فرض ضرب کن
                //OrgMaxOverTime += overtimeDetail.MaxOverTime * PersonSpecs.Where(c => c.HasPeyment == true).ToList().Count();
                //OrgMaxNightly += overtimeDetail.MaxNightly * PersonSpecs.Where(c => c.NightWork == true).ToList().Count();
                //OrgMaxHoliday += overtimeDetail.MaxHoliday * PersonSpecs.Where(c => c.HolidayWork == true).ToList().Count() * MonthHolidayCount / 100;
                OrgMaxOverTime += 120 * PersonSpecs.Where(c => c.OverTimeWork == true).ToList().Count();
                OrgMaxNightly += 25 * PersonSpecs.Where(c => c.NightWork == true).ToList().Count() * (month <= 6 ? 31 : 30 - MonthHolidayCount) / 100;
                OrgMaxHoliday += 50 * PersonSpecs.Where(c => c.HolidayWork == true).ToList().Count() * MonthHolidayCount / 100;

                //2-جمع سرانه اختصاص داده شده پرسنل سازمان را واکشی کن--------------------------------------------------------------------------
                //لیست سرانه ذخیره شده پرسنل معاونت را واکشی کن
                IList<OverTimePersonDetail> overTimePersonDetails = this.GetByPersonList(obj.OverTime, PersonIds);

                OrgAssignedOverTime += overTimePersonDetails.Select(c => c.MaxOverTime).Sum();
                OrgAssignedHoliday += overTimePersonDetails.Select(c => c.MaxHoliday).Sum();
                OrgAssignedNightly += overTimePersonDetails.Select(c => c.MaxNightly).Sum();
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------
            //3-سرانه اختصاص داده شده به خود شخص را نیز واکشی کن
            IList<decimal> personId = new List<decimal>();
            personId.Add(obj.Person.ID);
            OverTimePersonDetail overTimePersonDetail = this.GetByPersonList(obj.OverTime, personId).FirstOrDefault();
            decimal personOverTime = overTimePersonDetail.MaxOverTime;
            decimal personHoliday = overTimePersonDetail.MaxHoliday;
            decimal personNightly = overTimePersonDetail.MaxNightly;
            //-----------------------------------------------------------------------------------------------------------------------------------------
            if ((personOverTime - obj.MaxOverTime + OrgAssignedOverTime) > OrgMaxOverTime)
            {
                //Add Exception
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxOverTimeOverFlow, "مجموع اضافه کاری پیشنهادی مازاد سرانه سازمان می باشد", ExceptionSrc));
            }
            if ((personHoliday - obj.MaxHoliday + OrgAssignedHoliday) > OrgMaxHoliday)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxHolidayOverFlow, "مجموع تعطیلی کاری پیشنهادی مازاد سرانه سازمان می باشد", ExceptionSrc));
            }
            if ((personNightly - obj.MaxNightly + OrgAssignedNightly) > OrgMaxOverTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxNightlyOverFlow, "مجموع شب کاری پیشنهادی مازاد سرانه سازمان می باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void InsertValidate(OverTimePersonDetail obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(OverTimePersonDetail obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            Person person = bPerson.GetByID(obj.Person.ID);
            var personParam = person.PersonTASpec.GetParamValue(person.ID, "maxOvertimeBudget", DateTime.Now);
            int maxOverTime = personParam != null ? Utility.ToInteger(personParam.Value) : 0;

            if (maxOverTime != 0 && obj.MaxOverTime > maxOverTime)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxOverTimeOverFlow, "اضافه کاری پیشنهادی بیشتر از سقف " + maxOverTime + " ساعت مجاز نمی باشد", ExceptionSrc));
            }
            else if (maxOverTime == 0 && obj.MaxOverTime > 150)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxOverTimeOverFlow, "اضافه کاری پیشنهادی بیشتر از سقف 150 ساعت مجاز نمی باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(OverTimePersonDetail obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected void CustomeArchiveValidate(OverTimePersonDetailProxy obj, DateTime thisDate)
        {
            UIValidationExceptions exceptions = new UIValidationExceptions();
            if (obj.IsArchiveEnable)
            {
                PersianDateTime pDT = new PersianDateTime(thisDate);
                System.Globalization.PersianCalendar pCals = new System.Globalization.PersianCalendar();
                var DaysInMonth = pCals.GetDaysInMonth(pDT.Year, pDT.Month);

                if (Convert.ToInt32(obj.P6) + Convert.ToInt32(obj.P11) + Convert.ToInt32(obj.P9) > DaysInMonth)
                {
                    exceptions.Add(new ValidationException(ExceptionResourceKeys.DaysInMonthValidate, "جمع کارکرد, مرخصی بی حقوق, غیبت در ماه معتبر نمی باشد", ExceptionSrc));
                }

            }
            if (exceptions.Count > 0)
            {
                throw exceptions;
            }
        }

        #endregion
    }
}
