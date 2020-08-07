using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GTS.Clock.Presentation.WebApi.ViewModels.OverTimePersonDetail
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

        }
        public decimal Id { get; set; }
        public string PersonFullName { get; set; }
        public DateTime Date { get; set; }
        public int MaxOverTime { get; set; }
        public int MaxHolidayWork { get; set; }
        public int MaxNightWork { get; set; }

        public bool HasOverTime { get; set; }
        public bool HasHolidayWork { get; set; }
        public bool HasNightWork { get; set; }
    }
}