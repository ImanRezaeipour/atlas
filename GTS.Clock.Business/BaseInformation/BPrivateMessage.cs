using System;
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
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Charts;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.Security;
using NHibernate;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// پیام خصوصی
    /// created at: 3/17/2012 12:04:53 PM
    /// by        : Farhad Salavati
    /// write your name here
    /// </summary>
    public class BPrivateMessage : BaseBusiness<PrivateMessage>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BPrivateMessage";
        private EntityRepository<PrivateMessage> objectRep = new EntityRepository<PrivateMessage>();
		private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// تعداد کلیه پیام های خصوص دریافتی کاربر جاری را بر می گرداند
        /// </summary>
        /// <returns>تعداد</returns>
        public int GetAllRecievedMessagesCount() 
        {
            int count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToPersonID), BUser.CurrentUser.Person.ID),
                                                     new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToActive), true));
            return count;
        }

        /// <summary>
        /// تعداد  پیام های خصوصی دریافتی خوانده نشده را بر می گرداند 
        /// </summary>
        /// <param name="userID">کلید کاربر</param>
        /// <returns>تعداد</returns>
        public int GetAllUnReadRecievedMessagesCount(decimal userID) 
        {
            decimal personID = 0;
            if (BUser.CurrentUser != null && BUser.CurrentUser.ID != 0)
                personID = BUser.CurrentUser.Person.ID;
            else
            {
                BUser bUser = new BUser();
                User user = bUser.GetByID(userID);
                personID = user.Person.ID;
                NHibernateSessionManager.Instance.GetSession().Evict(user);
            }
            try
            {
                int count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToPersonID), personID),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToActive), true),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().Status), false));
                return count;
            }
            catch (Exception ex) 
            {
                LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// تعداد پیام های خصوصی ارسال شده کابر جاری را بر می گرداند
        /// </summary>
        /// <returns>تعداد</returns>
        public int GetAllSentMessageCount()
        {
            int count = objectRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromPersonID), BUser.CurrentUser.Person.ID),
                                                     new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromActive), true));
            return count;
        }

        /// <summary>
        /// کلیه پیام های خصوصی دریافتی کاربر جاری را به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد های هر صفحه</param>
        /// <returns>لیست پیام های خصوصی</returns>
        public IList<PrivateMessage> GetAllRecievedMessages(int pageIndex, int pageSize) 
        {
            try
            {
				//IList<PrivateMessage> list = objectRep.GetByCriteriaByPage(pageIndex, pageSize, new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToPersonID), BUser.CurrentUser.Person.ID),
				//														   new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().ToActive), true));
				PrivateMessage privateMessageAlias = null;
				IList<PrivateMessage> list = NHSession.QueryOver<PrivateMessage>(() => privateMessageAlias)
																			  .Where(() => privateMessageAlias.ToPersonID == BUser.CurrentUser.Person.ID && privateMessageAlias.ToActive == true)
																			  .OrderBy(() => privateMessageAlias.Date).Desc
																			  .Skip(pageIndex * pageSize)
																			  .Take(pageSize).List<PrivateMessage>();
                foreach (PrivateMessage pm in list)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        pm.TheDate = Utility.ToPersianDate(pm.Date);
                    }
                    else
                    {
                        pm.TheDate = Utility.ToString(pm.Date);
                    }
                }

                return list;
            }
            catch (Exception ex) 
            {
                LogException(ex, "BPrivateMessage", "GetAllRecievedMessages");
                throw ex;
            }
        }

        /// <summary>
        /// کلیه پیام های خصوصی ارسالی کاربر جاری را به صورت صفحه بندی شده بر می گرداند
        /// </summary>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <param name="pageSize">تعداد رکورد های هر صفحه</param>
        /// <returns>لیست پیام های خصوصی</returns>
        public IList<PrivateMessage> GetAllSentMessage(int pageIndex, int pageSize)
        {
            try
            {
				//IList<PrivateMessage> list = objectRep.GetByCriteriaByPage(pageIndex,pageSize, new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromPersonID), BUser.CurrentUser.Person.ID),
				//														   new CriteriaStruct(Utility.GetPropertyName(() => new PrivateMessage().FromActive), true));
                
				PrivateMessage privateMessageAlias=null;
				IList<PrivateMessage> list = NHSession.QueryOver<PrivateMessage>(() => privateMessageAlias)
					                                                          .Where(()=>privateMessageAlias.FromPersonID==BUser.CurrentUser.Person.ID && privateMessageAlias.FromActive==true)
																			  .OrderBy(() => privateMessageAlias.Date).Desc
																			  .Skip(pageIndex * pageSize)
															                  .Take(pageSize).List<PrivateMessage>();

                foreach (PrivateMessage pm in list)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        pm.TheDate = Utility.ToPersianDate(pm.Date);
                    }
                    else
                    {
                        pm.TheDate = Utility.ToString(pm.Date);
                    }
                }
			
				
                return list;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetAllSentMessage");
                throw ex;
            }
        }

        /// <summary>
        /// پیام را جهت خوانده شده علامت گذاری می کند
        /// </summary>
        /// <param name="messageId">کلید اصلی پیام</param>
        public void SetMessageAsRead(decimal messageId)
        {
            try
            {
                IList<PrivateMessage> list = this.GetAllRecievedMessages(0, this.GetAllRecievedMessagesCount());

                if (!list.Any(x => x.ID == messageId))
                {
                    throw new IllegalServiceAccess("XSS Attack Private Message", ExceptionSrc);
                }

                PrivateMessage pm = base.GetByID(messageId);
                pm.Status = true;
                this.SaveChanges(pm, UIActionType.EDIT);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "SetMessageAsRead");
                throw ex;
            }
        }

        /// <summary>
        /// پاسخ دهی به پیام
        /// </summary>
        /// <param name="messageId">کلید اصلی پیام</param>
        /// <param name="message">متن پیام</param>
        public void ReplyMessage(decimal messageId, string message)
        {
            try
            {
                IList<PrivateMessage> list = this.GetAllRecievedMessages(0, this.GetAllRecievedMessagesCount());

                if (!list.Any(x => x.ID == messageId))
                {
                    throw new IllegalServiceAccess("XSS Attack Private Message", ExceptionSrc);
                }

                PrivateMessage from = base.GetByID(messageId);
                PrivateMessage privateMsg = new PrivateMessage();
                privateMsg.Message = message;
                privateMsg.FromPersonID = BUser.CurrentUser.Person.ID;
                privateMsg.ToPersonID = from.FromPersonID;
                privateMsg.Date = DateTime.Now;
                privateMsg.Subject = from.Subject;
                privateMsg.Status = false;
                privateMsg.FromActive = true;
                privateMsg.ToActive = true;
                base.SaveChanges(privateMsg, UIActionType.ADD);

            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "ReplyMessage");
                throw ex;
            }
        }

        /// <summary>
        /// ارسال پیام جدید
        /// </summary>
        /// <param name="subject">عنوان</param>
        /// <param name="message">متن پیام</param>
        /// <param name="prsList">لیست دریافت کنندگان</param>
        public void NewMessage(string subject, string message, IList<PersonDepartmentProxy> prsList)
        {
            try
            {
                IList<Person> toPrsList = new BUnderManagment().GetPersonsByDepartment(prsList);
                foreach (Person person in toPrsList)
                {
                    PrivateMessage msg = new PrivateMessage();
                    msg.Subject = subject;
                    msg.Message = message;
                    msg.FromPersonID = BUser.CurrentUser.Person.ID;
                    msg.Date = DateTime.Now;
                    msg.ToPersonID = person.ID;
                    msg.ToActive = true;
                    msg.FromActive = true;
                    base.SaveChanges(msg, UIActionType.ADD);
                }
            }
            catch (Exception ex) 
            {
                LogException(ex, "BPrivateMessage", "NewMessage");
                throw ex;
            }
        }

        /// <summary>
        /// ارسال پیام خرابی سیستم
        /// </summary>
        /// <param name="subject">عنوان پیام</param>
        /// <param name="message">متن پیام</param>
        public void NewMessage(string subject, string message)
        {
            try
            {
                IList<User> toPrsList = new BRole().GetUsersInSysAdminRole();
                var persons = from o in toPrsList
                              where o.Active && o.Person.Active && !o.Person.IsDeleted
                              select o.Person;
                           
                foreach (Person person in persons)
                {
                    PrivateMessage msg = new PrivateMessage();
                    msg.Subject = subject;
                    msg.Message = message;
                    msg.FromPersonID = BUser.CurrentUser.Person.ID;
                    msg.Date = DateTime.Now;
                    msg.ToPersonID = person.ID;
                    msg.ToActive = true;
                    msg.FromActive = true;
                    base.SaveChanges(msg, UIActionType.ADD);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "NewMessage To Sys Admin");
                throw ex;
            }
        }

        /// <summary>
        /// حذف پیام از صندوق ارسالی
        /// </summary>
        /// <param name="messageIds">لیست کلید اصلی پیام</param>
        public void DeleteFromSentBox(IList<decimal> messageIds) 
        {
            try
            {
                IList<PrivateMessage> list = this.GetAllSentMessage(0, this.GetAllSentMessageCount());

                foreach (decimal messageId in messageIds)
                {
                    if (!list.Any(x => x.ID == messageId))
                    {
                        throw new IllegalServiceAccess("XSS Attack Private Message", ExceptionSrc);
                    }
                    PrivateMessage pm = base.GetByID(messageId);
                    pm.FromActive = false;
                    if (pm.ToActive)
                    {
                        base.SaveChanges(pm, UIActionType.EDIT);
                    }
                    else
                    {
                        base.SaveChanges(pm, UIActionType.DELETE);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "DeleteFromSentBox");
                throw ex;
            }
        }

        /// <summary>
        /// حذف پیام از صندوق دریافتی
        /// </summary>
        /// <param name="messageIds">لیست کلید اصلی پیام</param>
        public void DeleteFromInbox(IList<decimal> messageIds)
        {
            try
            {
                IList<PrivateMessage> list = this.GetAllRecievedMessages(0, this.GetAllRecievedMessagesCount());

                foreach (decimal messageId in messageIds)
                {
                    if (!list.Any(x => x.ID == messageId)) 
                    {
                        throw new IllegalServiceAccess("XSS Attack Private Message", ExceptionSrc);
                    }

                    PrivateMessage pm = base.GetByID(messageId);
                    pm.ToActive = false;
                    if (pm.FromActive)
                    {
                        base.SaveChanges(pm, UIActionType.EDIT);
                    }
                    else
                    {
                        base.SaveChanges(pm, UIActionType.DELETE);
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "DeleteFromInbox");
                throw ex;
            }
        }

        /// <summary>
        /// بررسی دسترسی به پیام های خصوصی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPrivateMessagesLoadAccess()
        {
        }

        /// <summary>
        /// بررسی دسترسی به پیام های خصوصی ارسالی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSendPrivateMessagesLoadAccess()
        {
        }

        /// <summary>
        /// بررسی دسترسی به پیام های خصوصی 
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSendPrivateMessageAccess()
        { 
        }

        /// <summary>
        /// بررسی دسترسی به حذف پیام های خصوصی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckDeletePrivateMessageAccess()
        { 
        }

        /// <summary>
        /// بررسی دسترسی به پیام های سیستمی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSystemMessageLoadAccess()
        { 
        }

        #region Tree

        /// <summary>
        /// ریشه درخت را برمیگرداند
        /// </summary>
        /// <returns>گره چارت سازمانی</returns>
        public Department GetDepartmentRoot()
        {
            try
            {
                BDepartment busDep = new BDepartment();
                return busDep.GetDepartmentsTree();
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetDepartmentRoot");
                throw ex;
            }
        }

        /// <summary>
        /// بچه های یک بخش را برمیگرداند
        /// </summary>
        /// <param name="nodeID">کلید اصلی گره</param>
        /// <returns>لیست گره های چارت سازمانی</returns>
        public IList<Department> GetDepartmentChilds(decimal nodeID)
        {
            try
            {
                BDepartment busDep = new BDepartment();
                return busDep.GetDepartmentChildsWithoutDA(nodeID);
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetDepartmentChilds");
                throw ex;
            }
        }

        /// <summary>
        /// پرسنل یک بخش را برمیگرداند
        /// </summary>
        /// <param name="departmentID">کلید اصلی گره چارت سازمانی</param>
        /// <returns>لیست پرسنل</returns>
        public IList<Person> GetDepartmentPerson(decimal departmentID)
        {
            try
            {
                BDepartment busDep = new BDepartment();
                return busDep.GetByID(departmentID).PersonList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BPrivateMessage", "GetDepartmentPerson");
                throw ex;
            }
        }
    
        #endregion

        #region BaseBusiness Implementation

        /// <summary>
        /// اعتبارسنجی عملیات درج در دیتابیس
        /// </summary>
        /// <param name="obj">پیام خصوصی</param>
        protected override void InsertValidate(PrivateMessage obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش در دیتابیس
        /// </summary>
        /// <param name="obj">پیام خصوصی</param>
        protected override void UpdateValidate(PrivateMessage obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبارسنجی عملیات حذف در دیتابیس
        /// </summary>
        /// <param name="obj">پیام خصوصی</param>
        protected override void DeleteValidate(PrivateMessage obj)
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
