using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.OverTimeFlow
{
    public class ApprovalAttendanceScheduleException : IEntity, IDisposable
    {
        public ApprovalAttendanceScheduleException()
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


        public virtual ApprovalAttendanceSchedule ApprovalAttendanceSchedule { get; set; }

        public virtual Person Person { get; set; }

        #endregion


        #region IDisposable Members
        public void Dispose()
        {

        }

        #endregion

    }
}
