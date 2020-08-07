using GTS.Clock.Model.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.OverTimeFlow
{
    /// <summary>
    /// جزئیات اضافه کار تشویقی
    /// </summary>
    public class OverTimeDetail : IEntity, IDisposable
    {
        public OverTimeDetail()
        {
            this.MaxHoliday = 0;
            this.MaxNightly = 0;
            this.MaxOverTime = 120;
        }

        #region Properties

        /// <summary>
        /// شناسه
        /// </summary>
        public virtual Decimal ID { get; set; }

        public virtual OverTime OverTime { get; set; }

        /// <summary>
        /// معاونت مربوطه از چارت سازمانی
        /// </summary>
        public virtual Department Department { get; set; }
         
        /// <summary>
        /// سرانه اضافه کاری تشویقی
        /// </summary>
        public virtual decimal MaxOverTime { get; set; }

        /// <summary>
        /// سرانه تعطیل کاری تشویقی
        /// </summary>
        public virtual decimal MaxHoliday { get; set; }

        /// <summary>
        /// سرانه شب کاری تشویقی
        /// </summary>
        public virtual decimal MaxNightly { get; set; }
         
        /// <summary>
        /// آخرین ثبت کننده یا ویرایش کننده
        /// </summary>
        public virtual Person ModifiedBy { get; set; }

        /// <summary>
        /// آخرین زمان ثبت یا ویرایش
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {

        }

        #endregion
    }
}
