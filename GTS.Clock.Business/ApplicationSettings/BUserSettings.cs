using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Globalization;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Model;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.BaseInformation;


namespace GTS.Clock.Business.AppSettings
{
    public class BUserSettings : BaseBusiness<UserSettings>
    {
        const string ExceptionSrc = "GTS.Clock.Business.AppSettings.BUserSettings";
        private string currentUser;
        public string CurrentUser
        {
            get
            {
                this.currentUser = Security.BUser.CurrentUser.UserName;
                return this.currentUser;
            }
        }
        UserRepository userRep = new UserRepository(false);
        EntityRepository<UserSettings> userSettingRep = new EntityRepository<UserSettings>(false);
        EntityRepository<EmailSettings> emailSettingRep = new EntityRepository<EmailSettings>(false);
        EntityRepository<DashboardSettings> dashboardSettingRep = new EntityRepository<DashboardSettings>(false);
        EntityRepository<Dashboards> dashboardsRep = new EntityRepository<Dashboards>(false);
        EntityRepository<SMSSettings> smsSettingRep = new EntityRepository<SMSSettings>(false);

        /// <summary>
        /// تنظیم کردن پوسته جاری
        /// </summary>
        /// <param name="skinID">کلید پوسته</param>
        public void SetCurrentSkin(decimal skinID)
        {
            try
            {
                EntityRepository<UISkin> skinRep = new EntityRepository<UISkin>(false);
                UserRepository rep = new UserRepository(false);
                User user = rep.GetByUserName(this.CurrentUser);
                UISkin skin = skinRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UISkin().ID), skinID)).FirstOrDefault();
                if (skin != null && user != null)
                {
                    UserSettings userSetings = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user)).FirstOrDefault();
                    if (userSetings != null)
                    {
                        userSetings.Skin = skin;
                        userSettingRep.Update(userSetings);
                    }
                    else
                    {
                        userSetings = new UserSettings();
                        userSetings.Skin = skin;
                        userSetings.User = user;
                        userSetings.Language = BLanguage.GetCurrentSystemLanguage();
                        userSettingRep.Save(userSetings);
                    }
                    SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalSkinSessionName);
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UserSettingsSkinOrUserNotExists, "پوسته یا کاربر در دیتابیس موجود نمیباشد", "GTS.Clock.Business.AppSettings.LanguageProvider.SetCurrentSkin");
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserSettings", "SetCurrentSkin");
                throw ex;
            }
        }

        /// <summary>
        /// پوسته جاری
        /// </summary>
        public static string CurrentSkin
        {
            get
            {
                try
                {
                    if (!SessionHelper.HasSessionValue(SessionHelper.BussinessLocalSkinSessionName))
                    {
                        string curentSkin = "";
                        User user = Security.BUser.CurrentUser;
                        if (user != null && user.ID > 0)
                        {
                            EntityRepository<UserSettings> appRep = new EntityRepository<UserSettings>(false);
                            IList<UserSettings> list = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user));
                            if (list.Count > 0 && list[0].Skin != null)
                            {
                                curentSkin = list[0].Skin.EnName;
                                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalSkinSessionName, curentSkin);
                            }
                        }
                        else
                        {
                            SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalSkinSessionName);
                        }
                        if (Utility.IsEmpty(curentSkin))
                        {
                            EntityRepository<UISkin> skinRep = new EntityRepository<UISkin>(false);
                            UISkin skin = skinRep.GetAll().FirstOrDefault();
                            if (skin != null)
                            {
                                curentSkin = skin.EnName;
                                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalSkinSessionName, curentSkin);
                            }
                        }
                        return curentSkin;
                    }
                    object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessLocalSkinSessionName);
                    if (obj != null)
                    {
                        return (string)obj;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "BUserSettings", "CurrentSkin");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// کلیه پوسته ها را بر می گرداند
        /// </summary>
        /// <returns>لیست پوسته ها</returns>
        public IList<UISkin> GetAll()
        {
            try
            {
                IList<UISkin> skins = new List<UISkin>();
                if (!SessionHelper.HasSessionValue(SessionHelper.BussinessAllSkinSessionName))
                {
                    EntityRepository<UISkin> skinRep = new EntityRepository<UISkin>(false);
                    skins = skinRep.GetAll();
                    SessionHelper.SaveSessionValue(SessionHelper.BussinessAllSkinSessionName, skins);
                    return skins;
                    foreach (UISkin skin in skins)
                    {
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            skin.Name = skin.FnName;
                        }
                        else
                        {
                            skin.Name = skin.EnName;
                        }
                    }
                    return skins;
                }
                object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessAllSkinSessionName);
                if (obj != null)
                {
                    skins = (IList<UISkin>)obj;
                    if (skins == null || skins.Count == 0)
                    {
                        SessionHelper.ClearSessionValue(SessionHelper.BussinessAllSkinSessionName);
                    }
                    foreach (UISkin skin in skins)
                    {
                        if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        {
                            skin.Name = skin.FnName;
                        }
                        else
                        {
                            skin.Name = skin.EnName;
                        }
                    }
                    return skins;
                }
                else
                {
                    return new List<UISkin>();
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserSettings", "GetAll");
                throw ex;
            }
        }

        #region Email

        /// <summary>
        /// تنظمیات ایمیل یک پرسنل را بر می گرداند
        /// </summary>
        /// <param name="personID">کد پرسنلی</param>
        /// <returns>تنظمیات ایمیل</returns>
        public EmailSettings GetEmailSetting(decimal personID)
        {
            try
            {
                Person person = new BPerson().GetByID(personID);
                UserSettings userSetting = this.GetUserSettings(person.User);
                EmailSettings settings = this.GetEmailSettings(userSetting);

                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetEmailSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات داشبورد یک کاربر را بر می گرداند
        /// </summary>
        /// <param name="personID">کد پرسنلی</param>
        /// <returns>تنظیمات داشبورد</returns>
        public DashboardSettings GetDashboardSetting(decimal personID)
        {
            try
            {
                Person person = new BPerson().GetByID(personID);
                UserSettings userSetting = this.GetUserSettings(person.User);
                DashboardSettings settings = this.GetDashboardSettings(userSetting);


                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetDashboardSetting");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personID">کد پرسنلی</param>
        /// <returns></returns>
        public CollectiveRequestRegistType GetOperatorCollectiveRequestRegistTypeSetting(decimal personID)
        {
            try
            {
                Person person = new BPerson().GetByID(personID);
                UserSettings userSetting = this.GetUserSettings(person.User);
                CollectiveRequestRegistType collectiveRequestTypeObj = userSetting.OperatorCollectiveRequestRegistType;


                return collectiveRequestTypeObj;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetDashboardSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات داشبورد کاربر جاری را بر می گرداند
        /// </summary>
        /// <returns>تنظیمات داشبورد</returns>
        public DashboardSettings GetDashboardSetting()
        {
            try
            {
                UserSettings userSetting = this.GetUserSettings(BUser.CurrentUser);
                DashboardSettings settings = this.GetDashboardSettings(userSetting); ;

                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetDashboardSetting");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CollectiveRequestRegistType GetOperatorCollectiveRequestRegistTypeSetting()
        {
            try
            {
                UserSettings userSetting = this.GetUserSettings(BUser.CurrentUser);
                CollectiveRequestRegistType collectiveRequestType = userSetting.OperatorCollectiveRequestRegistType;

                return collectiveRequestType;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetOperatorCollectiveRequestRegistTypeSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظمیات ایمیل کاربر جاری را بر می گرداند
        /// </summary>
        /// <returns>تنظیمات ایمیل</returns>
        public EmailSettings GetEmailSetting()
        {
            try
            {
                UserSettings userSetting = this.GetUserSettings(BUser.CurrentUser);
                EmailSettings settings = this.GetEmailSettings(userSetting); ;
                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetEmailSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات ایمیل کاربر جاری را دخیره می کند
        /// </summary>
        /// <param name="setting"></param>
        public void SaveEmailSetting(EmailSettings setting)
        {
            try
            {
                User user = userRep.GetById(BUser.CurrentUser.ID, false);
                this.SaveEmailSetting(setting, user);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات ایمیل را برای پرسنل جستجو شده ذخیره می کند
        /// </summary>
        /// <param name="setting">تنظیمات ایمیل</param>
        /// <param name="proxy">پروکسی جستجوی پیشرفته پرسنل</param>
        public void SaveEmailSetting(EmailSettings setting, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list;

                //don't select count 
                if (proxy.PersonId > 0)
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 1, PersonCategory.Public);
                }
                else
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy);
                }

                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveEmailSetting(setting, prs.User);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSettingAdvanceSearch");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات ایمیل را برای پرسنل جستجو شده ذخیره می کند
        /// </summary>
        /// <param name="setting">تنظیمات ایمیل</param>
        /// <param name="QuickSearch">عبارت جستجو سریع</param>
        public void SaveEmailSetting(EmailSettings setting, string QuickSearch)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearch(QuickSearch, PersonCategory.Public);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveEmailSetting(setting, prs.User);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSettingQuickSearch");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات ایمیل را برای پرسنل ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظیمات ایمیل</param>
        /// <param name="personId">کلید پرسنل</param>
        public void SaveEmailSetting(EmailSettings setting, decimal personId)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonId = personId };
                this.SaveEmailSetting(setting, proxy);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات ایمیل را برای کاربر ذخیره می کند  
        /// </summary>
        /// <param name="setting">تنظیمات ایمیل</param>
        /// <param name="user">کاربر</param>
        private void SaveEmailSetting(EmailSettings setting, User user)
        {
            try
            {

                /*if (user == null || user.ID == 0)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.CurrentUserIsNotValid, "کاربر جاری نا معتبر است", ExceptionSrc);
                }*/
                if (user.UserSetting == null || user.UserSetting.ID == 0)
                {
                    user.UserSetting = this.GetUserSettings(user);
                }
                if (user.UserSetting.EmailSettings == null)
                {
                    user.UserSetting.EmailSettings = this.GetEmailSettings(user.UserSetting);
                }

                UserSettings userSetting = base.GetByID(user.UserSetting.ID);
                userSetting.EmailSettings.Active = setting.Active;
                userSetting.EmailSettings.DayHour = Utility.RealTimeToIntTime(setting.TheDayHour);
                userSetting.EmailSettings.DayCount = setting.DayCount;
                userSetting.EmailSettings.Hour = Utility.RealTimeToIntTime(setting.TheHour);
                userSetting.EmailSettings.SendByDay = setting.SendByDay;

                #region validation
                UIValidationExceptions exceptions = new UIValidationExceptions();
                if (setting.Active)
                {
                    if (setting.SendByDay)
                    {
                        if (userSetting.EmailSettings.DayHour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_EmailTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                    }
                    else
                    {
                        if (userSetting.EmailSettings.Hour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_EmailTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                        else if (userSetting.EmailSettings.Hour < 5)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_EmailTimeLessThanMin, "تکرار زمان ارسال ایمیل حداقل 5 دقیقه میباشد", ExceptionSrc));
                        }
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
                #endregion

                this.SaveChanges(userSetting, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات  ایمیل را برای کل سازمان برمیگرداند
        /// </summary>
        /// <returns>لیست تنظمیاتایمیل</returns>
        public IList<InfoServiceProxy> GetAllEmailSettings()
        {
            try
            {
                IList<InfoServiceProxy> proxyList = new List<InfoServiceProxy>();
                IList<UserSettings> settingList = base.GetAll();

                var result = from o in settingList
                             where o.EmailSettings != null && o.EmailSettings.Active
                             select o.EmailSettings;
                IList<EmailSettings> emailSettingList = result.ToList<EmailSettings>();

                foreach (EmailSettings setting in emailSettingList)
                {
                    InfoServiceProxy proxy = new InfoServiceProxy();

                    proxy.PersonId = setting.UserSetting.User.Person.ID;
                    proxy.PersonName = setting.UserSetting.User.Person.Name;
                    proxy.PersonCode = setting.UserSetting.User.Person.PersonCode;
                    proxy.Sex = setting.UserSetting.User.Person.Sex;
                    proxy.SendByDay = setting.SendByDay;
                    proxy.EmailAddress = setting.UserSetting.User.Person.PersonDetail.EmailAddress;
                    proxy.SmsNumber = setting.UserSetting.User.Person.PersonDetail.MobileNumber;
                    if (setting.SendByDay)
                    {
                        proxy.RepeatePeriod = new TimeSpan(setting.DayCount, ((int)setting.DayHour / 60), setting.DayHour % 60, 0);
                    }
                    else
                    {
                        proxy.RepeatePeriod = new TimeSpan(((int)setting.Hour / 60), setting.Hour % 60, 0);
                    }
                    proxyList.Add(proxy);
                }
                return proxyList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetAllEmailSettings");
                throw ex;
            }
        }
        #endregion

        #region SMS

        /// <summary>
        /// تنظیمات  پیامک را برای کل سازمان برمیگرداند
        /// </summary>
        /// <returns>لیست تنظیمات</returns>
        public IList<InfoServiceProxy> GetAllSmsSettings()
        {
            try
            {
                IList<InfoServiceProxy> proxyList = new List<InfoServiceProxy>();
                IList<UserSettings> settingList = base.GetAll();

                var result = from o in settingList
                             where o.SMSSettings != null && o.SMSSettings.Active
                             select o.SMSSettings;
                IList<SMSSettings> smsSettingList = result.ToList<SMSSettings>();

                foreach (SMSSettings setting in smsSettingList)
                {
                    InfoServiceProxy proxy = new InfoServiceProxy();

                    proxy.PersonId = setting.UserSetting.User.Person.ID;
                    proxy.PersonName = setting.UserSetting.User.Person.Name;
                    proxy.PersonCode = setting.UserSetting.User.Person.PersonCode;
                    proxy.Sex = setting.UserSetting.User.Person.Sex;
                    proxy.SendByDay = setting.SendByDay;
                    proxy.EmailAddress = setting.UserSetting.User.Person.PersonDetail.EmailAddress;
                    proxy.SmsNumber = setting.UserSetting.User.Person.PersonDetail.MobileNumber;

                    if (setting.SendByDay)
                    {
                        proxy.RepeatePeriod = new TimeSpan(setting.DayCount, ((int)setting.DayHour / 60), setting.DayHour % 60, 0);
                    }
                    else
                    {
                        proxy.RepeatePeriod = new TimeSpan(((int)setting.Hour / 60), setting.Hour % 60, 0);
                    }
                    proxyList.Add(proxy);
                }
                return proxyList;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات پیامک پرسنل را بر میگرداند 
        /// </summary>
        /// <param name="personID">کلید پرسنل</param>
        /// <returns>تنظیمات پیامک</returns>
        public SMSSettings GetSMSSetting(decimal personID)
        {
            try
            {
                Person person = new BPerson().GetByID(personID);
                UserSettings userSetting = this.GetUserSettings(person.User);
                SMSSettings settings = this.GetSMSSettings(userSetting);

                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetSMSSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات پیامک کاربر جاری را بر میگرداند
        /// </summary>
        /// <returns>تنظیمات پیامک</returns>
        public SMSSettings GetSMSSetting()
        {
            try
            {
                UserSettings userSetting = this.GetUserSettings(BUser.CurrentUser);
                SMSSettings settings = this.GetSMSSettings(userSetting);
                settings.TheDayHour = Utility.IntTimeToRealTime(settings.DayHour);
                settings.TheHour = Utility.IntTimeToRealTime(settings.Hour);
                return settings;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "GetSMSSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات پیامک را برای کاربر جاری ذخیره می کند
        /// </summary>
        /// <param name="setting">تنظیمات پیامک</param>
        public void SaveSMSSetting(SMSSettings setting)
        {
            try
            {
                this.SaveSMSSetting(setting, BUser.CurrentUser);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettings");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات پیامک را برای پرسنل جستجو شده ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظیمات پیامک</param>
        /// <param name="proxy">پروکسی جستجوی پیشرفته پرسنل</param>
        public void SaveSMSSetting(SMSSettings setting, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list;

                //don't select count 
                if (proxy.PersonId > 0)
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 1, PersonCategory.Public);
                }
                else
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy);
                }
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveSMSSetting(setting, prs.User);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettingsAdvanceSearch");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات پیامک را برای پسنل جستجو شده ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظیمات پیامک</param>
        /// <param name="QuickSearch">عبارت جستجوی پرسنل</param>
        public void SaveSMSSetting(SMSSettings setting, string QuickSearch)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearch(QuickSearch, PersonCategory.Public);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveSMSSetting(setting, prs.User);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettingsQuickSearch");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات پیامک را برای پرسنل ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظیمات پیامک</param>
        /// <param name="personId">کلید پرسنل</param>
        public void SaveSMSSetting(SMSSettings setting, decimal personId)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonId = personId };
                this.SaveSMSSetting(setting, proxy);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettings");
                throw ex;
            }
        }

        /// <summary>
        /// تنظیمات پیامک را برای کاربر ذخیره می کند
        /// </summary>
        /// <param name="setting">تنظیمات پیامک</param>
        /// <param name="user">کاربر</param>
        private void SaveSMSSetting(SMSSettings setting, User user)
        {
            try
            {
                /*if (user == null || user.ID == 0)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.CurrentUserIsNotValid, "کاربر جاری نا معتبر است", ExceptionSrc);
                }*/
                if (user.UserSetting == null || user.UserSetting.ID == 0)
                {
                    user.UserSetting = this.GetUserSettings(user);
                }
                if (user.UserSetting.SMSSettings == null)
                {
                    user.UserSetting.SMSSettings = this.GetSMSSettings(user.UserSetting);
                }
                if (user.UserSetting.EmailSettings == null)
                {
                    user.UserSetting.EmailSettings = this.GetEmailSettings(user.UserSetting);
                }

                UserSettings userSetting = base.GetByID(user.UserSetting.ID);
                userSetting.SMSSettings.Active = setting.Active;
                userSetting.SMSSettings.DayHour = Utility.RealTimeToIntTime(setting.TheDayHour);
                userSetting.SMSSettings.DayCount = setting.DayCount;
                userSetting.SMSSettings.Hour = Utility.RealTimeToIntTime(setting.TheHour);
                userSetting.SMSSettings.SendByDay = setting.SendByDay;

                #region validation
                UIValidationExceptions exceptions = new UIValidationExceptions();
                if (setting.Active)
                {
                    if (setting.SendByDay)
                    {
                        if (userSetting.SMSSettings.DayHour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_SMSTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                    }
                    else
                    {
                        if (userSetting.SMSSettings.Hour == 0)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_SMSTimeIsNotValid, "زمان ارسال ایمیل نا معتبر است", ExceptionSrc));
                        }
                        else if (userSetting.SMSSettings.Hour < 5)
                        {
                            exceptions.Add(new ValidationException(ExceptionResourceKeys.UserSet_SMSTimeLessThanMin, "تکرار زمان ارسال ایمیل حداقل 5 دقیقه میباشد", ExceptionSrc));
                        }
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
                #endregion

                this.SaveChanges(userSetting, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveSMSSettings");
                throw ex;
            }
        }

        #endregion

        #region Insert Update Delete
        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        /// <param name="obj">تنظیمات کاربر</param>
        protected override void InsertValidate(UserSettings obj)
        {

        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        /// <param name="obj">تنظیمات کاربر</param>
        protected override void UpdateValidate(UserSettings obj)
        {

        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        /// <param name="obj">تنظیمات کاربر</param>
        protected override void DeleteValidate(UserSettings obj)
        {
        }
        #endregion


        /// <summary>
        /// تنظیمات کاربر را برمیگرداند
        /// در صورد عدم وجود آنرا درج میکند
        /// </summary>
        /// <param name="user">کاربر</param>
        /// <returns>تنظیمات کاربر</returns>
        private UserSettings GetUserSettings(User user)
        {
            if (user != null && user.ID > 0)
            {
                IList<UserSettings> userSettingList = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), new User() { ID = user.ID })).Where(u => u.SubSystemID == 1).ToList();
                if (userSettingList != null && userSettingList.Count > 0)
                {
                    user.UserSetting = userSettingList.First();
                    return user.UserSetting;
                }
                {
                    UserSettings userSetings = new UserSettings();
                    userSetings.User = user;
                    userSetings.SubSystemID = 1;
                    // userSetings.MonthlyOperationSchema = 1;
                    userSetings.Language = BLanguage.GetCurrentSystemLanguage();
                    userSettingRep.Save(userSetings);
                    user.UserSetting = userSetings;
                    return userSetings;
                }
            }
            return null;
        }

        /// <summary>
        /// تنظمیات ایمیل را با توجه به تنظیمات کاربر بر می گرداند
        /// </summary>
        /// <param name="userSettings">تنظیمات کاربر</param>
        /// <returns>تنظیمات ایمیل</returns>
        private EmailSettings GetEmailSettings(UserSettings userSettings)
        {
            if (userSettings != null && userSettings.ID > 0)
            {
                if (userSettings.EmailSettings != null && userSettings.EmailSettings.ID > 0)
                {
                    return userSettings.EmailSettings;
                }
                else
                {
                    IList<EmailSettings> emailSettingList = emailSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new EmailSettings().UserSetting), new UserSettings() { ID = userSettings.ID }));
                    if (emailSettingList != null && emailSettingList.Count > 0)
                    {
                        return emailSettingList.First();
                    }
                    else
                    {
                        EmailSettings obj = new EmailSettings();
                        obj.ID = userSettings.ID;
                        obj.UserSetting = userSettings;
                        obj.Active = false;
                        emailSettingRep.WithoutTransactSave(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// تنظمیات داشبورد را با توجه به تنظیمات کاربر بر می گرداند
        /// </summary>
        /// <param name="userSettings">تنظیمات کاربر</param>
        /// <returns>تنظیمات داشبورد</returns>
        private DashboardSettings GetDashboardSettings(UserSettings userSettings)
        {
            if (userSettings != null && userSettings.ID > 0)
            {
                if (userSettings.DashboardSettings != null && userSettings.DashboardSettings.ID > 0)
                {
                    return userSettings.DashboardSettings;
                }
                else
                {
                    IList<DashboardSettings> dashboardSettingList = dashboardSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new DashboardSettings().UserSetting), new UserSettings() { ID = userSettings.ID }));

                    if (dashboardSettingList != null && dashboardSettingList.Count > 0)
                    {
                        return dashboardSettingList.First();
                    }
                    else
                    {

                        IList<Dashboards> dashboardList = dashboardsRep.GetAll();
                        DashboardSettings obj = new DashboardSettings();
                        obj.ID = userSettings.ID;
                        obj.UserSetting = userSettings;

                        obj.Dashboard1 = dashboardList.SingleOrDefault(d => d.Name == "PrivateNews.aspx");
                        obj.Dashboard2 = dashboardList.SingleOrDefault(d => d.Name == "PersonnelInformationSummary.aspx");
                        obj.Dashboard3 = dashboardList.SingleOrDefault(d => d.Name == "PublicNews.aspx");
                        obj.Dashboard4 = dashboardList.SingleOrDefault(d => d.Name == "LocalDateTime.aspx");
                        dashboardSettingRep.WithoutTransactSave(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// تنظمیات پیامک را با توجه به تنظیمات کاربر بر می گرداند
        /// </summary>
        /// <param name="userSettings">تنظیمات کاربر</param>
        /// <returns>تنظیمات پیامک</returns>
        private SMSSettings GetSMSSettings(UserSettings userSettings)
        {
            if (userSettings != null && userSettings.ID > 0)
            {
                if (userSettings.SMSSettings != null && userSettings.SMSSettings.ID > 0)
                {
                    return userSettings.SMSSettings;
                }
                else
                {
                    IList<SMSSettings> smsSettingList = smsSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new SMSSettings().UserSetting), new UserSettings() { ID = userSettings.ID }));
                    if (smsSettingList != null && smsSettingList.Count > 0)
                    {
                        return smsSettingList.First();
                    }
                    else
                    {
                        SMSSettings obj = new SMSSettings();
                        obj.ID = userSettings.ID;
                        obj.UserSetting = userSettings;
                        obj.Active = false;
                        smsSettingRep.WithoutTransactSave(obj);
                        return obj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// تنظمیات داشبورد را برای کاربر جاری ذخیره می کند
        /// </summary>
        /// <param name="setting">تنظمیات داشبورد</param>
        public void SaveDashboardSetting(DashboardSettings setting)
        {
            try
            {
                User user = userRep.GetById(BUser.CurrentUser.ID, false);
                this.SaveDashboardSetting(setting, user);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveDashboardSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظمیات داشبورد را برای پرسنل جستجو شده ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظمیات داشبورد</param>
        /// <param name="proxy">پروکسی جستجوی پیشرفته پرسنل</param>
        public void SaveDashboardSetting(DashboardSettings setting, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list;

                //don't select count 
                if (proxy.PersonId > 0)
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 1, PersonCategory.Public);
                }
                else
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy);
                }

                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    if(prs.User != null && prs.User.ID != 0)
                       this.SaveDashboardSetting(setting, prs.User);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveEmailSettingAdvanceSearch");
                throw ex;
            }
        }

        /// <summary>
        /// تنظمیات داشبورد را برای پرسنل جستجو شده ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظمیات داشبورد</param>
        /// <param name="QuickSearch">عبارت جستجوی پرسنل</param>
        public void SaveDashboardSetting(DashboardSettings setting, string QuickSearch)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearch(QuickSearch, PersonCategory.Public);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveDashboardSetting(setting, prs.User);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveDashboardSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظمیات داشبورد را برای پرسنل ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظمیات داشبورد</param>
        /// <param name="personId">کلید پرسنل</param>
        public void SaveDashboardSetting(DashboardSettings setting, decimal personId)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                PersonAdvanceSearchProxy proxy = new PersonAdvanceSearchProxy() { PersonId = personId };
                this.SaveDashboardSetting(setting, proxy);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveDashboardSetting");
                throw ex;
            }
        }

        /// <summary>
        /// تنظمیات داشبورد را برای کاربر ذخیره می کند 
        /// </summary>
        /// <param name="setting">تنظمیات داشبورد</param>
        /// <param name="user">کاربر</param>
        private void SaveDashboardSetting(DashboardSettings setting, User user)
        {
            try
            {


                if (user.UserSetting == null || user.UserSetting.ID == 0)
                {
                    user.UserSetting = this.GetUserSettings(user);
                }
                if (user.UserSetting.DashboardSettings == null)
                {
                    user.UserSetting.DashboardSettings = this.GetDashboardSettings(user.UserSetting);
                }

                UserSettings userSetting = base.GetByID(user.UserSetting.ID);
                userSetting.DashboardSettings.Dashboard1 = setting.Dashboard1;
                userSetting.DashboardSettings.Dashboard2 = setting.Dashboard2;
                userSetting.DashboardSettings.Dashboard3 = setting.Dashboard3;
                userSetting.DashboardSettings.Dashboard4 = setting.Dashboard4;
                IList<Dashboards> dashboardList = new List<Dashboards>();
                if (userSetting.DashboardSettings.Dashboard1 != null)
                    dashboardList.Add(userSetting.DashboardSettings.Dashboard1);
                if (userSetting.DashboardSettings.Dashboard2 != null)
                    dashboardList.Add(userSetting.DashboardSettings.Dashboard2);
                if (userSetting.DashboardSettings.Dashboard3 != null)
                    dashboardList.Add(userSetting.DashboardSettings.Dashboard3);
                if (userSetting.DashboardSettings.Dashboard4 != null)
                    dashboardList.Add(userSetting.DashboardSettings.Dashboard4);


                #region validation
                UIValidationExceptions exceptions = new UIValidationExceptions();
                foreach (Dashboards item in dashboardList)
                {
                    if (dashboardList.Count(d => d.ID == item.ID) > 1)
                    {
                        exceptions.Add(new ValidationException(ExceptionResourceKeys.DashboardIsDuplicated, "داشبورد تکراری نباید انتخاب شود.", ExceptionSrc));
                        throw exceptions;
                    }
                }
                if (exceptions.Count > 0)
                    throw exceptions;
                #endregion

                this.SaveChanges(userSetting, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SaveDashboardSetting");
                throw ex;
            }
        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPersonnelUserSettingsLoadAccess()
        {
        }

        /// <summary>
        /// جهت پیش بینی نیازهای آتی
        /// پیاده سازی نشده است
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckManagementUserSettingsLoadAccess()
        {
        }

        /// <summary>
        /// شمای گزارش تایم شیت ماهانه برای کاربر جاری
        /// </summary>
        public static int CurrentMonthlyOperationReportSchema
        {
            get
            {
                try
                {
                    if (!SessionHelper.HasSessionValue(SessionHelper.BussinessLocalMonthlyOperationReportSchema))
                    {
                        int currentSchema = 0;
                        User user = Security.BUser.CurrentUser;
                        if (user != null && user.ID > 0)
                        {
                            EntityRepository<UserSettings> appRep = new EntityRepository<UserSettings>(false);
                            IList<UserSettings> list = appRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user));
                            if (list.Count > 0)
                            {
                                currentSchema = list[0].MonthlyOperationSchema;
                                SessionHelper.SaveSessionValue(SessionHelper.BussinessLocalMonthlyOperationReportSchema, currentSchema);
                            }
                        }
                        else
                        {
                            SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalMonthlyOperationReportSchema);
                        }
                        return currentSchema;
                    }

                    object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessLocalMonthlyOperationReportSchema);
                    if (obj != null)
                    {
                        return (int)obj;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "BUserSettings", "CurrentMonthlyOperationReportSchema");
                    throw ex;
                }
            }
        }

        /// <summary>
        /// شمای گزارش تایم شیت ماهانه برای کاربر جاری تنظیم می کند 
        /// </summary>
        /// <param name="Schema">کلید شما</param>
        public void SetCurrentMounthlyOperationSchema(int Schema)
        {
            try
            {
                UserRepository rep = new UserRepository(false);
                User user = rep.GetByUserName(this.CurrentUser);

                if (user != null)
                {
                    UserSettings userSetings = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user)).FirstOrDefault();
                    if (userSetings != null)
                    {

                        userSetings.MonthlyOperationSchema = Schema;
                        userSettingRep.Update(userSetings);
                    }

                    SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalMonthlyOperationReportSchema);
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UserSettingsUserGridSchemaSttingNotExists, "تنظیمات نمای شبکه ای کاربر در دیتابیس موجود نمی باشد", "GTS.Clock.Business.AppSettings.LanguageProvider.SetCurrentMounthlyOperationSchema");
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserSettings", "SetCurrentMounthlyOperationSchema");
                throw ex;
            }
        }

        /// <summary>
        /// شمای گزارش تایم شیت ماهانه برای کاربر جاری  شده ذخیره می کند 
        /// </summary>
        /// <param name="collectiveRequestObj">طریقه ثبت درخواست انبوه</param>
        public void SaveOperatorCollectiveRequestRegistType(CollectiveRequestRegistType collectiveRequestObj)
        {
            try
            {
                UserRepository rep = new UserRepository(false);
                User user = rep.GetByUserName(this.CurrentUser);

                if (user != null)
                {
                    UserSettings userSetings = userSettingRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new UserSettings().User), user)).FirstOrDefault();
                    if (userSetings != null)
                    {

                        userSetings.OperatorCollectiveRequestRegistType = collectiveRequestObj;
                        userSettingRep.Update(userSetings);
                    }

                    //SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalMonthlyOperationReportSchema);
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UserSettingsUserGridSchemaSttingNotExists, "تنظیمات نمای شبکه ای کاربر در دیتابیس موجود نمی باشد", "GTS.Clock.Business.AppSettings.LanguageProvider.SetCurrentMounthlyOperationSchema");
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserSettings", "SetOperatorCollectiveRequestRegistType");
                throw ex;
            }
        }

        /// <summary>
        /// شمای گزارش تایم شیت ماهانه برای پرسنل جستجو شده ذخیره می کند 
        /// </summary>
        /// <param name="collectiveRequestObj">طریقه ثبت درخواست انبوه</param>
        /// <param name="personId">کلید پرسنل</param>
        public void SaveOperatorCollectiveRequestRegistType(CollectiveRequestRegistType collectiveRequestObj, decimal personId)
        {
            try
            {
                Person person = new BPerson().GetByID(personId);
                UserSettings userSettings = this.GetUserSettings(person.User);



                if (userSettings != null)
                {

                    userSettings.OperatorCollectiveRequestRegistType = collectiveRequestObj;
                    userSettingRep.Update(userSettings);
                }
                else
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.UserSettingsUserGridSchemaSttingNotExists, "تنظیمات نمای شبکه ای کاربر در دیتابیس موجود نمی باشد", "GTS.Clock.Business.AppSettings.LanguageProvider.SetCurrentMounthlyOperationSchema");
                }
                //SessionHelper.ClearSessionValue(SessionHelper.BussinessLocalMonthlyOperationReportSchema);


            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BUserSettings", "SetOperatorCollectiveRequestRegistType");
                throw ex;
            }
        }

        /// <summary>
        /// شمای گزارش تایم شیت ماهانه برای پرسنل جستجو شده ذخیره می کند
        /// </summary>
        /// <param name="collectiveRequestObj">طریقه ثبت درخواست انبوه</param>
        /// <param name="QuickSearch">جستجوی کاربر</param>
        public void SaveOperatorCollectiveRequestRegistType(CollectiveRequestRegistType collectiveRequestObj, string QuickSearch)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearch(QuickSearch, PersonCategory.Public);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveOperatorCollectiveRequestRegistType(collectiveRequestObj, prs.ID);
                }

            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SetOperatorCollectiveRequestRegistType");
                throw ex;
            }
        }
        
        /// <summary>
        /// شمای گزارش تایم شیت ماهانه برای پرسنل جستجو شده ذخیره می کند
        /// </summary>
        /// <param name="collectiveRequestObj">طریقه ثبت درخواست انبوه</param>
        /// <param name="proxy">پروکسی جستجوی پیشرفته پرسنل</param>
        public void SaveOperatorCollectiveRequestRegistType(CollectiveRequestRegistType collectiveRequestObj, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                ISearchPerson searchTool = new BPerson();
                IList<Person> list;

                //don't select count 
                if (proxy.PersonId > 0)
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy, 0, 1, PersonCategory.Public);
                }
                else
                {
                    list = searchTool.GetPersonInAdvanceSearch(proxy);
                }

                var l = from o in list
                        select o;
                list = l.ToList<Person>();

                foreach (Person prs in list)
                {
                    this.SaveOperatorCollectiveRequestRegistType(collectiveRequestObj, prs.ID);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUserSettings", "SetOperatorCollectiveRequestRegistType");
                throw ex;
            }
        }

    }
}
