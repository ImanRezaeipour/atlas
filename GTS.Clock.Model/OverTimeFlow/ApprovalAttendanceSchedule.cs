using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.BaseInformation;

namespace GTS.Clock.Model.OverTimeFlow
{
    /// <summary>
    /// زمان بندی تایید کارکرد ماهیانه پرسنل و کارتابل تایید اضافه کار تشویقی مدیران و کارتابل اضافه کار تشویقی اداری
    /// </summary>
    public class ApprovalAttendanceSchedule : IEntity, IDisposable
    {
        public ApprovalAttendanceSchedule()
        {

        }

        #region Properties

        /// <summary>
        /// شناسه
        /// </summary>
        public virtual Decimal ID { get; set; }

        /// <summary>
        /// تاریخ ابتدای بازه
        /// </summary>
        public virtual DateTime DateFrom { get; set; }

        /// <summary>
        /// تاریخ انتهای بازه
        /// </summary>
        public virtual DateTime DateTo { get; set; }

        /// <summary>
        /// نوع تاییدیه
        /// </summary>
        public virtual ApprovalScheduleType ApprovalType { get; set; }

        /// <summary>
        /// کد دوره کارکرد
        /// </summary>
        public virtual int DateRangeOrder { get; set; }

        public virtual CostCenter CostCenter { get; set; }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {

        }

        #endregion
    }
}
