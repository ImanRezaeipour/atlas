using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model;
using GTS.Clock.Model.RequestFlow;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model.Charts;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Exceptions.UI;
using GTS.Clock.Infrastructure.NHibernateFramework;
using GTS.Clock.Business;
using GTS.Clock.Business.Charts;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.Security;
using GTS.Clock.Business.AppSettings;
using System.Globalization;
using GTS.Clock.Model.Security;
using GTS.Clock.Model.BaseInformation;
using GTS.Clock.Business.Leave;
using GTS.Clock.Business.WorkFlow;
using GTS.Clock.Model.MonthlyReport;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.Transform;
using System.Web.Configuration;
using GTS.Clock.Business.Temp;
using GTS.Clock.Business.Presentaion_Helper.Proxy;
using NHibernate;
using GTS.Clock.Model.UI;
using NHibernate.Type;
using GTS.Clock.Business.Reporting;


namespace GTS.Clock.Business.RequestFlow
{
    /// <summary>
    /// -----------
    /// </summary>
    public class BKartabl : MarshalByRefObject, IKartablRequests, IRegisteredRequests, IReviewedRequests
    {
        private const string ExceptionSrc = "GTS.Clock.Business.RequestFlow.BKartabl";
        private RequestRepository requestRep = new RequestRepository(false);
        private ISession NHSession = NHibernateSessionManager.Instance.GetSession();
        private RequestStatusRepositiory requestStatusRep = new RequestStatusRepositiory(false);
        IManagerKartabl bManagerKartablUnderManagment = new BUnderManagment();
        ISubstituteKartabl bSubstituteKartablUnderManagment = new BUnderManagment();
        IManagerReviewedRequests bmanagerReviewed = new BUnderManagment();
        IUserRegisteredRequests bUserRegistered = new BUnderManagment();
        IOperatorRegisteredRequests bOpperatorRegistered = new BUnderManagment();
        BRequest bRequest = new BRequest();
        ISearchPerson searchTool = new BPerson();
        private decimal workingPersonId = 0;
        private decimal workingUserId = 0;
        private string workingUsername = "";
        private string IKartablRequestsKey = "IKartablRequests", IRegisteredRequestsKey = "IRegisteredRequests", IReviewedRequestsKey = "IReviewedRequests";
        private int OperationBatchSizeValue = int.Parse(WebConfigurationManager.AppSettings[OperationBatchSize.OperationBatchSizeKey.ToString()]);
        BPermit permitBusiness = new BPermit();
        private BTemp bTemp = new BTemp();
        private UserRepository userRep = new UserRepository(false);

        enum OperatorRegistType
        {
            Request,
            Permit
        }
        internal class ManagerComparer : IEqualityComparer<RegisteredRequestsFlowLevel>
        {
            public bool Equals(RegisteredRequestsFlowLevel x, RegisteredRequestsFlowLevel y)
            {
                bool isEqual = false;
                if (x.ManagerID == y.ManagerID)
                    isEqual = true;
                return isEqual;
            }
            public int GetHashCode(RegisteredRequestsFlowLevel obj)
            {
                if (Object.ReferenceEquals(obj, null))
                    return 0;
                return obj.ManagerID.GetHashCode();
            }
        }
        internal class KartablProxyRequestComparer : IEqualityComparer<KartablProxy>
        {
            public bool Equals(KartablProxy x, KartablProxy y)
            {
                bool isEqual = false;
                if (x.RequestID == y.RequestID)
                    isEqual = true;
                return isEqual;
            }
            public int GetHashCode(KartablProxy obj)
            {
                if (Object.ReferenceEquals(obj, null))
                    return 0;
                return obj.RequestID.GetHashCode();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BKartabl()
        {
            GetCurentPersonId();
        }
        public BKartabl(bool byService)
        {

            //  if (workingPersonId == 0)
            //  {
            //Model.Security.User user = new BUser().GetByID(userId);
            //if (user != null)
            // {
            this.workingPersonId = 0;
            this.workingUsername = "";
            // }
            // }
        }
        /// <summary>
        /// تنها جهت تست
        /// </summary>
        /// <param name="personId"></param>
        public BKartabl(decimal personId, decimal userId, string username)
        {
            this.workingPersonId = personId;
            this.workingUsername = username;
            this.workingUserId = userId;
        }

        #region IRegisteredRequests Members

        /// <summary>
        /// جهت اطلاع رسانی در سرویس
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="date"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        IList<ContractKartablProxy> IRegisteredRequests.GetAllUserRequests(RequestState requestState, DateTime fromDate, DateTime toDate, decimal personId)
        {
            try
            {
                IList<ContractKartablProxy> kartablResult = new List<ContractKartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                result = bUserRegistered.GetAllRequests(personId, requestState, fromDate.Date, toDate.Date);

                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    ContractKartablProxy proxy = new ContractKartablProxy();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToPersianDate(req.FromDate);
                        proxy.TheToDate = Utility.ToPersianDate(req.ToDate);
                    }
                    else
                    {
                        proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToString(req.FromDate);
                        proxy.TheToDate = Utility.ToString(req.ToDate);
                    }
                    proxy.ID = req.ID;
                    proxy.RequestID = req.ID;
                    proxy.Description = req.Description;
                    proxy.Applicant = req.Applicant;
                    proxy.ManagerDescription = req.ManagerDescription;

                    proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
                    proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);

                    proxy.Row = i + 1;
                    proxy.RequestTitle = req.PrecardName;
                    proxy.Barcode = req.PersonCode;
                    proxy.RequestSource = ContractRequestSource.Undermanagment.ToString();
                    proxy.PersonId = req.PersonID;
                    if (req.Confirm == null)
                    {
                        proxy.FlowStatus = ContractRequestState.UnderReview.ToString();
                    }
                    else if (req.IsDeleted != null && (bool)req.IsDeleted)
                    {
                        proxy.FlowStatus = ContractRequestState.Deleted.ToString();
                    }
                    else if ((bool)req.Confirm)
                    {
                        proxy.FlowStatus = ContractRequestState.Confirmed.ToString();
                    }
                    else
                    {
                        proxy.FlowStatus = ContractRequestState.Unconfirmed.ToString();
                    }

                    if (req.LookupKey.Equals(RequestType.OverWork.ToString().ToLower()))
                    {
                        proxy.RequestType = ContractRequestType.OverWork.ToString();

                        //تنظیم زمان ابتدا و انتها
                        //درخواست بازه ای بدون انتدا و انتها
                        if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                        {
                            proxy.TheFromTime = proxy.TheToTime = "";
                        }
                    }
                    else if (req.IsDaily)
                    {
                        proxy.RequestType = ContractRequestType.Daily.ToString();
                    }
                    else if (req.IsHourly)
                    {
                        proxy.RequestType = ContractRequestType.Hourly.ToString();
                    }
                    kartablResult.Add(proxy);
                }

