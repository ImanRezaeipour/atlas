using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTS.Clock.Model.Concepts
{
      public class YearlyHolidayWorkGroups:IEntity
    {
          public virtual decimal ID { get; set; }
          public virtual CalendarType calendarType { get; set; }
          public virtual WorkGroup workGroup { get; set; }
          public virtual int Year { get; set; }
    }
}
