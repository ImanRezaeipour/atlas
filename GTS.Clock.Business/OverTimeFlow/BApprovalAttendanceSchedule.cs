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
    /// کلاس بیزینس مربوط به زمان بندی تایید کارکرد پرسنل و اضافه کار تشویقی مدیران,معاونین,اداری
    /// </summary>
    public class BApprovalAttendanceSchedule : BaseBusiness<ApprovalAttendanceSchedule>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private const string ExceptionSrc = "GTS.Clock.Business.OverTimeFlow.BApprovalAttendanceSchedule";
        private EntityRepository<ApprovalAttendanceSchedule> approvalAttendanceScheduleRepository = new EntityRepository<ApprovalAttendanceSchedule>();
        private BCostCenter CostCenterBusiness = new BCostCenter();
        private BPerson personBusiness = new BPerson();
        LanguagesName sysLanguageResource;

        public BApprovalAttendanceSchedule()
        {
            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }

            //this.Seed();
        }
          
        public override IList<ApprovalAttendanceSchedule> GetAll()
        {
            this.CheckCostCentersExists();
            return base.GetAll();
        }

        public ApprovalAttendanceSchedule GetByApprovalScheduleType(ApprovalScheduleType type)
        {
            ApprovalAttendanceSchedule approvalAttendanceScheduleAlias = null;
            ApprovalAttendanceSchedule item = NHSession.QueryOver(() => approvalAttendanceScheduleAlias)
                .Where(() => approvalAttendanceScheduleAlias.ApprovalType == type)
                .List<ApprovalAttendanceSchedule>().SingleOrDefault();
            return item;

        }

        public ApprovalAttendanceSchedule GetByApprovalScheduleTypeAndCostCenter(ApprovalScheduleType type, decimal personID)
        {
            Person person = personBusiness.GetByID(personID);
            if (person.CostCenter != null)
            {
                ApprovalAttendanceSchedule approvalAttendanceScheduleAlias = null;
                ApprovalAttendanceSchedule item = NHSession.QueryOver(() => approvalAttendanceScheduleAlias)
                    .Where(() => 
                        approvalAttendanceScheduleAlias.ApprovalType == type && 
                        approvalAttendanceScheduleAlias.CostCenter.ID == person.CostCenter.ID)
                    .List<ApprovalAttendanceSchedule>().SingleOrDefault();
                return item;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// کلیه زمان بندی های تایید کارکرد و اضافه کاری تشویقی را بر می گرداند
        /// </summary>
        /// <returns>لیست زمان بندی ها</returns>
        public IList<ApprovalAttendanceScheduleProxy> GetAllProxy()
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    this.CheckCostCentersExists();
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    NHibernateSessionManager.Instance.ClearSession();
                    IList<ApprovalAttendanceSchedule> items = approvalAttendanceScheduleRepository.GetAll();
                    IList<ApprovalAttendanceScheduleProxy> result = new List<ApprovalAttendanceScheduleProxy>();
                    foreach (ApprovalAttendanceSchedule item in items)
                    {
                        result.Add(ConvertToProxy(item));
                    }
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
        /// کلیه زمان بندی های تایید کارکرد و اضافه کاری تشویقی را بر اساس مرکز هزینه بر می گرداند
        /// </summary>
        /// <param name="costCenterId">کلید مرکز هزینه</param>
        /// <returns>لیست زمان بندی ها</returns>
        public IList<ApprovalAttendanceScheduleProxy> GetProxyByCostCenter(decimal costCenterId)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    this.CheckCostCentersExists();
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    NHibernateSessionManager.Instance.ClearSession();

                    ApprovalAttendanceSchedule ApprovalAttendanceScheduleAlias = null;
                    IList<ApprovalAttendanceSchedule> items = NHSession.QueryOver(() => ApprovalAttendanceScheduleAlias)
                                           .Where(() => ApprovalAttendanceScheduleAlias.CostCenter.ID == costCenterId)
                                           .List<ApprovalAttendanceSchedule>();

                    IList<ApprovalAttendanceScheduleProxy> result = new List<ApprovalAttendanceScheduleProxy>();
                    foreach (ApprovalAttendanceSchedule item in items)
                    {
                        result.Add(ConvertToProxy(item));
                    }

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
        /// مدل زمان بندی را به پروکسی مربوطه تبدیل می کند
        /// </summary>
        /// <param name="item">مدل</param>
        /// <returns>پروکسی</returns>
        public ApprovalAttendanceScheduleProxy ConvertToProxy(ApprovalAttendanceSchedule item)
        {
            ApprovalAttendanceScheduleProxy proxy = new ApprovalAttendanceScheduleProxy();
            proxy.ID = item.ID;
            proxy.DateFrom = item.DateFrom;
            proxy.DateTo = item.DateTo;
            proxy.DateRangeOrder = item.DateRangeOrder;
            if (item.CostCenter != null)
            {
                proxy.CostCenterID = item.CostCenter.ID;
                proxy.CostCenterName = item.CostCenter.Name;
            }
            switch (item.ApprovalType)
            {
                case ApprovalScheduleType.Personal: proxy.ApprovalType = "تایید کارکرد کارمندان";
                    break;
                case ApprovalScheduleType.Manager: proxy.ApprovalType = "تایید کارتابل مدیران";
                    break;
                case ApprovalScheduleType.Assistance: proxy.ApprovalType = "تایید کارتابل معاونین";
                    break;
                case ApprovalScheduleType.Administrative: proxy.ApprovalType = "تایید کارتابل اداری";
                    break;
            }

            return proxy;
        }

        /// <summary>
        /// عملیات ویرایش در دیتابیس از روی پروکسی 
        /// </summary>
        /// <param name="obj">پروکسی زمان بندی</param>
        /// <returns></returns>
        public decimal UpdateApprovalAttendanceScheduleProxy(ApprovalAttendanceScheduleProxy obj)
        {
            ApprovalAttendanceSchedule item = base.GetByID(obj.ID);
            item.DateFrom = obj.DateFrom;
            item.DateTo = obj.DateTo;
            item.DateRangeOrder = obj.DateRangeOrder;
            item.CostCenter = new CostCenter() { ID = obj.CostCenterID };

            return this.UpdateApprovalAttendanceSchedule(item);
        }

        private void CheckCostCentersExists()
        {
            var scheduleList = base.GetAll();
            var costCenterList = CostCenterBusiness.GetAll();

            foreach (var item in costCenterList)
            {
                //--------------------------------------------------------------
                if (!scheduleList.Where(c => c.CostCenter != null && c.CostCenter.ID == item.ID && c.ApprovalType == ApprovalScheduleType.Personal).Any())
                {
                    ApprovalAttendanceSchedule obj = new ApprovalAttendanceSchedule();
                    obj.ApprovalType = ApprovalScheduleType.Personal;
                    obj.DateFrom = DateTime.Now;
                    obj.DateTo = DateTime.Now.AddDays(1);
                    obj.DateRangeOrder = DateTime.Now.Month;
                    obj.CostCenter = new CostCenter() { ID = item.ID };
                    approvalAttendanceScheduleRepository.WithoutTransactSave(obj);
                }
                //--------------------------------------------------------------
                if (!scheduleList.Where(c => c.CostCenter != null && c.CostCenter.ID == item.ID && c.ApprovalType == ApprovalScheduleType.Manager).Any())
                {
                    ApprovalAttendanceSchedule obj = new ApprovalAttendanceSchedule();
                    obj.ApprovalType = ApprovalScheduleType.Manager;
                    obj.DateFrom = DateTime.Now;
                    obj.DateTo = DateTime.Now.AddDays(1);
                    obj.DateRangeOrder = DateTime.Now.Month;
                    obj.CostCenter = new CostCenter() { ID = item.ID };
                    approvalAttendanceScheduleRepository.WithoutTransactSave(obj);
                }
                //--------------------------------------------------------------
                if (!scheduleList.Where(c => c.CostCenter != null && c.CostCenter.ID == item.ID && c.ApprovalType == ApprovalScheduleType.Assistance).Any())
                {
                    ApprovalAttendanceSchedule obj = new ApprovalAttendanceSchedule();
                    obj.ApprovalType = ApprovalScheduleType.Assistance;
                    obj.DateFrom = DateTime.Now;
                    obj.DateTo = DateTime.Now.AddDays(1);
                    obj.DateRangeOrder = DateTime.Now.Month;
                    obj.CostCenter = new CostCenter() { ID = item.ID };
                    approvalAttendanceScheduleRepository.WithoutTransactSave(obj);
                }
                //--------------------------------------------------------------
                if (!scheduleList.Where(c => c.CostCenter != null && c.CostCenter.ID == item.ID && c.ApprovalType == ApprovalScheduleType.Administrative).Any())
                {
                    ApprovalAttendanceSchedule obj = new ApprovalAttendanceSchedule();
                    obj.ApprovalType = ApprovalScheduleType.Administrative;
                    obj.DateFrom = DateTime.Now;
                    obj.DateTo = DateTime.Now.AddDays(1);
                    obj.DateRangeOrder = DateTime.Now.Month;
                    obj.CostCenter = new CostCenter() { ID = item.ID };
                    approvalAttendanceScheduleRepository.WithoutTransactSave(obj);
                }
                //--------------------------------------------------------------
            }
        }

        #region CRUD

        /// <summary>
        /// عملیات ذخیره در دیتابیس
        /// </summary>
        /// <param name="obj">مدل زمان بندی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertApprovalAttendanceSchedule(ApprovalAttendanceSchedule obj)
        {
            return base.SaveChanges(obj, UIActionType.ADD);
        }

        /// <summary>
        /// عملیات ویرایش در دیتابیس
        /// </summary>
        /// <param name="obj">مدل زمان بندی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateApprovalAttendanceSchedule(ApprovalAttendanceSchedule obj)
        {
            return base.SaveChanges(obj, UIActionType.EDIT);
        }

        /// <summary>
        /// عملیات حذف در دیتابیس
        /// </summary>
        /// <param name="obj">مدل زمان بندی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteApprovalAttendanceSchedule(ApprovalAttendanceSchedule obj)
        {
            return base.SaveChanges(obj, UIActionType.DELETE);
        }

        #endregion

        #region Validation

        protected override void InsertValidate(ApprovalAttendanceSchedule obj)
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
            if (obj.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleCostCenterRequired, "درج - مرکز هزینه نباید خالی باشد", ExceptionSrc));
            }

            if (obj.ApprovalType == ApprovalScheduleType.None)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleTypeRequired, "درج - نوع نباید خالی باشد", ExceptionSrc));
            }

            if (obj.DateFrom >= obj.DateTo)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateValidRequired, "درج - بازه ی تاریخی معتبر نمی باشد", ExceptionSrc));
            }


            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void UpdateValidate(ApprovalAttendanceSchedule obj)
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
            if (obj.CostCenter == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleCostCenterRequired, "ویرایش - مرکز هزینه نباید خالی باشد", ExceptionSrc));
            }
            if (obj.ApprovalType == ApprovalScheduleType.None)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleTypeRequired, "ویرایش - نوع نباید خالی باشد", ExceptionSrc));
            }

            if (obj.DateFrom >= obj.DateTo)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateValidRequired, "ویرایش - بازه ی تاریخی معتبر نمی باشد", ExceptionSrc));
            }

            if (obj.DateRangeOrder == null)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateRangeOrderRequired, "ویرایش - کد دوره نباید خالی باشد", ExceptionSrc));
            }

            if (obj.DateRangeOrder > 12 || obj.DateRangeOrder < 1)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.ApprovalScheduleDateRangeOrderValidate, "ویرایش - کد دوره معتبر نمی باشد", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override void DeleteValidate(ApprovalAttendanceSchedule obj)
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