                return kartablResult;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetAllUserRequests");
                throw ex;
            }
        }
        IList<InfoRequest> IRegisteredRequests.GetAllUserRequests(RequestState requestState, int year, int month, decimal personId)
        {
            try
            {

                IList<InfoRequest> result = new List<InfoRequest>();
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                result = bUserRegistered.GetAllRequests(personId, requestState, fromDate, toDate);

                foreach (InfoRequest req in result)
                {
                    if (req.Confirm == null)
                    {
                        req.Status = RequestState.UnderReview;
                    }
                    else if (req.IsDeleted != null && (bool)req.IsDeleted)
                    {
                        req.Status = RequestState.Deleted;
                    }
                    else if ((bool)req.Confirm)
                    {
                        req.Status = RequestState.Confirmed;
                    }
                    else
                    {
                        req.Status = RequestState.Unconfirmed;
                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetAllUserRequests");
                throw ex;
            }
        }
        #region Count

        /// <summary>
        /// تعداد درخواستهای ثبت شده را برمیگرداند
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.GetUserRequestCount(RequestState requestState, int year, int month)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                //کاربر
                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();

                int result = 0;
                if (IsCurrentUserOperator)
                {
                    result = bOpperatorRegistered.GetRequestCount(personId, userId, requestState, fromDate, toDate);
                }
                else
                {
                    result = bUserRegistered.GetRequestCount(personId, requestState, fromDate, toDate);
                }
                return result;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetUserRequestCount");
                throw ex;
            }
        }
        int IRegisteredRequests.GetUserRequestCount(RequestState requestState, int year, int month, KartablSummaryItems itemSummary)
        {

            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                //کاربر
                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();

                int result = 0;
                if (IsCurrentUserOperator && itemSummary == KartablSummaryItems.UnKnown)
                {
                    result = bOpperatorRegistered.GetRequestCount(personId, userId, requestState, fromDate, toDate);
                }
                else
                {
                    if (!IsCurrentUserOperator && itemSummary == KartablSummaryItems.UnKnown)
                    {
                        result = bUserRegistered.GetRequestCount(personId, requestState, fromDate, toDate);
                    }
                    else
                    {
                        int fromMonth = 1;
                        int toMonth = 12;
                        switch (requestState)
                        {
                            case RequestState.UnderReview:
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                {
                                    year = new PersianDateTime(DateTime.Now).Year;
                                    //month = new PersianDateTime(DateTime.Now).Month;
                                    int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", requestState == RequestState.UnderReview ? year - 1 : year, fromMonth, 1));
                                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                                }
                                else
                                {
                                    year = DateTime.Now.Year;
                                    // month = DateTime.Now.Month;
                                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                                    fromDate = new DateTime(requestState == RequestState.UnderReview ? year - 1 : year, fromMonth, 1);
                                    toDate = new DateTime(year, toMonth, endOfMonth);
                                }

                                break;
                            case RequestState.Confirmed:
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                {
                                    year = new PersianDateTime(DateTime.Now).Year;
                                    month = new PersianDateTime(DateTime.Now).Month;
                                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                                }
                                else
                                {
                                    year = DateTime.Now.Year;
                                    month = DateTime.Now.Month;
                                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                    fromDate = new DateTime(year, month, 1);
                                    toDate = new DateTime(year, month, endOfMonth);
                                }
                                break;
                            case RequestState.Unconfirmed:
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                {
                                    year = new PersianDateTime(DateTime.Now).Year;
                                    month = new PersianDateTime(DateTime.Now).Month;
                                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                                }
                                else
                                {
                                    year = DateTime.Now.Year;
                                    month = DateTime.Now.Month;
                                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                    fromDate = new DateTime(year, month, 1);
                                    toDate = new DateTime(year, month, endOfMonth);
                                }
                                break;
                        }
                        result = bUserRegistered.GetRequestCount(personId, requestState, fromDate, toDate);
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetUserRequestCount");
                throw ex;
            }
        }
        /// <summary>
        /// یک درخواست برای خود اپراتورتور را درج میکند
        /// </summary>
        /// <param name="request"></param>
        /// <returns>تعدا صفحات را بعد از درج برمیگرداند
        /// زیرا لازم است بعد از درج ایندکس به آخرین صفحه منتقل شود</returns>
        int IRegisteredRequests.InsertRequest(Request request, int year, int month)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                {
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                }
                //DNN Note: Improve Perofrmance
                request.Person = new Person();
                request.IsDateSetByUser = true;
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                request = this.bRequest.InsertRequest(request);

                //int count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                int count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //End Of DNN
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequest");
                throw ex;
            }
        }

        /// <summary>
        /// تعداد درخواستها همرا با فیلتر را برمیگرداند
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int IRegisteredRequests.GetFilterUserRequestsCount(UserRequestFilterProxy filter)
        {
            try
            {
                DateTime? fromDate = null, toDate = null;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDate(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDate(filter.ToDate) : (DateTime?)null;
                }
                else
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDateTime(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDateTime(filter.ToDate) : (DateTime?)null;
                }

                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();
                int count = 0;
                if (filter.RequestSubmiter == null)
                    filter.RequestSubmiter = RequestSubmiter.OPERATOR;
                if (IsCurrentUserOperator)
                {
                    count = bOpperatorRegistered.GetRequestCountByFilter(personId, userId, filter.UnderManagmentPersonId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate);
                }
                else
                {
                    count = bUserRegistered.GetRequestCountByFilter(personId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate);
                }
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetFilterUserRequestCount");
                throw ex;
            }

        }

        #endregion

        /// <summary>
        /// درخواستهای کاربر را برمیگرداند
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IRegisteredRequests.GetAllUserRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize, KartablSummaryItems itemSummary)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                //کاربر
                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();

                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();
                if (IsCurrentUserOperator && itemSummary == KartablSummaryItems.UnKnown)
                {
                    result = bOpperatorRegistered.GetAllRequests(personId, userId, requestState, fromDate, toDate, pageIndex, pageSize);
                }
                else
                {
                    if (!IsCurrentUserOperator && itemSummary == KartablSummaryItems.UnKnown)
                    {
                        result = bUserRegistered.GetAllRequests(personId, requestState, fromDate, toDate, pageIndex, pageSize);
                    }
                    else
                    {
                        int fromMonth = 1;
                        int toMonth = 12;
                        switch (requestState)
                        {
                            case RequestState.UnderReview:
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                {
                                    year = new PersianDateTime(DateTime.Now).Year;
                                    //month = new PersianDateTime(DateTime.Now).Month;
                                    int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", requestState == RequestState.UnderReview ? year - 1 : year, fromMonth, 1));
                                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                                }
                                else
                                {
                                    year = DateTime.Now.Year;
                                    // month = DateTime.Now.Month;
                                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                                    fromDate = new DateTime(requestState == RequestState.UnderReview ? year - 1 : year, fromMonth, 1);
                                    toDate = new DateTime(year, toMonth, endOfMonth);
                                }

                                break;
                            case RequestState.Confirmed:
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                {
                                    year = new PersianDateTime(DateTime.Now).Year;
                                    month = new PersianDateTime(DateTime.Now).Month;
                                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                                }
                                else
                                {
                                    year = DateTime.Now.Year;
                                    month = DateTime.Now.Month;
                                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                    fromDate = new DateTime(year, month, 1);
                                    toDate = new DateTime(year, month, endOfMonth);
                                }
                                break;
                            case RequestState.Unconfirmed:
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                                {
                                    year = new PersianDateTime(DateTime.Now).Year;
                                    month = new PersianDateTime(DateTime.Now).Month;
                                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                                }
                                else
                                {
                                    year = DateTime.Now.Year;
                                    month = DateTime.Now.Month;
                                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                    fromDate = new DateTime(year, month, 1);
                                    toDate = new DateTime(year, month, endOfMonth);
                                }
                                break;
                        }
                        // if(itemSummary != KartablSummaryItems.UnKnown)
                        result = bUserRegistered.GetAllRequests(personId, requestState, fromDate, toDate, pageIndex, pageSize);
                    }
                }
                IList<Request> RequestParentList = this.GetRequestParent(result);
                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();

                    proxy = this.ConvertRegisterRequestToProxy(req, RequestParentList);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }

                return kartablResult;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetAllUserRequests");
                throw ex;
            }
        }

        /// <summary>
        /// درخواست ها با اعمال فیلتر
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IRegisteredRequests.GetFilterUserRequests(UserRequestFilterProxy filter, int pageIndex, int pageSize)
        {
            try
            {
                DateTime? fromDate = null, toDate = null;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDate(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDate(filter.ToDate) : (DateTime?)null;
                }
                else
                {
                    fromDate = filter.FromDate != null ? Utility.ToMildiDateTime(filter.FromDate) : (DateTime?)null;
                    toDate = filter.ToDate != null ? Utility.ToMildiDateTime(filter.ToDate) : (DateTime?)null;
                }
                //کاربر
                decimal personId = this.GetCurentPersonId();
                decimal userId = this.GetCurentUserId();
                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();
                if (IsCurrentUserOperator)
                {
                    // if (filter.RequestSubmiter == null)
                    //filter.RequestSubmiter = RequestSubmiter.OPERATOR;
                    result = bOpperatorRegistered.GetAllRequestsByFilter(personId, userId, filter.UnderManagmentPersonId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate, pageIndex, pageSize);
                }
                else
                {
                    // if (filter.RequestSubmiter == null)
                    //filter.RequestSubmiter = RequestSubmiter.USER;
                    result = bUserRegistered.GetAllRequestsByFilter(personId, userId, filter.RequestType, filter.RequestSubmiter, fromDate, toDate, pageIndex, pageSize);
                }
                IList<Request> RequestParentList = this.GetRequestParent(result);
                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();

                    proxy = this.ConvertRegisterRequestToProxy(req, RequestParentList);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }

                return kartablResult;

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "GetAllUserRequests");
                throw ex;
            }
        }

        /// <summary>
        /// حذف یک درخواست
        /// </summary>
        /// <param name="requestId"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        void IRegisteredRequests.DeleteRequest(decimal requestId)
        {
            try
            {
                var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IRegisteredRequestsKey);
                if (!ids.Contains(requestId))
                {
                    throw new IllegalServiceAccess("XSS Attack delete request", ExceptionSrc);
                }

                //DNN Note:Improve Performance
                //BRequest bReq = new BRequest();
                this.bRequest.SaveChanges(new Request() { ID = requestId }, UIActionType.DELETE);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "DeleteRequest");
                throw ex;
            }
        }

        /// <summary>
        ///     ثبت درخواست لغو برای یک درخواست
        /// </summary>
        /// <param name="requestId"></param>
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]

        int IRegisteredRequests.TerminateRequest(decimal requestId, string ActionDescription, int month, int year)
        {
            try
            {
                NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
                EntityRepository<Request> requestRepository = new EntityRepository<Request>(false);

                BPrecard precardBusiness = new BPrecard();

                RequestStatus requestStatusAlias = null;
                Request requestAlias = null;
                UIValidationExceptions exception = new UIValidationExceptions();

                var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IRegisteredRequestsKey);
                if (!ids.Contains(requestId))
                {
                    throw new IllegalServiceAccess("XSS Attack delete request", ExceptionSrc);
                }
                if (requestId == 0 || requestId == null)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                    throw exception;
                }
                //درخواست اصلی از دیتابیس بازیابی شود
                Request request = requestRepository.GetById(requestId, false);
                //اگر درخواست اصلی در ابتدای جریان کاری خود باشد و هیچ تایید روی آن صورت نگرفته باشد باید جلوی درخواست لغو آن گرفته شود و به کاربر پیشنهاد شود درخواست حذف کند
                if (request.EndFlow == false && request.RequestStatusList.Count == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestFlowNotStarted, "جریان درخواست اصلی شروع نشده, شما می توانید درخواست را مستقیما حذف نمایید", ExceptionSrc));
                    throw exception;
                }
                //اگر درخواست اصلی خودش از نوع لغو درخواست بود نمی توان درخواست لغو روی آن ثبت کرد
                if (request.Precard.Code == "141")
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestFlowNotStarted, "امکان ثبت درخواست لغو برای درخواستی که خود از نوع درخواست لغو است, نمی باشد", ExceptionSrc));
                    throw exception;
                }
                //اگر درخواست اصلی حذف و یا رد شده بوده نمی توان درخواست لغو ثبت کرد
                if (request.RequestStatusList.Where(x => x.EndFlow == true && (x.Confirm == false || x.IsDeleted == true)).Count() > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestFlowNotStarted, "امکان ثبت درخواست لغو روی درخواستی که توسط مدیر حذف یا رد گردیده است , نمی باشد", ExceptionSrc));
                    throw exception;
                }
                //اگر درخواست اصلی, قبلا برای آن درخواست لغو ثبت شده بود اجازه  ثبت مجدد ندهد
                int rowCount = CurrentNHSession.QueryOver<Request>(() => requestAlias).Where(() => requestAlias.Parent.ID == requestId).RowCount();
                if (rowCount > 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestFlowNotStarted, "برای درخواست انتخاب شده قبلا درخواست لغو ثبت شده است", ExceptionSrc));
                    throw exception;
                }
                //یک درخواست جدید با پیشکارت درخواست لغو با و رفرنس درخواست اصلی ثبت گردد

                Precard precard = precardBusiness.GetPrecardByCode("141");
                //TODO//safaei//convert Miladi from database to shamsi and set to terminateRequest

                //DateTime PersianFromDate = Convert.ToDateTime(Utility.ToPersianDateTime(request.FromDate).PersianDate);
                //DateTime PersianToDate = Convert.ToDateTime(Utility.ToPersianDateTime(request.ToDate).PersianDate);               

                Request terminateRequest = new Request()
                {
                    Parent = request,
                    Precard = precard,
                    //FromDate = PersianFromDate,
                    //ToDate = PersianToDate,
                    //TheFromDate = String.Format("{0}/{1}/{2}", PersianFromDate.Year, PersianFromDate.Month, PersianFromDate.Day),
                    //TheToDate = String.Format("{0}/{1}/{2}", PersianToDate.Year, PersianToDate.Month, PersianToDate.Day),
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    TheFromDate = Utility.ToPersianDate(request.FromDate),
                    TheToDate = Utility.ToPersianDate(request.ToDate),
                    FromTime = request.FromTime,
                    ToTime = request.ToTime,
                    TheToTime = Utility.IntTimeToRealTime(request.ToTime),
                    TheFromTime = Utility.IntTimeToRealTime(request.FromTime),
                    TimeDuration = request.TimeDuration,
                    TheTimeDuration = Utility.IntTimeToRealTime(request.TimeDuration),
                    Description = ActionDescription,
                    //RegistrationDate=??
                    //User=??,
                    OperatorUser = request.OperatorUser,
                    AttachmentFile = null,
                    EndFlow = false,
                    IsEdited = false,
                    IsDateSetByUser = true
                };
                //Insert Request-------------------------------------
                try
                {
                    if (!ValidatePrecardForCurrentUser(terminateRequest))
                    {
                        throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                    }
                    //DNN Note:Improve Performance
                    terminateRequest.Person = new Person();
                    //IRegisteredRequests t = new BKartabl();
                    //BRequest busRequest = new BRequest();
                    request = this.bRequest.InsertRequest(terminateRequest);

                    //int count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                    int count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                    //End of DNN Note
                    return count;
                }
                catch (Exception ex)
                {
                    BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequest");
                    throw ex;
                }
                //-------------------------------------
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "TerminateRequest");
                throw ex;
            }
        }


        #region Operator Insert
        /// <summary>
        /// ثبت درخواست توسط اپراتور
        /// </summary>
        /// <param name="request"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertRequest(Request request, int year, int month, decimal personId)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                {
                    throw new IllegalServiceAccess("دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                }
                //DNN Note:Improve Performance
                request.IsDateSetByUser = true;
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                request.Person = new Person() { ID = personId == 0 ? -1 : personId };
                request = this.bRequest.InsertRequest(request);

                //int count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                int count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //ENd of DNN Note
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequest by Operator");
                throw ex;
            }
        }

        /// <summary>
        /// ثبت تردد انبوه برای همه پرسنل تحت مدیریت
        /// </summary>
        /// <param name="request"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertCollectiveRequest(Request request, IList<decimal> checkedPersons, int year, int month, out int registReqeustFailedCount)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                {
                    throw new IllegalServiceAccess("دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                }
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount("", PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchMethodBaseByPage(0, count, "", PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where checkedPersons.Contains(o.ID)
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Request) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                    }
                }
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //END OF DNN
                this.LogCollectiveRequest(request, list.Count);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }

        /// <summary>
        /// ثبت تردد ابوده توسط اپراتور برای پرسنل انتخابی
        /// </summary>
        /// <param name="request"></param>
        /// <param name="proxy"></param>
        /// <param name="checkedPersons">برای این افراد نباید درخواست ثبت شود</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertCollectiveRequest(Request request, PersonAdvanceSearchProxy proxy, IList<decimal> checkedPersons, int year, int month, out int registReqeustFailedCount)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                {
                    throw new IllegalServiceAccess("دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                }
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where checkedPersons.Contains(o.ID)
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Request) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                    }
                }
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                this.LogCollectiveRequest(request, list.Count);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }

        /// <summary>
        /// ثبت تردد ابوده توسط اپراتور برای پرسنل انتخابی
        /// </summary>
        /// <param name="request"></param>
        /// <param name="proxy"></param>
        /// <param name="checkedPersons">برای این افراد نباید درخواست ثبت شود</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        int IRegisteredRequests.InsertCollectiveRequest(Request request, string quickSearch, IList<decimal> checkedPersons, int year, int month, out int registReqeustFailedCount)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                {
                    throw new IllegalServiceAccess("دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                }
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(quickSearch, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchMethodBaseByPage(0, count, quickSearch, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where checkedPersons.Contains(o.ID)
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Request) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                    }
                }
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //END OF DNN Note
                this.LogCollectiveRequest(request, list.Count);
                return count;
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }

        int IRegisteredRequests.InsertCollectiveRequest(Request request, string quickSearch, int year, int month, out int registReqeustFailedCount)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                {
                    throw new IllegalServiceAccess("دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                }
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(quickSearch, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchByPage(0, count, quickSearch, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();
                //DNN Note : Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Request) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                    }
                }
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                this.LogCollectiveRequest(request, list.Count);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }

        int IRegisteredRequests.InsertCollectiveRequest(Request request, PersonAdvanceSearchProxy proxy, int year, int month, out int registReqeustFailedCount)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                {
                    throw new IllegalServiceAccess("دسترسی غیر مجاز به پیشکارت", ExceptionSrc);
                }
                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Request) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                    }

                }
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //END of DNN Note
                this.LogCollectiveRequest(request, list.Count);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequest by Operator");
                throw ex;
            }
        }



        #endregion

        #region Operator UnderManagment Grid

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, string searchValue)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }
                ISearchPerson searchTool = new BPerson();
                IList<Person> prsList = searchTool.QuickSearchByPage(pageIndex, pageSize, searchValue, PersonCategory.Operator_UnderManagment);
                var l = from prs in prsList
                        select prs.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }

                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, string quickSearch, IList<decimal> uncheckedPersons)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }

                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.QuickSearchByPage(pageIndex, pageSize, quickSearch, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where uncheckedPersons.Contains(o.ID) == false
                        select o.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }
                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, PersonAdvanceSearchProxy proxy)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }

                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        select o.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }
                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        IList<UnderManagmentInfoProxy> IRegisteredRequests.GetAllByPage(int pageIndex, int pageSize, int year, int month, PersonAdvanceSearchProxy proxy, IList<decimal> uncheckedPersons)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(String.Format("{0}/{1}/1", year, month));
                }
                else
                {
                    date = new DateTime(year, month, 1);
                }

                ISearchPerson searchTool = new BPerson();
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where uncheckedPersons.Contains(o.ID) == false
                        select o.ID;

                IList<UnderManagementPerson> underResult = new List<UnderManagementPerson>();
                ManagerRepository managerRepository = new ManagerRepository(false);
                underResult = managerRepository.GetUnderManagment(month, month > 0 ? 0 : Utility.ToDateRangeIndex(date, BLanguage.CurrentSystemLanguage), date.ToString("yyyy/MM/dd"), l.ToList<decimal>(), pageIndex, pageSize);


                IList<UnderManagmentInfoProxy> Result = new List<UnderManagmentInfoProxy>();
                foreach (UnderManagementPerson under in underResult)
                {
                    CalcInfoProxy calcInfoProxy = new CalcInfoProxy();
                    calcInfoProxy.DailyAbsence = under.DailyAbsence;
                    calcInfoProxy.DailyLeave = under.DailyMeritoriouslyLeave;
                    calcInfoProxy.HourlyAbsence = under.HourlyUnallowableAbsence;
                    calcInfoProxy.HourlyLeave = under.HourlyMeritoriouslyLeave;
                    calcInfoProxy.OverTime = under.AllowableOverTime;

                    Result.Add(new UnderManagmentInfoProxy() { PersonID = under.PersonId, PersonName = under.PersonName, PersonCode = under.BarCode, CalcInfo = calcInfoProxy });
                }
                return Result;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        #endregion

        #region Elemets

        /// <summary>
        /// تمام پیشکارتهای ساعتی که میتوان روی آنها درخواست داد را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<Precard> IRegisteredRequests.GetAllHourlyRequestTypes()
        {
            //DNN Note:improve performance
            //BRequest busRequest = new BRequest();

            List<Precard> list = new List<Precard>();
            list.AddRange(this.bRequest.GetAllTraffics());
            list.AddRange(this.bRequest.GetAllHourlyLeaves());
            list.AddRange(this.bRequest.GetAllHourlyDutis());
            list = list.OrderByDescending(c => c.Order).ToList();
            return list;
        }

        /// <summary>
        /// تمام پیشکارتهای روزانه که میتوان روی آنها درخواست داد را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<Precard> IRegisteredRequests.GetAllDailyRequestTypes()
        {
            //DNN Note:Improve Performance
            //BRequest busRequest = new BRequest();

            List<Precard> list = new List<Precard>();
            list.AddRange(this.bRequest.GetAllDailyLeaves());
            list.AddRange(this.bRequest.GetAllDailyDuties());
            list = list.OrderByDescending(c => c.Order).ToList();
            return list;
        }

        /// <summary>
        /// تمام پیشکارتهای اضافه کاری که میتوان روی آنها درخواست داد را برمیگرداند
        /// </summary>
        /// <returns></returns>
        IList<Precard> IRegisteredRequests.GetAllOverTimeRequestTypes()
        {
            //DNN Note:improve performance
            //BRequest busRequest = new BRequest();
            List<Precard> list = new List<Precard>();
            list.AddRange(this.bRequest.GetAllOverWorks());
            list = list.OrderByDescending(c => c.Order).ToList();
            return list;
        }

        IList<Precard> IRegisteredRequests.GetAllImperativeRequestTypes()
        {
            //DNN Note:Improve Performance
            //BRequest busRequest = new BRequest();

            List<Precard> list = new List<Precard>();
            list.AddRange(this.bRequest.GetAllImperatives());
            list = list.OrderByDescending(c => c.Order).ToList();
            return list;
        }


        IList<Doctor> IRegisteredRequests.GetAllDoctors()
        {
            //DNN Note:improve performance
            //BRequest brequest = new BRequest();
            return this.bRequest.GetAllDoctors();
        }

        IList<Illness> IRegisteredRequests.GetAllIllness()
        {
            //DNN Note:improve performance
            //BRequest brequest = new BRequest();
            return this.bRequest.GetAllIllness();
        }

        IList<DutyPlace> IRegisteredRequests.GetAllDutyPlaceRoot()
        {
            //DNN Note:improve performance
            //BRequest brequest = new BRequest();
            return this.bRequest.GetAllDutyPlaceRoot();
        }

        IList<DutyPlace> IRegisteredRequests.GetAllDutyPlaceChild(decimal parentId)
        {
            //DNN Note:improve performance
            //BRequest brequest = new BRequest();
            return this.bRequest.GetAllDutyPlaceChild(parentId);
        }

        #endregion

        /// <summary>
        /// جهت اعمال دسترسی در واسط کاربر
        /// آیا کاربر فعلی اپراتور است
        /// </summary>
        bool IRegisteredRequests.IsCurrentUserOperator
        {
            get
            {
                return this.IsCurrentUserOperator;
                //BOperator op = new BOperator();
                //IList<Operator> theOpp = op.GetOperator(this.GetCurentPersonId());
                //return theOpp.Count > 0 ? true : false;
            }
        }

        #region IRegisteredRequests Search

        IList<Person> IRegisteredRequests.GetAllPerson(int pageIndex, int pageSize)
        {
            return searchTool.QuickSearchByPage(pageIndex, pageSize, String.Empty, PersonCategory.Operator_UnderManagment);
        }

        IList<Person> IRegisteredRequests.QuickSearchByPage(int pageIndex, int pageSize, string searchKey)
        {
            return searchTool.QuickSearchByPage(pageIndex, pageSize, searchKey, PersonCategory.Operator_UnderManagment);
        }

        IList<Person> IRegisteredRequests.GetPersonInAdvanceSearch(PersonAdvanceSearchProxy proxy, int pageIndex, int pageSize)
        {
            return searchTool.GetPersonInAdvanceSearch(proxy, pageIndex, pageSize, PersonCategory.Operator_UnderManagment);
        }

        int IRegisteredRequests.GetPersonCount()
        {
            return searchTool.GetPersonInQuickSearchCount(String.Empty, PersonCategory.Operator_UnderManagment);
        }

        int IRegisteredRequests.GetPersonInQuickSearchCount(string searchValue)
        {
            return searchTool.GetPersonInQuickSearchCount(searchValue, PersonCategory.Operator_UnderManagment);
        }

        int IRegisteredRequests.GetPersonInAdvanceSearchCount(PersonAdvanceSearchProxy proxy)
        {
            return searchTool.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Operator_UnderManagment);
        }

        #endregion

        #endregion

        #region IKartablRequests Members

        /// <summary>
        /// تعداد درخواستها زا با اعمال شرایط برمیگرداند
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        int IKartablRequests.GetRequestCount(RequestType requestType, int year, int month)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
                //decimal substitutePersonId = GetCurenttSubstitute();

                int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, requestType, fromDate, toDate);
                return result;
                /*if (substitutePersonId > 0)
                {
                    int result = bSubstituteKartablUnderManagment.GetRequestCount(managerID, substitutePersonId, requestType, fromDate, toDate);

                    return result;
                }
                else
                {
                    int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, requestType, fromDate, toDate);
                    return result;
                }*/
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        int IKartablRequests.GetRequestCount(string searchKey, int year, int month)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
                //decimal substitutePersonId = GetCurenttSubstitute();

                int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, searchKey, fromDate, toDate);
                return result;

                /* if (substitutePersonId > 0)
                 {
                     IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Substitute_UnderManagment);

                     int result = bSubstituteKartablUnderManagment.GetRequestCount(managerID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate);

                     return result;
                 }
                 else
                 {
                     IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Manager_UnderManagment);

                     int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, searchKey, fromDate, toDate);

                     return result;
                 }*/
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستها را با اعمال جستجو برمیگرداند
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IKartablRequests.GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count, KartablSummaryItems itemSummary, ViewState viewState, string FromDate, string ToDate)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                if (itemSummary == KartablSummaryItems.UnKnown)
                {
                    switch (viewState)
                    {
                        case ViewState.YearMonth:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                            }
                            else
                            {
                                int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                fromDate = new DateTime(year, month, 1);
                                toDate = new DateTime(year, month, endOfMonth);
                            }

                            break;
                        case ViewState.Date:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                fromDate = Utility.ToMildiDate(FromDate);
                                toDate = Utility.ToMildiDate(ToDate);
                            }
                            else
                            {
                                fromDate = Utility.ToMildiDateTime(FromDate);
                                toDate = Utility.ToMildiDateTime(ToDate);
                            }
                            if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                            {
                                exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                            }
                            if (exception.Count > 0)
                            {
                                throw exception;
                            }
                            break;
                    }
                }
                else
                {
                    int fromMonth = 1;
                    int toMonth = 12;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        year = new PersianDateTime(DateTime.Now).Year;
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year - 1, fromMonth, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                    }
                    else
                    {
                        year = DateTime.Now.Year;
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                        fromDate = new DateTime(year - 1, fromMonth, 1);
                        toDate = new DateTime(year, toMonth, endOfMonth);
                    }

                }
                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
                // decimal substitutePersonId = GetCurenttSubstitute();

                result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, searchKey, fromDate, toDate, pageIndex, pageSize, orderby, out count, itemSummary);
                IList<Request> RequestParentList = this.GetRequestParent(result);
                /* if (substitutePersonId > 0)
                 {
                     IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Substitute_UnderManagment);

                     result = bSubstituteKartablUnderManagment.GetAllRequests(manager.ID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate, pageIndex, pageSize, orderby);
                 }
                 else
                 {
                     IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, 100, searchKey, PersonCategory.Manager_UnderManagment);

                     result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, searchKey, fromDate, toDate, pageIndex, pageSize, orderby);
                 }*/

                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();

                    proxy = this.ConvertKartablRequestToProxy(req, managerID, RequestParentList);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// درخواستها را با اعمال شرایط برمیگرداند
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IKartablRequests.GetAllRequests(RequestType requestType, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count)
        {
            try
            {
                DateTime fromDate, toDate;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }
                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
                /* decimal substitutePersonId = GetCurenttSubstitute();
                 if (substitutePersonId > 0)
                 {
                     result = bSubstituteKartablUnderManagment.GetAllRequests(manager.ID, substitutePersonId, requestType, fromDate, toDate, pageIndex, pageSize, orderby);
                 }
                 else
                 {
                     result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, requestType, fromDate, toDate, pageIndex, pageSize, orderby);
                 }*/

                result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, requestType, fromDate, toDate, pageIndex, pageSize, orderby, out count);
                IList<Request> RequestParentList = this.GetRequestParent(result);
                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();
                    // proxy = this.ConvertKartablRequestToProxy(req, managerID, RequestParentList);
                    proxy = this.ConvertKartablRequestToProxy(req, managerID, RequestParentList);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }
                // kartablResult = kartablResult.Distinct(new KartablProxyRequestComparer()).ToList();
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }
        IList<KartablProxy> IKartablRequests.GetAllRequests(RequestType requestType, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count, KartablSummaryItems itemSummary, ViewState viewState, string FromDate, string ToDate)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                if (itemSummary == KartablSummaryItems.UnKnown)
                {
                    switch (viewState)
                    {
                        case ViewState.YearMonth:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                            }
                            else
                            {
                                int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                fromDate = new DateTime(year, month, 1);
                                toDate = new DateTime(year, month, endOfMonth);
                            }

                            break;
                        case ViewState.Date:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                fromDate = Utility.ToMildiDate(FromDate);
                                toDate = Utility.ToMildiDate(ToDate);
                            }
                            else
                            {
                                fromDate = Utility.ToMildiDateTime(FromDate);
                                toDate = Utility.ToMildiDateTime(ToDate);
                            }
                            if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                            {
                                exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                            }
                            if (exception.Count > 0)
                            {
                                throw exception;
                            }
                            break;
                    }
                }
                else
                {
                    int fromMonth = 1;
                    int toMonth = 12;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        year = new PersianDateTime(DateTime.Now).Year;
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year - 1, fromMonth, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                    }
                    else
                    {
                        year = DateTime.Now.Year;
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                        fromDate = new DateTime(year - 1, fromMonth, 1);
                        toDate = new DateTime(year, toMonth, endOfMonth);
                    }

                }
                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                decimal managerID = manager.ID;
                /* decimal substitutePersonId = GetCurenttSubstitute();
                 if (substitutePersonId > 0)
                 {
                     result = bSubstituteKartablUnderManagment.GetAllRequests(manager.ID, substitutePersonId, requestType, fromDate, toDate, pageIndex, pageSize, orderby);
                 }
                 else
                 {
                     result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, requestType, fromDate, toDate, pageIndex, pageSize, orderby);
                 }*/

                result = bManagerKartablUnderManagment.GetAllRequests(manager.ID, requestType, fromDate, toDate, pageIndex, pageSize, orderby, out count, itemSummary);
                IList<Request> RequestParentList = this.GetRequestParent(result);
                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    KartablProxy proxy = new KartablProxy();
                    proxy = this.ConvertKartablRequestToProxy(req, managerID, RequestParentList);
                    proxy.Row = i + 1;

                    kartablResult.Add(proxy);
                }
                // kartablResult = kartablResult.Distinct(new KartablProxyRequestComparer()).ToList();
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }
        /// <summary>
        /// جهت سرویس اطلاع رسانی
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        IList<ContractKartablProxy> IKartablRequests.GetAllRequests(decimal personId)
        {
            try
            {
                IList<ContractKartablProxy> kartablResult = new List<ContractKartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                Manager manager = new BManager().GetManager(personId);
                decimal managerID = manager.ID;
                decimal substitutePersonId = GetCurenttSubstitute(personId);
                if (substitutePersonId > 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    result = bManagerKartablUnderManagment.GetAllRequests(manager.ID);
                }

                for (int i = 0; i < result.Count; i++)
                {
                    InfoRequest req = result[i];
                    ContractKartablProxy proxy = new ContractKartablProxy();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToPersianDate(req.FromDate);
                        proxy.TheToDate = Utility.ToPersianDate(req.ToDate);
                    }
                    else
                    {
                        proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                        proxy.TheFromDate = Utility.ToString(req.FromDate);
                        proxy.TheToDate = Utility.ToString(req.ToDate);
                    }
                    proxy.ID = req.ID;
                    proxy.RequestID = req.ID;
                    proxy.ManagerFlowID = req.mngrFlowID;
                    proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
                    proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                    proxy.Row = i + 1;
                    proxy.RequestTitle = req.PrecardName;
                    proxy.Description = req.Description;
                    proxy.Applicant = req.Applicant;
                    proxy.Barcode = req.PersonCode;
                    proxy.OperatorUser = req.OperatorUser;
                    proxy.RequestSource = ContractRequestSource.Undermanagment.ToString();
                    proxy.PersonId = req.PersonID;
                    kartablResult.Add(proxy);
                }
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// تایید یا عدم تایید
        /// </summary>
        /// <param name="requsts"></param>
        /// <param name="status">تایید یا عدم تایید</param>
        /// <param name="description">توضیح جهت  عدم تایید درخواست</param>
        bool IKartablRequests.SetStatusOfRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            try
            {
                requestValidationProxyList = new List<RequestKartablValidationProxy>();
                UIValidationExceptions exception = new UIValidationExceptions();
                if (requests.Count == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                    throw exception;
                }
                if (status != RequestState.Confirmed && status != RequestState.Unconfirmed)
                    return false;

                var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IKartablRequestsKey);
                foreach (KartableSetStatusProxy req in requests)
                {
                    if (req.RequestID > 0 && !ids.Contains(req.RequestID))
                    {
                        throw new IllegalServiceAccess("XSS Attack on SetStatusOfRequest", ExceptionSrc);
                    }
                }

                //IList<Permit> permitList = new List<Permit>();
                try
                {
                    description = Utility.IsEmpty(description) ? "" : description;
                    bool endFlow = false;
                    bool confirm = false;
                    bool appliedFlowCondition = false;

                    EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
                    EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
                    //DNN Note:Improve Performance
                    //BRequest requestBusiness = new BRequest();
                    RequestKartablValidationProxy requestValidationProxy = null;
                    var mngFlowIds = from req in requests
                                     group req by req.ManagerFlowID;

                    foreach (var mngFlw in mngFlowIds)
                    {
                        decimal mngFlwId = mngFlw.Key;
                        var list = from r in requests
                                   where r.ManagerFlowID == mngFlwId
                                   select r.RequestID;

                        if (status == RequestState.Confirmed)
                        {
                            ManagerFlow mf = mngFlwRep.GetById(mngFlwId, false);
                            bool existsNextLevel = mngFlwRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => mf.Flow), mf.Flow),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => mf.Level), mf.Level + 1, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => mf.Active), true)) > 0;
                            endFlow = existsNextLevel == false;
                            confirm = true;
                        }
                        else if (status == RequestState.Unconfirmed)
                        {
                            endFlow = true;
                            confirm = false;
                        }
                        foreach (decimal reqId in list)
                        {
                            Permit permitObj = new Permit();
                            using (NHibernateSessionManager.Instance.BeginTransactionOn())
                            {
                                try
                                {
                                    if (rsRep.Find().Where(x => x.EndFlow && x.Request.ID == reqId).Count() == 0)
                                    {
                                        //DNN Note:Improve Performance
                                        //Request request = requestBusiness.GetByID(reqId);
                                        Request request = this.bRequest.GetByID(reqId);
                                        if (confirm && request.Precard.Code == "141")
                                        {
                                            endFlow = ProcessTerminateRequest(request, mngFlwId, endFlow, applicatorID);
                                        }

                                        RequestStatus rs = new RequestStatus();
                                        rs.ManagerFlow = new ManagerFlow() { ID = mngFlwId };
                                        rs.Request = new Request() { ID = reqId };
                                        rs.Confirm = confirm;
                                        rs.EndFlow = endFlow;
                                        rs.Description = description;
                                        rs.Date = DateTime.Now;
                                        rs.Applicator = new Person() { ID = applicatorID };

                                        requestValidationProxy = new RequestKartablValidationProxy();
                                        requestValidationProxy.PrecardName = request.Precard.Name;
                                        if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                                            requestValidationProxy.Date = request.FromDate.ToShortDateString();
                                        else
                                            requestValidationProxy.Date = Utility.ToPersianDate(request.FromDate);
                                        requestValidationProxy.PersonName = request.Person.Name;
                                        if (this.CheckRequestValueAppliedFlowConditionValue(mngFlwId, request))
                                        {
                                            appliedFlowCondition = true;
                                            rs.EndFlow = true;
                                        }
                                        else
                                            appliedFlowCondition = false;
                                        if (endFlow || appliedFlowCondition)
                                        {
                                            if (confirm && request.Precard.Code != "141")
                                            {
                                                permitObj = this.SavePermit(request);
                                                //if(permitObj != null)
                                                //   permitList.Add(permitObj);
                                            }
                                            if (rs.EndFlow)
                                            {
                                                request.EndFlow = true;
                                                //new BRequest().SaveChanges(request, UIActionType.EDIT);
                                                this.bRequest.SaveChanges(request, UIActionType.EDIT);
                                            }
                                            else
                                            {
                                                //DNN Note
                                                //جهت بررسی قانون واسط کاربری مهلت تایید درخواست در تمامی مراحل تایید درخواست را بدون تغییر ذخییره می کنیم
                                                //new BRequest().SaveChanges(request, UIActionType.EDIT);
                                                this.bRequest.SaveChanges(request, UIActionType.EDIT);
                                            }
                                        }
                                        else
                                        {
                                            //DNN Note
                                            //جهت بررسی قانون واسط کاربری مهلت تایید درخواست در تمامی مراحل تایید درخواست را بدون تغییر ذخییره می کنیم
                                            //new BRequest().SaveChanges(request, UIActionType.EDIT);
                                            this.bRequest.SaveChanges(request, UIActionType.EDIT);
                                        }
                                        rsRep.WithoutTransactSave(rs);
                                        ///ImperativeRequest Update : IsLocked = false 
                                        if (!confirm)
                                        {
                                            if (request.RequestChildList.Any())
                                            {
                                                string desc = string.Empty;
                                                //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
                                                var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
                                                ManagerFlow managerFlow = mngFlwRep.GetById(mngFlwId, false);
                                                if (managerFlow.Manager.Person != null)
                                                    desc = string.Format("این درخواست به دلیل رد درخواست اصلی توسط {0} حذف گردید", managerFlow.Manager.Person.Name);
                                                else
                                                    desc = string.Format("این درخواست به دلیل تایید درخواست اصلی توسط {0} حذف گردید", managerFlow.Manager.OrganizationUnit.Person.Name);
                                                this.DeleteTerminateRequest(terminateRequestID, mngFlwId, desc);
                                            }
                                            ChangeImperativeRequestLockState(request);
                                        }
                                    }
                                    NHibernateSessionManager.Instance.CommitTransactionOn();
                                }

                                catch (Exception ex)
                                {

                                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequest");
                                    requestValidationProxy.Exception = ex;
                                    requestValidationProxyList.Add(requestValidationProxy);
                                    //permitList = permitList.Where(p =>permitObj != null && p.ID != permitObj.ID).ToList();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequest");
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequest");
                throw ex;
            }
            return true;
        }
        bool IKartablRequests.SetStatusOfRequestByService(IList<KartableSetStatusProxy> requests, RequestState status, string description)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                if (requests.Count == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                    throw exception;
                }
                if (status != RequestState.Confirmed && status != RequestState.Unconfirmed)
                    return false;

                //var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IKartablRequestsKey);
                //foreach (KartableSetStatusProxy req in requests)
                //{
                //    if (req.RequestID > 0 && !ids.Contains(req.RequestID))
                //    {
                //        throw new IllegalServiceAccess("XSS Attack on SetStatusOfRequest", ExceptionSrc);
                //    }
                //}


                try
                {
                    description = Utility.IsEmpty(description) ? "" : description;
                    bool endFlow = false;
                    bool confirm = false;
                    bool appliedFlowCondition = false;
                    EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
                    EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
                    //DNN Note:Improve Performance
                    //BRequest requestBusiness = new BRequest();
                    RequestKartablValidationProxy requestValidationProxy = null;
                    var mngFlowIds = from req in requests
                                     group req by req.ManagerFlowID;

                    foreach (var mngFlw in mngFlowIds)
                    {
                        decimal mngFlwId = mngFlw.Key;
                        var list = from r in requests
                                   where r.ManagerFlowID == mngFlwId
                                   select r.RequestID;
                        if (status == RequestState.Confirmed)
                        {
                            ManagerFlow mf = mngFlwRep.GetById(mngFlwId, false);
                            bool existsNextLevel = mngFlwRep.GetCountByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => mf.Flow), mf.Flow),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => mf.Level), mf.Level + 1, CriteriaOperation.GreaterEqThan),
                                                         new CriteriaStruct(Utility.GetPropertyName(() => mf.Active), true)) > 0;
                            endFlow = existsNextLevel == false;
                            confirm = true;
                        }
                        else if (status == RequestState.Unconfirmed)
                        {
                            endFlow = true;
                            confirm = false;
                        }
                        foreach (decimal reqId in list)
                        {
                            using (NHibernateSessionManager.Instance.BeginTransactionOn())
                            {
                                try
                                {
                                    if (rsRep.Find().Where(x => x.EndFlow && x.Request.ID == reqId).Count() == 0)
                                    {
                                        RequestStatus rs = new RequestStatus();
                                        rs.ManagerFlow = new ManagerFlow() { ID = mngFlwId };
                                        rs.Request = new Request() { ID = reqId };
                                        rs.Confirm = confirm;
                                        rs.EndFlow = endFlow;
                                        rs.Description = description;
                                        rs.Date = DateTime.Now;
                                        //DNN Note:Improve Performance
                                        //Request request = requestBusiness.GetByID(reqId);
                                        Request request = this.bRequest.GetByID(reqId);
                                        requestValidationProxy = new RequestKartablValidationProxy();
                                        requestValidationProxy.PrecardName = request.Precard.Name;
                                        if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                                            requestValidationProxy.Date = request.FromDate.ToShortDateString();
                                        else
                                            requestValidationProxy.Date = Utility.ToPersianDate(request.FromDate);
                                        requestValidationProxy.PersonName = request.Person.Name;
                                        if (this.CheckRequestValueAppliedFlowConditionValue(mngFlwId, request))
                                        {
                                            appliedFlowCondition = true;
                                            rs.EndFlow = true;
                                        }
                                        else
                                            appliedFlowCondition = false;
                                        if (endFlow || appliedFlowCondition)
                                        {
                                            if (confirm)
                                                this.SavePermit(request);
                                            if (rs.EndFlow)
                                            {
                                                request.EndFlow = true;
                                                //new BRequest().SaveChanges(request, UIActionType.EDIT);
                                                this.bRequest.SaveChanges(request, UIActionType.EDIT);
                                            }
                                            else
                                            {
                                                //DNN Note
                                                //جهت بررسی قانون واسط کاربری مهلت تایید درخواست در تمامی مراحل تایید درخواست را بدون تغییر ذخییره می کنیم
                                                //new BRequest().SaveChanges(request, UIActionType.EDIT);
                                                this.bRequest.SaveChanges(request, UIActionType.EDIT);
                                            }
                                        }
                                        else
                                        {
                                            //DNN Note
                                            //جهت بررسی قانون واسط کاربری مهلت تایید درخواست در تمامی مراحل تایید درخواست را بدون تغییر ذخییره می کنیم
                                            //new BRequest().SaveChanges(request, UIActionType.EDIT);
                                            this.bRequest.SaveChanges(request, UIActionType.EDIT);
                                        }
                                        rsRep.WithoutTransactSave(rs);
                                        ///ImperativeRequest Update : IsLocked = false 
                                        if (!confirm)
                                            ChangeImperativeRequestLockState(request);
                                    }
                                    NHibernateSessionManager.Instance.CommitTransactionOn();
                                }

                                catch (Exception ex)
                                {

                                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequest");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequest");
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequest");
                throw ex;
            }
            return true;
        }
        bool IKartablRequests.SetStatusOfSpecialRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            try
            {
                requestValidationProxyList = new List<RequestKartablValidationProxy>();
                UIValidationExceptions exception = new UIValidationExceptions();
                if (requests.Count == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                    throw exception;
                }
                if (status != RequestState.Confirmed && status != RequestState.Unconfirmed)
                    return false;

                var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.ISpecialKartablRequestsKey);
                foreach (KartableSetStatusProxy req in requests)
                {
                    if (req.RequestID > 0 && !ids.Contains(req.RequestID))
                    {
                        throw new IllegalServiceAccess("XSS Attack on SetStatusOfSpecialRequest", ExceptionSrc);
                    }
                }

                try
                {
                    description = Utility.IsEmpty(description) ? "" : description;
                    bool endFlow = false;
                    bool confirm = false;
                    EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
                    EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
                    //DNN Note:improve Performance
                    //BRequest requestBusiness = new BRequest();
                    RequestKartablValidationProxy requestValidationProxy = null;

                    var mngFlowIds = from req in requests
                                     group req by req.ManagerFlowID;

                    foreach (var mngFlw in mngFlowIds)
                    {
                        decimal mngFlwId = mngFlw.Key;
                        var list = from r in requests
                                   where r.ManagerFlowID == mngFlwId
                                   select r.RequestID;
                        if (status == RequestState.Confirmed)
                        {
                            endFlow = true;
                            confirm = true;
                        }
                        else if (status == RequestState.Unconfirmed)
                        {
                            endFlow = true;
                            confirm = false;
                        }
                        foreach (decimal reqId in list)
                        {
                            using (NHibernateSessionManager.Instance.BeginTransactionOn())
                            {
                                try
                                {
                                    RequestStatus rs = new RequestStatus();
                                    rs.ManagerFlow = new ManagerFlow() { ID = mngFlwId };
                                    rs.Request = new Request() { ID = reqId };
                                    rs.Confirm = confirm;
                                    rs.EndFlow = endFlow;
                                    rs.IsDeleted = false;
                                    rs.Description = description;
                                    rs.Date = DateTime.Now;
                                    rs.Applicator = new Person() { ID = applicatorID };

                                    //DNN Note:improve Performance
                                    //Request request = new BRequest().GetByID(reqId);
                                    Request request = this.bRequest.GetByID(reqId);
                                    requestValidationProxy = new RequestKartablValidationProxy();
                                    requestValidationProxy.PrecardName = request.Precard.Name;
                                    if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                                        requestValidationProxy.Date = request.FromDate.ToShortDateString();
                                    else
                                        requestValidationProxy.Date = Utility.ToPersianDate(request.FromDate);
                                    requestValidationProxy.PersonName = request.Person.Name;
                                    IList<RequestStatus> endFlowRequestStatus = request.RequestStatusList.Where(x => x.ManagerFlow.Active &&
                                                                                                                     x.EndFlow)
                                                                                                         .ToList<RequestStatus>();
                                    foreach (RequestStatus endFlowRS in endFlowRequestStatus)
                                    {
                                        endFlowRS.EndFlow = false;
                                        rsRep.WithoutTransactSave(endFlowRS);
                                    }
                                    this.SavePermit(request);

                                    request.EndFlow = endFlow;
                                    //DNN Note:improve Performance
                                    this.bRequest.SaveChanges(request, UIActionType.EDIT);

                                    rsRep.WithoutTransactSave(rs);
                                    ///ImperativeRequest Update : IsLocked = false && Delete all Permits for this request
                                    if (confirm)
                                    {
                                        if (request.Precard.Code == "141" && request.Parent != null)
                                        {
                                            ManagerFlow managerFlow = mngFlwRep.GetById(mngFlwId, false);
                                            string desc = string.Empty;
                                            if (managerFlow.Manager.Person != null)
                                                desc = string.Format("این درخواست به دلیل تایید درخواست لغو آن توسط {0} حذف گردید", managerFlow.Manager.Person.Name);
                                            else
                                                desc = string.Format("این درخواست به دلیل تایید درخواست لغو آن توسط {0} حذف گردید", managerFlow.Manager.OrganizationUnit.Person.Name);
                                            this.DeleteParentOfTerminateRequest(request.Parent.ID, mngFlwId, desc);
                                        }
                                        //اگر درخواست لغو داشت برو درخواست لغو را هم رد کن
                                        if (request.RequestChildList.Any())
                                        {
                                            string desc = string.Empty;
                                            //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
                                            var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
                                            ManagerFlow managerFlow = mngFlwRep.GetById(mngFlwId, false);
                                            if (managerFlow.Manager.Person != null)
                                                desc = string.Format("این درخواست به دلیل تایید درخواست اصلی توسط {0} حذف گردید", managerFlow.Manager.Person.Name);
                                            else
                                                desc = string.Format("این درخواست به دلیل تایید درخواست اصلی توسط {0} حذف گردید", managerFlow.Manager.OrganizationUnit.Person.Name);
                                            this.DeleteTerminateRequest(terminateRequestID, mngFlwId, desc);
                                        }
                                    }
                                    if (!confirm)
                                    {
                                        new BPermit().DeleteByRequestId(request.ID, request.Person.ID, request.FromDate);

                                        //اگر درخواست لغو داشت برو درخواست لغو را هم رد کن
                                        if (request.RequestChildList.Any())
                                        {
                                            string desc = string.Empty;
                                            //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
                                            var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
                                            ManagerFlow managerFlow = mngFlwRep.GetById(mngFlwId, false);
                                            if (managerFlow.Manager.Person != null)
                                                desc = string.Format("این درخواست به دلیل رد درخواست اصلی توسط {0} حذف گردید", managerFlow.Manager.Person.Name);
                                            else
                                                desc = string.Format("این درخواست به دلیل تایید درخواست اصلی توسط {0} حذف گردید", managerFlow.Manager.OrganizationUnit.Person.Name);
                                            this.DeleteTerminateRequest(terminateRequestID, mngFlwId, desc);
                                        }

                                        ChangeImperativeRequestLockState(request);
                                    }
                                    NHibernateSessionManager.Instance.CommitTransactionOn();
                                }
                                catch (Exception ex)
                                {
                                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfSpecialRequest");
                                    requestValidationProxy.Exception = ex;
                                    requestValidationProxyList.Add(requestValidationProxy);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfSpecialRequest");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfSpecialRequest");
                throw ex;
            }
            return true;
        }

        protected bool ProcessTerminateRequest(Request request, decimal mngFlwId, bool endFlow, decimal applicatorID)
        {
            IList<decimal> ids = new List<decimal>();
            bool EndFlowTerminate = false;
            IList<KartableSetStatusProxy> parentRequestProxyList = new List<KartableSetStatusProxy>();
            IList<RequestKartablValidationProxy> requestValidationProxyList = new List<RequestKartablValidationProxy>();
            var parentRequest = request.Parent;
            //اگر درخواست اصلی و درخواست لغو هر دو در یک مرحله باشند - مهم نیست که آخرین مرحله باشد یا خیر
            //درخواست اصلی و درخواست لغو آن هر دو باید تایید نهایی شوند و جریان آنها به اتمام برسد و درخواست اصلی نیز باید لغو گردد
            if (request.RequestStatusList.Count + 1 == parentRequest.RequestStatusList.Count)
            {
                //درخواست پدر در جریان کاری خود حذف شود و در صورت وجود پرمیت آن نیز حذف گردد
                ids.Add(parentRequest.ID);
                SessionHelper.SaveSessionValue(SessionHelper.ISpecialKartablRequestsKey, ids);

                parentRequestProxyList.Add(new KartableSetStatusProxy() { RequestID = parentRequest.ID, ManagerFlowID = mngFlwId });
                DeleteSpecialRequest(parentRequestProxyList, string.Empty, out requestValidationProxyList, applicatorID, Caller.Kartable);
                EndFlowTerminate = true;
            }
            //اگر درخواست اصلی تایید نهایی شده باشد و درخواست لغو در آخرین مرحله تایید باشد
            //درخواست لغو تایید نهایی می شود و درخواست اصلی نیز باید حذف گردد
            else if (request.RequestStatusList.Count + 1 <= parentRequest.RequestStatusList.Count && parentRequest.EndFlow && endFlow)
            {
                //درخواست پدر در جریان کاری خود حذف شود و در صورت وجود پرمیت آن نیز حذف گردد
                ids.Add(parentRequest.ID);
                SessionHelper.SaveSessionValue(SessionHelper.ISpecialKartablRequestsKey, ids);

                parentRequestProxyList.Add(new KartableSetStatusProxy() { RequestID = parentRequest.ID, ManagerFlowID = mngFlwId });
                DeleteSpecialRequest(parentRequestProxyList, string.Empty, out requestValidationProxyList, applicatorID, Caller.Kartable);
                EndFlowTerminate = true;
            }
            //اگر درخواست اصلی تایید نهایی نشده باشد و درخواست لغو قبل تر از مرحله درخواست اصلی باشد و در ممرحله آخر نیز نباشد 

            else if (request.RequestStatusList.Count + 1 <= parentRequest.RequestStatusList.Count && !parentRequest.EndFlow)
            {
                //درخواست لغو تایید می شود و به مرحله بعد می رود
                //ادامه جریان SetStatusOfRequest
                EndFlowTerminate = false;
            }
            return EndFlowTerminate;
        }

        private void ChangeImperativeRequestLockState(Request request)
        {
            BImperativeRequest imperativeRequestBusiness = new BImperativeRequest();
            if (request.Precard.IsMonthly && request.Precard.PrecardGroup.IntLookupKey == 6)
            {
                int year = 0;
                int month = 0;
                ImperativeRequest ImperativeRequest = new ImperativeRequest();
                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Parsi:
                        PersianCalendar pCal = new PersianCalendar();
                        year = pCal.GetYear(request.FromDate);
                        month = pCal.GetMonth(request.FromDate);
                        break;
                    case LanguagesName.English:
                        year = request.FromDate.Year;
                        month = request.FromDate.Month;
                        break;
                }
                ImperativeRequest imperativeRequest = new ImperativeRequest()
                {
                    Person = new Person() { ID = request.Person.ID },
                    Precard = new Precard() { ID = request.Precard.ID },
                    Year = year,
                    Month = month
                };
                ImperativeRequest impReq = imperativeRequestBusiness.GetImperativeRequest(imperativeRequest);
                if (impReq != null)
                {
                    impReq.IsLocked = false;
                    imperativeRequestBusiness.SaveChanges(impReq, UIActionType.EDIT);
                }
            }
        }

        /// <summary>
        /// پیشینه یک درخواست
        /// </summary>
        /// <param name="requestId"></param>
        KartablRequestHistoryProxy IKartablRequests.GetRequestHistory(decimal requestId)
        {
            KartablRequestHistoryProxy proxy = new KartablRequestHistoryProxy();
            Request request = requestRep.GetById(requestId, false);
            if (request == null)
            {
                throw new ItemNotExists("درخواست موردنظر در دیتابیس موجود نمیباشد", ExceptionSrc);
            }
            IList<Request> list = requestRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => request.Person), request.Person),
                                          new CriteriaStruct(Utility.GetPropertyName(() => request.Precard), request.Precard),
                                          new CriteriaStruct(Utility.GetPropertyName(() => request.ToDate), request.ToDate, CriteriaOperation.LessThan)).OrderBy(x => x.ToDate).ToList();
            #region Hourly Items
            if (request.Precard.IsHourly)
            {
                if (list.Count > 0)
                {
                    Request r = list.Last();

                    if (r.FromTime >= 0)
                        proxy.From = Utility.IntTimeToTime(r.FromTime);
                    if (r.ToTime >= 0)
                        proxy.To = Utility.IntTimeToTime(r.ToTime);

                    proxy.Description = r.Description;
                }
                //در مورد درخواستهای تردد بی معنی است
                if (!request.Precard.PrecardGroup.LookupKey.ToLower().Equals(PrecardGroupsName.traffic.ToString().ToLower()))
                {
                    DateTime date = request.ToDate;
                    DateTime monthStart = new DateTime();
                    DateTime monthEnd = new DateTime();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month);
                        monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, 1));
                        monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, endOfMonth));
                    }
                    else
                    {
                        monthStart = new DateTime(date.Year, date.Month, 1);
                        monthEnd = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                    }
                    list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                    var a = from o in list
                            select o.ToTime - o.FromTime;
                    int sum = a.Sum();
                    if (sum >= 0)
                        proxy.UesedInMonth = sum == 0 ? "00:00" : Utility.IntTimeToTime(sum);

                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, 12);
                        monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 1, 1));
                        monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 12, endOfMonth));
                    }
                    else
                    {
                        monthStart = new DateTime(date.Year, 1, 1);
                        monthEnd = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));
                    }
                    list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                    var b = from o in list
                            select o.ToTime - o.FromTime;
                    sum = b.Sum();
                    if (sum >= 0)
                        proxy.UesedInYear = sum == 0 ? "00:00" : Utility.IntTimeToTime(sum);

                }
            }
            #endregion

            #region Daily Items
            else if (request.Precard.IsDaily)
            {
                if (list.Count > 0)
                {
                    Request r = list.Last();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.From = Utility.ToPersianDate(r.FromDate);
                        proxy.To = Utility.ToPersianDate(r.ToDate);
                    }
                    else
                    {
                        proxy.From = Utility.ToString(r.FromDate);
                        proxy.To = Utility.ToString(r.ToDate);
                    }

                    proxy.Description = r.Description;
                }

                DateTime date = request.ToDate;
                DateTime monthStart = new DateTime();
                DateTime monthEnd = new DateTime();
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month);
                    monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, 1));
                    monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, Utility.ToPersianDateTime(date).Month, endOfMonth));
                }
                else
                {
                    monthStart = new DateTime(date.Year, date.Month, 1);
                    monthEnd = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                }
                list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                var a = from o in list
                        select (o.ToDate - o.FromDate).Days + 1;
                int sum = a.Sum();
                if (sum >= 0)
                    proxy.UesedInMonth = sum.ToString();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = new PersianCalendar().GetDaysInMonth(Utility.ToPersianDateTime(date).Year, 12);
                    monthStart = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 1, 1));
                    monthEnd = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", Utility.ToPersianDateTime(date).Year, 12, endOfMonth));
                }
                else
                {
                    monthStart = new DateTime(date.Year, 1, 1);
                    monthEnd = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, date.Month));
                }
                list = requestRep.GetAllUsedRequest(request.Person.ID, request.Precard.ID, monthStart, monthEnd);

                var b = from o in list
                        select (o.ToDate - o.FromDate).Days + 1;
                sum = b.Sum();
                if (sum >= 0)
                    proxy.UesedInYear = sum.ToString();
            }
            #endregion

            #region Leave
            if (request.Precard.PrecardGroup.LookupKey.Equals(PrecardGroupsName.leave.ToString()))
            {
                proxy.IsLeave = true;
                int year = 0, month = 0, day = 0;
                ILeaveInfo leaveInfo = new BRemainLeave();

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    year = Utility.ToPersianDateTime(request.ToDate).Year;
                    month = Utility.ToPersianDateTime(request.ToDate).Month;
                    day = Utility.ToPersianDateTime(request.ToDate).Day;
                }
                else
                {
                    year = request.ToDate.Year;
                    month = request.ToDate.Month;
                    day = request.ToDate.Day;
                }
                int mDay, mMinutes, yDay, yMinutes;
                GTSEngineWS.TotalWebServiceClient gtsEngineWS = new GTS.Clock.Business.GTSEngineWS.TotalWebServiceClient();
                gtsEngineWS.GTS_ExecuteByPersonID(BUser.CurrentUser.UserName, request.Person.ID);
                leaveInfo.GetRemainLeaveToEndOfMonth(request.Person.ID, year, month, 0, out mDay, out mMinutes);
                leaveInfo.GetRemainLeaveToEndOfYear(request.Person.ID, year, month, day, out yDay, out yMinutes);

                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    proxy.RemainLeaveInMonth = String.Format(" {0} روز و {1} ساعت", mDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(mMinutes));
                    proxy.RemainLeaveInYear = String.Format(" {0} روز و {1} ساعت", yDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(yMinutes));
                }
                else
                {
                    proxy.RemainLeaveInMonth = String.Format(" {0} day and {1} hours", mDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(mMinutes));
                    proxy.RemainLeaveInYear = String.Format(" {0} day and {1} hours", yDay.ToString(), mMinutes == 0 ? "00:00" : Utility.IntTimeToTime(yMinutes));
                }
            }
            #endregion

            return proxy;
        }

        /// <summary>
        ///   مراحل جریان و وضعیت درخواست در هریک را نشان میدهد
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="managerFlowId"></param>
        /// <returns></returns>
        IList<KartablFlowLevelProxy> IKartablRequests.GetRequestLevelsByManagerFlowID(decimal requestId, decimal managerFlowId)
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
            try
            {
                KartablFlowLevelProxyList = this.GetRequestLevelsByOperationFlow(requestId, managerFlowId);
                return KartablFlowLevelProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestLevels");
                throw ex;
            }
        }

        IList<KartablFlowLevelProxy> IKartablRequests.GetSpecialRequestLevels(decimal requestId, decimal managerFlowId)
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
            try
            {
                KartablFlowLevelProxyList = this.GetSpecialRequestLevelsByOperationFlow(requestId, managerFlowId);
                return KartablFlowLevelProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestLevels");
                throw ex;
            }
        }


        IList<KartablFlowLevelProxy> IKartablRequests.GetRequestLevelsByPersonnelID(decimal requestId, decimal personnelID)
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
            try
            {
                KartablFlowLevelProxyList = this.GetRequestLevelsByPendingFlow(requestId, personnelID);
                return KartablFlowLevelProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestLevels");
                throw ex;
            }
        }

        IList<KartablFlowLevelProxy> IKartablRequests.GetRequestLevelsByRequestID(decimal requestId)
        {
            try
            {
                IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
                NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
                Request requestAlias = null;
                RequestStatus requestStatusAlias = null;
                IList<RequestStatus> RequestStatusList = NHSession.QueryOver<RequestStatus>(() => requestStatusAlias)
                                                                  .JoinAlias(() => requestStatusAlias.Request, () => requestAlias)
                                                                  .Where(() => requestAlias.ID == requestId)
                                                                  .List<RequestStatus>();
                foreach (RequestStatus requestStatusItem in RequestStatusList)
                {
                    KartablFlowLevelProxy proxy = new KartablFlowLevelProxy();
                    proxy.ManagerName = requestStatusItem.ManagerFlow.Manager.ThePerson.Name;
                    if (requestStatusItem.IsDeleted)
                        proxy.RequestStatus = RequestState.Deleted;
                    else if (!requestStatusItem.Confirm)
                        proxy.RequestStatus = RequestState.Unconfirmed;
                    else if (requestStatusItem.Confirm)
                        proxy.RequestStatus = RequestState.Confirmed;
                    else
                        proxy.RequestStatus = RequestState.UnderReview;
                    proxy.Description = requestStatusItem.Description;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                        proxy.TheDate = Utility.ToPersianDate(requestStatusItem.Date);
                    else
                        proxy.TheDate = Utility.ToString(requestStatusItem.Date);
                    KartablFlowLevelProxyList.Add(proxy);
                }
                return KartablFlowLevelProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestLevelsByRequestID");
                throw ex;
            }
        }


        private IList<KartablFlowLevelProxy> GetRequestLevelsByOperationFlow(decimal requestId, decimal managerFlowId)
        {
            IList<KartablFlowLevelProxy> result = new List<KartablFlowLevelProxy>();
            IList<decimal> RequestStatusIdsList = new List<decimal>();
            EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
            EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
            ManagerFlow mf = mngFlwRep.GetById(managerFlowId, false);
            IList<ManagerFlow> managerFlowList = mngFlwRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => mf.Flow), mf.Flow));
            managerFlowList = managerFlowList.Where(x => x.Active).OrderBy(x => x.Level).ToList();

            KartablFlowLevelProxy proxy = null;

            foreach (ManagerFlow mngf in managerFlowList)
            {
                proxy = new KartablFlowLevelProxy();
                if (mngf.Manager.Person != null)
                    proxy.ManagerName = mngf.Manager.ThePerson.Name;
                else
                    proxy.ManagerName = mngf.Manager.TheOrganizationUnit.Name + " " + "(" + mngf.Manager.ThePerson.Name + ")";
                IList<RequestStatus> rsList = rsRep.GetByCriteria(new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().ManagerFlow), mngf),
                                                                  new CriteriaStruct(Utility.GetPropertyName(() => new RequestStatus().Request), new Request() { ID = requestId }));
                if (rsList.Count > 0)
                {
                    RequestStatus rs = rsList.First();
                    RequestStatusIdsList.Add(rs.ID);
                    proxy.Description = rs.Description;
                    if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                    {
                        proxy.TheDate = Utility.ToPersianDate(rs.Date);
                    }
                    else
                    {
                        proxy.TheDate = Utility.ToString(rs.Date);
                    }
                    proxy.TheTime = rs.Date.TimeOfDay.ToString();
                    if (rs.IsDeleted)
                        proxy.RequestStatus = RequestState.Deleted;
                    else if (rs.Confirm)
                        proxy.RequestStatus = RequestState.Confirmed;
                    else
                        proxy.RequestStatus = RequestState.Unconfirmed;
                    if (rs.Applicator != null)
                        proxy.Applicator = rs.Applicator.FirstName + " " + rs.Applicator.LastName;
                }
                else
                {
                    proxy.RequestStatus = RequestState.UnderReview;
                }
                result.Add(proxy);
                if (rsList.Count > 0)
                {
                    if (rsList.First().EndFlow)
                    {
                        IList<KartablFlowLevelProxy> trashUnderReviewKartablFlowLevelProxyList = result.Where(x => x.RequestStatus == RequestState.UnderReview).ToList<KartablFlowLevelProxy>();
                        result = result.Except(trashUnderReviewKartablFlowLevelProxyList).ToList<KartablFlowLevelProxy>();
                        break;
                    }
                }
            }

            IList<RequestStatus> SpecialRequestStatusList = this.GetSpecialRequestStatusList(requestId, RequestStatusIdsList);
            foreach (RequestStatus specialRequestStatusItem in SpecialRequestStatusList)
            {
                proxy = new KartablFlowLevelProxy();
                proxy.ManagerName = specialRequestStatusItem.ManagerFlow.Manager.ThePerson.Name;
                proxy.Description = specialRequestStatusItem.Description;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    proxy.TheDate = Utility.ToPersianDate(specialRequestStatusItem.Date);
                }
                else
                {
                    proxy.TheDate = Utility.ToString(specialRequestStatusItem.Date);
                }
                proxy.TheTime = specialRequestStatusItem.Date.TimeOfDay.ToString();
                if (specialRequestStatusItem.IsDeleted)
                    proxy.RequestStatus = RequestState.Deleted;
                else if (specialRequestStatusItem.Confirm)
                    proxy.RequestStatus = RequestState.Confirmed;
                else if (!specialRequestStatusItem.Confirm)
                    proxy.RequestStatus = RequestState.Unconfirmed;
                else
                    proxy.RequestStatus = RequestState.UnderReview;
                if (proxy.RequestStatus != RequestState.UnderReview)
                {
                    IList<KartablFlowLevelProxy> trashUnderReviewKartablFlowLevelProxyList = result.Where(x => x.RequestStatus == RequestState.UnderReview).ToList<KartablFlowLevelProxy>();
                    result = result.Except(trashUnderReviewKartablFlowLevelProxyList).ToList<KartablFlowLevelProxy>();
                }
                if (specialRequestStatusItem.Applicator != null)
                    proxy.Applicator = specialRequestStatusItem.Applicator.FirstName + " " + specialRequestStatusItem.Applicator.LastName;
                result.Add(proxy);
            }

            return result;
        }

        private IList<RequestStatus> GetSpecialRequestStatusList(decimal requestId, IList<decimal> ExistingRequestStatusIdsList)
        {
            NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
            IList<RequestStatus> SpecialRequestStatusList = null;
            Request requestAlias = null;
            RequestStatus requestStatusAlias = null;
            IList<decimal> accessableIDs = ExistingRequestStatusIdsList;

            if (accessableIDs.Count < this.OperationBatchSizeValue && this.OperationBatchSizeValue < 2100)
            {
                SpecialRequestStatusList = NHSession.QueryOver(() => requestStatusAlias)
                                                    .JoinAlias(() => requestStatusAlias.Request, () => requestAlias)
                                                    .Where(() => requestAlias.ID == requestId &&
                                                                !requestStatusAlias.ID.IsIn(ExistingRequestStatusIdsList.ToArray()) &&
                                                                 requestStatusAlias.EndFlow)
                                                    .List<RequestStatus>();
            }
            else
            {
                GTS.Clock.Model.Temp.Temp tempAlias = null;
                string operationGUID = bTemp.InsertTempList(accessableIDs);
                var requestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
                                                                    .JoinAlias(() => requestStatusAlias.TempList, () => tempAlias)
                                                                    .Where(() => tempAlias.OperationGUID == operationGUID)
                                                                    .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
                                                                    .Select(x => x.ID);


                SpecialRequestStatusList = NHSession.QueryOver(() => requestStatusAlias)
                                                    .JoinAlias(() => requestStatusAlias.Request, () => requestAlias)
                                                    .Where(() => requestAlias.ID == requestId &&
                                                                 requestStatusAlias.EndFlow)
                                                    .WithSubquery.WhereNotExists(requestStatusSubQuery).List<RequestStatus>();

                bTemp.DeleteTempList(operationGUID);
            }
            return SpecialRequestStatusList;
        }

        private IList<KartablFlowLevelProxy> GetSpecialRequestLevelsByOperationFlow(decimal requestId, decimal managerFlowId)
        {
            IList<KartablFlowLevelProxy> result = new List<KartablFlowLevelProxy>();
            NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
            Request requestAlias = null;
            RequestStatus requestStatusAlias = null;
            Flow flowAlias = null;
            ManagerFlow managerFlowAlias = null;
            Manager managerAlias = null;
            KartablFlowLevelProxy proxy = null;

            IList<RequestStatus> RequestStatusList = NHSession.QueryOver<RequestStatus>(() => requestStatusAlias)
                                                              .JoinAlias(() => requestStatusAlias.Request, () => requestAlias)
                                                              .Where(() => requestAlias.ID == requestId)
                                                              .List<RequestStatus>();

            IList<RequestStatus> filteredRequestStatusList = new List<RequestStatus>();
            for (int i = 0; i < RequestStatusList.Count(); i++)
            {
                if (RequestStatusList.Count > 1)
                {
                    if (i + 1 < RequestStatusList.Count)
                    {
                        if (RequestStatusList[i].ManagerFlow.Manager.ID == RequestStatusList[i + 1].ManagerFlow.Manager.ID)
                            continue;
                        else
                            filteredRequestStatusList.Add(RequestStatusList[i]);
                    }
                    else
                        filteredRequestStatusList.Add(RequestStatusList[i]);
                }
                else
                    filteredRequestStatusList.Add(RequestStatusList[i]);
            }

            foreach (RequestStatus rs in filteredRequestStatusList)
            {
                proxy = new KartablFlowLevelProxy();
                proxy.RequestStatusID = rs.ID;
                proxy.ManagerName = rs.ManagerFlow.Manager.ThePerson.Name;
                if (rs.IsDeleted)
                    proxy.RequestStatus = RequestState.Deleted;
                else if (!rs.Confirm)
                    proxy.RequestStatus = RequestState.Unconfirmed;
                else if (rs.Confirm)
                    proxy.RequestStatus = RequestState.Confirmed;
                else
                    proxy.RequestStatus = RequestState.UnderReview;

                proxy.Description = rs.Description;
                if (BLanguage.CurrentLocalLanguage == LanguagesName.Parsi)
                {
                    proxy.TheDate = Utility.ToPersianDate(rs.Date);
                }
                else
                {
                    proxy.TheDate = Utility.ToString(rs.Date);
                }
                proxy.TheTime = rs.Date.TimeOfDay.ToString();
                if (rs.Applicator != null)
                    proxy.Applicator = rs.Applicator.FirstName + " " + rs.Applicator.LastName;
                result.Add(proxy);
            }

            if (result.Count > 0 && result.Last().RequestStatus == RequestState.Confirmed)
            {
                RequestStatus requestStatus = filteredRequestStatusList.Where(x => x.ID == result.Last().RequestStatusID).FirstOrDefault();
                if (requestStatus != null && !requestStatus.EndFlow)
                {
                    Request request = this.bRequest.GetByID(requestId);
                    this.NHSession.Evict(request);
                    if (!request.EndFlow)
                    {
                        IList<ManagerFlow> managerFlowList = this.NHSession.QueryOver<ManagerFlow>(() => managerFlowAlias)
                                                                           .JoinAlias(() => managerFlowAlias.Manager, () => managerAlias)
                                                                           .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                                                           .Where(() => !flowAlias.IsDeleted &&
                                                                                         flowAlias.ActiveFlow &&
                                                                                         managerAlias.Active &&
                                                                                         managerFlowAlias.Active &&
                                                                                         managerFlowAlias.Level > requestStatus.ManagerFlow.Level &&
                                                                                         flowAlias.ID == requestStatus.ManagerFlow.Flow.ID
                                                                                 )
                                                                           .OrderBy(x => x.Level).Asc
                                                                           .List<ManagerFlow>();

                        foreach (ManagerFlow managerFlowItem in managerFlowList)
                        {
                            proxy = new KartablFlowLevelProxy();
                            proxy.RequestStatusID = 0;
                            proxy.ManagerName = managerFlowItem.Manager.ThePerson.Name;
                            proxy.RequestStatus = RequestState.UnderReview;
                            proxy.Description = string.Empty;
                            result.Add(proxy);
                        }
                    }
                }
            }

            if (result.Count == 0)
            {
                Request request = bRequest.GetByID(requestId);
                if (request.RequestSubstitute != null)
                {
                    proxy = new KartablFlowLevelProxy();
                    proxy.RequestStatusID = 0;
                    proxy.ManagerName = request.RequestSubstitute.SubstitutePerson.Name;
                    proxy.Description = request.RequestSubstitute.Description;
                    if (request.RequestSubstitute.Confirmed.HasValue)
                    {
                        if (request.RequestSubstitute.Confirmed.Value)
                            proxy.RequestStatus = RequestState.Confirmed;
                        else
                            proxy.RequestStatus = RequestState.Unconfirmed;
                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                proxy.TheDate = Utility.ToPersianDate(request.RequestSubstitute.OperationDate);
                                break;
                            case LanguagesName.English:
                                proxy.TheDate = Utility.ToString(request.RequestSubstitute.OperationDate);
                                break;
                        }
                        proxy.TheTime = request.RequestSubstitute.OperationDate.TimeOfDay.ToString();
                    }
                    else
                        proxy.RequestStatus = RequestState.UnderReview;
                    this.NHSession.Evict(request);
                    result.Add(proxy);
                }
            }
            return result;
        }


        private IList<KartablFlowLevelProxy> GetRequestLevelsByPendingFlow(decimal requestId, decimal personnelId)
        {
            IList<KartablFlowLevelProxy> KartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
            IList<RegisteredRequestsFlowLevel> RegisteredRequestsFlowLevelList = this.requestStatusRep.GetRequestLevelsByPendingFlow(requestId, personnelId);
            RegisteredRequestsFlowLevelList = RegisteredRequestsFlowLevelList.Distinct(new ManagerComparer()).ToList();
            foreach (RegisteredRequestsFlowLevel registeredRequestsFlowLevelItem in RegisteredRequestsFlowLevelList)
            {
                KartablFlowLevelProxyList.Add(new KartablFlowLevelProxy() { ManagerName = registeredRequestsFlowLevelItem.ManagerName });
            }
            return KartablFlowLevelProxyList;
        }

        /// <summary>
        /// تعداد فلتر کارتابل
        /// </summary>
        /// <param name="fliters"></param>      
        int IKartablRequests.GetRequestsByFilterCount(IList<RequestFliterProxy> fliters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// فلتر کارتابل
        /// </summary>
        /// <param name="fliters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        IList<KartablProxy> IKartablRequests.GetAllRequestsByFilter(IList<RequestFliterProxy> fliters, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region IReviewedRequests Members

        /// <summary>
        /// تعداد درخواستها را با اعمال شرایط برمیگرداند
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        int IReviewedRequests.GetRequestCount(RequestState requestState, int year, int month)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                        fromDate = new DateTime(year, month, 1);
                        toDate = new DateTime(year, month, endOfMonth);
                    }
                    //مدیر
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    int result = bmanagerReviewed.GetRequestCount(managerID, requestState, fromDate, toDate);
                    return result;

                    #region comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();
                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        int result = bSubstituteReviewedRequest.GetRequestCount(managerID, substitutePersonId, requestState, fromDate, toDate);
                        return result;
                    }
                    else if (managerID > 0)
                    {
                        int result = bmanagerReviewed.GetRequestCount(managerID, requestState, fromDate, toDate);
                        return result;
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد1", this.workingUsername), ExceptionSrc);
                    }*/

                    #endregion
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد2", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        int IReviewedRequests.GetRequestCount(string searchKey, int year, int month)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                        fromDate = new DateTime(year, month, 1);
                        toDate = new DateTime(year, month, endOfMonth);
                    }
                    //مدیر

                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    int result = bmanagerReviewed.GetRequestCount(managerID, searchKey, fromDate, toDate);
                    return result;
                    #region comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();

                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        //int count = searchTool.GetPersonInQuickSearchCount(searchKey, PersonCategory.Substitute_UnderManagment);
                        //if (count == 0)
                        //    return 0;
                        //IList<Person> quciSearchInUnderManagment = searchTool.QuickSearchByPage(0, count, searchKey, PersonCategory.Substitute_UnderManagment);

                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Substitute_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0)
                            return 0;

                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        int result = bSubstituteReviewedRequest.GetRequestCount(managerID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate);
                        return result;
                    }
                    else if (managerID > 0)
                    {
                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Manager_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0)
                            return 0;
                        int result = bmanagerReviewed.GetRequestCount(managerID, quciSearchInUnderManagment, fromDate, toDate);
                        return result;
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                    }*/

                    #endregion
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestCount");
                throw ex;
            }
        }

        IList<KartablProxy> IReviewedRequests.GetAllRequests(string searchKey, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count, ViewState viewState, string FromDate, string ToDate)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    DateTime fromDate = DateTime.Now;
                    DateTime toDate = DateTime.Now;
                    switch (viewState)
                    {
                        case ViewState.YearMonth:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                            }
                            else
                            {
                                int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                fromDate = new DateTime(year, month, 1);
                                toDate = new DateTime(year, month, endOfMonth);
                            }

                            break;
                        case ViewState.Date:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                fromDate = Utility.ToMildiDate(FromDate);
                                toDate = Utility.ToMildiDate(ToDate);
                            }
                            else
                            {
                                fromDate = Utility.ToMildiDateTime(FromDate);
                                toDate = Utility.ToMildiDateTime(ToDate);
                            }
                            if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                            {
                                exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                            }
                            if (exception.Count > 0)
                            {
                                throw exception;
                            }
                            break;
                    }
                    //مدیر

                    IList<KartablProxy> kartablResult = new List<KartablProxy>();
                    IList<InfoRequest> result = new List<InfoRequest>();
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    result = bmanagerReviewed.GetAllRequests(managerID, searchKey, fromDate, toDate, pageIndex, pageSize, orderby, out count);
                    IList<Request> RequestParentList = this.GetRequestParent(result);
                    #region comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();

                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        //int count = searchTool.GetPersonInQuickSearchCount(searchKey, PersonCategory.Substitute_UnderManagment);
                        //if (count == 0)
                        //{
                        //    return new List<KartablProxy>();
                        //}
                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Substitute_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0) 
                        {
                            return new List<KartablProxy>();
                        }
                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        result = bSubstituteReviewedRequest.GetAllRequests(managerID, substitutePersonId, quciSearchInUnderManagment, fromDate, toDate, pageIndex, pageSize, orderby);
                    }
                    else if (managerID > 0)
                    {
                        //int count = searchTool.GetPersonInQuickSearchCount(searchKey, PersonCategory.Manager_UnderManagment);
                        //if (count == 0)
                        //{
                        //    return new List<KartablProxy>();
                        //}
                        IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Manager_UnderManagment);
                        if (quciSearchInUnderManagment == null || quciSearchInUnderManagment.Count == 0)
                        {
                            return new List<KartablProxy>();
                        }
                        result = bmanagerReviewed.GetAllRequests(managerID, quciSearchInUnderManagment, fromDate, toDate, pageIndex, pageSize, orderby);

                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                    }*/

                    #endregion

                    for (int i = 0; i < result.Count; i++)
                    {
                        InfoRequest req = result[i];
                        KartablProxy proxy = new KartablProxy();
                        proxy = this.ConvertReviewdRequestToProxy(req, RequestParentList);
                        proxy.Row = i + 1;

                        kartablResult.Add(proxy);
                    }
                    return kartablResult;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// جهت نمایش در درخواستهای بررسی شده
        /// </summary>
        /// <param name="requestState"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<KartablProxy> IReviewedRequests.GetAllRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize, KartablOrderBy orderby, out int count, ViewState viewState, string FromDate, string ToDate)
        {
            try
            {
                if (GetCurentPersonId() > 0)
                {
                    UIValidationExceptions exception = new UIValidationExceptions();
                    DateTime fromDate = DateTime.Now;
                    DateTime toDate = DateTime.Now;
                    switch (viewState)
                    {
                        case ViewState.YearMonth:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                                fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                                toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                            }
                            else
                            {
                                int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                                fromDate = new DateTime(year, month, 1);
                                toDate = new DateTime(year, month, endOfMonth);
                            }

                            break;
                        case ViewState.Date:
                            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                            {
                                fromDate = Utility.ToMildiDate(FromDate);
                                toDate = Utility.ToMildiDate(ToDate);
                            }
                            else
                            {
                                fromDate = Utility.ToMildiDateTime(FromDate);
                                toDate = Utility.ToMildiDateTime(ToDate);
                            }
                            if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                            {
                                exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                            }
                            if (exception.Count > 0)
                            {
                                throw exception;
                            }
                            break;
                    }
                    //مدیر                   
                    IList<KartablProxy> kartablResult = new List<KartablProxy>();
                    IList<InfoRequest> result = new List<InfoRequest>();
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    decimal managerID = manager.ID;
                    result = bmanagerReviewed.GetAllRequests(managerID, requestState, fromDate, toDate, pageIndex, pageSize, orderby, out count);
                    IList<Request> RequestParentList = this.GetRequestParent(result);
                    #region Comment
                    /*  decimal substitutePersonId = GetCurenttSubstitute();

                    if (substitutePersonId > 0 && new BSubstitute().GetSubstituteManager(substitutePersonId) > 0)
                    {
                        managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        result = bSubstituteReviewedRequest.GetAllRequests(managerID, substitutePersonId, requestState, fromDate, toDate, pageIndex, pageSize, orderby);
                    }
                    else if (managerID > 0)
                    {
                        result = bmanagerReviewed.GetAllRequests(managerID, requestState, fromDate, toDate, pageIndex, pageSize, orderby);
                    }
                    else
                    {
                        throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                    }*/

                    #endregion

                    for (int i = 0; i < result.Count; i++)
                    {
                        InfoRequest req = result[i];
                        KartablProxy proxy = new KartablProxy();

                        proxy = this.ConvertReviewdRequestToProxy(req, RequestParentList);
                        proxy.Row = i + 1;

                        kartablResult.Add(proxy);
                    }
                    return kartablResult;
                }
                else
                {
                    throw new IllegalServiceAccess(String.Format("این سرویس بعللت اعتبارسنجی قابل دسترسی نمیباشد. شناسه کاربری {0} میباشد", this.workingUsername), ExceptionSrc);
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequests");
                throw ex;
            }
        }

        /// <summary>
        /// حذف یک درخواست توسط مدیر
        /// </summary>
        /// <param name="requestId"></param>
        void IReviewedRequests.DeleteRequst(IList<KartableSetStatusProxy> requests, string managerDescription, decimal applicatorID)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                decimal requestId = 0;
                if (requests.Count != 0)
                {
                    requestId = requests[0].RequestID;


                    var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IReviewedRequestsKey);
                    if (!ids.Contains(requestId))
                    {
                        throw new IllegalServiceAccess("XSS Attack kartabl history delete request", ExceptionSrc);
                    }

                    //DNN Note:Improve Performance
                    //BRequest busRequest = new BRequest();
                    //Request request = busRequest.GetByID(requestId);
                    Request request = this.bRequest.GetByID(requestId);

                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    IList<RequestStatus> RequestStatusList = request.RequestStatusList.ToList();
                    IList<RequestStatus> EndFlowRequestStatusList = RequestStatusList.Where(x => x.EndFlow).ToList<RequestStatus>();
                    RequestStatus status = RequestStatusList.Where(x => x.ManagerFlow.Manager.ID == manager.ID).ToList().FirstOrDefault();
                    if (status == null)
                    {
                        decimal substitutePersonId = GetCurenttSubstitute();
                        if (substitutePersonId > 0)
                        {
                            decimal managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                            status = request.RequestStatusList.Where(x => x.ManagerFlow.Manager.ID == managerID).ToList().FirstOrDefault();
                        }
                    }
                    int managerFlowLevel = status.ManagerFlow.Level;
                    int ManagerUpLevelCount = RequestStatusList.Count(r => r.ManagerFlow.Level > managerFlowLevel);
                    if (ManagerUpLevelCount > 0)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.RequestUsedByFlow, "این درخواست بدلیل استفاده در جریان کاری قابل حذف نیست", ExceptionSrc));
                        throw exception;
                    }
                    //آخرین وضعیت درخواست را از حالت اندفلو خارج کن
                    foreach (RequestStatus endFlowRequestStatusItem in EndFlowRequestStatusList)
                    {
                        //DNN Note:Improve Performance
                        endFlowRequestStatusItem.EndFlow = false;
                        this.bRequest.SaveChanges(request, UIActionType.EDIT);
                    }

                    //یک وضعیت حذف جدید برای درخواست ثبت کن
                    if (status != null)
                    {
                        status.IsDeleted = true;
                        status.EndFlow = true;
                        status.Date = DateTime.Now;
                        status.Description = managerDescription;
                        status.Applicator = new Person() { ID = applicatorID };
                        //DNN Note:Improve Performance
                        this.bRequest.SaveChanges(request, UIActionType.EDIT);

                        new BPermit().DeleteByRequestId(requestId, request.Person.ID, request.FromDate);

                        ///ImperativeRequest Update : IsLocked = false
                        ChangeImperativeRequestLockState(request);
                    }

                    //اگر درخواست لغو داشت برو درخواست لغو را هم حذف کن
                    if (request.RequestChildList.Any())
                    {
                        //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
                        var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
                        var managerFlowID = requests[0].ManagerFlowID;
                        string description = string.Format("این درخواست به دلیل حذف درخواست اصلی توسط {0} حذف گردید", manager.Person != null ? manager.Person.Name : manager.OrganizationUnit.Person.Name);
                        DeleteTerminateRequest(terminateRequestID, managerFlowID, description);
                    }
                }
                else
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                if (exception.Count > 0)
                {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteRequst");
                throw ex;
            }
        }

        protected void DeleteParentRequst(IList<KartableSetStatusProxy> requests, string managerDescription)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                decimal requestId = 0;
                //if (requests.Count != 0)
                //{
                requestId = requests[0].RequestID;

                //var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IReviewedRequestsKey);
                //if (!ids.Contains(requestId))
                //{
                //    throw new IllegalServiceAccess("XSS Attack kartabl history delete request", ExceptionSrc);
                //}

                //DNN Note:Improve Performance
                //BRequest busRequest = new BRequest();
                //Request request = busRequest.GetByID(requestId);
                Request request = this.bRequest.GetByID(requestId);

                Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                IList<RequestStatus> RequestStatusList = request.RequestStatusList.ToList();
                IList<RequestStatus> EndFlowRequestStatusList = RequestStatusList.Where(x => x.EndFlow).ToList<RequestStatus>();
                RequestStatus status = RequestStatusList.Where(x => x.ManagerFlow.Manager.ID == manager.ID).ToList().FirstOrDefault();
                if (status == null)
                {
                    decimal substitutePersonId = GetCurenttSubstitute();
                    if (substitutePersonId > 0)
                    {
                        decimal managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                        status = request.RequestStatusList.Where(x => x.ManagerFlow.Manager.ID == managerID).ToList().FirstOrDefault();
                    }
                }
                //int managerFlowLevel = status.ManagerFlow.Level;
                //int ManagerUpLevelCount = RequestStatusList.Count(r => r.ManagerFlow.Level > managerFlowLevel);
                //if (ManagerUpLevelCount > 0)
                //{
                //    exception.Add(new ValidationException(ExceptionResourceKeys.RequestUsedByFlow, "این درخواست بدلیل استفاده در جریان کاری قابل حذف نیست", ExceptionSrc));
                //    throw exception;
                //}
                //آخرین وضعیت درخواست را از حالت اندفلو خارج کن
                foreach (RequestStatus endFlowRequestStatusItem in EndFlowRequestStatusList)
                {
                    endFlowRequestStatusItem.EndFlow = false;
                    //DNN Note:Improve Performance
                    this.bRequest.SaveChanges(request, UIActionType.EDIT);
                }

                //یک وضعیت حذف جدید برای درخواست ثبت کن
                if (status != null)
                {
                    status.IsDeleted = true;
                    status.EndFlow = true;
                    status.Date = DateTime.Now;
                    status.Description = managerDescription;
                    //DNN Note:Improve Performance
                    this.bRequest.SaveChanges(request, UIActionType.EDIT);

                    new BPermit().DeleteByRequestId(requestId, request.Person.ID, request.FromDate);

                    ///ImperativeRequest Update : IsLocked = false
                    ChangeImperativeRequestLockState(request);
                }
                //}
                //else
                //    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                if (exception.Count > 0)
                {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteRequst");
                throw ex;
            }
        }

        void IReviewedRequests.DeleteRequestByService(IList<KartableSetStatusProxy> requests, string managerDescription, Flow flow)
        {
            try
            {
                UIValidationExceptions exception = new UIValidationExceptions();
                decimal requestId = 0;
                if (requests.Count != 0)
                {
                    requestId = requests[0].RequestID;

                    //var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IReviewedRequestsKey);
                    //if (!ids.Contains(requestId))
                    //{
                    //    throw new IllegalServiceAccess("XSS Attack kartabl history delete request", ExceptionSrc);
                    //}

                    //DNN Note:Improve Performance
                    //BRequest busRequest = new BRequest();
                    //Request request = busRequest.GetByID(requestId);
                    Request request = this.bRequest.GetByID(requestId);

                    //Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    Manager manager = flow.ManagerFlowList.SingleOrDefault().Manager;
                    IList<RequestStatus> RequestStatusList = request.RequestStatusList.ToList();
                    IList<RequestStatus> EndFlowRequestStatusList = RequestStatusList.Where(x => x.EndFlow).ToList<RequestStatus>();
                    RequestStatus status = RequestStatusList.Where(x => x.ManagerFlow.Level == RequestStatusList.Max(m => m.ManagerFlow.Level)).ToList().FirstOrDefault();
                    //if (status == null)
                    //{
                    //    decimal substitutePersonId = GetCurenttSubstitute();
                    //    if (substitutePersonId > 0)
                    //    {
                    //        decimal managerID = new BSubstitute().GetSubstituteManager(substitutePersonId);
                    //        status = RequestStatusList.Where(x => x.ManagerFlow.Manager.ID == managerID).ToList().FirstOrDefault();
                    //    }
                    //}
                    if (status == null && flow.IsForService)
                    {
                        //DNN Note:Improve Performance
                        //new BRequest().DeleteRequestByService(request);
                        this.bRequest.DeleteRequestByService(request);
                    }
                    else
                    {
                        //int managerFlowLevel = status.ManagerFlow.Level;
                        //int ManagerUpLevelCount = RequestStatusList.Count(r => r.ManagerFlow.Level > managerFlowLevel);
                        //if (ManagerUpLevelCount > 0)
                        //{
                        //    exception.Add(new ValidationException(ExceptionResourceKeys.RequestUsedByFlow, "این درخواست بدلیل استفاده در جریان کاری قابل حذف نیست", ExceptionSrc));
                        //    throw exception;
                        //}
                        foreach (RequestStatus endFlowRequestStatusItem in EndFlowRequestStatusList)
                        {
                            //DNN Note:Improve Performance
                            endFlowRequestStatusItem.EndFlow = false;
                            this.bRequest.SaveChanges(request, UIActionType.EDIT);
                        }
                    }


                    if (status != null)
                    {
                        status.IsDeleted = true;
                        status.EndFlow = true;
                        status.Date = DateTime.Now;
                        status.Description = managerDescription;
                        //DNN Note:Improve Performance
                        //busRequest.SaveChanges(request, UIActionType.EDIT);
                        this.bRequest.SaveChanges(request, UIActionType.EDIT);

                        new BPermit().DeleteByRequestId(requestId, request.Person.ID, request.FromDate);

                        ///ImperativeRequest Update : IsLocked = false
                        ChangeImperativeRequestLockState(request);
                    }
                    Permit permitObj = new BPermit().GetExistingPermit(request.ID);
                    if (permitObj != null)
                    {
                        new BPermit().DeleteByRequestId(request.ID, request.Person.ID, request.FromDate);
                    }
                }
                else
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                if (exception.Count > 0)
                {
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteRequstByService");
                throw ex;
            }
        }
        public void DeleteSpecialRequest(IList<KartableSetStatusProxy> requests, string managerDescription, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID, Caller caller)
        {
            try
            {
                requestValidationProxyList = new List<RequestKartablValidationProxy>();
                var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.ISpecialKartablRequestsKey);
                foreach (KartableSetStatusProxy req in requests)
                {
                    if (req.RequestID > 0 && !ids.Contains(req.RequestID))
                    {
                        throw new IllegalServiceAccess("XSS Attack on SetStatusOfRequest", ExceptionSrc);
                    }
                }


                managerDescription = Utility.IsEmpty(managerDescription) ? "" : managerDescription;
                EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
                EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
                //DNN Note:Improve Performance
                //BRequest requestBusiness = new BRequest();
                RequestKartablValidationProxy requestValidationProxy = null;
                var mngFlowIds = from req in requests
                                 group req by req.ManagerFlowID;

                foreach (var mngFlw in mngFlowIds)
                {
                    decimal mngFlwId = mngFlw.Key;
                    var list = from r in requests
                               where r.ManagerFlowID == mngFlwId
                               select r.RequestID;

                    foreach (decimal reqId in list)
                    {
                        using (NHibernateSessionManager.Instance.BeginTransactionOn())
                        {
                            try
                            {
                                ManagerFlow mf = mngFlwRep.GetById(mngFlwId, false);
                                //DNN Note:Improve Performance
                                //BRequest busRequest = new BRequest();
                                //Request request = busRequest.GetByID(reqId);

                                Request request = this.bRequest.GetByID(reqId);

                                IList<RequestStatus> endFlowRequestStatusList = new List<RequestStatus>();
                                requestValidationProxy = new RequestKartablValidationProxy();
                                requestValidationProxy.PrecardName = request.Precard.Name;
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                                    requestValidationProxy.Date = request.FromDate.ToShortDateString();
                                else
                                    requestValidationProxy.Date = Utility.ToPersianDate(request.FromDate);
                                requestValidationProxy.PersonName = request.Person.Name;
                                if (request.Parent != null)
                                {
                                    endFlowRequestStatusList = request.RequestStatusList.Where(x => x.EndFlow &&
                                                                                                    !x.IsDeleted
                                                                                                   && x.ManagerFlow.Active
                                                                                              )
                                                                                         .ToList<RequestStatus>();
                                }
                                else
                                {
                                    endFlowRequestStatusList = request.RequestStatusList.Where(x => x.EndFlow
                                                                                                    && x.ManagerFlow.Active
                                                                                              )
                                                                                         .ToList<RequestStatus>();
                                }
                                foreach (RequestStatus endFlowRequestStatusItem in endFlowRequestStatusList)
                                {
                                    endFlowRequestStatusItem.EndFlow = false;
                                    rsRep.WithoutTransactSave(endFlowRequestStatusItem);
                                }
                                bool PreEndFlow = request.EndFlow;
                                request.EndFlow = true;

                                //DNN Note:Improve Performance
                                //busRequest.SaveChanges(request, UIActionType.EDIT);
                                this.bRequest.SaveChanges(request, UIActionType.EDIT);

                                RequestStatus status = new RequestStatus();
                                status.ManagerFlow = new ManagerFlow() { ID = mf.ID };
                                status.Request = new Request() { ID = reqId };
                                status.IsDeleted = true;
                                status.EndFlow = true;
                                status.Date = DateTime.Now;
                                status.Description = managerDescription;
                                status.Applicator = new Person() { ID = applicatorID };
                                rsRep.WithoutTransactSave(status);

                                new BPermit().DeleteByRequestId(reqId, request.Person.ID, request.FromDate);

                                switch (caller)
                                {
                                    case Caller.Kartable:
                                        if (request.RequestChildList.Any() && PreEndFlow == true)
                                        {
                                            //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
                                            var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
                                            string description = string.Format("این درخواست به دلیل حذف درخواست اصلی توسط {0} حذف گردید", mf.Manager.Person != null ? mf.Manager.Person.Name : mf.Manager.OrganizationUnit.Person.Name);
                                            this.DeleteTerminateRequest(terminateRequestID, mngFlwId, description);
                                        }
                                        break;
                                    case Caller.SpecialKartable:
                                        if (request.RequestChildList.Any())
                                        {
                                            //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
                                            var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
                                            string description = string.Format("این درخواست به دلیل حذف درخواست اصلی توسط {0} حذف گردید", mf.Manager.Person != null ? mf.Manager.Person.Name : mf.Manager.OrganizationUnit.Person.Name);
                                            this.DeleteTerminateRequest(terminateRequestID, mngFlwId, description);
                                        }
                                        break;
                                }
                                //اگر درخواست لغو داشت برو درخواست لغو را هم حذف کن

                                if (request.Precard.Code == "141" && request.Parent != null && request.Parent.EndFlow == true)
                                {
                                    string description = string.Format("این درخواست به دلیل حذف درخواست لغو توسط {0} تایید گردید", mf.Manager.Person != null ? mf.Manager.Person.Name : mf.Manager.OrganizationUnit.Person.Name);
                                    this.ConfirmeParentOfTerminateRequest(request.Parent.ID, mngFlwId, description);
                                }

                                ///ImperativeRequest Update : IsLocked = false
                                ChangeImperativeRequestLockState(request);
                                NHibernateSessionManager.Instance.CommitTransactionOn();
                            }
                            catch (Exception ex)
                            {

                                NHibernateSessionManager.Instance.RollbackTransactionOn();
                                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteSpecialRequest");
                                requestValidationProxy.Exception = ex;
                                requestValidationProxyList.Add(requestValidationProxy);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                //NHibernateSessionManager.Instance.RollbackTransactionOn();
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteSpecialRequest");
                throw ex;
            }
        }

        protected void DeleteSpecialRequest(IList<KartableSetStatusProxy> requests, string managerDescription)
        {
            try
            {

                managerDescription = Utility.IsEmpty(managerDescription) ? "" : managerDescription;
                EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
                EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
                //DNN Note:Improve Performance
                //BRequest requestBusiness = new BRequest();
                RequestKartablValidationProxy requestValidationProxy = null;
                var mngFlowIds = from req in requests
                                 group req by req.ManagerFlowID;

                foreach (var mngFlw in mngFlowIds)
                {
                    decimal mngFlwId = mngFlw.Key;
                    var list = from r in requests
                               where r.ManagerFlowID == mngFlwId
                               select r.RequestID;

                    foreach (decimal reqId in list)
                    {
                        using (NHibernateSessionManager.Instance.BeginTransactionOn())
                        {
                            try
                            {
                                ManagerFlow mf = mngFlwRep.GetById(mngFlwId, false);
                                //DNN Note:Improve Performance
                                //BRequest busRequest = new BRequest();
                                //Request request = busRequest.GetByID(reqId);
                                Request request = this.bRequest.GetByID(reqId);

                                requestValidationProxy = new RequestKartablValidationProxy();
                                requestValidationProxy.PrecardName = request.Precard.Name;
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                                    requestValidationProxy.Date = request.FromDate.ToShortDateString();
                                else
                                    requestValidationProxy.Date = Utility.ToPersianDate(request.FromDate);
                                requestValidationProxy.PersonName = request.Person.Name;
                                IList<RequestStatus> endFlowRequestStatusList = request.RequestStatusList.Where(x => x.EndFlow
                                                                                                                   && x.ManagerFlow.Active
                                                                                                               )
                                                                                                         .ToList<RequestStatus>();
                                foreach (RequestStatus endFlowRequestStatusItem in endFlowRequestStatusList)
                                {
                                    endFlowRequestStatusItem.EndFlow = false;
                                    rsRep.WithoutTransactSave(endFlowRequestStatusItem);
                                }
                                bool PreEndFlow = request.EndFlow;
                                request.EndFlow = true;
                                //DNN Note:Improve Performance
                                //busRequest.SaveChanges(request, UIActionType.EDIT);
                                this.bRequest.SaveChanges(request, UIActionType.EDIT);

                                RequestStatus status = new RequestStatus();
                                status.ManagerFlow = new ManagerFlow() { ID = mf.ID };
                                status.Request = new Request() { ID = reqId };
                                status.IsDeleted = true;
                                status.EndFlow = true;
                                status.Date = DateTime.Now;
                                status.Description = managerDescription;
                                rsRep.WithoutTransactSave(status);

                                new BPermit().DeleteByRequestId(reqId, request.Person.ID, request.FromDate);

                                //اگر درخواست لغو داشت برو درخواست لغو را هم حذف کن
                                if (request.RequestChildList.Any() && PreEndFlow == false)
                                {
                                    //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
                                    var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
                                    string description = string.Format("این درخواست به دلیل حذف درخواست اصلی توسط {0} حذف گردید", mf.Manager.Person != null ? mf.Manager.Person.Name : mf.Manager.OrganizationUnit.Person.Name);
                                    this.DeleteTerminateRequest(terminateRequestID, mngFlwId, description);
                                }

                                ChangeImperativeRequestLockState(request);

                                NHibernateSessionManager.Instance.CommitTransactionOn();
                            }
                            catch (Exception ex)
                            {
                                NHibernateSessionManager.Instance.RollbackTransactionOn();
                                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteSpecialRequest");
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                //NHibernateSessionManager.Instance.RollbackTransactionOn();
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteSpecialRequest");
                throw ex;
            }
        }

        //protected void DeleteSpecialRequestByService(IList<KartableSetStatusProxy> requests, string managerDescription, out IList<RequestKartablValidationProxy> requestValidationProxyList)
        //{
        //    try
        //    {
        //        requestValidationProxyList = new List<RequestKartablValidationProxy>();

        //        managerDescription = Utility.IsEmpty(managerDescription) ? "" : managerDescription;
        //        EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
        //        EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
        //        BRequest requestBusiness = new BRequest();
        //        RequestKartablValidationProxy requestValidationProxy = null;
        //        var mngFlowIds = from req in requests
        //                         group req by req.ManagerFlowID;

        //        foreach (var mngFlw in mngFlowIds)
        //        {
        //            decimal mngFlwId = mngFlw.Key;
        //            var list = from r in requests
        //                       where r.ManagerFlowID == mngFlwId
        //                       select r.RequestID;

        //            foreach (decimal reqId in list)
        //            {
        //                using (NHibernateSessionManager.Instance.BeginTransactionOn())
        //                {
        //                    try
        //                    {


        //                        ManagerFlow mf = mngFlwRep.GetById(mngFlwId, false);
        //                        BRequest busRequest = new BRequest();
        //                        Request request = busRequest.GetByID(reqId);
        //                        requestValidationProxy = new RequestKartablValidationProxy();
        //                        requestValidationProxy.PrecardName = request.Precard.Name;
        //                        if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
        //                            requestValidationProxy.Date = request.FromDate.ToShortDateString();
        //                        else
        //                            requestValidationProxy.Date = Utility.ToPersianDate(request.FromDate);
        //                        requestValidationProxy.PersonName = request.Person.Name;
        //                        IList<RequestStatus> endFlowRequestStatusList = request.RequestStatusList.Where(x => x.ManagerFlow.Active &&
        //                                                                                                             x.EndFlow)
        //                                                                                                 .ToList<RequestStatus>();
        //                        foreach (RequestStatus endFlowRequestStatusItem in endFlowRequestStatusList)
        //                        {
        //                            endFlowRequestStatusItem.EndFlow = false;
        //                            rsRep.WithoutTransactSave(endFlowRequestStatusItem);
        //                        }

        //                        request.EndFlow = true;
        //                        busRequest.SaveChanges(request, UIActionType.EDIT);

        //                        RequestStatus status = new RequestStatus();
        //                        status.ManagerFlow = new ManagerFlow() { ID = mf.ID };
        //                        status.Request = new Request() { ID = reqId };
        //                        status.IsDeleted = true;
        //                        status.EndFlow = true;
        //                        status.Date = DateTime.Now;
        //                        status.Description = managerDescription;
        //                        rsRep.WithoutTransactSave(status);

        //                        new BPermit().DeleteByRequestId(reqId, request.Person.ID, request.FromDate);

        //                        //اگر درخواست لغو داشت برو درخواست لغو را هم حذف کن
        //                        if (request.RequestChildList.Any())
        //                        {
        //                            //قاعدتا یک درخواست همیشه می تواند فقط یک درخواست لغو در صورت وجود داشته باشد
        //                            var terminateRequestID = request.RequestChildList.FirstOrDefault().ID;
        //                            string description = string.Format("این درخواست به دلیل حذف درخواست اصلی توسط {0} حذف گردید", mf.Manager.Person.Name);
        //                            DeleteTerminateRequest(terminateRequestID, mngFlwId, description);
        //                        }

        //                        ///ImperativeRequest Update : IsLocked = false
        //                        ChangeImperativeRequestLockState(request);

        //                        NHibernateSessionManager.Instance.CommitTransactionOn();
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                        NHibernateSessionManager.Instance.RollbackTransactionOn();
        //                        BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteSpecialRequest");
        //                        requestValidationProxy.Exception = ex;
        //                        requestValidationProxyList.Add(requestValidationProxy);
        //                    }
        //                }
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        //NHibernateSessionManager.Instance.RollbackTransactionOn();
        //        BaseBusiness<Entity>.LogException(ex, "BKartabl", "DeleteSpecialRequest");
        //        throw ex;
        //    }
        //}

        protected void DeleteTerminateRequest(decimal RequestID, decimal ManagerFlowID, string Description)
        {
            IList<decimal> ids = new List<decimal>();
            ids.Add(RequestID);
            SessionHelper.SaveSessionValue(SessionHelper.IReviewedRequestsKey, ids);

            IList<KartableSetStatusProxy> KartableSetStatusProxyList = new List<KartableSetStatusProxy>();
            KartableSetStatusProxyList.Add(new KartableSetStatusProxy()
            {
                RequestID = RequestID,
                ManagerFlowID = ManagerFlowID
            });
            this.DeleteSpecialRequest(KartableSetStatusProxyList, Description);
        }

        protected bool SetStatusOfParentRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList)
        {
            try
            {
                requestValidationProxyList = new List<RequestKartablValidationProxy>();
                UIValidationExceptions exception = new UIValidationExceptions();

                if (status != RequestState.Confirmed && status != RequestState.Unconfirmed)
                    return false;

                try
                {
                    description = Utility.IsEmpty(description) ? "" : description;
                    bool endFlow = false;
                    bool confirm = false;
                    EntityRepository<RequestStatus> rsRep = new EntityRepository<RequestStatus>(false);
                    EntityRepository<ManagerFlow> mngFlwRep = new EntityRepository<ManagerFlow>(false);
                    //DNN Note:Improve Performance
                    //BRequest requestBusiness = new BRequest();

                    RequestKartablValidationProxy requestValidationProxy = null;

                    var mngFlowIds = from req in requests
                                     group req by req.ManagerFlowID;

                    foreach (var mngFlw in mngFlowIds)
                    {
                        decimal mngFlwId = mngFlw.Key;
                        var list = from r in requests
                                   where r.ManagerFlowID == mngFlwId
                                   select r.RequestID;
                        if (status == RequestState.Confirmed)
                        {
                            endFlow = true;
                            confirm = true;
                        }
                        else if (status == RequestState.Unconfirmed)
                        {
                            endFlow = true;
                            confirm = false;
                        }
                        foreach (decimal reqId in list)
                        {
                            using (NHibernateSessionManager.Instance.BeginTransactionOn())
                            {
                                try
                                {
                                    RequestStatus rs = new RequestStatus();
                                    rs.ManagerFlow = new ManagerFlow() { ID = mngFlwId };
                                    rs.Request = new Request() { ID = reqId };
                                    rs.Confirm = confirm;
                                    rs.EndFlow = endFlow;
                                    rs.IsDeleted = false;
                                    rs.Description = description;
                                    rs.Date = DateTime.Now;

                                    //DNN Note:Improve Performance
                                    //Request request = new BRequest().GetByID(reqId);
                                    Request request = this.bRequest.GetByID(reqId);

                                    requestValidationProxy = new RequestKartablValidationProxy();
                                    requestValidationProxy.PrecardName = request.Precard.Name;
                                    if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                                        requestValidationProxy.Date = request.FromDate.ToShortDateString();
                                    else
                                        requestValidationProxy.Date = Utility.ToPersianDate(request.FromDate);
                                    requestValidationProxy.PersonName = request.Person.Name;
                                    IList<RequestStatus> endFlowRequestStatus = request.RequestStatusList.Where(x => x.ManagerFlow.Active &&
                                                                                                                     x.EndFlow)
                                                                                                         .ToList<RequestStatus>();
                                    foreach (RequestStatus endFlowRS in endFlowRequestStatus)
                                    {
                                        endFlowRS.EndFlow = false;
                                        rsRep.WithoutTransactSave(endFlowRS);
                                    }
                                    this.SavePermit(request);

                                    request.EndFlow = endFlow;
                                    //DNN Note:Improve Performance
                                    this.bRequest.SaveChanges(request, UIActionType.EDIT);

                                    rsRep.WithoutTransactSave(rs);
                                    ///ImperativeRequest Update : IsLocked = false && Delete all Permits for this request

                                    if (!confirm)
                                    {
                                        new BPermit().DeleteByRequestId(request.ID, request.Person.ID, request.FromDate);

                                        ChangeImperativeRequestLockState(request);
                                    }
                                    NHibernateSessionManager.Instance.CommitTransactionOn();
                                }
                                catch (Exception ex)
                                {
                                    NHibernateSessionManager.Instance.RollbackTransactionOn();
                                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfSpecialRequest");
                                    requestValidationProxy.Exception = ex;
                                    requestValidationProxyList.Add(requestValidationProxy);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //NHibernateSessionManager.Instance.RollbackTransactionOn();
                    BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfSpecialRequest");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfSpecialRequest");
                throw ex;
            }
            return true;
        }

        protected void ConfirmeParentOfTerminateRequest(decimal RequestID, decimal ManagerFlowID, string Description)
        {
            IList<decimal> ids = new List<decimal>();
            ids.Add(RequestID);
            SessionHelper.SaveSessionValue(SessionHelper.IReviewedRequestsKey, ids);

            IList<KartableSetStatusProxy> KartableSetStatusProxyList = new List<KartableSetStatusProxy>();
            KartableSetStatusProxyList.Add(new KartableSetStatusProxy()
            {
                RequestID = RequestID,
                ManagerFlowID = ManagerFlowID
            });
            IList<RequestKartablValidationProxy> requestValidationProxyList = new List<RequestKartablValidationProxy>();
            bool result = this.SetStatusOfParentRequest(KartableSetStatusProxyList, RequestState.Confirmed, Description, out requestValidationProxyList);
        }

        protected void DeleteParentOfTerminateRequest(decimal RequestID, decimal ManagerFlowID, string Description)
        {
            IList<decimal> ids = new List<decimal>();
            ids.Add(RequestID);
            SessionHelper.SaveSessionValue(SessionHelper.IReviewedRequestsKey, ids);

            IList<KartableSetStatusProxy> KartableSetStatusProxyList = new List<KartableSetStatusProxy>();
            KartableSetStatusProxyList.Add(new KartableSetStatusProxy()
            {
                RequestID = RequestID,
                ManagerFlowID = ManagerFlowID
            });
            IList<RequestKartablValidationProxy> requestValidationProxyList = new List<RequestKartablValidationProxy>();
            this.DeleteParentRequst(KartableSetStatusProxyList, Description);
            //bool result = this.SetStatusOfParentRequest(KartableSetStatusProxyList, RequestState.Deleted, Description, out requestValidationProxyList);
        }
        #endregion

        /// <summary>
        /// تعداد درخواستها را با اعمال شرایط برمیگرداند
        /// اگر شخص مدیر نباشد خالی برمیگردد
        /// جهت استفاده در خلاصه وضعیت
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetManagerKartablRequestCount(decimal userID, int year)
        {
            try
            {
                User user = null;
                if (this.workingUsername == string.Empty || this.workingPersonId == 0)
                {
                    BUser bUser = new BUser();
                    user = bUser.GetByID(userID);
                    NHibernateSessionManager.Instance.GetSession().Evict(user);
                }
                if (user != null)
                {
                    if (this.workingUsername == string.Empty)
                        this.workingUsername = user.UserName;
                    if (this.workingPersonId == 0)
                        this.workingPersonId = user.Person.ID;
                }


                int fromMonth = 1;
                int toMonth = 12;
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, fromMonth, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                        fromDate = new DateTime(year, fromMonth, 1);
                        toDate = new DateTime(year, toMonth, endOfMonth);
                    }
                    //نمایش آیتم سالهای پیش
                    fromDate = fromDate.AddYears(-1);

                    //مدیر
                    Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    //int count = 0;
                    if (manager.ID > 0)
                    {
                        int result = bManagerKartablUnderManagment.GetRequestCount(manager.ID, RequestType.None, fromDate, toDate);
                        //IList<InfoRequest> requests = bManagerKartablUnderManagment.GetAllRequests(manager.ID, fromDate, toDate, out count);

                        //int result = requests.Count;

                        //if (requests.Count > 0)
                        //{
                        //    IList<InfoRequest> requestsTemp = requests.OrderBy(o => o.ToDate).ToList();
                        //    lastRequestDate = requestsTemp.LastOrDefault().ToDate;
                        //}
                        //else
                        //{
                        //    lastRequestDate = new DateTime();
                        //}
                        return result;
                    }
                    else
                    {
                        // lastRequestDate = new DateTime();
                        return -1;
                    }
                }
                else
                {
                    // lastRequestDate = new DateTime();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetManagerKartablRequestCount");
                throw ex;
            }

        }

        public IList<InfoRequest> GetManagerKartablRequest(decimal managerId, int year)
        {
            try
            {
                int count = 0;
                int fromMonth = 1;
                int toMonth = 12;
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, fromMonth, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                        fromDate = new DateTime(year, fromMonth, 1);
                        toDate = new DateTime(year, toMonth, endOfMonth);
                    }
                    //نمایش آیتم سالهای پیش
                    fromDate = fromDate.AddYears(-1);

                    //مدیر
                    //Manager manager = new BManager().GetManagerByUsername(this.workingUsername);
                    IList<InfoRequest> requests;
                    if (managerId > 0)
                    {

                        requests = bManagerKartablUnderManagment.GetAllRequests(managerId, fromDate, toDate, out count);




                        return requests;
                    }
                    else
                    {
                        return new List<InfoRequest>();
                    }
                }
                else
                {
                    return new List<InfoRequest>();
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetManagerKartablRequestCount");
                throw ex;
            }

        }

        /// <summary>
        /// تعداد درخواستها را با اعمال شرایط برمیگرداند
        /// اگر شخص مدیر نباشد خالی برمیگردد
        /// جهت استفاده در خلاصه وضعیت
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetSubstituteKartablRequestCount(decimal userID, int year)
        {
            try
            {
                if (this.workingPersonId == 0)
                {
                    BUser bUser = new BUser();
                    User user = bUser.GetByID(userID);
                    this.workingPersonId = user.Person.ID;
                    NHibernateSessionManager.Instance.GetSession().Evict(user);
                }

                int fromMonth = 1;
                int toMonth = 12;
                if (GetCurentPersonId() > 0)
                {
                    DateTime fromDate, toDate;
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, toMonth);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, fromMonth, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, toMonth, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, toMonth);
                        fromDate = new DateTime(year, fromMonth, 1);
                        toDate = new DateTime(year, toMonth, endOfMonth);
                    }

                    //نمایش آیتم سالهای پیش
                    fromDate = fromDate.AddYears(-1);

                    //جانشین
                    decimal substitutePersonId = GetCurenttSubstitute();
                    if (substitutePersonId > 0)
                    {
                        SubstituteRepository rep = new SubstituteRepository(false);
                        int result = bSubstituteKartablUnderManagment.GetRequestCount(substitutePersonId, fromDate, toDate);

                        return result;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetSubstituteKartablRequestCount");
                throw ex;
            }

        }

        /// <summary>
        /// کاربر فعلی 
        /// </summary>
        /// <returns></returns>
        private decimal GetCurentPersonId()
        {
            if (workingPersonId == 0)
            {
                Model.Security.User user = BUser.CurrentUser;
                if (user != null)
                {
                    this.workingPersonId = user.Person.ID;
                    this.workingUsername = user.UserName;
                }
            }
            return workingPersonId;
        }

        /// <summary>
        /// کاربر فعلی 
        /// </summary>
        /// <returns></returns>
        private decimal GetCurentUserId()
        {
            if (workingUserId == 0)
            {
                Model.Security.User user = BUser.CurrentUser;
                if (user != null)
                {
                    this.workingUserId = user.ID;
                    this.workingUsername = user.UserName;
                }
            }
            return workingUserId;
        }

        /// <summary>
        /// آیا کاربر فعلی جانشین است
        /// </summary>
        /// <returns></returns>
        private decimal GetCurenttSubstitute()
        {
            SubstituteRepository rep = new SubstituteRepository(false);
            if (rep.IsSubstitute(this.workingPersonId))
                return this.workingPersonId;
            else
                return 0;
        }

        /// <summary>
        /// آیا کاربر فعلی جانشین است
        /// </summary>
        /// <returns></returns>
        public decimal GetCurenttSubstitute(decimal personId)
        {
            SubstituteRepository rep = new SubstituteRepository(false);
            if (rep.IsSubstitute(personId))
                return this.workingPersonId;
            else
                return 0;
        }

        /// <summary>
        /// ثبت درخواست بعنوان مجوز
        /// اگر درخواست اضافه کار و یا ساعتی باشد و مجوزی با همان پیشکارت و تاریخ موجود باشد به همان زوج مرتب اضافه میکند
        /// </summary>
        /// <param name="request"></param>
        private Permit SavePermit(Request request)
        {
            try
            {
                BPermit busPermit = new BPermit();
                Permit permit = new Permit();
                PermitPair permitPair = new PermitPair();
                Precard precard = new PrecardRepository().GetById(request.Precard.ID, false);

                if (precard.PrecardGroup == null)
                {
                    throw new InvalidDatabaseStateException(UIFatalExceptionIdentifiers.PrecardGroupIsNull, "گروه پیشکارت دستوری خالی است", ExceptionSrc);
                }
                string name = precard.PrecardGroup.LookupKey;
                PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
                IList<Permit> list = busPermit.GetExistingPermit(request.Person.ID, request.Precard.ID, request.FromDate, request.ToDate);
                if (list.Count > 0 && (groupName == PrecardGroupsName.overwork || request.Precard.IsHourly))
                {
                    permit = list.First();
                    if (permit.Pairs == null)
                        permit.Pairs = new List<PermitPair>();
                    if (permit.Pairs.Where(x => x.RequestID == request.ID).Count() > 0)
                        return null;
                    permitPair.Permit = permit;
                    permitPair.RequestID = request.ID;
                    permitPair.From = request.FromTime;
                    permitPair.To = request.ToTime;
                    permitPair.Value = request.TimeDuration;
                    permitPair.PreCardID = request.Precard.ID;
                    permitPair.IsFilled = true;
                    permit.Pairs.Add(permitPair);

                    BaseBusiness<Entity>.LogUserAction(String.Format("Add Permit Pair:Request Id:{0},Request Date:{1}", request.ID, Utility.ToString(request.FromDate)));
                    busPermit.SaveChanges(permit, UIActionType.EDIT);


                }
                else
                {
                    if (groupName == PrecardGroupsName.overwork)
                    {
                        permit.IsPairly = false;
                        if (request.ToTime - request.FromTime == request.TimeDuration)
                        {
                            permit.IsPairly = true;
                        }
                    }
                    else if (request.Precard.IsHourly)
                    {
                        permit.IsPairly = true;
                    }
                    else
                    {
                        permit.IsPairly = false;
                    }

                    permit.FromDate = request.FromDate;
                    permit.ToDate = request.ToDate;
                    permit.Pairs = new List<PermitPair>() { permitPair };
                    permit.Person = request.Person;

                    permitPair.Permit = permit;
                    permitPair.RequestID = request.ID;
                    permitPair.From = request.FromTime;
                    permitPair.To = request.ToTime;
                    permitPair.Value = request.TimeDuration;
                    permitPair.PreCardID = request.Precard.ID;
                    permitPair.IsFilled = true;

                    BaseBusiness<Entity>.LogUserAction(String.Format("Add Permit :Request Id:{0},Request Date:{1}", request.ID, Utility.ToString(request.FromDate)));
                    busPermit.SaveChanges(permit, UIActionType.ADD);
                }
                return permit;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// جهت اعمال دسترسی در واسط کاربر
        /// آیا کاربر فعلی اپراتور است
        /// بعلت مشکل در تست کردن دوبار نوشته شده است
        /// </summary>
        public bool IsCurrentUserOperator
        {
            get
            {
                if (SessionHelper.HasSessionValue(SessionHelper.GTSCurrentUserManagmentState))
                {
                    Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);
                    if (Utility.ToBoolean(managementState["IsOperator"]))
                        return true;
                    else return false;
                }
                else
                {
                    throw new Exception("مقدار مورد انتظار در سشن موجود نمیباشد - 2001");
                }
                //BOperator op = new BOperator();
                //IList<Operator> opList = op.GetOperator(this.GetCurentPersonId());
                //return opList.Count > 0 ? true : false;
            }
        }

        public void GetAllKartablData(DateTime fromDate, DateTime toDate, RequestType requestType, decimal managerId, int pageIndex, int pageSize, KartablOrderBy orderby)
        {
            this.requestRep.GetAllKartablData(fromDate, toDate, RequestType.None, managerId, pageIndex, pageSize, orderby);
        }

        private KartablProxy ConvertKartablRequestToProxy(InfoRequest req, decimal managerId)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                proxy.ThePureFromDate = Utility.ToPersianDate(req.FromDate);
                proxy.ThePureToDate = Utility.ToPersianDate(req.ToDate);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                proxy.ThePureFromDate = Utility.ToString(req.FromDate);
                proxy.ThePureToDate = Utility.ToString(req.ToDate);
            }
            string registerRequestTime = " - " + Utility.IntTimeToRealTime((req.RegisterDate.Hour * 60) + req.RegisterDate.Minute); ;
            proxy.RegistrationDate += registerRequestTime;
            proxy.ID = req.ID;
            proxy.ParentID = req.ParentID;
            proxy.ChildsCount = req.ChildsCount;
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = req.mngrFlowID;

            proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);

            if (!req.IsMonthly && !req.IsDaily)
                proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
            else
                if (req.IsMonthly && (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), req.LookupKey) == PrecardGroupsName.imperative)
                    proxy.TheDuration = req.TimeDuration.ToString();
                else
                    if (req.IsDaily)
                        proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";

            proxy.RequestTitle = req.PrecardName;
            proxy.Description = req.Description;
            proxy.Applicant = req.ApplicantFirstName + " " + req.ApplicantLastName;
            if (Utility.IsEmpty(proxy.Applicant))
            {
                proxy.Applicant = req.Applicant;
            }
            proxy.Barcode = req.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            if (req.ManagerID == managerId)
                proxy.RequestSource = RequestSource.Undermanagment;
            else
                proxy.RequestSource = RequestSource.Substitute;
            proxy.PersonId = req.PersonID;
            string name = req.LookupKey;
            PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
            if (groupName == PrecardGroupsName.overwork)
            {
                proxy.RequestType = RequestType.OverWork;

                //تنظیم زمان ابتدا و انتها
                //درخواست بازه ای بدون انتدا و انتها
                if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else if (groupName == PrecardGroupsName.imperative)
            {
                proxy.RequestType = RequestType.Imperative;
            }
            else
            {
                if (req.IsHourly)
                {
                    proxy.RequestType = RequestType.Hourly;
                }
                else if (req.IsDaily)
                {
                    proxy.RequestType = RequestType.Daily;
                }
                else if (req.IsMonthly)
                {
                    proxy.RequestType = RequestType.Monthly;
                }
                else
                {
                    proxy.RequestType = RequestType.None;
                }
            }
            proxy.AttachmentFile = req.AttachmentFile;
            proxy.PersonImage = req.PersonImage;
            proxy.IsEdited = req.IsEdited;
            if (req.RequestSubstituteID != 0)
                proxy.RequestSubstituteID = req.RequestSubstituteID;
            else
                proxy.RequestSubstituteID = 0;
            proxy.DepartmentId = req.DepartmentId;
            proxy.DepartmentName = req.DepartmentName;
            return proxy;
        }

        private KartablProxy ConvertKartablRequestToProxy(InfoRequest req, decimal managerId, IList<Request> RequestParentList)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                proxy.ThePureFromDate = Utility.ToPersianDate(req.FromDate);
                proxy.ThePureToDate = Utility.ToPersianDate(req.ToDate);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                proxy.ThePureFromDate = Utility.ToString(req.FromDate);
                proxy.ThePureToDate = Utility.ToString(req.ToDate);
            }
            string registerRequestTime = " - " + Utility.IntTimeToRealTime((req.RegisterDate.Hour * 60) + req.RegisterDate.Minute); ;
            proxy.RegistrationDate += registerRequestTime;
            proxy.ID = req.ID;
            proxy.ParentID = req.ParentID;
            proxy.ChildsCount = req.ChildsCount;
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = req.mngrFlowID;

            proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);

            //اگر درخواست ساعتی باشد یا از نوع لغو باشد و روی درخواست ساعتی زده شده باشد
            if ((!req.IsMonthly && !req.IsDaily) || (req.ParentID != 0 && req.TimeDuration != -1000 && req.FromTime != -1000 && req.ToTime != -1000))
                proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
            else
                if (req.IsMonthly && (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), req.LookupKey) == PrecardGroupsName.imperative)
                    proxy.TheDuration = req.TimeDuration.ToString();
                else
                    if (req.IsDaily && req.FromTime == -1000 && req.ToTime == -1000)
                        proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            proxy.RequestTitle = req.PrecardName;
            proxy.Description = req.Description;
            proxy.Applicant = req.ApplicantFirstName + " " + req.ApplicantLastName;
            if (Utility.IsEmpty(proxy.Applicant))
            {
                proxy.Applicant = req.Applicant;
            }
            proxy.Barcode = req.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            if (req.ManagerID == managerId)
                proxy.RequestSource = RequestSource.Undermanagment;
            else
                proxy.RequestSource = RequestSource.Substitute;
            proxy.PersonId = req.PersonID;
            string name = req.LookupKey;
            PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
            if (groupName == PrecardGroupsName.overwork)
            {
                proxy.RequestType = RequestType.OverWork;

                //تنظیم زمان ابتدا و انتها
                //درخواست بازه ای بدون انتدا و انتها
                if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else if (groupName == PrecardGroupsName.imperative)
            {
                proxy.RequestType = RequestType.Imperative;
            }
            else
            {
                if (req.ParentID != 0)
                {
                    Request request = RequestParentList.Where(x => x.ID == req.ParentID).FirstOrDefault();
                    if (request != null && request.Precard.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (request != null && request.Precard.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (request != null && request.Precard.IsMonthly)
                    {
                        proxy.RequestType = RequestType.Monthly;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }

                }
                else
                {
                    if (req.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (req.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (req.IsMonthly)
                    {
                        proxy.RequestType = RequestType.Monthly;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }
                }
            }
            proxy.AttachmentFile = req.AttachmentFile;
            proxy.PersonImage = req.PersonImage;
            proxy.IsEdited = req.IsEdited;
            if (req.RequestSubstituteID != 0)
                proxy.RequestSubstituteID = req.RequestSubstituteID;
            else
                proxy.RequestSubstituteID = 0;
            proxy.DepartmentId = req.DepartmentId;
            proxy.DepartmentName = req.DepartmentName;
            return proxy;
        }
        private KartablProxy ConvertRegisterRequestToProxy(InfoRequest req, IList<Request> RequestParentList)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {


                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                proxy.ThePureFromDate = Utility.ToPersianDate(req.ToDate);
                proxy.ThePureToDate = Utility.ToPersianDate(req.ToDate);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                proxy.ThePureFromDate = Utility.ToString(req.FromDate);
                proxy.ThePureToDate = Utility.ToString(req.ToDate);
            }
            string registerRequestTime = " - " + Utility.IntTimeToRealTime((req.RegisterDate.Hour * 60) + req.RegisterDate.Minute); ;
            proxy.RegistrationDate += registerRequestTime;
            proxy.ID = req.ID;
            proxy.ParentID = req.ParentID;
            proxy.ChildsCount = req.ChildsCount;
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = req.mngrFlowID;

            proxy.TheFromTime = req.FromTime == 0 ? "" : Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
            if (req.ParentID == 0)
            {
                if (!req.IsMonthly && !req.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                else if (req.IsDaily)
                    proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            }
            else
            {
                //اگر درخواست از نوع درخواست لغو باشد باید نوع پیشکارت را از درخواست پدر چک کند
                Request parent = bRequest.GetRequestByID(req.ParentID);
                if (!parent.Precard.IsMonthly && !parent.Precard.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                else if (parent.Precard.IsDaily)
                    proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            }

            proxy.RequestTitle = req.PrecardName;
            proxy.Description = req.Description;
            proxy.ManagerDescription = req.ManagerDescription;
            proxy.Applicant = req.Applicant;
            if (Utility.IsEmpty(proxy.Applicant))
            {
                proxy.Applicant = req.Applicant;
            }
            proxy.Barcode = req.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            proxy.RequestSource = RequestSource.Undermanagment;
            proxy.PersonId = req.PersonID;
            if (req.Confirm == null)
            {
                if (!this.permitBusiness.CheckPermitPairExistanceByRequest(req.ID))
                {
                    if (req.RequestSubstituteConfirm == null)
                        proxy.FlowStatus = RequestState.UnderReview;
                    else
                    {
                        if ((bool)req.RequestSubstituteConfirm)
                            proxy.FlowStatus = RequestState.UnderReview;
                        else
                            proxy.FlowStatus = RequestState.Unconfirmed;
                    }
                }
                else
                {
                    if (req.EndFlow)
                        proxy.FlowStatus = RequestState.Confirmed;
                    else
                        proxy.FlowStatus = RequestState.Unconfirmed;
                }
            }
            else if (req.IsDeleted != null && (bool)req.IsDeleted)
            {
                proxy.FlowStatus = RequestState.Deleted;
            }
            else if ((bool)req.Confirm)
            {
                proxy.FlowStatus = RequestState.Confirmed;
            }
            else
            {
                proxy.FlowStatus = RequestState.Unconfirmed;
            }

            if (req.LookupKey.Equals(RequestType.OverWork.ToString().ToLower()))
            {
                proxy.RequestType = RequestType.OverWork;

                //تنظیم زمان ابتدا و انتها
                //درخواست بازه ای بدون انتدا و انتها
                if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else
            {
                if (req.ParentID != 0)
                {
                    Request request = RequestParentList.Where(x => x.ID == req.ParentID).FirstOrDefault();
                    if (request != null && request.Precard.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (request != null && request.Precard.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }
                }
                else
                {
                    if (req.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (req.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                }

            }
            proxy.AttachmentFile = req.AttachmentFile;
            proxy.IsEdited = req.IsEdited;
            proxy.RequestSubstituteID = req.RequestSubstituteID;
            proxy.RequestSubstituteConfirm = req.RequestSubstituteConfirm;
            return proxy;
        }

        private KartablProxy ConvertReviewdRequestToProxy(InfoRequest req, IList<Request> RequestParentList)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                proxy.ThePureFromDate = Utility.ToPersianDate(req.FromDate);
                proxy.ThePureToDate = Utility.ToPersianDate(req.ToDate);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                proxy.ThePureFromDate = Utility.ToString(req.FromDate);
                proxy.ThePureToDate = Utility.ToString(req.ToDate);
            }
            string registerRequestTime = " - " + Utility.IntTimeToRealTime((req.RegisterDate.Hour * 60) + req.RegisterDate.Minute); ;
            proxy.RegistrationDate += registerRequestTime;
            proxy.ID = req.ID;
            proxy.ParentID = req.ParentID;
            proxy.ChildsCount = req.ChildsCount;
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = req.mngrFlowID;


            proxy.TheFromTime = req.FromTime == 0 ? "" : Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);

            if (req.ParentID == 0)
            {
                if (!req.IsMonthly && !req.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                else
                    if (req.IsMonthly && (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), req.LookupKey) == PrecardGroupsName.imperative)
                        proxy.TheDuration = req.TimeDuration.ToString();
                    else
                        if (req.IsDaily)
                            proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            }
            else
            {
                //اگر درخواست از نوع درخواست لغو باشد باید نوع پیشکارت را از درخواست پدر چک کند
                Request parent = bRequest.GetRequestByID(req.ParentID);
                if (!parent.Precard.IsMonthly && !parent.Precard.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                else
                    if (parent.Precard.IsMonthly && (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), req.LookupKey) == PrecardGroupsName.imperative)
                        proxy.TheDuration = parent.TimeDuration.ToString();
                    else
                        if (parent.Precard.IsDaily)
                            proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            }

            proxy.RequestTitle = req.PrecardName;
            proxy.Description = req.Description;
            proxy.Applicant = req.Applicant;
            proxy.Barcode = req.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            proxy.RequestSource = RequestSource.Undermanagment;
            proxy.PersonId = req.PersonID;
            if (req.IsDeleted != null && (bool)req.IsDeleted)
            {
                proxy.FlowStatus = RequestState.Deleted;
            }
            else if (req.Confirm == null)
            {
                proxy.FlowStatus = RequestState.UnderReview;
            }
            else if (req.Confirm != null && (bool)req.Confirm)
            {
                proxy.FlowStatus = RequestState.Confirmed;
            }
            else if (req.Confirm != null)
            {
                proxy.FlowStatus = RequestState.Unconfirmed;
            }

            if (req.LookupKey.Equals(RequestType.OverWork.ToString().ToLower()))
            {
                proxy.RequestType = RequestType.OverWork;
            }
            else if (req.LookupKey.Equals(RequestType.Imperative.ToString().ToLower()))
            {
                proxy.RequestType = RequestType.Imperative;
            }
            else
            {
                if (req.ParentID != 0)
                {
                    Request request = RequestParentList.Where(x => x.ID == req.ParentID).FirstOrDefault();
                    if (request != null && request.Precard.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (request != null && request.Precard.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (request != null && request.Precard.IsMonthly)
                    {
                        proxy.RequestType = RequestType.Monthly;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }
                }
                else
                {
                    if (req.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (req.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (req.IsMonthly)
                    {
                        proxy.RequestType = RequestType.Monthly;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }
                }
            }

            proxy.AttachmentFile = req.AttachmentFile;
            proxy.PersonImage = req.PersonImage;
            proxy.IsEdited = req.IsEdited;
            proxy.DepartmentId = req.DepartmentId;
            proxy.DepartmentName = req.DepartmentName;
            return proxy;
        }

        public KartablProxy ConvertRegisterRequestToProxy(Request req)
        {
            KartablProxy proxy = new KartablProxy();
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                proxy.ThePureFromDate = Utility.ToPersianDate(req.ToDate);
                proxy.ThePureToDate = Utility.ToPersianDate(req.ToDate);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                proxy.ThePureFromDate = Utility.ToString(req.FromDate);
                proxy.ThePureToDate = Utility.ToString(req.ToDate);
            }
            string registerRequestTime = " - " + Utility.IntTimeToRealTime((req.RegisterDate.Hour * 60) + req.RegisterDate.Minute); ;
            proxy.RegistrationDate += registerRequestTime;
            proxy.ID = req.ID;
            //proxy.ParentID = req.Parent.ID;
            //proxy.ChildsCount = req.ChildsCount;
            proxy.RequestID = req.ID;
            //proxy.ManagerFlowID = req.mngrFlowID;

            if (req.Parent == null)
            {
                proxy.TheFromTime = req.FromTime == 0 ? "" : Utility.IntTimeToRealTime(req.FromTime);
                proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);
                if (!req.Precard.IsMonthly && !req.Precard.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                else
                    if (req.Precard.IsDaily)
                        proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            }
            else
            {
                //اگر درخواست از نوع درخواست لغو باشد باید نوع پیشکارت را از درخواست پدر چک کند
                Request parent = bRequest.GetRequestByID(req.Parent.ID);
                proxy.TheFromTime = parent.FromTime == 0 ? "" : Utility.IntTimeToRealTime(parent.FromTime);
                proxy.TheToTime = Utility.IntTimeToRealTime(parent.ToTime);
                if (!parent.Precard.IsMonthly && !parent.Precard.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(parent.TimeDuration);
                else
                    if (parent.Precard.IsDaily)
                        proxy.TheDuration = parent.TimeDuration != -1000 ? parent.TimeDuration.ToString() : parent.FromDate > DateTime.MinValue && parent.ToDate > DateTime.MinValue && parent.ToDate >= parent.FromDate ? (parent.ToDate.Subtract(parent.FromDate).Days + 1).ToString() : "0";
            }

            proxy.RequestTitle = req.Precard.Name;
            proxy.Description = req.Description;
            proxy.Applicant = req.Person.Name;
            //proxy.ManagerDescription = req.ManagerDescription;
            //proxy.Applicant = req.Applicant;
            //if (Utility.IsEmpty(proxy.Applicant))
            //{
            //    proxy.Applicant = req.Applicant;
            //}
            proxy.Barcode = req.Person.PersonCode;
            proxy.OperatorUser = req.OperatorUser;
            proxy.RequestSource = RequestSource.Undermanagment;
            proxy.PersonId = req.Person.ID;
            //if (req.Confirm == null)
            //{
            //    if (!this.permitBusiness.CheckPermitPairExistanceByRequest(req.ID))
            //        proxy.FlowStatus = RequestState.UnderReview;
            //    else
            //    {
            //        if (req.EndFlow)
            //            proxy.FlowStatus = RequestState.Confirmed;
            //        else
            //            proxy.FlowStatus = RequestState.Unconfirmed;
            //    }
            //}
            //else if (req.IsDeleted != null && (bool)req.IsDeleted)
            //{
            //    proxy.FlowStatus = RequestState.Deleted;
            //}
            //else if ((bool)req.Confirm)
            //{
            //    proxy.FlowStatus = RequestState.Confirmed;
            //}
            //else
            //{
            //    proxy.FlowStatus = RequestState.Unconfirmed;
            //}

            if (req.Precard.PrecardGroup.LookupKey.Equals(RequestType.OverWork.ToString().ToLower()))
            {
                proxy.RequestType = RequestType.OverWork;

                //تنظیم زمان ابتدا و انتها
                //درخواست بازه ای بدون انتدا و انتها
                if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else if (req.Precard.IsDaily)
            {
                proxy.RequestType = RequestType.Daily;
            }
            else if (req.Precard.IsHourly)
            {
                proxy.RequestType = RequestType.Hourly;
            }

            proxy.AttachmentFile = req.AttachmentFile;
            proxy.IsEdited = req.IsEdited;
            return proxy;
        }

        private bool ValidatePrecardForCurrentUser(Request request)
        {
            if (request.Precard == null || (request.Precard != null && request.Precard.ID == 0))
                return true;
            bool isPrecardAccessible = false;
            if (request.Precard != null && request.Precard.ID > 0)
            {
                decimal precardId = request.Precard.ID;
                Precard precard = new BPrecard().GetByID(precardId);
                if (precard.AccessRoleList != null)
                {
                    if (precard.AccessRoleList.Any(x => x.ID == BUser.CurrentUser.Role.ID))
                    {
                        isPrecardAccessible = true;
                        return isPrecardAccessible;
                    }
                    else
                    {
                        if (SessionHelper.HasSessionValue(SessionHelper.GTSCurrentUserManagmentState))
                        {
                            IList<decimal> otherRoleIDsList = new List<decimal>();
                            Dictionary<string, object> managementState = (Dictionary<string, object>)SessionHelper.GetSessionValue(SessionHelper.GTSCurrentUserManagmentState);
                            if ((bool)managementState["IsManager"] && managementState["ManagerRoleId"] != null)
                                otherRoleIDsList.Add((decimal)managementState["ManagerRoleId"]);
                            if ((bool)managementState["IsOperator"] && managementState["OperatorRoleId"] != null)
                                otherRoleIDsList.Add((decimal)managementState["OperatorRoleId"]);
                            if ((bool)managementState["IsSubstitute"] && managementState["SubstituteRoleId"] != null)
                                otherRoleIDsList.Add((decimal)managementState["SubstituteRoleId"]);

                            foreach (int otherRoleID in otherRoleIDsList)
                            {
                                if (precard.AccessRoleList.Any(x => x.ID == otherRoleID))
                                {
                                    isPrecardAccessible = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return isPrecardAccessible;
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRegisteredRequestsLoadAccess_onMainPage()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRegisteredRequestsLoadAccess_onMonthlyOperationGridSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRegisteredRequestsLoadAccess_onMonthlyOperationGanttChartSchema()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRequestRgisterLoadAccess_onNormalUser()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRequestRgisterLoadAccess_onOperator()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleHourlyRequestByNormalUser(Request request, int year, int month)
        {
            //DNN Note:Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month);
            return (this as IRegisteredRequests).InsertRequest(request, year, month);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleDailyRequestByNormalUser(Request request, int year, int month)
        {
            //DNN Note:Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month);
            return (this as IRegisteredRequests).InsertRequest(request, year, month);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleOverTimeRequestByNormalUser(Request request, int year, int month)
        {
            //DNN Note:Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month);
            return (this as IRegisteredRequests).InsertRequest(request, year, month);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleHourlyRequestByOperator(Request request, int year, int month, decimal personnelID)
        {
            //DNN Note:Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month, personnelID);
            return (this as IRegisteredRequests).InsertRequest(request, year, month, personnelID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleDailyRequestByOperator(Request request, int year, int month, decimal personnelID)
        {
            //DNN Note:Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month, personnelID);
            return (this as IRegisteredRequests).InsertRequest(request, year, month, personnelID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleOverTimeRequestByOperator(Request request, int year, int month, decimal personnelID)
        {
            //DNN Note:Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequest(request, year, month, personnelID);
            return (this as IRegisteredRequests).InsertRequest(request, year, month, personnelID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveHourlyRequestByOperator()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveDailyRequestByOperator()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveOverTimeRequestByOperator()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        int IRegisteredRequests.InsertImperativeRequestByOperator(Request request, ImperativeRequest imperativeRequest, IList<decimal> PersonIDsList)
        {
            try
            {
                //DNN Note:Improve Performance
                //IRegisteredRequests kartableBusiness = new BKartabl();
                //BRequest requestBusiness = new BRequest();
                BImperativeRequest imperativeRequestBusiness = new BImperativeRequest();

                foreach (decimal personID in PersonIDsList)
                {
                    imperativeRequest.Person = new Person() { ID = personID };
                    ImperativeRequest impReq = imperativeRequestBusiness.GetImperativeRequest(imperativeRequest);
                    if (impReq != null && !impReq.IsLocked)
                    {
                        Request req = (Request)request.Clone();
                        req.IsDateSetByUser = true;
                        req.Person = new Person() { ID = personID };
                        req.TheTimeDuration = impReq.Value.ToString();
                        req.Description = impReq.Description;
                        this.bRequest.InsertRequest(req);
                    }
                }

                //int count = kartableBusiness.GetUserRequestCount(RequestState.UnKnown, imperativeRequest.Year, imperativeRequest.Month);
                int count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, imperativeRequest.Year, imperativeRequest.Month);
                this.LogCollectiveRequest(request, PersonIDsList.Count);
                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertImperativeRequestByOperator");
                throw ex;
            }

        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckKartableLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool ConfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            //DNN Note:Improve Performance
            //return ((IKartablRequests)(new BKartabl())).SetStatusOfRequest(requests, status, description, out requestValidationProxyList, applicatorID);
            return (this as IKartablRequests).SetStatusOfRequest(requests, status, description, out requestValidationProxyList, applicatorID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool UnconfirmRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            //DNN Note:Improve Performance
            //return ((IKartablRequests)(new BKartabl())).SetStatusOfRequest(requests, status, description, out requestValidationProxyList, applicatorID);
            return (this as IKartablRequests).SetStatusOfRequest(requests, status, description, out requestValidationProxyList, applicatorID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool ConfirmSpecialRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            //DNN Note:Improve Performance
            //return ((IKartablRequests)(new BKartabl())).SetStatusOfSpecialRequest(requests, status, description, out requestValidationProxyList, applicatorID);
            return (this as IKartablRequests).SetStatusOfSpecialRequest(requests, status, description, out requestValidationProxyList, applicatorID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public bool UnconfirmSpecialRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            //DNN Note:Improve Performance
            //return ((IKartablRequests)(new BKartabl())).SetStatusOfSpecialRequest(requests, status, description, out requestValidationProxyList, applicatorID);
            return (this as IKartablRequests).SetStatusOfSpecialRequest(requests, status, description, out requestValidationProxyList, applicatorID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSurveyedRequestsLoadAccess()
        {
        }

        //public int GetAllSpecialKartableRequestsCount(RequestType requestType, RequestState requestState, int year, int month, string searchKey)
        //{

        //    try
        //    {
        //        NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
        //        BManager bManager = new BManager();
        //        BUser bUser = new BUser();
        //        Manager managerAlias = null;
        //        ManagerFlow managerFlowAlias = null;
        //        Flow flowAlias = null;
        //        DateTime fromDate, toDate;
        //        int count = 0;
        //        Request requestAlias = null;
        //        RequestStatus requestStatusAlias = null;
        //        Precard precardAlias = null;
        //        PrecardGroups precardGroupsAlias = null;
        //        Person personAlias = null;
        //        Department departmentAlias = null;
        //        GTS.Clock.Model.Temp.Temp departmentTempAlias = null;
        //        GTS.Clock.Model.Temp.Temp precardTempAlias = null;
        //        string departmentOperationGUID = string.Empty;
        //        string precardOperationGUID = string.Empty;
        //        NHibernate.IQueryOver<Request, Request> requestsQueryExpression = null;


        //        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
        //            fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
        //            toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
        //        }
        //        else
        //        {
        //            int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
        //            fromDate = new DateTime(year, month, 1);
        //            toDate = new DateTime(year, month, endOfMonth);
        //        }

        //        Manager manager = bManager.GetManagerByUsername(this.workingUsername);
        //        if (manager == null)
        //        {
        //            UIValidationExceptions exception = new UIValidationExceptions();
        //            exception.Add(ExceptionResourceKeys.ManagerIsInvalid, "شخص جاری مدیر نمی باشد", ExceptionSrc);
        //            throw exception;
        //        }

        //        IList<Manager> managerList = CurrentNHSession.QueryOver<Manager>(() => managerAlias)
        //                                     .JoinAlias(() => managerAlias.ManagerFlowList, () => managerFlowAlias)
        //                                     .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
        //                                     .Where(() => !flowAlias.IsDeleted &&
        //                                                  flowAlias.ActiveFlow &&
        //                                                  managerAlias.Active &&
        //                                                  managerAlias.ID == manager.ID &&
        //                                                  managerFlowAlias.Active &&
        //                                                  managerFlowAlias.Level == 1)
        //                                     .WithSubquery
        //                                     .WhereNotExists(QueryOver.Of<ManagerFlow>(() => managerFlowAlias)
        //                                                              .Where(() => managerFlowAlias.Flow.ID == flowAlias.ID)
        //                                                              .And(() => managerFlowAlias.Active)
        //                                                              .And(() => managerFlowAlias.Level > 1)
        //                                                              .Select(x => x.ID)
        //                                                    )
        //                                     .List<Manager>();
        //        if (managerList == null || managerList.Count == 0)
        //        {
        //            UIValidationExceptions exception = new UIValidationExceptions();
        //            exception.Add(ExceptionResourceKeys.ManagerFlowIsInvalid, "جریانی که تنها مدیر آن شخص جاری باشد وجود ندارد", ExceptionSrc);
        //            throw exception;
        //        }

        //        IList<decimal> accessibleDepartments = ((IDataAccess)bUser).GetAccessibleDeparments();
        //        if (accessibleDepartments == null || accessibleDepartments.Count == 0)
        //        {
        //            UIValidationExceptions exception = new UIValidationExceptions();
        //            exception.Add(ExceptionResourceKeys.NoAccessibleDepartmentIsAvailable, "سطوح دسترسی بخش برای شخص جاری مقدار نگرفته است", ExceptionSrc);
        //            throw exception;
        //        }

        //        IList<decimal> accessiblePrecards = ((IDataAccess)bUser).GetAccessiblePrecards();
        //        if (accessiblePrecards == null || accessiblePrecards.Count == 0)
        //        {
        //            UIValidationExceptions exception = new UIValidationExceptions();
        //            exception.Add(ExceptionResourceKeys.NoAccessiblePrecardIsAvailable, "سطوح دسترسی پیشکارت برای شخص جاری مقدار نگرفته است", ExceptionSrc);
        //            throw exception;
        //        }

        //        departmentOperationGUID = this.bTemp.InsertTempList(accessibleDepartments);
        //        precardOperationGUID = this.bTemp.InsertTempList(accessiblePrecards);

        //        if (searchKey == string.Empty)
        //        {
        //            if (requestType != RequestType.None)
        //            {
        //                requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias)
        //                                               .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                               .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                               .JoinAlias(() => precardAlias.PrecardGroup, () => precardGroupsAlias)
        //                                               .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                               .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                               .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                               .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                             precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                           ((requestAlias.FromDate >= fromDate &&
        //                                                             requestAlias.FromDate <= toDate) ||
        //                                                            (requestAlias.ToDate >= fromDate &&
        //                                                             requestAlias.ToDate <= toDate))
        //                                                     );
        //                switch (requestType)
        //                {
        //                    case RequestType.Hourly:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsHourly &&
        //                                                                                      precardGroupsAlias.IntLookupKey != (int)RequestType.OverWork);
        //                        break;
        //                    case RequestType.Daily:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsDaily &&
        //                                                                                      precardGroupsAlias.IntLookupKey != (int)RequestType.OverWork);
        //                        break;
        //                    case RequestType.OverWork:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.IntLookupKey == (int)RequestType.OverWork);
        //                        break;
        //                    case RequestType.Imperative:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.IntLookupKey == (int)RequestType.Imperative);
        //                        break;
        //                }
        //                count = requestsQueryExpression.RowCount();
        //            }
        //            else
        //            {
        //                if (requestState != RequestState.UnKnown)
        //                {
        //                    requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias).Left
        //                                                              .JoinAlias(() => requestAlias.RequestStatusList, () => requestStatusAlias)
        //                                                              .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                                              .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                                              .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                                              .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                                              .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                                              .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                                            precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                                          ((requestAlias.FromDate >= fromDate &&
        //                                                                            requestAlias.FromDate <= toDate) ||
        //                                                                           (requestAlias.ToDate >= fromDate &&
        //                                                                            requestAlias.ToDate <= toDate))
        //                                                                    );

        //                    var DeletedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
        //                                                                .Where(() => requestStatusAlias.EndFlow == true)
        //                                                                .And(() => requestStatusAlias.IsDeleted)
        //                                                                .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
        //                                                                .Select(x => x.ID);

        //                    switch (requestState)
        //                    {
        //                        case RequestState.Confirmed:
        //                            var DeletedAndUnconfirmedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
        //                                                                                      .Where(() => requestStatusAlias.EndFlow == true)
        //                                                                                      .And(() => requestStatusAlias.IsDeleted || !requestStatusAlias.Confirm)
        //                                                                                      .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
        //                                                                                      .Select(x => x.ID);
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.Confirm && requestStatusAlias.EndFlow)
        //                                                                             .WithSubquery
        //                                                                             .WhereNotExists(DeletedAndUnconfirmedRequestStatusSubQuery);
        //                            break;
        //                        case RequestState.Unconfirmed:
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => !requestStatusAlias.Confirm && requestStatusAlias.EndFlow)
        //                                                                             .WithSubquery
        //                                                                             .WhereNotExists(DeletedRequestStatusSubQuery);
        //                            break;
        //                        case RequestState.UnderReview:
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => !requestAlias.EndFlow);
        //                            break;
        //                        case RequestState.Deleted:
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.EndFlow && requestStatusAlias.IsDeleted);
        //                            break;
        //                    }
        //                    count = requestsQueryExpression.RowCount();
        //                }
        //                else
        //                {
        //                    count = CurrentNHSession.QueryOver<Request>(() => requestAlias)
        //                                                   .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                                   .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                                   .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                                   .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                                   .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                                   .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                                 precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                               ((requestAlias.FromDate >= fromDate &&
        //                                                                 requestAlias.FromDate <= toDate) ||
        //                                                                (requestAlias.ToDate >= fromDate &&
        //                                                                 requestAlias.ToDate <= toDate))
        //                                                         )
        //                                                   .RowCount();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            count = CurrentNHSession.QueryOver<Request>(() => requestAlias)
        //                                    .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                    .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                    .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                    .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                    .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                    .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                  precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                 (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                  personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                  personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                  precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                  requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
        //                                                  ) &&
        //                                                ((requestAlias.FromDate >= fromDate &&
        //                                                  requestAlias.FromDate <= toDate) ||
        //                                                 (requestAlias.ToDate >= fromDate &&
        //                                                  requestAlias.ToDate <= toDate))
        //                                           )
        //                                     .RowCount();
        //        }

        //        return count;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllSpecialKartableRequestsCount");
        //        throw ex;
        //    }
        //}


        public int GetAllSpecialKartableRequestsCount(RequestType requestType, RequestState requestState, int year, int month, string searchKey, ViewState viewState, string FromDate, string ToDate)
        {
            try
            {
                NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
                BManager bManager = new BManager();
                BUser bUser = new BUser();
                Manager managerAlias = null;
                ManagerFlow managerFlowAlias = null;
                Flow flowAlias = null;
                UIValidationExceptions exception = new UIValidationExceptions();
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                int count = 0;
                Request requestAlias = null;
                Request subRequestAlias = null;
                Request subRequestParent = null;
                RequestStatus requestStatusAlias = null;
                Precard precardAlias = null;
                PrecardGroups precardGroupsAlias = null;
                Person personAlias = null;
                Department departmentAlias = null;
                RequestSubstitute requestSubstituteAlias = null;
                GTS.Clock.Model.Temp.Temp departmentTempAlias = null;
                GTS.Clock.Model.Temp.Temp precardTempAlias = null;
                GTS.Clock.Model.Temp.Temp RequestTempAlias = null;
                string departmentOperationGUID = string.Empty;
                string precardOperationGUID = string.Empty;
                string RequestOperationGUID = string.Empty;
                NHibernate.IQueryOver<Request, Request> requestsQueryExpression = null;
                switch (viewState)
                {
                    case ViewState.YearMonth:
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                            fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                            toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                        }
                        else
                        {
                            int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                            fromDate = new DateTime(year, month, 1);
                            toDate = new DateTime(year, month, endOfMonth);
                        }

                        break;
                    case ViewState.Date:
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            fromDate = Utility.ToMildiDate(FromDate);
                            toDate = Utility.ToMildiDate(ToDate);
                        }
                        else
                        {
                            fromDate = Utility.ToMildiDateTime(FromDate);
                            toDate = Utility.ToMildiDateTime(ToDate);
                        }
                        if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                        }
                        if (exception.Count > 0)
                        {
                            throw exception;
                        }
                        break;
                }


                Manager manager = bManager.GetManagerByUsername(this.workingUsername);
                if (manager == null)
                {
                    //UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.ManagerIsInvalid, "شخص جاری مدیر نمی باشد", ExceptionSrc);
                    throw exception;
                }

                IList<Manager> managerList = CurrentNHSession.QueryOver<Manager>(() => managerAlias)
                                             .JoinAlias(() => managerAlias.ManagerFlowList, () => managerFlowAlias)
                                             .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                             .Where(() => !flowAlias.IsDeleted &&
                                                          flowAlias.ActiveFlow &&
                                                          managerAlias.Active &&
                                                          managerAlias.ID == manager.ID &&
                                                          managerFlowAlias.Active &&
                                                          managerFlowAlias.Level == 1)
                                             .WithSubquery
                                             .WhereNotExists(QueryOver.Of<ManagerFlow>(() => managerFlowAlias)
                                                                      .Where(() => managerFlowAlias.Flow.ID == flowAlias.ID)
                                                                      .And(() => managerFlowAlias.Active)
                                                                      .And(() => managerFlowAlias.Level > 1)
                                                                      .Select(x => x.ID)
                                                            )
                                             .List<Manager>();
                if (managerList == null || managerList.Count == 0)
                {
                    // UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.ManagerFlowIsInvalid, "جریانی که تنها مدیر آن شخص جاری باشد وجود ندارد", ExceptionSrc);
                    throw exception;
                }

                IList<decimal> accessibleDepartments = ((IDataAccess)bUser).GetAccessibleDeparments();
                if (accessibleDepartments == null || accessibleDepartments.Count == 0)
                {
                    //UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.NoAccessibleDepartmentIsAvailable, "سطوح دسترسی بخش برای شخص جاری مقدار نگرفته است", ExceptionSrc);
                    throw exception;
                }

                IList<decimal> accessiblePrecards = ((IDataAccess)bUser).GetAccessiblePrecards();
                if (accessiblePrecards == null || accessiblePrecards.Count == 0)
                {
                    //UIValidationExceptions exception = new UIValidationExceptions();
                    exception.Add(ExceptionResourceKeys.NoAccessiblePrecardIsAvailable, "سطوح دسترسی پیشکارت برای شخص جاری مقدار نگرفته است", ExceptionSrc);
                    throw exception;
                }

                departmentOperationGUID = this.bTemp.InsertTempList(accessibleDepartments);
                precardOperationGUID = this.bTemp.InsertTempList(accessiblePrecards);

                if (requestType != RequestType.None)
                {
                    requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias)
                                                              .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                              .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
                                                              .JoinAlias(() => precardAlias.PrecardGroup, () => precardGroupsAlias)
                                                              .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                              .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
                                                              .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
                                                              .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
                                                                             precardTempAlias.OperationGUID == precardOperationGUID &&
                                                                            !personAlias.IsDeleted && personAlias.Active &&
                                                                            (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                             personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                             personAlias.FullName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                             personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                             precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                             requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                            ) &&
                                                                           ((requestAlias.FromDate >= fromDate &&
                                                                             requestAlias.FromDate <= toDate) ||
                                                                            (requestAlias.ToDate >= fromDate &&
                                                                             requestAlias.ToDate <= toDate))
                                                                     );
                    switch (requestType)
                    {
                        case RequestType.Hourly:
                            IList<decimal> RequestDailyIds = NHSession.QueryOver<Request>(() => requestAlias)
                                                                      .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                                      .Where(() => precardAlias.IsDaily &&
                                                                                   requestAlias.Parent == null &&
                                                                                  ((requestAlias.FromDate >= fromDate && requestAlias.FromDate <= toDate) ||
                                                                                   (requestAlias.ToDate >= fromDate && requestAlias.ToDate <= toDate)
                                                                                  )
                                                                             )
                                                                      .Select(x => x.ID)
                                                                      .List<decimal>();

                            RequestOperationGUID = bTemp.InsertTempList(RequestDailyIds);
                            var DailyTermimateRequests = QueryOver.Of<Request>(() => subRequestAlias)
                                                                        .JoinAlias(() => subRequestAlias.Parent, () => subRequestParent)
                                                                        .JoinAlias(() => subRequestParent.TempList, () => RequestTempAlias)
                                                                         .Where(() => subRequestAlias.Parent != null &&
                                                                                      RequestTempAlias.OperationGUID == RequestOperationGUID &&
                                                                                      subRequestAlias.ID == requestAlias.ID
                                                                                )
                                                                         .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsHourly &&
                                                                                          precardGroupsAlias.LookupKey != RequestType.OverWork.ToString().ToLower()
                                                                                   )
                                                                             .WithSubquery
                                                                             .WhereNotExists(DailyTermimateRequests);
                            break;
                        case RequestType.Daily:
                            IList<decimal> RequestHourlyIds = NHSession.QueryOver<Request>(() => requestAlias)
                                                                     .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                                     .Where(() => precardAlias.IsHourly &&
                                                                                  requestAlias.Parent == null &&
                                                                                 ((requestAlias.FromDate >= fromDate && requestAlias.FromDate <= toDate) ||
                                                                                  (requestAlias.ToDate >= fromDate && requestAlias.ToDate <= toDate)
                                                                                 )
                                                                            )
                                                                     .Select(x => x.ID)
                                                                     .List<decimal>();

                            RequestOperationGUID = bTemp.InsertTempList(RequestHourlyIds);
                            var HourlyTermimateRequests = QueryOver.Of<Request>(() => subRequestAlias)
                                                                         .JoinAlias(() => subRequestAlias.Parent, () => subRequestParent)
                                                                         .JoinAlias(() => subRequestParent.TempList, () => RequestTempAlias)
                                                                         .Where(() => subRequestAlias.Parent != null &&
                                                                                      RequestTempAlias.OperationGUID == RequestOperationGUID &&
                                                                                      subRequestAlias.ID == requestAlias.ID
                                                                                )
                                                                         .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsDaily &&
                                                                                          precardGroupsAlias.LookupKey != RequestType.OverWork.ToString().ToLower()
                                                                                   )
                                                                              .WithSubquery
                                                                              .WhereNotExists(HourlyTermimateRequests);
                            break;
                        case RequestType.OverWork:
                            requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.LookupKey == RequestType.OverWork.ToString().ToLower());
                            break;
                        case RequestType.Imperative:
                            requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.LookupKey == RequestType.Imperative.ToString().ToLower());
                            break;
                        case RequestType.Terminate:
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestAlias.Parent != null);
                            break;
                    }
                    count = requestsQueryExpression.RowCount();
                    bTemp.DeleteTempList(RequestOperationGUID);
                }
                else if (requestState != RequestState.UnKnown)
                {
                    requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias).Left
                                                              .JoinAlias(() => requestAlias.RequestSubstitute, () => requestSubstituteAlias).Left
                                                              .JoinAlias(() => requestAlias.RequestStatusList, () => requestStatusAlias)
                                                              .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                              .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
                                                              .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                              .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
                                                              .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
                                                              .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
                                                                            precardTempAlias.OperationGUID == precardOperationGUID &&
                                                                           !personAlias.IsDeleted && personAlias.Active &&
                                                                           (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.FullName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                           ) &&
                                                                          ((requestAlias.FromDate >= fromDate &&
                                                                            requestAlias.FromDate <= toDate) ||
                                                                           (requestAlias.ToDate >= fromDate &&
                                                                            requestAlias.ToDate <= toDate))
                                                                    );

                    var DeletedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
                                                                .Where(() => requestStatusAlias.EndFlow == true)
                                                                .And(() => requestStatusAlias.IsDeleted)
                                                                .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
                                                                .Select(x => x.ID);

                    switch (requestState)
                    {
                        case RequestState.Confirmed:
                            var DeletedAndUnconfirmedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
                                                                                      .Where(() => requestStatusAlias.EndFlow == true)
                                                                                      .And(() => requestStatusAlias.IsDeleted || !requestStatusAlias.Confirm)
                                                                                      .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
                                                                                      .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.Confirm && requestStatusAlias.EndFlow)
                                                                             .WithSubquery
                                                                             .WhereNotExists(DeletedAndUnconfirmedRequestStatusSubQuery);
                            break;
                        case RequestState.Unconfirmed:
                            requestsQueryExpression = requestsQueryExpression.Where(() => (!requestStatusAlias.Confirm && requestStatusAlias.EndFlow) || !(bool)requestSubstituteAlias.Confirmed)
                                                                             .WithSubquery
                                                                             .WhereNotExists(DeletedRequestStatusSubQuery);
                            break;
                        case RequestState.UnderReview:
                            var UnconfirmedRequestSubstituteSubQuery = QueryOver.Of<RequestSubstitute>(() => requestSubstituteAlias)
                                                    .Where(() => !(bool)requestSubstituteAlias.Confirmed)
                                                    .And(() => requestSubstituteAlias.Request.ID == requestAlias.ID)
                                                    .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => !requestAlias.EndFlow)
                                                                             .WithSubquery
                                                                             .WhereNotExists(UnconfirmedRequestSubstituteSubQuery);
                            break;
                        case RequestState.Deleted:
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.EndFlow && requestStatusAlias.IsDeleted);
                            break;
                        case RequestState.Terminated:
                            Request requestChildsAlias = null;
                            var HasTerminateRequestSubQuery = QueryOver.Of<Request>(() => requestChildsAlias)
                                                                       .Where(() => requestChildsAlias.ID == requestAlias.ID)
                                                                       .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.EndFlow && requestStatusAlias.IsDeleted)
                                                                             .WithSubquery
                                                                             .WhereExists(HasTerminateRequestSubQuery);
                            break;
                    }
                    count = requestsQueryExpression.RowCount();
                }
                else
                {
                    count = CurrentNHSession.QueryOver<Request>(() => requestAlias)
                                            .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                            .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
                                            .JoinAlias(() => requestAlias.Person, () => personAlias)
                                            .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
                                            .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
                                            .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
                                                          precardTempAlias.OperationGUID == precardOperationGUID &&
                                                         !personAlias.IsDeleted && personAlias.Active &&
                                                         (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                          personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                          personAlias.FullName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                          personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                          precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                          requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                          ) &&
                                                        ((requestAlias.FromDate >= fromDate &&
                                                          requestAlias.FromDate <= toDate) ||
                                                         (requestAlias.ToDate >= fromDate &&
                                                          requestAlias.ToDate <= toDate))
                                                   )
                                             .RowCount();
                }

                if (departmentOperationGUID != string.Empty)
                    bTemp.DeleteTempList(departmentOperationGUID);
                if (precardOperationGUID != string.Empty)
                    bTemp.DeleteTempList(precardOperationGUID);

                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllSpecialKartableRequestsCount");
                throw ex;
            }
        }

        //public IList<KartablProxy> GetAllSpecialKartableRequests(RequestType requestType, RequestState requestState, int year, int month, int pageIndex, int pageSize, string searchKey, KartablOrderBy orderby)
        //{
        //    try
        //    {
        //        NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
        //        BManager bManager = new BManager();
        //        BUser bUser = new BUser();
        //        DateTime fromDate, toDate;
        //        Request requestAlias = null;
        //        RequestStatus requestStatusAlias = null;
        //        Precard precardAlias = null;
        //        PrecardGroups precardGroupsAlias = null;
        //        Person personAlias = null;
        //        Department departmentAlias = null;
        //        IList<Request> requestsList = null;
        //        int reqIndex = 1;
        //        GTS.Clock.Model.Temp.Temp departmentTempAlias = null;
        //        GTS.Clock.Model.Temp.Temp precardTempAlias = null;
        //        string departmentOperationGUID = string.Empty;
        //        string precardOperationGUID = string.Empty;
        //        NHibernate.IQueryOver<Request, Request> requestsQueryExpression = null;
        //        System.Linq.Expressions.Expression<Func<object>> orderByPath = null;


        //        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
        //        {
        //            int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
        //            fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
        //            toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
        //        }
        //        else
        //        {
        //            int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
        //            fromDate = new DateTime(year, month, 1);
        //            toDate = new DateTime(year, month, endOfMonth);
        //        }

        //        IList<KartablProxy> kartablResult = new List<KartablProxy>();
        //        IList<InfoRequest> result = new List<InfoRequest>();

        //        IList<decimal> accessibleDepartments = ((IDataAccess)bUser).GetAccessibleDeparments();
        //        IList<decimal> accessiblePrecards = ((IDataAccess)bUser).GetAccessiblePrecards();

        //        departmentOperationGUID = this.bTemp.InsertTempList(accessibleDepartments);
        //        precardOperationGUID = this.bTemp.InsertTempList(accessiblePrecards);

        //        switch (orderby)
        //        {
        //            case KartablOrderBy.PersonCode:
        //                orderByPath = () => personAlias.BarCode;
        //                break;
        //            case KartablOrderBy.PersonName:
        //                orderByPath = () => personAlias.FirstName;
        //                break;
        //            case KartablOrderBy.RegisteredBy:
        //                orderByPath = () => requestAlias.OperatorUser;
        //                break;
        //            case KartablOrderBy.RequestSubject:
        //                orderByPath = () => precardAlias.Name;
        //                break;
        //            case KartablOrderBy.RequestDate:
        //                orderByPath = () => requestAlias.FromDate;
        //                break;
        //        }

        //        if (searchKey == string.Empty)
        //        {
        //            if (requestType != RequestType.None)
        //            {
        //                requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias)
        //                                                          .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                                          .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                                          .JoinAlias(() => precardAlias.PrecardGroup, () => precardGroupsAlias)
        //                                                          .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                                          .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                                          .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                                          .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                                        precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                                      ((requestAlias.FromDate >= fromDate &&
        //                                                                        requestAlias.FromDate <= toDate) ||
        //                                                                       (requestAlias.ToDate >= fromDate &&
        //                                                                        requestAlias.ToDate <= toDate))
        //                                                                 );
        //                switch (requestType)
        //                {
        //                    case RequestType.Hourly:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsHourly &&
        //                                                                                      precardGroupsAlias.LookupKey != RequestType.OverWork.ToString().ToLower());
        //                        break;
        //                    case RequestType.Daily:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsDaily &&
        //                                                                                      precardGroupsAlias.LookupKey != RequestType.OverWork.ToString().ToLower());
        //                        break;
        //                    case RequestType.OverWork:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.LookupKey == RequestType.OverWork.ToString().ToLower());
        //                        break;
        //                    case RequestType.Imperative:
        //                        requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.LookupKey == RequestType.Imperative.ToString().ToLower());
        //                        break;
        //                }
        //                requestsList = requestsQueryExpression.OrderBy(orderByPath)
        //                                                      .Desc
        //                                                      .Skip(pageIndex * pageSize)
        //                                                      .Take(pageSize)
        //                                                      .List<Request>();
        //            }
        //            else
        //            {
        //                if (requestState != RequestState.UnKnown)
        //                {
        //                    requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias).Left
        //                                                              .JoinAlias(() => requestAlias.RequestStatusList, () => requestStatusAlias)
        //                                                              .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                                              .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                                              .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                                              .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                                              .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                                              .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                                            precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                                          ((requestAlias.FromDate >= fromDate &&
        //                                                                            requestAlias.FromDate <= toDate) ||
        //                                                                           (requestAlias.ToDate >= fromDate &&
        //                                                                            requestAlias.ToDate <= toDate))
        //                                                                    );

        //                    var DeletedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
        //                                                                .Where(() => requestStatusAlias.EndFlow == true)
        //                                                                .And(() => requestStatusAlias.IsDeleted)
        //                                                                .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
        //                                                                .Select(x => x.ID);

        //                    switch (requestState)
        //                    {
        //                        case RequestState.Confirmed:
        //                            var DeletedAndUnconfirmedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
        //                                                                                      .Where(() => requestStatusAlias.EndFlow == true)
        //                                                                                      .And(() => requestStatusAlias.IsDeleted || !requestStatusAlias.Confirm)
        //                                                                                      .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
        //                                                                                      .Select(x => x.ID);
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.Confirm && requestStatusAlias.EndFlow)
        //                                                                             .WithSubquery
        //                                                                             .WhereNotExists(DeletedAndUnconfirmedRequestStatusSubQuery);
        //                            break;
        //                        case RequestState.Unconfirmed:
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => !requestStatusAlias.Confirm && requestStatusAlias.EndFlow)
        //                                                                             .WithSubquery
        //                                                                             .WhereNotExists(DeletedRequestStatusSubQuery);
        //                            break;
        //                        case RequestState.UnderReview:
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => !requestAlias.EndFlow);
        //                            break;
        //                        case RequestState.Deleted:
        //                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.EndFlow && requestStatusAlias.IsDeleted);
        //                            break;
        //                    }
        //                    requestsList = requestsQueryExpression.OrderBy(orderByPath)
        //                                                          .Desc
        //                                                          .Skip(pageIndex * pageSize)
        //                                                          .Take(pageSize)
        //                                                          .List<Request>();
        //                }
        //                else
        //                {
        //                    requestsList = CurrentNHSession.QueryOver<Request>(() => requestAlias)
        //                                                   .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                                   .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                                   .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                                   .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                                   .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                                   .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                                 precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                               ((requestAlias.FromDate >= fromDate &&
        //                                                                 requestAlias.FromDate <= toDate) ||
        //                                                                (requestAlias.ToDate >= fromDate &&
        //                                                                 requestAlias.ToDate <= toDate))
        //                                                         )
        //                                                   .OrderBy(orderByPath)
        //                                                   .Desc
        //                                                   .Skip(pageIndex * pageSize)
        //                                                   .Take(pageSize)
        //                                                   .List<Request>();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            requestsList = CurrentNHSession.QueryOver<Request>(() => requestAlias)
        //                                           .JoinAlias(() => requestAlias.Precard, () => precardAlias)
        //                                           .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
        //                                           .JoinAlias(() => requestAlias.Person, () => personAlias)
        //                                           .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
        //                                           .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
        //                                           .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
        //                                                         precardTempAlias.OperationGUID == precardOperationGUID &&
        //                                                        (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                         personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                         personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                         precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
        //                                                         requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
        //                                                        ) &&
        //                                                       ((requestAlias.FromDate >= fromDate &&
        //                                                         requestAlias.FromDate <= toDate) ||
        //                                                        (requestAlias.ToDate >= fromDate &&
        //                                                         requestAlias.ToDate <= toDate))
        //                                                 )
        //                                           .OrderBy(orderByPath)
        //                                           .Desc
        //                                           .Skip(pageIndex * pageSize)
        //                                           .Take(pageSize)
        //                                           .List<Request>();
        //        }

        //        if (departmentOperationGUID != string.Empty)
        //            this.bTemp.DeleteTempList(departmentOperationGUID);
        //        if (precardOperationGUID != string.Empty)
        //            this.bTemp.DeleteTempList(precardOperationGUID);


        //        foreach (Request requestItem in requestsList)
        //        {
        //            KartablProxy proxy = new KartablProxy();
        //            proxy = this.ConvertSpecialKartablRequestToProxy(requestItem, reqIndex, pageIndex, pageSize);
        //            kartablResult.Add(proxy);
        //            reqIndex++;
        //        }
        //        SessionHelper.ClearSessionValue(SessionHelper.ISpecialKartablRequestsKey);
        //        SessionHelper.SaveSessionValue(SessionHelper.ISpecialKartablRequestsKey, requestsList.Select(x => x.ID).ToList<decimal>());
        //        return kartablResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllSpecialKartableRequests");
        //        throw ex;
        //    }

        //}

        public IList<KartablProxy> GetAllSpecialKartableRequests(RequestType requestType, RequestState requestState, int year, int month, int pageIndex, int pageSize, string searchKey, KartablOrderBy orderby, ViewState viewState, string FromDate, string ToDate)
        {
            try
            {
                NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
                BManager bManager = new BManager();
                BUser bUser = new BUser();
                UIValidationExceptions exception = new UIValidationExceptions();
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                Request requestAlias = null;
                Request subRequestAlias = null;
                Request subRequestParent = null;
                RequestStatus requestStatusAlias = null;
                Precard precardAlias = null;
                PrecardGroups precardGroupsAlias = null;
                Person personAlias = null;
                Department departmentAlias = null;
                RequestSubstitute requestSubstituteAlias = null;
                IList<Request> requestsList = null;
                int reqIndex = 1;
                GTS.Clock.Model.Temp.Temp departmentTempAlias = null;
                GTS.Clock.Model.Temp.Temp precardTempAlias = null;
                GTS.Clock.Model.Temp.Temp RequestTempAlias = null;
                string departmentOperationGUID = string.Empty;
                string precardOperationGUID = string.Empty;
                string RequestOperationGUID = string.Empty;
                NHibernate.IQueryOver<Request, Request> requestsQueryExpression = null;
                System.Linq.Expressions.Expression<Func<object>> orderByPath = null;
                switch (viewState)
                {
                    case ViewState.YearMonth:
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                            fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                            toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                        }
                        else
                        {
                            int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                            fromDate = new DateTime(year, month, 1);
                            toDate = new DateTime(year, month, endOfMonth);
                        }

                        break;
                    case ViewState.Date:
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            fromDate = Utility.ToMildiDate(FromDate);
                            toDate = Utility.ToMildiDate(ToDate);
                        }
                        else
                        {
                            fromDate = Utility.ToMildiDateTime(FromDate);
                            toDate = Utility.ToMildiDateTime(ToDate);
                        }
                        if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                        }
                        if (exception.Count > 0)
                        {
                            throw exception;
                        }
                        break;
                }
                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                IList<InfoRequest> result = new List<InfoRequest>();

                IList<decimal> accessibleDepartments = ((IDataAccess)bUser).GetAccessibleDeparments();
                IList<decimal> accessiblePrecards = ((IDataAccess)bUser).GetAccessiblePrecards();

                departmentOperationGUID = this.bTemp.InsertTempList(accessibleDepartments);
                precardOperationGUID = this.bTemp.InsertTempList(accessiblePrecards);

                switch (orderby)
                {
                    case KartablOrderBy.PersonCode:
                        orderByPath = () => personAlias.BarCode;
                        break;
                    case KartablOrderBy.PersonName:
                        orderByPath = () => personAlias.FirstName;
                        break;
                    case KartablOrderBy.RegisteredBy:
                        orderByPath = () => requestAlias.OperatorUser;
                        break;
                    case KartablOrderBy.RequestSubject:
                        orderByPath = () => precardAlias.Name;
                        break;
                    case KartablOrderBy.RequestDate:
                        orderByPath = () => requestAlias.FromDate;
                        break;
                    case KartablOrderBy.None:
                        orderByPath = () => requestAlias.ID;
                        break;
                }

                if (requestType != RequestType.None)
                {

                    requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias).Left
                                                              .JoinAlias(() => requestAlias.RequestSubstitute, () => requestSubstituteAlias)
                                                              .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                              .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
                                                              .JoinAlias(() => precardAlias.PrecardGroup, () => precardGroupsAlias)
                                                              .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                              .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
                                                              .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
                                                              .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
                                                                            precardTempAlias.OperationGUID == precardOperationGUID &&
                                                                           !personAlias.IsDeleted && personAlias.Active &&
                                                                           (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.FullName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                           ) &&
                                                                          ((requestAlias.FromDate >= fromDate &&
                                                                            requestAlias.FromDate <= toDate) ||
                                                                           (requestAlias.ToDate >= fromDate &&
                                                                            requestAlias.ToDate <= toDate))
                                                                    );
                    switch (requestType)
                    {
                        case RequestType.Hourly:
                            IList<decimal> RequestDailyIds = NHSession.QueryOver<Request>(() => requestAlias)
                                                                       .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                                       .Where(() => precardAlias.IsDaily &&
                                                                                    requestAlias.Parent == null &&
                                                                                   ((requestAlias.FromDate >= fromDate && requestAlias.FromDate <= toDate) ||
                                                                                    (requestAlias.ToDate >= fromDate && requestAlias.ToDate <= toDate)
                                                                                   )
                                                                              )
                                                                       .Select(x => x.ID)
                                                                       .List<decimal>();
                            RequestOperationGUID = bTemp.InsertTempList(RequestDailyIds);
                            var DailyTermimateRequests = QueryOver.Of<Request>(() => subRequestAlias)
                                                                  .JoinAlias(() => subRequestAlias.Parent, () => subRequestParent)
                                                                  .JoinAlias(() => subRequestParent.TempList, () => RequestTempAlias)
                                                                         .Where(() => subRequestAlias.Parent != null &&
                                                                                      RequestTempAlias.OperationGUID == RequestOperationGUID &&
                                                                                      subRequestAlias.ID == requestAlias.ID
                                                                                )
                                                                         .Select(x => x.ID);


                            requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsHourly &&
                                                                                          precardGroupsAlias.LookupKey != RequestType.OverWork.ToString().ToLower()
                                                                                   )
                                                                             .WithSubquery
                                                                             .WhereNotExists(DailyTermimateRequests);
                            break;
                        case RequestType.Daily:
                            IList<decimal> RequestHourlyIds = NHSession.QueryOver<Request>(() => requestAlias)
                                                                      .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                                      .Where(() => precardAlias.IsHourly &&
                                                                                   requestAlias.Parent == null &&
                                                                                  ((requestAlias.FromDate >= fromDate && requestAlias.FromDate <= toDate) ||
                                                                                   (requestAlias.ToDate >= fromDate && requestAlias.ToDate <= toDate)
                                                                                  )
                                                                             )
                                                                      .Select(x => x.ID)
                                                                      .List<decimal>();

                            RequestOperationGUID = bTemp.InsertTempList(RequestHourlyIds);
                            var HourlyTermimateRequests = QueryOver.Of<Request>(() => subRequestAlias)
                                                                         .JoinAlias(() => subRequestAlias.Parent, () => subRequestParent)
                                                                         .JoinAlias(() => subRequestParent.TempList, () => RequestTempAlias)
                                                                         .Where(() => subRequestAlias.Parent != null &&
                                                                                      RequestTempAlias.OperationGUID == RequestOperationGUID &&
                                                                                      subRequestAlias.ID == requestAlias.ID
                                                                                )
                                                                         .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => precardAlias.IsDaily &&
                                                                                          precardGroupsAlias.LookupKey != RequestType.OverWork.ToString().ToLower())
                                                                             .WithSubquery
                                                                             .WhereNotExists(HourlyTermimateRequests);
                            break;
                        case RequestType.OverWork:
                            requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.LookupKey == RequestType.OverWork.ToString().ToLower());
                            break;
                        case RequestType.Imperative:
                            requestsQueryExpression = requestsQueryExpression.Where(() => precardGroupsAlias.LookupKey == RequestType.Imperative.ToString().ToLower());
                            break;
                        case RequestType.Terminate:
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestAlias.Parent != null);
                            break;
                    }
                    requestsList = requestsQueryExpression.OrderBy(orderByPath)
                                                          .Desc
                                                          .Skip(pageIndex * pageSize)
                                                          .Take(pageSize)
                                                          .List<Request>();
                    bTemp.DeleteTempList(RequestOperationGUID);
                }
                else if (requestState != RequestState.UnKnown)
                {
                    requestsQueryExpression = CurrentNHSession.QueryOver<Request>(() => requestAlias).Left
                                                              .JoinAlias(() => requestAlias.RequestSubstitute, () => requestSubstituteAlias).Left
                                                              .JoinAlias(() => requestAlias.RequestStatusList, () => requestStatusAlias)
                                                              .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                              .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
                                                              .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                              .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
                                                              .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
                                                              .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
                                                                            precardTempAlias.OperationGUID == precardOperationGUID &&
                                                                           !personAlias.IsDeleted && personAlias.Active &&
                                                                           (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.FullName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                           ) &&
                                                                          ((requestAlias.FromDate >= fromDate &&
                                                                            requestAlias.FromDate <= toDate) ||
                                                                           (requestAlias.ToDate >= fromDate &&
                                                                            requestAlias.ToDate <= toDate))
                                                                    );

                    var DeletedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
                                                                .Where(() => requestStatusAlias.EndFlow == true)
                                                                .And(() => requestStatusAlias.IsDeleted)
                                                                .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
                                                                .Select(x => x.ID);

                    switch (requestState)
                    {
                        case RequestState.Confirmed:
                            var DeletedAndUnconfirmedRequestStatusSubQuery = QueryOver.Of<RequestStatus>(() => requestStatusAlias)
                                                                                      .Where(() => requestStatusAlias.EndFlow == true)
                                                                                      .And(() => requestStatusAlias.IsDeleted || !requestStatusAlias.Confirm)
                                                                                      .And(() => requestStatusAlias.Request.ID == requestAlias.ID)
                                                                                      .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.Confirm && requestStatusAlias.EndFlow)
                                                                             .WithSubquery
                                                                             .WhereNotExists(DeletedAndUnconfirmedRequestStatusSubQuery);
                            break;
                        case RequestState.Unconfirmed:
                            requestsQueryExpression = requestsQueryExpression.Where(() => (!requestStatusAlias.Confirm && requestStatusAlias.EndFlow) || !(bool)requestSubstituteAlias.Confirmed)
                                                                             .WithSubquery
                                                                             .WhereNotExists(DeletedRequestStatusSubQuery);
                            break;
                        case RequestState.UnderReview:
                            var UnconfirmedRequestSubstituteSubQuery = QueryOver.Of<RequestSubstitute>(() => requestSubstituteAlias)
                                                                                .Where(() => !(bool)requestSubstituteAlias.Confirmed)
                                                                                .And(() => requestSubstituteAlias.Request.ID == requestAlias.ID)
                                                                                .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => !requestAlias.EndFlow)
                                                                             .WithSubquery
                                                                             .WhereNotExists(UnconfirmedRequestSubstituteSubQuery);
                            break;
                        case RequestState.Deleted:
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.EndFlow && requestStatusAlias.IsDeleted);
                            break;
                        case RequestState.Terminated:
                            Request requestChildsAlias = null;
                            var HasTerminateRequestSubQuery = QueryOver.Of<Request>(() => requestChildsAlias)
                                                                       .Where(() => requestChildsAlias.ID == requestAlias.ID)
                                                                       .Select(x => x.ID);
                            requestsQueryExpression = requestsQueryExpression.Where(() => requestStatusAlias.EndFlow && requestStatusAlias.IsDeleted)
                                                                             .WithSubquery
                                                                             .WhereExists(HasTerminateRequestSubQuery);
                            break;
                    }
                    requestsList = requestsQueryExpression.OrderBy(orderByPath)
                                                          .Desc
                                                          .Skip(pageIndex * pageSize)
                                                          .Take(pageSize)
                                                          .List<Request>();
                }
                else
                {
                    requestsList = CurrentNHSession.QueryOver<Request>(() => requestAlias).Left
                                                   .JoinAlias(() => requestAlias.RequestSubstitute, () => requestSubstituteAlias)
                                                   .JoinAlias(() => requestAlias.Precard, () => precardAlias)
                                                   .JoinAlias(() => precardAlias.TempList, () => precardTempAlias)
                                                   .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                   .JoinAlias(() => personAlias.ExtDepartment, () => departmentAlias)
                                                   .JoinAlias(() => departmentAlias.TempList, () => departmentTempAlias)
                                                   .Where(() => departmentTempAlias.OperationGUID == departmentOperationGUID &&
                                                                 precardTempAlias.OperationGUID == precardOperationGUID &&
                                                                !personAlias.IsDeleted && personAlias.Active &&
                                                                (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                 personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                 personAlias.FullName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                 personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                 precardAlias.Name.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                 requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                ) &&
                                                               ((requestAlias.FromDate >= fromDate &&
                                                                 requestAlias.FromDate <= toDate) ||
                                                                (requestAlias.ToDate >= fromDate &&
                                                                 requestAlias.ToDate <= toDate))
                                                         )
                                                   .OrderBy(orderByPath)
                                                   .Desc
                                                   .Skip(pageIndex * pageSize)
                                                   .Take(pageSize)
                                                   .List<Request>();
                }

                if (departmentOperationGUID != string.Empty)
                    this.bTemp.DeleteTempList(departmentOperationGUID);
                if (precardOperationGUID != string.Empty)
                    this.bTemp.DeleteTempList(precardOperationGUID);

                IList<Request> RequestParentList = this.GetRequestParent(requestsList);
                foreach (Request requestItem in requestsList)
                {
                    KartablProxy proxy = new KartablProxy();
                    proxy = this.ConvertSpecialKartablRequestToProxy(requestItem, reqIndex, pageIndex, pageSize, RequestParentList);
                    kartablResult.Add(proxy);
                    reqIndex++;
                }
                SessionHelper.ClearSessionValue(SessionHelper.ISpecialKartablRequestsKey);
                SessionHelper.SaveSessionValue(SessionHelper.ISpecialKartablRequestsKey, requestsList.Select(x => x.ID).ToList<decimal>());
                return kartablResult;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllSpecialKartableRequests");
                throw ex;
            }

        }

        private KartablProxy ConvertSpecialKartablRequestToProxy(Request req, int reqIndex, int pageIndex, int pageSize, IList<Request> RequestParentList)
        {
            KartablProxy proxy = new KartablProxy();
            proxy.Row = pageIndex * pageSize + reqIndex;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(req.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.Parsi);
                proxy.ThePureFromDate = Utility.ToPersianDate(req.FromDate);
                proxy.ThePureToDate = Utility.ToPersianDate(req.ToDate);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(req.RegisterDate);
                proxy.TheFromDate = Utility.ToString(req.FromDate) + " " + Utility.GetDayName(req.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(req.ToDate) + " " + Utility.GetDayName(req.ToDate, LanguagesName.English);
                proxy.ThePureFromDate = Utility.ToString(req.FromDate);
                proxy.ThePureToDate = Utility.ToString(req.ToDate);
            }
            string registerRequestTime = " - " + Utility.IntTimeToRealTime((req.RegisterDate.Hour * 60) + req.RegisterDate.Minute);
            proxy.RegistrationDate += registerRequestTime;
            proxy.ID = req.ID;
            proxy.ParentID = req.Parent != null ? req.Parent.ID : 0;
            proxy.ChildsCount = req.RequestChildList.Count();
            proxy.RequestID = req.ID;
            proxy.ManagerFlowID = new BManager().GetManagerByUsername(this.workingUsername).ManagerFlowList.Where(x => x.Active).First().ID;

            proxy.TheFromTime = Utility.IntTimeToRealTime(req.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(req.ToTime);

            if (req.Parent == null)
            {
                if (!req.Precard.IsMonthly && !req.Precard.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                else
                    if (req.Precard.IsMonthly && req.Precard.IsDastootyOverwork)
                        proxy.TheDuration = req.TimeDuration.ToString();
                    else
                        if (req.Precard.IsDaily)
                            proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            }
            else
            {
                //اگر درخواست از نوع درخواست لغو باشد باید نوع پیشکارت را از درخواست پدر چک کند
                Request parent = req.Parent;
                if (!parent.Precard.IsMonthly && !parent.Precard.IsDaily)
                    proxy.TheDuration = Utility.IntTimeToTime(req.TimeDuration);
                else
                    if (parent.Precard.IsMonthly && parent.Precard.IsDastootyOverwork)
                        proxy.TheDuration = req.TimeDuration.ToString();
                    else
                        if (parent.Precard.IsDaily)
                            proxy.TheDuration = req.TimeDuration != -1000 ? req.TimeDuration.ToString() : req.FromDate > DateTime.MinValue && req.ToDate > DateTime.MinValue && req.ToDate >= req.FromDate ? (req.ToDate.Subtract(req.FromDate).Days + 1).ToString() : "0";
            }

            proxy.RequestTitle = req.Precard.Name;
            proxy.Description = req.Description;
            proxy.Applicant = req.Person.FirstName + " " + req.Person.LastName;
            proxy.Barcode = req.Person.BarCode;
            proxy.OperatorUser = req.OperatorUser;
            proxy.RequestSource = RequestSource.Undermanagment;
            proxy.PersonId = req.Person.ID;
            string name = req.Precard.PrecardGroup.LookupKey;
            PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
            if (groupName == PrecardGroupsName.overwork)
            {
                proxy.RequestType = RequestType.OverWork;

                //تنظیم زمان ابتدا و انتها
                //درخواست بازه ای بدون انتدا و انتها
                if (req.TimeDuration > 0 && req.FromTime == 1439 && req.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else if (groupName == PrecardGroupsName.imperative)
            {
                proxy.RequestType = RequestType.Imperative;
            }
            else
            {
                if (req.Parent != null)
                {
                    Request request = RequestParentList.Where(x => x.ID == req.Parent.ID).FirstOrDefault();
                    if (request != null && request.Precard.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (request != null && request.Precard.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (request != null && request.Precard.IsMonthly)
                    {
                        proxy.RequestType = RequestType.Monthly;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }
                }
                else
                {
                    if (req.Precard.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (req.Precard.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (req.Precard.IsMonthly)
                    {
                        proxy.RequestType = RequestType.Monthly;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }

                }
            }

            if (req.RequestStatusList != null && req.RequestStatusList.Count > 0)
            {
                if (req.RequestStatusList.Any(x => x.EndFlow && x.IsDeleted))
                {
                    proxy.FlowStatus = RequestState.Deleted;
                }
                else if (req.RequestStatusList.Any(x => x.EndFlow && !x.Confirm) || (req.RequestSubstitute != null && req.RequestSubstitute.Confirmed.HasValue && !req.RequestSubstitute.Confirmed.Value && req.RequestStatusList.Count == 0))
                {
                    proxy.FlowStatus = RequestState.Unconfirmed;
                }
                else if (req.RequestStatusList.Any(x => x.EndFlow && x.Confirm))
                {
                    proxy.FlowStatus = RequestState.Confirmed;
                }
                else if (req.RequestSubstitute != null && req.RequestSubstitute.Confirmed.HasValue && !req.RequestSubstitute.Confirmed.Value)
                {
                    proxy.FlowStatus = RequestState.Unconfirmed;
                }
                else
                    proxy.FlowStatus = RequestState.UnderReview;
            }
            else
            {
                if (!this.permitBusiness.CheckPermitPairExistanceByRequest(proxy.RequestID))
                {
                    if (req.RequestSubstitute != null && req.RequestSubstitute.Confirmed.HasValue && !req.RequestSubstitute.Confirmed.Value)
                        proxy.FlowStatus = RequestState.Unconfirmed;
                    else
                        proxy.FlowStatus = RequestState.UnderReview;
                }
                else
                    proxy.FlowStatus = RequestState.Confirmed;
            }


            proxy.AttachmentFile = req.AttachmentFile;
            proxy.PersonImage = req.Person.PersonDetail.Image;
            proxy.IsEdited = req.IsEdited;

            if (req.RequestSubstitute != null)
            {
                proxy.RequestSubstituteID = req.RequestSubstitute.ID;
                if (req.RequestSubstitute.Confirmed.HasValue)
                    proxy.RequestSubstituteConfirm = req.RequestSubstitute.Confirmed.Value;
            }
            proxy.DepartmentId = req.Person.Department.ID;
            proxy.DepartmentName = req.Person.Department.Name;
            return proxy;
        }

        private KartablProxy ConvertRequestSubstituteKartablRequestToProxy(RequestSubstitute reqSubstitute, int reqIndex, int pageIndex, int pageSize)
        {
            KartablProxy proxy = new KartablProxy();
            proxy.Row = pageIndex * pageSize + reqIndex;
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                proxy.RegistrationDate = Utility.ToPersianDate(reqSubstitute.Request.RegisterDate);
                proxy.TheFromDate = Utility.ToPersianDate(reqSubstitute.Request.FromDate) + " " + Utility.GetDayName(reqSubstitute.Request.FromDate, LanguagesName.Parsi);
                proxy.TheToDate = Utility.ToPersianDate(reqSubstitute.Request.ToDate) + " " + Utility.GetDayName(reqSubstitute.Request.ToDate, LanguagesName.Parsi);
            }
            else
            {
                proxy.RegistrationDate = Utility.ToString(reqSubstitute.Request.RegisterDate);
                proxy.TheFromDate = Utility.ToString(reqSubstitute.Request.FromDate) + " " + Utility.GetDayName(reqSubstitute.Request.FromDate, LanguagesName.English);
                proxy.TheToDate = Utility.ToString(reqSubstitute.Request.ToDate) + " " + Utility.GetDayName(reqSubstitute.Request.ToDate, LanguagesName.English);
            }
            string registerRequestTime = " - " + Utility.IntTimeToRealTime((reqSubstitute.Request.RegisterDate.Hour * 60) + reqSubstitute.Request.RegisterDate.Minute);
            proxy.RegistrationDate += registerRequestTime;
            proxy.ID = reqSubstitute.ID;
            proxy.RequestID = reqSubstitute.Request.ID;

            proxy.TheFromTime = Utility.IntTimeToRealTime(reqSubstitute.Request.FromTime);
            proxy.TheToTime = Utility.IntTimeToRealTime(reqSubstitute.Request.ToTime);
            if (!reqSubstitute.Request.Precard.IsMonthly && !reqSubstitute.Request.Precard.IsDaily)
                proxy.TheDuration = Utility.IntTimeToTime(reqSubstitute.Request.TimeDuration);
            else
                if (reqSubstitute.Request.Precard.IsMonthly && reqSubstitute.Request.Precard.IsDastootyOverwork)
                    proxy.TheDuration = reqSubstitute.Request.TimeDuration.ToString();
                else
                    if (reqSubstitute.Request.Precard.IsDaily)
                        proxy.TheDuration = reqSubstitute.Request.TimeDuration != -1000 ? reqSubstitute.Request.TimeDuration.ToString() : reqSubstitute.Request.FromDate > DateTime.MinValue && reqSubstitute.Request.ToDate > DateTime.MinValue && reqSubstitute.Request.ToDate >= reqSubstitute.Request.FromDate ? (reqSubstitute.Request.ToDate.Subtract(reqSubstitute.Request.FromDate).Days + 1).ToString() : "0";

            proxy.RequestTitle = reqSubstitute.Request.Precard.Name;
            proxy.Description = reqSubstitute.Description + " - " + reqSubstitute.Request.Description;
            proxy.Applicant = reqSubstitute.Request.Person.FirstName + " " + reqSubstitute.Request.Person.LastName;
            proxy.Barcode = reqSubstitute.Request.Person.BarCode;
            proxy.OperatorUser = reqSubstitute.Request.OperatorUser;
            proxy.PersonId = reqSubstitute.Request.Person.ID;
            proxy.RequestSubstituteID = reqSubstitute.SubstitutePerson.ID;
            proxy.RequestSubstituteConfirm = reqSubstitute.Confirmed;
            string name = reqSubstitute.Request.Precard.PrecardGroup.LookupKey;
            PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
            if (groupName == PrecardGroupsName.overwork)
            {
                proxy.RequestType = RequestType.OverWork;

                if (reqSubstitute.Request.TimeDuration > 0 && reqSubstitute.Request.FromTime == 1439 && reqSubstitute.Request.ToTime == 1439)
                {
                    proxy.TheFromTime = proxy.TheToTime = "";
                }
            }
            else if (groupName == PrecardGroupsName.imperative)
            {
                proxy.RequestType = RequestType.Imperative;
            }
            else if (reqSubstitute.Request.Precard.IsHourly)
            {
                proxy.RequestType = RequestType.Hourly;
            }
            else if (reqSubstitute.Request.Precard.IsDaily)
            {
                proxy.RequestType = RequestType.Daily;
            }
            else if (reqSubstitute.Request.Precard.IsMonthly)
            {
                proxy.RequestType = RequestType.Monthly;
            }
            else
            {
                proxy.RequestType = RequestType.None;
            }

            switch (reqSubstitute.Confirmed)
            {
                case true:
                    proxy.FlowStatus = RequestState.Confirmed;
                    break;
                case false:
                    proxy.FlowStatus = RequestState.Unconfirmed;
                    break;
                default:
                    proxy.FlowStatus = RequestState.UnderReview;
                    break;
            }
            proxy.DepartmentId = reqSubstitute.Request.Person.Department.ID;
            proxy.DepartmentName = reqSubstitute.Request.Person.Department.Name;
            return proxy;
        }


        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSpecialKartableLoadAccess()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRequestRgisterLoadAccess_onOperatorPermit()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleHourlyRequestByOperatorPermit(Request request, int year, int month, decimal personnelID, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            //DNN Note: Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequestByOperatorPermit(request, year, month, personnelID, out requestValidationProxyList, applicatorID);
            return (this as IRegisteredRequests).InsertRequestByOperatorPermit(request, year, month, personnelID, out requestValidationProxyList, applicatorID);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleRequestByPermitByService(Request request, decimal personnelID, ManagerFlow managerFlow)
        {
            //DNN Note: Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequestByPermitByService(request, personnelID, managerFlow);
            return (this as IRegisteredRequests).InsertRequestByPermitByService(request, personnelID, managerFlow);
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleDailyRequestByOperatorPermit(Request request, int year, int month, decimal personnelID, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            //DNN Note: Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequestByOperatorPermit(request, year, month, personnelID, out requestValidationProxyList, applicatorID);
            return (this as IRegisteredRequests).InsertRequestByOperatorPermit(request, year, month, personnelID, out requestValidationProxyList, applicatorID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public int InsertSingleOverTimeRequestByOperatorPermit(Request request, int year, int month, decimal personnelID, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            //DNN Note: Improve Performance
            //return ((IRegisteredRequests)(new BKartabl())).InsertRequestByOperatorPermit(request, year, month, personnelID, out requestValidationProxyList, applicatorID);
            return (this as IRegisteredRequests).InsertRequestByOperatorPermit(request, year, month, personnelID, out requestValidationProxyList, applicatorID);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveHourlyRequestByOperatorPermit()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveDailyRequestByOperatorPermit()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void InsertCollectiveOverTimeRequestByOperatorPermit()
        {
        }

        public int InsertCollectiveRequestByOperatorPermit(Request request, IList<decimal> checkedPersons, int year, int month, out IList<RequestKartablValidationProxy> requestValidationProxyList, out int registReqeustFailedCount, decimal applicatorID)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount("", PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchMethodBaseByPage(0, count, "", PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where checkedPersons.Contains(o.ID)
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                        requestsList.Add(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Permit) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                    }
                }

                this.InsertPermitByOperatorPermit(managerFlow, requestsList, out requestValidationProxyList, applicatorID);

                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //END OF DNN Note
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
        }

        public int InsertCollectiveRequestByOperatorPermit(Request request, string quickSearch, IList<decimal> checkedPersons, int year, int month, out IList<RequestKartablValidationProxy> requestValidationProxyList, out int registReqeustFailedCount, decimal applicatorID)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(quickSearch, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchMethodBaseByPage(0, count, quickSearch, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where checkedPersons.Contains(o.ID)
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Perforamnce
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {


                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                        requestsList.Add(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Permit) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");


                    }
                }

                this.InsertPermitByOperatorPermit(managerFlow, requestsList, out requestValidationProxyList, applicatorID);
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }

        }

        public int InsertCollectiveRequestByOperatorPermit(Request request, PersonAdvanceSearchProxy proxy, IList<decimal> checkedPersons, int year, int month, out IList<RequestKartablValidationProxy> requestValidationProxyList, out int registReqeustFailedCount, decimal applicatorID)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        where checkedPersons.Contains(o.ID)
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {


                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                        requestsList.Add(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Permit) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");


                    }
                }

                this.InsertPermitByOperatorPermit(managerFlow, requestsList, out requestValidationProxyList, applicatorID);

                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
        }

        public int InsertCollectiveRequestByOperatorPermit(Request request, int year, int month, out IList<RequestKartablValidationProxy> requestValidationProxyList, out int registReqeustFailedCount, decimal applicatorID)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(string.Empty, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchMethodBaseByPage(0, count, string.Empty, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {


                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                        requestsList.Add(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Permit) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");


                    }
                }

                this.InsertPermitByOperatorPermit(managerFlow, requestsList, out requestValidationProxyList, applicatorID);
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //End of DNN Note
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
        }

        public int InsertCollectiveRequestByOperatorPermit(Request request, string quickSearch, int year, int month, out IList<RequestKartablValidationProxy> requestValidationProxyList, out int registReqeustFailedCount, decimal applicatorID)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInQuickSearchCount(quickSearch, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.QuickSearchMethodBaseByPage(0, count, quickSearch, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                        requestsList.Add(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Permit) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                    }
                }

                this.InsertPermitByOperatorPermit(managerFlow, requestsList, out requestValidationProxyList, applicatorID);
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //DNN Note:Improve Performance
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
        }
        public int InsertCollectiveRequestByOperatorPermit(Request request, PersonAdvanceSearchProxy proxy, int year, int month, out IList<RequestKartablValidationProxy> requestValidationProxyList, out int registReqeustFailedCount, decimal applicatorID)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                ISearchPerson searchTool = new BPerson();
                int count = searchTool.GetPersonInAdvanceSearchCount(proxy, PersonCategory.Operator_UnderManagment);
                IList<Person> list = searchTool.GetPersonInAdvanceSearch(proxy, 0, count, PersonCategory.Operator_UnderManagment);
                var l = from o in list
                        select o;
                list = l.ToList<Person>();
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                Request reqObject = new Request();
                registReqeustFailedCount = 0;
                foreach (Person prs in list)
                {
                    try
                    {
                        reqObject = (Request)request.Clone();
                        reqObject.IsDateSetByUser = true;
                        reqObject.Person = prs;
                        reqObject = this.bRequest.InsertRequest(reqObject);
                        requestsList.Add(reqObject);
                    }
                    catch (Exception ex)
                    {
                        registReqeustFailedCount++;
                        string errorMessage = string.Empty;
                        if (reqObject != null && reqObject.Person != null)
                            errorMessage = GetErrorTitleForInsertCollectiveRequest(reqObject, OperatorRegistType.Permit) + ex.Message;
                        Exception exception = new Exception(errorMessage);
                        BaseBusiness<Entity>.LogException(exception, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                    }
                }

                this.InsertPermitByOperatorPermit(managerFlow, requestsList, out requestValidationProxyList, applicatorID);
                //count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertCollectiveRequestByOperatorPermit");
                throw ex;
            }
        }
        int IRegisteredRequests.InsertRequestByOperatorPermit(Request request, int year, int month, decimal personId, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            try
            {
                if (!ValidatePrecardForCurrentUser(request))
                    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                request.IsDateSetByUser = true;
                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                request.Person = new Person() { ID = personId == 0 ? -1 : personId };
                request = this.bRequest.InsertRequest(request);

                requestsList.Add(request);
                this.InsertPermitByOperatorPermit(managerFlow, requestsList, out requestValidationProxyList, applicatorID);

                //int count = t.GetUserRequestCount(RequestState.UnKnown, year, month);
                int count = (this as IRegisteredRequests).GetUserRequestCount(RequestState.UnKnown, year, month);
                //End of DNN Note
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequestByOperatorPermit");
                throw ex;
            }
        }
        int IRegisteredRequests.InsertRequestByPermitByService(Request request, decimal personId, ManagerFlow managerFlow)
        {
            try
            {
                //if (!ValidatePrecardForCurrentUser(request))
                //    throw new IllegalServiceAccess("XSS Attack دسترسی غیر مجاز به پیشکارت", ExceptionSrc);

                // ManagerFlow managerFlow = this.InsertRequestByOperationPermitValidate();

                IList<Request> requestsList = new List<Request>();

                //request.IsDateSetByUser = true;

                //DNN Note:Improve Performance
                //IRegisteredRequests t = new BKartabl();
                //BRequest busRequest = new BRequest();
                request.Person = new Person() { ID = personId == 0 ? -1 : personId };
                request = this.bRequest.InsertRequestByUIValidate(request);

                requestsList.Add(request);
                this.InsertPermitByOperatorPermitService(managerFlow, requestsList);

                int count = 0;
                //= t.GetUserRequestCount(RequestState.UnKnown, year, month);
                return count;
            }
            catch (UIValidationExceptions ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequestByOperatorPermit");
                throw ex;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "IRegisteredRequests", "InsertRequestByOperatorPermit");
                throw ex;
            }
        }
        private ManagerFlow InsertRequestByOperationPermitValidate()
        {
            UIValidationExceptions exception = new UIValidationExceptions();
            if (!(new BManager().GetManager(BUser.CurrentUser.Person.ID).ID > 0))
                exception.Add(new ValidationException(ExceptionResourceKeys.CurrentUserIsNotManager, "کاربر جاری مدیر نمی باشد", ExceptionSrc));
            if (!this.IsCurrentUserOperator)
                exception.Add(new ValidationException(ExceptionResourceKeys.CurrentUserIsNotOperator, "کاربر جاری اپراتور نمی باشد", ExceptionSrc));

            NHibernate.ISession NHSession = NHibernateSessionManager.Instance.GetSession();
            ManagerFlow managerFlow = null;
            ManagerFlow managerFlowAlias = null;
            Flow flowAlias = null;
            Operator operatorAlias = null;
            GTS.Clock.Model.Temp.Temp managerFlowTempAlias = null;
            GTS.Clock.Model.Temp.Temp operatorTempAlias = null;
            IList<Operator> operatorList = new BOperator().GetOperator(BUser.CurrentUser.Person.ID).ToList<Operator>();
            IList<ManagerFlow> managerFlowList = new BManager().GetManager(BUser.CurrentUser.Person.ID).ManagerFlowList;

            if (managerFlowList != null && managerFlowList.Count > 0 && operatorList != null && operatorList.Count > 0)
            {
                string managerFlowOperationGUID = this.bTemp.InsertTempList(managerFlowList.Select(x => x.ID).ToList());
                string operatorOperationGUID = this.bTemp.InsertTempList(operatorList.Select(x => x.ID).ToList());


                var ManagerFlowLevelsSubQuery = QueryOver.Of<ManagerFlow>(() => managerFlowAlias)
                                                         .Where(() => managerFlowAlias.Active)
                                                         .And(() => managerFlowAlias.Flow.ID == flowAlias.ID)
                                                         .And(() => managerFlowAlias.Level > 1)
                                                         .Select(x => x.ID);

                managerFlow = NHSession.QueryOver<ManagerFlow>(() => managerFlowAlias)
                                       .JoinAlias(() => managerFlowAlias.Flow, () => flowAlias)
                                       .JoinAlias(() => managerFlowAlias.TempList, () => managerFlowTempAlias)
                                       .JoinAlias(() => flowAlias.OperatorList, () => operatorAlias)
                                       .JoinAlias(() => operatorAlias.TempList, () => operatorTempAlias)
                                       .Where(() => managerFlowAlias.Active &&
                                                    flowAlias.ActiveFlow &&
                                                    !flowAlias.IsDeleted &&
                                                    managerFlowAlias.Level == 1 &&
                                                    managerFlowTempAlias.OperationGUID == managerFlowOperationGUID &&
                                                    operatorTempAlias.OperationGUID == operatorOperationGUID
                                             )
                                       .WithSubquery
                                       .WhereNotExists(ManagerFlowLevelsSubQuery)
                                       .List<ManagerFlow>()
                                       .FirstOrDefault();

                this.bTemp.DeleteTempList(managerFlowOperationGUID);
                this.bTemp.DeleteTempList(operatorOperationGUID);

            }
            if (managerFlow == null)
                exception.Add(new ValidationException(ExceptionResourceKeys.ManagersCountInOperatorFlowIsGreaterThanOne, "جریانی که تنها مدیر آن اپراتور جاری باشد وجود ندارد", ExceptionSrc));
            if (exception.Count > 0)
                throw exception;

            return managerFlow;
        }

        private void InsertPermitByOperatorPermit(ManagerFlow managerFlow, IList<Request> requestsList, out IList<RequestKartablValidationProxy> requestValidationProxyList, decimal applicatorID)
        {
            requestValidationProxyList = new List<RequestKartablValidationProxy>();
            if (requestsList != null && requestsList.Count() > 0)
            {
                SessionHelper.ClearSessionValue(SessionHelper.IKartablRequestsKey);
                SessionHelper.SaveSessionValue(SessionHelper.IKartablRequestsKey, requestsList.Select(x => x.ID).ToList<decimal>());
                foreach (Request request in requestsList)
                {
                    string description = string.Empty;
                    string sexuality = string.Empty;
                    IList<KartableSetStatusProxy> requestProxyList = new List<KartableSetStatusProxy>();
                    KartableSetStatusProxy requestProxy = new KartableSetStatusProxy()
                    {
                        RequestID = request.ID,
                        ManagerFlowID = managerFlow.ID
                    };
                    requestProxyList.Add(requestProxy);
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Parsi:
                            switch (BUser.CurrentUser.Person.Sex)
                            {
                                case PersonSex.Male:
                                    sexuality = "آقای ";
                                    break;
                                case PersonSex.Female:
                                    sexuality = "خانم ";
                                    break;
                            }
                            description = "ثبت مجوز توسط ";
                            break;
                        case LanguagesName.English:
                            switch (BUser.CurrentUser.Person.Sex)
                            {
                                case PersonSex.Male:
                                    sexuality = "Mr ";
                                    break;
                                case PersonSex.Female:
                                    sexuality = "Mrs ";
                                    break;
                            }
                            description = "Request Register By ";
                            break;
                    }
                    description += sexuality + BUser.CurrentUser.Person.Name;
                    //((IKartablRequests)(new BKartabl())).SetStatusOfRequest(requestProxyList, RequestState.Confirmed, description, out requestValidationProxyList, applicatorID);
                    //DNN Note:improve Performance
                    (this as IKartablRequests).SetStatusOfRequest(requestProxyList, RequestState.Confirmed, description, out requestValidationProxyList, applicatorID);
                    //End of DNN
                }
                SessionHelper.ClearSessionValue(SessionHelper.IKartablRequestsKey);
            }
        }
        private void InsertPermitByOperatorPermitService(ManagerFlow managerFlow, IList<Request> requestsList)
        {

            if (requestsList != null && requestsList.Count() > 0)
            {
                //SessionHelper.ClearSessionValue(SessionHelper.IKartablRequestsKey);
                //SessionHelper.SaveSessionValue(SessionHelper.IKartablRequestsKey, requestsList.Select(x => x.ID).ToList<decimal>());
                foreach (Request request in requestsList)
                {
                    string description = string.Empty;
                    //string sexuality = string.Empty;
                    IList<KartableSetStatusProxy> requestProxyList = new List<KartableSetStatusProxy>();
                    KartableSetStatusProxy requestProxy = new KartableSetStatusProxy()
                    {
                        RequestID = request.ID,
                        ManagerFlowID = managerFlow.ID
                    };
                    requestProxyList.Add(requestProxy);
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Parsi:

                            description = "ثبت مجوز توسط سرویس ";
                            break;
                        case LanguagesName.English:

                            description = "Request Register By Service ";
                            break;
                    }
                    //description +=  managerFlow.Manager.Person.Name;
                    //((IKartablRequests)(new BKartabl())).SetStatusOfRequestByService(requestProxyList, RequestState.Confirmed, description);
                    (this as IKartablRequests).SetStatusOfRequestByService(requestProxyList, RequestState.Confirmed, description);
                }
                SessionHelper.ClearSessionValue(SessionHelper.IKartablRequestsKey);
            }
        }
        private bool CheckRequestValueAppliedFlowConditionValue(decimal managerFlowID, Request request)
        {
            bool isAppliedFlowConditionValue = false;
            ManagerFlowCondition managerFlowCondition = new ManagerFlowCondition();
            IList<ManagerFlowCondition> managerFlowConditionList = new List<ManagerFlowCondition>();
            Dictionary<decimal, IList<ManagerFlowCondition>> managerFlowConditionDic = new Dictionary<decimal, IList<ManagerFlowCondition>>();
            BManagerFlowCondition bManagerFlowCondition = new BManagerFlowCondition();
            if (!managerFlowConditionDic.Keys.Contains(managerFlowID))
            {
                managerFlowConditionList = bManagerFlowCondition.GetAllManagerFlowConditionsByManagerFlowID(managerFlowID);
                managerFlowConditionDic.Add(managerFlowID, managerFlowConditionList);
            }
            managerFlowConditionList = managerFlowConditionDic[managerFlowID];
            managerFlowCondition = managerFlowConditionList.Where(x => x.PrecardAccessGroupDetail.Precard.ID == request.Precard.ID && x.EndFlow).FirstOrDefault();
            if (managerFlowCondition != null)
            {
                ConditionOperators conditionOperator = (ConditionOperators)managerFlowCondition.Operator;
                Precard precard = managerFlowCondition.PrecardAccessGroupDetail.Precard;
                PrecardGroupsName precardGroupsName = (PrecardGroupsName)managerFlowCondition.PrecardAccessGroupDetail.Precard.PrecardGroup.IntLookupKey;
                int requestValue = 0;
                switch (precardGroupsName)
                {
                    case PrecardGroupsName.leave:
                    case PrecardGroupsName.leaveestelajy:
                    case PrecardGroupsName.duty:
                        if (precard.IsHourly)
                            requestValue = request.TimeDuration;
                        else
                            if (precard.IsDaily)
                                requestValue = (int)(request.ToDate - request.FromDate).TotalDays + 1;
                        if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, requestValue))
                            isAppliedFlowConditionValue = true;
                        break;
                    case PrecardGroupsName.overwork:
                        requestValue = ((int)(request.ToDate - request.FromDate).TotalDays + 1) * request.TimeDuration;
                        if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, requestValue))
                            isAppliedFlowConditionValue = true;
                        break;
                    case PrecardGroupsName.traffic:
                        if ((request.FromTime != -1000 && request.ToTime != 1000 && request.TimeDuration != 1000) || (request.FromTime != 1000 && request.ToTime != -1000 && request.TimeDuration != 1000))
                            if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, request.TimeDuration))
                                isAppliedFlowConditionValue = true;
                        break;
                    case PrecardGroupsName.imperative:
                        if (this.CheckRequestValueIsInRangeConditionValue(conditionOperator, managerFlowCondition.Value, request.TimeDuration))
                            isAppliedFlowConditionValue = true;
                        break;
                    default:
                        break;
                }
            }
            return isAppliedFlowConditionValue;
        }

        public bool CheckRequestValueIsInRangeConditionValue(ConditionOperators conditionOperators, string conditionValue, int requestValue)
        {
            bool isInRange = false;
            int conditionVal = 0;
            switch (conditionOperators)
            {
                case ConditionOperators.Equal:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue == conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.NotEqual:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue != conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.GreaterThan:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue > conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.GreaterThanOrEqual:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue >= conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.LessThan:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue < conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.LessThanOrEqual:
                    conditionVal = int.Parse(conditionValue, CultureInfo.InvariantCulture);
                    if (requestValue <= conditionVal || conditionVal == 0)
                        isInRange = true;
                    break;
                case ConditionOperators.Between:
                    string[] conditionValueParts = conditionValue.Split(new char[] { ',' });
                    if (conditionValueParts.Length == 2 && (requestValue >= int.Parse(conditionValueParts[0], CultureInfo.InvariantCulture) && requestValue <= int.Parse(conditionValueParts[1], CultureInfo.InvariantCulture)) || (int.Parse(conditionValueParts[0], CultureInfo.InvariantCulture) == 0 && (int.Parse(conditionValueParts[1], CultureInfo.InvariantCulture) == 0)))
                        isInRange = true;
                    break;
                default:
                    break;
            }

            return isInRange;
        }

        public IList<RequestHistory> GetRequestHistoryByRequestID(decimal requestId)
        {
            IList<RequestHistory> RequestHistoryList = new List<RequestHistory>();
            try
            {
                Request requestObj = bRequest.GetRequestByID(requestId);
                RequestHistoryList = requestObj.RequestHistoryList;


                foreach (RequestHistory item in RequestHistoryList)
                {
                    switch (BLanguage.CurrentSystemLanguage)
                    {
                        case LanguagesName.Unknown:
                            break;
                        case LanguagesName.Parsi:
                            item.TheFromDate = Utility.ToPersianDate(item.FromDate);
                            item.TheToDate = Utility.ToPersianDate(item.ToDate);
                            break;
                        case LanguagesName.English:
                            item.TheFromDate = item.FromDate.ToShortDateString();
                            item.TheToDate = item.ToDate.ToShortDateString();
                            break;
                        default:
                            break;
                    }
                    item.TheFromTime = Utility.IntTimeToRealTime(item.FromTime);
                    item.TheToTime = Utility.IntTimeToRealTime(item.ToTime);
                    if (item.FromTime == -1000 && item.ToTime == -1000)
                    {
                        item.TheDuration = item.Duration.ToString();
                    }
                    else
                    {
                        item.TheDuration = Utility.IntTimeToRealTime(item.Duration);
                        if (item.Duration > 0 && item.FromTime == 1439 && item.ToTime == 1439)
                        {
                            item.TheFromTime = item.TheToTime = "";
                        }
                    }




                    item.PrecardName = requestObj.Precard.Name;

                }

                return RequestHistoryList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestHistoryByRequestID");
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateRequestByManager_onKartable(RequestHistory requestHistory)
        {
            UpdateRequestByManager(requestHistory);
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void UpdateRequestByManager_onSpecialKartable(RequestHistory requestHistory)
        {
            UpdateRequestByManager(requestHistory);
        }
        private void UpdateRequestByManager(RequestHistory requestHistory)
        {
            try
            {
                if (requestHistory.RequestType == RequestType.Daily || requestHistory.RequestType == RequestType.Hourly || requestHistory.RequestType == RequestType.OverWork)
                {
                    //DNN Note:Improve Performance
                    //BKartabl bkartable = new BKartabl();
                    decimal requestID = requestHistory.Request.ID;
                    Request requestObj = bRequest.GetRequestByID(requestID);

                    //RequestHistory requestHistoryOrginalObj = bkartable.GetRequestHistoryByRequestID(requestHistory.Request.ID).FirstOrDefault();
                    RequestHistory requestHistoryOrginalObj = this.GetRequestHistoryByRequestID(requestHistory.Request.ID).FirstOrDefault();
                    //End of DNN
                    EntityRepository<RequestHistory> requestHistoryRep = new EntityRepository<RequestHistory>();

                    using (NHibernateSessionManager.Instance.BeginTransactionOn())
                    {

                        requestHistory.FromDate = requestObj.FromDate;
                        requestHistory.ToDate = requestObj.ToDate;
                        requestHistory.FromTime = requestObj.FromTime;
                        requestHistory.ToTime = requestObj.ToTime;
                        requestHistory.Duration = requestObj.TimeDuration;

                        requestHistory.AttachmentFile = requestObj.AttachmentFile;
                        switch (BLanguage.CurrentSystemLanguage)
                        {
                            case LanguagesName.Unknown:
                                break;
                            case LanguagesName.Parsi:

                                requestObj.FromDate = Utility.ToMildiDate(requestHistory.TheFromDate);
                                requestObj.ToDate = Utility.ToMildiDate(requestHistory.TheToDate);

                                break;
                            case LanguagesName.English:
                                requestObj.FromDate = DateTime.Parse(requestHistory.TheFromDate);
                                requestObj.ToDate = DateTime.Parse(requestHistory.TheToDate);

                                break;
                            default:
                                requestObj.FromDate = DateTime.Parse(requestHistory.TheFromDate);
                                requestObj.ToDate = DateTime.Parse(requestHistory.TheToDate);
                                requestHistory.FromDate = DateTime.Parse(requestHistory.TheFromDate);
                                requestHistory.ToDate = DateTime.Parse(requestHistory.TheToDate);
                                break;
                        }
                        requestObj.FromTime = Utility.RealTimeToIntTime(requestHistory.TheFromTime);
                        requestObj.ToTime = Utility.RealTimeToIntTime(requestHistory.TheToTime);
                        requestObj.TimeDuration = Utility.RealTimeToIntTime(requestHistory.TheDuration);
                        requestObj.IsEdited = true;
                        requestObj.ContinueOnTomorrow = requestHistory.ContinueOnTomorrow;
                        requestObj.AllOnTomorrow = requestHistory.AllOnTomorrow;
                        if (requestHistory.NewAttachmentFile == null || requestHistory.NewAttachmentFile == string.Empty)
                            requestObj.AttachmentFile = requestHistory.AttachmentFile;
                        else
                            requestObj.AttachmentFile = requestHistory.NewAttachmentFile;
                        bool requestIsPermit = false;
                        foreach (RequestStatus item in requestObj.RequestStatusList)
                        {
                            if (item.EndFlow == true)
                                requestIsPermit = true;
                        }
                        if (requestIsPermit == true)
                            throw new ValidationException(ExceptionResourceKeys.EditRequestNotAllowed, " درخواست قابل ویرایش نمی باشد.", ExceptionSrc);

                        PrecardGroupsName pgn = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), requestObj.Precard.PrecardGroup.LookupKey);
                        if (pgn == PrecardGroupsName.traffic)
                            requestObj.AllOnTomorrow = false;

                        bRequest.SaveChanges(requestObj, UIActionType.EDIT);
                        requestHistory.Request = requestObj;
                        if (requestHistoryOrginalObj == null)
                            requestHistoryRep.WithoutTransactSave(requestHistory);
                    }
                }
                else
                {
                    throw new ValidationException(ExceptionResourceKeys.EditRequestNotAllowed, " درخواست قابل ویرایش نمی باشد.", ExceptionSrc);
                }

            }
            catch (Exception ex)
            {
                NHibernateSessionManager.Instance.RollbackTransactionOn();
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "UpdateRequestByManager");
                throw ex;
            }
        }
        public KartableGridClientSettings GetKartableGridClientSettings(GridSettingCaller Caller)
        {
            try
            {
                User user = userRep.GetById(BUser.CurrentUser.ID, false);
                decimal workingUserSettingsId = user.UserSetting.ID;
                KartableGridClientSettings kartablGridClientSetting = NHSession.Query<KartableGridClientSettings>()
                                                                               .Where(x => x.UserSetting.ID == workingUserSettingsId && x.Type == Caller)
                                                                               .SingleOrDefault();
                return kartablGridClientSetting;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetCulomnGridKartabl");
                throw ex;
            }
        }
        public void CheckNextDayHourlyRequestLoadAccess_RequestRegister_onNormalUser()
        {
        }

        public void CheckNextDayHourlyRequestLoadAccess_RequestRegister_onOperator()
        {
        }

        public void CheckNextDayHourlyRequestLoadAccess_RequestRegister_onOperatorPermit()
        {
        }

        public void CheckAllNextDayHourlyRequestLoadAccess_RequestRegister_onNormalUser()
        {
        }

        public void CheckAllNextDayHourlyRequestLoadAccess_RequestRegister_onOperator()
        {
        }

        public void CheckAllNextDayHourlyRequestLoadAccess_RequestRegister_onOperatorPermit()
        {
        }

        public void CheckNextDayOvertimeRequestLoadAccess_RequestRegister_onNormalUser()
        {
        }

        public void CheckNextDayOvertimeRequestLoadAccess_RequestRegister_onOperator()
        {
        }

        public void CheckNextDayOvertimeRequestLoadAccess_RequestRegister_onOperatorPermit()
        {
        }

        public void CheckAllNextDayOvertimeRequestLoadAccess_RequestRegister_onNormalUser()
        {
        }

        public void CheckAllNextDayOvertimeRequestLoadAccess_RequestRegister_onOperator()
        {
        }

        public void CheckAllNextDayOvertimeRequestLoadAccess_RequestRegister_onOperatorPermit()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckNextDayHourlyRequestLoadAccess_RequestHistory_onKartable()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckNextDayHourlyRequestLoadAccess_RequestHistory_onSpecialKartable()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckNextDayOvertimeRequestLoadAccess_RequestHistory_onKartable()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckNextDayOvertimeRequestLoadAccess_RequestHistory_onSpecialKartable()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAllNextDayHourlyRequestLoadAccess_RequestHistory_onKartable()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAllNextDayHourlyRequestLoadAccess_RequestHistory_onSpecialKartable()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAllNextDayOvertimeRequestLoadAccess_RequestHistory_onKartable()
        {
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckAllNextDayOvertimeRequestLoadAccess_RequestHistory_onSpecialKartable()
        {
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckKartablSettingLoadAccess()
        {
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSpecialKartableSettingLoadAccess()
        {
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSurveySettingLoadAccess()
        {
        }
        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckRequestSubstituteKartableSettingLoadAccess()
        {
        }
        private string GetErrorTitleForInsertCollectiveRequest(Request request, OperatorRegistType registType)
        {
            string message = string.Empty;
            string precardName = new BPrecard().GetByID(request.Precard.ID).Name;
            string registTypeStr = string.Empty;
            switch (registType)
            {
                case OperatorRegistType.Request:
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Unknown:
                            break;
                        case LanguagesName.Parsi:
                            registTypeStr = "درخواست";
                            break;
                        case LanguagesName.English:
                            registTypeStr = "Request";
                            break;
                        default:
                            break;
                    }
                    break;
                case OperatorRegistType.Permit:
                    switch (BLanguage.CurrentLocalLanguage)
                    {
                        case LanguagesName.Unknown:
                            break;
                        case LanguagesName.Parsi:
                            registTypeStr = "مجوز";
                            break;
                        case LanguagesName.English:
                            registTypeStr = "Permit";
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (BLanguage.CurrentLocalLanguage)
            {
                case LanguagesName.Unknown:
                    message = " خطا در ثبت " + registTypeStr + " انبوه - " + "(" + "شماره پرسنلی : " + request.Person.BarCode + " --- " + "نام و نام خانوادگی : " + request.Person.Name + " --- " + "نوع درخواست : " + precardName + " --- " + "تاریخ : " + request.TheFromDate + ") --- ";
                    break;
                case LanguagesName.Parsi:
                    message = " خطا در ثبت " + registTypeStr + " انبوه - " + "(" + "شماره پرسنلی : " + request.Person.BarCode + " --- " + "نام و نام خانوادگی : " + request.Person.Name + " --- " + "نوع درخواست : " + precardName + " --- " + "تاریخ : " + request.TheFromDate + ") --- ";
                    break;
                case LanguagesName.English:
                    message = "error in regist " + registTypeStr + " collective - " + "(" + "Barcode : " + request.Person.BarCode + " --- " + "Name : " + request.Person.Name + " --- " + "Precard Name : " + precardName + " --- " + "Date : " + request.TheFromDate + ") --- ";

                    break;
                default:
                    break;
            }

            return message;
        }

        private void LogCollectiveRequest(Request request, int personnelCount)
        {
            try
            {
                Precard precard = new BPrecard().GetByID(request.Precard.ID);
                NHSession.Evict(precard);
                string action = "Insert Collective Request by " + BUser.CurrentUser.Person.Name;
                string objectInfo = string.Empty;
                switch (BLanguage.CurrentSystemLanguage)
                {
                    case LanguagesName.Parsi:
                        objectInfo = "درج درخواست انبوه با پیشکارت " + precard.Name + " برای " + personnelCount + " نفر بوسیله " + BUser.CurrentUser.Person.Name;
                        break;
                    case LanguagesName.English:
                        objectInfo = "Insert Collective Request with " + precard.Name + " Precard for " + personnelCount + " Personnel by " + BUser.CurrentUser.Person.Name;
                        break;
                }
                BSystemReports bSystemReports = new BSystemReports();
                bSystemReports.InsertSystemReportCurrentUserActivity(new System.Web.UI.Page(), action, objectInfo);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "LogCollectiveRequest");
                throw;
            }
        }

        public void CheckRequestSubstituteKartableLoadAccess()
        {
        }

        public int GetAllRequestSubstituteKartableRequestsCount(RequestState requestState, int year, int month, string searchKey)
        {
            try
            {
                NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
                BUser bUser = new BUser();
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                int count = 0;
                Nullable<bool> confirmed = null;
                RequestSubstitute requestSubstituteAlias = null;
                Request requestAlias = null;
                Person personAlias = null;
                Person requestSubstitutePersonAlias = null;
                NHibernate.IQueryOver<RequestSubstitute, RequestSubstitute> requestSubstituteQueryExpression = null;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                    fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                    toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                }
                else
                {
                    int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                    fromDate = new DateTime(year, month, 1);
                    toDate = new DateTime(year, month, endOfMonth);
                }

                requestSubstituteQueryExpression = CurrentNHSession.QueryOver<RequestSubstitute>(() => requestSubstituteAlias)
                                                                   .JoinAlias(() => requestSubstituteAlias.SubstitutePerson, () => requestSubstitutePersonAlias)
                                                                   .JoinAlias(() => requestSubstituteAlias.Request, () => requestAlias)
                                                                   .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                                   .Where(() => requestSubstitutePersonAlias.ID == BUser.CurrentUser.Person.ID &&
                                                                               !personAlias.IsDeleted && personAlias.Active &&
                                                                               (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                                personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                                personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                                requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                               ) &&
                                                                              ((requestAlias.FromDate >= fromDate &&
                                                                                requestAlias.FromDate <= toDate) ||
                                                                               (requestAlias.ToDate >= fromDate &&
                                                                                requestAlias.ToDate <= toDate)
                                                                              )
                                                                         );

                switch (requestState)
                {
                    case RequestState.Confirmed:
                        confirmed = true;
                        break;
                    case RequestState.Unconfirmed:
                        confirmed = false;
                        break;
                }

                if (requestState != RequestState.UnKnown)
                    count = requestSubstituteQueryExpression.Where(x => x.Confirmed == confirmed)
                                                            .RowCount();
                else
                    count = requestSubstituteQueryExpression.RowCount();

                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequestSubstituteKartableCount");
                throw ex;
            }
        }
        public int GetAllRequestSubstituteKartableRequestsCount(RequestState requestState, int year, int month, string searchKey, ViewState viewState, string FromDate, string ToDate)
        {
            try
            {
                NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
                UIValidationExceptions exception = new UIValidationExceptions();
                BUser bUser = new BUser();
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                int count = 0;
                Nullable<bool> confirmed = null;
                RequestSubstitute requestSubstituteAlias = null;
                Request requestAlias = null;
                Person personAlias = null;
                Person requestSubstitutePersonAlias = null;
                NHibernate.IQueryOver<RequestSubstitute, RequestSubstitute> requestSubstituteQueryExpression = null;
                switch (viewState)
                {
                    case ViewState.YearMonth:
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                            fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                            toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                        }
                        else
                        {
                            int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                            fromDate = new DateTime(year, month, 1);
                            toDate = new DateTime(year, month, endOfMonth);
                        }

                        break;
                    case ViewState.Date:
                        if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                        {
                            fromDate = Utility.ToMildiDate(FromDate);
                            toDate = Utility.ToMildiDate(ToDate);
                        }
                        else
                        {
                            fromDate = Utility.ToMildiDateTime(FromDate);
                            toDate = Utility.ToMildiDateTime(ToDate);
                        }
                        if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                        {
                            exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                        }
                        if (exception.Count > 0)
                        {
                            throw exception;
                        }
                        break;
                }

                requestSubstituteQueryExpression = CurrentNHSession.QueryOver<RequestSubstitute>(() => requestSubstituteAlias)
                                                                   .JoinAlias(() => requestSubstituteAlias.SubstitutePerson, () => requestSubstitutePersonAlias)
                                                                   .JoinAlias(() => requestSubstituteAlias.Request, () => requestAlias)
                                                                   .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                                   .Where(() => requestSubstitutePersonAlias.ID == BUser.CurrentUser.Person.ID &&
                                                                               !personAlias.IsDeleted && personAlias.Active &&
                                                                               (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                                personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                                personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                                requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                               ) &&
                                                                              ((requestAlias.FromDate >= fromDate &&
                                                                                requestAlias.FromDate <= toDate) ||
                                                                               (requestAlias.ToDate >= fromDate &&
                                                                                requestAlias.ToDate <= toDate)
                                                                              )
                                                                         );

                switch (requestState)
                {
                    case RequestState.Confirmed:
                        confirmed = true;
                        break;
                    case RequestState.Unconfirmed:
                        confirmed = false;
                        break;
                }

                if (requestState != RequestState.UnKnown)
                    count = requestSubstituteQueryExpression.Where(x => x.Confirmed == confirmed)
                                                            .RowCount();
                else
                    count = requestSubstituteQueryExpression.RowCount();

                return count;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetAllRequestSubstituteKartableCount");
                throw ex;
            }
        }

        public IList<KartablProxy> GetAllRequestSubstituteKartableRequests(RequestState requestState, int year, int month, int pageIndex, int pageSize, string searchKey, KartablOrderBy orderby, ViewState viewState, string FromDate, string ToDate)
        {
            NHibernate.ISession CurrentNHSession = NHibernateSessionManager.Instance.GetSession();
            UIValidationExceptions exception = new UIValidationExceptions();
            BUser bUser = new BUser();
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now; ;
            Nullable<bool> confirmed = null;
            RequestSubstitute requestSubstituteAlias = null;
            Request requestAlias = null;
            Person personAlias = null;
            Person requestSubstitutePersonAlias = null;
            IList<RequestSubstitute> requestSubstituteList = null;
            int reqSubstituteIndex = 1;
            NHibernate.IQueryOver<RequestSubstitute, RequestSubstitute> requestSubstituteQueryExpression = null;
            System.Linq.Expressions.Expression<Func<object>> orderByPath = null;
            switch (viewState)
            {
                case ViewState.YearMonth:
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        int endOfMonth = Utility.GetEndOfPersianMonth(year, month);
                        fromDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, 1));
                        toDate = Utility.ToMildiDate(String.Format("{0}/{1}/{2}", year, month, endOfMonth));
                    }
                    else
                    {
                        int endOfMonth = Utility.GetEndOfMiladiMonth(year, month);
                        fromDate = new DateTime(year, month, 1);
                        toDate = new DateTime(year, month, endOfMonth);
                    }

                    break;
                case ViewState.Date:
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        fromDate = Utility.ToMildiDate(FromDate);
                        toDate = Utility.ToMildiDate(ToDate);
                    }
                    else
                    {
                        fromDate = Utility.ToMildiDateTime(FromDate);
                        toDate = Utility.ToMildiDateTime(ToDate);
                    }
                    if (fromDate > toDate || fromDate == Utility.GTSMinStandardDateTime || toDate == Utility.GTSMinStandardDateTime)
                    {
                        exception.Add(new ValidationException(ExceptionResourceKeys.DateNotValid, "مقدار تاریخ ابتدا یا انتها معتبر نمی باشد", ExceptionSrc));
                    }
                    if (exception.Count > 0)
                    {
                        throw exception;
                    }
                    break;
            }

            IList<KartablProxy> kartablResult = new List<KartablProxy>();
            IList<InfoRequest> result = new List<InfoRequest>();

            switch (orderby)
            {
                case KartablOrderBy.PersonCode:
                    orderByPath = () => personAlias.BarCode;
                    break;
                case KartablOrderBy.PersonName:
                    orderByPath = () => personAlias.FirstName;
                    break;
                case KartablOrderBy.RegisteredBy:
                    orderByPath = () => requestAlias.OperatorUser;
                    break;
                case KartablOrderBy.RequestDate:
                    orderByPath = () => requestAlias.FromDate;
                    break;
                case KartablOrderBy.None:
                    orderByPath = () => requestAlias.ID;
                    break;
            }

            switch (requestState)
            {
                case RequestState.Confirmed:
                    confirmed = true;
                    break;
                case RequestState.Unconfirmed:
                    confirmed = false;
                    break;
            }

            requestSubstituteQueryExpression = CurrentNHSession.QueryOver<RequestSubstitute>(() => requestSubstituteAlias)
                                                               .JoinAlias(() => requestSubstituteAlias.SubstitutePerson, () => requestSubstitutePersonAlias)
                                                               .JoinAlias(() => requestSubstituteAlias.Request, () => requestAlias)
                                                               .JoinAlias(() => requestAlias.Person, () => personAlias)
                                                               .Where(() => requestSubstitutePersonAlias.ID == BUser.CurrentUser.Person.ID &&
                                                                           !personAlias.IsDeleted && personAlias.Active &&
                                                                           (personAlias.FirstName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.LastName.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            personAlias.BarCode.IsInsensitiveLike(searchKey, MatchMode.Anywhere) ||
                                                                            requestAlias.Description.IsInsensitiveLike(searchKey, MatchMode.Anywhere)
                                                                           ) &&
                                                                          ((requestAlias.FromDate >= fromDate &&
                                                                            requestAlias.FromDate <= toDate) ||
                                                                           (requestAlias.ToDate >= fromDate &&
                                                                            requestAlias.ToDate <= toDate)
                                                                          )
                                                                     );

            if (requestState != RequestState.UnKnown)
                requestSubstituteQueryExpression = requestSubstituteQueryExpression.Where(x => x.Confirmed == confirmed);

            requestSubstituteList = requestSubstituteQueryExpression.OrderBy(orderByPath)
                                                                    .Desc
                                                                    .Skip(pageIndex * pageSize)
                                                                    .Take(pageSize)
                                                                    .List<RequestSubstitute>();
            foreach (RequestSubstitute requestSubstituteItem in requestSubstituteList)
            {
                KartablProxy proxy = new KartablProxy();
                proxy = this.ConvertRequestSubstituteKartablRequestToProxy(requestSubstituteItem, reqSubstituteIndex, pageIndex, pageSize);
                kartablResult.Add(proxy);
                reqSubstituteIndex++;
            }
            SessionHelper.ClearSessionValue(SessionHelper.IRequestSubstituteKartableRequests);
            SessionHelper.SaveSessionValue(SessionHelper.IRequestSubstituteKartableRequests, requestSubstituteList.Select(x => x.ID).ToList<decimal>());
            return kartablResult;


        }

        public bool ConfirmRequestSubstituteRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList)
        {
            return SetStatusOfRequestSubstituteRequest(requests, status, description, out requestValidationProxyList);
        }

        public bool UnconfirmRequestSubstituteRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList)
        {
            return SetStatusOfRequestSubstituteRequest(requests, status, description, out requestValidationProxyList);
        }

        public bool SetStatusOfRequestSubstituteRequest(IList<KartableSetStatusProxy> requests, RequestState status, string description, out IList<RequestKartablValidationProxy> requestValidationProxyList)
        {
            try
            {
                requestValidationProxyList = new List<RequestKartablValidationProxy>();
                UIValidationExceptions exception = new UIValidationExceptions();
                if (requests.Count == 0)
                {
                    exception.Add(new ValidationException(ExceptionResourceKeys.RequestRequired, "درخواست انتخاب نشده است", ExceptionSrc));
                    throw exception;
                }
                if (status != RequestState.Confirmed && status != RequestState.Unconfirmed)
                    return false;

                var ids = (IList<decimal>)SessionHelper.GetSessionValue(SessionHelper.IRequestSubstituteKartableRequests);
                foreach (KartableSetStatusProxy req in requests)
                {
                    if (req.RequestSubstituteID > 0 && !ids.Contains(req.RequestSubstituteID))
                    {
                        throw new IllegalServiceAccess("XSS Attack on SetStatusOfRequestSubstituteRequest", ExceptionSrc);
                    }
                }

                description = Utility.IsEmpty(description) ? "" : description;
                RequestKartablValidationProxy requestValidationProxy = null;
                RequestStatus requestStatusAlias = null;
                Request requestAlias = null;
                RequestSubstitute requestSubstitute = null;
                bool confirmed = false;
                BRequestSubstitute bRequestSubstitute = new BRequestSubstitute();

                switch (status)
                {
                    case RequestState.Confirmed:
                        confirmed = true;
                        break;
                    case RequestState.Unconfirmed:
                        confirmed = false;
                        break;
                }

                IList<decimal> list = requests.Select(x => x.RequestID).ToList<decimal>();

                IList<RequestStatus> requestStatusList = NHSession.QueryOver<RequestStatus>(() => requestStatusAlias)
                                                                  .JoinAlias(() => requestStatusAlias.Request, () => requestAlias)
                                                                  .Where(() => requestAlias.ID.IsIn(list.ToArray()))
                                                                  .List<RequestStatus>();

                //list = list.Except(list.Where(x => requestStatusList != null &&
                //                                   requestStatusList.Select(y => y.Request)
                //                                                    .ToList<Request>()
                //                                                    .Select(z => z.ID)
                //                                                    .Contains(x)
                //                             )
                //                       .ToList<decimal>()
                //                  )
                //           .ToList<decimal>();

                foreach (decimal reqId in list)
                {
                    using (NHibernateSessionManager.Instance.BeginTransactionOn())
                    {
                        try
                        {
                            decimal requestSubstituteID = 0;
                            bool IsRequestSubstituteInFlow = false;

                            requestSubstituteID = requests.Where(x => x.RequestID == reqId)
                                                          .Select(x => x.RequestSubstituteID)
                                                          .FirstOrDefault();
                            requestSubstitute = bRequestSubstitute.GetByID(requestSubstituteID);
                            if (requestSubstitute != null)
                            {
                                requestValidationProxy = new RequestKartablValidationProxy();
                                requestValidationProxy.PrecardName = requestSubstitute.Request.Precard.Name;
                                if (BLanguage.CurrentSystemLanguage == LanguagesName.English)
                                    requestValidationProxy.Date = requestSubstitute.Request.FromDate.ToShortDateString();
                                else
                                    requestValidationProxy.Date = Utility.ToPersianDate(requestSubstitute.Request.FromDate);
                                requestValidationProxy.PersonName = requestSubstitute.Request.Person.Name;

                                if (requestStatusList != null)
                                {
                                    IsRequestSubstituteInFlow = requestStatusList.Select(x => x.Request)
                                                                                 .ToList<Request>()
                                                                                 .Select(y => y.ID)
                                                                                 .Contains(reqId);
                                    if (IsRequestSubstituteInFlow)
                                    {
                                        UIValidationExceptions uiValidationExceptions = new UIValidationExceptions();
                                        ValidationException validationException = new ValidationException(ExceptionResourceKeys.RequestUsedByFlow, "درخواست در جریان کار قرار گرفته است", ExceptionSrc);
                                        uiValidationExceptions.ExceptionList.Add(validationException);
                                        throw uiValidationExceptions;
                                    }
                                    else
                                    {
                                        requestSubstitute.Confirmed = confirmed;
                                        requestSubstitute.Description = description;
                                        bRequestSubstitute.SaveChanges(requestSubstitute, UIActionType.EDIT);
                                    }
                                }

                            }
                            NHibernateSessionManager.Instance.CommitTransactionOn();
                        }
                        catch (Exception ex)
                        {
                            NHibernateSessionManager.Instance.RollbackTransactionOn();
                            BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequestSubstituteRequest");
                            requestValidationProxy.Exception = ex;
                            requestValidationProxyList.Add(requestValidationProxy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "SetStatusOfRequestSubstituteRequest");
                throw ex;
            }
            return true;
        }


        public IList<KartablFlowLevelProxy> GetRequestLevelsByRequestSubstituteID(decimal requestId, decimal substitutePersonID)
        {
            try
            {
                IList<KartablFlowLevelProxy> kartablFlowLevelProxyList = new List<KartablFlowLevelProxy>();
                BRequestSubstitute bRequestSubstitute = new BRequestSubstitute();
                RequestSubstitute requestSubstitute = bRequestSubstitute.GetRequestSubstitute(requestId, substitutePersonID);
                if (requestSubstitute != null)
                {
                    KartablFlowLevelProxy proxy = new KartablFlowLevelProxy();
                    proxy.ManagerName = requestSubstitute.SubstitutePerson.Name;
                    proxy.Description = requestSubstitute.Description;
                    if (requestSubstitute.Confirmed != null)
                    {
                        if (!(bool)requestSubstitute.Confirmed)
                            proxy.RequestStatus = RequestState.Unconfirmed;
                        else
                            proxy.RequestStatus = RequestState.Confirmed;

                        switch (BLanguage.CurrentLocalLanguage)
                        {
                            case LanguagesName.Parsi:
                                proxy.TheDate = Utility.ToPersianDate(requestSubstitute.OperationDate);
                                break;
                            case LanguagesName.English:
                                proxy.TheDate = Utility.ToString(requestSubstitute.OperationDate);
                                break;
                        }
                        proxy.TheTime = requestSubstitute.OperationDate.TimeOfDay.ToString();
                    }
                    else
                        proxy.RequestStatus = RequestState.UnderReview;

                    kartablFlowLevelProxyList.Add(proxy);
                }
                return kartablFlowLevelProxyList;
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BKartabl", "GetRequestLevelsByRequestSubstituteID");
                throw ex;
            }
        }
        //خروجی :درخواست های والد درخواست های لغو
        public IList<Request> GetRequestParent(IList<InfoRequest> infoRequestList)
        {
            IList<Request> RequestParent = new List<Request>();
            Request requestAlias = null;
            RequestParent = NHSession.QueryOver<Request>(() => requestAlias)
                                     .Where(() => requestAlias.ID.IsIn(infoRequestList.Where(y => y.ParentID != 0).Select(y => y.ParentID).ToArray()))
                                     .List<Request>();
            return RequestParent;
        }
        public IList<Request> GetRequestParent(IList<Request> infoRequestList)
        {
            Request requestAlias = null;
            IList<Request> RequestParent = NHSession.QueryOver<Request>(() => requestAlias)
                                       .Where(() => requestAlias.ID.IsIn(infoRequestList.Where(y => y.Parent != null && y.Parent.ID != 0).Select(y => y.Parent.ID).ToArray()))
                                       .List<Request>();
            return RequestParent;
        }
    }
}