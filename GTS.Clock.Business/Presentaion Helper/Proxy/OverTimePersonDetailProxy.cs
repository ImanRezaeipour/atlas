using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Business.Presentaion_Helper.Proxy
{
    public class OverTimePersonDetailProxy
    {
        public OverTimePersonDetailProxy()
        {
            this.HasOverTime = false;
            this.HasNightWork = false;
            this.HasHolidayWork = false;

            this.MaxNightWork = 0;
            this.MaxHolidayWork = 0;
            this.MaxOverTime = 0;
            this.IsArchiveEnable = false;
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


        /// <summary>
        /// ناهار
        /// "5"
        /// </summary>
        public string P1 { get; set; }
        public string P1Old { get; set; }

        /// <summary>
        /// تعطیل ناهار
        /// "5"
        /// </summary>
        public string P2 { get; set; }
        public string P2Old { get; set; }

        /// <summary>
        /// اضافه کاری
        /// "09:20"
        /// </summary>
        public string P3 { get; set; }
        public string P3Old { get; set; }

        /// <summary>
        /// تعطیل کاری
        /// "20"
        /// </summary>
        public string P4 { get; set; }
        public string P4Old { get; set; }
        /// <summary>
        /// شب کاری 
        /// "09:20"
        /// </summary>
        public string P5 { get; set; }
        public string P5Old { get; set; }

        /// <summary>
        /// مرخصی بی حقوق
        /// "25"
        /// </summary>
        public string P6 { get; set; }
        public string P6Old { get; set; }

        /// <summary>
        /// مرخصی استحقاقی
        /// "25"
        /// </summary>
        public string P7 { get; set; }
        public string P7Old { get; set; }

        /// <summary>
        /// استعلاجی
        /// "25"
        /// </summary>
        public string P8 { get; set; }
        public string P8Old { get; set; }

        /// <summary>
        /// غیبت
        /// "25"
        /// </summary>
        public string P9 { get; set; }
        public string P9Old { get; set; }

        /// <summary>
        /// کسر کار
        /// "06:46"
        /// </summary>
        public string P10 { get; set; }
        public string P10Old { get; set; }

        /// <summary>
        /// کارکرد
        /// "12"
        /// </summary>
        public string P11 { get; set; }
        public string P11Old { get; set; }

        /// <summary>
        /// کارکرد ساعتی
        /// "53:27"
        /// </summary>
        public string P12 { get; set; }
        public string P12Old { get; set; }

        public bool IsArchiveEnable { get; set; }
    }
}
