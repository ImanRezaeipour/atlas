using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Business.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Concepts;
using GTS.Clock.Model;
using GTS.Clock.Business.WorkFlow;
using GTS.Clock.Model.RequestFlow;

namespace GTS.Clock.Business.RequestFlow
{
    public class BSentryPermits : MarshalByRefObject
    {
        IDataAccess accessPort = new BUser();
        PersonRepository prsRep = new PersonRepository(false);
        BPermit busPermit = new BPermit();
        ISearchPerson searchTool = new BPerson();

        public int GetPermitCount(RequestType requestType, string theDate)
        {
            try
            {
                return this.GetPermitCount(null, requestType, theDate);
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetPermitCount");
                throw ex;
            }
        }

        public int GetPermitCount(string searchKey, string theDate)
        {
            try
            {
                return this.GetPermitCount(searchKey, RequestType.None, theDate);
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetPermitCount");
                throw ex;
            }
        }

        private int GetPermitCount(string searchKey, RequestType requestType, string theDate)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(theDate);
                }
                else
                {
                    date = Utility.ToMildiDateTime(theDate);
                }

                //IList<decimal> controlStationIds = accessPort.GetAccessibleControlStations();
                List<decimal> prsIds = new List<decimal>();
                IList<decimal> precardIds = accessPort.GetAccessiblePrecards();

