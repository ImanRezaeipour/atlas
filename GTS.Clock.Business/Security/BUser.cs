using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Security;
using GTS.Clock.Model;
using GTS.Clock.Model.AppSetting;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Exceptions;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.NHibernateFramework;
using System.Web;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Report;
using GTS.Clock.Business.Reporting;
using GTS.Clock.Business.Rules;
using NHibernate;
using GTS.Clock.Business.RequestFlow;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using AtlasEncryption;

namespace GTS.Clock.Business.Security
{
    public class BUser : BaseBusiness<User>, IDataAccess
    {
        private const string ExceptionSrc = "GTS.Clock.Business.Security.BUser";
        UserRepository userRepository = new UserRepository(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        UserRepository Rep = new UserRepository(false);
        private const string PasswordFormatEnabled = "PasswordFormatEnabled";
        #region Login Services

        /// <summary>
        /// :اربر فعلی را برمیگرداند
        /// اگر خالی باشد تهی برمیگرداند
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                try
                {
                    string username = "";

                    GTS.Clock.Infrastructure.Log.BusinessServiceLogger loggerr = new Infrastructure.Log.BusinessServiceLogger();
                    StringBuilder log = new StringBuilder();

                    if (!SessionHelper.HasSessionValue("UserIdentity") &&
                        System.Web.HttpContext.Current != null &&
                        System.Web.HttpContext.Current.User != null &&
                        System.Web.HttpContext.Current.User.Identity != null &&
                        System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        SessionHelper.SaveSessionValue("UserIdentity", System.Web.HttpContext.Current.User.Identity.Name);
                        SessionHelper.ClearSessionValue(SessionHelper.BussinessCurentUser);
                        log.AppendLine("Get Username: " + System.Web.HttpContext.Current.User.Identity.Name);
                    }
                    if (SessionHelper.HasSessionValue("UserIdentity"))
                    {
                        username = SessionHelper.GetSessionValue("UserIdentity").ToString();
                        username = username.Replace("TEHRAN\\", "");
                    }
                    //if (System.Web.HttpContext.Current != null &&
                    //    System.Web.HttpContext.Current.User != null &&
                    //    System.Web.HttpContext.Current.User.Identity != null)
                    //{
                    //    username = System.Web.HttpContext.Current.User.Identity.Name;
                    //}
                    else
                    {
                        username = "NUnitUser";
                    }
                    //LogException(new Exception("BUser.CurrentUser" + username));
                    /* log.AppendLine("username: " + username);
                     log.AppendLine("HasSessionValue: " + SessionHelper.HasSessionValue("UserIdentity").ToString());
                     log.AppendLine("HttpContext.Current: " + (System.Web.HttpContext.Current == null ? "null" : "OK"));
                     log.AppendLine("HttpContext.Current.User: " + (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User != null ? "OK" : "null"));
                     log.AppendLine("Current.User.Identity: " + (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null ? "OK" : "null"));
                     log.AppendLine("User.Identity.IsAuthenticated: " + (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated ? "OK" : "False"));
                     loggerr.Error("", log.ToString());
                     loggerr.Flush() */
                    if (!SessionHelper.HasSessionValue(SessionHelper.BussinessCurentUser))
                    {
                        UserRepository userRep = new UserRepository(false);
                        Model.Security.User proxyUser = new User();
                        proxyUser.Person = new Person();
                        proxyUser.Role = new Role();

                        Model.Security.User user = userRep.GetByUserName(Utility.GetSimpleUsername(username));
                        if (user == null || user.ID == 0)
                        {
                            user = new User();
                            user.UserName = "";
                            user.Role = new Role();
                            user.Person = new GTS.Clock.Model.Person();
                        }
                        if (user.Role == null)
                        {
                            RoleRepository roleRep = new RoleRepository();
                            Role role = roleRep.GetByRoleCode(((int)RoleCustomCodeType.User).ToString());
                            user.Role = role;
                            user = userRep.SaveOrUpdate(user);
                            NHibernateSessionManager.Instance.GetSession().Evict(role);
                        }
                        proxyUser.ID = user.ID;
                        proxyUser.UserName = user.UserName;
                        proxyUser.Role.ID = user.Role.ID;
                        proxyUser.Role.CustomCode = user.Role.CustomCode;
                        proxyUser.Role.Name = user.Role.Name;
                        proxyUser.Person.ID = user.Person.ID;
                        proxyUser.Person.FirstName = user.Person.FirstName;
                        proxyUser.Person.LastName = user.Person.LastName;
                        proxyUser.Person.PersonCode = user.Person.PersonCode;
                        proxyUser.Person.Sex = user.Person.Sex;
                        SessionHelper.SaveSessionValue(SessionHelper.BussinessCurentUser, proxyUser);
                        //NHibernateSessionManager.Instance.ClearSession();
                        return proxyUser;
                    }
                    object obj = SessionHelper.GetSessionValue(SessionHelper.BussinessCurentUser);
                    if (obj != null)
                        return (User)obj;
                    else
                    {
                        Model.Security.User user = new User();
                        user = new User();
                        user.UserName = "";
                        user.Role = new Role();
                        user.Person = new GTS.Clock.Model.Person();
                        return user;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// نشست مربوط به کاربر را خالی میکند
        /// </summary>
        public static void ClearUserCach()
        {
            SessionHelper.ClearAllCachedData();
        }

        /// <summary>
        /// اگر شناسه کاربری موجود نباشد تهی برمیگرداند
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetByUsername(string username)
        {
            User user = userRepository.GetByUserName(username);
            return user;
        }
        public IList<User> GetByPersonId(decimal personId)
        {
            IList<User> userList = userRepository.GetByPersonId(personId);
            return userList;
        }
        public decimal GetPersonIdByUsername(string username)
        {
            User user = userRepository.GetByUserName(username);
            if (user != null && user.Person != null)
                return user.Person.ID;
            return 0;
        }

        /// <summary>
        ///  باید پروایدر آن تغییر کنداگر شخص قرار است از اکتیو دایرکتوری استفاده کند 
        /// </summary>
        /// <param name="username"></param>
        public MembershipProviders GetDefaultAuthenticationProvider(string username)
        {
            //if (this.ActiveDirectoryISAllowed(username))
            //{
            //    return MembershipProviders.ADMembershipProvider;
            //}
            return MembershipProviders.GTSMembershipProvider;
        }

        /// <summary>
        /// تنظیم نام کاربری کامل در دامین
        /// </summary>
        /// <param name="username"></param>
        public string GetCompleteDoaminUsername(string username)
        {
            username = Utility.GetSimpleUsername(username);
            User user = this.GetByUsername(username);
            if (user.ActiveDirectoryAuthenticate)
            {
                string doamin = "";// this.GetDomainInfo;
                if (doamin.Length > 0)
                {
                    return username + "@" + doamin;
                }
            }
            return username;
        }

        /// <summary>
        /// تعویض کلمه عبور
        /// </summary>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="reNewPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string currentPassword, string newPassword, string reNewPassword)
        {
            using (NHibernateSessionManager.Instance.BeginTransactionOn())
            {
                try
                {
                    UIValidationExceptions exception = new UIValidationExceptions();

                    Crypto crypto = new Crypto();

                    if (Utility.IsEmpty(crypto.DecryptStringAES(newPassword)) || Utility.IsEmpty(crypto.DecryptStringAES(newPassword)))
                    {
                        exception.Add(ExceptionResourceKeys.UserPasswordIsNull, "Password is null", ExceptionSrc);
                    }
                    if (!newPassword.Equals(reNewPassword))
                    {
                        exception.Add(ExceptionResourceKeys.UserConfirmPasswordNotEqual, "ConfirmPassword is not equal to password", ExceptionSrc);
                    }

                    if (exception.Count == 0)
                        this.ValidatePasswordFormat(crypto.DecryptStringAES(newPassword), ref exception);

                    if (exception.Count > 0)
                        throw exception;

                    GTSMembershipProvider mermbership = new GTSMembershipProvider();
                    bool success = mermbership.ChangePassword(BUser.CurrentUser.UserName, currentPassword, newPassword);
                    if (!success)
                    {
                        exception.Add(ExceptionResourceKeys.UserPasswordIsNotCorrect, "password is not corrent", ExceptionSrc);
                        throw exception;
                    }
                    NHibernateSessionManager.Instance.CommitTransactionOn();
                    LogUserAction("Edit");
                    return success;
                }
                catch (Exception ex)
                {
                    LogException(ex, "BUser", "ChangePassword");
                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                    throw ex;
                }
            }
        }

        private void ValidatePasswordFormat(string password, ref UIValidationExceptions exception)
        {
            try
            {
                bool isPasswordFormatEnabled = false;
                bool.TryParse(WebConfigurationManager.AppSettings[PasswordFormatEnabled], out isPasswordFormatEnabled);
                if (isPasswordFormatEnabled)
                {
                    PasswordFormat passwordFormat = NHSession.QueryOver<PasswordFormat>().List().FirstOrDefault();
                    if (passwordFormat != null)
                    {
                        if (passwordFormat.ContainsWords)
                        {
                            if (passwordFormat.ContainsNumbers)
                            {
                                if (!passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(password, @"^[A-Za-z0-9]*$"))
                                        exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordMustContainsWordOrNumber, "کلمه عبور فقط باید شامل حرف یا عدد باشد", ExceptionSrc));
                                }
                            }
                            else
                            {
                                if (passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(password, @"^([^0-9]*$)"))
                                        exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordMustNotContainsNumber, "کلمه عبور نباید شامل عدد باشد", ExceptionSrc));
                                }
                                else
                                {
                                    if (!Regex.IsMatch(password, @"^[A-Za-z]*$"))
                                        exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordMustContainsOnlyWord, "کلمه عبور فقط باید شامل حرف باشد", ExceptionSrc));
                                }
                            }
                        }
                        else
                        {
                            if (passwordFormat.ContainsNumbers)
                            {
                                if (passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(password, @"^([^A-Za-z]*$)"))
                                        exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordMustNotContainsWord, "کلمه عبور نباید شامل حرف باشد", ExceptionSrc));
                                }
                                else
                                {
                                    if (!Regex.IsMatch(password, @"^[0-9]*$"))
                                        exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordMustContainsOnlyNumber, "کلمه عبور فقط باید شامل عدد باشد", ExceptionSrc));
                                }
                            }
                            else
                            {
                                if (passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(password, @"^([^A-Za-z0-9]*$)"))
                                        exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordMustNotContainsWordOrNumber, "کلمه عبور نباید شامل حرف یا عدد باشد", ExceptionSrc));
                                }
                            }
                        }
                        if (exception.Count == 0)
                        {
                            ValidationException validationException = null;
                            if (passwordFormat.MinLength > 0 && password.Length < passwordFormat.MinLength)
                            {
                                validationException = new ValidationException(ExceptionResourceKeys.UserPasswordMinCharacterLength, "حداقل تعداد کاراکتر مجاز برای کلمه عبور", ExceptionSrc);
                                validationException.Data.Add("Info", passwordFormat.MinLength);
                            }
                            if (passwordFormat.MaxLength > 0 && password.Length > passwordFormat.MaxLength)
                            {
                                validationException = new ValidationException(ExceptionResourceKeys.UserPasswordMaxCharacterLength, "حداکثر تعداد کاراکتر مجاز برای کلمه عبور", ExceptionSrc);
                                validationException.Data.Add("Info", passwordFormat.MaxLength);
                            }
                            if (validationException != null)
                                exception.Add(validationException);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, ExceptionSrc, "ValidatePasswordFormat");
                throw;
            }
        }
        public bool ValidatePasswordFormat()
        {
            string Password = string.Empty;
            bool InconsistentPasswordFormat = false;
            bool IsPasswordFormatEnabled = false;
            object Usernameobj = SessionHelper.GetSessionValue(SessionHelper.LoginUsername);
            if (Usernameobj != null)
            {
                CryptData cryptData = new CryptData((string)Usernameobj);
                object Paswordobj = SessionHelper.GetSessionValue(SessionHelper.LoginPassword);
                if (Paswordobj != null)
                    Password = cryptData.DecryptData((string)Paswordobj);
                bool.TryParse(WebConfigurationManager.AppSettings[PasswordFormatEnabled], out IsPasswordFormatEnabled);
                if (IsPasswordFormatEnabled)
                {
                    PasswordFormat passwordFormat = NHSession.QueryOver<PasswordFormat>().List().FirstOrDefault();
                    if (passwordFormat != null)
                    {
                        if (passwordFormat.ContainsWords)
                        {
                            if (passwordFormat.ContainsNumbers)
                            {
                                if (!passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(Password, @"^[A-Za-z0-9]*$"))
                                        InconsistentPasswordFormat = true;
                                }
                            }
                            else
                            {
                                if (passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(Password, @"^([^0-9]*$)"))
                                        InconsistentPasswordFormat = true;
                                }
                                else
                                {
                                    if (!Regex.IsMatch(Password, @"^[A-Za-z]*$"))
                                        InconsistentPasswordFormat = true;
                                }
                            }
                        }
                        else
                        {
                            if (passwordFormat.ContainsNumbers)
                            {
                                if (passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(Password, @"^([^A-Za-z]*$)"))
                                        InconsistentPasswordFormat = true;
                                }
                                else
                                {
                                    if (!Regex.IsMatch(Password, @"^[0-9]*$"))
                                        InconsistentPasswordFormat = true;
                                }
                            }
                            else
                            {
                                if (passwordFormat.ContainsSymbols)
                                {
                                    if (!Regex.IsMatch(Password, @"^([^A-Za-z0-9]*$)"))
                                        InconsistentPasswordFormat = true;
                                }
                            }
                        }
                        if (!InconsistentPasswordFormat)
                        {
                            if (passwordFormat.MinLength > 0 && Password.Length < passwordFormat.MinLength)
                            {
                                InconsistentPasswordFormat = true;
                            }
                            if (passwordFormat.MaxLength > 0 && Password.Length > passwordFormat.MaxLength)
                            {
                                InconsistentPasswordFormat = true;
                            }
                        }
                    }
                }
            }
            return InconsistentPasswordFormat;
        }

        /// <summary>
        /// شناسه کاربر جاری
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserName()
        {
            return BUser.CurrentUser.UserName;
        }

        #endregion

        #region List And Search

        /// <summary>
        /// تعداد پرسنل را باتوجه به کلمه جستجو برمیگرداند
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public int QuickSearchPersonCount(string searchKey)
        {
            try
            {
                ISearchPerson bperson = new BPerson();
                int count = bperson.GetPersonInQuickSearchCount(searchKey);
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "QuickSearchPersonCount");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد کل کاربران
        /// </summary>
        /// <returns></returns>
        public int GetAllUsersCount()
        {
            try
            {
                ISearchPerson bperson = new BPerson();
                int count = bperson.GetPersonCount();
                return count;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllUsersCount");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد نتایج جستجو را برمیگرداند
        /// </summary>
        /// <param name="key"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int GetAllByPageBySearchCount(UserSearchKeys key, string searchValue)
        {
            try
            {
                //DNN Note:Improve Performance
                IDataAccess dataAccess = new BUser();
                int userCount = 0;
                userCount = userRepository.GetNumberOfUsersByQuickSearch(key, searchValue, BUser.CurrentUser.ID);
                return userCount;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllByPageBySearchCount");
                throw ex;
            }
        }

        /// <summary>
        /// در بین پرسنلی جستجوی سریع انجام میدهد
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns></returns>
        public IList<Person> QuickSearchPersonByPage(string searchKey, int pageIndex, int pageSize)
        {
            try
            {
                ISearchPerson bperson = new BPerson(SysLanguageResource.English, LocalLanguageResource.English);
                IList<Person> list = bperson.QuickSearchByPage(pageIndex, pageSize, searchKey);
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "QuickSearchPersonByPage");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<UserProxy> GetAllByPage(int startIndex, int pageSize)
        {
            try
            {
                //DNN Note:Improve Performance
                //IDataAccess dataAccess = new BUser();
                //int userCount = 0;
                //decimal[] arr = dataAccess.GetAccessiblePersonByDepartment().ToArray();
                IList<User> list = userRepository.GetAllByPage(BUser.CurrentUser.ID, startIndex, pageSize);
                IList<UserProxy> users = new List<UserProxy>();
                for (int i = 0; i < list.Count; i++)
                {
                    User user = list[i];
                    UserProxy proxy = new UserProxy(user);
                    if (proxy.Active && !Utility.IsEmpty(user.Person.EndEmploymentDate))
                    {
                        proxy.Active = DateTime.Now > user.Person.EndEmploymentDate ? false : true;
                    }
                    users.Add(proxy);
                }
                return users;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllUsers");
                throw ex;
            }
        }

        /// <summary>
        /// نتایج جستجو
        /// </summary>
        /// <param name="key"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IList<UserProxy> GetAllByPageBySearch(UserSearchKeys key, string searchValue, int pageIndex, int pageSize)
        {
            try
            {
                //DNN Note:Improve Performance
                IList<User> list = null;
                //IDataAccess dataAccess = new BUser();
                list = userRepository.GetAllByPageQuickSearch(key, searchValue, BUser.CurrentUser.ID, pageIndex, pageSize);
                if (list != null)
                {
                    IList<UserProxy> users = new List<UserProxy>();
                    foreach (User user in list)
                    {
                        UserProxy proxy = new UserProxy(user);
                        if (proxy.Active && !Utility.IsEmpty(user.Person.EndEmploymentDate))
                        {
                            if (DateTime.Now > user.Person.EndEmploymentDate)
                            {
                                proxy.Active = false;
                                user.Active = false;
                                this.SaveChanges(user, UIActionType.EDIT);
                            }
                            else
                                proxy.Active = true;
                        }
                        users.Add(proxy);
                    }

                    return users;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogException(ex, "BManager", "GetAllByPageBySearch");
                throw ex;
            }
        }

        /// <summary>
        /// درخت نقشها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public Role GetRoleTree()
        {
            try
            {
                BRole brole = new BRole();
                return brole.GetRoleTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetRoleTree");
                throw ex;
            }
        }

        /// <summary>
        /// لیست دامین های نوجود در سرور را برمیگرداند
        /// </summary>
        /// <returns></returns>
        public IList<Domains> GetActiveDirectoryDomains()
        {
            try
            {
                EntityRepository<Domains> rep = new EntityRepository<Domains>();
                IList<Domains> list = rep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new Domains().Active), true));
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetActiveDirectoryDomains");
                throw ex;
            }
        }

        /// <summary>
        /// لیست کاربران یک دامین را برمیگرداند
        /// </summary>
        /// <param name="doaminName"></param>
        /// <returns></returns>
        public IList<string> GetActiveDirectoryUsers(decimal domianId)
        {
            try
            {
                IList<string> result = new List<string>();
                BActiveDirectory bAD = new BActiveDirectory();
                Domains domain = bAD.GetById(domianId);

                LdapAuthentication ldap = new LdapAuthentication(domain.Domain, domain.UserName, domain.Password);
                result = ldap.GetAllDomainUsers();
                if (result != null)
                {
                    result = result.OrderBy(a => a).ToList();
                    //Array.Sort<string>(result.ToArray());
                }
                return result;
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetActiveDirectoryUsers");
                throw ex;
            }
        }

        #endregion


        /// <summary>
        /// درج
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertUser(UserProxy proxy)
        {
            try
            {
                User user = UserProxy.Export(proxy);
                if (!proxy.ActiveDirectoryAuthenticate)
                {
                    Crypto cryptoObj = new Crypto();
                    user.Password = cryptoObj.DecryptStringAES(user.Password);
                    user.ConfirmPassword = cryptoObj.DecryptStringAES(user.ConfirmPassword);
                }
                return this.SaveChanges(user, UIActionType.ADD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal EditUser(UserProxy proxy)
        {
            try
            {
                User user = UserProxy.Export(proxy);
                if (!proxy.ActiveDirectoryAuthenticate)
                {
                    Crypto cryptoObj = new Crypto();
                    user.Password = cryptoObj.DecryptStringAES(user.Password);
                    user.ConfirmPassword = cryptoObj.DecryptStringAES(user.ConfirmPassword);
                }
                return this.SaveChanges(user, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "EditUser");
                throw ex;
            }
        }

        /// <summary>
        /// بروز رسانی نقش کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public decimal EditUser(decimal userId, decimal roleId)
        {
            try
            {
                User user = this.GetByID(userId);
                user.Role = new Role() { ID = roleId };
                return this.SaveChanges(user, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "EditUser Role");
                throw ex;
            }
        }

        /// <summary>
        /// حذف
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteUser(UserProxy proxy)
        {
            try
            {
                User user = UserProxy.Export(proxy);
                return this.SaveChanges(user, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void InsertValidate(User user)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (user.Person == null || user.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPersonIsNotSpecified, "درج - Person not Provided", ExceptionSrc));
            }
            if (user.Role == null || user.Role.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserRoleIsNotSpecified, "درج - Person not Provided", ExceptionSrc));
            }
            if (Utility.IsEmpty(user.UserName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameNotProvided, "درج - Username not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && Utility.IsEmpty(user.Password))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordIsNull, "درج - Password not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && user.Password != user.ConfirmPassword)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserConfirmPasswordNotEqual, "درج - ConfirmPassword is not equal to password", ExceptionSrc));
            }

            Person personAlias = null;
            User userAlias = null;
            if (NHibernateSessionManager.Instance.GetSession().QueryOver<User>(() => userAlias)
                                                              .JoinAlias(() => userAlias.Person, () => personAlias)
                                                              .Where(() => userAlias.UserName == user.UserName &&
                                                                           userAlias.Active &&
                                                                          !personAlias.IsDeleted
                                                                    )
                                                              .RowCount() > 0
               )
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameReplication, "درج - Username Replication", ExceptionSrc));
            }

            if (exception.Count == 0 && !user.IsAutomaticGenerated)
                this.ValidatePasswordFormat(user.OriginalPassword, ref exception);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(User user)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (user.Person == null || user.Person.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPersonIsNotSpecified, "بروزرسانی - Person not Provided", ExceptionSrc));
            }
            if (user.Role == null || user.Role.ID == 0)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserRoleIsNotSpecified, "بروزرسانی - Person not Provided", ExceptionSrc));
            }
            if (Utility.IsEmpty(user.UserName))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameNotProvided, "بروزرسانی - Username not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && Utility.IsEmpty(user.Password))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserPasswordIsNull, "بروزرسانی - Password not Provided", ExceptionSrc));
            }
            else if (!user.ActiveDirectoryAuthenticate && user.IsPasswodChanged && user.Password != user.ConfirmPassword)
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UserConfirmPasswordNotEqual, "بروزرسانی - ConfirmPassword is not equal to password", ExceptionSrc));
            }

            Person personAlias = null;
            User userAlias = null;
            if (NHibernateSessionManager.Instance.GetSession().QueryOver<User>(() => userAlias)
                                                              .JoinAlias(() => userAlias.Person, () => personAlias)
                                                              .Where(() => userAlias.UserName == user.UserName &&
                                                                           userAlias.Active &&
                                                                           userAlias.ID != user.ID &&
                                                                          !personAlias.IsDeleted
                                                                    )
                                                              .RowCount() > 0
               )
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.UsernameReplication, "بروزرسانی - Username Replication", ExceptionSrc));
            }

            if (!user.Active)
            {
                BManager bManager = new BManager();
                BPerson bPerson = new BPerson();
                Person person = bPerson.GetByID(user.Person.ID);
                if (person.OrganizationUnit != null && person.OrganizationUnit.ID != 0)
                    person.OldOrganizationUnitId = person.OrganizationUnit.ID;
                IList<User> personUsers = this.GetPersonUsers(person.ID);
                if (personUsers.Count() == 1 && personUsers[0].ID == user.ID && personUsers[0].Active && bManager.CheckPersonUsedInFlowAsManager(person))
                    exception.Add(new ValidationException(ExceptionResourceKeys.PersonUsedInFlowAsManager, "پرسنل یا پست سازمانی منتسب به پرسنل در جریان کاری بعنوان مدیر تعریف شده است", ExceptionSrc));
                this.NHSession.Evict(person);
                if (personUsers.Count > 0)
                {
                    foreach (User personUserItem in personUsers)
                    {
                        this.NHSession.Evict(personUserItem);
                    }
                }
                User User = NHSession.QueryOver<User>()
                                      .Where(x => x.ID == user.ID)
                                      .SingleOrDefault();
                NHSession.Evict(User);
                if (User.Person.ID != user.Person.ID)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.UserNameNotAbleEdit, "نام پرسنلی کاربر قابل ویرایش نمی باشد", ExceptionSrc));
                }

            }

            if (exception.Count == 0 && user.IsPasswodChanged)
                this.ValidatePasswordFormat(user.OriginalPassword, ref exception);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(User user)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            BManager bManager = new BManager();
            BPerson bPerson = new BPerson();
            Person person = bPerson.GetByID(user.Person.ID);
            if (person.OrganizationUnit != null && person.OrganizationUnit.ID != 0)
                person.OldOrganizationUnitId = person.OrganizationUnit.ID;
            IList<User> personUsers = this.GetPersonUsers(person.ID);
            if (personUsers.Count() == 1 && personUsers[0].ID == user.ID && bManager.CheckPersonUsedInFlowAsManager(person))
                exception.Add(new ValidationException(ExceptionResourceKeys.PersonUsedInFlowAsManager, "پرسنل یا پست سازمانی منسب به پرسنل در جریان کاری بعنوان مدیر تعریف شده است", ExceptionSrc));
            this.NHSession.Evict(person);
            if (personUsers.Count > 0)
            {
                foreach (User personUserItem in personUsers)
                {
                    this.NHSession.Evict(personUserItem);
                }
            }

            User usr = this.GetByUsername(HttpContext.Current.User.Identity.Name);
            if (user.ID == usr.ID)
                exception.Add(new ValidationException(ExceptionResourceKeys.CurrentUserDeleteNotAllowed, "کاربر جاری مجاز به حذف اکانت کاربری خود نمی باشد", ExceptionSrc));
            this.NHSession.Evict(usr);

            if (exception.Count > 0)
            {
                throw exception;
            }
        }
        protected override bool Delete(User user)
        {
            try
            {
                Rep.DeleteDataAccessManagerList(user);
                return true;
            }
            catch (Exception ex)
            {
                LogException(ex, "Buser", "Delete");
                throw ex;
            }
        }
        private IList<User> GetPersonUsers(decimal personID)
        {
            Person personAlias = null;
            User userAlias = null;
            IList<User> userList = new List<User>();
            userList = this.NHSession.QueryOver<User>(() => userAlias)
                                     .JoinAlias(() => userAlias.Person, () => personAlias)
                                     .Where(() => !personAlias.IsDeleted &&
                                                   personAlias.Active &&
                                                   userAlias.Active &&
                                                   personAlias.ID == personID
                                           )
                                     .List<User>();

            return userList;
        }

        protected override void GetReadyBeforeSave(User user, UIActionType action)
        {

            if (!Utility.IsEmpty(user.Password) && (action == UIActionType.ADD || (action == UIActionType.EDIT && user.IsPasswodChanged)))
            {
                user.OriginalPassword = user.Password;
                user.Password = Utility.GetHashedCode(user.Password);
                if (Utility.VerifyHashCode(user.ConfirmPassword, user.Password))
                {
                    //این تساوی بعدا در اعتبارسنجی بررسی میشود
                    user.ConfirmPassword = user.Password;
                }
            }
            ///ممکن است شخص نخواهد کلمه عبور را ویرایش کند
            else if (action == UIActionType.EDIT && !user.IsPasswodChanged)
            {
                user.Password = userRepository.GetPasswordByUserId(user.ID);
            }
            if (action == UIActionType.ADD)
            {
                user.LastActivityDate = DateTime.Now;
            }
            else if (action == UIActionType.EDIT)
            {
                user.LastActivityDate = userRepository.GetLastActivityDateByUserId(user.ID);
                if (Utility.IsEmpty(user.LastActivityDate))
                {
                    user.LastActivityDate = DateTime.Now;
                }
            }


        }

        protected override void OnSaveChangesSuccess(User user, UIActionType action)
        {
            try
            {
                if (action == UIActionType.ADD) //ایجاد تنظیمات
                {
                    foreach (int ssi in Enum.GetValues(typeof(SubSystemIdentifier)))
                    {
                        UserSettings userSettings = new UserSettings();
                        userSettings.Language = new Languages() { ID = BLanguage.GetCurrentSystemLanguage().ID };
                        userSettings.User = user;
                        userSettings.SubSystemID = ssi;
                        EntityRepository<UserSettings> setRep = new EntityRepository<UserSettings>(false);
                        setRep.Save(userSettings);
                    }

                }
            }
            catch (Exception ex)
            {
                LogException(ex, "UserSettings Creation");
                throw ex;
            }
        }

        #region IDataAccess Members

        /// <summary>
        /// بخشهایی که اجازه دسترسی به آنها دارد به همراه پدران و بچه ها را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<decimal> IDataAccess.GetAccessibleDeparments()
        {
            BApplicationSettings.CheckGTSLicense();

            if (userRepository.HasAllDepartmentAccess(this.CurrentUserId))
            {
                IList<Department> list = new DepartmentRepository(false).GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                BDepartment bDepartment = new BDepartment();
                List<decimal> depList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                depList.AddRange(userRepository.GetUserDepartmentList(this.CurrentUserId));
                foreach (decimal depId in depList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<Department> childs = new List<Department>();
                    childs = bDepartment.GetDepartmentChildsByParentPath(depId);
                    childAndParentsList.AddRange(bDepartment.GetByID(depId).ParentPathList);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                depList.AddRange(childAndParentsList);
                return depList.Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleRoles()
        {
            BApplicationSettings.CheckGTSLicense();

            if (userRepository.HasAllRoleAccess(this.CurrentUserId) > 0)
            {
                IList<Role> list = new RoleRepository(false).GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                BRole bRole = new BRole();
                List<decimal> roleList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                roleList.AddRange(userRepository.GetUserRoleList(this.CurrentUserId));
                foreach (decimal roleId in roleList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<Role> childs = new List<Role>();
                    childs = bRole.GetRoleChildsByParentPath(roleId);
                    childAndParentsList.AddRange(bRole.GetByID(roleId).ParentPathList);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                roleList.AddRange(childAndParentsList);
                return roleList.Distinct().ToList();
            }
        }


        IList<decimal> IDataAccess.GetAccessibleOrgans()
        {
            if (userRepository.HasAllOrganAccess(this.CurrentUserId))
            {
                IList<OrganizationUnit> list = new BOrganizationUnit().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                //DNN Note:Improve Performance
                //IDataAccess asscessPort = new BUser();
                BOrganizationUnit borgan = new BOrganizationUnit();

                List<decimal> organList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                organList.AddRange(userRepository.GetUserOrganList(this.CurrentUserId));
                foreach (decimal organId in organList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<OrganizationUnit> childs = new List<OrganizationUnit>();
                    childs = borgan.GetOrganiztionChildsByParentPath(organId);
                    childAndParentsList.AddRange(borgan.GetByID(organId).ParentPathList);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                organList.AddRange(childAndParentsList);
                return organList.Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleWorkGroups()
        {
            if (userRepository.HasAllWorkGroupAccess(this.CurrentUserId) > 0)
            {
                IList<WorkGroup> list = new WorkGroupRepository(false).GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                return userRepository.GetUserWorkGroupIdList(this.CurrentUserId).Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleEmploymentTypes()
        {
            if (userRepository.HasAllEmploymentTypesAccess(this.CurrentUserId) > 0)
            {
                IList<EmploymentType> list = new EntityRepository<EmploymentType>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
                return userRepository.GetUserEmployTypeIdsList(this.CurrentUserId).Distinct().ToList();
        }

        IList<decimal> IDataAccess.GetAccessibleCostCenters()
        {
            if (userRepository.HasAllCostCentersAccess(this.CurrentUserId) > 0)
            {
                IList<CostCenter> list = new EntityRepository<CostCenter>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
                return userRepository.GetUserCostCenterIdsList(this.CurrentUserId).Distinct().ToList();
        }

        IList<decimal> IDataAccess.GetAccessibleRuleGroups()
        {
            if (userRepository.HasAllRuleGroupAccess(this.CurrentUserId) > 0)
            {
                IList<RuleCategory> list = new EntityRepository<RuleCategory>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                //return userRepository.GetUserRuleGroupIdList(this.CurrentUserId);
                BRuleCategory bRuleCat = new BRuleCategory();
                List<decimal> ruleCatList = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                ruleCatList.AddRange(userRepository.GetUserRuleGroupIdList(this.CurrentUserId));
                foreach (decimal ruleCatId in ruleCatList)//اضافه کردن بچه ها و والد ها 
                {
                    IList<RuleCategory> childs = new List<RuleCategory>();
                    //childs = bRuleCat.GetReportChildsByParentPath(ruleCatId);
                    childAndParentsList.Add(bRuleCat.GetByID(ruleCatId).Parent.ID);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                ruleCatList.AddRange(childAndParentsList);
                return ruleCatList.Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleShifts()
        {
            if (userRepository.HasAllShiftAccess(this.CurrentUserId) > 0)
            {
                IList<Shift> list = new EntityRepository<Shift>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                return userRepository.GetUserShiftIdList(this.CurrentUserId).Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessiblePrecards()
        {
            if (userRepository.HasAllPrecardAccess(this.CurrentUserId) > 0)
            {
                IList<Precard> list = new EntityRepository<Precard>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                return userRepository.GetUserPrecardIdList(this.CurrentUserId).Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleControlStations()
        {
            if (userRepository.HasAllControlStationAccess(this.CurrentUserId) > 0)
            {
                IList<ControlStation> list = new EntityRepository<ControlStation>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                return userRepository.GetUserControlStationIDList(this.CurrentUserId).Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleControlStations(decimal userID)
        {
            if (userRepository.HasAllControlStationAccess(userID) > 0)
            {
                IList<ControlStation> list = new EntityRepository<ControlStation>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                return userRepository.GetUserControlStationIDList(userID).Distinct().ToList();
            }
        }


        IList<decimal> IDataAccess.GetAccessibleDoctors()
        {
            if (userRepository.HasAllDoctorAccess(this.CurrentUserId) > 0)
            {
                IList<Doctor> list = new EntityRepository<Doctor>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                return userRepository.GetUserDoctorIdList(this.CurrentUserId).Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleManagers()
        {
            if (userRepository.HasAllManagerAccess(this.CurrentUserId) > 0)
            {
                IList<Manager> list = new ManagerRepository(false).GetAll().Where(x => x.Active).ToList();
                var ids = from mng in list
                          select mng.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                return userRepository.GetUserManagerIdList(this.CurrentUserId).Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleFlows()
        {
            IList<Flow> list = new EntityRepository<Flow>().GetAll();
            if (userRepository.HasAllFlowAccess(this.CurrentUserId) > 0)
            {
                var ids = from obj in list
                          where obj.IsDeleted == false
                          select obj.ID;

                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                IList<decimal> userRepositoryGetUserFlowIdList = userRepository.GetUserFlowIdList(this.CurrentUserId);
                var ids = from o in list
                          where userRepositoryGetUserFlowIdList.Any(x => x == o.ID) && o.IsDeleted == false
                          select o.ID;

                return ids.ToList<decimal>().Distinct().ToList();
            }
        }

        IList<decimal> IDataAccess.GetAccessibleReports()
        {
            if (userRepository.HasAllReportAccess(this.CurrentUserId) > 0)
            {
                IList<Report> list = new EntityRepository<Report>().GetAll();
                var ids = from obj in list
                          select obj.ID;
                return ids.ToList<decimal>().Distinct().ToList();
            }
            else
            {
                BReport bReport = new BReport();
                List<decimal> reportListId = new List<decimal>();
                List<decimal> childAndParentsList = new List<decimal>();
                IList<Report> ReportRoleAccesslist = new List<Report>();
                IList<Report> ReportRoleManagmentAccesslist = new List<Report>();
                Report reportAlias = null;
                Role roleAlias = null;
                ReportRoleAccesslist = NHSession.QueryOver(() => reportAlias)
                                                .JoinAlias(() => reportAlias.AccessRoleList, () => roleAlias)
                                                .Where(() => roleAlias.ID == BUser.CurrentUser.Role.ID && reportAlias.SubSystemId == SubSystemIdentifier.TimeAtendance).List<Report>();
                reportListId.AddRange(userRepository.GetUserReportIdList(this.CurrentUserId));
                Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);
                List<decimal> roleManagmentListId = new List<decimal>();
                if (managementState.ContainsKey("ManagerRoleId"))
                {
                    roleManagmentListId.Add(Utility.ToDecimal(managementState["ManagerRoleId"]));
                }
                if (managementState.ContainsKey("SubstituteRoleId"))
                {
                    roleManagmentListId.Add(Utility.ToDecimal(managementState["SubstituteRoleId"]));
                }
                if (managementState.ContainsKey("OperatorRoleId"))
                {
                    roleManagmentListId.Add(Utility.ToDecimal(managementState["OperatorRoleId"]));
                }
                foreach (decimal rolemanagmentId in roleManagmentListId)
                {


                    ReportRoleManagmentAccesslist = NHSession.QueryOver(() => reportAlias)
                                                .JoinAlias(() => reportAlias.AccessRoleList, () => roleAlias)
                                                .Where(() => roleAlias.ID == rolemanagmentId && reportAlias.SubSystemId == SubSystemIdentifier.TimeAtendance).List<Report>();
                    foreach (Report item in ReportRoleManagmentAccesslist)
                    {
                        if (ReportRoleAccesslist.Count(r => r.ID == item.ID) == 0)
                            ReportRoleAccesslist.Add(item);
                    }
                }


                foreach (Report item in ReportRoleAccesslist)
                {
                    if (reportListId.Count(r => r == item.ID) == 0)
                        reportListId.Add(item.ID);
                }

                foreach (decimal reportId in reportListId)//اضافه کردن بچه ها و والد ها 
                {
                    IList<Report> childs = new List<Report>();
                    childs = bReport.GetReportChildsByParentPath(reportId);
                    Report report = bReport.GetByID(reportId);
                    childAndParentsList.AddRange(report.ParentPathList);
                    NHibernateSessionManager.Instance.GetSession().Evict(report);
                    var ids = from child in childs
                              select child.ID;
                    childAndParentsList.AddRange(ids.ToList<decimal>());
                }
                reportListId.AddRange(childAndParentsList);

                return reportListId.Distinct().ToList();
            }
        }

        /// <summary>
        /// تمام پرسنل منتسب به بخشهای یک کاربر را بصورت سلسله مراتبی استخراج میکند
        /// </summary>
        /// <returns></returns>
        IList<decimal> IDataAccess.GetAccessiblePersonByDepartment()
        {
            //DNN Note:Improve Performance
            BDepartment bDepartment = new BDepartment();
            //IDataAccess port = new BUser();
            List<decimal> personList = new List<decimal>();
            //IList<decimal> depList = port.GetAccessibleDeparments();
            IList<decimal> depList = (this as IDataAccess).GetAccessibleDeparments();
            foreach (decimal depId in depList)
            {
                Department dep = bDepartment.GetByID(depId);
                var persons = from prs in dep.PersonList
                              select prs.ID;
                personList.AddRange(persons.ToList());
            }
            return personList.Distinct().ToList();
        }

        /// <summary>
        /// تمام پرسنل منتسب به بخشهای یک کاربر و ایستگاه کنترل را بصورت سلسله مراتبی استخراج میکند
        /// </summary>
        /// <returns></returns>
        IList<decimal> IDataAccess.GetAccessiblePersonByDepartmentAndControlSatation()
        {
            BDepartment bDepartment = new BDepartment();
            //DNN Note:Improve Performance
            //IDataAccess port = new BUser();
            List<decimal> personList = new List<decimal>();
            //IList<decimal> depList = port.GetAccessibleDeparments();
            IList<decimal> depList = (this as IDataAccess).GetAccessibleDeparments();
            foreach (decimal depId in depList)
            {
                Department dep = bDepartment.GetByID(depId);
                var persons = from prs in dep.PersonList
                              select prs.ID;
                personList.AddRange(persons.ToList());

            }
            IList<decimal> ctlStationList = (this as IDataAccess).GetAccessibleControlStations();
            PersonRepository prsRep = new PersonRepository(false);
            personList.AddRange(prsRep.GetAllPersonByControlStaion(ctlStationList.ToArray()));

            return personList.Distinct().ToList();
        }

        #endregion

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckUsersLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPasswordChangeLoadAccess()
        {
        }

        public IList<decimal> GetAllUserIDList(decimal currentUserID, UserSearchKeys? searchKey, string searchTerm, bool singleResult)
        {
            IList<decimal> UserIDList = new List<decimal>();
            try
            {
                return userRepository.GetAllUserIDList(currentUserID, searchKey, searchTerm, singleResult);
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "GetAllUserIDList");
                return UserIDList;
            }
        }

        public void CreateBasePersonUser(Person person)
        {
            try
            {
                Role role = (new BRole()).GetByCustomCode(((int)RoleCustomCodeType.User).ToString());
                if (role != null && !this.userRepository.CheckIsUserDefined(person))
                {
                    if (role != null)
                    {
                        UserProxy userProxy = new UserProxy();
                        userProxy.Active = true;
                        userProxy.PersonID = person.ID;
                        if (person.PersonDetail != null && person.PersonDetail.RoleID != 0)
                            userProxy.RoleID = person.PersonDetail.RoleID;
                        else
                            userProxy.RoleID = role.ID;
                        userProxy.UserName = person.BarCode;
                        userProxy.Password = person.UserPassword;
                        userProxy.ConfirmPassword = person.UserPassword;
                        userProxy.IsPasswodChanged = true;
                        userProxy.ActiveDirectoryAuthenticate = false;
                        userProxy.IsAutomaticGenerated = true;
                        this.InsertUser(userProxy);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BUser", "CreateBasePersonUser");
                throw ex;
            }
        }

        public IList<decimal> GetCurrentUserAccessibleRoleIdsList()
        {
            IList<decimal> roleIdsList = new List<decimal>();
            roleIdsList.Add(BUser.CurrentUser.Role.ID);
            if (SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState) != null)
            {
                Dictionary<string, object> CurrentUserManagmentState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);
                if (CurrentUserManagmentState.ContainsKey("IsManager") && CurrentUserManagmentState.ContainsKey("ManagerRoleId") && (bool)CurrentUserManagmentState["IsManager"])
                    roleIdsList.Add((decimal)CurrentUserManagmentState["ManagerRoleId"]);
                if (CurrentUserManagmentState.ContainsKey("IsOperator") && CurrentUserManagmentState.ContainsKey("OperatorRoleId") && (bool)CurrentUserManagmentState["IsOperator"])
                    roleIdsList.Add((decimal)CurrentUserManagmentState["OperatorRoleId"]);
                if (CurrentUserManagmentState.ContainsKey("IsSubstitute") && CurrentUserManagmentState.ContainsKey("SubstituteRoleId") && (bool)CurrentUserManagmentState["IsSubstitute"])
                    roleIdsList.Add((decimal)CurrentUserManagmentState["SubstituteRoleId"]);
            }
            return roleIdsList;
        }


    }


}