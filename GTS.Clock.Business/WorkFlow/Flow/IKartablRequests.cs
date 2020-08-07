using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// کارتابل مدیر
    /// </summary>
    public interface IKartablRequests
    {
        int GetRequestCount(RequestType requestType, int year, int month);

        int GetRequestCount(string searchKey, int year, int month);
        
        IList<KartablProxy> GetAllRequests(RequestType requestType, int year, int month, int pageIndex, int pageSize,KartablOrderBy orderby,out int count);
        IList<KartablProxy> GetAllRequests(RequestType requestType, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count, KartablSummaryItems itemSummary , ViewState viewState , string fromDate , string toDate);
        IList<ContractKartablProxy> GetAllRequests(decimal personId);

        IList<KartablProxy> GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count, KartablSummaryItems itemSummary, ViewState viewState, string fromDate, string toDate);

		bool SetStatusOfRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID);
        bool SetStatusOfRequestByService(IList<KartableSetStatusProxy> requests, RequestState status, string description);
        KartablRequestHistoryProxy GetRequestHistory(decimal requestId);

        IList<KartablFlowLevelProxy> GetRequestLevelsByManagerFlowID(decimal requestId, decimal managerFlowId);

        IList<KartablFlowLevelProxy> GetRequestLevelsByPersonnelID(decimal requestId, decimal personnelID);

        IList<KartablFlowLevelProxy> GetRequestLevelsByRequestID(decimal requestId);

        IList<KartablFlowLevelProxy> GetRequestLevelsByRequestSubstituteID(decimal requestId, decimal substitutePersonID);

        int GetRequestsByFilterCount(IList<RequestFliterProxy> fliters);

        IList<KartablProxy> GetAllRequestsByFilter(IList<RequestFliterProxy> fliters, int pageIndex, int pageSize, KartablOrderBy orderby);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckKartableLoadAccess();


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckSpecialKartableLoadAccess();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
		bool ConfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
		bool UnconfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID);

        int GetAllSpecialKartableRequestsCount(RequestType requestType, RequestState requestState, int year, int month, string searchKey , ViewState viewState , string fromDate , string toDate);
        IList<KartablProxy> GetAllSpecialKartableRequests(RequestType requestType, RequestState requestState, int year, int month, int pageIndex, int pageSize, string searchKey, KartablOrderBy orderby , ViewState viewState , string fromDate , string toDate);
		bool SetStatusOfSpecialRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool ConfirmSpecialRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool UnconfirmSpecialRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool ConfirmRequestSubstituteRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        bool UnconfirmRequestSubstituteRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList);

        bool SetStatusOfRequestSubstituteRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void DeleteSpecialRequest(IList<KartableSetStatusProxy> requests, string managerDescription, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID , Caller caller);

        IList<KartablFlowLevelProxy> GetSpecialRequestLevels(decimal requestId, decimal managerFlowId);

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckKartablSettingLoadAccess();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckSpecialKartableSettingLoadAccess();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckSurveySettingLoadAccess();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckRequestSubstituteKartableSettingLoadAccess();

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void CheckRequestSubstituteKartableLoadAccess();

        int GetAllRequestSubstituteKartableRequestsCount(RequestState requestState, int year, int month, string searchKey, ViewState viewState, string FromDate, string ToDate);
        IList<KartablProxy> GetAllRequestSubstituteKartableRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize, string searchKey, KartablOrderBy orderby, ViewState viewState, string FromDate, string ToDate);


    }
}
