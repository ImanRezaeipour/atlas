using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class OverTimePersonProxy
    {
        public OverTimePersonProxy()
        {
            this.MaxOverTime = 0;
            this.MaxHoliday = 0;
            this.MaxNightly = 0;
            this.RealMaxOverTime = 0;
            this.RealMaxHoliday = 0;
            this.RealMaxNightly = 0;
        }

        /// <summary>
        /// کلید اصلی
        /// </summary>
        public decimal ID { get; set; }

        /// <summary>
        /// کلید اصلی پرسنل
        /// </summary>
        public decimal PersonId { get; set; }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string PersonFullName { get; set; }

        /// <summary>
        /// کد پرسنلی
        /// </summary>
        public string PersonCode { get; set; }

        #region اطلاعات بودجه تشویقی

        /// <summary>
        /// پرسنل مجوز اضافه کاری تشویقی دارد یا خیر
        /// </summary>
        public bool HasOverTimeWork { get; set; }

        /// <summary>
        /// پرسنل مجوز تعطیل کاری تشویقی دارد یا خیر
        /// </summary>
        public bool HasHolidayWork { get; set; }

        /// <summary>
        /// پرسنل مجوز شب کاری تشویقی دارد یا خیر
        /// </summary>
        public bool HasNightWork { get; set; }

        /// <summary>
        /// سرانه اضافه کاری تشویقی پرسنل
        /// </summary>
        public decimal MaxOverTime { get; set; }

        /// <summary>
        ///  سرانه تعطیل کاری تشویقی پرسنل
        /// </summary>
        public decimal MaxHoliday { get; set; }

        /// <summary>
        /// سرانه شب کاری تشویقی پرسنل
        /// </summary>
        public decimal MaxNightly { get; set; }

        /// <summary>
        /// سرانه اضافه کاری تشویقی معاونت مربوطه
        /// </summary>
        public decimal RealMaxOverTime { get; set; }

        /// <summary>
        ///  سرانه تعطیل کاری تشویقی معاونت مربوطه
        /// </summary>
        public decimal RealMaxHoliday { get; set; }

        /// <summary>
        /// سرانه شب کاریس معاونت مربوطه
        /// </summary>
        public decimal RealMaxNightly { get; set; }

        #endregion

        #region اطلاعات کارکرد

        /// <summary>
        /// مرخصی استحقاقی روزانه
        /// </summary>
        public string DailyMeritoriouslyLeave { get; set; }

        /// <summary>
        /// مرخصی استعلاجی روزانه
        /// </summary>
        public string DailySickLeave { get; set; }

        /// <summary>
        /// مرخصی بی حقوق روزانه
        /// </summary>
        public string DailyWithoutPayLeave { get; set; }

        /// <summary>
        /// غیبت روزانه
        /// </summary>
        public string DailyAbsence { get; set; }

        /// <summary>
        /// کارکرد خالص روزانه
        /// </summary>
        public string DailyPureOperation { get; set; }

        /// <summary>
        /// اضافه کار مجاز
        /// </summary>
        public string AllowableOverTime { get; set; }

        /// <summary>
        /// اضافه کار غیر مجاز
        /// </summary>
        public string UnallowableOverTime { get; set; }

        #endregion
    }
}
