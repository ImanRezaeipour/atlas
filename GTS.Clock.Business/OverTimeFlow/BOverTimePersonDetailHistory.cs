using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.PersonInfo;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Shifts;
using GTS.Clock.Business.Temp;
using GTS.Clock.Business.WorkedTime;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model;
using GTS.Clock.Model.Charts;
using GTS.Clock.Model.MonthlyReport;
using GTS.Clock.Model.OverTimeFlow;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transaction;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.OverTimeFlow
{
    /// <summary>
    /// کلاس بیزینس مربوط به تاریخچه تغییرات سرانه اضافه کار تشویقی, شب کاری تشویقی, تعطیل کار تشویقی
    /// </summary>
    public class BOverTimePersonDetailHistory : BaseBusiness<OverTimePersonDetailHistory>
    {
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private const string ExceptionSrc = "GTS.Clock.Business.OverTimeFlow.BOverTimePersonDetailHistory";
        private EntityRepository<OverTimePersonDetailHistory> overTimePersonDetailRepositoryHistory = new EntityRepository<OverTimePersonDetailHistory>();
 
        LanguagesName sysLanguageResource;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        public BOverTimePersonDetailHistory()
        {
            if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                this.sysLanguageResource = LanguagesName.Parsi;
            }
            else if (AppSettings.BLanguage.CurrentSystemLanguage == LanguagesName.English)
            {
                this.sysLanguageResource = LanguagesName.English;
            }
        }
          
        /// <summary>
        /// لیست تاریخچه سرانه پرسنل را بر می گرداند
        /// </summary>
        /// <returns>لیست سرانه پرسنل </returns>
        public IList<OverTimePersonDetailHistory> GetAll()
        {
            IList<OverTimePersonDetailHistory> list = base.GetAll();
            return list;
        }
           
        #region CRUD

        /// <summary>
        /// عملیات ذخیره تاریخچه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل سرانه</param>
        /// <returns></returns>
        //[ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal InsertOverTimePersonDetailHistory(OverTimePersonDetailHistory obj)
        { 
            return base.SaveChanges(obj, UIActionType.ADD);
        }
  
        /// <summary>
        /// عملیات ویرایش تاریخچه در دیتابیس
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //[ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal UpdateOverTimePersonDetailHistory(OverTimePersonDetailHistory obj)
        { 
            return base.SaveChanges(obj, UIActionType.EDIT);
        }
         
        /// <summary>
        /// عملیات حذف تاریخچه در دیتابیس
        /// </summary>
        /// <param name="obj">مدل تاریخچه</param>
        /// <returns></returns>
        //[ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public decimal DeleteOverTimePersonDetailHistory(OverTimePersonDetailHistory obj)
        {
            return base.SaveChanges(obj, UIActionType.DELETE);
        }

        #endregion

        #region Validation
          
        protected override void InsertValidate(OverTimePersonDetailHistory obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void UpdateValidate(OverTimePersonDetailHistory obj)
        {
            UIValidationExceptions exception = new UIValidationExceptions();

            if (exception.Count > 0)
            {
                throw exception;
            }
        }

        protected override void DeleteValidate(OverTimePersonDetailHistory obj)
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
