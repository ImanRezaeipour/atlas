using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.BaseInformation;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Temp;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Repository.OverTimeFlow;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.BaseInformation;
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
    /// کلاس بیزینس مربوط به استثناء زمان بندی تایید کارکرد پرسنل و اضافه کار تشویقی مدیران,معاونین,اداری
    /// </summary>
    public class BApprovalAttendanceScheduleException : BaseBusiness<ApprovalAttendanceScheduleException>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private const string ExceptionSrc = "GTS.Clock.Business.OverTimeFlow.BApprovalAttendanceScheduleException";
        private EntityRepository<ApprovalAttendanceScheduleException> ApprovalAttendanceScheduleExceptionRepository = new EntityRepository<ApprovalAttendanceScheduleException>();
        private BCostCenter CostCenterBusiness = new BCostCenter();
        private BPerson personBusiness = new BPerson();
        LanguagesName sysLanguageResource;

        public BApprovalAttendanceScheduleException()
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
        /// لیست استثناء ها را برمیگرداند
        /// </summary>
        /// <returns>لیست استثناء ها</returns>
        public override IList<ApprovalAttendanceScheduleException> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// لیست استثناء های برای یک آبجکت زمانبندی را برمیگرداند
        /// </summary>
        /// <param name="ApprovalAttendanceScheduleID">کلید اصلی آبجکت زمانبندی</param>
        /// <returns>لیست پروکسی استثناء ها</returns>
        public IList<ApprovalAttendanceScheduleExceptionProxy> GetListProxyByApprovalAttendanceScheduleID(decimal ApprovalAttendanceScheduleID)
        {
            ApprovalAttendanceScheduleException approvalAttendanceScheduleExceptionAlias = null;
            IQueryOver<ApprovalAttendanceScheduleException, ApprovalAttendanceScheduleException> searchQuery;

            searchQuery = NHSession.QueryOver(() => approvalAttendanceScheduleExceptionAlias)
                 .Where(() => approvalAttendanceScheduleExceptionAlias.ApprovalAttendanceSchedule.ID == ApprovalAttendanceScheduleID);

            var result = searchQuery.List<ApprovalAttendanceScheduleException>().ToList();

            IList<ApprovalAttendanceScheduleExceptionProxy> proxyList = new List<ApprovalAttendanceScheduleExceptionProxy>();
            foreach (var item in result)
            {
                ApprovalAttendanceScheduleExceptionProxy proxy = new ApprovalAttendanceScheduleExceptionProxy();
                proxy.ID = item.ID;
                proxy.DateTo = item.DateTo;
                proxy.DateFrom = item.DateFrom;
                proxy.PersonID = item.Person.ID;
                proxy.PersonFullName = item.Person.FirstName + " " + item.Person.LastName;
                proxy.PersonCode = item.Person.BarCode;
                proxyList.Add(proxy);
            }
            return proxyList;
        }

        /// <summary>
        /// لیست استثناء های برای یک آبجکت زمانبندی و یک پرسنل را برمیگرداند 
        /// </summary>
        /// <param name="ApprovalAttendanceScheduleID">کلید اصلی آبجکت زمانبندی</param>
        /// <param name="PersonID">کلید اصلی پرسنل</param>
        /// <returns>لیست استثناء ها</returns>
        public IList<ApprovalAttendanceScheduleException> GetListProxyByApprovalAttendanceScheduleID(decimal ApprovalAttendanceScheduleID, decimal PersonID)
        {
            ApprovalAttendanceScheduleException approvalAttendanceScheduleExceptionAlias = null;
            IQueryOver<ApprovalAttendanceScheduleException, ApprovalAttendanceScheduleException> searchQuery;

            searchQuery = NHSession.QueryOver(() => approvalAttendanceScheduleExceptionAlias)
                 .Where(() =>
                     approvalAttendanceScheduleExceptionAlias.ApprovalAttendanceSchedule.ID == ApprovalAttendanceScheduleID &&
                     approvalAttendanceScheduleExceptionAlias.Person.ID == PersonID
                     );

            var result = searchQuery.List<ApprovalAttendanceScheduleException>().ToList();
            return result;
        }


        /// <summary>
        /// مدل را به لیست پروکسی جهت استفاده در UI تبدیل می کند
        /// </summary>
        /// <param name="list"> مدل</param>
        /// <returns> پروکسی</returns>
        public ApprovalAttendanceScheduleExceptionProxy ConvertToProxy(ApprovalAttendanceScheduleException obj)
        {
            ApprovalAttendanceScheduleExceptionProxy proxy = new ApprovalAttendanceScheduleExceptionProxy();
            proxy.ID = obj.ID;
            proxy.DateTo = obj.DateTo;
            proxy.DateFrom = obj.DateFrom;
            proxy.PersonID = obj.Person.ID;
            proxy.PersonFullName = obj.Person.FirstName + " " + obj.Person.LastName;
            proxy.PersonCode = obj.Person.BarCode;
            proxy.ApprovalAttendanceScheduleID = obj.ApprovalAttendanceSchedule.ID;

            return proxy;
        }

        #region CRUD

        /// <summary>
        /// عملیات ذخیره در دیتابیس
        /// </summary>
        /// <param name="obj">مدل استثناء زمان بندی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertApprovalAttendanceScheduleException(ApprovalAttendanceScheduleException obj)
        {
            return base.SaveChanges(obj, UIActionType.ADD);
        }

        /// <summary>
        /// عملیات ویرایش در دیتابیس
        /// </summary>
        /// <param name="obj">مدل استثناء زمان بندی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateApprovalAttendanceScheduleException(ApprovalAttendanceScheduleException obj)
        {
            return base.SaveChanges(obj, UIActionType.EDIT);
        }

        /// <summary>
        /// عملیات حذف در دیتابیس
        /// </summary>
        /// <param name="obj">مدل استثناء زمان بندی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteApprovalAttendanceScheduleException(ApprovalAttendanceScheduleException obj)
        {
            return base.SaveChanges(obj, UIActionType.DELETE);
        }

        #endregion

        #region Validation

        protected override void InsertValidate(ApprovalAttendanceScheduleException obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (obj.DateTo == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateToRequired, "درج - تاریخ ابتدا نباید خالی باشد", ExceptionSrc));
            }

            if (obj.DateFrom == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateFromRequired, "درج - تاریخ انتها نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Person == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleCostCenterRequired, "درج - پرسنل نباید خالی باشد", ExceptionSrc));
            }
            if (obj.DateFrom > obj.DateTo)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateValidRequired, "درج - بازه ی تاریخی معتبر نمی باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void UpdateValidate(ApprovalAttendanceScheduleException obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (obj.DateTo == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateToRequired, "ویرایش - تاریخ ابتدا نباید خالی باشد", ExceptionSrc));
            }
            if (obj.DateFrom == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateFromRequired, "ویرایش - تاریخ انتها نباید خالی باشد", ExceptionSrc));
            }
            if (obj.Person == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleCostCenterRequired, "ویرایش - پرسنل نباید خالی باشد", ExceptionSrc));
            }
            if (obj.DateFrom >= obj.DateTo)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateValidRequired, "ویرایش - بازه ی تاریخی معتبر نمی باشد", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void DeleteValidate(ApprovalAttendanceScheduleException obj)
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
