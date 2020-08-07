using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// درخواستهای بررسی شده
    /// مربوط به مدیر میباشد
    /// </summary>
    public interface IReviewedRequests
    {
        /// <summary>
        /// تعداد درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int GetRequestCount(RequestState requestState, int year, int month);

        /// <summary>
        /// تعداد درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int GetRequestCount(string searchKey, int year, int month);

        /// <summary>
        /// درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby,out int count , ViewState viewState , string fromDate , string toDate);


        /// <summary>
        /// درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> GetAllRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count, ViewState viewState, string fromDate, string toDate);

        /// <summary>
        /// حذف یک درخواست توسط مدیر
        /// </summary>
        /// <param name="requestId"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void DeleteRequst(IList<KartableSetStatusProxy> requests, string managerDescription, decimal applicatorID);
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void DeleteRequestByService(IList<KartableSetStatusProxy> requests, string managerDescription, Flow flow);
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckSurveyedRequestsLoadAccess();

        
    }
}
