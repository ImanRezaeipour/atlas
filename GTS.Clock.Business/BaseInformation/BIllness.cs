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

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// نوع بیماری
    /// </summary>
    public class BIllness : BaseBusiness<Illness>
    {
        public BIllness() { }
        const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BIllness";
        private EntityRepository<Illness> staionRepository = new EntityRepository<Illness>(false);

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام  تکراری نباشد
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        protected override void InsertValidate(Illness illness)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(illness.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRequierd, "درج - نام نباید خالی باشد", ExceptionSrc));
            }
            else if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => illness.Name), illness.Name)) > 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRepeated, "درج - نام نباید تکراری باشد", ExceptionSrc));
            }

            
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// «اعتبارسنجی
        /// «نام نباید خالی باشد
        /// «نام نوع استخدام تکراری نباشد
        /// </summary>
        /// <param name="station"></param>
        protected override void UpdateValidate(Illness illness)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(illness.Name))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRequierd, "نام نباید خالی باشد", ExceptionSrc));
            }
            else
            {
                if (staionRepository.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => illness.Name), illness.Name),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => illness.ID), illness.ID, CriteriaOperation.NotEqual)) > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.IllnessNameRepeated, "نام نباید تکراری باشد", ExceptionSrc));
                }
            }            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف نوع بیماری
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        protected override void DeleteValidate(Illness illness)
        {
           
        }

        /// <summary>
        /// بررسی دسترسی به نوع بیماری
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckIllnessesLoadAccess()
        {
        }

        /// <summary>
        /// عملیات درج نوع بیماری در دیتابیس
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات درج</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// عملیات ویرایش نوع بیماری در دیتابیس
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateIllness(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// عملیات حذف نوع بیماری در دیتابیس
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteIllness(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه در درخواست های ثبت شده
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestDaily_onRequestRegister(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی در درخواست های ثبت شده 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestHourly_onRequestRegister(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه در درخواست روزانه برای غیبت ها 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestDaily_onDailyRequestOnAbsence(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه در درخواست روزانه برای غیبت ها 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestHourly_onHourlyRequestOnAbsence(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه توسط اپراتور در درخواست های ثبت شده 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestDaily_onRequestRegisterByOperator(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست مجوز روزانه توسط اپراتور در درخواست های ثبت شده 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestDaily_onRequestRegisterByOperatorPermit(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی توسط اپراتور در درخواست های ثبت شده 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestHourly_onRequestRegisterByOperator(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست مجوز ساعتی توسط اپراتور در درخواست های ثبت شده 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onRequestHourly_onRequestRegisterByOperatorPermit(Illness illness, UIActionType UAT)
        {
            return base.SaveChanges(illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی توسط پرسنل در شمای گرید تایم شیت 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onHourlyRequest_onPersonnel_onGridSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی توسط پرسنل در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onHourlyRequest_onPersonnel_onGanttChartSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی توسط اپراتور در شمای گرید تایم شیت 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onHourlyRequest_onOperator_onGridSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی توسط اپراتور در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onHourlyRequest_onOperator_onGanttChartSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }
         
        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی توسط مدیر در شمای گرید تایم شیت 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onHourlyRequest_onManager_onGridSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست ساعتی توسط مدیر در شمای گانت تایم شیت 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onHourlyRequest_onManager_onGanttChartSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه توسط پرسنل در شمای گرید تایم شیت 
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onDailyRequest_onPersonnel_onGridSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه توسط پرسنل در شمای گانت تایم شیت  
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onDailyRequest_onPersonnel_onGanttChartSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه توسط اپراتور در شمای گرید تایم شیت  
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onDailyRequest_onOperator_onGridSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه توسط اپراتور در شمای گانت تایم شیت  
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onDailyRequest_onOperator_onGanttChartSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه توسط مدیر در شمای گرید تایم شیت  
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onDailyRequest_onManager_onGridSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }

        /// <summary>
        /// درج نوع بیماری در درخواست روزانه توسط مدیر در شمای گانت تایم شیت  
        /// </summary>
        /// <param name="illness">نوع بیماری</param>
        /// <param name="UAT">عملیات اصلی</param>
        /// <returns>کلید اصلی نوع بیماری</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertIllness_onDailyRequest_onManager_onGanttChartSchema(Illness Illness, UIActionType UAT)
        {
            return base.SaveChanges(Illness, UAT);
        }
 
    }
}