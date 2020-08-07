using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Temp;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Repository.OverTimeFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.OverTimeFlow;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.OverTimeFlow
{
    /// <summary>
    /// کلاس بیزینس مربوط به سرانه اضافه کار تشویقی, شب کاری تشویقی, تعطیل کار تشویقی
    /// </summary>
    public class BOverTimeDetail : BaseBusiness<OverTimeDetail>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private const string ExceptionSrc = "GTS.Clock.Business.OverTimeFlow.BOverTimeDetail";
        IDataAccess accessPort = new BUser();
        private BTemp bTemp = new BTemp();
        private BDepartment bDepartment = new BDepartment();
        private BPerson bPerson = new BPerson();
        private EntityRepository<OverTime> overTimeRepository = new EntityRepository<OverTime>();
        private OverTimeDetailRepository overTimeDetailRepository = new OverTimeDetailRepository(false);
        private BCalendarType bCalendar = new BCalendarType();
        int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        private IList<Person> personList;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public BOverTimeDetail()
        {

        }

        /// <summary>
        /// کلیه سرانه های اضافه کار تشویقی, شب کاری تشویقی, تعطیل کار تشویقی را بر می گرداند
        /// بر اساس دسترسی کاربر به دپارتمان 
        /// </summary>
        /// <returns>لیست پروکسی سرانه</returns>
        public override IList<OverTimeDetail> GetAll()
        {
            OverTime overTimeAlias = null;
            OverTimeDetail overTimeDetailAlias = null;
            Department departmentAlias = null;

            IList<OverTimeDetail> list = new List<OverTimeDetail>();
            IList<decimal> accessableIDs = accessPort.GetAccessibleDeparments();

            if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                list = NHSession.QueryOver(() => overTimeDetailAlias)
                .JoinAlias(() => overTimeDetailAlias.OverTime, () => overTimeAlias)
                .JoinAlias(() => overTimeDetailAlias.Department, () => departmentAlias)
                .Where(() => departmentAlias.ID.IsIn(accessableIDs.ToList()) && overTimeAlias.IsActive == true)
                .List<OverTimeDetail>();
            }
            else
            {
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                list = NHSession.QueryOver(() => overTimeDetailAlias)
                      .JoinAlias(() => overTimeDetailAlias.OverTime, () => overTimeAlias)
                      .JoinAlias(() => overTimeDetailAlias.Department, () => departmentAlias)
                      .JoinAlias(() => departmentAlias.TempList, () => tempAlias)
                      .Where(() => tempAlias.OperationGUID == operationGUID && overTimeAlias.IsActive == true)
                      .List<OverTimeDetail>();

                this.bTemp.DeleteTempList(operationGUID);
            }
            return list;
        }

        /// <summary>
        /// آبجکت دوره بودجه رو بر اساس تاریخ بر می گرداند
        /// </summary>
        /// <param name="date">تاریخ</param>
        /// <returns>آبجکت دوره بودجه</returns>
        public OverTime GetPeriodByDate(DateTime date)
        {
            OverTime overTimeAlias = null;
            OverTime item = NHSession.QueryOver(() => overTimeAlias)
                .Where(() => overTimeAlias.Date == date)
                .List<OverTime>().SingleOrDefault();
            return item;
        }

        /// <summary>
        /// آبجکت دوره بودجه رو بر اساس کلید اصلی بر می گرداند
        /// </summary>
        /// <param name="Id">کلید اصلی</param>
        /// <returns>آبجکت دوره بودجه</returns>
        public OverTime GetPeriodById(decimal Id)
        {
            OverTime overTimeAlias = null;
            OverTime item = NHSession.QueryOver(() => overTimeAlias)
                .Where(() => overTimeAlias.ID == Id)
                .List<OverTime>().SingleOrDefault();
            return item;
        }

        /// <summary>
        /// آبجکت دوره بودجه فعال رو بر اساس تاریخ بر می گرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public OverTime GetActivePeriodByDate(DateTime date)
        {
            OverTime overTimeAlias = null;
            OverTime item = NHSession.QueryOver(() => overTimeAlias)
                .Where(() => overTimeAlias.Date == date && overTimeAlias.IsActive == true)
                .List<OverTime>().SingleOrDefault();
            return item;
        }


        /// <summary>
        /// کلیه سرانه های اضافه کار تشویقی, شب کاری تشویقی, تعطیل کار تشویقی را بر می گرداند
        /// </summary>
        /// <returns>لیست پروکسی سرانه</returns>
        public IList<OverTimeDetail> GetAllWithoutAthoriZe()
        {
            IList<OverTimeDetail> list = base.GetAll();
            return list;
        }

        /// <summary>
        /// سرانه های اضافه کار تشویقی, شب کاری تشویقی, تعطیل کار تشویقی مربوط به ماه/سال مشخص به صورت صفحه بندی شده بر می گرداند
        /// بر اساس دسترسی کاربر به دپارتمان
        /// </summary>
        /// <param name="parentDepartmentId">آی دی ارگان/سازمان جهت جستجو</param>
        /// <param name="departmentName">نام معاونت جهت جستجو</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <param name="pageIndex">ایندکس صفحه</param>
        /// <param name="pageSize">اندازه صفحه</param>
        /// <param name="count">تعداد کل رکوردها بدون صفحه بندی</param>
        /// <returns>لیست پروکسی سرانه</returns>
        public IList<OverTimeProxy> GetDetails(decimal parentDepartmentId, string departmentName, int year, int month, int pageIndex, int pageSize, out int count)
        {
            DateTime Date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                Date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                Date = new DateTime(year, month, 1);
            }
            //-----------------------------------------------------------------
            //چک کن اگه موجودیت والد برای این ماه/سال در دیتابیس وجود نداشت, یه موجودیت والد ذخیره کن
            this.CheckOverTimeExists(Date);
            //-----------------------------------------------------------------

            OverTime overTimeAlias = null;
            OverTimeDetail overTimeDetailAlias = null;
            Department departmentAlias = null;
            IQueryOver<OverTimeDetail, OverTimeDetail> searchQuery;
            IList<decimal> accessableIDs = accessPort.GetAccessibleDeparments();

            if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
            {
                searchQuery = NHSession.QueryOver(() => overTimeDetailAlias)
                .JoinAlias(() => overTimeDetailAlias.OverTime, () => overTimeAlias)
                .JoinAlias(() => overTimeDetailAlias.Department, () => departmentAlias)
                    .Where(() =>
                        departmentAlias.ID.IsIn(accessableIDs.ToList())
                        && overTimeAlias.Date == Date
                        && departmentAlias.ParentPath.IsLike("," + parentDepartmentId + ",", MatchMode.Anywhere)
                        && departmentAlias.DepartmentType == DepartmentType.Assistance
                        && overTimeAlias.IsActive == true);
            }
            else
            {
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                string operationGUID = this.bTemp.InsertTempList(accessableIDs);
                searchQuery = NHSession.QueryOver(() => overTimeDetailAlias)
                      .JoinAlias(() => overTimeDetailAlias.OverTime, () => overTimeAlias)
                      .JoinAlias(() => overTimeDetailAlias.Department, () => departmentAlias)
                      .JoinAlias(() => departmentAlias.TempList, () => tempAlias)
                      .Where(() =>
                            tempAlias.OperationGUID == operationGUID
                            && overTimeAlias.Date == Date
                            && departmentAlias.ParentPath.IsLike("," + parentDepartmentId + ",", MatchMode.Anywhere)
                            && departmentAlias.DepartmentType == DepartmentType.Assistance
                            && overTimeAlias.IsActive == true);

                this.bTemp.DeleteTempList(operationGUID);
            }

            if (!Utility.IsEmpty(departmentName))
                searchQuery.Where(() => departmentAlias.Name.IsLike(departmentName, MatchMode.Anywhere));

            count = searchQuery.RowCount();
            IList<OverTimeDetail> list = new List<OverTimeDetail>();
            list = searchQuery.Skip(pageIndex * pageSize).Take(pageSize).List<OverTimeDetail>();

            personList = bPerson.GetPersonsInfoForOverTime();

            IList<OverTimeProxy> proxyList = new List<OverTimeProxy>();
            var calendarList = bCalendar.GetCalendarListByDateRange(Date, Date.AddMonths(1), "6");
            foreach (var item in list)
            {
                OverTimeProxy proxy = ConvertToProxy(item);
                proxy.MonthHolidayCount = calendarList != null ? calendarList.Count() : 0;
                proxyList.Add(proxy);
            }

            return proxyList;
        }

        /// <summary>
        /// سرانه معاونتی را در تاریخ مشخص که پرسنل در مسیر زیر شاخه آن معاونت است را بر می گرداند
        /// </summary>
        /// <param name="person">پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>سرانه</returns>
        public OverTimeDetail GetDetailByPerson(decimal personId, DateTime date)
        {
            string parentPath = "nothing";
            var dep = bDepartment.GetDepartmentByPersonId(personId);
            if (dep != null)
                parentPath = dep.ParentPath;

            //سرانه معاونتی را که شخص در مسیر زیر شاخه آن مدیریت است را بر می گرداند
            IList<OverTimeDetail> list = overTimeDetailRepository.GetDetailByPersonDepartmentparentPath(date, parentPath);
            return list.FirstOrDefault();
        }

        /// <summary>
        /// سرانه معاونتی را در تاریخ مشخص که پرسنل در مسیر زیر شاخه آن معاونت است را بر می گرداند
        /// </summary>
        /// <param name="person">پرسنل</param>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>سرانه</returns>
        public OverTimeDetail GetDetailByPersonForValidation(decimal personId, DateTime date)
        {
            string parentPath = "nothing";
            var dep = bDepartment.GetDepartmentByPersonId(personId);
            if (dep != null)
                parentPath = dep.ParentPath + dep.ID.ToString() + ",";

            //سرانه معاونتی را که شخص در مسیر زیر شاخه آن مدیریت است را بر می گرداند
            IList<OverTimeDetail> list = overTimeDetailRepository.GetDetailByPersonDepartment(date, parentPath);
            return list.FirstOrDefault();
        }

        /// <summary>
        /// لیست بودجه همه معاونت هایی که با شخص در یک سازمان می باشند را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید شخص</param>
        /// <param name="date">تاریخ سرانه</param>
        /// <returns>لیست بودجه معاونت ها</returns>
        public IList<OverTimeDetail> GetOrganizationDetailByPerson(decimal personId, DateTime date)
        {
            var department = bDepartment.GetOraganizationByPersonId(personId);

            //سرانه همه معاونت هایی را که شخص در مسیر زیر شاخه سازمان مربوطه است را بر می گرداند
            IList<OverTimeDetail> list = overTimeDetailRepository.GetDetailsByOrganizationPath(date, department.ID);
            return list;
        }

        /// <summary>
        /// لیست بودجه همه معاونت هایی که با بخش در یک سازمان می باشند را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید بخش</param>
        /// <param name="date">تاریخ سرانه</param>
        /// <returns>لیست بودجه معاونت ها</returns>
        public IList<OverTimeDetail> GetOrganizationDetailByDepartmentId(decimal DepartmentId, DateTime date)
        {
            //سرانه همه معاونت هایی را که شخص در مسیر زیر شاخه سازمان مربوطه است را بر می گرداند
            IList<OverTimeDetail> list = overTimeDetailRepository.GetDetailsByOrganizationPath(date, DepartmentId);
            return list;
        }

        public IList<OverTime> GetActivePeriod()
        {
            return overTimeRepository.GetAll().ToList();
        }

        /// <summary>
        /// چک می کند اگر موجودیت والد برای این ماه/سال در دیتابیس وجود نداشت, یه موجودیت والد ذخیره کن
        /// </summary>
        /// <param name="Date">تاریخ به صورت اولین روز ماه/سال</param>
        private void CheckOverTimeExists(DateTime Date)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    OverTime overTime = overTimeRepository.Find(c => c.Date == Date).FirstOrDefault();
                    if (overTime == null)
                    {
                        overTime = new OverTime();
                        overTime.Date = Date;
                        overTime.IsActive = true;
                        overTime = overTimeRepository.WithoutTransactSave(overTime);
                    }
                    //لیست همه ی معاونت ها از چارت سازمانی را بکش بیرون
                    IList<Department> departmentsList = new List<Department>();
                    departmentsList = this.NHSession.QueryOver<Department>()
                                                    .Where(c => c.DepartmentType == DepartmentType.Assistance)
                                                    .List<Department>();
                    // به ازای هر معاونتی که وجود داره یه سرانه براش در دیتابیس ذخیره کن
                    foreach (var department in departmentsList)
                    {
                        if (!overTimeDetailRepository.Find(c => c.Department.ID == department.ID && c.OverTime.Date == overTime.Date).Any())
                        {
                            var calendarList = bCalendar.GetCalendarListByDateRange(Date, Date.AddMonths(1), "6");
                            OverTimeDetail overTimeDetail = new OverTimeDetail()
                            {
                                ModifiedDate = DateTime.Now,
                                ModifiedBy = BUser.CurrentUser.Person,
                                OverTime = overTime,
                                Department = department,
                                MaxOverTime = 120,
                                MaxNightly = 0,//TODO
                                MaxHoliday = 50//Convert.ToDecimal(calendarList.Count()) / 2
                            };
                            overTimeDetailRepository.WithoutTransactSave(overTimeDetail);
                        }
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
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
        /// مدل سرانه را به پروکسی مربوطه تبدیل می کند
        /// </summary>
        /// <param name="item">مدل</param>
        /// <returns>پروکسی</returns>
        public OverTimeProxy ConvertToProxy(OverTimeDetail item)
        {
            try
            {
                OverTimeProxy proxy = new OverTimeProxy();
                proxy.ID = item.ID;
                proxy.Date = item.OverTime.Date;
                proxy.IsActive = item.OverTime.IsActive;
                proxy.DepartmentName = item.Department.Name;
                if (personList != null)
                {
                    proxy.OverTimePersonCount = personList.Where(c => c.Department.ParentPath != null && (c.Department.ID == item.Department.ID || c.Department.ParentPath.Contains("," + item.Department.ID + ",")) && c.PersonTASpec.OverTimeWork == true).Count();
                    proxy.NightlyPersonCount = personList.Where(c => c.Department.ParentPath != null && (c.Department.ID == item.Department.ID || c.Department.ParentPath.Contains("," + item.Department.ID + ",")) && c.PersonTASpec.NightWork == true).Count();
                    proxy.HolidayPersonCount = personList.Where(c => c.Department.ParentPath != null && (c.Department.ID == item.Department.ID || c.Department.ParentPath.Contains("," + item.Department.ID + ",")) && c.PersonTASpec.HolidayWork == true).Count();
                }
                else
                {
                    proxy.OverTimePersonCount = bPerson.GetAllPersonsCountByDepartmentParentID(item.Department.ID, OverTimePersuasiveType.OverTimeWork);
                    proxy.NightlyPersonCount = bPerson.GetAllPersonsCountByDepartmentParentID(item.Department.ID, OverTimePersuasiveType.NightWork);
                    proxy.HolidayPersonCount = bPerson.GetAllPersonsCountByDepartmentParentID(item.Department.ID, OverTimePersuasiveType.HolidayWork);
                }
                proxy.MaxOverTime = item.MaxOverTime;
                proxy.MaxHoliday = item.MaxHoliday;
                proxy.MaxNightly = item.MaxNightly;

                return proxy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// تایید اضافه کار تشویقی یک دوره(ماه) توسط اداری جهت پرداخت
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>شناسه رکورد</returns>
        public decimal ApproveOverTime(int year, int month)
        {
            DateTime date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            //-----------------------------------------------------------------------
            OverTime overTime = this.GetPeriodByDate(date);
            overTime.ApprovedDate = DateTime.Now;
            overTime.IsApproved = true;
            //این دوره هنوز در دیتابیس تعریف نشده
            if (overTime == null)
                return 0;
            //قبلا تایید شده
            if (overTime.IsApproved == true)
                return 0;

            overTime = overTimeRepository.Update(overTime);
            return overTime.ID;
        }

        /// <summary>
        /// بررسی می کنه آیا دوره تایید نهایی توسط اداری شده است یا خیر
        /// </summary>
        /// <param name="year">سال</param>
        /// <param name="month">ماه</param>
        /// <returns>بلی/خیر</returns>
        public bool IsApprovedOverTime(int year, int month)
        {
            DateTime date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                date = new DateTime(year, month, 1);
            }
            //-----------------------------------------------------------------------
            OverTime overTime = this.GetPeriodByDate(date);

            return overTime.IsApproved;
        }
        #region CRUD

        /// <summary>
        /// عملیات ذخیره سرانه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل سرانه</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertOverTimeDetail(OverTimeDetail obj)
        {
            obj.ModifiedBy = BUser.CurrentUser.Person;
            obj.ModifiedDate = DateTime.Now;
            return base.SaveChanges(obj, UIActionType.ADD);
        }

        /// <summary>
        /// عملیات ویرایش سرانه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل سرانه</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateOverTimeDetail(OverTimeDetail obj)
        {
            obj.ModifiedBy = BUser.CurrentUser.Person;
            obj.ModifiedDate = DateTime.Now;
            return base.SaveChanges(obj, UIActionType.EDIT);
        }

        /// <summary>
        /// عملیات حذف سرانه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل سرانه</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOverTimeDetail(OverTimeDetail obj)
        {
            return base.SaveChanges(obj, UIActionType.DELETE);
        }

        #endregion

        #region Validation

        protected override void InsertValidate(OverTimeDetail obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (obj.OverTime.Date == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DateRequierd, "درج - تاریخ نباید خالی باشد", ExceptionSrc));
            }
            //if (overTime.MaxOverTime == null)
            //{
            //    exception.Add(new ValidationException(ExceptionResourceKeys.MaxOverTimeRequierd, "درج - سرانه اضافه کاری تشویقی نباید خالی باشد", ExceptionSrc));
            //}
            //if (overTime.MaxHoliday == null)
            //{
            //    exception.Add(new ValidationException(ExceptionResourceKeys.MaxHolidayRequierd, "درج - سرانه تعطیل کاری تشویقی نباید خالی باشد", ExceptionSrc));
            //}
            //if (overTime.MaxNightly == null)
            //{
            //    exception.Add(new ValidationException(ExceptionResourceKeys.MaxNightlyRequierd, "درج - سرانه شب کاری تشویقی نباید خالی باشد", ExceptionSrc));
            //}

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(OverTimeDetail obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (obj.OverTime.Date == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DateRequierd, "ویرایش - تاریخ نباید خالی باشد", ExceptionSrc));
            }
            if (obj.MaxHoliday == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxHolidayRequierd, "سرانه تعطیل کاری نباید خالی باشد", ExceptionSrc));
            }
            if (obj.MaxHoliday > 100 || obj.MaxHoliday < 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MaxHolidayRequierd, "سرانه تعطیل کاری باید بین 0 تا 100 باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(OverTimeDetail obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        #endregion
    }
}
