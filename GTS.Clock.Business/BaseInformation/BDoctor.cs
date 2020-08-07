using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model;
using System.Reflection;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Temp;
using GTS.Clock.Infrastructure;
using NHibernate;
using NHibernate.Criterion;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// دکتر
    /// </summary>
    public class BDoctor : BaseBusiness<Doctor>
    {
        IDataAccess accessPort = new BUser();
        public BDoctor() { }
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BDoctor";
        private EntityRepository<Doctor> staionRepository = new EntityRepository<Doctor>(false);
		private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
		private BTemp bTemp = new BTemp();
		int operationBatchSizeValue = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        
        /// <summary>
        /// کلیه دکترها را بر می گرداند
        /// </summary>
        /// <returns>لیست دکترها</returns>
        public override IList<Doctor> GetAll()
        {
			IList<decimal> accessableIDs = accessPort.GetAccessibleDoctors();
			IList<Doctor> list = new List<Doctor>();
			if (accessableIDs.Count < this.operationBatchSizeValue && this.operationBatchSizeValue < 2100)
			{
				 list = staionRepository.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Doctor().ID), accessableIDs.ToArray(), CriteriaOperation.IN));
			}
			else
			{
				Doctor doctorAlias = null;
				GTS.Clock.Model.Temp.Temp tempAlias = null;
				string operationGUID = this.bTemp.InsertTempList(accessableIDs);
				list = NHSession.QueryOver(() => doctorAlias)
												  .JoinAlias(() => doctorAlias.TempList, () => tempAlias)
												  .Where(() => tempAlias.OperationGUID == operationGUID)
												  .List<Doctor>();
				this.bTemp.DeleteTempList(operationGUID);
			}
            return list;
        }

        public IList<Doctor> GetAll(string SearchItem)
        {
            Doctor DoctorAlias = null;
            IList<Doctor> DoctorList = NHSession.QueryOver<Doctor>(() => DoctorAlias)
                                                 .Where(() => DoctorAlias.FirstName.IsInsensitiveLike(SearchItem, MatchMode.Anywhere) ||
                                                              DoctorAlias.LastName.IsInsensitiveLike(SearchItem, MatchMode.Anywhere)
                                                       )
                                                .List<Doctor>();
            return DoctorList;
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام  تکراری نباشد
        /// نظام پزشکی در صورت تهی نبودن باید یکتا باشد
        /// </summary>
        /// <param name="doctor">دکتر</param>
        protected override void InsertValidate(Doctor doctor)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(doctor.LastName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DoctorLastNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            if (Utility.IsEmpty(doctor.Nezampezaeshki))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DoctorNazampezeshkiRequired, "درج - کد نظام پزشکی نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => doctor.Nezampezaeshki), doctor.Nezampezaeshki)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DoctorNezampezeshkiRepeated, "درج - کد نظام پزشکی نباید تکراری باشد", ExceptionSrc));
                }
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام دکتر تکراری نباشد
        /// </summary>
        /// <param name="doctor">دکتر</param>
        protected override void UpdateValidate(Doctor doctor)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(doctor.LastName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DoctorLastNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            if (!Utility.IsEmpty(doctor.Nezampezaeshki))
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => doctor.Nezampezaeshki), doctor.Nezampezaeshki),
                                                        new CriteriaStruct(Utility.GetPropertyName(() => doctor.ID), doctor.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.DoctorNezampezeshkiRepeated, "درج - کد نظام پزشکی نباید تکراری باشد", ExceptionSrc));
                }
            }
            if (Utility.IsEmpty(doctor.Nezampezaeshki))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.DoctorNazampezeshkiRequired, "درج - کد نظام پزشکی نباید خالی باشد", ExceptionSrc));
            }    

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف دکتر
        /// </summary>
        /// <param name="doctor">دکتر</param>
        protected override void DeleteValidate(Doctor doctor)
        {
           
        }

        /// <summary>
        /// اعمال دسترسی به کاربر جاری بعد از ذخیره دکتر در دتیابیس
        /// </summary>
        /// <param name="obj">دکتر</param>
        /// <param name="action">نوع عملیات</param>
        protected override void OnSaveChangesSuccess(Doctor obj, UIActionType action)
        {
            if (action == UIActionType.ADD)
            {
                new BDataAccess().InsertDataAccess(Infrastructure.DataAccessLevelOperationType.Single, Infrastructure.DataAccessParts.Doctor, obj.ID, BUser.CurrentUser.ID, null, "");
            }
        }

        /// <summary>
        /// بررسی دسترسی به دکترها
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPhysiciansLoadAccess()
        { 
        }

        /// <summary>
        /// علمیات درج دکتر در دیتبایس
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// عملیات ویرایش دکتر در دیتابیس
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePhysician(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// عملیات حذف دکتر از دیتابیس
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePhysician(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط پرسنل در شمای جدولی تایم شیت
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onHourlyRequest_onPersonnel_onGridSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط پرسنل در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onHourlyRequest_onPersonnel_onGanttChartSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط اپراتور در شمای چدولی تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onHourlyRequest_onOperator_onGridSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط اپراتور در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onHourlyRequest_onOperator_onGanttChartSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط مدیر در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onHourlyRequest_onManager_onGridSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط مدیر در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onHourlyRequest_onManager_onGanttChartSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط پرسنل در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onDailyRequest_onPersonnel_onGridSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط پرسنل در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onDailyRequest_onPersonnel_onGanttChartSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط اپراتور در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onDailyRequest_onOperator_onGridSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط اپراتور در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onDailyRequest_onOperator_onGanttChartSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط مدیر در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onDailyRequest_onManager_onGridSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط مدیر در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onDailyRequest_onManager_onGanttChartSchema(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط اپراتور در  فرم درخواست های ثبت شده 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onRequestDaily_onRequestRegisterByOperator(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط اپراتور در  فرم درخواست های ثبت شده 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onRequestDaily_onRequestRegisterByOperatorPermit(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط اپراتور  فرم درخواست های ثبت شده 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onRequestHourly_onRequestRegisterByOperator(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست اپراتور توسط پرسنل  فرم درخواست های ثبت شده 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onRequestHourly_onRequestRegisterByOperatorPermit(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);
        }

        /// <summary>
        /// ذخیره دکتر در درخواست ساعتی توسط پرسنل در  فرم درخواست های ثبت شده 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onRequestHourly_onRequestRegister(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT); 
        }

        /// <summary>
        /// ذخیره دکتر در درخواست روزانه توسط پرسنل در فرم درخواست های ثبت شده 
        /// </summary>
        /// <param name="physician">دکتر</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی دکتر</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPhysician_onRequestDaily_onRequestRegister(Doctor physician, UIActionType UAT)
        {
            return base.SaveChanges(physician, UAT);  
        }

    }
}