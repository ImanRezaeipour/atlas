using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transaction;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.OverTimeFlow;
using GTS.Clock.Model.OverTimeFlow;

namespace GTS.Clock.Business.PersonInfo
{
    public class BPersonApprovalAttendance : BaseBusiness<PersonApprovalAttendance>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.PersonInfo.BPersonApprovalAttendance";

        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private EntityRepository<PersonApprovalAttendance> personApprovalAttendanceRepository = new EntityRepository<PersonApprovalAttendance>();
        private BApprovalAttendanceSchedule ApprovalAttendanceScheduleBusiness = new BApprovalAttendanceSchedule();
        private BApprovalAttendanceScheduleException ApprovalAttendanceScheduleExceptionBusiness = new BApprovalAttendanceScheduleException();
        public BPersonApprovalAttendance()
        {

        }

        public IList<PersonApprovalAttendance> GetAll()
        {
            return base.GetAll();
        }

        public bool GetAccessToApprove(int year, int month, decimal personId)
        {
            bool result = true;
            DateTime Date;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                Date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
            }
            else
            {
                Date = new DateTime(year, month, 1);
            }

            //آیا قبلا تایید کرده است؟-----------------------------------------------------------------

            if (CheckIsDuplicate(Date, personId))
                result = false;
            //آیا خارج مهلت تایید است؟-----------------------------------------------------------------
            Person person = new PersonRepository().GetById(personId, false);
            if (CheckIsExpireTime(person))
                result = false;

