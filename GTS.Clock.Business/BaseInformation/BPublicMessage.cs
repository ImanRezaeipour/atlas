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
using GTS.Clock.Model.UI;
using GTS.Clock.Business.Security;
using NHibernate;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// پیام عمومی
    /// created at: 3/4/2012 2:34:56 PM
    /// by        : Farhad Salvati
    /// write your name here
    /// </summary>
    public class BPublicMessage : BaseBusiness<PublicMessage>
    {
        private const string ExceptionSrc = "GTS.Clock.Business.BaseInformation.BPublicMessage";
        private EntityRepository<PublicMessage> objectRep = new EntityRepository<PublicMessage>();
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        #region BaseBusiness Implementation

        /// <summary>
        /// اعتبار سنجی عملیات درج در دیتابیس
        /// </summary>
        /// <param name="obj">پیام عمومی</param>
        protected override void InsertValidate(PublicMessage msg)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(msg.Message))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageContentRequierd, "متن پیام مشخص نشده است", ExceptionSrc));
            }
            if (Utility.IsEmpty(msg.Subject))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageSubjecttRequierd, "موضوع پیام مشخص نشده است", ExceptionSrc));
            }
            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش در دیتابیس
        /// </summary>
        /// <param name="obj">پیام عمومی</param>
        protected override void UpdateValidate(PublicMessage msg)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (Utility.IsEmpty(msg.Message))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageContentRequierd, "متن پیام مشخص نشده است", ExceptionSrc));
            }
            if (Utility.IsEmpty(msg.Subject))
            {
                exception.Add(new ValidationException(ExceptionResourceKeys.PublicMessageSubjecttRequierd, "موضوع پیام مشخص نشده است", ExceptionSrc));
            }

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف در دیتابیس
        /// </summary>
        /// <param name="obj">پیام عمومی</param>
        protected override void DeleteValidate(PublicMessage msg)
        {
            UIValidationExceptions exception = new UIValidationExceptions();


            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// بررسی دسترسی پیام های عمومی
        /// </summary>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckPublicNewsLoadAccess()
        {
        }

        /// <summary>
        /// عملیات درج پیام عمومی در دیتابیس
        /// </summary>
        /// <param name="publicNews">پیام عمومی</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی پیام</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertPublicNews(PublicMessage publicNews, UIActionType UAT)
        {
            return base.SaveChanges(publicNews, UAT);
        }

        /// <summary>
        /// عملیات ویرایش پیام عمومی در دیتابیس
        /// </summary>
        /// <param name="publicNews">پیام عمومی</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی پیام</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdatePublicNews(PublicMessage publicNews, UIActionType UAT)
        {
            return base.SaveChanges(publicNews, UAT);
        }

        /// <summary>
        /// عملیات حذف پیام عمومی در دیتابیس
        /// </summary>
        /// <param name="publicNews">پیام عمومی</param>
        /// <param name="UAT">نوع عملیات</param>
        /// <returns>کلید اصلی پیام</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeletePublicNews(PublicMessage publicNews, UIActionType UAT)
        {
            return base.SaveChanges(publicNews, UAT);
        }

        /// <summary>
        /// کلیه پیام های عمومی را بر می گرداند
        /// </summary>
        /// <returns>لیست پیام ها</returns>
        public override IList<PublicMessage> GetAll()
        {
            IList<PublicMessage> list = base.GetAll();
            if (list != null)
            {
                list = list.OrderByDescending(x => x.Date).OrderBy(c => c.Order).ToList();
            }
            else
                list = new List<PublicMessage>();
            return list;
        }

        /// <summary>
        /// تعداد کلیه پیام های عمومی را بر می گرداند
        /// </summary>
        /// <returns>تعداد</returns>
        public int GetAllPublicNewsCount()
        {
            PublicMessage publicMessageAlias = null;
            int count = NHSession.QueryOver<PublicMessage>(() => publicMessageAlias).Where(() => publicMessageAlias.Active == true).RowCount();
            return count;
        }

        /// <summary>
        /// کلیه پیام های عمومی را به صورت صفحه بندی شده بر می گرداند 
        /// </summary>
        /// <param name="pageSize">تعداد رکورد های هر صفحه</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <returns>لیست پیام ها</returns>
        public IList<PublicMessage> GetPublicNewsByPaging(int pageSize, int pageIndex)
        {
            //IList<PublicMessage> list = base.GetAll().Where(x => x.Active).OrderByDescending(x => x.Date).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            PublicMessage publicMessageAlias = null;
            IList<PublicMessage> list = NHSession.QueryOver<PublicMessage>(() => publicMessageAlias).Where(() => publicMessageAlias.Active == true).OrderBy(() => publicMessageAlias.Order).Desc.Skip(pageIndex * pageSize).Take(pageSize).List<PublicMessage>();

            if (list == null)
                list = new List<PublicMessage>();
            return list;
        }
        #endregion
    }
}
