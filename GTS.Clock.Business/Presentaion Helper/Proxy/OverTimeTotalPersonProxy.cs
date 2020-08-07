using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class OverTimeTotalPersonProxy
    {
        public OverTimeTotalPersonProxy()
        {
            this.OverTimeCount = 0;
            this.HolidayCount = 0;
            this.NightlyCount = 0;
            this.MaxOverTime = 0;
            this.MaxHoliday = 0;
            this.MaxNightly = 0;
            this.RealMaxOverTime = 0;
            this.RealMaxHoliday = 0;
            this.RealMaxNightly = 0;
        }

        public int Total { get; set; }

        public int OverTimeCount { get; set; }
        /// <summary>
        /// سرانه اضافه کاری تشویقی پرسنل
        /// </summary>
        public decimal MaxOverTime { get; set; }
        /// <summary>
        /// سرانه اضافه کاری تشویقی معاونت مربوطه
        /// </summary>
        public decimal RealMaxOverTime { get; set; }
        public decimal RemainingOverTime
        {
            get { return RealMaxOverTime - MaxOverTime; }
        }
         
        public int HolidayCount { get; set; }
        /// <summary>
        ///  سرانه تعطیل کاری تشویقی پرسنل
        /// </summary>
        public decimal MaxHoliday { get; set; }
        /// <summary>
        ///  سرانه تعطیل کاری تشویقی معاونت مربوطه
        /// </summary>
        public decimal RealMaxHoliday { get; set; }
        public decimal RemainingHoliday
        {
            get { return RealMaxHoliday - MaxHoliday; }
        }
         
        public int NightlyCount { get; set; }
        /// <summary>
        /// سرانه شب کاری تشویقی پرسنل
        /// </summary>
        public decimal MaxNightly { get; set; }
        /// <summary>
        /// سرانه شب کاریس معاونت مربوطه
        /// </summary>
        public decimal RealMaxNightly { get; set; }
        public decimal RemainingNightly
        {
            get { return RealMaxNightly - MaxNightly; }
        }
    }
}