            return result;
        }

        /// <summary>
        /// چک کن کارکرد قبلا تایید شده یا خیر
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public bool CheckIsDuplicate(DateTime Date, decimal personId)
        {
            PersonApprovalAttendance personApprovalAttendanceAlias = null;
            IQueryOver<PersonApprovalAttendance, PersonApprovalAttendance> searchQuery;
            searchQuery = NHSession.QueryOver(() => personApprovalAttendanceAlias)
                            .Where(() => personApprovalAttendanceAlias.Date == Date
                                        && personApprovalAttendanceAlias.Person.ID == personId);

            PersonApprovalAttendance item = searchQuery.SingleOrDefault<PersonApprovalAttendance>();

            if (item != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// چک کن مهلت تایید کارکرد رد شده است یا خیر
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public bool CheckIsExpireTime(Person person)
        {
            var currentDate = DateTime.Now.Date;
            //---------------------------------------------------------------------
            //ابتدا چک کن مرکز هزینه برای پرسنل تعریف شده است یا خیر
            UIValidationExceptions exception = new UIValidationExceptions();
            if (person.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterRequired, "مرکز هزینه به پرسنل اختصاص داده نشده است", ExceptionSrc));
                throw exception;
            }
            //---------------------------------------------------------------------
            //آبجت زمانبندی را برای پرسنل بر اساس مرکزهزینه آن واکشی کن
            ApprovalAttendanceSchedule approvalSchedule = ApprovalAttendanceScheduleBusiness.GetByApprovalScheduleTypeAndCostCenter(ApprovalScheduleType.Personal, person.ID);
            if (approvalSchedule == null)
            {
                return true;
            }
            //---------------------------------------------------------------------
            //چک کن این پرسنل در لیست استثناء زمانبندی واکشی شده هست یا خیر ؟
            var exceptionList = ApprovalAttendanceScheduleExceptionBusiness.GetListProxyByApprovalAttendanceScheduleID(approvalSchedule.ID, person.ID);
            if (exceptionList != null || exceptionList.Count > 0)
            {
                if(exceptionList.Where(c => c.DateFrom.Date <= currentDate && c.DateTo.Date >= currentDate).Any())
                {
                    return false;
                }
            }
            //---------------------------------------------------------------------

            if (approvalSchedule.DateFrom.Date <= currentDate && approvalSchedule.DateTo.Date >= currentDate)
                return false;
            else
                return true;
            //---------------------------------------------------------------------
        }

        /// <summary>
        /// بررسی می کنه که آخرین روز گزارش کارکرد کوچکتر از روز جاری است یا خیر
        /// با این روش می توانیم بررسی کنیم که ماه  کامل شده است یا خیر 
        /// </summary>
        /// <param name="Date">آخرین روز گزارش کارکرد ماهانه</param>
        /// <returns>بلی/خیر</returns>
        public bool CheckMonthCompeleted(DateTime Date)
        {
            return Date < DateTime.Now;
        }

        /// <summary>
        /// تایید کارکرد توسط شخص پرسنل
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personId"></param>
        public void ApprovedAttendanceByPerson(int year, int month, decimal personId, string FromDate, string ToDate)
        {
            DateTime Date;
            DateTime _ToDate;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                Date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                _ToDate = Utility.ToMildiDate(ToDate);
            }
            else
            {
                Date = new DateTime(year, month, 1);
                _ToDate = Utility.ToMildiDateTime(ToDate);
            }
            //-----------------------------------
            if (!CheckMonthCompeleted(_ToDate))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalMonthNotCompeleted, "ماه انتخاب شده جهت تایید تردد صحیح نمی باشد", ExceptionSrc));
                throw exception;
            }
            //-----------------------------------
            Model.Security.User user = BUser.CurrentUser;
            PersonApprovalAttendance personApprovalAttendance = new PersonApprovalAttendance();
            personApprovalAttendance.Person = new Model.Person() { ID = personId };
            personApprovalAttendance.Date = Date;
            personApprovalAttendance.RegisterDatetime = DateTime.Now;
            personApprovalAttendance.Approved = true;
            personApprovalAttendance.RegisterPerson = user.Person;

            this.InsertPersonApprovalAttendance(personApprovalAttendance);
        }

        /// <summary>
        /// تایید کارکرد توسط اپراتور
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personId"></param>
        public void ApprovedAttendanceByOperator(int year, int month, decimal personId, string FromDate, string ToDate)
        {
            DateTime Date;
            DateTime _ToDate;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                Date = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                _ToDate = Utility.ToMildiDate(ToDate);
            }
            else
            {
                Date = new DateTime(year, month, 1);
                _ToDate = Utility.ToMildiDateTime(ToDate);
            }
            //-----------------------------------
            if (!CheckMonthCompeleted(_ToDate))
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalMonthNotCompeleted, "ماه انتخاب شده جهت تایید تردد صحیح نمی باشد", ExceptionSrc));
                throw exception;
            }
            //-----------------------------------
            Model.Security.User user = BUser.CurrentUser;
            PersonApprovalAttendance personApprovalAttendance = new PersonApprovalAttendance();
            personApprovalAttendance.Person = new Model.Person() { ID = personId };
            personApprovalAttendance.Date = Date;
            personApprovalAttendance.RegisterDatetime = DateTime.Now;
            personApprovalAttendance.Approved = true;
            personApprovalAttendance.RegisterPerson = user.Person;

            this.InsertPersonApprovalAttendanceByOperator(personApprovalAttendance);
        }

        #region CRUD

        /// <summary>
        /// عملیات ذخیره در دیتابیس 
        /// </summary>
        /// <param name="obj">آبجکت تاییدیه کارکرد</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPersonApprovalAttendance(PersonApprovalAttendance obj)
        {
            InsertByPersonValidate(obj);
            return base.SaveChanges(obj, UIActionType.ADD);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPersonApprovalAttendanceByOperator(PersonApprovalAttendance obj)
        {
            InsertByOperatorValidate(obj);
            return base.SaveChanges(obj, UIActionType.ADD);
        }

        /// <summary>
        /// عملیات ویرایش در دیتابیس 
        /// </summary>
        /// <param name="obj">آبجکت تاییدیه کارکرد</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePersonApprovalAttendance(PersonApprovalAttendance obj)
        {
            return base.SaveChanges(obj, UIActionType.EDIT);
        }

        /// <summary>
        /// عملیات حذف در دیتابیس
        /// </summary>
        /// <param name="obj">آبجکت تاییدیه کارکرد</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePersonApprovalAttendance(PersonApprovalAttendance obj)
        {
            return base.SaveChanges(obj, UIActionType.DELETE);
        }

        #endregion

        #region Validation
        protected override void InsertValidate(PersonApprovalAttendance obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected void InsertByPersonValidate(PersonApprovalAttendance obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (obj.Person.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterRequired, "مرکز هزینه به پرسنل اختصاص داده نشده است", ExceptionSrc));
            }

            if (CheckIsDuplicate(obj.Date, obj.Person.ID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalDuplicate, "کارکرد قبلا تایید شده است", ExceptionSrc));
            }

            if (CheckIsExpireTime(obj.Person))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalExpire, "مهلت تایید کارکرد به اتمام رسیده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected void InsertByOperatorValidate(PersonApprovalAttendance obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (obj.Person.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.CostCenterRequired, "مرکز هزینه به پرسنل اختصاص داده نشده است", ExceptionSrc));
            }

            if (CheckIsDuplicate(obj.Date, obj.Person.ID))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalDuplicate, "کارکرد قبلا تایید شده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(PersonApprovalAttendance obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(PersonApprovalAttendance obj)
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
