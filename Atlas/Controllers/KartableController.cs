using GTS.Clock.Business.GridSettings;
using GTS.Clock.Business.RequestFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http; 
using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure.Exceptions; 
using GTS.Clock.Infrastructure;
using GTS.Clock.Business.Proxy;
using System.Web;
using Atlas.Api.ViewModel;

namespace Atlas.Api.Controllers
{

    public class KartableController : BaseController
    {
        #region Properties

        public BKartableGridClientSettings BkartableGridClientSettings
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BKartableGridClientSettings>();
            }
        }

        public IKartablRequests KartableBusiness
        {
            get
            {
                return (IKartablRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());

            }
        }

        public IReviewedRequests SurveyBusiness
        {
            get
            {
                return (IReviewedRequests)(BusinessHelper.GetBusinessInstance<BKartabl>());
            }
        }

        public BSentryPermits SentryBusiness
        {
            get
            {
                return BusinessHelper.GetBusinessInstance<BSentryPermits>();
            }
        }
         
        public BLanguage LangProv
        {
            get
            {
                return new BLanguage();
            }
        }
        public BKartabl bKartable
        {
            get
            {
                return new BKartabl();
            }
        }

        #endregion

        public DataTablePageData<KartablProxy> Get()
        {
            int draw;
            int pageSize;
            //خواندن اطلاعات ارسالی از طرف گرید
            var dtParams = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            if (dtParams.Count > 0)
            {
                //Paging Info
                draw = Convert.ToInt32(dtParams["draw"]);
                int start = Convert.ToInt32(dtParams["start"]);
                pageSize = Convert.ToInt32(dtParams["length"]);

                //Get Filter Info
                string firstName = dtParams["firstName"];
                string lastName = dtParams["lastName"];
                //Other

                //Get Sort Info
                //string sortColIndex = dtParams["order[0][column]"].ToString();
                //string sortColName = dtParams["columns[" + sortColIndex + "][data]"];
                //string sortDirection = dtParams["order[0][dir]"];
            }
            //-------------------------------------------------------------------------------
            else
            {
                pageSize = 20;
                //int pageIndex = 0;
                //string sortColName = "None";
                //string LoadState = "None";
                //int count = 0;
                draw = 1;
            }
            int pageIndex = 0;
            int month = 1;
            int year = 1395;
            int recordsTotal;
            IList<KartablProxy> KartableList = null;
            KartableList = this.KartableBusiness.GetAllRequests((RequestType)Enum.Parse(typeof(RequestType), "None"), year, month, pageIndex, pageSize, (KartablOrderBy)Enum.Parse(typeof(KartablOrderBy), "None"), out recordsTotal);


            return new DataTablePageData<KartablProxy>
            {
                data = KartableList,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal,
                draw = ++draw
            };
        }
    }
}
