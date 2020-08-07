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
using GTS.Clock.Model.Security;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.BoxService;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Business.RequestFlow;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Business.BaseInformation;

namespace GTS.Clock.Business.BoxService
{
    /// <summary>
    /// نواحی داشبورد
    /// created at: 2011-11-24 2:18:29 PM
    /// write your name here
    /// </summary>
    public class BMainPageBox
    {
        const string ExceptionSrc = "GTS.Clock.Business.BoxService.BMainPageBox";
        EntityRepository<PublicMessage> pblMesgRep = new EntityRepository<PublicMessage>();
        EntityRepository<KartablSummary> kartablRep = new EntityRepository<KartablSummary>();

        /// <summary>
        /// پیغامهای عمومی را برمیگرداند
        /// </summary>
        /// <returns>لیست پیغامهای عمومی</returns>
        public IList<PublicMessage> GetPublicMessages()
        {
            try
            {
                IList<PublicMessage> list = new BPublicMessage().GetAll();

                return list;
            }
            catch (Exception ex)
            {
                BaseBusiness<PublicMessage>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// پیغامهای عمومی را به صورت صفحه بندی برمیگرداند
        /// </summary>
        /// <param name="pageSize">تعداد رکورد در هر صفحه</param>
        /// <param name="pageIndex">شماره صفحه</param>
        /// <returns>لیست پیغامهای عمومی</returns>
        public IList<PublicMessage> GetPublicMessagesByPaging(int pageSize, int pageIndex)
        {
            try
            {
                IList<PublicMessage> list = new BPublicMessage().GetPublicNewsByPaging(pageSize, pageIndex);

                return list;
            }
            catch (Exception ex)
            {
                BaseBusiness<PublicMessage>.LogException(ex);
                throw ex;
            }
        }
        
        /// <summary>
        /// تعداد پیغامهای عمومی را برمیگرداند 
        /// </summary>
        /// <returns>تعداد</returns>
        public int GetAllPublicNewsCount()
        {
            try
            {
                return new BPublicMessage().GetAllPublicNewsCount();
            }
            catch (Exception ex)
            {
                BaseBusiness<PublicMessage>.LogException(ex);
                throw ex;
            }
        }
        
        /// <summary>
        /// خلاصه کارتابل را بر می گرداند
        /// </summary>
        /// <param name="userID">کلید اصلی کاربر</param>
        /// <returns>لیست خلاصه کارتابل</returns>
        public IList<KartablSummary> GetKartablSummary(decimal userID)
        {
            try
            {
                BKartabl bKartabl = new BKartabl();
                IList<KartablSummary> list = kartablRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new KartablSummary().Active), true))
                                                                     .OrderBy(x => x.Order).ToList();
                IDashboardBRequest busRequest = new BRequest();
                int year = 0, month = 0;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = new PersianDateTime(DateTime.Now).Year;
                    month = new PersianDateTime(DateTime.Now).Month;
                }
                else
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
                foreach (KartablSummary ks in list)
                {
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        ks.Title = ks.FnTitle;
                    }
                    else
                    {
                        ks.Title = ks.EnTitle;
                    }
                    KartablSummaryItems item = (KartablSummaryItems)Enum.Parse(typeof(KartablSummaryItems), ks.Key);

                    int count = 0;

                    switch (item)
                    {
                        case KartablSummaryItems.ConfirmedRequestCount:
                            count = busRequest.GetAllRequestCount(userID, year, month, RequestState.Confirmed);
                            ks.Value = count.ToString();
                            break;
                        case KartablSummaryItems.NotConfirmedRequestCount:
                            count = busRequest.GetAllRequestCount(userID, year, month, RequestState.Unconfirmed);
                            ks.Value = count.ToString();
                            break;
                        case KartablSummaryItems.MainRecievedRequestCount:
                            count = bKartabl.GetManagerKartablRequestCount(userID, year);
                            ks.Value = count == -1 ? "" : count.ToString();
                            break;
                        case KartablSummaryItems.SubstituteRecievedRequestCount:
                            count = bKartabl.GetSubstituteKartablRequestCount(userID, year);
                            ks.Value = count == -1 ? "" : count.ToString();
                            break;
                        case KartablSummaryItems.InFlowRequestCount:
                            count = busRequest.GetAllRequestCount(userID, year, RequestState.UnderReview);
                            ks.Value = count.ToString();
                            break;
                        case KartablSummaryItems.PrivateMessageCount:
                            BPrivateMessage busMsg = new BPrivateMessage();
                            ks.Value = Utility.ToString(busMsg.GetAllUnReadRecievedMessagesCount(userID));
                            break;
                        case KartablSummaryItems.UnderReviewRequestSubstituteRequestsCount:
                            try
                            {
                               count = bKartabl.GetAllRequestSubstituteKartableRequestsCount(RequestState.UnderReview, year, month, string.Empty);
                            }
                            catch (Exception)
                            {
                            } 
                            ks.Value = count.ToString();
                            break;
                    }
                }

                list = list.Except(list.Where(x => x.Key == KartablSummaryItems.UnderReviewRequestSubstituteRequestsCount.ToString() && x.Value == 0.ToString()).ToList<KartablSummary>()).ToList<KartablSummary>();

                return list;
            }
            catch (Exception ex)
            {
                BaseBusiness<PublicMessage>.LogException(ex);
                throw ex;
            }
        }

    }
}
