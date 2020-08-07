using GTS.Clock.Model.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.OverTimeFlow
{
    /// <summary>
    /// اضافه کار تشویقی
    /// </summary>
    public class OverTime : IEntity, IDisposable
    {
        public OverTime()
        {
            this.IsActive = true;
            this.IsApproved = false;
            this.ApprovedDate = DateTime.Now;
        }

        #region Properties

        /// <summary>
        /// شناسه
        /// </summary>
        public virtual Decimal ID { get; set; }


        /// <summary>
        /// تاریخ جهت نگهداری سال و ماه
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// بسته/باز بودن سرانه جهت ویرایش در آن ماه
        /// </summary>
        public virtual Boolean IsActive { get; set; }

        /// <summary>
        /// تاییدیه اداری جهت ارسال برای پرداخت
        /// </summary>
        public virtual Boolean IsApproved { get; set; }

        /// <summary>
        /// زمان تایید اداری
        /// </summary>
        public virtual DateTime ApprovedDate { get; set; }

        public virtual IList<OverTimeDetail> DetailList { get; set; }


        #endregion

        #region IDisposable Members
        public void Dispose()
        {

        }

        #endregion
    }
}