                if (searchKey != null)
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds.AddRange(ids.ToList<decimal>());
                }
                else
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch("", PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds = ids.ToList<decimal>();
                }

                IList<InfoRequest> requestList = new RequestRepository(false).GetAllRequestMinOneLevelConfirm(prsIds, precardIds, date, SentryPermitsOrderBy.PermitSubject);
                IList<Precard> precardList = new BPrecard().GetAll();
                switch (requestType)
                {
                    case RequestType.Daily:
                        requestList = requestList.Where(x => precardList.Any(y => y.IsDaily && y.ID == x.PrecardID)).ToList();
                        break;
                    case RequestType.Hourly:
                        requestList = requestList.Where(x => precardList.Any(y => y.IsHourly && y.ID == x.PrecardID)).ToList();
                        break;
                }

                int count = requestList.Count;
                return count;
            }
            catch (Exception ex)
            {

                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetPermitCount");
                throw ex;
            }
        }

        public IList<KartablProxy> GetAllPermits(RequestType requestType, string theDate, bool isEndFlowRequestsView, int pageIndex, int pageSize, SentryPermitsOrderBy orderby, out int count)
        {
            try
            {
                return this.GetAllPermits(null, requestType, theDate, isEndFlowRequestsView, pageIndex, pageSize, orderby, out count);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetAllPermits");
                throw ex;
            }
        }

        public IList<KartablProxy> GetAllPermits(string searchKey, string theDate, bool isEndFlowRequestsView, int pageIndex, int pageSize, SentryPermitsOrderBy orderby, out int count)
        {
            try
            {
                return this.GetAllPermits(searchKey, RequestType.None, theDate, isEndFlowRequestsView, pageIndex, pageSize, orderby, out count);
            }
            catch (Exception ex)
            {
                BaseBusiness<Entity>.LogException(ex, "BSentryPermits", "GetAllPermits");
                throw ex;
            }
        }

        private IList<KartablProxy> GetAllPermits(string searchKey, RequestType requestType, string theDate, bool isEndFlowRequestsView, int pageIndex, int pageSize, SentryPermitsOrderBy orderby, out int count)
        {
            try
            {
                DateTime date;
                if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                {
                    date = Utility.ToMildiDate(theDate);
                }
                else
                {
                    date = Utility.ToMildiDateTime(theDate);
                }

                //IList<decimal> controlStationIds = accessPort.GetAccessibleControlStations();
                List<decimal> prsIds = new List<decimal>();
                IList<decimal> precardIds = accessPort.GetAccessiblePrecards();

                if (searchKey != null)
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch(searchKey, PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds.AddRange(ids.ToList<decimal>());
                }
                else
                {
                    IList<Person> quciSearchInUnderManagment = searchTool.QuickSearch("", PersonCategory.Sentry_UnderManagment);
                    var ids = from o in quciSearchInUnderManagment
                              select o.ID;
                    prsIds = ids.ToList<decimal>();
                }

                IList<InfoRequest> requestList = new RequestRepository(false).GetAllRequestMinOneLevelConfirm(prsIds, precardIds, date, orderby);
                if (requestList == null)
                    requestList = new List<InfoRequest>();
                IList<Precard> precardList = new BPrecard().GetAll();
                switch (requestType)
                {
                    case RequestType.Daily:
                        requestList = requestList.Where(x => precardList.Any(y => y.IsDaily && y.ID == x.PrecardID) && x.LookupKey != PrecardGroupsName.terminate.ToString() && ((x.ChildsCount != 0 && x.ChildsUnConfirmCount != 0 && x.ChildsCount == x.ChildsUnConfirmCount) || x.ChildsCount == 0)).ToList();
                        break;
                    case RequestType.Hourly:
                        requestList = requestList.Where(x => precardList.Any(y => y.IsHourly && y.ID == x.PrecardID) && x.LookupKey != PrecardGroupsName.overwork.ToString() && x.LookupKey != PrecardGroupsName.imperative.ToString() && x.LookupKey != PrecardGroupsName.terminate.ToString() && ((x.ChildsCount != 0 && x.ChildsUnConfirmCount != 0 && x.ChildsCount == x.ChildsUnConfirmCount) || x.ChildsCount == 0)).ToList();
                        break;
                    case RequestType.Monthly:
                        requestList = requestList.Where(x => precardList.Any(y => y.IsMonthly && y.ID == x.PrecardID) && x.LookupKey != PrecardGroupsName.terminate.ToString() && ((x.ChildsCount != 0 && x.ChildsUnConfirmCount != 0 && x.ChildsCount == x.ChildsUnConfirmCount) || x.ChildsCount == 0)).ToList();
                        break;
                    case RequestType.OverWork:
                        requestList = requestList.Where(x => x.LookupKey == PrecardGroupsName.overwork.ToString() && ((x.ChildsCount != 0 && x.ChildsUnConfirmCount != 0 && x.ChildsCount == x.ChildsUnConfirmCount) || x.ChildsCount == 0)).ToList();
                        break;
                    case RequestType.Imperative:
                        requestList = requestList.Where(x => x.LookupKey == PrecardGroupsName.imperative.ToString()).ToList();
                        break;
                    case RequestType.Terminate:
                        requestList = requestList.Where(x => x.ParentID != null ).ToList();
                        break;
                    case RequestType.None:
                        requestList = requestList.Where(x => x.LookupKey != PrecardGroupsName.terminate.ToString() && ((x.ChildsCount != 0 && x.ChildsUnConfirmCount != 0 && x.ChildsCount == x.ChildsUnConfirmCount) || x.ChildsCount == 0)).ToList();
                        break;
                }
                if (isEndFlowRequestsView)
                    requestList = requestList.Where(x => x.Confirm.HasValue && x.Confirm.Value).ToList();

                count = requestList.Count;
                requestList = requestList.Skip(pageSize * pageIndex).Take(pageSize).ToList();


                IList<KartablProxy> kartablResult = new List<KartablProxy>();
                int counter = 0;
                foreach (InfoRequest request in requestList)
                {
                    counter++;
                    KartablProxy proxy = new KartablProxy();
                    if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
                    {
                        proxy.TheFromDate = Utility.ToPersianDate(request.FromDate);
                        proxy.TheToDate = Utility.ToPersianDate(request.ToDate);
                    }
                    else
                    {
                        proxy.TheFromDate = Utility.ToString(request.FromDate);
                        proxy.TheToDate = Utility.ToString(request.ToDate);
                    }
                    proxy.ID = request.ID;
                    proxy.RequestID = request.ID;
                    proxy.TheFromTime = Utility.IntTimeToRealTime(request.FromTime);
                    proxy.TheToTime = Utility.IntTimeToRealTime(request.ToTime);
                    proxy.Row = counter;
                    proxy.RequestTitle = request.PrecardName;
                    proxy.Applicant = request.Applicant;
                    proxy.PersonImage = request.PersonImage;
                    proxy.Barcode = request.PersonCode;
                    proxy.PersonId = request.PersonID;
                    proxy.Description = request.Description;
                    string name = request.LookupKey;
                    PrecardGroupsName groupName = (PrecardGroupsName)Enum.Parse(typeof(PrecardGroupsName), name);
                    if (groupName == PrecardGroupsName.overwork)
                    {
                        proxy.RequestType = RequestType.OverWork;

                        //تنظیم زمان ابتدا و انتها
                        //درخواست بازه ای بدون انتدا و انتها
                        if (request.TimeDuration > 0 && request.FromTime == 1439 && request.ToTime == 1439)
                        {
                            proxy.TheFromTime = proxy.TheToTime = "";
                        }
                    }
                    else if (groupName == PrecardGroupsName.imperative)
                    {
                        proxy.RequestType = RequestType.Imperative;
                    }
                    else if (request.IsHourly)
                    {
                        proxy.RequestType = RequestType.Hourly;
                    }
                    else if (request.IsDaily)
                    {
                        proxy.RequestType = RequestType.Daily;
                    }
                    else if (request.IsMonthly)
                    {
                        proxy.RequestType = RequestType.Monthly;
                    }
                    else
                    {
                        proxy.RequestType = RequestType.None;
                    }
                    proxy.DepartmentId = request.DepartmentId;
                    proxy.DepartmentName = request.DepartmentName;
                    kartablResult.Add(proxy);

                }
                return kartablResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ServiceAuthorizeBehavior(ServiceAuthorizeState.Enforce)]
        public void CheckSentryLoadAccess()
        {
        }

    }
}
