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
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure;
using NHibernate;
using System.Reflection;
using GTS.Clock.Business.Proxy;
namespace GTS.Clock.Business.GridSettings
{
    /// <summary>
    /// تنظیمات کاربر جهت نمایش تعداد ستون در گزارش کارکرد ماهانه 
    /// </summary>
    public class BKartableGridClientSettings : BaseBusiness<KartableGridClientSettings>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.GridSettings.BKartableGridClientSettings";
        private decimal workingUserSettingsId = 0;
        private decimal workingLanguageIdId = 0;
        private EntityRepository<KartableGridClientSettings> rep = new EntityRepository<KartableGridClientSettings>(false);
        private UserRepository userRep = new UserRepository(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

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
        public BKartableGridClientSettings()
        {
        }

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="username">نام کاربری</param>
        public BKartableGridClientSettings(string username)
        {
            this.UserName = username;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCaller"></param>
        /// <returns></returns>
        public IList<KartableGridClientSettingsProxy> GetKartableGridClientSettings(GridSettingCaller requestCaller)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                IList<KartableGridClientSettings> kartableGridClientSettingList = null;
                IList<KartableGridClientSettingsProxy> GridClintSettingProxyList = new List<KartableGridClientSettingsProxy>();
                try
                {
                    if (ValidateUser())
                    {
                        switch (requestCaller)
                        {
                            case GridSettingCaller.Kartable:
                                kartableGridClientSettingList = NHSession.QueryOver<KartableGridClientSettings>()
                                                                         .Where(x => x.UserSetting.ID == workingUserSettingsId &&
                                                                                     x.Type == GridSettingCaller.Kartable
                                                                               )
                                                                         .List<KartableGridClientSettings>();

                                if (kartableGridClientSettingList == null || kartableGridClientSettingList.Count == 0)
                                {
                                    foreach (KartablGridSetting setting in Enum.GetValues(typeof(KartablGridSetting)))
                                    {
                                        KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                        GridClientSettingsProxy.ID = 0;
                                        GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                        GridClientSettingsProxy.Exist = false;
                                        GridClientSettingsProxy.GridColumn = setting.ToString();
                                        GridClientSettingsProxy.ViewState = true;
                                        GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                    }
                                }
                                else
                                {
                                    foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                    {
                                        foreach (KartablGridSetting setting in Enum.GetValues(typeof(KartablGridSetting)))
                                        {
                                            if (pInfo.Name == setting.ToString())
                                            {
                                                KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                                GridClientSettingsProxy.ID = kartableGridClientSettingList[0].ID;
                                                GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                                GridClientSettingsProxy.Exist = true;
                                                GridClientSettingsProxy.GridColumn = setting.ToString();
                                                GridClientSettingsProxy.ViewState = (bool)pInfo.GetValue(kartableGridClientSettingList[0], null);
                                                GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                            }
                                        }
                                    }
                                }
                                break;
                            case GridSettingCaller.SpecialKartable:
                                kartableGridClientSettingList = NHSession.QueryOver<KartableGridClientSettings>()
                                                                         .Where(x => x.UserSetting.ID == workingUserSettingsId &&
                                                                                     x.Type == GridSettingCaller.SpecialKartable
                                                                               )
                                                                         .List<KartableGridClientSettings>();

                                if (kartableGridClientSettingList == null || kartableGridClientSettingList.Count == 0)
                                {
                                    foreach (SpecialKartablGridSetting setting in Enum.GetValues(typeof(SpecialKartablGridSetting)))
                                    {
                                        KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                        GridClientSettingsProxy.ID = 0;
                                        GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                        GridClientSettingsProxy.Exist = false;
                                        GridClientSettingsProxy.GridColumn = setting.ToString();
                                        GridClientSettingsProxy.ViewState = true;
                                        GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                    }
                                }
                                else
                                {
                                    foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                    {
                                        foreach (SpecialKartablGridSetting setting in Enum.GetValues(typeof(SpecialKartablGridSetting)))
                                        {
                                            if (pInfo.Name == setting.ToString())
                                            {
                                                KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                                GridClientSettingsProxy.ID = kartableGridClientSettingList[0].ID;
                                                GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                                GridClientSettingsProxy.Exist = true;
                                                GridClientSettingsProxy.GridColumn = setting.ToString();
                                                GridClientSettingsProxy.ViewState = (bool)pInfo.GetValue(kartableGridClientSettingList[0], null);
                                                GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                            }
                                        }
                                    }
                                }
                                break;
                            case GridSettingCaller.Survey:
                                kartableGridClientSettingList = NHSession.QueryOver<KartableGridClientSettings>()
                                                                         .Where(x => x.UserSetting.ID == workingUserSettingsId &&
                                                                                     x.Type == GridSettingCaller.Survey
                                                                               )
                                                                         .List<KartableGridClientSettings>();

                                if (kartableGridClientSettingList == null || kartableGridClientSettingList.Count == 0)
                                {
                                    foreach (ServeyKartablGridSetting setting in Enum.GetValues(typeof(ServeyKartablGridSetting)))
                                    {
                                        KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                        GridClientSettingsProxy.ID = 0;
                                        GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                        GridClientSettingsProxy.Exist = false;
                                        GridClientSettingsProxy.GridColumn = setting.ToString();
                                        GridClientSettingsProxy.ViewState = true;
                                        GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                    }
                                }
                                else
                                {
                                    foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                    {
                                        foreach (ServeyKartablGridSetting setting in Enum.GetValues(typeof(ServeyKartablGridSetting)))
                                        {
                                            if (pInfo.Name == setting.ToString())
                                            {
                                                KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                                GridClientSettingsProxy.ID = kartableGridClientSettingList[0].ID;
                                                GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                                GridClientSettingsProxy.Exist = true;
                                                GridClientSettingsProxy.GridColumn = setting.ToString();
                                                GridClientSettingsProxy.ViewState = (bool)pInfo.GetValue(kartableGridClientSettingList[0], null);
                                                GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                            }
                                        }
                                    }
                                }
                                break;
                            case GridSettingCaller.RequestSubstituteKartable:
                                 kartableGridClientSettingList = NHSession.QueryOver<KartableGridClientSettings>()
                                                                         .Where(x => x.UserSetting.ID == workingUserSettingsId &&
                                                                                     x.Type == GridSettingCaller.RequestSubstituteKartable
                                                                               )
                                                                         .List<KartableGridClientSettings>();

                                if (kartableGridClientSettingList == null || kartableGridClientSettingList.Count == 0)
                                {
                                    foreach (RequestSubstituteKartableGridSetting setting in Enum.GetValues(typeof(RequestSubstituteKartableGridSetting)))
                                    {
                                        KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                        GridClientSettingsProxy.ID = 0;
                                        GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                        GridClientSettingsProxy.Exist = false;
                                        GridClientSettingsProxy.GridColumn = setting.ToString();
                                        GridClientSettingsProxy.ViewState = true;
                                        GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                    }
                                }
                                else
                                {
                                    foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                    {
                                        foreach (RequestSubstituteKartableGridSetting setting in Enum.GetValues(typeof(RequestSubstituteKartableGridSetting)))
                                        {
                                            if (pInfo.Name == setting.ToString())
                                            {
                                                KartableGridClientSettingsProxy GridClientSettingsProxy = new KartableGridClientSettingsProxy();
                                                GridClientSettingsProxy.ID = kartableGridClientSettingList[0].ID;
                                                GridClientSettingsProxy.CurrentUserSettingID = workingUserSettingsId;
                                                GridClientSettingsProxy.Exist = true;
                                                GridClientSettingsProxy.GridColumn = setting.ToString();
                                                GridClientSettingsProxy.ViewState = (bool)pInfo.GetValue(kartableGridClientSettingList[0], null);
                                                GridClintSettingProxyList.Add(GridClientSettingsProxy);
                                            }
                                        }
                                    }
                                }
                                break;
                        }

                    }
                    else
                    {
                        throw new IllegalServiceAccess("کاربر یا تنظیمات کاربر در دیتابیس موجود نیست", ExceptionSrc);
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    return GridClintSettingProxyList;
                }
                catch (Exception ex)
                {
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    LogException(ex, "BKartableGridClientSettings", "GetKartableGridClientSettings");
                    throw ex;
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCaller"></param>
        /// <param name="MasterColArray"></param>
        /// <param name="SettingsColArray"></param>
        /// <param name="Exist"></param>
        /// <param name="CurrentSettingId"></param>
        /// <param name="CurrentUserSettingId"></param>
        public void UpdateGridSettings_Kartable(GridSettingCaller requestCaller, Dictionary<string, string> MasterColArray, Dictionary<string, string> SettingsColArray, string Exist, decimal CurrentSettingId ,decimal CurrentUserSettingId)
        {
            try
            {
                KartableGridClientSettings kartableGridClientSetting = null;
                switch (requestCaller)
                {
                    case GridSettingCaller.Kartable:
                        switch (Exist)
                        {
                            case "True":
                                kartableGridClientSetting = new KartableGridClientSettings { ID = CurrentSettingId };
                                kartableGridClientSetting.UserSetting = new UserSettings { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.Kartable;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (KartablGridSetting setting in Enum.GetValues(typeof(KartablGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }
                                }
                                rep.Update(kartableGridClientSetting);
                                break;

                            case "False":
                                kartableGridClientSetting = new KartableGridClientSettings();
                                kartableGridClientSetting.UserSetting = new UserSettings { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.Kartable;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (KartablGridSetting setting in Enum.GetValues(typeof(KartablGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }
                                }
                                base.Insert(kartableGridClientSetting);
                                break;
                        }
                        break;
                    case GridSettingCaller.SpecialKartable:
                        switch (Exist)
                        {
                            case "True":
                                kartableGridClientSetting = new KartableGridClientSettings { ID = CurrentSettingId };
                                kartableGridClientSetting.UserSetting = new UserSettings() { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.SpecialKartable;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (SpecialKartablGridSetting setting in Enum.GetValues(typeof(SpecialKartablGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }
                                }
                                rep.Update(kartableGridClientSetting);
                                break;

                            case "False":
                                kartableGridClientSetting = new KartableGridClientSettings();
                                kartableGridClientSetting.UserSetting = new UserSettings() { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.SpecialKartable;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (SpecialKartablGridSetting setting in Enum.GetValues(typeof(SpecialKartablGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }

                                }
                                // rep.WithoutTransactSave(kartableGridClientSetting);
                                base.Insert(kartableGridClientSetting);
                                break;
                        }

                        break;
                    case GridSettingCaller.Survey:
                        switch (Exist)
                        {
                            case "True":
                                kartableGridClientSetting = new KartableGridClientSettings { ID = CurrentSettingId };
                                kartableGridClientSetting.UserSetting = new UserSettings { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.Survey;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (ServeyKartablGridSetting setting in Enum.GetValues(typeof(ServeyKartablGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }
                                }
                                rep.Update(kartableGridClientSetting);
                                break;

                            case "False":
                                kartableGridClientSetting = new KartableGridClientSettings();
                                kartableGridClientSetting.UserSetting = new UserSettings { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.Survey;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (ServeyKartablGridSetting setting in Enum.GetValues(typeof(ServeyKartablGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }
                                }
                                base.Insert(kartableGridClientSetting);
                                break;
                        }
                        break;

                    case GridSettingCaller.RequestSubstituteKartable:
                        switch (Exist)
                        {
                            case "True":
                                kartableGridClientSetting = new KartableGridClientSettings { ID = CurrentSettingId };
                                kartableGridClientSetting.UserSetting = new UserSettings { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.RequestSubstituteKartable;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (RequestSubstituteKartableGridSetting setting in Enum.GetValues(typeof(RequestSubstituteKartableGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }
                                }
                                rep.Update(kartableGridClientSetting);
                                break;

                            case "False":
                                kartableGridClientSetting = new KartableGridClientSettings();
                                kartableGridClientSetting.UserSetting = new UserSettings { ID = CurrentUserSettingId };
                                kartableGridClientSetting.Type = GridSettingCaller.RequestSubstituteKartable;
                                foreach (PropertyInfo pInfo in typeof(KartableGridClientSettings).GetProperties())
                                {
                                    foreach (RequestSubstituteKartableGridSetting setting in Enum.GetValues(typeof(RequestSubstituteKartableGridSetting)))
                                    {
                                        if (pInfo.Name == setting.ToString())
                                        {
                                            pInfo.SetValue(kartableGridClientSetting, bool.Parse(SettingsColArray[MasterColArray[pInfo.Name]]), null);
                                            break;
                                        }
                                        else
                                        {
                                            if (pInfo.Name != "ID" && pInfo.Name != "UserSetting" && pInfo.Name != "Type")
                                                pInfo.SetValue(kartableGridClientSetting, false, null);
                                        }
                                    }
                                }
                                base.Insert(kartableGridClientSetting);
                                break;
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                LogException(ex, "BKartableGridClientSettings", "UpdateGridSettings_Kartable");
                throw ex;
            }
           
        }
       
        /// <summary>
        /// اعتبارسنجی عملیات درج 
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void InsertValidate(KartableGridClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        /// <summary>
        /// اعتبارسنجی عملیات ویرایش 
        /// </summary>
        /// <param name="clientSettings">تنظیمات</param>
        protected override void UpdateValidate(KartableGridClientSettings clientSettings)
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
        /// اعتبارسنجی عملیات حذف
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void DeleteValidate(KartableGridClientSettings obj)
        {
            throw new IllegalServiceAccess("دسترسی به این سرویس مجاز نیمباشد", ExceptionSrc);
        }

        /// <summary>
        /// آماده سازی تنظمیات جهت درج در دیتابیس
        /// </summary>
        /// <param name="clientSettings">تنظیمات</param>
        /// <param name="action">نوع عملیات</param>
        protected override void GetReadyBeforeSave(KartableGridClientSettings clientSettings, UIActionType action)
        {
            if (ValidateUser() && action == UIActionType.EDIT)
            {
                clientSettings.UserSetting = new UserSettings() { ID = this.workingUserSettingsId };
            }
        }

        /// <summary>
        /// عملیات درج در دیتابیس
        /// </summary>
        /// <param name="obj">تنظیمات</param>
        protected override void Insert(KartableGridClientSettings obj)
        {
            rep.WithoutTransactSave(obj);
        }

        /// <summary>
        /// اگر نام کاربری وجود نداشته باشد یا رکورد تنظیمات کاربر در دیتابیس  موجود نباشد غلط برمیگرداند
        /// </summary>
        /// <returns>بلی/خیر</returns>
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
        /// زبان انتخابی کاربر را اعتبارستجی میکند
        /// </summary>
        /// <returns>بلی/خیر</returns>
        private bool ValidateLanguage()
        {
            if (this.workingLanguageIdId > 0)
                return true;
            if (Utility.IsEmpty(this.UserName))
            {
                this.UserName = Security.BUser.CurrentUser.UserName;
                AppSettings.BLanguage blang = new GTS.Clock.Business.AppSettings.BLanguage();
                Languages lang = blang.GetLanguageByUsername(this.UserName);
                if (lang.ID > 0)
                {
                    this.workingLanguageIdId = lang.ID;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                AppSettings.BLanguage blang = new GTS.Clock.Business.AppSettings.BLanguage();
                Languages lang = blang.GetLanguageByUsername(this.UserName);
                if (lang.ID > 0)
                {
                    this.workingLanguageIdId = lang.ID;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kartableGridClientSettings"></param>
        /// <param name="UAT"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal SaveChanges_onPersonnelState(KartableGridClientSettings kartableGridClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(kartableGridClientSettings, UAT);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kartableGridClientSettings"></param>
        /// <param name="UAT"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public decimal GridSettingSaveChanges(KartableGridClientSettings kartableGridClientSettings, UIActionType UAT)
        {
            return base.SaveChanges(kartableGridClientSettings, UAT);
        }
    }
}
