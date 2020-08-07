using GTS.Clock.Business.AppSettings;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Presentation.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GTS.Clock.Presentation.WebApi.Api
{

    [RoutePrefix("api/yearmonth")]
    [Route("{action}")]

    public class YearMonthController : ApiController
    {
        static List<SelectItem> yearItems = new List<SelectItem>();
        static List<SelectItem> monthItems = new List<SelectItem>();

        static YearMonthController()
        {
            if (BLanguage.CurrentSystemLanguage == LanguagesName.Parsi)
            {
                var persianDatetime = Utility.ToPersianDateTime(DateTime.Now);
                int currentYear = persianDatetime.Year;
                for (var i = (currentYear - 1); i <= (currentYear + 1); i++)
                {
                    yearItems.Add(new SelectItem { id = i, text = i.ToString() });
                }
                //----------------------------------------------------------------
                monthItems.Add(new SelectItem { id = 1, text = "فروردین" });
                monthItems.Add(new SelectItem { id = 2, text = "اردیبهشت" });
                monthItems.Add(new SelectItem { id = 3, text = "خرداد" });
                monthItems.Add(new SelectItem { id = 4, text = "تیر" });
                monthItems.Add(new SelectItem { id = 5, text = "مرداد" });
                monthItems.Add(new SelectItem { id = 6, text = "شهریور" });
                monthItems.Add(new SelectItem { id = 7, text = "مهر" });
                monthItems.Add(new SelectItem { id = 8, text = "آبان" });
                monthItems.Add(new SelectItem { id = 9, text = "آذر" });
                monthItems.Add(new SelectItem { id = 10, text = "دی" });
                monthItems.Add(new SelectItem { id = 11, text = "بهمن" });
                monthItems.Add(new SelectItem { id = 12, text = "اسفند" });
            }
            else
            {
                int currentYear = DateTime.Now.Year;
                for (var i = (currentYear - 1); i <= (currentYear + 1); i++)
                {
                    yearItems.Add(new SelectItem { id = i, text = i.ToString() });
                }
                //----------------------------------------------------------------
                monthItems.Add(new SelectItem { id = 1, text = "January" });
                monthItems.Add(new SelectItem { id = 2, text = "February" });
                monthItems.Add(new SelectItem { id = 3, text = "March" });
                monthItems.Add(new SelectItem { id = 4, text = "April" });
                monthItems.Add(new SelectItem { id = 5, text = "May" });
                monthItems.Add(new SelectItem { id = 6, text = "June" });
                monthItems.Add(new SelectItem { id = 7, text = "July" });
                monthItems.Add(new SelectItem { id = 8, text = "August" });
                monthItems.Add(new SelectItem { id = 9, text = "September" });
                monthItems.Add(new SelectItem { id = 10, text = "October" });
                monthItems.Add(new SelectItem { id = 11, text = "November" });
                monthItems.Add(new SelectItem { id = 12, text = "December" });
            }

        }
         
        public SelectPageData GetMonths()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var page = s2Param["page"];
            var size = s2Param["size"];
            var q = s2Param["q"];

            int pageIndex = page == null ? 0 : Convert.ToInt32(page) - 1;
            int pageSize = Convert.ToInt32(size);

            IQueryable<SelectItem> result = monthItems.AsQueryable();

            if (q != null)
            {
                result = result.Where(item => item.text.Contains(q.ToString()));
            }

            return new SelectPageData
            {
                items = result.Skip(pageIndex * pageSize).Take(pageSize),
                total_count = monthItems.Count
            };
        }
         
        public SelectPageData GetYears()
        {
            var s2Param = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var page = s2Param["page"];
            var size = s2Param["size"];
            var q = s2Param["q"];

            int pageIndex = page == null ? 0 : Convert.ToInt32(page) - 1;
            int pageSize = Convert.ToInt32(size);

            IQueryable<SelectItem> result = yearItems.AsQueryable();

            if (q != null)
            {
                result = result.Where(item => item.text.Contains(q.ToString()));
            }

            return new SelectPageData
            {
                items = result.Skip(pageIndex * pageSize).Take(pageSize),
                total_count = yearItems.Count
            };
        }
    }
}
