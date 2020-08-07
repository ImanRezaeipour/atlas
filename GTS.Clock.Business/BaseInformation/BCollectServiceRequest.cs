using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Model.Concepts;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.BaseInformation
{
    /// <summary>
    /// سرویس انتقال تردد ها از سرویس جمع آوری به دیتابیس اطلس
    /// </summary>
    public class BCollectServiceRequest : BaseBusiness<CollectServiceRequest>
    {

        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();

        /// <summary>
        /// کلیه تردد های یک پرسنل را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید پرسنل</param>
        /// <returns>لیست ترددها</returns>
        public IList<CollectServiceRequest> GetByPerson(decimal personId)
        {
            try
            {
                CollectServiceRequest collectServiceRequestAlias = null;
                IList<CollectServiceRequest> collectServiceRequetList = NHSession.QueryOver<CollectServiceRequest>(() => collectServiceRequestAlias)
                                                                        .Where(() => collectServiceRequestAlias.Person.ID == personId).List();
                return collectServiceRequetList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BCollectServiceRequest", "GetByPerson");
                throw ex;
            }
        }

        /// <summary>
        /// آماده سازی ترددها قبل از عملیات درج در دیتابیس
        /// </summary>
        /// <param name="collectServiceRequest"></param>
        /// <param name="action"></param>
        protected override void GetReadyBeforeSave(CollectServiceRequest collectServiceRequest, UIActionType action)
        {
            collectServiceRequest.Date = collectServiceRequest.Date.Date;
        }

        /// <summary>
        /// ترددهای یک پرسنل در یک تاریخ مشخص را بر می گرداند
        /// </summary>
        /// <param name="personId">کلید پرسنل</param>
        /// <param name="trafficDate">تاریخ</param>
        /// <returns>لیست ترددها</returns>
        public IList<CollectServiceRequest> GetByPerson(decimal personId, DateTime trafficDate)
        {
            try
            {
                CollectServiceRequest collectServiceRequestAlias = null;
                IList<CollectServiceRequest> collectServiceRequetList = NHSession.QueryOver<CollectServiceRequest>(() => collectServiceRequestAlias)
                                                                        .Where(() => collectServiceRequestAlias.Person.ID == personId && collectServiceRequestAlias.Date == trafficDate).List();
                return collectServiceRequetList;
            }
            catch (Exception ex)
            {
                LogException(ex, "BCollectServiceRequest", "GetByPerson");
                throw ex;
            }
        }

        /// <summary>
        /// اعتبار سنجی عملیات درج تردد
        /// </summary>
        /// <param name="obj"></param>
        protected override void InsertValidate(CollectServiceRequest obj)
        {

        }

        /// <summary>
        /// اعتبار سنجی عملیات ویرایش تردد
        /// </summary>
        /// <param name="obj"></param>
        protected override void UpdateValidate(CollectServiceRequest obj)
        {

        }

        /// <summary>
        /// اعتبار سنجی عملیات حذف تردد
        /// </summary>
        /// <param name="obj"></param>
        protected override void DeleteValidate(CollectServiceRequest obj)
        {

        }

        /// <summary>
        /// عملیات درج تردد های انتقالی
        /// </summary>
        /// <param name="collectServiceRequest">تردد</param>
        /// <returns>کلید تردد درج شده</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public decimal InsertCollectServiceRequest(CollectServiceRequest collectServiceRequest)
        {
            try
            { 
                decimal id = this.SaveChanges(collectServiceRequest, UIActionType.ADD);
                return id;
            }
            catch (Exception ex)
            {
                LogException(ex, "BCollectServiceRequest", "InsertCollectServiceRequest");
                throw ex;
            }
        }

        /// <summary>
        /// عملیات ویرایش تردد های انتقالی 
        /// </summary>
        /// <param name="collectServiceRequest">تردد ها</param>
        /// <returns>کلید تردد ویرایش شده</returns>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Avoid)]
        public decimal UpdateCollectServiceRequest(CollectServiceRequest collectServiceRequest)
        {
            try
            {

                decimal id = this.SaveChanges(collectServiceRequest, UIActionType.EDIT);
                return id;
            }
            catch (Exception ex)
            {
                LogException(ex, "BCollectServiceRequest", "UpdateCollectServiceRequest");
                throw ex;
            }
        }
    }
}
