using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.UI;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.NHibernateFramework;
using System.Reflection;
using GTS.Clock.Business.Security;

namespace GTS.Clock.Business.GridSettings
{
    /// <summary>
    /// تنظیمات کاربر جهت نمایش تعداد ستون در گانچارت ماهانه
    /// </summary>
    public class BGanttChartClientSettings : BaseBusiness<GanttChartClientSettings>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.GridSettings.BGanttChartClientSettings";
        private decimal workingUserSettingsId = 0;
        //private decimal workingLanguageIdId = 0;
        private EntityRepository<GanttChartClientSettings> rep = new EntityRepository<GanttChartClientSettings>(false);
        UserRepository userRep = new UserRepository(false);

        /// <summary>
        /// نام کاربری
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public BGanttChartClientSettings()
        {
        }

        /// <summary>
        ///  سازنده کلاس با ورودی نام کاربری
        /// </summary>
        /// <param name="username">نام کاربری</param>
        public BGanttChartClientSettings(string username)
        {
            this.UserName = username;
        }

        /// <summary>
        /// تنظیمات را برمیگرداند
        /// اگر موجود نباشد ایجاد میکند
        /// </summary>
        /// <returns>تنظیمات</returns>
        public GanttChartClientSettings GetGanttChartClientSettings()
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                GanttChartClientSettings Result = null;
                try
                {
                    if (ValidateUser())
                    {
                        EntityRepository<GanttChartClientSettings> rep = new EntityRepository<GanttChartClientSettings>(false);
                        IList<GanttChartClientSettings> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new GanttChartClientSettings().UserSetting), new UserSettings() { ID = workingUserSettingsId }));
                        if (list != null && list.Count > 0)
                        {
                            Result = list[0];
                        }
                        else//insert record
                        {
                            GanttChartClientSettings settings = new GanttChartClientSettings();
                            foreach (PropertyInfo info in typeof(GanttChartClientSettings).GetProperties())
                            {
                                if (info.PropertyType == typeof(Boolean))
                                {
                                    info.SetValue(settings, true, null);
                                }
                            }
                            settings.UserSetting = new UserSettings() { ID = workingUserSettingsId };
                            base.Insert(settings);
                            Result = settings;
                        }
                    }
                    else
                    {
                        throw new IllegalServiceAccess("کاربر یا تنظیمات کاربر در دیتابیس موجود نیست", ExceptionSrc);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return Result;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "GanttChartClientSettings", "GetGanttChartClientSettings");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات درج
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void InsertValidate(GanttChartClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش
        /// </summary>
        /// <param name="clientSettings">تنظیمات</param>
        protected override void UpdateValidate(GanttChartClientSettings clientSettings)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (!ValidateUser())
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MonthlyOpCurentUserIsNotValid, " کاربر فعلی سیستم نامعتبر است", ExceptionSrc));
            }
            if (clientSettings.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.MonthlyOpIDMustSpecified, " شناسه تنظیمات باید مشخص شود", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void DeleteValidate(GanttChartClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        /// <summary>
        /// آماده سازی تنظیمات قبل از ذخیره در دیتابیس
        /// </summary>
        /// <param name="clientSettings">تنظیمات</param>
        /// <param name="action">نوع عملیات</param>
        protected override void GetReadyBeforeSave(GanttChartClientSettings clientSettings, UIActionType action)
        {
            if (ValidateUser() && action == UIActionType.EDIT)
            {
                clientSettings.UserSetting = new UserSettings() { ID = this.workingUserSettingsId };
            }
        }

        /// <summary>
        /// درج تنظمیات در دیتابیس
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void Insert(GanttChartClientSettings obj)
        {
            rep.WithoutTransactSave(obj);
        }

        /// <summary>
        /// اگر نام کاربری وجود نداشته باشد یا رکورد تنظیمات کاربر در دیتابیس  موجود نباشد غلط برمیگرداند
        /// </summary>
        /// <returns>وضعیت اعتبارسنجی</returns>
        private bool ValidateUser()
        {
            if (this.workingUserSettingsId > 0)
                return true;
            if (Utility.IsEmpty(this.UserName))
            {
                User user = userRep.GetById(BUser.CurrentUser.ID, false);
              
                if (user != null && user.UserSetting != null && user.UserSetting.ID > 0)
                {
                    this.UserName = user.UserName;
                    this.workingUserSettingsId = user.UserSetting.ID;
                }
            }
            else
            {              
                User user = userRep.GetByUserName(this.UserName);
                if (user != null && user.UserSetting != null && user.UserSetting.ID > 0)
                {
                    this.workingUserSettingsId = user.UserSetting.ID;
                }
            }
            if (this.workingUserSettingsId > 0)
            {
                NHibernateSessionManager.Instance.ClearSession();
                return true;
            }
            return false;
        }

        /// <summary>
        /// اعمال تغییرات تنظیمات در دیتبایس برای پرسنل
        /// </summary>
        /// <param name="ganttChartClientSettings">تنظیمات</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی تنظیمات</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal SaveChanges_onPersonnelState(GanttChartClientSettings ganttChartClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(ganttChartClientSettings, UAT);
        }

        /// <summary>
        /// اعمال تغییرات تنظیمات در دیتبایس برای مدیر 
        /// </summary>
        /// <param name="ganttChartClientSettings">تنظمیات</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی تنظیمات</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal SaveChanges_onManagerState(GanttChartClientSettings ganttChartClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(ganttChartClientSettings, UAT);
        }

    }
}
