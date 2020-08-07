using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.Api.ViewModel
{
    public class OverTimePersonDetailViewModel
    {
        public OverTimePersonDetailViewModel()
        {
            this.HasOverTime = false;
            this.HasNightWork = false;
            this.HasHolidayWork = false;

            this.MaxNightWork = 0;
            this.MaxHolidayWork = 0;
            this.MaxOverTime = 0;
            this.IsArchiveEnable = false;
            this.IsApprove = true;

        }
        public decimal Id { get; set; }
        public string PersonFullName { get; set; }
        public DateTime Date { get; set; }
        public decimal MaxOverTime { get; set; }
        public decimal MaxHolidayWork { get; set; }
        public decimal MaxNightWork { get; set; }

        public bool HasOverTime { get; set; }
        public bool HasHolidayWork { get; set; }
        public bool HasNightWork { get; set; }

        public decimal OldValueMaxOverTime { get; set; }
        public decimal OldValueMaxHoliday { get; set; }
        public decimal OldValueMaxNightly { get; set; }

        public bool IsApprove { get; set; }


        /// <summary>
        /// ناهار
        /// "5"
        /// </summary>
        public string P1 { get; set; }

        /// <summary>
        /// تعطیل ناهار
        /// "5"
        /// </summary>
        public string P2 { get; set; }

        /// <summary>
        /// اضافه کاری
        /// "09:20"
        /// </summary>
        public string P3 { get; set; }

        /// <summary>
        /// تعطیل کاری
        /// "20"
        /// </summary>
        public string P4 { get; set; }
        /// <summary>
        /// شب کاری 
        /// </summary>
        public string P5 { get; set; }

        /// <summary>
        /// مرخصی بی حقوق
        /// "25"
        /// </summary>
        public string P6 { get; set; }

        /// <summary>
        /// مرخصی استحقاقی
        /// "25"
        /// </summary>
        public string P7 { get; set; }

        /// <summary>
        /// استعلاجی
        /// "25"
        /// </summary>
        public string P8 { get; set; }

        /// <summary>
        /// غیبت
        /// "25"
        /// </summary>
        public string P9 { get; set; }

        /// <summary>
        /// کسر کار
        /// "06:46"
        /// </summary>
        public string P10 { get; set; }

        /// <summary>
        /// کارکرد
        /// "12"
        /// </summary>
        public string P11 { get; set; }

        /// <summary>
        /// کارکرد ساعتی
        /// "53:27"
        /// </summary>
        public string P12 { get; set; }

        public bool IsArchiveEnable { get; set; }

    }
}