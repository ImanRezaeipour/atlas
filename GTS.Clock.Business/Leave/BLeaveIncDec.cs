﻿using System;
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
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Validation.Configuration;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.Leave
{
    /// <summary>
    /// افزایش/کاهش سهمیه مرخصی
    /// created at: 2012-02-06 11:00:19 AM
    /// by        : Farhad Salvati
    /// write your name here
    /// </summary>
    public class BLeaveIncDec : BaseBusiness<LeaveIncDec>
    {
        int minutesInDay = 8 * 60;
        private const string ExceptionSrc = "GTS.Clock.Business.Leave.BLeaveIncDec";
        private EntityRepository<LeaveIncDec> objectRep = new EntityRepository<LeaveIncDec>();

        /// <summary>
        /// کلیه افزایش/کاهش های سهمیه مرخصی را برای یک پرسنل برمی گرداند
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <returns>لیست پروکسی افزایش/کاهش سهمیه مرخصی</returns>
        public IList<LeaveIncDecProxy> GetAllByPersonId(decimal personId)
        {
            try
            {
                IList<LeaveIncDecProxy> result = new List<LeaveIncDecProxy>();
                Person prs = new BPerson().GetByID(personId);
                foreach (LeaveIncDec l in prs.LeaveIncDecList)
                {
                    LeaveIncDecProxy proxy = new LeaveIncDecProxy() { ID = l.ID, Description = l.Description };
                    proxy.Day = Math.Abs(l.Day).ToString();
                    proxy.Hour = Utility.IntTimeToTime(Math.Abs(l.Minute), true);
                    proxy.Action = l.Type;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.Date = Utility.ToPersianDate(l.Date);
                    }
                    else
                    {
                        proxy.Date = Utility.ToString(l.Date);
                    }
                    result.Add(proxy);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BLeaveIncDec", "GetAllByLeaveYear");
                throw ex;
            }
        }

        /// <summary>
        /// درج افزایش/کاهش سهمیه مرخصی در دیتابیس
        /// </summary>
        /// <param name="personId">کلید اصلی پرسنل</param>
        /// <param name="day">روز</param>
        /// <param name="time">زمان</param>
        /// <param name="action">افزایش/کاهش</param>
        /// <param name="description">شرح</param>
        /// <param name="date">تاریخ</param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertLeaveIncDec(decimal personId, string day, string time, LeaveIncDecAction action, string description, string date)
        {
            try
            {
                LeaveIncDec l = new LeaveIncDec();
                l.Person = new Person() { ID = personId };
                l.Description = description;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    l.Date = Utility.ToMildiDate(date);
                }
                else
                {
                    l.Date = Utility.ToMildiDateTime(date);
                }
                l.Day = Utility.ToInteger(day);
                l.Minute = Utility.RealTimeToIntTime(time);
                if (action == LeaveIncDecAction.Decrease)
                {
                    l.Day *= -1;
                    l.Minute *= -1;
                }
                this.SaveChanges(l, UIActionType.ADD);
                return l.ID;
            }
            catch (Exception ex)
            {
                LogException(ex, "BLeaveIncDec", "DeleteLeaveIncDec");
                throw ex;
            }
        }

        /// <summary>
        /// عملیات حذف افزایش/کاهش سهمیه مرخصی در دیتابیس
        /// </summary>
        /// <param name="leaveIncDecId">کلید اصلی افزایش/کاهش سهمیه مرخصی</param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void DeleteLeaveIncDec(decimal leaveIncDecId)
        {
            try
            {
                LeaveIncDec l = new LeaveIncDec();
                l = base.GetByID(leaveIncDecId);
                this.SaveChanges(l, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                LogException(ex, "BLeaveIncDec", "DeleteLeaveIncDec");
                throw ex;
            }
        }

        /// <summary>
        /// بررسی دسترسی به رزو مانده مرخصی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckLeaveReserveLoadAccess()
        {
        }

        #region BaseBusiness Implementation

        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// </summary>
        /// <param name="obj">افزایش/کاهش سهمیه مرخصی</param>
        protected override void InsertValidate(LeaveIncDec obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش
        /// </summary>
        /// <param name="obj">افزایش/کاهش سهمیه مرخصی</param>
        protected override void UpdateValidate(LeaveIncDec obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            throw new NotImplementedException();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="obj">افزایش/کاهش سهمیه مرخصی</param>
        protected override void DeleteValidate(LeaveIncDec obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی قوانین واسط کاربری افزایش/کاهش سهمیه مرخصی
        /// </summary>
        /// <param name="obj">افزایش/کاهش سهمیه مرخصی</param>
        /// <param name="action">نوع عملیات</param>
        protected override void UIValidate(LeaveIncDec obj, UIActionType action)
        {
            UIValidator.DoValidate(obj);
        }

        /// <summary>
        /// به روزرسانی نشانگر محاسبات
        /// </summary>
        /// <param name="obj">افزایش/کاهش سهمیه مرخصی</param>
        /// <param name="action">نوع عملیات</param>
        protected override void UpdateCFP(LeaveIncDec obj, UIActionType action)
        {
            base.UpdateCFP(obj.Person.ID, obj.Date);
        }

        #endregion
    }
}
